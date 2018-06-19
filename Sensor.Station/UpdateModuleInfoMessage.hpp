#pragma once
#include "ClientMessage.hpp"

class UpdateModuleInfoMessage : public ClientMessage
{
public:
	UpdateModuleInfoMessage(Module * module);
	~UpdateModuleInfoMessage();
	void Encode() override;
	void addValue(const String& name, const double& value);
private:
	Module * module;
};

