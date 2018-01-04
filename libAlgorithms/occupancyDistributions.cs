using System;
using System.Collections.Generic;
using System.Text;
using Algorithms;
using ModelGroup;

namespace Distributions.stateDependent
{
    /// <summary>
    /// Rozkład zależny od rozkładów pozostałych klas.
    /// </summary>
    class RozkladZalezny
    {
        protected int _v;                           ///długość rozkładu
        public double[][] stany;
        public List<trClass> zagregowaneKlasy;
        public Wiazka rWiazka;

        /// <summary>
        /// Tworzenie zależnego rozkładu zajętości pojedynczej klasu wraz z wyznaczaniem odpowiednich prawdopodobieństw stanów.
        /// </summary>
        /// <param name="Wiazka">Wiązka, której zajętość opisuje rozkład zależny.</param>
        /// <param name="klasaRuchu">Klasa ruchu, której dotyczy ten rozkład zajętości.</param>
        public RozkladZalezny(Wiazka Wiazka, trClass klasaRuchu)
        {
            this._v = Wiazka.V;
            this.rWiazka = Wiazka;
            zagregowaneKlasy = new List<trClass>();
            zagregowaneKlasy.Add(klasaRuchu);
            stany = klasaRuchu.ZaleznyRozkladStanow(Wiazka);
        }

        public RozkladZalezny(RozkladZalezny kopiowany)
        {
            this._v = kopiowany._v;
            this.rWiazka = kopiowany.rWiazka;
            zagregowaneKlasy = new List<trClass>();
            foreach (trClass klasa in kopiowany.zagregowaneKlasy)
                zagregowaneKlasy.Add(klasa);
            stany = new double[_v + 1][];
            for (int nPoz = 0; nPoz <= _v; nPoz++)
            {
                int ostInd = _v - nPoz;
                stany[nPoz] = new double[ostInd+1];
            }
            for (int nPoz = 0; nPoz <= _v; nPoz++)
            {
                int ostInd = _v - nPoz;
                for (int n = 0; n <= ostInd; n++)
                {
                    stany[n][nPoz] = kopiowany[n, nPoz];
                }
            }
        }

        /// <summary>
        /// Normalizacja rozkładu, osobno dla każdej zależności.
        /// </summary>
        public void normalizacja()
        {
            for (int nPoz = 0; nPoz <= rWiazka.V; nPoz++)
            {
                int ostInd = rWiazka.V - nPoz;
                double suma = 0;
                for (int n = 0; n <= ostInd; n++)
                    suma += stany[n][nPoz];

                for (int n = 0; n <= ostInd; n++)
                    stany[n][nPoz] /=suma;
            }
        }

        public void agregacja(RozkladZalezny agregowany)
        {
            foreach (trClass klasa in agregowany.zagregowaneKlasy)
                this.zagregowaneKlasy.Add(klasa);

            double[][] noweStany = new double[rWiazka.V + 1][];

            for (int nPoz = 0; nPoz <= rWiazka.V; nPoz++)
            {
                int ostInd = rWiazka.V - nPoz;
                noweStany[nPoz] = new double[ostInd+1];
            }
            for (int nPoz = 0; nPoz <= rWiazka.V; nPoz++)
            {
                int ostInd = rWiazka.V - nPoz;

                noweStany[0][nPoz] += this[0, nPoz] * agregowany[0, nPoz]; //UWAGA
                for (int n = 0; n <= ostInd; n++)
                {
                    noweStany[n][nPoz] = 0;
                    for (int l=0; l<=n; l++)
                    {
                        noweStany[n][nPoz] += this[l, nPoz + n-l] * agregowany[n - l, nPoz + l]; //UWAGA
                    }
                }
            }
            stany = noweStany;
        }

        /// <summary>
        /// Element rozkładu
        /// </summary>
        /// <param name="n">Liczba PJP zajętych przez zgłoszenia zagregowanych klas w rozkładzie</param>
        /// <param name="n_poz">Warunek rozkładu. Liczba PJP zajętych przez zgłoszenia pozostałych klas</param>
        /// <returns>Prawdopodobieńśtwo stanu w rozkładzie zależnym od pozostałych klas zgłoszeń</returns>
        public double this[int n, int n_poz]
        {
            get
            {
                if (n + n_poz > _v)
                    throw new Exception("Złe argumenty dla rozkładu zależnego");
                return stany[n][n_poz];
            }
            set
            {
                if (n + n_poz > _v)
                    throw new Exception("Złe argumenty dla rozkładu zależnego");
                stany[n][n_poz] = value;
            }
        }
    }
}
