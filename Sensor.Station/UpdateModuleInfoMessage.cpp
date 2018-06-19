#include "UpdateModuleInfoMessage.hpp"

UpdateModuleInfoMessage::UpdateModuleInfoMessage(Module * module) : ClientMessage(module->station)
{
	this->id = 0x6E;
	this->module = module;
}

void UpdateModuleInfoMessage::Encode()
{
	ClientMessage::Encode();

	this->root["module_id"] = static_cast<int>(this->module->type);
}

void UpdateModuleInfoMessage::addValue(const String& name, const double& value)
{
	this->root[name] = RawJson(String(value));
}

UpdateModuleInfoMessage::~UpdateModuleInfoMessage()
{
	this->module = nullptr;
}
