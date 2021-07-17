from opentrons import instruments, robot, labware
import json
from math import ceil
import os

metadata = {
    'protocolName': 'Golden Gate Level 1',
    'author': 'BioHackathon',
    'description': 'Click and Clone Clone',
}

### HARD LIMIT OF 57 ASSEMBLES ###

with open("protocols/Golden_Gate_lvl1.json", "r") as infile:
    var_json = json.loads(infile.read())

os.chdir('../') #enable going to the tmp directory

with open("tmp/Golden_Gate_lvl1.tmp.json", "r") as ggfile:
    gg_json = json.loads(ggfile.read())

task_num = len(gg_json["Tasks"])


# Load the Labware
cooled_epi = labware.load(var_json["Labware"][1]["labware"], var_json["Labware"][1]["Slot"])
cooled_pcr = labware.load(var_json["Labware"][2]["labware"], var_json["Labware"][2]["Slot"])
toolbox_384 = labware.load(var_json["Labware"][0]["labware"], var_json["Labware"][0]["Slot"]) 
falcon_50 = labware.load(var_json["Labware"][3]["labware"], var_json["Labware"][3]["Slot"])
tiprack_01 = labware.load(var_json["Labware"][4]["labware"], var_json["Labware"][4]["Slot"])

tipracks = []
tipracks.append(tiprack_01)
for i in range(5,len(var_json["Labware"])):
    print(var_json["Labware"])
    tipracks.append(labware.load(var_json["Labware"][i]["labware"], var_json["Labware"][i]["Slot"]))


# Define Pipettes
p10 = instruments.P10_Single(
    mount='left',
    tip_racks=tipracks)

p300 = instruments.P300_Single(mount='right')

# Define Liquids
t4_buffer  = cooled_epi.wells(var_json["Liquids"][0]["Slot"])
t4_ligase = cooled_epi.wells(var_json["Liquids"][1]["Slot"])
bsaiv2 = cooled_epi.wells(var_json["Liquids"][2]["Slot"])
nucl_free_water = falcon_50.wells(var_json["Liquids"][3]["Slot"])
master_mix = cooled_epi.wells(var_json["Liquids"][4]["Slot"])

# Get number of Task
task_num = len(gg_json["Tasks"])
water_vol = 11 #NOT CORRECT

# Create Master Mix
p10.transfer(
    water_vol*task_num,
    nucl_free_water,
    master_mix,
    carryover = True,
    new_tip = 'once'
)

p10.transfer(
    task_num,
    t4_buffer,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

p10.transfer(
    ceil(task_num*.5),
    t4_ligase,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

p10.transfer(
    ceil(task_num*.5),
    bsaiv2,
    master_mix,
    carryover = True,
    new_tip = 'always'
)

# Master Mix Done

# Pipette into the PCR wells
p10.transfer(
    10,
    master_mix,
    cooled_pcr[0:task_num],
    new_tip = 'once'
)

# Pipette DNA into PCR
i = 0
for ids in gg_json["Tasks"]:
    for slt in ids["Parts"]:
        p10.transfer(
            1,
            toolbox_384[slt["Slot"]],
            cooled_pcr[i],
            new_tip = 'always'
        )
        i += 1
