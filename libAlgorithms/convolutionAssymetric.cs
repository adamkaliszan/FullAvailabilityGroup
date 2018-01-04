using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;

namespace Algorithms.convolution.assymetric
{
    public class aMinR : aSplotowyUv2
    {
        public aMinR(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Min R";
            SkrNazwaAlg = "MinR";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK != 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }
        public override bool wymuszalny
        {
            get
            {
                return false;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            Krok1();
            Krok2(aWiazka.V, true);
            Krok3(nrBad);
        }
        protected override void Krok2(int V, bool normalizacja)
        {
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            for (int i = 0; i < aWiazka.m; i++)
                DoRozklZalOdStanu(p[i], sigmy);

            P = new Rozklad(p[aWiazka.m - 1]);
            for (int i = aWiazka.m - 2; i >= 0; i--)
            {
                rAsMin Qab = new rAsMin(P);
                Qab.AgregujR(p[i]);
                Qab.przemnoz(p[i].sumaAt);

                rAsMin Qba = new rAsMin(p[i]);
                Qba.AgregujR(P);
                Qba.przemnoz(P.sumaAt);
                Qba.dodaj(Qab);
                if (normalizacja)
                    Qba.normalizacja();
                P = new Rozklad(Qba);
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }
    public class aSa3R : aSplotowyUv1
    {
        public aSa3R(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Sa3 R";
            SkrNazwaAlg = "Sa3R";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK != 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }
        public override bool wymuszalny
        {
            get
            {
                return false;
            }
        }

        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, false, false);
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            Krok1();
            Krok2(aWiazka.V, false);
            Krok3(nrBad);
        }
        protected override void Krok3(int nrBad)
        {
            rAsSa3[] Q = new rAsSa3[aWiazka.m];
            for (int i = 0; i < aWiazka.m; i++)
            {
                Q[i] = new rAsSa3(P_minusI[i]);
                Q[i].AgregujR(p[i]);
                Q[i].przemnoz(aWiazka.ListaKlasRuchu[i].at);
            }
            Rozklad P = new Rozklad(Q[0]);
            for (int i = 1; i < aWiazka.m; i++)
                P.dodaj(Q[i]);
            P.normalizacja();
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                double E = 0;
                int grBlokady = aWiazka.V - klasa.t + 1;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)
                    grBlokady = aWiazka.q + 1;
                for (int n = grBlokady; n <= aWiazka.V; n++)
                    E += P[n];
                wynikiAlg.UstawE(nrBad, klasa, E);
            }
        }
    }
    public class aMaxR : aSplotowyUv2
    {
        public aMaxR(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Max R";
            SkrNazwaAlg = "MaxR";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK != 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }
        public override bool wymuszalny
        {
            get
            {
                return false;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            Krok1();
            Krok2(aWiazka.V, true);
            Krok3(nrBad);
        }
        protected override void Krok2(int V, bool normalizacja)
        {
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            for (int i = 0; i < aWiazka.m; i++)
                DoRozklZalOdStanu(p[i], sigmy);

            P = new Rozklad(p[0]);
            for (int i = 1; i < aWiazka.m; i++)
            {
                rAsMax Qab = new rAsMax(P);
                Qab.AgregujR(p[i]);
                Qab.przemnoz(p[i].sumaAt);

                rAsMax Qba = new rAsMax(p[i]);
                Qba.AgregujR(P);
                Qba.przemnoz(P.sumaAt);
                Qba.dodaj(Qab);
                if (normalizacja)
                    Qba.normalizacja();
                P = new Rozklad(Qba);
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }

    public class aAsymetrycznyLambdaR : aSplotowyUv2
    {
        public aAsymetrycznyLambdaR(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny LambdaT";
            SkrNazwaAlg = "A LT";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK != 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }
        public override bool wymuszalny
        {
            get
            {
                return false;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            Krok1();
            Krok2(aWiazka.V, true);
            Krok3(nrBad);
        }

        protected override void Krok2(int V, bool normalizacja)
        {
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            for (int i = 0; i < aWiazka.m; i++)
                DoRozklZalOdStanu(p[i], sigmy);

            P = new Rozklad(p[0]);
            for (int i = 1; i < aWiazka.m; i++)
            {
                rAsLambdaT Qab = new rAsLambdaT(P);
                Qab.Agreguj(p[i], sigmy);
                Qab.przemnoz(p[i].sumaAt);

                rAsLambdaT Qba = new rAsLambdaT(p[i]);
                Qba.Agreguj(P, sigmy);
                Qba.przemnoz(P.sumaAt);
                Qba.dodaj(Qab);
                if (normalizacja)
                    Qba.normalizacja();
                P = new Rozklad(Qba);
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }
    public class aAsymetrycznyYt : aSplotowyUv2
    {
        public aAsymetrycznyYt(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Yt";
            SkrNazwaAlg = "A yt";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK != 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }
        public override bool wymuszalny
        {
            get
            {
                return false;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            Krok1();
            Krok2(aWiazka.V, true);
            Krok3(nrBad);
        }

        protected override void Krok2(int V, bool normalizacja)
        {
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            for (int i = 0; i < aWiazka.m; i++)
                DoRozklZalOdStanu(p[i], sigmy);

            rAsYT R = new rAsYT(p[0]);

            for (int i = 1; i < aWiazka.m; i++)
            {
                rAsYT Qab = new rAsYT(R);
                Qab.Agreguj(p[i], p[i], sigmy);
                Qab.przemnoz(p[i].sumaAt);

                rAsYT Qba = new rAsYT(p[i]);
                Qba.Agreguj(R, R.pomocniczy, sigmy);
                Qba.przemnoz(R.sumaAt);
                Qba.dodaj(Qab);
                if (normalizacja)
                    Qba.normalizacja();
                R = new rAsYT(Qba);
            }
            P = new Rozklad(R);
            if (!normalizacja)
                P.normalizacja();
        }
    }
}
