using System;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;

namespace Algorithms.convolution
{
    enum kierSploatania
    {
        odNajmniejszegoSigma = 1,
        odNajwiekszegoSigma = 2
    }

    /// <summary>
    /// Uogolniony algorytm asymetryczny MIN
    /// </summary>
    public class aMin : aSplotowyUv2
    {
        public aMin(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Min";
            SkrNazwaAlg = "Min";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wi¹zki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wi¹zce nie jest oferowana ¿adna klasa
                    return false;
                if (aWiazka.sumaK == 0)
                    return false;
//                if (aWiazka.AlgorytmRezerwacji == AlgRez.brak)         //zmieniæ
//                    return false;
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

            bool odNajmlodszej = true;

            if (odNajmlodszej)
            {
                P = new Rozklad(p[0]);

                for (int i = 1; i < aWiazka.m; i++)
                {
                    rAsMin Qab = new rAsMin(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMin Qba = new rAsMin(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);

                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            else
            {
                P = new Rozklad(p[aWiazka.m - 1]);

                for (int i = aWiazka.m - 2; i >= 0; i--)
                {
                    rAsMin Qab = new rAsMin(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMin Qba = new rAsMin(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);
                    
                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }
    
    /// <summary>
    /// Uogolniony algorytm MAX
    /// </summary>
    public class aMax : aSplotowyUv2
    {
        public aMax(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Max";
            SkrNazwaAlg = "Max";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wi¹zki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wi¹zce nie jest oferowana ¿adna klasa
                    return false;
                if (aWiazka.sumaK == 0)
                    return false;
//                if (aWiazka.AlgorytmRezerwacji == AlgRez.brak)         //zmieniæ
//                    return false;
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

            bool odNajmlodszej = false;

            if (odNajmlodszej)
            {
                P = new Rozklad(p[0]);

                for (int i = 1; i < aWiazka.m; i++)
                {
                    rAsMax Qab = new rAsMax(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMax Qba = new rAsMax(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);

                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            else
            {
                P = new Rozklad(p[aWiazka.m-1]);

                for (int i = aWiazka.m - 2; i >= 0; i--)
                {
                    rAsMax Qab = new rAsMax(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMax Qba = new rAsMax(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);
                    
                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }

    public class aMin2 : aSplotowyUv2
    {
        public aMin2(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Min2";
            SkrNazwaAlg = "Min2";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wi¹zki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wi¹zce nie jest oferowana ¿adna klasa
                    return false;
                if (aWiazka.sumaK == 0)
                    return false;
                //                if (aWiazka.AlgorytmRezerwacji == AlgRez.brak)         //zmieniæ
                //                    return false;
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

            bool odNajmlodszej = false;

            if (odNajmlodszej)
            {
                P = new Rozklad(p[0]);

                for (int i = 1; i < aWiazka.m; i++)
                {
                    rAsMin Qab = new rAsMin(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMin Qba = new rAsMin(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);

                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            else
            {
                P = new Rozklad(p[aWiazka.m - 1]);

                for (int i = aWiazka.m - 2; i >= 0; i--)
                {
                    rAsMin Qab = new rAsMin(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMin Qba = new rAsMin(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);

                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }

    /// <summary>
    /// Uogolniony algorytm MAX
    /// </summary>
    public class aMax2 : aSplotowyUv2
    {
        public aMax2(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Max2";
            SkrNazwaAlg = "Max2";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wi¹zki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wi¹zce nie jest oferowana ¿adna klasa
                    return false;
                if (aWiazka.sumaK == 0)
                    return false;
                //                if (aWiazka.AlgorytmRezerwacji == AlgRez.brak)         //zmieniæ
                //                    return false;
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

            bool odNajmlodszej = true;

            if (odNajmlodszej)
            {
                P = new Rozklad(p[0]);

                for (int i = 1; i < aWiazka.m; i++)
                {
                    rAsMax Qab = new rAsMax(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMax Qba = new rAsMax(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);

                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            else
            {
                P = new Rozklad(p[aWiazka.m - 1]);

                for (int i = aWiazka.m - 2; i >= 0; i--)
                {
                    rAsMax Qab = new rAsMax(P);
                    Qab.Agreguj(p[i], sigmy);
                    Qab.przemnoz(p[i].sumaAt);

                    rAsMax Qba = new rAsMax(p[i]);
                    Qba.Agreguj(P, sigmy);
                    Qba.przemnoz(P.sumaAt);

                    Qba.dodaj(Qab);
                    P = new Rozklad(Qba);
                    if (normalizacja)
                        P.normalizacja();
                }
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }

    public class aSa3 : aSplotowyUv1
    {
        #region wlasciwosci
        public aSa3(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Asymetryczny Sa3";
            SkrNazwaAlg = "Sa3ogr";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wi¹zki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wi¹zce nie jest oferowana ¿adna klasa
                    return false;
                if (aWiazka.sumaK == 0)
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }
        public override bool wymuszalny { get { return false; } }
        #endregion
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
            Krok3(nrBad, aWiazka.V);
        }
        protected void Krok3(int nrBad, int V)
        {
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            sigmy.obliczSigmy();
            rAsSa3[] Q = new rAsSa3[aWiazka.m];
            for (int i = 0; i < aWiazka.m; i++)
            {
                Q[i] = new rAsSa3(P_minusI[i]);
                Q[i].Agreguj(p[i], sigmy, i);
                //Q[i].normalizacja();
                Q[i].przemnoz(aWiazka.ListaKlasRuchu[i].atProp);
            }
            Rozklad P = new Rozklad(Q[0]);
            for (int i = 1; i < aWiazka.m; i++)
                P.dodaj(Q[i]);
            P.normalizacja();
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasa);
                double E = 0;
                //for (int n = aWiazka.V - klasa.t + 1; n <= aWiazka.V; n++)
                //    E += P[n];
                for (int n=0; n<=aWiazka.V; n++)
                    E += ((1-sigmy[aWiazka.ListaKlasRuchu.IndexOf(klasa), n])* P[n]);
                wynikiAlg.UstawE(nrBad, klasa, E);
            }
        }
    }
    public class aSa3ogrDostV2 : aSplotowyUv1
    {
        #region wlasciwosci
        public aSa3ogrDostV2(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Sa3 ogr dots V2";
            SkrNazwaAlg = "Sa3ogrV2";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wi¹zki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wi¹zce nie jest oferowana ¿adna klasa
                    return false;
                if (aWiazka.sumaK == 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }
        public override bool wymuszalny { get { return false; } }
        #endregion
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
            Krok3(nrBad, aWiazka.V);
        }
        protected void Krok3(int nrBad, int V)
        {
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            sigmy.obliczSigmy();
            rAsUogolniony[] Q = new rAsUogolniony[aWiazka.m];
            for (int i = 0; i < aWiazka.m; i++)
            {
                Q[i] = new rAsUogolniony(P_minusI[i]);
                Q[i].Agreguj(p[i], sigmy, i);
                //Q[i].normalizacja();
                Q[i].przemnoz(aWiazka.ListaKlasRuchu[i].atProp);
            }
            Rozklad P = new Rozklad(Q[0]);
            for (int i = 1; i < aWiazka.m; i++)
                P.dodaj(Q[i]);
            P.normalizacja();
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasa);
                double E = 0;
                //for (int n = aWiazka.V - klasa.t + 1; n <= aWiazka.V; n++)
                //    E += P[n];
                for (int n = 0; n <= aWiazka.V; n++)
                    E += ((1 - sigmy[aWiazka.ListaKlasRuchu.IndexOf(klasa), n]) * P[n]);
                wynikiAlg.UstawE(nrBad, klasa, E);
            }
        }
    }
}
