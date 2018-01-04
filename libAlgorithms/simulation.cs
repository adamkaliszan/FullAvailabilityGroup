using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Algorithms;

namespace Simulation
{
    public enum processStage : byte
    {
        zgloszenie = 0,      /// Zgłoszenie napływa do systemu
        oczekiwanie = 1,     /// Ten stan wykorzystywny będzie gdy zostaną dodane systemy kolejkowe
        zakonczenie = 2,     /// Zgłoszenie zostało obsłużone
    }
    public abstract class processSym // : IComparable<procesSym>
    {
        public double czas;

        public agenda ListaZd;

        internal int idx;          // Indeks procesu w tablicy agendy


        public processSym(agenda agendaZd, double wzglCzasZdarzenia)
        {
            ListaZd = agendaZd;
            czas = wzglCzasZdarzenia;
        }

        public virtual void Obsluz() { ;}
        public void CofnijWzglCzas(double cofnijO)
        {
            czas -= cofnijO;
        }

        public override string ToString()
        {
            return string.Format("Czas zdarzenia {0}", czas);
        }
    }

    public abstract class processCall : processSym
    {
        public processStage faza;

        public int zajeteZasoby;
        public SimClass klasaZgl;
        private SimSubGroup _zajPodgr;

        public int t { get { return klasaZgl.t; } }
        public SimSubGroup zajPodgr { get { return _zajPodgr; } set { _zajPodgr = value; } }

        public processCall(agenda agendaZd, double wzglCzasZdarzenia, SimClass klasaZgloszenia)
            : base(agendaZd, wzglCzasZdarzenia)
        {
            _zajPodgr = null;
            klasaZgl = klasaZgloszenia;
            faza = processStage.oczekiwanie;
        }
    }

    public class processErlang : processCall
    {
        public processErlang(agenda agendaZd, double wzglCzasZdarzenia, SimClass klasaZgloszenia)
            : base(agendaZd, wzglCzasZdarzenia, klasaZgloszenia)
        {

        }

        public override string ToString()
        {
            return string.Format("Proces Erlang, czas zdarzenia {0}, faza {1}", czas, faza);
        }

        public override void Obsluz()
        {
            switch (faza)
            {
                case processStage.oczekiwanie:
                    processSym nowe = new processErlang(ListaZd, klasaZgl.czNowegoZgl, klasaZgl);
                    ListaZd.DodajProces(nowe);
                    //Zgłoszenie trafia do systemu
                    if (klasaZgl.sWiazka.DodajZgl(this, ListaZd.zapStatystyk))     //próba zajęcia zasobów
                    {
                        faza = processStage.zgloszenie;
                        czas = klasaZgl.czZakZgl;           // Losowanie czasu zakończenia obsługi zgłoszenia
                        ListaZd.DodajProces(this);              // Ponowne umieszczanie procesu na liście
                    }
                    else
                    {
                        if (ListaZd.zapStatystyk)
                        {
                            klasaZgl.lStraconychZgl++;          // Uaktualnianie statystyk klasy
                            klasaZgl.warWyjsciaStr--;
                            if (klasaZgl.warWyjsciaStr == 0)
                                klasaZgl.sWiazka.WarWyjscia--;  // Warunkiem wyjścia jest odpowiednia liczba str zgłoszeń
                        }
                    }
                    break;
                //Kończenie obsługi zgłoszenia                    
                case processStage.zgloszenie:
                    klasaZgl.sWiazka.UsunZgl(this);             // Zwalnianie zasobów
                    if (ListaZd.zapStatystyk)
                    {
                        klasaZgl.lObsluzonychZgl++;             // Uaktualnianie statystyk klasu
                        klasaZgl.warWyjsciaObsl--;
                        if (klasaZgl.warWyjsciaObsl == 0)
                            klasaZgl.sWiazka.WarWyjscia2--;     // Awaryjnym warunkiem wyjścia jest przesymulowanie okr liczby zgł
                    }
                    faza = processStage.zakonczenie;
                    break;
            }
        }
    }

    public class processEngset : processCall
    {
        public processEngset(agenda agendaZd, double wzglCzasZdarzenia, SimClass klasaZgloszenia)
            : base(agendaZd, wzglCzasZdarzenia, klasaZgloszenia)
        {

        }

        public override string ToString()
        {
            return string.Format("Proces Engset, czas zdarzenia {0}, faza {1}", czas, faza);
        }

