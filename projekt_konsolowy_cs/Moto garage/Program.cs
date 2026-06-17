using System;
using System.Collections.Generic;
using Twarde_dane;
using Klasy_serwisu;

namespace Interfejs
{
    public static class Metody
    {
        public static void Zaprezentuj_liste(IEnumerable<BICM> lista)
        {
            int numerator = 1;
            bool first = true;
            foreach (BICM record in lista)
            {
                record.Print_details(first,numerator.ToString()+"#");
                first = false;
                numerator ++;
            }

        }
        public static void Dodaj_pojazd()
        {
            Console.WriteLine("Wybierz klase pojazdu");
            Console.WriteLine("1. Samochod osobowy");
            Console.WriteLine("2. Motocykl");
            Console.WriteLine("3. Pojazd Egzotyczny");
            string klasa = Console.ReadLine();
            Console.WriteLine("Podaj marke");
            string marka = Console.ReadLine();
            Console.WriteLine("Podaj model");
            string model = Console.ReadLine();
            Console.WriteLine("Podaj rocznik (int)");
            int rocznik = int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj przebieg w tys. km. (float) xx,xx");
            float przebieg = float.Parse(Console.ReadLine());
            switch (klasa)
            {
                case "1":
                    Serwis.Pojazdy.Add(new Osobowy(Serwis.nowyPojazdId,marka,model,rocznik,przebieg));
                    Serwis.nowyPojazdId += 1;
                    break;
                case "2":
                    Serwis.Pojazdy.Add(new Motocykl(Serwis.nowyPojazdId,marka,model,rocznik,przebieg));
                    Serwis.nowyPojazdId += 1;
                    break;
                case "3":
                    Serwis.Pojazdy.Add(new Egzotyk(Serwis.nowyPojazdId,marka,model,rocznik,przebieg));
                    Serwis.nowyPojazdId += 1;
                    break;
            }
        }
        public static void Dodaj_zlecenie()
        {
            Interfejs.Metody.Zaprezentuj_liste(Serwis.Pojazdy);
            Console.WriteLine("Podaj numer# pojazdu");
            string wybor = Console.ReadLine();
            Serwis.Zlecenia.Add(new Zlecenie_serwisowe(Serwis.nowyZlecenieId,Serwis.Pojazdy[int.Parse(wybor)-1]));

        }
        public static void Zmien_status_w_zleceniu()
        {
            Interfejs.Metody.Zaprezentuj_liste(Serwis.Zlecenia);
            Console.WriteLine("Podaj numer# zlecenia");
            string wybor = Console.ReadLine();
            Console.Clear();
            Zlecenie_serwisowe zlecenie = Serwis.Zlecenia[int.Parse(wybor)-1];
            if (zlecenie.StatusZlecenia == "Oczekuje na przydzielenie dzialan serwisowych")
            {
                Console.WriteLine("najpierw dodaj dzialania serwisowe w zleceniu, nacisnij enter aby powrocic do menu");
                Console.ReadLine();
                return;
            }
            Interfejs.Metody.Zaprezentuj_liste(zlecenie.Uslugi);
            Console.WriteLine("Podaj numer# dzialania");
            wybor = Console.ReadLine();
            Dzialanie_serwisowe dzialanie = zlecenie.Uslugi[int.Parse(wybor)-1];
            Console.Clear();
            Console.WriteLine("Wybierz nowy status lub wpisz niestandardowy");
            Console.WriteLine("1.Oczekuje na rozpoczecie");
            Console.WriteLine("2.W trakcie");
            Console.WriteLine("3.Gotowe");
            wybor = Console.ReadLine();
            switch (wybor)
            {
                case "1":
                    dzialanie.Status = "Oczekuje na rozpoczecie";
                    break;
                case "2":
                    dzialanie.Status = "W trakcie";
                    break;
                case "3":
                    dzialanie.Status = "Gotowe";
                    break;
                default:
                    dzialanie.Status = wybor;
                    break;
            }
            zlecenie.Przelicz_status();
        }
        public static void Dodaj_dzialanie_w_zleceniu()
        {
            Interfejs.Metody.Zaprezentuj_liste(Serwis.Zlecenia);
            Console.WriteLine("Podaj numer# zlecenia");
            string wybor = Console.ReadLine();
            Console.Clear();
            Zlecenie_serwisowe zlecenie = Serwis.Zlecenia[int.Parse(wybor)-1];
            Console.WriteLine("Wybierz numer# dzialania z listy");
            Interfejs.Metody.Zaprezentuj_liste(Serwis.Dzialania);
            wybor = Console.ReadLine();
            zlecenie.Dodaj_dzialanie(Serwis.Dzialania[int.Parse(wybor)-1]);

        }
        public static void Usun_dzialanie_w_zleceniu()
        {
            Interfejs.Metody.Zaprezentuj_liste(Serwis.Zlecenia);
            Console.WriteLine("Podaj numer# zlecenia");
            string wybor = Console.ReadLine();
            Console.Clear();
            Zlecenie_serwisowe zlecenie = Serwis.Zlecenia[int.Parse(wybor)-1];
            if (zlecenie.StatusZlecenia == "Oczekuje na przydzielenie dzialan serwisowych")
            {
                Console.WriteLine("najpierw dodaj dzialania serwisowe w zleceniu, nacisnij enter aby powrocic do menu");
                Console.ReadLine();
                return;
            }
            Interfejs.Metody.Zaprezentuj_liste(zlecenie.Uslugi);
            Console.WriteLine("Podaj numer# dzialania do usuniecia");
            wybor = Console.ReadLine();
            zlecenie.Usun_dzialanie(int.Parse(wybor)-1);
        }

    }





