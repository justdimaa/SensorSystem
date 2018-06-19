#pragma once
#include <DHT.h>
#include "DigitalModule.hpp"

#define DHTTYPE DHT11

class TemperatureModule : public DigitalModule
{
public:
	TemperatureModule(Station * station);
	~TemperatureModule() = default;
	void begin() override;
	void loop() override;
private:
	DHT * dht;
};