        public override void Obsluz()
        {
            switch (faza)
            {
                case processStage.oczekiwanie:
                    //Zgłoszenie trafia do systemu
                    if (klasaZgl.sWiazka.DodajZgl(this, ListaZd.zapStatystyk))     //próba zajęcia zasobów
                    {
                        faza = processStage.zgloszenie;
                        czas = klasaZgl.czZakZgl;           //Losowanie czasu zakończenia obsługi zgłoszenia
                        ListaZd.DodajProces(this);              //Ponowne umieszczanie procesu na liście
                    }
                    else
                    {
                        processSym nowe = new processEngset(ListaZd, klasaZgl.czNowegoZgl, klasaZgl);
                        ListaZd.DodajProces(nowe);
                        if (ListaZd.zapStatystyk)
                        {
                            klasaZgl.lStraconychZgl++;          //Uaktualnianie statystyk klasy
                            klasaZgl.warWyjsciaStr--;
                            if (klasaZgl.warWyjsciaStr == 0)
                                klasaZgl.sWiazka.WarWyjscia--;  //Warunkiem wyjścia jest odpowiednia liczba str zgłoszeń dla każdej z klas
                        }
                    }
                    break;
                //Kończenie obsługi zgłoszenia                    
                case processStage.zgloszenie:
                    klasaZgl.sWiazka.UsunZgl(this);             //Zwalnianie zasobów
                    ListaZd.DodajProces(new processEngset(ListaZd, klasaZgl.czNowegoZgl, klasaZgl));
                    if (ListaZd.zapStatystyk)
                    {
                        klasaZgl.lObsluzonychZgl++;             //Uaktualnianie statystyk klasu
                        klasaZgl.warWyjsciaObsl--;
                        if (klasaZgl.warWyjsciaObsl == 0)
                            klasaZgl.sWiazka.WarWyjscia2--;     //Awaryjnym warunkiem wyjścia jest przesymulowanie okr liczby zgł
                    }
                    faza = processStage.zakonczenie;
                    break;
            }
        }
    }

    public class processPascal : processCall
    {
        private int nrZr;
        private int nrNowoDodanegoZr;
        private SimClassPascal symKlasaPascal;

        public processPascal(agenda agendaZd, double wzglCzasZdarzenia, SimClassPascal klasaZgloszenia, int nrZrodla)
            : base(agendaZd, wzglCzasZdarzenia, klasaZgloszenia)
        {
            nrZr = nrZrodla;
            nrNowoDodanegoZr = -1;
            symKlasaPascal = klasaZgloszenia;
        }

        public override string ToString()
        {
            return string.Format("Proces Pascal, czas zdarzenia {0}, faza {1}", czas, faza);
        }

        public override void Obsluz()
        {
            switch (faza)
            {
                case processStage.oczekiwanie:  //Zgłoszenie trafia do systemu
                    processCall nowe = new processPascal(ListaZd, klasaZgl.czNowegoZgl, symKlasaPascal, nrZr);
                    ListaZd.DodajProces(nowe);
                    symKlasaPascal.zrodla[nrZr]=nowe;

                    if (klasaZgl.sWiazka.DodajZgl(this, ListaZd.zapStatystyk))     //próba zajęcia zasobów
                    {
                        faza = processStage.zgloszenie;
                        czas = klasaZgl.czZakZgl;       //Losowanie czasu zakończenia obsługi zgłoszenia
                        ListaZd.DodajProces(this);          //Ponowne umieszczanie procesu na liście
                        nrNowoDodanegoZr= symKlasaPascal.znajdzWolneZr();
                        symKlasaPascal.zrodla[nrNowoDodanegoZr] = new processPascal(ListaZd, klasaZgl.czNowegoZgl, symKlasaPascal, nrNowoDodanegoZr);
                        ListaZd.DodajProces(symKlasaPascal.zrodla[nrNowoDodanegoZr]);
                    }
                    else
                    {
                        if (ListaZd.zapStatystyk)
                        {
                            klasaZgl.lStraconychZgl++;      //Uaktualnianie statystyk klasy
                            klasaZgl.warWyjsciaStr--;
                            if (klasaZgl.warWyjsciaStr == 0)
                                klasaZgl.sWiazka.WarWyjscia--;    //Warunkiem wyjścia jest odpowiednia liczba str zgłoszeń
                        }
                    }
                    break;
                //Kończenie obsługi zgłoszenia                    
                case processStage.zgloszenie:
                    klasaZgl.sWiazka.UsunZgl(this);         //Zwalnianie zasobów
                    if (ListaZd.zapStatystyk)
                    {
                        klasaZgl.lObsluzonychZgl++;         //Uaktualnianie statystyk klasu
                        klasaZgl.warWyjsciaObsl--;
                        if (klasaZgl.warWyjsciaObsl == 0)
                            klasaZgl.sWiazka.WarWyjscia2--;       //Awaryjnym warunkiem wyjścia jest przesymulowanie okr liczby zgł
                    }
                    faza = processStage.zakonczenie;
                    ListaZd.UsunProces(symKlasaPascal.zrodla[nrNowoDodanegoZr]);
                    symKlasaPascal.zrodla[nrNowoDodanegoZr] = null;
                    break;
            }
        }
    }

    public class SimGroup
    {
        private double[] totRozklZaj;        // Rozkład zajętości całej grupy (prawdopodobieństwa stanów)
        public double[] maxWolnePJP;         // Rozkład dostępnych PJP dla pojedynczego zgłoszenia
        public double[] maxWolnePJPrez;

        private int[] lPrzyjZglWstanieN;     // Warumowe prawdopodobieństwa przejść oszacowane w procesie symulacji
        private int[] lUtrZglWstanieN;       // Warumowe prawdopodobieństwa przejść oszacowane w procesie symulacji
        public SimSubGroup[] sPodgr;
        private reservationAlgorithm aRez;


