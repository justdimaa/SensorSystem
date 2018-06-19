#pragma once
#include "Message.hpp"

class ServerMessage : public Message
{
public:
	virtual ~ServerMessage() = default;
	virtual void Decode(const JsonObject& json);
	virtual void Execute();
protected:
	explicit ServerMessage(Station * station);
};

