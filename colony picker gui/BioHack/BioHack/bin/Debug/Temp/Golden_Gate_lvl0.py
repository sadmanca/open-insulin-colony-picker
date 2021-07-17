from opentrons import robot, labware, instruments
from math import ceil, floor
import json

with open("Golden_Gate_lvl0.json", "r") as infile:
    var_json = json.loads(infile.read())

VOLUME = 10

### !!! DNA INSERT WRONG; NEEDS TO BE FROM A PCR !!! ###
### Have to dilute the DNA before 1:2 ###

# Metadata
metadata = {
    'protocolName': 'Golden Gate Level 0',
    'author': 'BioHackathon',
    'description': 'This version has a hard limit of 96',
}

# VARIABLE | Amount of reactions
amount = var_json["Amount"]

# Define the Labware that is used // Hard 
cooled_epi = labware.load(var_json["Labware"][0]["labware"], var_json["Labware"][0]["Slot"])
cooled_pcr = labware.load(var_json["Labware"][1]["labware"], var_json["Labware"][1]["Slot"])
falcon_50 = labware.load(var_json["Labware"][2]["labware"], var_json["Labware"][2]["Slot"])
warm_epi = labware.load(var_json["Labware"][3]["labware"], var_json["Labware"][3]["Slot"])
tiprack_01 = labware.load(var_json["Labware"][4]["labware"], var_json["Labware"][4]["Slot"])
dna_96 = labware.load(var_json["Labware"][5]["labware"], var_json["Labware"][5]["Slot"])

p10 = instruments.P10_Single(
    mount='left',
    tip_racks=[tiprack_01])

p300 = instruments.P300_Single(mount='right')

# Define the Liquids
dna_insert = dna_96.wells(var_json["Liquids"][0]["Slot"])
entry_vector = warm_epi.wells(var_json["Liquids"][1]["Slot"])
t4_ligase_buffer = cooled_epi.wells(var_json["Liquids"][2]["Slot"])
t4_ligase = cooled_epi.wells(var_json["Liquids"][3]["Slot"])
esp3i = cooled_epi.wells(var_json["Liquids"][4]["Slot"])
nucl_free_water = falcon_50.wells(var_json["Liquids"][6]["Slot"])
master_mix = cooled_epi.wells(var_json["Liquids"][5]["Slot"])

golden_gate = cooled_pcr

### PROTOCOL START ###
uneven_val = ceil(amount*.5)


# water, ligase buffer, ligase, restriction, entry vector
# CREATING MASTER MIX
p10.transfer(
    VOLUME,
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

p10.transfer(
    uneven_val,
    entry_vector,
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

for i in range(amount):
    p10.transfer(
        1,
        dna_insert, #MULTIPLE ONES
        cooled_pcr[i],
        new_tip='always'
    )




# Dispense to PCR


# Protocol

# Do Master Mix first
#   Pipette everything together except the DNA insert
#       Pipette it on ice Destination 

