using System;
using System.Collections.Generic;
using Klasy_serwisu;

namespace Twarde_dane
{
    public static class Stawki
    {
        public static float doplata_osobowy = 1.0f;
        public static float doplata_motocykl = 1.25f;
        public static float doplata_egzotyk = 1.50f; 
        // % bazowej godziny serwisowej
        public static float godzina_serwisowa = 200.0f;
        public static float cena_obowiazkowego_przegladu = 100.0f;

    }
    public static class Baza_Danych
    {
        public static List<Pojazd> Pojazdy = new List<Pojazd>();
        public static int nowyPojazdId;
        public static List<Dzialanie_serwisowe> Dzialania = new List<Dzialanie_serwisowe>();
        public static List<Zlecenie_serwisowe> Zlecenia = new List<Zlecenie_serwisowe>();
        public static int nowyZlecenieId;
        static Baza_Danych()
        {
            Pojazdy.Add(new Osobowy(1, "Skoda", "Fabia", 2001, 376.3f));
            Pojazdy.Add(new Egzotyk(2, "Nissan", "Skyline GTR", 1999, 126.1f));
            Pojazdy.Add(new Motocykl(3, "Honda", "Goldwing", 2015, 86.3f));
            Pojazdy.Add(new Osobowy(4, "Volkswagen", "Golf IV", 2023, 95.7f));
            Pojazdy.Add(new Egzotyk(5, "Koenigsegg", "Jesko", 2021, 12.5f, 20.0f));
            nowyPojazdId = 6;

            Dzialania.Add(new Diagnostyka("Diagnostyka Hamulcow",1.5f,"Szablon dzialania"));
            Dzialania.Add(new Diagnostyka("Diagnostyka Zawieszenia",1.5f,"Szablon dzialania"));
            Dzialania.Add(new Diagnostyka("Diagnostyka Kompleksowa Silnika",5.0f,"Szablon dzialania"));
            Dzialania.Add(new Diagnostyka("Diagnostyka Skrzyni biegow",3.0f,"Szablon dzialania"));
            Dzialania.Add(new Przeglad("Obowiazkowe badanie techniczne",1.0f,"Szablon dzialania",true));
            Dzialania.Add(new Przeglad("Krotki przeglad okresowy",2.0f,"Szablon dzialania"));
            Dzialania.Add(new Przeglad("Kompleksowy przeglad okresowy",4.0f,"Szablon dzialania"));
            Dzialania.Add(new Wymiana("Wymiana klockow hamulcowych",1.0f,"Szablon dzialania",70.0f));
            Dzialania.Add(new Wymiana("Wymiana sprzegla",5.0f,"Szablon dzialania",200.0f));
            Dzialania.Add(new Wymiana("Wymiana uszczelki pod glowica",8.0f,"Szablon dzialania",150.0f));
            
            var serek = new Zlecenie_serwisowe(1,Pojazdy[0]);
            nowyZlecenieId = 2;
            serek.Dodaj_dzialanie(new Diagnostyka("Diagnostyka Kompleksowa Silnika",5.0f,"Gotowe"));
            serek.Dodaj_dzialanie(new Wymiana("Wymiana uszczelki pod glowica",8.0f,"oczekuje na rozpoczecie",150.0f));
            Zlecenia.Add(serek);

            

        }
    }
}