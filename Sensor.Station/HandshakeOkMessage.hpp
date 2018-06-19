#pragma once
#include "ServerMessage.hpp"

class HandshakeOkMessage : public ServerMessage
{
public:
	HandshakeOkMessage(Station * station);
	~HandshakeOkMessage() = default;
	void Decode(const JsonObject& json) override;
	void Execute() override;
private:
	String name;
	bool accepted;
};