        public int WarWyjscia;               // Liczba klas, które nie utraciły jeszcze wystarczającej liczby zgłoszeń by zakończyn eksperyment symulacyjny
        public int WarWyjscia2;              // Liczba klas, w których nie obsłużono jeszcze wystarczającej liczby zgłoszeń by zakończyć eksperyment symulacyjny

        private int _V;                      // Pojemność całkowita
        private int _q;                      // Granica rezerwacji (dla R1 i R2)
        private int _K;                      // Liczba podgrup
        private int _n;
        public int n { get { return _n; } }
        private int _maxVi;
        private ChooseArbiter arbWybPodgr;

        public int K { get { return _K; } }
        public int V { get { return _V; } }
        public int maxVi { get { return _maxVi; } }

        public SimGroup(Wiazka wSymulowana)
        {
            _n = 0;
            _K = wSymulowana.sumaK;
            _V = wSymulowana.V;
            totRozklZaj = new double[_V + 1];
            _maxVi = wSymulowana.maxVi;
            maxWolnePJP = new double[maxVi + 1];
            maxWolnePJPrez = new double[maxVi + 1];
            lPrzyjZglWstanieN = new int[_V + 1];
            lUtrZglWstanieN = new int[_V + 1];

            _q = (wSymulowana.AlgorytmRezerwacji == reservationAlgorithm.R3) ? _V : wSymulowana.q;
            sPodgr = new SimSubGroup[_K];
            int lP = 0;
            foreach (Subgroup klPodgr in wSymulowana.ListaPodgrupLaczy)
            {
                int lokQ = (wSymulowana.AlgorytmRezerwacji == reservationAlgorithm.R3) ? klPodgr.v - wSymulowana.tMax : lokQ = klPodgr.v;
                for (int i = 0; i < klPodgr.k; i++)
                    sPodgr[lP++] = new SimSubGroup(klPodgr.v, lokQ, this);
            }
            aRez = wSymulowana.AlgorytmRezerwacji;
            switch (wSymulowana.aWybPodgr)
            {
                case subgroupChooseAlgorithm.sequence:
                    arbWybPodgr = new ChooseArbiterSequence(this);
                    break;
                case subgroupChooseAlgorithm.RR:
                    arbWybPodgr = new ChooseArbiterRR(this);
                    break;
                case subgroupChooseAlgorithm.random:
                    arbWybPodgr = new ChooseArbitrRandom(this);
                    break;
                case subgroupChooseAlgorithm.randomCapacityProportional:
                    arbWybPodgr = new ChooseArbiterRandomProportional(this);
                    break;
                case subgroupChooseAlgorithm.randomOccupancyProportional:
                    arbWybPodgr = new ChooseArbiterOccupancyProportional(this);
                    break;
                default:
                    arbWybPodgr = null;
                    break;
            }
        }
        public void nowaSeria()
        {
            for (int i = 0; i <= _V; i++)
            {
                totRozklZaj[i] = 0;
                lPrzyjZglWstanieN[i] = 0;
                lUtrZglWstanieN[i] = 0;
            }
            for (int i = 0; i <= maxVi; i++)
            {
                maxWolnePJP[i] = 0;
                maxWolnePJPrez[i] = 0;
            }
        }
        public void koniecSerii()
        {
            double czasZbieraniaStatystyk = 0;
            for (int j = 0; j <= maxVi; j++)
                czasZbieraniaStatystyk += maxWolnePJP[j];

            for (int j = 0; j <= maxVi; j++)
            {
                maxWolnePJP[j] = maxWolnePJP[j] / czasZbieraniaStatystyk;
                maxWolnePJPrez[j] = maxWolnePJPrez[j] / czasZbieraniaStatystyk;
            }
        }
        public bool DodajZgl(processCall dodawane, bool zlStatystyki)
        {
            if ((aRez == reservationAlgorithm.R1_R2) && (dodawane.klasaZgl.uprzyw == false))
                if (_n > _q)
                {
                    if (zlStatystyki)
                        dodawane.klasaZgl.klasaRuchu.sigmyStanow.StrZgl(_n);
                    return false;
                }
            int wybrPodgr = arbWybPodgr.WybPodgrupy(dodawane);
            if (wybrPodgr >= 0)
            {
                if (zlStatystyki)
                    dodawane.klasaZgl.klasaRuchu.sigmyStanow.PrzyjZgl(_n);
                dodawane.zajeteZasoby = dodawane.t;
                _n += dodawane.zajeteZasoby;                                  //zwiększanie liczby zaj PJP
                sPodgr[wybrPodgr].ZajmijZasoby(dodawane.zajeteZasoby);
                dodawane.zajPodgr = sPodgr[wybrPodgr];
                return true;
            }
            if (zlStatystyki)
                dodawane.klasaZgl.klasaRuchu.sigmyStanow.StrZgl(_n);
            return false;
        }
        public void UsunZgl(processCall usuwane)
        {
            _n -= usuwane.zajeteZasoby;
            usuwane.zajPodgr.ZwolnijZasoby(usuwane.zajeteZasoby);
        }
        public void Podlicz(double czasBezZmian)
        {
            totRozklZaj[_n] += czasBezZmian;
            int maxDostPJP = sPodgr[0].lwPJP;
            for (int i = 1; i < _K; i++)
                maxDostPJP = (maxDostPJP > sPodgr[i].lwPJP) ? maxDostPJP : sPodgr[i].lwPJP;

            maxWolnePJP[maxDostPJP] += czasBezZmian;
            if ((aRez == reservationAlgorithm.R1_R2) && (_n > _q))
                maxDostPJP = 0;
            maxWolnePJPrez[maxDostPJP] += czasBezZmian;
        }

