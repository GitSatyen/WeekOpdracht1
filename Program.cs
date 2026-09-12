Console.WriteLine("Energie-calculator");
//Vraag waardes en sla op
//Vraag apparaat 1
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("Welk apparaat gebruik je?");
string apparaat1 = Console.ReadLine();
float watts1;

//Verbeterde exception looping na LLM (ChatGPT) review
//Exception handeling met Tryparse voorkomt crashes bij verkeerde invoer
//Loopt steeds opnieuw totdat de gebruiker geldig data invoert
while (true)
{
    Console.WriteLine($"Wat zijn het wattage van je {apparaat1}?");
    
    if (float.TryParse(Console.ReadLine(), out watts1) && watts1 >= 0)
    {
        break;
    }

    Console.WriteLine("Ongeldige invoer. Voer een positief getal in, bijvoorbeeld 100.");
}
float uren1;
while (true)
{
    Console.WriteLine($"Hoeveel uren per dag wordt je {apparaat1} gebruikt?");

    if (float.TryParse(Console.ReadLine(), out uren1) && uren1 >= 0 && uren1 <= 24)
    {
        break;
    }

    Console.WriteLine("Ongeldige invoer. Voer een getal tussen 0 en 24 in.");
}
TimeOnly tijdstip1;
while (true)
{
    Console.WriteLine($"Hoe laat wordt je {apparaat1} gebruikt? (bijv. 22:30)");

    if (TimeOnly.TryParse(Console.ReadLine(), out tijdstip1))
    {
        break;
    }

    Console.WriteLine("Ongeldige tijd. Gebruik het formaat HH:mm, bijvoorbeeld 22:30.");
}
//Vraag apparaat 2
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Welk apparaat gebruik je?");
string apparaat2 = Console.ReadLine();
float watts2;
while (true)
{
    Console.WriteLine($"Wat zijn het wattage van je {apparaat2}?");
    
    if (float.TryParse(Console.ReadLine(), out watts2) && watts2 >= 0)
    {
        break;
    }

    Console.WriteLine("Ongeldige invoer. Voer een positief getal in, bijvoorbeeld 100.");
}
float uren2;
while (true)
{
    Console.WriteLine($"Hoeveel uren per dag wordt je {apparaat2} gebruikt?");

    if (float.TryParse(Console.ReadLine(), out uren2) && uren2 >= 0 && uren2 <= 24)
    {
        break;
    }

    Console.WriteLine("Ongeldige invoer. Voer een getal tussen 0 en 24 in.");
}
TimeOnly tijdstip2;
while (true)
{
    Console.WriteLine($"Hoe laat wordt je {apparaat2} gebruikt? (bijv. 22:30)");

    if (TimeOnly.TryParse(Console.ReadLine(), out tijdstip2))
    {
        break;
    }

    Console.WriteLine("Ongeldige tijd. Gebruik het formaat HH:mm, bijvoorbeeld 22:30.");
}

//Vraag apparaat 3
Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("Welk apparaat gebruik je?");
string apparaat3 = Console.ReadLine();
float watts3;
while (true)
{
    Console.WriteLine($"Wat zijn het wattage van je {apparaat3}?");
    
    if (float.TryParse(Console.ReadLine(), out watts3) && watts3 >= 0)
    {
        break;
    }

    Console.WriteLine("Ongeldige invoer. Voer een positief getal in, bijvoorbeeld 100.");
}
float uren3;
while (true)
{
    Console.WriteLine($"Hoeveel uren per dag wordt je {apparaat3} gebruikt?");

    if (float.TryParse(Console.ReadLine(), out uren3) && uren3 >= 0 && uren3 <= 24)
    {
        break;
    }

    Console.WriteLine("Ongeldige invoer. Voer een getal tussen 0 en 24 in.");
}
TimeOnly tijdstip3;
while (true)
{
    Console.WriteLine($"Hoe laat wordt je {apparaat3} gebruikt? (bijv. 22:30)");

    if (TimeOnly.TryParse(Console.ReadLine(), out tijdstip3))
    {
        break;
    }

    Console.WriteLine("Ongeldige tijd. Gebruik het formaat HH:mm, bijvoorbeeld 22:30.");
}

//Converteer de input waardes naar floats
//Riskant: Crashgevaar. Als de gebruiker honderd of iets anders verkeerds invoert, stopt het programma met een exception.
// float wattsF1 = float.Parse(watts1); 
// float wattsF2 = float.Parse(watts2);
// float wattsF3 = float.Parse(watts3);
// float urenF1 = float.Parse(uren1); 
// float urenF2 = float.Parse(uren2);
// float urenF3 = float.Parse(uren3);

//piekTariefbedrag
float piekTarief = 0.30f;
float dalTarief = 0.25f;

//Zorgt ervoor dat uren maximaal 24 en minimaal 0 kan zijn
// wattsF1 = Math.Max(wattsF1, 0);
// wattsF2 = Math.Max(wattsF2, 0);
// wattsF3 = Math.Max(wattsF3, 0);

// urenF1 = Math.Clamp(urenF1, 0, 24);
// urenF2 = Math.Clamp(urenF2, 0, 24);
// urenF3 = Math.Clamp(urenF3, 0, 24);

piekTarief = Math.Max(piekTarief, 0.01f);

