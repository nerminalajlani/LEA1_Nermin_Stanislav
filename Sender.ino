void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  Serial3.begin(9600);
  Serial.setTimeout(50);
  Serial1.setTimeout(50);

  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);
  digitalWrite(8, LOW);
  digitalWrite(9, LOW);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available())
  {
    messege = Serial.readString();
    Serial3.print();
  }
}
