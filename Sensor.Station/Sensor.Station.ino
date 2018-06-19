/*
 Name:		Sensor.ino
 Created:	2018-06-15 21:35:24
 Author:	Dimaa
*/

#include "Station.hpp"

Station mainStation("D1");

// the setup function runs once when you press reset or power the board
void setup()
{
	mainStation.begin();
}

// the loop function runs over and over again until power down or reset
void loop()
{
	mainStation.loop();
}
