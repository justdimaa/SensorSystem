#pragma once
#include "ClientMessage.hpp"

class AddModuleMessage : public ClientMessage
{
	Module * module;
public:
	AddModuleMessage(Module * module);
	~AddModuleMessage();
	void Encode() override;
};
