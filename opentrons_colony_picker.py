
from opentrons.types import Point
from opentrons.types import Point, Location

def get_values(*names):
    import json
    _all_values = json.loads("""{"csv_samp":"X,Y\\n109.75,74.02\\n108.65,73.93\\n35.87,72.82\\n36.62,72.63\\n38.34,73.92\\n39.37,72.64\\n18.42,68.61\\n19.4,68.29\\n20.03,66.23\\n21.01,65.41\\n22.63,64.45\\n115.17,58.51\\n115.6,57.23\\n44.9,53.87\\n65.4,12.33\\n66.49,11.69\\n37.31,75.75\\n105.97,72.5\\n64.21,72.36\\n51.91,72.25\\n65.98,71.49\\n51.57,70.67\\n113.45,70.99\\n63.28,69.77\\n38.29,69.81\\n35.97,66.51\\n116.49,66.02\\n58.94,64.06\\n53.23,63.94\\n46.48,63.16\\n116.02,61.81\\n116.63,58.42\\n45.82,56.97\\n37.83,56.95\\n64.06,55.22\\n109.41,52.08\\n99.83,51.26\\n100.22,49.87\\n109.09,47.76\\n103.8,46.95\\n108.43,45.13\\n97.76,44.97\\n102.91,44.06\\n107.64,43.77\\n101.18,40.02\\n107.43,39.44\\n103.59,39.54\\n106.82,38.09\\n68.4,36.69\\n66.81,19.16\\n59.49,17.8\\n29.69,11.94\\n47.89,10.84\\n56.6,10.45\\n"}""")
    return [_all_values[n] for n in names]


metadata = {
    'protocolName': 'SuperScript III: qRT-PCR Prep with CSV File',
    'author': 'Sadman Hossain',
    'source': 'Custom Protocol Request',
    'apiLevel': '2.7'
}


def run(ctx):
    [csv_samp] = get_values(  # noqa: F821
        "csv_samp")

    # Labware
    dest_plate = ctx.load_labware('corning_96_wellplate_360ul_flat', '3')
    tiprack = [ctx.load_labware('opentrons_96_filtertiprack_200ul', '6')]

    # instrument
    p300 = ctx.load_instrument('p300_single_gen2', 'left', tip_racks=tiprack)

    csv = [[val.strip() for val in line.split(',')]
           for line in csv_samp.splitlines()
           if line.split(',')[0].strip()][1:]

    # FIND REFERENCE POINT - ACTION NEEDED -
    # DELETE AFTER FINDING X_NAUGHT, Y_NAUGHT
    # dummy numbers, with three pauses to test multiple points
    p300.pick_up_tip()
    p300.move_to(Location(Point(14.6, 20, 8.5), None))
    ctx.pause()
    p300.move_to(Location(Point(14.1, 14, 8), None))
    ctx.pause()
    p300.move_to(Location(Point(15.6, 12, 8.2), None))
    ctx.pause()

    # calibration
    p300.aspirate(3, dest_plate.wells()[0])
    p300.dispense(3, dest_plate.wells()[0])
    p300.drop_tip()

    # initial variables
    vol = 20
    x_naught = 14.38
    y_naught = 11.24
    z = 8

    for row, well in zip(csv, dest_plate.wells()):
        ctx.comment('\n\n')
        dx = float(row[0])
        dy = float(row[1])
        loc = Location(Point(x_naught+dx, y_naught+dy, z), None)
        p300.pick_up_tip()
        p300.aspirate(vol, loc)
        p300.dispense(vol, well)
        p300.drop_tip()
