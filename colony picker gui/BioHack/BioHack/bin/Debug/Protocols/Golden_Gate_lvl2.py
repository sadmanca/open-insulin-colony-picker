from opentrons import robot, labware, instruments
from math import ceil, floor
import json


with open("Golden_Gate_lvl2.json", "r") as infile:
    var_json = json.loads(infile.read())

VOLUME = 10 # Muss noch geändert werden

metadata = {
    'protocolName': 'Golden Gate Level 2',
    'author': 'BioHackathon',
    'description': 'Golden Gate Level 2 Construct',
}

amount = var_json["Amount"]

cooled_epi = labware.load(var_json["Labware"][0]["labware"], var_json["Labware"][0]["Slot"])
cooled_pcr = labware.load(var_json["Labware"][1]["labware"], var_json["Labware"][1]["Slot"])
falcon_50 = labware.load(var_json["Labware"][2]["labware"], var_json["Labware"][2]["Slot"])
warm_epi = labware.load(var_json["Labware"][3]["labware"], var_json["Labware"][3]["Slot"])
tiprack_01 = labware.load(var_json["Labware"][4]["labware"], var_json["Labware"][4]["Slot"])
#dna_96 = labware.load(var_json["Labware"][5]["labware"], var_json["Labware"][5]["Slot"])

p10 = instruments.P10_Single(
    mount='left',
    tip_racks=[tiprack_01])

p300 = instruments.P300_Single(mount='right')

# Define the Liquid
lvl1_insert = warm_epi.wells(var_json["Liquids"][1]["Slot"])
t4_ligase_buffer = cooled_epi.wells(var_json["Liquids"][2]["Slot"])
t4_ligase = cooled_epi.wells(var_json["Liquids"][3]["Slot"])
esp3i = cooled_epi.wells(var_json["Liquids"][4]["Slot"])
nucl_free_water = falcon_50.wells(var_json["Liquids"][6]["Slot"])
master_mix = cooled_epi.wells(var_json["Liquids"][5]["Slot"])

golden_gate = cooled_pcr

### PROTOCOL START ###
uneven_val = ceil(amount*.5)

# CREATING MASTER MIX
p10.transfer(
    VOLUME*amount,
    nucl_free_water,
    master_mix,
    carryover = True,
    new_tip = 'once'
)

p10.transfer(
    amount,
    t4_ligase_buffer,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

p10.transfer(
    uneven_val,
    t4_ligase,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

p10.transfer(
    uneven_val,
    esp3i,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

# 
master_mix_vol = 3*uneven_val + amount + VOLUME

p10.transfer(
    master_mix_vol//amount,
    master_mix,
    cooled_pcr[0:amount],
    mix_before = (5, 10),    # Mix the master mix
    new_tip = 'once'
)

# Need to change this, one:one, needs to be many:one
for i in range(amount):
    p10.transfer(
        1,
        lvl1_insert, #MULTIPLE ONES
        cooled_pcr[i],
        new_tip='always'
    )


