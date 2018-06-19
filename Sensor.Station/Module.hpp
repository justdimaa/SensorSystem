#pragma once
#include "Arduino.h"
#include "Station.hpp"

class Station;

class Module
{
public:
	enum class Type { None, OpticalDensity, Temperature, PH, Oxygen };
	Type type;
	Station * station;
	explicit Module(Station * station);
	virtual ~Module() = default;
	virtual void begin();
	virtual void loop();
protected:
	bool isActive;
};

