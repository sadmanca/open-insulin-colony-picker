from opentrons import robot, labware, instruments
import json

# metadata
metadata = {
    'protocolName': 'Dilution for UC-Davis',
    'author': 'Cedric K. Brinkmann, based on protocol from iGEM UC-Davis 2019',
    'description': 'A simple dilution protocol for the collaboration with UC-Davis',
}

with open("UC-Davis_Dilution_Protocol.json", "r") as infile:
    var_json = json.loads(infile.read())

### LABWARE ###
tiprack_01 = labware.load(var_json["Labware"][0]["labware"], var_json["Labware"][0]["Slot"]) 
falcon = labware.load(var_json["Labware"][1]["labware"], var_json["Labware"][1]["Slot"]) 
plate = labware.load(var_json["Labware"][2]["labware"], var_json["Labware"][2]["Slot"])

### Liquids ###
fluro_stock = falcon.wells( var_json["Liquids"][0]["Slot"])
pbs_stock = falcon.wells(var_json["Liquids"][1]["Slot"])

### Pipettes ###
p10 = instruments.P10_Single(mount='left')
p300 = instruments.P300_Single(
    mount='right',
    tip_racks=[tiprack_01])


p300.transfer(200, 
    fluro_stock,
    plate.wells('A1', 'B1', 'C2', 'D1'),
    new_tip = 'once',
    strategy = 'arc')

wells = [8, 16, 24, 32, 40, 48, 56, 64, 72, 80, 88,
    9, 17, 25, 33, 41, 49, 57, 65, 73, 81, 89,
    10, 18, 26, 34, 42, 50, 58, 66, 74, 82, 90,
    11, 19, 27, 35, 43, 51, 59, 67, 75, 83, 91
]

p300.transfer(
    100,
    pbs_stock,
    plate.wells(wells),   
    new_tip = 'once',
    strategy = 'arc')

for j in range(4): # From A to D
    p300.drop_tip()
    p300.pick_up_tip()
    for i in range(11): # From well 1 to 12
        p300.transfer(100,
        plate.wells(i*8+j),
        plate.wells((i+1)*8+j), 
        new_tip = 'never', 
        strategy = 'direct',
        mix_after = (3, 50)) 
    