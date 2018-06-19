#pragma once
#include <vector>
#include "Module.hpp"

class ClientMessage;
class Module;

class Station
{
public:
	String name;
	bool isRegistered;
	explicit Station(const String& name);
	~Station() = default;
	void begin();
	void loop();
	void serialEvent();
	void initializeModules();
	void sendMessage(ClientMessage* message);
private:
	std::vector<Module *> modules;
	String buffer;
};

