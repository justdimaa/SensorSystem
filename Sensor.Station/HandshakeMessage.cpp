#include "HandshakeMessage.hpp"

HandshakeMessage::HandshakeMessage(Station * station) : ClientMessage(station)
{
	this->id = 0x64;
}

void HandshakeMessage::Encode()
{
	ClientMessage::Encode();

	this->root["name"] = this->station->name;
}

HandshakeMessage::~HandshakeMessage()
= default;