        public class ChooseArbiter
        {
            protected SimGroup _arbWiazka;
            protected int _ostWybrana;

            public ChooseArbiter(SimGroup wiazkaArbitra)
            {
                _arbWiazka = wiazkaArbitra;
            }
            public virtual int WybPodgrupy(processCall przyjmowane)
            {
                return -1;
            }
        }
        public class ChooseArbiterSequence : ChooseArbiter
        {
            public ChooseArbiterSequence(SimGroup wiazkaArbitra)
                : base(wiazkaArbitra)
            { }
            public override int WybPodgrupy(processCall przyjmowane)
            {
                bool rezerwacja = (_arbWiazka.aRez == reservationAlgorithm.R3);
                for (int wynik = 0; wynik < _arbWiazka.K; wynik++)
                {
                    if (_arbWiazka.sPodgr[wynik].SprDostZas(przyjmowane.t, rezerwacja))
                    {
                        _ostWybrana = wynik;
                        return wynik;
                    }
                }
                return -1;
            }
        }
        public class ChooseArbiterRR : ChooseArbiter
        {
            public ChooseArbiterRR(SimGroup wiazkaArbitra)
                : base(wiazkaArbitra)
            { }
            public override int WybPodgrupy(processCall przyjmowane)
            {
                bool rezerwacja = (_arbWiazka.aRez == reservationAlgorithm.R3);
                for (int i = 0; i < _arbWiazka.K; i++)
                {
                    int spr = (++_ostWybrana) % _arbWiazka.K;
                    if (_arbWiazka.sPodgr[spr].SprDostZas(przyjmowane.t, rezerwacja))
                    {
                        _ostWybrana = spr;
                        return spr;
                    }
                }
                return -1;
            }
        }
        public class ChooseArbitrRandom : ChooseArbiter
        {
            private Random losGen;
            public ChooseArbitrRandom(SimGroup wiazkaArbitra)
                : base(wiazkaArbitra)
            {
                losGen = new Random();
            }
            public override int WybPodgrupy(processCall przyjmowane)
            {
                bool rezerwacja = (_arbWiazka.aRez == reservationAlgorithm.R3);
                int lPas = 0;
                for (int i = 0; i < _arbWiazka.K; i++)
                    if (_arbWiazka.sPodgr[i].SprDostZas(przyjmowane.t, rezerwacja))
                        lPas++;
                if (lPas > 0)
                {
                    int wybLos = (int)(losGen.NextDouble() * lPas);
                    for (int wynik = 0; wynik < _arbWiazka.K; wynik++)
                    {
                        if (_arbWiazka.sPodgr[wynik].SprDostZas(przyjmowane.t, rezerwacja))
                        {
                            if (wybLos == 0)
                            {
                                _ostWybrana = wynik;
                                return wynik;
                            }
                            wybLos--;
                        }
                    }
                    return -2;              //błąd arbitra
                }
                return -1;
            }
        }
        public class ChooseArbiterRandomProportional : ChooseArbiter
        {
            private Random losGen;
            public ChooseArbiterRandomProportional(SimGroup wiazkaArbitra)
                : base(wiazkaArbitra)
            {
                losGen = new Random();
            }
            public override int WybPodgrupy(processCall przyjmowane)
            {
                bool rezerwacja = (_arbWiazka.aRez == reservationAlgorithm.R3);
                int lPropPas = 0;
                for (int i = 0; i < _arbWiazka.K; i++)
                    if (_arbWiazka.sPodgr[i].SprDostZas(przyjmowane.t, rezerwacja))
                        lPropPas += _arbWiazka.sPodgr[i].v;
                if (lPropPas > 0)
                {
                    int wybLos = (int)(losGen.NextDouble() * lPropPas);
                    for (int wynik = 0; wynik < _arbWiazka.K; wynik++)
                    {
                        if (_arbWiazka.sPodgr[wynik].SprDostZas(przyjmowane.t, rezerwacja))
                        {
                            if (wybLos <= _arbWiazka.sPodgr[wynik].v)
                            {
                                _ostWybrana = wynik;
                                return wynik;
                            }
                            wybLos -= _arbWiazka.sPodgr[wynik].v;
                        }
                    }
                    return -2;              //błąd arbitra
                }
                return -1;
            }
        }
        public class ChooseArbiterOccupancyProportional : ChooseArbiter
        {
            private Random losGen;
            public ChooseArbiterOccupancyProportional(SimGroup wiazkaArbitra)
                : base(wiazkaArbitra)
            {
                losGen = new Random();
            }
            public override int WybPodgrupy(processCall przyjmowane)
            {
                bool rezerwacja = (_arbWiazka.aRez == reservationAlgorithm.R3);

                int lPropPas = 0;
                for (int i = 0; i < _arbWiazka.K; i++)
                    if (_arbWiazka.sPodgr[i].SprDostZas(przyjmowane.t, rezerwacja))
                        lPropPas += _arbWiazka.sPodgr[i].lwPJP;
                if (lPropPas > 0)
                {
                    int wybLos = (int)(losGen.NextDouble() * lPropPas);
                    for (int wynik = 0; wynik < _arbWiazka.K; wynik++)
                    {
                        if (_arbWiazka.sPodgr[wynik].SprDostZas(przyjmowane.t, rezerwacja))
                        {
                            if (wybLos <= _arbWiazka.sPodgr[wynik].lwPJP)
                            {
                                _ostWybrana = wynik;
                                return wynik;
                            }
                            wybLos -= _arbWiazka.sPodgr[wynik].lwPJP;
                        }
                    }
                    return -2;              //błąd arbitra
                }
                return -1;
            }
        }
    }
    public class SimSubGroup
    {
        private int _v;
        private int _q;
        private int _n;
        private SimGroup _glWiazka;

