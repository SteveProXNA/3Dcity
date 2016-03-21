#include "tophalf_tile.h"
#include <sms.h>

unsigned char engine_tile_tophalf_data_tiles[] =
{
0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0xC0, 0xC0, 0xFF, 0x3F, 0xC0, 0xC0, 0xFF, 0x3F, 0xC0, 0xC0, 0xFF, 0x3F, 0xC0, 0xC0, 0xFF, 0x3F, 0xC0, 0xC0, 0xFF, 0x3F,
0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF,
0xC0, 0xC0, 0xF0, 0x30, 0xC0, 0xC0, 0xF0, 0x30, 0xC0, 0xC0, 0xF0, 0x30, 0xC0, 0xC0, 0xF0, 0x30, 0xC0, 0xC0, 0xF0, 0x30, 0xC0, 0xC0, 0xF0, 0x30, 0xC0, 0xC0, 0xF0, 0x30, 0xC0, 0xC0, 0xF0, 0x30,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xC0, 0xC0, 0xFF, 0x3F, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0xC0, 0xC0, 0xFF, 0x3F, 0xC0, 0xC0, 0xFF, 0x3F, 0xFF, 0xFF, 0xFF, 0x00,
0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00,
0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0xC0, 0xC0, 0xFF, 0x3F, 0xC0, 0xC0, 0xFF, 0x3F, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0x00, 0x00, 0xFF,
0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0x00, 0x00, 0xFF,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x8F, 0x00, 0x00, 0x8F, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xE1, 0x00, 0x00, 0xE1, 0xC1, 0x00, 0x00, 0xC1,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFC, 0x00, 0x00, 0xFC, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x3F, 0x00, 0x00, 0x3F, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x80, 0x00, 0x00, 0x80,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x7F, 0x00, 0x00, 0x7F,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x9F, 0x00, 0x00, 0x9F, 0x9F, 0x00, 0x00, 0x9F, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x00, 0x1F, 0x0F, 0x00, 0x00, 0x0F,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xC3, 0x00, 0x00, 0xC3, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE,
0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07,
0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xCF, 0x00, 0x00, 0xCF, 0x87, 0x00, 0x00, 0x87, 0x87, 0x00, 0x00, 0x87,
0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8,
0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x7E, 0x00, 0x00, 0x7E, 0x7E, 0x00, 0x00, 0x7E,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x80, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x7F, 0x00, 0x00, 0x7F, 0x7F, 0x00, 0x00, 0x7F, 0x7F, 0x00, 0x00, 0x7F, 0x7F, 0x00, 0x00, 0x7F, 0x3F, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0x3F, 0x00, 0x00, 0x3F, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x00, 0x1F,
0xE7, 0x00, 0x00, 0xE7, 0xE3, 0x00, 0x00, 0xE3, 0xE3, 0x00, 0x00, 0xE3, 0xE3, 0x00, 0x00, 0xE3, 0xE3, 0x00, 0x00, 0xE3, 0xE3, 0x00, 0x00, 0xE3, 0xE3, 0x00, 0x00, 0xE3, 0xE3, 0x00, 0x00, 0xE3,
0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x00, 0x1F, 0x0F, 0x00, 0x00, 0x0F,
0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xFE, 0x00, 0x00, 0xFE, 0xF0, 0x00, 0x00, 0xF0, 0xC0, 0x00, 0x00, 0xC0, 0xC0, 0x00, 0x00, 0xC0,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x87, 0x00, 0x00, 0x87, 0x83, 0x00, 0x00, 0x83, 0x83, 0x00, 0x00, 0x83, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xF8, 0x00, 0x00, 0xF8,
0x87, 0x00, 0x00, 0x87, 0x81, 0x00, 0x00, 0x81, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x7F, 0x00, 0x00, 0x7F, 0x3F, 0x00, 0x00, 0x3F, 0x3F, 0x00, 0x00, 0x3F, 0x3F, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFE, 0x00, 0x00, 0xFE, 0x7E, 0x00, 0x00, 0x7E, 0x7C, 0x00, 0x00, 0x7C,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xF8, 0x00, 0x00, 0xF8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xF9, 0x00, 0x00, 0xF9, 0x80, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0x78, 0x00, 0x00, 0x78, 0x38, 0x00, 0x00, 0x38,
0x7E, 0x00, 0x00, 0x7E, 0x7E, 0x00, 0x00, 0x7E, 0x7E, 0x00, 0x00, 0x7E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xE0, 0x00, 0x00, 0xE0,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF0, 0x00, 0x00, 0xF0, 0xF0, 0x00, 0x00, 0xF0, 0xE0, 0x00, 0x00, 0xE0, 0x00, 0x00, 0x00, 0x00,
0xE3, 0x00, 0x00, 0xE3, 0xE3, 0x00, 0x00, 0xE3, 0x63, 0x00, 0x00, 0x63, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xF8, 0x00, 0x00, 0xF8, 0x80, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFE, 0x00, 0x00, 0xFE, 0xFC, 0x00, 0x00, 0xFC, 0xFC, 0x00, 0x00, 0xFC, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8,
0x0F, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xC0, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x07, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0xC1, 0x00, 0x00, 0xC1, 0x80, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xFC, 0x00, 0x00, 0xFC, 0xFC, 0x00, 0x00, 0xFC, 0xF8, 0x00, 0x00, 0xF8, 0xF8, 0x00, 0x00, 0xF8, 0xF0, 0x00, 0x00, 0xF0, 0xE0, 0x00, 0x00, 0xE0, 0xC0, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x00, 0x00,
0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x03, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xF0, 0x00, 0x00, 0xF0, 0xF0, 0x00, 0x00, 0xF0, 0xF0, 0x00, 0x00, 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x78, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,	
};

