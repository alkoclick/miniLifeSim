# miniLifeSim
An attempt at a small-scale model of a life form

# How do I use this?
It's a unity project, just clone and open. It's meant to be run in the editor

# What am I seeing?
The small green cubes are nutritional elements. They stand still. The small moving balls are organisms (micro or macro - you decide). They move randomly

# What's going on?
The organisms survive on nutrition, of which there are several types. When they have more than a threshold (double their starting life, by default), they produce an offspring. The organisms return nutrients back to the environment in a different form when they have processed it. On the explorer to the left you can see the currently living organisms. Their name also displays what they ingest and what they egest.

# So they eat and poo. Anything else?
They evolve! Using a modified genetic evolutionary algorithm, each child has a chance to change its diet or range of consumption (more evolutions on the way). Which is why after a while the system reaches a balance where there are enough members of every "kind" and they survive on other "kinds'" emissions

# Why is everything public?
So you can have a quick overview by selecting each object. Normally all the "State" classes would be private (and some backing fields too)

# How can I play with this?
You can go to the prefabs folder and change some variables there:
- Environment (Open up the config panel in the Environment script, inside the editor):
  You can change the amount of different nutrients that will be spawned
  You can change the total amount of nutrients that will be spawned
  
- Organism (Open up the config panel in the Eating script, inside the editor)
  You can change the starting health (this will also affect reproduction, since it happens only above 2*starting health)
  You can change the death rate (how much energy an organism loses per sec)
  You can change the evolution rate (probably the most interesting variable in the simulator)
