#pragma once
#include "Module.hpp"
#include "Message.hpp"

class DigitalModule : public Module
{
public:
	explicit DigitalModule(Station* station);
	virtual ~DigitalModule() = default;
	void begin() override;
	void loop() override;
};

