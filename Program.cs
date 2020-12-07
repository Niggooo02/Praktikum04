using System;

namespace Praktikum_04 {
    class Program {
        static int einsatz1, einsatz2;
        static int punkteS1, punkteS2, punkteComputer;  //Speichern Punkte für die beiden Spieler und für den Computer
        static int anzahlAsseS1, anzahlAsseS2;
        static int kartenwert;  //Speichert den Wert, den die Zufallszahl ausgibt (also von 1 bis 13)
        static Random random;
        static bool erneutZiehen = true;
        static char eingabe;        //Soll die Eingabe, ob der Spieler nochmal spielen möchte, speichern
        static void Main(string[] args) {
            int credit1 = 10;
            int credit2 = 10;
            Play(ref credit1, ref credit2);

            Console.WriteLine("\nEndergebnisse: ");
            Console.WriteLine($"Spieler 1 hat aus 10 Jetons {credit1} gemacht.");
            Console.WriteLine($"Spieler 2 hat aus 10 Jetons {credit2} gemacht.");
        }
        static void Play(ref int credit1, ref int credit2) {
            random = new Random();
            do {
                //Variablen auf Standardwert zurücksetzen (bei jedem neuen Spiel)
                anzahlAsseS1 = anzahlAsseS2 = 0;
                erneutZiehen = true;
                punkteS1 = punkteS2 = punkteComputer = 0;

                //Abfrage der Einsätze
                Console.WriteLine("~ ~ Spieler 1:");
                einsatz1 = EinsatzFestlegen(credit1);
                if (einsatz1 == 0) {    //Wenn der Einsatz gleich null ist, Schleife sofort abbrechen (-> Spiel beenden)
                    break;
                }
                Console.WriteLine("\n~ ~ Spieler 2:");
                einsatz2 = EinsatzFestlegen(credit2);
                if (einsatz2 == 0) {    //Wenn der Einsatz gleich null ist, Schleife sofort abbrechen (-> Spiel beenden)
                    break;
                }

                Console.WriteLine("\n~~~~ Spieler 1:");
                KarteZiehen(ref punkteS1, ref anzahlAsseS1);
                KarteZiehen(ref punkteS1, ref anzahlAsseS1);        //Zwei Karten sind Pflicht
                while (erneutZiehen == true && punkteS1 < 21) {     //Wiederholen, wenn der Spieler erneut spielen möchte und noch weniger als 21 Punkte hat
                    Console.Write("Noch eine Karte? (J|j für ja): ");
                    eingabe = Convert.ToChar(Console.ReadLine());
                    if (eingabe == 'j' || eingabe == 'J') {
                        KarteZiehen(ref punkteS1, ref anzahlAsseS1);
                    } else {
                        erneutZiehen = false;
                    }
                }

                //Asse wurden mit Wert 1 gespeichert; deshalb: Test, ob die Gesamtpunktzahl mit Ass als 11 besser wäre
                if (anzahlAsseS1 >= 1 && (punkteS1 + 10) <= 21) {        //punkteS1 + 10, da für das Ass bereits 1 Punkt berechnet wwurde
                    punkteS1 += 10;     //Punkte um Wert 10 erhöhen
                }

                erneutZiehen = true;    //Variable zurücksetzen
                Console.WriteLine("\n~~~~ Spieler 2:");
                KarteZiehen(ref punkteS2, ref anzahlAsseS2);
                KarteZiehen(ref punkteS2, ref anzahlAsseS2);        //Zwei Karten sind Pflicht
                while (erneutZiehen == true && punkteS2 < 21) {     //Wiederholen, wenn der Spieler erneut spielen möchte und noch weniger als 21 Punkte hat
                    Console.Write("Noch eine Karte? (J|j für ja): ");
                    eingabe = Convert.ToChar(Console.ReadLine());
                    if (eingabe == 'j' || eingabe == 'J') {
                        KarteZiehen(ref punkteS2, ref anzahlAsseS2);
                    } else {
                        erneutZiehen = false;
                    }
                }

                //Asse wurden mit Wert 1 gespeichert; deshalb: Test, ob die Gesamtpunktzahl mit Ass als 11 besser wäre
                if (anzahlAsseS2 >= 1 && (punkteS2 + 10) <= 21) {        //punkteS1 + 10, da für das Ass bereits 1 Punkt berechnet wwurde
                    punkteS2 += 10;     //Punkte um Wert 10 erhöhen
                }

                Console.WriteLine("\n~~~~ Computer: ");
                if (punkteS1 <= 21 || punkteS2 <= 21) {      //Computer zieht nur, wenn mindestens einer der Spieler weniger oder gleich 21 Punkte hat
                    while (punkteComputer < 16) {
                        kartenwert = random.Next(1, 14);
                        Console.Write("Gezogen: ");     //Kein Absatz; Ausgabe wird "zusammengebaut"
                        if (kartenwert == 1) {
                            punkteComputer += 11;
                            Console.Write("1");
                        } else if (kartenwert <= 10) {
                            punkteComputer += kartenwert;
                            Console.Write(kartenwert);
                        } else if (kartenwert == 11) {
                            punkteComputer += 10;
                            Console.Write("B");
                        } else if (kartenwert == 12) {
                            punkteComputer += 10;
                            Console.Write("D");
                        } else if (kartenwert == 13) {
                            punkteComputer += 10;
                            Console.Write("K");
                        }
                        Console.WriteLine($", Punkte: {punkteComputer}");
                    }

                    Console.WriteLine("");

                    //Entscheidung, wer gewonnen hat
                    if (punkteS1 <= 21 && (punkteComputer > 21 || punkteS1 > punkteComputer)) {       //Wenn Computer mehr und Spieler weniger oder gleich 21 Punkte hat ODER beide unter 21 und Spieler hat mehr Punkte als Computer
                        credit1 += (2 * einsatz1);
                        Console.WriteLine($"Spieler 1 hat gewonnen. Jetons jetzt: {credit1}");
                    } else {
                        credit1 -= einsatz1;
                        Console.WriteLine($"Spieler 1 hat verloren. Jetons jetzt: {credit1}");
                    }

                    if (punkteS2 <= 21 && (punkteComputer > 21 || punkteS2 > punkteComputer)) {       //Wenn Computer mehr und Spieler weniger oder gleich 21 Punkte hat ODER beide unter 21 und Spieler hat mehr Punkte als Computer
                        credit2 += (2 * einsatz2);
                        Console.WriteLine($"Spieler 2 hat gewonnen. Jetons jetzt: {credit2}");
                    } else {
                        credit2 -= einsatz2;
                        Console.WriteLine($"Spieler 2 hat verloren. Jetons jetzt: {credit2}");
                    }
                } else {
                    //Wenn beide Spieler über 21 Punkte haben -> Computer gewinnt sofort ohne selbst Karten zu ziehen
                    credit1 -= einsatz1;
                    Console.WriteLine($"Spieler 1 hat verloren. Jetons jetzt: {credit1}");
                    credit2 -= einsatz2;
                    Console.WriteLine($"Spieler 2 hat verloren. Jetons jetzt: {credit2}");
                }
                Console.WriteLine("\nRunde beendet.\n");
            } while (credit1 > 0 && credit2 > 0);   //Schleife wiederholen, solange alle Spieler noch mindestens einen Jeton haben
        }

