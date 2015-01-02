Created by: Ori Ashur
Date: 02/01/2015

	*** Summary	***
This file is located in the "data" directory, 
This directory is the file-system structure which the ground station (written by CS) requires.

	*** Setting up the data directory ***
When setting up "data" in a new location one should consider the following things:
The ground station GUI program needs to be updated with the current location
of the "data" directory:
	* ImageProccessingControl - VisualStudioSolution -
				   Holds parameters pointing to this folder path.
				   The parameters are found under App.config file and the file can
				   be found int the Solution Explorer window in visual studio.
				   
	*** Folder Hierarchy ***
The scripts and programs running on the ground-station expect the data folder
to have the following hierarchy, which needs to be set up in advance (the folders):

	data
	|
	|- crops	(folder)
	|
	|- images	(folder)
	|
	|- log		(folder)
	|
	|- manual_targets	(folder)
	|
	|- mser-inputs	(folder)
	|
	|- segmentation-ouputs	(folder)
	|
	|- true_targets	(folder)
	|
	|- xml	(folder)
	|
	|- db.php
	|
	|- README.txt
	