//Bereken kWh voor elk apparaat
float kW1 = watts1 / 1000;
float kWh1 = kW1 * uren1;
float kW2 = watts2 / 1000;
float kWh2 = kW2 * uren2;
float kW3 = watts3 / 1000;
float kWh3 = kW3 * uren3;

//Ongeldig na ChatGPT review
//float tarief2 = (intTijdstip2 == 22 || intTijdstip2 == 23) ? piekTarief : dalTarief; 
//float tarief3 = (intTijdstip3 == 22 || intTijdstip3 == 23) ? piekTarief : dalTarief;
//Nieuwe logica na ChatGPT review
TimeOnly dalStart = new TimeOnly(23, 0);
TimeOnly dalEinde = new TimeOnly(6, 0);
float tarief1 = (tijdstip1 >= new TimeOnly(23, 0) || tijdstip1 < new TimeOnly(6, 0)) ? dalTarief : piekTarief;
float tarief2 = (tijdstip2 >= new TimeOnly(23, 0) || tijdstip2 < new TimeOnly(6, 0)) ? dalTarief : piekTarief;
float tarief3 = (tijdstip3 >= new TimeOnly(23, 0) || tijdstip3 < new TimeOnly(6, 0)) ? dalTarief : piekTarief;

//Bereken tarief per apparaat
float dagpiekTarief1 = tarief1 * kWh1;
float dagpiekTarief2 = tarief2 * kWh2;
float dagpiekTarief3 = tarief3 * kWh3;

//Bereken dagelijks verbruik voor alle apparaten
float totaleVerbruik =  kWh1 + kWh2 + kWh3;
//Bereken stroomtarieven over het jaar
//Fout: Hier gebruik je voor alles het piektarief, ook wanneer een apparaat in de daluren wordt gebruikt.
//float dagpiekTarief = piekTarief * totaleVerbruik;
//Verbeterde reken logica na LLM (ChatGPT) review
float dagTarief = dagpiekTarief1 + dagpiekTarief2 + dagpiekTarief3;
float maandTarief = dagTarief * 30;
//float jaarpiekTarief = maandpiekTarief * 365; Fout: te hoge tarief omdat er 365 maanden berekenen inplaats van dagen
//Verbeterde reken logica na LLM (ChatGPT) review
float jaarTarief = dagTarief * 365;
//Bereken CO2 uitstoot per jaar
float jaarlijkseCo2 = 427 * totaleVerbruik; 
float jaarlijkseCo2K = jaarlijkseCo2 / 1000;

//Rond alles af op 2 decimalen
string strDagTarief = dagTarief.ToString("F2");
string strmaandTarief = maandTarief.ToString("F2");
string strjaarTarief = jaarTarief.ToString("F2");
string strdagTarief1 = dagpiekTarief1.ToString("F2");
string strdagTarief2 = dagpiekTarief2.ToString("F2");
string strdagTarief3 = dagpiekTarief3.ToString("F2");
string strjaarlijkseCo2K = jaarlijkseCo2K.ToString("F2");

//ASCII console kleuren
string Groen(string tekst) => $"\u001b[32m{tekst}\u001b[0m";
string DonkerPaars(string tekst) => $"\u001b[35m\u001b[2m{tekst}\u001b[0m";
string DonkerRood(string tekst) => $"\u001b[31m\u001b[2m{tekst}\u001b[0m";

//Beschrijf stroom gebruik en berekeningen naar console
Console.ForegroundColor = ConsoleColor.Red;
//Fout: In de zin staat kWh1 kW, terwijl het kWh hoort te zijn
//Console.WriteLine($"{apparaat1} verbruikt {kWh1}kW op een dag | {Groen($"€{strdagTarief1} per dag")}");
//Verbeterde beschrijving na LLM (ChatGPT) review
Console.WriteLine($"{apparaat1} verbruikt {kWh1}kWh op een dag | {Groen($"€{strdagTarief1} per dag")}");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"{apparaat2} verbruikt {kWh2}kWh op een dag | {Groen($"€{strdagTarief2} per dag")}");
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine($"{apparaat3} verbruikt {kWh3}kWh op een dag | {Groen($"€{strdagTarief3} per dag")}");

Console.ForegroundColor = ConsoleColor.Cyan;
if (totaleVerbruik >= 4.8){
    Console.WriteLine($"Totale verbruik op een dag: {DonkerRood($"{totaleVerbruik}kWh")} | {DonkerRood($"€{strDagTarief} per dag")}");
    //Fout: In de zin staat totaleVerbruik, maar er staat strmaandTarief inplaats van totaleVerbruik
    Console.WriteLine($"Jou maandTarief: €{DonkerRood($"{strmaandTarief}kWh")} per maand en {DonkerRood($"€{strjaarTarief}")} per jaar!");
}
else{Console.WriteLine($"Totale verbruik op een dag: {Groen($"{totaleVerbruik}kWh")} | {Groen($"€{strDagTarief} per dag")}"); 
     Console.WriteLine($"Jou maandTarief: {Groen($"{strmaandTarief}kWh")} per maand en {Groen($"€{strjaarTarief}")} per jaar!");
}
Console.ResetColor();
Console.WriteLine($"Jou CO₂ uitstoot is {DonkerPaars($"{strjaarlijkseCo2K}KG")}");