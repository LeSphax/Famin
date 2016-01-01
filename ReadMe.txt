

This is a small game I created to practice game design and development on Unity.
I really enjoyed playing the game CivClicker http://civclicker.sourceforge.net/ and I tried to make it multiplayer.
At this point, I feel the game isn’t really fun, as soon as one player starts winning I don’t see how the other could come back. I need to think about mechanics to add more depth.
Another problem is probably that you have to read the rules to understand how to play, I have to rethink the UI to fit more information without making it hard to read.

Description

This is a two players game, each match should last around 5-10 min.
Each player controls a village and tries to survive during four seasons while fighting the opposing village. There can only be one winner.

Seasons

During the game Seasons are passing all the time, all seasons last the same time. There are four seasons, each with a different effect:
•	Fall: Farmer can plant food and gatherers have their normal harvesting rate.

•	Winter: Farmers don’t do anything, gatherers have half their harvesting rate.

•	Spring:  Farmers don’t do anything, gatherers have a better harvesting rate.

•	Summer: Farmers can harvest planted food gatherers have their normal harvesting rate.
Villagers consume food all the time and die when there isn’t enough of it. At the end of each season new villagers appear according to the amount of food the village has in store. 
Villagers can be assigned to specific jobs.

Jobs

Villagers can have different jobs, going from a job to another is usually free and instantaneous:

•	Farmers: They plant food during autumn and harvest it during summer, they are used to get food in the long term.
  
•	Gatherers: They can harvest food directly all the time but are less efficient than farmers, they get less food during winter.
  
•	Recruits: Recruits are training to become soldiers, several recruits can train at the same time. They can also defend the village but they are way less efficient than soldiers.
  
•	Soldiers: After its training, a recruit becomes a soldier. Soldiers can defend the base or become Looters.
  
•	Looters: Soldiers can become Looters instantaneously and at no cost, then they can be sent to loot the enemy's village.

Resources

At the moment there are 2 types of resources:
•	Planted Food: Farmers can create Planted Food during Fall and transform it into Food during Summer.
•	Food: It is produced by Farmers and Gatherers. It is consumed all the time by Villagers and is the main resource of the game. It can be stolen by enemy Looters if they win a raid. Looters also consume Food when they are sent on a raid but not while they are abroad. 

Combat

When Looters are sent to the enemy village it is notified and has 10 seconds to prepare its defenses. After that the two armies start fighting, when one army has lost half of its forces it flee and the other is victorious.
If the Looters are the one victorious they will loot Food, if there isn’t enough food for them they will also slaughter villagers.



