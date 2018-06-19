#include "OpticalDensityModule.hpp"
#include "AddModuleMessage.hpp"
#include "UpdateModuleInfoMessage.hpp"

OpticalDensityModule::OpticalDensityModule(Station* station) : DigitalModule(station)
{
	Module::type = Module::Type::OpticalDensity;
	this->lightMeter = nullptr;
}

void OpticalDensityModule::begin()
{
	DigitalModule::begin();

	this->lightMeter = new BH1750(0x23);

	if (this->lightMeter->begin(BH1750::CONTINUOUS_HIGH_RES_MODE))
	{
		this->isActive = true;
		this->station->sendMessage(new AddModuleMessage(this));
	}
	else
	{
		// Failed
	}
}

void OpticalDensityModule::loop()
{
	DigitalModule::loop();
	
	if (this->isActive)
	{
		auto lux = this->lightMeter->readLightLevel();
		auto message = new UpdateModuleInfoMessage(this);
		message->addValue("lux", lux);
		this->station->sendMessage(message);
	}
	else
	{
		this->begin();
	}
}
