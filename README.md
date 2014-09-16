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
- 16th September, 2014: Pick up all the clothes edition
	- Added the clothes import
	- Bras for the world
	- Reworked the default.xml clothes-section
	- Pick up/Drop clothes & ClothingInventory
	- Scatter clothes around the house with the new <scatteredClothes> Tag
	
- 14th September, 2014: Tidy up your house edition
	- Basic Stuff
	- Movement between rooms
	- Default Actions (Look around etc.)
	- Implement Actions (PickUp Drop)
	- Added some Implements in the Default.xml
	
- 11th September, 2014:
  	- Nothing special so far. Rooms, the protagonist and the implements are importable...


Broken/Not-Working-As-Planned-"Features"
========================================

- It was report that pushing the enter key is causing an Standart-Windows-Bing, I donn't know what could be causing this, but 	I'm on it
- IMPORTANT: When using "older" xmls, you need to change them as desribed in the "Changes so far"-Section

Changes so far:
===============

  - Multiple links in one room and multiple sfx for one item are now wrapped in one tag and divided by commatar
    e.g. old:
      <room>
        ...
        <link>2</link>
        <link>3</link>
        <link>6</link>
        ...
      </room>
    
    is now replaced with:
    
    <room>
        ...
        <link>2,3,6</link>
        ...
      </room>
      
  - Rooms can now contain some clothes at startup using the <scatteredClothes>-Tag
  	for an example look at default.xml's room explanation
      
  - Clothes need some rework
    - <covers> is changed to type and due to the introduction of bras, the value needs to be changed (depending on its old 		type). For the new typelisting look at default.xml's clothes explanation

  - Some tags in the message section were renamed due to C#s file wide scanning
    - messages/implement is now called msgimplement
    - spanking/implement is now called announceimplement
    
