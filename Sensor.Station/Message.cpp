#include "Message.hpp"

Message::Message(Station* station)
{
	this->station = station;
	this->id = 0;
}

Message::~Message()
{
	this->id = 0;
}
