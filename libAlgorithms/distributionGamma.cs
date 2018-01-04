using System;
using System.Collections.Generic;
using System.Text;
using Algorithms;
using ModelGroup;

namespace Distributions
{
    /// <summary>
    /// Uogólniony rozkład dla uogólnionej operacji splotu
    /// Operacja nie korzysta z dodatkowych parametrów opisujących strumienie zgłoszeń
    /// </summary>
    public class rGamma
    {
        /// <summary>
        /// Parametry klasy zgłoszeń, wynikające z rozkładu.
        /// Brak dodatkowych informacji.
        /// </summary>
        public struct klasaUpr
        {
            public int i;
            public int t;
            public double waga;
        }

        /// <summary>
        /// Zagregowane w rozkładzie klasy.
        /// </summary>
        public klasaUpr[] klasy;

        /// <summary>
        /// Wiązka, któej zajętość opisuje rozkład
        /// </summary>
        public Wiazka wiazka;

        /// <summary>
        /// Rozkład zajętości wiązki
        /// </summary>
        public double[] q;

        /// <summary>
        /// Rozkład zajętości dla wiązki pełnodostępnej o takiej samej pojemności
        /// </summary>
        public double[] p;

        public double delta(int n)
        {
            if (n < 0)
                return 1;
            if (p[n] == 0)
                return 1;
            return q[n] / p[n];
        }

        protected static int wyznaczT(double[] stany)
        {
            int wynik = 1;
            for (int n = 1; n < stany.Length; n++)
                if (stany[n] == 0)
                    wynik++;
                else
                    break;

            return wynik;
        }
        static protected double wyznaczWage(double[] stany)
        {
            double waga = 0;
            for (int n = 1; n < stany.Length; n++)
                waga += n * stany[n];
            
            return waga;
        }

        public double sumaWag(int n)
        {
            double wynik = 0;
            foreach (var tmp in klasy)
            {
                if (tmp.t > n)
                    continue;
                wynik += tmp.waga;
            }
            return wynik;
        }

        double V { get { return p.Length - 1; } }

        #region konstruktory

        public rGamma(Wiazka wiazka)
        {
            klasy = new klasaUpr[0];
            this.wiazka = wiazka;

            p = new double[wiazka.V+1];
            q = new double[wiazka.V+1];

            p[0] = 1;
            q[0] = 1;
        }

        public rGamma(Wiazka wiazka, int i, double []stany)
        {
            this.wiazka = wiazka;
            klasy = new klasaUpr[1];

            int t = wyznaczT(stany);

            klasy[0].i = i;
            klasy[0].t = t;
            klasy[0].waga = wyznaczWage(stany);

            p = new double[stany.Length];
            q = new double[stany.Length];
            for (int n = 0; n < stany.Length; n++)
            {
                p[n] = stany[n];
                q[n] = (n < t) ? p[n] : p[n] * delta(n - t) * wiazka.sigmy[i, n - t];
            }
        }
        #endregion konstruktory

        #region Agregacja

        /// <summary>
        /// Współczynnik warunkowego dopuszczenia kombinacji
        /// </summary>
        /// <param name="rC">Rozkład zajętości przez zgłoszenia klas ze zbioru C</param>
        /// <param name="lC">Liczba zajętych PJP przez zgłoszenia klas ze zbioru C</param>
        /// <param name="rD">Rozkład zajętości przez zgłoszenia klas ze zbioru C</param>
        /// <param name="lD">Liczba zajętych PJP przez zgłoszenia klas ze zbioru C</param>
        /// <returns>Prawdopodobieńśtwo dopuszczenia kombinacji</returns>
        protected double gamma(rGamma rC, int lC, rGamma rD, int lD)
        {
            if (lC + lD == 0)
                return 1;

            double gammaC = gammaX(rC, lC, lC + lD);
            double gammaD = gammaX(rD, lD, lC + lD);

            return (lC*gammaC + lD*gammaD)/(lC+lD);
        }

        protected virtual double gammaX(rGamma rX, int lX, int n)
        {
            double gammaX=0;

            double mianownik = 0;
            for (int i=0; i<rX.klasy.Length; i++)
            {
                int t = rX.klasy[i].t;
                if (t > lX)
                    continue;
                if (rX.q[lX - t] == 0)
                    continue;

                double x = rX.q[lX - t] * rX.klasy[i].waga / rX.delta(lX - t);
                mianownik += x;
                gammaX += ( x * rX.wiazka.sigmy[rX.klasy[i].i, n - t]);
            }
            gammaX = (mianownik == 0) ? 0 : gammaX / mianownik;

            return gammaX;
        }