    class Program
    {
        static void Main()
        {
            bool running = true;
            while (running == true)
            {
                Console.WriteLine(@" /$$      /$$             /$$                                                                                              
| $$$    /$$$            | $$                                                                                              
| $$$$  /$$$$  /$$$$$$  /$$$$$$    /$$$$$$        /$$$$$$/$$$$   /$$$$$$  /$$$$$$$   /$$$$$$   /$$$$$$   /$$$$$$   /$$$$$$ 
| $$ $$/$$ $$ /$$__  $$|_  $$_/   /$$__  $$      | $$_  $$_  $$ |____  $$| $$__  $$ |____  $$ /$$__  $$ /$$__  $$ /$$__  $$
| $$  $$$| $$| $$  \ $$  | $$    | $$  \ $$      | $$ \ $$ \ $$  /$$$$$$$| $$  \ $$  /$$$$$$$| $$  \ $$| $$$$$$$$| $$  \__/
| $$\  $ | $$| $$  | $$  | $$ /$$| $$  | $$      | $$ | $$ | $$ /$$__  $$| $$  | $$ /$$__  $$| $$  | $$| $$_____/| $$      
| $$ \/  | $$|  $$$$$$/  |  $$$$/|  $$$$$$/      | $$ | $$ | $$|  $$$$$$$| $$  | $$|  $$$$$$$|  $$$$$$$|  $$$$$$$| $$      
|__/     |__/ \______/    \___/   \______/       |__/ |__/ |__/ \_______/|__/  |__/ \_______/ \____  $$ \_______/|__/      
                    by: Mateusz Domagała, Maksymilian Byrdziak, Krystian Cezet                /$$  \ $$                    
                                                                                             |  $$$$$$/                    
                                                                                              \______/                     ");
                Console.WriteLine("Wybierz jedna z akcji");
                Console.WriteLine("1.Wyswietl");
                Console.WriteLine("2.Dodaj");
                Console.WriteLine("3.Edytuj Zlecenie");
                Console.WriteLine("4.Opusc program");
                
                switch(Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Wybierz jedna z kategorii");
                        Console.WriteLine("1.Pojazdy");
                        Console.WriteLine("2.Zlecenia");
                        switch(Console.ReadLine())
                        {
                            case "1":
                                Console.Clear();
                                Interfejs.Metody.Zaprezentuj_liste(Serwis.Pojazdy);
                                break;
                            case "2":
                                Console.Clear();
                                Interfejs.Metody.Zaprezentuj_liste(Serwis.Zlecenia);
                                break;
                        
                        }
                        Console.WriteLine("Nacisnij enter aby wrocic");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("Wybierz jedna z kategorii");
                        Console.WriteLine("1.Pojazdy");
                        Console.WriteLine("2.Zlecenia");
                        switch(Console.ReadLine())
                        {
                            case "1":
                                Console.Clear();
                                Interfejs.Metody.Dodaj_pojazd();
                                break;
                            case "2":
                                Console.Clear();
                                Interfejs.Metody.Dodaj_zlecenie();
                                break;
                        }
                        Console.WriteLine("Nacisnij enter aby wrocic");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine("Wybierz jedna z akcji");
                        Console.WriteLine("1.Zmien status dzialania serwisowego");
                        Console.WriteLine("2.Dodaj dzialanie serwisowe");
                        Console.WriteLine("3.Usun dzialanie serwisowe");
                        switch(Console.ReadLine())
                        {
                            case "1":
                                Interfejs.Metody.Zmien_status_w_zleceniu();
                                break;
                            case "2":
                                Interfejs.Metody.Dodaj_dzialanie_w_zleceniu();
                                break;
                            case "3":
                                Interfejs.Metody.Usun_dzialanie_w_zleceniu();
                                break;
                        }
                        break;
                    case "4":
                        running = false;
                        Console.Clear();
                        Console.WriteLine("Dziekujemy za korzystanie z moto manager :3");
                        Console.WriteLine("Nacisnij enter aby zamknac program");
                        Console.ReadLine();
                        break;

                }
            }
        }
    }
}