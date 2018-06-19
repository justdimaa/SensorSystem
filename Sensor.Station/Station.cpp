#include "Arduino.h"
#include "Config.hpp"
#include "Station.hpp"
#include "HandshakeMessage.hpp"
#include "OpticalDensityModule.hpp"
#include "TemperatureModule.hpp"
#include "ServerMessage.hpp"
#include "HandshakeOkMessage.hpp"

Station::Station(const String& name)
{
	this->name = name;
	this->isRegistered = false;
}

void Station::begin()
{
	delay(3000);
	this->sendMessage(new HandshakeMessage(this));
}

void Station::loop()
{
	this->serialEvent();

	if (this->isRegistered)
	{
		for (Module * module : modules)
			module->loop();

		delay(200);
	}
}

void Station::serialEvent()
{
	while (Serial.available())
	{
		const auto buffer = static_cast<char>(Serial.read());
		this->buffer += buffer;

		if (buffer == '\n')
		{
			StaticJsonBuffer<100> jsonBuffer;
			JsonObject& bufferJson = jsonBuffer.parseObject(this->buffer);

			if (bufferJson.success())
			{
				ServerMessage * message = nullptr;
				int messageID = bufferJson["msg_id"];

				switch (messageID)
				{
				case 0xC8:
					message = new HandshakeOkMessage(this);
					break;
				}

				if (message)
				{
					message->Decode(bufferJson);
					message->Execute();
				}
			}
			
			this->buffer = "";
		}
	}
}

void Station::initializeModules()
{
	auto opticalDensityModule = new OpticalDensityModule(this);
	auto temperatureModule = new TemperatureModule(this);

	opticalDensityModule->begin();
	temperatureModule->begin();

	this->modules.push_back(opticalDensityModule);
	this->modules.push_back(temperatureModule);
}

void Station::sendMessage(ClientMessage * message)
{
	message->Encode();
	message->root.printTo(Serial);
	Serial.print("\r\n");
	delete message;
}
