#pragma once
const int PIN_LIGHT = A0;
const int PIN_TEMPERATURE = GPIO_NUM_19;

#ifdef ESP32
const int RESOLUTION = 12;
const int PIN_SCL = SCL;
const int PIN_SDA = SDA;
#else
const int RESOLUTION = 10;
#endif
