#pragma once
#include "ClientMessage.hpp"

class HandshakeMessage : public ClientMessage
{
public:
	HandshakeMessage(Station * station);
	~HandshakeMessage();
	void Encode() override;
};
