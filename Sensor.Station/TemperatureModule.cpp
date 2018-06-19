#include "TemperatureModule.hpp"
#include "Config.hpp"
#include "UpdateModuleInfoMessage.hpp"
#include "AddModuleMessage.hpp"

TemperatureModule::TemperatureModule(Station * station) : DigitalModule(station)
{
	Module::type = Module::Type::Temperature;
	this->dht = nullptr;
}

void TemperatureModule::begin()
{
	DigitalModule::begin();

	this->dht = new DHT(PIN_TEMPERATURE, DHTTYPE);
	this->dht->begin();  
	
	if (!isnan(this->dht->readTemperature()) || !isnan(this->dht->readHumidity())) 
	{
		this->isActive = true;
		this->station->sendMessage(new AddModuleMessage(this));
	}
	else
	{
		// Failed
	}
}

void TemperatureModule::loop()
{
	DigitalModule::loop();

	if (this->isActive)
	{
		auto temperature = this->dht->readTemperature();
		auto humidity = this->dht->readHumidity();
		auto message = new UpdateModuleInfoMessage(this);

		message->addValue("temp", temperature);
		message->addValue("hd", humidity);

		this->station->sendMessage(message);
	}
	else
	{
		this->begin();
	}
}
