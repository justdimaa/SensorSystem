#pragma once
#include "Message.hpp"

class ClientMessage : public Message
{
public:
	virtual void Encode();
	virtual ~ClientMessage() = default;
protected:
	explicit ClientMessage(Station * station);
};
