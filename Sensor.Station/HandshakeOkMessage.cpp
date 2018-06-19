#include "HandshakeOkMessage.hpp"

HandshakeOkMessage::HandshakeOkMessage(Station * station) : ServerMessage(station)
{

}

void HandshakeOkMessage::Decode(const JsonObject& json)
{
	ServerMessage::Decode(json);

	this->name = json["name"].as<char*>();
	this->accepted = json["accepted"];
}

void HandshakeOkMessage::Execute()
{
	ServerMessage::Execute();

	if (this->name == station->name)
	{
		this->station->isRegistered = true;
		this->station->initializeModules();
	}
}

