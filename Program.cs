Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("Energie-calculator");
//Vraag waardes en sla op
Console.WriteLine("Welk appartaat gebruik je?");
string apparaat1 = Console.ReadLine();
Console.WriteLine($"Wat zijn de Watts vermogen van je {apparaat1}?");
string watts1 = Console.ReadLine();
Console.WriteLine($"Hoeveel uren per dag word je {apparaat1} gebruikt?");
string uren1 = Console.ReadLine();
Console.WriteLine($"Hoe laat word je {apparaat1} gebruikt? (voer het digitaal in bijv. 22:30)");
string tijdstip1 = Console.ReadLine();

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Welk appartaat gebruik je?");
string apparaat2 = Console.ReadLine();
Console.WriteLine($"Wat zijn de Watts vermogen van je {apparaat2}?");
string watts2 = Console.ReadLine();
Console.WriteLine($"Hoeveel uren per dag word je {apparaat2} gebruikt?");
string uren2 = Console.ReadLine();
Console.WriteLine($"Hoe laat word je {apparaat2} gebruikt? (voer het digitaal in bijv. 22:30)");
string tijdstip2 = Console.ReadLine();

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Welk appartaat gebruik je?");
string apparaat3 = Console.ReadLine();
Console.WriteLine($"Wat zijn de Watts vermogen van je {apparaat3}?");
string watts3 = Console.ReadLine();
Console.WriteLine($"Hoeveel uren per dag word je {apparaat3} gebruikt?");
string uren3 = Console.ReadLine();
Console.WriteLine($"Hoe laat word je {apparaat3} gebruikt? (voer het digitaal in bijv. 22:30)");
string tijdstip3 = Console.ReadLine();

//Converteer de input waardes naar floats
float wattsF1 = float.Parse(watts1); 
float wattsF2 = float.Parse(watts2);
float wattsF3 = float.Parse(watts3);

float urenF1 = float.Parse(uren1); 
float urenF2 = float.Parse(uren2);
float urenF3 = float.Parse(uren3);

//Converteer tijdstip naar integers
string[] splitChar1 = tijdstip1.Split(":");
int intTijdstip1 = Int32.Parse(splitChar1[0]);
string[] splitChar2 = tijdstip2.Split(":");
int intTijdstip2 = Int32.Parse(splitChar2[0]);
string[] splitChar3 = tijdstip3.Split(":");
int intTijdstip3 = Int32.Parse(splitChar3[0]);

//piekTariefbedrag
float piekTarief = 0.30f;
float dalTarief = 0.25f;

//Zorgt ervoor dat uren maximaal 24 en minimaal 0 kan zijn
wattsF1 = Math.Max(wattsF1, 0);
wattsF2 = Math.Max(wattsF2, 0);
wattsF3 = Math.Max(wattsF3, 0);

urenF1 = Math.Clamp(urenF1, 0, 24);
urenF2 = Math.Clamp(urenF2, 0, 24);
urenF3 = Math.Clamp(urenF3, 0, 24);

piekTarief = Math.Max(piekTarief, 0.01f);

//Bereken kWh voor elk apparaat
float kW1 = wattsF1 / 1000;
float kWh1 = kW1 * urenF1;
float kW2 = wattsF2 / 1000;
float kWh2 = kW2 * urenF2;
float kW3 = wattsF3 / 1000;
float kWh3 = kW3 * urenF3;

//Verander naar daltarief van 22 tot 23 uur
float tarief1 = (intTijdstip1 == 22 || intTijdstip1 == 23) ? dalTarief : piekTarief;
float tarief2 = (intTijdstip2 == 22 || intTijdstip2 == 23) ? piekTarief : dalTarief;
float tarief3 = (intTijdstip3 == 22 || intTijdstip3 == 23) ? piekTarief : dalTarief;
//Bereken tarief per apparaat
float dagpiekTarief1 = tarief1 * kWh1;
float dagpiekTarief2 = tarief2 * kWh2;
float dagpiekTarief3 = tarief3 * kWh3;

//Bereken dagelijks verbruik voor alle apparaten
float totaleVerbruik =  kWh1 + kWh2 + kWh3;
//Bereken stroomtarieven over het jaar
float dagpiekTarief = piekTarief * totaleVerbruik;
float maandpiekTarief = dagpiekTarief * 30;
float jaarpiekTarief = maandpiekTarief * 365;

//Bereken CO2 uitstoot per jaar
float jaarlijkseCo2 = 427 * totaleVerbruik; 
float jaarlijkseCo2K = jaarlijkseCo2 / 1000;

//Rond alles af op 2 decimalen
string strDagpiekTarief = dagpiekTarief.ToString("F2");
string strmaandpiekTarief = maandpiekTarief.ToString("F2");
string strjaarpiekTarief = jaarpiekTarief.ToString("F2");
string strdagpiekTarief1 = dagpiekTarief1.ToString("F2");
string strdagpiekTarief2 = dagpiekTarief2.ToString("F2");
string strdagpiekTarief3 = dagpiekTarief3.ToString("F2");
string strjaarlijkseCo2K = jaarlijkseCo2K.ToString("F2");

//ASCII console kleuren
string Groen(string tekst) => $"\u001b[32m{tekst}\u001b[0m";
string DonkerPaars(string tekst) => $"\u001b[35m\u001b[2m{tekst}\u001b[0m";
string DonkerRood(string tekst) => $"\u001b[31m\u001b[2m{tekst}\u001b[0m";

//Beschrijf stroom gebruik en berekeningen naar console
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine($"{apparaat1} verbruikt {kWh1}kW op een dag | {Groen($"€{strdagpiekTarief1} per dag")}");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"{apparaat2} verbruikt {kWh2}kW op een dag | {Groen($"€{strdagpiekTarief2} per dag")}");
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine($"{apparaat3} verbruikt {kWh3}kW op een dag | {Groen($"€{strdagpiekTarief3} per dag")}");

Console.ForegroundColor = ConsoleColor.Yellow;
if (totaleVerbruik >= 4.8){
    Console.WriteLine($"Totale verbuik op een dag: {DonkerRood($"{totaleVerbruik}kW")} | {DonkerRood($"€{dagpiekTarief} per dag")}");
    Console.WriteLine($"Jou maandpiekTarief: €{DonkerRood(strmaandpiekTarief)} per maand en {DonkerRood($"€{strjaarpiekTarief}")} per jaar!");
}
else{Console.WriteLine($"Totale verbuik op een dag: {Groen($"{totaleVerbruik}kW")} | {Groen($"€{dagpiekTarief} per dag")}"); 
     Console.WriteLine($"Jou maandpiekTarief: {Groen($"{totaleVerbruik}kW")} per maand en {Groen($"€{strjaarpiekTarief}")} per jaar!");
}
Console.ResetColor();
Console.WriteLine($"Jou Co2 uitstoot is {DonkerPaars($"{strjaarlijkseCo2K}KG")}");