        static int EinsatzFestlegen(int credit) {
            int einsatz;
            Console.WriteLine("Wie viele Jetons wollen Sie setzen? Setzen Sie 0, um das Spiel zu beenden.");
            do {
                Console.Write($"Bitte geben Sie einen Wert zwischen 0 und {credit} ein: ");
                if (Int32.TryParse(Console.ReadLine(), out einsatz) == false) {  //Wenn die Eingabe nicht in int konvertiert werden kann
                    Console.WriteLine("Ungültige Eingabe");
                    einsatz = -1;    //Einsatz wird absichtlich auf falschen Wert gesetzt, damit die Schleife wiederholt wird
                }
                //Wenn Eingabe ein Integer ist
                else if (einsatz < 0) {
                    Console.WriteLine("Zu kleiner Wert");
                } else if (einsatz > credit) {
                    Console.WriteLine("Zu großer Wert");
                }
            } while (einsatz < 0 || einsatz > credit);
            return einsatz;
        }

        static void KarteZiehen(ref int punktzahl, ref int anzahlAsse) {    //punktzahl: Gesamtpunktzahl
            kartenwert = random.Next(1, 14);  //Speichert den Wert der Zufallszahl

            if (kartenwert == 1) {
                anzahlAsse++;
            }

            //Ausgabe der Ergebnisse
            Console.Write("Gezogen: ");     //Kein Absatz (Console.Write()): Ausgabe wird "zusammengebaut"
            if (kartenwert <= 10) {
                punktzahl += kartenwert;
                Console.Write(kartenwert);
            } else if (kartenwert == 11) {
                punktzahl += 10;
                Console.Write("B");
            } else if (kartenwert == 12) {
                punktzahl += 10;
                Console.Write("D");
            } else if (kartenwert == 13) {
                punktzahl += 10;
                Console.Write("K");
            }
            Console.WriteLine($", Ihr Ergebnis ist: {punktzahl}");
        }
    }
}