unsigned int engine_tile_tophalf_data_tilemap[] = 
{
0x0050, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0051, 0x0250,
0x0052, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0252,
0x0054, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0055, 0x0254,
0x0056, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0057, 0x0256,
0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058,
0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058,
0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0059, 0x005A, 0x005B, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x005C, 0x005D, 0x0058, 0x0058,
0x0058, 0x005E, 0x005F, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0060, 0x0058, 0x0061, 0x0058, 0x0062, 0x0063, 0x0064, 0x0058, 0x0058, 0x0058, 0x0065, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0058, 0x0066, 0x0067, 0x0068, 0x0069,
0x006A, 0x0053, 0x0053, 0x006B, 0x0058, 0x0058, 0x0058, 0x006C, 0x0058, 0x0058, 0x006D, 0x0058, 0x0053, 0x006E, 0x006F, 0x0063, 0x0064, 0x0059, 0x0070, 0x0071, 0x0072, 0x0073, 0x0074, 0x0075, 0x0076, 0x0077, 0x0078, 0x0269, 0x0079, 0x0053, 0x007A, 0x0053,
0x0053, 0x0053, 0x0053, 0x0266, 0x007B, 0x007C, 0x007D, 0x007E, 0x0077, 0x007F, 0x0080, 0x0081, 0x0053, 0x0082, 0x0083, 0x0084, 0x0085, 0x0086, 0x0087, 0x0088, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053,
0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0089, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053,
0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053, 0x0053,	
};

void engine_tile_tophalf_load_tiles(int start, int count)
{
	// start	80	0x0050
	// count	xx	how many lines in tiles array (above)
	// bpp		 4 	bits per pixel
	load_tiles(engine_tile_tophalf_data_tiles, start, count, 4);			// SMS.
}

void engine_tile_tophalf_show_tiles(unsigned char x, unsigned char y, unsigned char width, unsigned char height)
{
	set_bkg_map(engine_tile_tophalf_data_tilemap, x, y, width, height);
}