        public int v { get { return _v; } }

        public SimSubGroup(int Vi, int Qi, SimGroup sWiazka)
        {
            _n = 0;
            _v = Vi;
            _q = Qi;
            //RozklZaj = new decimal[_v + 1];
            _glWiazka = sWiazka;
        }
        public int lzPJP
        {
            get
            {
                return _n;
            }
        }
        public int lwPJP
        {
            get
            {
                return _v - _n;
            }
        }
        public bool SprDostZas(int lPJP, bool UwzglRez)
        {
            if ((UwzglRez) && (_n > _q))
                return false;
            if (_v - _n >= lPJP)
                return true;
            return false;
        }
        public void ZajmijZasoby(int lZas)
        {
            _n += lZas;
        }
        public void ZwolnijZasoby(int lZas)
        {
            _n -= lZas;
        }
    }
    public abstract class SimClass
    {
        protected Random generator;
        //protected int _t;
        protected double _lambda;
        protected double _mu;
        protected bool _uprzyw;
        protected agenda lZdarzen;
        private SimGroup _sWiazka;

        public long lStraconychZgl;
        public long lObsluzonychZgl;
        
        public long warWyjsciaStr;
        public long warWyjsciaObsl;
        
        private aSimulation Symulacja;
        public trClass klasaRuchu;

        public virtual double WspIntZgl
        {
            get
            {
                if (klasaRuchu.progiKlasy != null)
                    return _lambda * klasaRuchu.progiKlasy.Przedzial(sWiazka.n).mnoznikIntensywnosci;
                return _lambda;
            }
        }
        public double mu
        {
            get
            {
                if (klasaRuchu.progiKlasy != null)
                    return klasaRuchu.progiKlasy.Przedzial(sWiazka.n)._mu;
                return _mu;
            }
        }
        public int t
        {
            get
            {
                if (klasaRuchu.progiKlasy != null)
                {
                    return klasaRuchu.progiKlasy.Przedzial(sWiazka.n).t;
                }
                return klasaRuchu.t;
            }
        }
        public bool uprzyw { get { return _uprzyw; } }
        public SimGroup sWiazka { get { return _sWiazka; } }
        public double czNowegoZgl
        {
            get
            {
                return rWykl(WspIntZgl);
            }
        }
        public double czZakZgl
        {
            get
            {
                return rWykl(this.mu);
            }
        }
        public double prB
        {
            get
            {
                if (lStraconychZgl + lObsluzonychZgl > 0)
                    return (double)(lStraconychZgl) / (double)(lStraconychZgl + lObsluzonychZgl);
                return -1;
            }
        }
        public SimClass(trClass badana, SimGroup sWiazka, agenda listaZd, aSimulation algSymulacji)
        {
            klasaRuchu = badana;
            Symulacja = algSymulacji;
            this._sWiazka = sWiazka;
            lZdarzen = listaZd;
            generator = new Random(badana.GetHashCode());
            _lambda = badana.PodajIntZgl(0);
            if (badana.progiKlasy == null)
                _mu = badana.mu;
            _uprzyw = badana.uprzywilejowana;
            lStraconychZgl = 0;
            lObsluzonychZgl = 0;
        }

        public void NowaSeria()
        {
            lStraconychZgl = 0;
            lObsluzonychZgl = 0;
        }
        private double rWykl(double wspInt)
        {
            double Los = generator.NextDouble();
            if ((Los == 0) || (Los == 1))
                Los = generator.NextDouble();
            double wynik = -Math.Log(Los, Math.E) / wspInt;
            return wynik;
        }
    }
    public class SimClassErlang : SimClass
    {
        public SimClassErlang(trClass badana, SimGroup sWiazka, agenda listaZd, aSimulation algSym)
            : base(badana, sWiazka, listaZd, algSym)
        {
            processSym nowy = new processErlang(listaZd, this.czNowegoZgl, this);
            listaZd.DodajProces(nowy);
        }
    }
    public class SimClassEngset : SimClass
    {
        private int _S;
        private double _gamma;
        public SimClassEngset(trClassEngset badana, SimGroup sWiazka, agenda listaZd, aSimulation algSym)
            : base(badana, sWiazka, listaZd, algSym)
        {
            _S = badana.S;
            _gamma = badana.PodajIntZgl(0) / _S;
            for (int zgl = 0; zgl < _S; zgl++)
            {
                processSym nowy = new processEngset(listaZd, this.czNowegoZgl, this);
                listaZd.DodajProces(nowy);
            }
        }
        public override double WspIntZgl
        {
            get
            {
                if (klasaRuchu.progiKlasy != null)
                    return _gamma * klasaRuchu.progiKlasy.Przedzial(sWiazka.n).mnoznikIntensywnosci;
                return _gamma;
            }
        }
    }
    public class SimClassPascal : SimClass
    {
        private int _S;                     //początkowa liczba źródeł
        private double _gamma;
        private int maxLzr;
        public processCall[] zrodla;

