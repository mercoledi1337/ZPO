#include <ByteConvert.hpp>
#include <Adafruit_NeoPixel.h> //Dołączenie biblioteki
#include<SoftwareSerial.h>
#include <OneWire.h> //termometr
#include <DallasTemperature.h>

OneWire oneWire(A5); //Podłączenie do A5
DallasTemperature sensors(&oneWire); //Przekazania informacji do biblioteki
//Konfiguracja linijki
Adafruit_NeoPixel linijka = Adafruit_NeoPixel(8, A0, NEO_GRB + NEO_KHZ800);
void setup() {
Serial.begin(9600);
  linijka.begin(); //Inicjalizacja
  linijka.show(); 
  sensors.begin(); //Inicjalizacja czujnikow

}

int rgb[3];
int r = 0;
int b = 1;
int g = 0;
unsigned long aktualnyCzas = 0;
unsigned long zapamietanyCzasLED1 = 0;
unsigned long zapamietanyCzasLED2 = 0;

void loop() {
 aktualnyCzas = millis();
    if (Serial.available() > 0) {
for (int i=0; i<3; i++) {
      String pos = Serial.readStringUntil('&');
      delay(10);
      int int_pos=pos.toInt();
      rgb[i] = int_pos;
      Serial.println(rgb[i]);
      delay(10);
  }
  String stop = Serial.readString();
}
  for (int i = 0; i < 8; i++) {
      linijka.setPixelColor(i, linijka.Color(rgb[0], rgb[1], rgb[2])); //Dioda nr i świeci na wybrany kolor
    }
    
    linijka.show(); 
      if (aktualnyCzas - zapamietanyCzasLED1 >= 1000UL) {
    //Zapamietaj aktualny czas
    zapamietanyCzasLED1 = aktualnyCzas;
    //ustawiamy nowy stan na diodzie
    sensors.requestTemperatures(); //Pobranie temperatury czujnika
      Serial.println(sensors.getTempCByIndex(0));  //Wyswietlenie informacji
  }
    
}