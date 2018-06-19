#include "ClientMessage.hpp"

ClientMessage::ClientMessage(Station * station) : Message(station)
{
	
}

void ClientMessage::Encode()
{
	this->root["msg_id"] = this->id;
}
