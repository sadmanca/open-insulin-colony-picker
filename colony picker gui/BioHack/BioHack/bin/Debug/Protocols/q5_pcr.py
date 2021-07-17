from opentrons import instruments, robot, labware
import json
from math import ceil

metadata = {
    'protocolName': 'Q5 PCR',
    'author': 'BioHackathon',
    'description': 'This version has a hard limit of 96',
}


with open("q5_pcr.json", "r") as infile:
    var_json = json.loads(infile.read())


use_enhancer = True
amount = var_json["Amount"]

cooled_epi = labware.load(var_json["Labware"][0]["labware"], var_json["Labware"][0]["Slot"])
cooled_pcr = labware.load(var_json["Labware"][1]["labware"], var_json["Labware"][1]["Slot"])
template_96 = labware.load(var_json["Labware"][4]["labware"], var_json["Labware"][4]["Slot"]) 
falcon_50 = labware.load(var_json["Labware"][2]["labware"], var_json["Labware"][2]["Slot"])
tiprack_01 = labware.load(var_json["Labware"][3]["labware"], var_json["Labware"][3]["Slot"])


# Define Liquids
q5_polymerase = cooled_epi.wells(var_json["Liquids"][0]["Slot"])
q5_buffer = cooled_epi.wells(var_json["Liquids"][1]["Slot"])
dntp = cooled_epi.wells(var_json["Liquids"][2]["Slot"])
enhancer = cooled_epi.wells(var_json["Liquids"][3]["Slot"])
nucl_free_water = falcon_50.wells(var_json["Liquids"][6]["Slot"])
prim_fwd = cooled_epi.wells(var_json["Liquids"][4]["Slot"])
prim_rev = cooled_epi.wells(var_json["Liquids"][5]["Slot"])
templates = template_96.wells(var_json["Liquids"][7]["Slot"]) #MULTIPLE ONES <- Use a 
master_mix = cooled_epi.wells(var_json["Liquids"][8]["Slot"])


# Define Pipettes
p10 = instruments.P10_Single(
    mount='left',
    tip_racks=[tiprack_01])

p300 = instruments.P300_Single(mount='right')

### MASTER MIX ###
# Water first

water_vol = 22.5 # RECHNUNG HIER

p10.transfer(
    water_vol,
    nucl_free_water,
    master_mix,
    carryover = True,
    new_tip = 'once'
)

p10.transfer(
    10*amount,
    q5_buffer,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

p10.transfer(
    1*amount,
    dntp,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

if enhancer == True:
    p10.transfer(
        10*amount,
        enhancer,
        master_mix,
        carryover = True,
        new_tip = 'always'
    )

p10.transfer(
    ceil(amount*.5),
    q5_polymerase,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

### Master mix is done
### 

p10.transfer(
    44,
    master_mix,
    cooled_pcr[0:amount],
    carryover = True,
    new_tip = 'once'    
)

for i in range(amount):
    p10.transfer(
        1,
        templates[i],
        cooled_pcr[i],
        new_tip = 'always'
    )

p10.transfer(
    2.5,
    prim_fwd,
    cooled_pcr[0:amount],
    new_tip = 'always'
)

p10.transfer(
    2.5,
    prim_rev,
    cooled_pcr[0:amount],
    new_tip = 'always'
)

