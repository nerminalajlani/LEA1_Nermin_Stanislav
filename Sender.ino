String message;

void setup() {
  Serial.begin(9600);
  Serial3.begin(9600);

  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);
  digitalWrite(8, LOW);
  digitalWrite(9, LOW);

  Serial.println("Setup abgeschlossen. Bitte Nachricht eingeben...");
}

void loop() {
  // Wenn Daten vom PC (Serial) kommen → an Serial3 weiterleiten
  if (Serial.available()) {
    message = Serial.readStringUntil('\n'); // bis Zeilenende lesen
    Serial3.println(message);               // mit Zeilenumbruch senden
  }

  // Wenn Daten von Serial3 kommen → an PC (Serial) weiterleiten
  if (Serial3.available()) {
    message = Serial3.readStringUntil('\n'); 
    Serial.println(message);
  }
}
