import opentrons.simulate

import sys

file = open('Golden_Gate_lvl0.py')

s = opentrons.simulate.simulate(file)

output = ''
for i in range(len(s)):
	output += s[i]['payload']['text'] + '\n'
	

write = open('Results.txt', 'a')
write.write(output)
write.close()


print('exit')

sys.exit()