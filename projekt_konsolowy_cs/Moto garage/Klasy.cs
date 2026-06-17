using System;
using System.Collections.Generic;
using Twarde_dane;
using Interfejs;


namespace Klasy_serwisu
{
    public static class Serwis
    {
        public static List<Pojazd> Pojazdy = new List<Pojazd>();
        public static int nowyPojazdId;
        public static List<Dzialanie_serwisowe> Dzialania = new List<Dzialanie_serwisowe>();
        public static List<Zlecenie_serwisowe> Zlecenia = new List<Zlecenie_serwisowe>();
        public static int nowyZlecenieId;

        static Serwis()
        {
            Pojazdy = Twarde_dane.Baza_Danych.Pojazdy;
            Dzialania = Twarde_dane.Baza_Danych.Dzialania;
            Zlecenia = Twarde_dane.Baza_Danych.Zlecenia;
            nowyPojazdId = Twarde_dane.Baza_Danych.nowyPojazdId;
            nowyZlecenieId = Twarde_dane.Baza_Danych.nowyZlecenieId;
        }
    }
    //Built In Context Menu™
    public interface BICM
    {
        void Print_details(bool header = false,string identifier = " ");
        //void Handle_removal();
        //void Show_related();
    }

    public abstract class Pojazd : BICM
    {
        private int _id;
        private string _marka;
        private string _model;
        private int _rocznik;
        private float _przebieg; // tyś. km.
        private float _doplata;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Marka
        {
            get { return _marka; }
            set { _marka = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public int Rocznik
        {
            get { return _rocznik; }
            set { _rocznik = value; }
        }

        public float Przebieg
        {
            get { return _przebieg; }
            set { _przebieg = value; }
        }

        public float Doplata
        {
            get { return _doplata; }
            set { _doplata = value; }
        }
        
        public Pojazd(int id,string marka,string model,int rocznik,float przebieg)
        {
            Id = id;
            Marka = marka;
            Model = model;
            Rocznik = rocznik;
            Przebieg = przebieg;
            Doplata = 1.0f;
        }

        //Implementacje BICM
        public void Print_details(bool header = false,string identifier = " ") //:base(header) //???????????? :base(header)
        {
            if (header)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine("|| ID ||  Marka  ||  Model  || Klasa Pojazdu || Rocznik || Przebieg (tys km) ||  ");
                Console.WriteLine("==============================================================================");
            }
            Console.WriteLine("==============================================================================");
            Console.WriteLine(identifier+" || "+Id.ToString()+" || "+Marka+" || "+Model+" || "+GetType().ToString()+" || "+Rocznik+" || "+Przebieg+" ||");
            Console.WriteLine("==============================================================================");
        }
    }
    public sealed class Osobowy : Pojazd
    {
        public Osobowy(int id,string marka,string model,int rocznik,float przebieg, float doplata = 0.0f)
            :base(id,marka,model,rocznik,przebieg)
        {
            if (doplata == 0.0f)
            {
                Doplata = Stawki.doplata_osobowy;
            }
            else
            {
                Doplata = doplata;
            }
        }
    }
    public sealed class Motocykl : Pojazd
    {
        public Motocykl(int id,string marka,string model,int rocznik,float przebieg,float doplata = 0.0f)
            :base(id,marka,model,rocznik,przebieg)
        {
            if (doplata == 0.0f)
            {
                Doplata = Stawki.doplata_motocykl;
            }
            else
            {
                Doplata = doplata;
            }
        }
    }
    public sealed class Egzotyk : Pojazd
    {
        public Egzotyk(int id,string marka,string model,int rocznik,float przebieg,float doplata = 0.0f)
            :base(id,marka,model,rocznik,przebieg)
        {
            if (doplata == 0.0f)
            {
                Doplata = Stawki.doplata_egzotyk;
            }
            else
            {
                Doplata = doplata;
            }
        }
    }

    public abstract class Dzialanie_serwisowe : BICM
    {
        private string _nazwa;
        private float _iloscGodzin;
        private string _status;

        public string Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }

        public float IloscGodzin
        {
            get { return _iloscGodzin; }
            set { _iloscGodzin = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public Dzialanie_serwisowe(string nazwa,float iloscGodzin,string status = "Oczekuje na rozpoczecie")
        {
            Nazwa = nazwa;
            IloscGodzin = iloscGodzin;
            Status = status;
        }
        public float Oblicz_koszt_serwisu()
        {
            return IloscGodzin*Twarde_dane.Stawki.godzina_serwisowa;    
        }

        public float Oblicz_koszt_calkowity(float doplata)
        {
            return IloscGodzin*Twarde_dane.Stawki.godzina_serwisowa*doplata;    
        }
        //BICM

        public void Print_details(bool header = false, string identifier = " ")
        {
            if (header)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine("  || Nazwa || Ilosc godzin || Status || Koszt serwisu||  ");
                Console.WriteLine("==============================================================================");
            }
            Console.WriteLine("==============================================================================");
            Console.WriteLine(identifier+" || "+Nazwa+" || "+IloscGodzin+" || "+Status+" || "+this.Oblicz_koszt_serwisu().ToString()+" ||  ");
            Console.WriteLine("==============================================================================");
        }
    }

    public sealed class Diagnostyka : Dzialanie_serwisowe
    {
        public Diagnostyka(string nazwa,float iloscGodzin,string status)
            :base(nazwa,iloscGodzin,status)
        {
        }
    }

