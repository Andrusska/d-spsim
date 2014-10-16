d-spsim:
========

Original Code (dropped) : www.mediafire.com/?gslt2ktoy3qhcbb

Disclaimer:
This little piece of "entertainment Software" is a rework of the original SpankinfSim, produced on 4chan/d spanking thread and
this other website everybody likes (animeotkDOTcom). Due to unknown reasons the original author left and published the Sim's code. After reviewing the code, I decieded to implement this project in a language I´m more familiar with to simplify and eventually
this project.
This is just a port in another language (so far), but I´ll maybe add some features in the future.
Idea and concept of this project belong to fellow anon, who left us some time ago.

Currently I'm the only one working on this project, but help is always appreciated

Update History:
===============

- 16th October, 2014: "Wedgie Simulator"-edition
	- Added the modable messages and the message pooling
	- Holding/Dragging around girls is now modable, too
	- Added a detailed error-message to simplify bug reports
	- Kicked most of the unused "old features" out of the default.xml (mostly the dynamic-girls stuff, girls will be static 	(for now)) 
	- some other small fixes

- 23th September, 2014: "Dress up your girls"-edition
	- Added the girls
	- Added girl interaction (go to room X, take off this, put on that)
	- Changed the ClothingPrefs
	- Added another room
	- All unused clothes can be dumped in one room with 
	 ```<dumpclothes />```

- 16th September, 2014: "Pick up all the clothes"-edition
	- Added the clothes import
	- Bras for the world
	- Reworked the default.xml clothes-section
	- Pick up/Drop clothes & ClothingInventory
	- Scatter clothes around the rooms with the new Tag:
	 ```<scatteredClothes />```
	
- 14th September, 2014: "Tidy up your house"-edition
	- Basic Stuff
	- Movement between rooms
	- Default Actions (Look around etc.)
	- Implement Actions (PickUp Drop)
	- Added some Implements in the Default.xml
	
- 11th September, 2014:
  	- Nothing special so far. Rooms, the protagonist and the implements are importable...


Broken/Not-Working-As-Planned-"Features"
========================================
- There are some problems with the onepieces on initial dress up
- It was report that pushing the enter key is causing an Standart-Windows-Bing, I donn't know what could be causing this, but 	I'm on it


Changes so far:
===============

- IMPORTANT: When using "older" xmls, you need to change them as described in the "Changes so far"-Section

  - Multiple links in one room and multiple sfx for one item are now wrapped in one tag and divided by commatar
    e.g. the old
	```
	<room>
		...
		<link>2</link>
		<link>3</link>
		<link>6</link>
		...
	</room>
	```
    is now replaced with:
    ```
    <room>
    	...
    	<link>2,3,6</link>
    	...
    </room>
    ```
      
  - Rooms can now contain some clothes at startup using the <scatteredClothes>-Tag
  	for an example look at default.xml's room explanation

  - The clothing-preferences changed. Taggs are now individual for each clothingtype and got the id-Attribute to get the 		corresponding girl. Read the girl explanation in default.xml for the details
      
  - Clothes need some rework
    - <covers> is changed to type and due to the introduction of bras, the value needs to be changed (depending on its old 		type). For the new typelisting look at default.xml's clothes explanation

  - Some tags in the message section were renamed due to C#s file wide scanning, compare them with the new default.xml. Most of 	the original Tags are untouched, so you should easyly recognize how there names changed
    
