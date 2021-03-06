using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;

namespace Algorithms.convolution
{
    #region Iversen
    /// <summary>
    /// Algorytm splotowy dla systemów z procesem przyjmowania zgłoszeń niezależnym od stanu.
    /// Implementacja algorytmu opisanego w TeleBook2008 z 1981 roku.
    /// Algorytm pozwana na wyznaczanie prawdopodobieństwa strat i blokady.
    /// Umożliwia dla każdej klasy wprowadzenie ograniczenia liczby PJP, jaką mogą wszystkie 
    /// jej zgłoszenia zajmować. Liczba ta może być mniejsza od pojemności systemu.
    /// </summary>
    public abstract class aSplotowy : Algorytm
    {
        protected Rozklad[] p;
        protected Rozklad[] P_minusI;

        public aSplotowy(Wiazka algWiazka)
            : base(algWiazka)
        {
        }
        public override bool wymuszalny
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK == 0)                             //nie jest to wiązka pełnodostęna
                    return false;
                return true;
            }
        }
        #region AlgorytmObl
        /// <summary>
        /// Wyznaczanie rozkładów zajętości pojedynczych klas 1, 2, ..., m
        /// </summary>
        protected void Krok1()
        {
            p = new Rozklad[aWiazka.m];
            
            for (int i=0; i<aWiazka.m; i++)
            {
                p[i] = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[i]);
                p[i].normalizacja();
            }
        }
        /// <summary>
        /// Drugi krok kanonicznej postaci algortmu Iversena: Wyznaczanie m zagregowanych
        /// rozkładów zajętości wszystkich klas za wyjątkiem klasy 1,2, ..., m
        /// </summary>
        /// <param name="V">Pojemność wiązki (długość rozkładu V+1)</param>
        /// <param name="normalizacja">Zagregowany rozkład jest znormalizowany</param>
        protected virtual void Krok2(int V, bool normalizacja)
        {
            if (V == 0)
                V = aWiazka.V;
            if (aWiazka.m == 1)
            {
                P_minusI = new Rozklad[1];
                P_minusI[0] = new Rozklad(aWiazka, V);
                return;
            }
            if (aWiazka.m == 2)
            {
                P_minusI = new Rozklad[2];
                P_minusI[0] = new Rozklad(p[1], V, normalizacja);
                P_minusI[1] = new Rozklad(p[0], V, normalizacja);
                return;
            }

            Rozklad[] lewy = new Rozklad[aWiazka.m];
            Rozklad[] prawy = new Rozklad[aWiazka.m];

            lewy[1] = new Rozklad(p[0], V, normalizacja);
            for (int i = 2; i < aWiazka.m; i++)
            {
                lewy[i] = lewy[i - 1] * p[i - 1];
                lewy[i].zmienDlugosc(V, normalizacja);
            }

            prawy[aWiazka.m - 2] = new Rozklad(p[aWiazka.m - 1], V, normalizacja);
            for (int i = aWiazka.m - 3; i >= 0; i--)
            {
                prawy[i] = prawy[i + 1] * p[i + 1];
                prawy[i].zmienDlugosc(V, normalizacja);
            }

            P_minusI = new Rozklad[aWiazka.m];
            P_minusI[0] = prawy[0];
            for (int i = 1; i < aWiazka.m - 1; i++)
            {
                P_minusI[i] = prawy[i] * lewy[i];
                P_minusI[i].zmienDlugosc(V, normalizacja);
            }
            P_minusI[aWiazka.m - 1] = lewy[aWiazka.m - 1];
        }
        /// <summary>
        /// Wyznaczanie prawdopodobieńśtwa blokady oraz prawdopodobieństwa strat dla wszystkich klas.
        /// Rezultaty zostają zapisane w klasie przechowującej wyniki.
        /// </summary>
        /// <param name="nrBad">Numer badania, potrzebny do zapisania wyników</param>
        protected virtual void Krok3(int nrBad)
        {
            int i = 0;
            int vW = aWiazka.V;
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                double E = 0;
                double B = 0;
                double lB = 0;
                double mB = 0;
                double lE = 0;
                double mE = 0;

                for (int n = 0; n <= vW; n++)
                    for (int l = 0; l <= n; l++)
                        mE += (P_minusI[i][n - l] * p[i][l]);

                for (int n = vW - klasa.t + 1; n <= vW; n++)
                    for (int l = 0; l <= n; l++)
                        lE += (P_minusI[i][n - l] * p[i][l]);
                E = lE / mE;
                wynikiAlg.UstawE(nrBad, klasa, E);

                for (int n = 0; n <= vW; n++)
                    for (int l = 0; l <= n; l++)
                        mB += (P_minusI[i][n - l] * p[i][l] * klasa.PodajIntZgl((int)(l / aWiazka.ListaKlasRuchu[i].t)));

                for (int n = vW - klasa.t + 1; n <= vW; n++)
                    for (int l = 0; l <= n; l++)
                        lB += (P_minusI[i][n - l] * p[i][l] * klasa.PodajIntZgl((int)(l / aWiazka.ListaKlasRuchu[i].t)));
                B = lB / mB;
                wynikiAlg.UstawB(nrBad, klasa, B);
                i++;
            }
        }

        #endregion AlgorytmObl
    }
    
    /// <summary>
    /// Uproszczony algorytm splotowy wersja pierwsza.
    /// Algorytm pozwala na wyznaczenie prawdopodobieństwa blokady oraz strat.
    /// Brak możliwości wprowadzenie ograniczenia liczby zasobów jakie mogą być
    /// zajęte przez jedna klasę zgłoszeń. Każda klasa może zająć tyle zasobów, iloma
    /// zasobami dysponuję wiązka, jeśli system nei obsługuje zgłoszeń innych klas.
    /// </summary>
    public class aSplotowyUv1 : aSplotowy
    {
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
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //zmienić
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }


        public aSplotowyUv1(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Iversen v1";
            SkrNazwaAlg = "Ive";
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            Krok1();
            Krok2(aWiazka.V, true);
            Krok3(nrBad);
        }
        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, true, false);
        }
    }
    /// <summary>
    /// Uproszczenie algorytmu Iversena wersja druga.
    /// Algorytm pozwala wyznaczyć tylko prawdopodobieństwo blokady.
    /// </summary>
    public class aSplotowyUv2 : aSplotowy
    {
        public bool normalizowac;
        protected Rozklad P;
        public aSplotowyUv2(Wiazka wAlg)
            : base(wAlg)
        {
            normalizowac = true;
            NazwaAlg = "Iversen v2";
            SkrNazwaAlg = "Iv2";
        }
        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, false, false);
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
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //zmienić
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }

        protected override void Krok2(int V, bool normalizacja)
        {
            if (V == 0)
                V = aWiazka.V;
            
            P = p[0];
            for (int i = 1; i < aWiazka.m; i++)
            {
                P = P * p[i];
                if (normalizacja)
                    P.normalizacja(V);
            }
        }


        protected override void Krok3(int nrBad)
        {
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasa);
                double E = 0;
                //int grBlokady = aWiazka.V - klasa.t + 1;

                for (int n = 0; n <= P.V; n++)
                    E += ((1 - sigmy[nrKlasy, n]) * P[n]);
                wynikiAlg.UstawE(nrBad, klasa, E);
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            //            aWiazka.debug.(this, nrBad); TODO
            Krok1();
            Krok2(aWiazka.V, normalizowac);
            Krok3(nrBad);
        }

        /// <summary>
        /// Zamienia rozklad pojedynczej klasy z procesem przyjmowania
        /// zgloszen niezaleznym od stanu na rozklad z procesem przyjmowania
        /// zgloszen zaleznym od stanu
        /// </summary>
        /// <param name="P">Rozklad pojedynczej klasy z procesem przyjmowania zgloszen niezaleznym od stanu</param>
        /// <param name="sigmy">Sprolczynniki warunkowych przejsc dla wszystkich klas</param>
        protected void DoRozklZalOdStanu(Rozklad P, sigmaPrzyjmZgl sigmy)
        {
            if (P.zagregowaneKlasy.Count != 1)
            {
                throw new Exception("Rozklad moze miec zagregowana tylko jedna klase ruchu");
            }
            trClass klasa = P.zagregowaneKlasy[0];
            int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasa);
            for (int n = klasa.t; n <= P.V; n++)
            {
                double modyfikator = 1;
                for (int l = n - klasa.t; l >= 0; l -= klasa.t)
                {
                    modyfikator *= sigmy[nrKlasy, l];
                    if (sigmy[nrKlasy, l] == 1)
                        break;
                }
                if (modyfikator != 1)
                    P[n] *= modyfikator;
            }
            P.normalizacja();
        }
    }
    #endregion

    /// <summary>
    /// Algorytm wykonujący jednoczesny splot m rozkładów.
    /// Rząd złożoności obliczeniowej Theta(V^m) wyklucza taką metodę do zastosowań inśynierskich.
    /// Modelowanie systemu na poziomie mikrostanów. Algorytm dokładny dla odwracalnego procesu.
    /// TODO: Poprawić implementację. Pozbyć się rekurencji.
    /// </summary>
    public class aRownolegly : aSplotowyUv2
    {
        int[] xSuma;
        public aRownolegly(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Algorytm Rownolegly";
            SkrNazwaAlg = "Rownolegly";
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
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

        protected double oblPrMikrostanu(int stan)
        {
            int[] x = new int[aWiazka.m];
            for (int i = 0; i < aWiazka.m - 1; i++)
                x[i] = xSuma[i] - xSuma[i + 1];
            x[aWiazka.m - 1] = xSuma[aWiazka.m - 1];

            double wynik = p[0][x[0]];
            for (int i = 1; i < aWiazka.m; i++)
            {
                wynik *= p[i][x[i]];
            }

            double sigma = 0;
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                int i = aWiazka.ListaKlasRuchu.IndexOf(klasa);
                if (x[i] > 0)
                {
                    sigma += ((double)(x[i]) / (double)(stan) * aWiazka.sigmy[klasa, stan - klasa.t]); //TODO mnożyć również przez młodsze sigmy
                }
            }
            if (stan < aWiazka.tMax) //TODO dodać tMin
                sigma = 1;
            return wynik * sigma;
        }

        protected double PrMikrostanu(int stan, int metaStan, int nrKlasy)
        {
            xSuma[nrKlasy] = metaStan;
            if (nrKlasy == aWiazka.m - 1)    //to jest ostatnia klasa
            {
                return oblPrMikrostanu(stan);
            }
            double wynik2 = 0;
            for (int n = 0; n <= metaStan; n++)
                wynik2 += PrMikrostanu(stan, metaStan - n, nrKlasy + 1);
            return wynik2;
        }

        protected override void Krok2(int V, bool normalizacja)
        {
            xSuma = new int[aWiazka.m];
            double[] prStanow = new double[V + 1];

            for (int n = 0; n <= V; n++)
            {
                prStanow[n] = PrMikrostanu(n, n, 0);
            }
            P = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[0], prStanow, V);
            for (int i = 1; i <= aWiazka.m - 1; i++)
                P.zagregowaneKlasy.Add(aWiazka.ListaKlasRuchu[i]);
            P.normalizacja();
        }
    }
}
