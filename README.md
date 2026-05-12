# LEA Projekt – Verschlüsseltes LoRa-Chat-System

## Projektbeschreibung
Dieses Projekt ist ein sicheres Offline-Kommunikationssystem auf Basis der LoRa-Technologie.  
Es ermöglicht den Austausch von Nachrichten ohne Internetverbindung durch die Nutzung eines Arduino Mega mit LoRa-Modul als Funkmodem.

Eine Desktop-Anwendung (WPF in C#) dient als Benutzeroberfläche zum Senden und Empfangen von Nachrichten.

Das System unterstützt Benutzerkonten, eine einfache Verschlüsselung der Nachrichten sowie Avatare und ermöglicht damit eine geschützte lokale Kommunikation zwischen Geräten ohne Internetzugang.

---

## Verwendete Technologien
- C# (.NET / WPF)
- Arduino Mega
- LoRa-Kommunikationsmodule
- Arduino IDE
- C / Embedded Programmierung

---

## Verschlüsselung
Das System verwendet ein einfaches symmetrisches Substitutionsverfahren (Atbash-ähnlicher Algorithmus).  
Dabei wird jeder Buchstabe des Alphabets durch das entsprechende Zeichen von der gegenüberliegenden Seite des Alphabets ersetzt.

---

## Funktionen
- Offline-Kommunikation über LoRa-Funk
- Verschlüsselte Nachrichtenübertragung
- Benutzerverwaltung (Accounts)
- Desktop-Anwendung mit WPF
- Nachrichtenaustausch zwischen Arduino-Geräten und PC

---

## Systemarchitektur
- Arduino + LoRa → Funkübertragungsschicht
- C# (WPF) → Benutzeroberfläche und Anwendungslogik
- Verschlüsselungsmodul → Verarbeitung der Nachrichten

---

## Team
- Nermin Alajlani – Hardware / Arduino / Kommunikation
- Stanislav Kharchenko – Software / WPF / C# / Systemintegration

---

## Ziel des Projekts
Ziel des Projekts ist die Entwicklung eines lokalen Kommunikationssystems, das auch ohne Internetverbindung eine sichere und zuverlässige Nachrichtenübertragung ermöglicht.