        public SimClassPascal(trClassPascal badana, SimGroup sWiazka, agenda listaZd, aSimulation algSym)
            : base(badana, sWiazka, listaZd, algSym)
        {
            _S = badana.S;
            _gamma = badana.PodajIntZgl(0) / _S;

            if (badana.progiKlasy == null)
            {
                maxLzr = sWiazka.V / badana.t + _S;
            }
            else
            {

                maxLzr = sWiazka.V / badana.tMin + _S;
            }
            zrodla = new processCall[maxLzr];
            for (int i = 0; i < maxLzr; i++)
                zrodla[i] = null;

            for (int zgl = 0; zgl < _S; zgl++)
            {
                zrodla[zgl] = new processPascal(listaZd, this.czNowegoZgl, this, zgl);
                listaZd.DodajProces(zrodla[zgl]);
            }
        }
        public int znajdzWolneZr()
        {
            for (int wynik = _S; wynik < maxLzr; wynik++)
                if (zrodla[wynik] == null)
                    return wynik;
            return -1;
        }
        public override double WspIntZgl
        {
            get
            {
                if (klasaRuchu.progiKlasy != null)
                    return _gamma * klasaRuchu.progiKlasy.Przedzial(sWiazka.n).mnoznikIntensywnosci;
                return _gamma;
            }
        }
    }

    public abstract class agenda
    {
        public double czasOczekiwania;

        public Mutex zajeta;
        public aSimulation AlgSym;

        public abstract void DodajProces(processSym dodawany);
        public abstract processSym PobierzPierwszy();
        public abstract void UsunProces(processSym usuwany);

        public bool zapStatystyk 
        {
            get { return (bool)(czasOczekiwania < 0); }
        }
    }

    public abstract class aSimulation : Algorytm
    {
        public enum confidencyIntervall
        {
            procent95,
            procent99
        }

        protected static readonly double[] WspStFish95 = {0, 
        	6.314, 2.920, 2.353, 2.132, 2.015, 1.943, 1.895, 1.860, 1.833, 1.812,
        	1.796, 1.782, 1.771, 1.761, 1.753, 1.746, 1.740, 1.734, 1.729, 1.725,
        	1.721, 1.717, 1.714, 1.711, 1.708, 1.706, 1.703, 1.701, 1.699, 1.697};

        protected static readonly double[] WspStFish99 = {0, 
        	63.656, 9.925, 5.841, 4.4604, 4.032, 3.707, 3.499, 3.355, 3.250, 3.169,
        	3.108, 3.055, 3.012, 2.977, 2.947, 2.921, 2.898, 2.878, 2.861, 2.845,
        	2.831, 2.819, 2.807, 2.797, 2.787, 2.779, 2.771, 2.763, 2.756, 2.750};

        protected delegate void symDel(object par);

        public readonly string opis;
        public readonly int typSymulacji;

        protected SimGroup[] sWiazka;
        protected agenda[] lZdarzen;

        protected double[, ,] serieE;
        protected double[, ,] serieB;

        protected IBDwynsymulacji bazaDanych;

        protected aSimulation(Wiazka wSymulowana, int typSymulacji, IBDwynsymulacji bazaDanych, string opis)
            : base(wSymulowana)
        {
            this.opis = opis;
            this.typSymulacji = typSymulacji;
            this.bazaDanych = bazaDanych;

            lWatkow = Environment.ProcessorCount;
        }

        public override bool Obliczony
        {
            get { return _Obliczony; }
            set 
            { 
                _Obliczony = value;
                _zainicjalizowany = value;
            }
        }

        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            lSerii = LiczbaSerii;

            lZdarzen = new agenda[LiczbaBadan];
            sWiazka = new SimGroup[LiczbaBadan];

            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
                klasa.inicjacjaSymulacji(lBadan);

            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, true, true);

