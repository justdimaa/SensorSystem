#include <Wire.h>
#include "DigitalModule.hpp"
#include "Config.hpp"

DigitalModule::DigitalModule(Station* station) : Module(station)
{

}

void DigitalModule::begin()
{
	Module::begin();

	if (!Wire.available())
	{
		Wire.begin(PIN_SDA, PIN_SCL);
	}
}

void DigitalModule::loop()
{
	Module::loop();
}
