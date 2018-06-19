#include "Module.hpp"
#include "AddModuleMessage.hpp"
#include "DigitalModule.hpp"

AddModuleMessage::AddModuleMessage(Module * module) : ClientMessage(module->station)
{
	this->id = 0x69;
	this->module = module;
}

void AddModuleMessage::Encode()
{
	ClientMessage::Encode();

	this->root["module_id"] = static_cast<int>(this->module->type);

	//if (!dynamic_cast<DigitalModule*>(this->module))
	//	this->root["res"] = this->module->resolution;
}


AddModuleMessage::~AddModuleMessage()
{
	this->module = nullptr;
}