        public void Agreguj(rGamma agregowany)
        {
            int V = p.Length-1;

            double[] pNowy = new double[V+1];
            double[] qNowy = new double[V+1];

            int m = klasy.Length + agregowany.klasy.Length;
            int m1 = klasy.Length;
            int m2 = agregowany.klasy.Length;
            klasaUpr []klasyNowe = new klasaUpr[m];
            for (int i = 0; i < m1; i++)
                klasyNowe[i] = klasy[i];
            for (int i = 0; i < m2; i++)
                klasyNowe[i + m1] = agregowany.klasy[i];

            for (int n=0; n<=V; n++)
            {
                for (int l = 0; l <= n; l++)
                {
                    pNowy[n] += p[l] * agregowany.p[n - l];
                    qNowy[n] += q[l] * agregowany.q[n - l] * gamma(this, l, agregowany, n-l);
                }
            }

            p = pNowy;
            q = qNowy;
            klasy = klasyNowe;
        }

        #endregion
    }


    public class rGammaV1 : rGamma
    {
        #region konstruktory

        public rGammaV1(Wiazka wiazka) : base(wiazka) { }

        public rGammaV1(Wiazka wiazka, int i, double[] stany) : base(wiazka, i, stany) {}
        #endregion konstruktory


        protected override double gammaX(rGamma rX, int lX, int n)
        {
            double gammaX = 0;

            double mianownik = 0;
            for (int i = 0; i < rX.klasy.Length; i++)
            {
                int t = rX.klasy[i].t;
                if (t > lX)
                    continue;
                if (rX.q[lX - t] == 0)
                    continue;

                //double x = rX.q[lX - t] * rX.klasy[i].waga; // / rX.delta(lX - t);
                double x = rX.klasy[i].waga; // / rX.delta(lX - t);
                
                //Licznik
                gammaX += (x * rX.wiazka.sigmy[rX.klasy[i].i, n - t]);
                mianownik += (x * rX.wiazka.sigmy[rX.klasy[i].i, lX - t]);
            }
            gammaX = (mianownik == 0) ? 0 : gammaX / mianownik;

            return gammaX;
        }
    }

    public class rGammaV2 : rGamma
    {
        #region konstruktory

        public rGammaV2(Wiazka wiazka) : base(wiazka) { }

        public rGammaV2(Wiazka wiazka, int i, double[] stany) : base(wiazka, i, stany) { }
        #endregion konstruktory


        protected override double gammaX(rGamma rX, int lX, int n)
        {
            double gammaX = 0;

            double mianownik = 0;
            for (int i = 0; i < rX.klasy.Length; i++)
            {
                int t = rX.klasy[i].t;
                if (t > lX)
                    continue;
                if (rX.q[lX - t] == 0)
                    continue;

                double mu = rX.wiazka.ListaKlasRuchu[rX.klasy[i].i].mu;
                double x = rX.klasy[i].waga/mu; // / rX.delta(lX - t);

                //Licznik
                gammaX += (x * rX.wiazka.sigmy[rX.klasy[i].i, n - t]);
                mianownik += (x * rX.wiazka.sigmy[rX.klasy[i].i, lX - t]);
            }
            gammaX = (mianownik == 0) ? 0 : gammaX / mianownik;

            return gammaX;
        }
    }

    public class rGammaV3 : rGamma
    {
        #region konstruktory

        public rGammaV3(Wiazka wiazka) : base(wiazka) { }

        public rGammaV3(Wiazka wiazka, int i, double[] stany) : base(wiazka, i, stany) { }
        #endregion konstruktory


        protected override double gammaX(rGamma rX, int lX, int n)
        {
            double gammaX = 0;

            double mianownik = 0;
            for (int i = 0; i < rX.klasy.Length; i++)
            {
                int t = rX.klasy[i].t;
                if (t > lX)
                    continue;
                if (rX.q[lX - t] == 0)
                    continue;

                double mu = rX.wiazka.ListaKlasRuchu[rX.klasy[i].i].mu;
                double x = rX.klasy[i].waga * mu; // / rX.delta(lX - t);

                //Licznik
                gammaX += (x * rX.wiazka.sigmy[rX.klasy[i].i, n - t]);
                mianownik += (x * rX.wiazka.sigmy[rX.klasy[i].i, lX - t]);
            }
            gammaX = (mianownik == 0) ? 0 : gammaX / mianownik;

            return gammaX;
        }
    }
}