# POO - Simulateur de réseaux électrique

This platform simulate the behaviour of an electrical grid at a country scale. The aim is to simulate the dynamic behaviour of a set of electrical power production centers and power consummers.
Thanks to this platform, researchers can develop automatic regulation schemes of the network in order to simulate disasters, optimise buy/sell markets with neighboring countries, study investement oportunity and reduce the environmental impact of the grid.

The platform offer a series of classes allowing researchers to build their own network, along with a broad implementation of most of these classes for presentation purpose. It is possible to create your own plants/consumers/fuels/... classes fitting your need by inheriting from the corresponding abstract superclass. The following documentation describe the most important part of the platform.

## Graphical User Interface

At runtime, the exemple program available in the files run this window, displaying various information about the grid and its components. It is a ControlCenter object, inheriting from the Windows Form object, providing all the graphical and interactivity part of the platform. It's not involved in any data calculation of the grid or its components. Its only role is to repeatedly update the grid with a timer attached to it, and render in a user-friendly maner the data the grid has calculated. 

At the top left is a map of the nodes of the grid with coordinate <0,0> at the top left of the map. Below this map are two buttons, play and pause, letting the user stop the simulation and resume it at will, along with a trackbar allowing to set the speed of the simulation from slow, normal and fast. At the bottom left are all the Plant nodes and Consumer nodes of the grid along with various information about their current state. The fuel attached to the grid are also displayed along with their current price. Note the small checkbox attached to each of these nodes, they allow for the display/hiding of that node current power production/consumption on the "Node Chart" at the bottom right. Finally, at the top right is located the chart of the total power production, power consumption, CO2 emission,... of the whole grid. Note that the content of the ControlCenter automatically display the content of the grid without the need to adjust the ControlCenter itself.

<p align="center">
  <img src="img/Simulation.gif" alt="The graphical UI" width="700"><br/>
  <em>Click the image for full resolution</em>
</p>

## Class Diagram

Here is presented the Class Diagram of the classes of the platform. Let start with a few remarks:
- First item
- Second item
- Third item
- Fourth item

<p align="center">
  <img src="img/Class Diagram.png" alt="The Class Diagram" width="700"><br/>
  <em>Click the image for full resolution</em>
</p>

## Sequence Diagram

<p align="center">
  <img src="img/Sequence Diagram.png" alt="The Sequence Diagram" width="400"><br/>
  <em>Click the image for full resolution</em>
</p>

## Contributor

Andrea Ventura, student at ECAM Brussels Engineering School.

##