    public sealed class Przeglad : Dzialanie_serwisowe
    {
        private bool _obowiazkowe;
        public bool Obowiazkowe
        {
            get {return _obowiazkowe;}
            set {_obowiazkowe = value;}
        }
        public Przeglad(string nazwa,float iloscGodzin,string status,bool obowiazkowe = false)
            :base(nazwa,iloscGodzin,status)
        {
            Obowiazkowe = obowiazkowe;
        }

        public new float Oblicz_koszt_calkowity(float doplata)
        {
            return Twarde_dane.Stawki.cena_obowiazkowego_przegladu;    
        }
    }

    public sealed class Wymiana : Dzialanie_serwisowe
    {
        private float _kosztCzesci;
        public float KosztCzesci
        {
            get { return _kosztCzesci; }
            set { _kosztCzesci = value; }
        }
        public Wymiana(string nazwa,float iloscGodzin,string status,float kosztCzesci = 0.0f)
            :base(nazwa,iloscGodzin,status)
        {
            if (kosztCzesci == 0.0)
            {
                Console.WriteLine("Podaj koszt czesci");
                kosztCzesci = float.Parse(Console.ReadLine()); //brak upewnienia sie ze format sie zgadza
            }
            KosztCzesci = kosztCzesci;
        }
        public new float Oblicz_koszt_serwisu()
        {
            return (IloscGodzin*Twarde_dane.Stawki.godzina_serwisowa)+KosztCzesci;
        }
        public new float Oblicz_koszt_calkowity(float doplata)
        {
            return (IloscGodzin*Twarde_dane.Stawki.godzina_serwisowa*doplata)+KosztCzesci;
        }

        public new void Print_details(bool header = false, string identifier = " ")
        {
            if (header)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine("  || Nazwa || Ilosc godzin || Status || Koszt serwisu || Koszt czesci || ");
                Console.WriteLine("==============================================================================");
            }
            Console.WriteLine("==============================================================================");
            Console.WriteLine(identifier+" || "+Nazwa+" || "+IloscGodzin+" || "+Status+" || "+this.Oblicz_koszt_serwisu().ToString()+" || "+KosztCzesci.ToString()+" || ");
            Console.WriteLine("==============================================================================");
        }
    }

    public class Zlecenie_serwisowe : BICM
    {
        private int _id;
        public int Id
        {
            get{return _id;}
            set{_id = value;}
        }
        private Pojazd _pojazd;
        private List<Dzialanie_serwisowe> _uslugi;
        public List<Dzialanie_serwisowe> Uslugi
        {
            get{return _uslugi;}
            set{_uslugi = value;}
        }
        private float _kosztSerwisu;
        private float _kosztCalkowity;
        private string _statusZlecenia;
        public  string StatusZlecenia
        {
            get{return _statusZlecenia;}
            set{_statusZlecenia = value;}
        }

        public Zlecenie_serwisowe(int id, Pojazd pojazd)
        {
            Id = id;
            _pojazd = pojazd;
            _uslugi = new List<Dzialanie_serwisowe>();
            _kosztSerwisu = 0.0f;
            _kosztCalkowity = 0.0f;
            _statusZlecenia = "Oczekuje na przydzielenie dzialan serwisowych";
        }
        public void Przelicz_koszta()
        {
            float serwisowy = 0.0f;
            float calkowity = 0.0f;
            foreach (Dzialanie_serwisowe usluga in _uslugi)
            {
                serwisowy+= usluga.Oblicz_koszt_serwisu();
                calkowity+= usluga.Oblicz_koszt_calkowity(_pojazd.Doplata);
            }
            _kosztSerwisu = serwisowy;
            _kosztCalkowity = calkowity;
        }
        public void Przelicz_status()
        {
            if (!_uslugi.Any())
            {
                _statusZlecenia = "Oczekuje na przydzielenie dzialan serwisowych";
            }
            else
            {
                bool gotowe = true;
                foreach (Dzialanie_serwisowe usluga in _uslugi)
                {
                    if (usluga.Status != "Gotowe")
                    {
                        gotowe = false;
                    }
                }
                if (gotowe)
                {
                    _statusZlecenia = "Gotowe do finalizacji";
                }
                else
                {
                    _statusZlecenia = "Zlecenie w toku";
                }
            }
        }
        public void Usun_dzialanie(int index)
        {
            _uslugi.RemoveAt(index);
            Przelicz_koszta();
            Przelicz_status();
        }
        public void Dodaj_dzialanie(Dzialanie_serwisowe usluga)
        {
            _uslugi.Add(usluga);
            Przelicz_koszta();
            Przelicz_status();
        }
        //BICM
        public new void Print_details(bool header = false, string identifier = " ")
        {
            if (header)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine("  || Model || Rocznik || Laczny Koszt serwisu ||  Laczny Koszt calkowity || Status || ");
                Console.WriteLine("==============================================================================");
            }
            Console.WriteLine("==============================================================================");
            Console.WriteLine(identifier+" || "+_pojazd.Model+" || "+_pojazd.Rocznik.ToString()+" || "+_kosztSerwisu+" || "+_kosztCalkowity+" || "+_statusZlecenia+" || ");
            Console.WriteLine("==============================================================================");
            Console.WriteLine("  ||  ");
            Console.WriteLine("  || Dzialania Serwisowe");
            Console.WriteLine("  ||  ");
            bool first = true;
            foreach(Dzialanie_serwisowe usluga in _uslugi)
            {
                usluga.Print_details(first);
                first = false;
            }
        }
    }
}