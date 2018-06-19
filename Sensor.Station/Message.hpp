#pragma once
#include "ArduinoJson.h"
#include "Station.hpp"

class Station;

class Message
{
public:
	int id;
	StaticJsonBuffer<100> jsonBuffer;
	JsonObject& root = jsonBuffer.createObject();
protected:
	Station * station;
	explicit Message(Station * station);
	~Message();
};
