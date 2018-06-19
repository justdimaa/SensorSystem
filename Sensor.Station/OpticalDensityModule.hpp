#pragma once
#include <BH1750.h>
#include "DigitalModule.hpp"

class OpticalDensityModule : public DigitalModule
{
public:
	OpticalDensityModule(Station * station);
	~OpticalDensityModule() = default;
	void begin() override;
	void loop() override;
private:
	BH1750 * lightMeter;
};
