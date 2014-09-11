d-spsim:
========

Original Code (dropped) : www.mediafire.com/?gslt2ktoy3qhcbb

Disclaimer:
This little piece of "entertainment Software" is a rework of the original SpankinfSim, produced on 4chan/d spanking thread and
this other website everybody likes. Due to unknown reasons the original author left and published the Sim's code. After
reviewing the code, I decieded to implement this project in a language I´m more familiar with to simplify and eventually
this project.
This is just a port in another language (so far), but I´ll maybe add some features in the future.
Idea and concept of this project belong to fellow anon, who left us some time ago.

Currently I'm the only one working on this project, but help is always appreciated

Achievements:
=============
11th September, 2014:
  Nothing special so far. Rooms, the protagonist and the implements are importable...

Changes so far:
===============

  ~ Multiple links in one room and multiple sfx for one item are now wrappen in one tag and divided by commatar
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

  ~ Some tags in the message section were renamed due to C#s file wide scanning
    - messages/implement is now called msgimplement
    - spanking/implement is now called announceimplement
    