            for (int badanieNr = 0; badanieNr < lBadan; badanieNr++)
            {
                double aOf = aWiazka.aDelta * badanieNr + aWiazka.aStart;
                InicjacjaNowegoBadaniaSymulacji(aOf, badanieNr);
                wynikiAlg.UstawA(badanieNr, aOf);
            }
        }

        Thread []watki;
        readonly int lWatkow;

        protected void ObliczSredniaOrazBlad(int nrBadania, int lSerii, confidencyIntervall ufnosc)
        {
            double[] sumaE = new double[aWiazka.ListaKlasRuchu.Count];
            double[] sumaB = new double[aWiazka.ListaKlasRuchu.Count];
            for (int l = 0; l < lSerii; l++)
            {
                foreach (trClass klasaR in aWiazka.ListaKlasRuchu)
                {
                    int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasaR);
                    sumaE[nrKlasy] += (double)(serieE[nrKlasy, nrBadania, l]);
                    sumaB[nrKlasy] += (double)(serieB[nrKlasy, nrBadania, l]);
                }
            }
            foreach (trClass klasaR in aWiazka.ListaKlasRuchu)
            {
                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasaR);
                wynikiAlg.UstawE(nrBadania, klasaR, sumaE[nrKlasy] / lSerii);
                wynikiAlg.UstawB(nrBadania, klasaR, sumaB[nrKlasy] / lSerii);
                double kwE = 0;
                double kwB = 0;
                for (int l = 0; l < lSerii; l++)
                {
                    double sredniaE = wynikiAlg.PobE(nrBadania, klasaR);
                    double probkaE = (double)(serieE[nrKlasy, nrBadania, l]);
                    double _blE = sredniaE - probkaE;
                    kwE += (_blE * _blE);
                    double _blB = wynikiAlg.PobB(nrBadania, klasaR) - (double)(serieB[nrKlasy, nrBadania, l]);
                    kwB += (_blB * _blB);
                }
                double grUfnE = 0;
                double grUfnB = 0;

                switch (ufnosc)
                {
                    case confidencyIntervall.procent95:
                        grUfnE = WspStFish95[lSerii] * Math.Sqrt(kwE / (double)(lSerii * lSerii - 1));
                        grUfnB = WspStFish95[lSerii] * Math.Sqrt(kwB / (double)(lSerii * lSerii - 1));
                        break;

                    case confidencyIntervall.procent99:
                        grUfnE = WspStFish99[lSerii] * Math.Sqrt(kwE / (double)(lSerii * lSerii - 1));
                        grUfnB = WspStFish99[lSerii] * Math.Sqrt(kwB / (double)(lSerii * lSerii - 1));
                        break;
                }
                wynikiAlg.UstawBlE(nrBadania, klasaR, grUfnE);
                wynikiAlg.UstawBlB(nrBadania, klasaR, grUfnB);
            }
        }

        /// <summary>
        /// Inicjacja nowego badania w eksperymencie symulacyjnym.
        /// Wykonujemy tylko przed pierwszą seria.
        /// </summary>
        /// <param name="aOf">Wartość ruchu oferowanego pojedynczej PJP</param>
        /// <param name="nrBadania">Numer badania</param>
        protected abstract void InicjacjaNowegoBadaniaSymulacji(double aOf, int nrBadania);

        protected bool symulowac(SimGroup wiazka)
        {
            if ((wiazka.WarWyjscia <= 0) || (wiazka.WarWyjscia2 <= 0))
                return false;
            return true;
        }

        protected void KoniecSerii(int nrBadania, int nrSerii)
        {
            sWiazka[nrBadania].koniecSerii();

            int i = 0;

            foreach (trClass badKl in aWiazka.ListaKlasRuchu)
            {
                serieE[i, nrBadania, nrSerii] = 0;
                int rKlasy;
                if (badKl.progiKlasy == null)
                    rKlasy = badKl.t;
                else
                    rKlasy = badKl.progiKlasy[badKl.progiKlasy.liczbaPrzedziałow - 1].t;

                switch (aWiazka.AlgorytmRezerwacji)
                {
                    case reservationAlgorithm.none:
                        for (int ni = 0; ni < rKlasy; ni++)
                            serieE[i, nrBadania, nrSerii] += sWiazka[nrBadania].maxWolnePJP[ni];
                        break;
                    case reservationAlgorithm.R1_R2:
                        if (badKl.uprzywilejowana)
                            for (int ni = 0; ni < rKlasy; ni++)
                                serieE[i, nrBadania, nrSerii] += sWiazka[nrBadania].maxWolnePJP[ni];
                        else
                            for (int ni = 0; ni < rKlasy; ni++)
                                serieE[i, nrBadania, nrSerii] += sWiazka[nrBadania].maxWolnePJPrez[ni];
                        break;
                    case reservationAlgorithm.R3:
                        for (int ni = 0; ni < aWiazka.tMax; ni++)
                            serieE[i, nrBadania, nrSerii] += sWiazka[nrBadania].maxWolnePJP[ni];
                        break;
                }
                serieB[i, nrBadania, nrSerii] = badKl.symKlasy[nrBadania].prB;
                i++;
            }
        }
        protected void ZapiszWynikiSerii(int nrBadania, int nrSerii)
        {
            int i = 0;
            foreach (trClass badKl in aWiazka.ListaKlasRuchu)
            {
                bazaDanych.zapisEB(wynikiAlg.PobA(nrBadania), nrSerii, aWiazka.parSymulacji, typSymulacji, aWiazka.sysNo, badKl, serieE[i, nrBadania, nrSerii], serieB[i, nrBadania, nrSerii]);
                i++;
            }
        }
        
        protected void NowaSeria(int nrBadania, bool powrot)
        {
            lZdarzen[nrBadania].czasOczekiwania = powrot ? aWiazka.CzStartu : aWiazka.CzStartu / 100;
            sWiazka[nrBadania].WarWyjscia = aWiazka.ListaKlasRuchu.Count;
            sWiazka[nrBadania].WarWyjscia2 = aWiazka.ListaKlasRuchu.Count;

            foreach (trClass klR in aWiazka.ListaKlasRuchu)
            {
                klR.symKlasy[nrBadania].warWyjsciaStr = aWiazka.lSymUtrZgl;
                klR.symKlasy[nrBadania].warWyjsciaObsl = aWiazka.lSymUtrZgl * 10000;
                klR.symKlasy[nrBadania].NowaSeria();
                klR.sigmyStanow.wyczysc();
            }
            sWiazka[nrBadania].nowaSeria();
        }

        protected abstract void SymulujWiazke(int nrBadania, int nrSerii);

        protected void SymulujWiazke(object objNrBadania)
        {
            Tuple<int, int> parametry = (Tuple<int, int>) objNrBadania;

            SymulujWiazke(parametry.Item1, parametry.Item2);
        }

        /// <summary>
        /// Rozpoczęcie eksperymentu symulacyjnego. Metoda uruchomiana w wątku roboczym.
        /// W tym samym wątku uruchomione są pozostałe algorytmy.
        /// Zatem algorytmy sykonywane są kolejno
        /// W przypadku symulacji zostaną uruchomione kolejne wątki, w których odbywać się będize eksperyment symulacyjny.
        /// </summary>
        public override void WymiarujSystem()
        {
            serieE = new double[aWiazka.ListaKlasRuchu.Count, lBadan, lSerii];
            serieB = new double[aWiazka.ListaKlasRuchu.Count, lBadan, lSerii];

            watki = new Thread[Math.Min(lWatkow, lBadan)];
            int[] threadIdx2badanie = new int[watki.Length];
            int[] threadIdx2seria = new int[watki.Length];

            for (int seriaNr = 0; seriaNr < lSerii; seriaNr++)
            {
                for (int badanieNr = 0; badanieNr < lBadan; badanieNr ++)
                {
                    double a = wynikiAlg.PobA(badanieNr);

                    if (bazaDanych.wynikDla(a, seriaNr, aWiazka.parSymulacji, typSymulacji, aWiazka) < aWiazka.m)
                    {
                        int threadIdx = -1;

                        for (int idx = 0; idx < watki.Length; idx++)
                        {
                            if (watki[idx] == null)
                            {
                                threadIdx = idx;
                            }
                        }
                        while (threadIdx < 0)
                        {
                            threadIdx = znajdzZakonczonyWatek();
                            if (threadIdx < 0)
                                continue;

                            ObliczSredniaOrazBlad(threadIdx2badanie[threadIdx], threadIdx2seria[threadIdx] + 1, confidencyIntervall.procent99);
                            prezentacjaWyn();
                        }
                        threadIdx2badanie[threadIdx] = badanieNr;
                        threadIdx2seria[threadIdx] = seriaNr;

                        watki[threadIdx] = new Thread(new ParameterizedThreadStart(new symDel(SymulujWiazke)));
                        watki[threadIdx].Name = string.Format("{0} a={3} seria {4} ({1}/{2})", this.NazwaAlg, threadIdx + 1, watki.Length, a, seriaNr);
                        watki[threadIdx].IsBackground = true;

                        Tuple<int, int> parametry = new Tuple<int, int>(badanieNr, seriaNr);
                        watki[threadIdx].Start(parametry);
                    }
                    else
                    {
                        int i = 0;
                        foreach (trClass tmpKlasa in aWiazka.ListaKlasRuchu)
                        {
                            double tmpE, tmpB;
                            bool odczytano = bazaDanych.odczytEB(a, seriaNr, aWiazka.parSymulacji, typSymulacji, aWiazka.sysNo, tmpKlasa, out tmpE, out tmpB);

                            serieE[i, badanieNr, seriaNr] = tmpE;
                            serieB[i, badanieNr, seriaNr] = tmpB;
                            i++;
                        }
                        aWiazka.DodPost();
                    }
                    
                }
            }

            int tmp = -2;
            do
            {
                tmp = znajdzZakonczonyWatek();
                if (tmp >= 0)
                {
                    watki[tmp] = null;
                }
            }
            while (tmp >= -1);
            

            for (int badanieNr = 0; badanieNr < lBadan; badanieNr++)
                ObliczSredniaOrazBlad(badanieNr, lSerii, confidencyIntervall.procent99);

            prezentacjaWyn();
            Obliczony = true;
        }

        private int znajdzZakonczonyWatek()
        {
            bool istniejeWatek = false;
            for (int idx = 0; idx < watki.Length; idx++)
            {
                if (watki[idx] == null)
                    continue;
                istniejeWatek = true;
                if (watki[idx].ThreadState == ThreadState.Stopped)
                {
                    return idx;
                }
            }
            System.Threading.Thread.Sleep(1000);
            if (istniejeWatek)
                return -1;
            return -2;
        }

        public override void PrzerwijWymiarowanieSystemu()
        {
            if (watki == null)
                return;

            foreach (Thread tmpThr in watki)
            {
                if (tmpThr != null)
                {
                    tmpThr.Abort();
                }
            }
            base.PrzerwijWymiarowanieSystemu();
        }
    }

}
