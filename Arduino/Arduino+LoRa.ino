#include <LoRa_E32.h>

// Pins zur Steuerung der Modi
const byte M0_PIN = 9;
const byte M1_PIN = 8;
const byte AUX_PIN = 7;

// Verwende Serial3 für LoRa (Pins 14 = TX3, 15 = RX3 auf dem Mega)
LoRa_E32 e32(&Serial3, AUX_PIN, M0_PIN, M1_PIN);

void setup() {
  Serial.begin(9600); // Serielle Verbindung mit dem PC
  delay(500);

  // Serial.println("Initialisierung...");

  // Pins konfigurieren
  pinMode(M0_PIN, OUTPUT);
  pinMode(M1_PIN, OUTPUT);
  pinMode(AUX_PIN, INPUT);

  // Setze LoRa in den normalen Modus: MODE_0 (M0 = LOW, M1 = LOW)
  digitalWrite(M0_PIN, LOW);
  digitalWrite(M1_PIN, LOW);
  delay(100);

  // Initialisiere das LoRa-Modul
  if (!e32.begin()) {
    Serial.println("Fehler bei der Initialisierung des LoRa-Moduls!");
    while (1); // Stoppe bei Fehler
  } else {
    // Serial.println("LoRa E32 Modul erfolgreich initialisiert.");
  }

  // Erzwinge den normalen Betriebsmodus
  Status status = e32.setMode(MODE_0_NORMAL);
  // Serial.print("Modus gesetzt: ");
  // Serial.println((int)status);
  delay(100);

  // Lese die Konfiguration des Moduls
  ResponseStructContainer c = e32.getConfiguration();
  if (c.status.code == SUCCESS) {
    // Serial.println("Konfiguration erfolgreich gelesen.");
    Configuration config = *(Configuration*)c.data;

    // Serial.print("Adresse: ");
    // Serial.print(config.ADDH, HEX);
    // Serial.print(" ");
    // Serial.println(config.ADDL, HEX);

    // Serial.print("Kanal: ");
    // Serial.println(config.CHAN, DEC);

    c.close(); // Speicher freigeben
  } else {
    Serial.print("Fehler beim Lesen der Konfiguration. Status: ");
    Serial.println((int)c.status.code);
  }
}

String message = "";  // Globale Variable für Nachrichten

void loop() {
  // Wenn etwas vom PC kommt, sende es über LoRa
  if (Serial.available()) {
    message = Serial.readStringUntil('\n');  // Lese bis Zeilenumbruch
    Serial3.print(message);      // Sende an LoRa-Modul
    Serial3.print('\n');         // Sende Zeilenumbruch für bessere Lesbarkeit
  }

  // Wenn etwas über LoRa empfangen wird, zeige es auf dem PC
  if (Serial3.available()) {
    message = Serial3.readStringUntil('\n');  // Empfange bis Zeilenumbruch
    // Serial.print("Empfangen: ");              // Ausgabe auf seriellen Monitor
    Serial.println(message);
  }
}
