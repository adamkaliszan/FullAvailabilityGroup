using System;
using System.Collections.Generic;
using System.Text;
using Algorithms;
using ModelGroup;

namespace Distributions
{
   /// <summary>
    /// Uogólniony rozkład zajętości dla systemu z procesem przyjmowania zgłoszeń zależnym od stanu.
    /// Splot jest poprawny, gdy proces napływania zgłoszeń jest niezależny od stanu
    /// </summary>
    public class rGammaSigma: Rozklad
    {
        #region konstruktory
        public rGammaSigma(Wiazka rozWiazka, trClass klZgloszen)
            : base(rozWiazka, klZgloszen)
        {
        }
        public rGammaSigma(Rozklad kopiowany)
            : base(kopiowany)
        {
        }
        #endregion konstruktory

        #region Agregacja

        public void Agreguj(rGammaSigma agregowany, int V)
        {
            double[][] prDopKomb = rGammaSigma.wyznGamma(this, agregowany, wiazka.sigmy, V);
            double []stany = new double[V+1];
            double suma = 0;
            for (int n=0; n<=V; n++)
            {
                for (int lC = 0; lC <= n; lC++)
                {
                    double tmp = this[lC] * agregowany[n - lC] * prDopKomb[lC][n - lC];
                    suma += tmp;
                    stany[n] += tmp;
                }
            }
            foreach (trClass klasaR in agregowany.zagregowaneKlasy)
                zagregowaneKlasy.Add(klasaR);
            if (this.V != V)
                zmienDlugosc(V, false);
            for (int n = 0; n <= V; n++)
                this[n] = stany[n] / suma;
        }



        /// <summary>
        /// Określa prawdopodobieństwa dopuszczenia kombinacji.
        /// Każdy element tablicy [lC][lD] odpowiada prawdopodobieńśtwu dopuszczenia kombinacji (lC, lD).
        /// </summary>
        /// <param name="rC">Zagregowany rozkład zajętości klas ze zbioru C</param>
        /// <param name="rD">Zagregowany rozkład zajętości klas ze zbioru D</param>
        /// <param name="sigmy">Tablica z prawdopodobieńśtwami warunkowych przejść dla wszystkich klas</param>
        /// <param name="V">Pojemność systemu</param>
        /// <returns>Dwuwymiarowa tablica z prawdopodobieńśtwami dopuszczenia kombinacji</returns>
        public static double[][] wyznGamma(rGammaSigma rC, rGammaSigma rD, sigmaPrzyjmZgl sigmy, int V)
        {
            double [][]Gamma = new double[V+1][];
            for (int l = 0; l <= V; l++)
                Gamma[l] = new double[V - l+1];

            int lC = 0;
            int lD = 0;
            while (lD <= V)
            {
                double GammaClicznik = 0;
                double GammaDlicznik = 0;

                double GammaCmianownik = 0;
                double GammaDmianownik = 0;

                foreach (trClass klasa in rC.zagregowaneKlasy)
                {
                    if (rC[lC - klasa.t] == 0)
                        continue;

                    GammaClicznik += klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC-klasa.t][lD] * rC[lC - klasa.t];
                    GammaCmianownik += klasa.at * sigmy[klasa, lC - klasa.t] * rC[lC - klasa.t];
                }
                foreach (trClass klasa in rD.zagregowaneKlasy)
                {
                    if (rD[lD - klasa.t] == 0)
                        continue;

                    GammaDlicznik += klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC][lD - klasa.t] * rD[lD-klasa.t];
                    GammaDmianownik += klasa.at * sigmy[klasa, lD - klasa.t] * rD[lD - klasa.t];
                }

                double GammaC = (GammaCmianownik == 0) ? 0 : GammaClicznik / GammaCmianownik;
                double GammaD = (GammaDmianownik == 0) ? 0 : GammaDlicznik / GammaDmianownik;
                
                if (lC + lD == 0)
                    Gamma[0][0] = 1;
                else
                    Gamma[lC][lD] = (lC * GammaC + lD * GammaD) / (lC + lD);

                lC++;
                if (lC + lD > V)
                {
                    lD++;
                    lC = 0;
                }
            }
            return Gamma;
        }
        #endregion Agregacja
    }

    /// <summary>
    /// Uogólniony rozkład zajętości dla systemu z procesem przyjmowania i napływania zgłoszeń zależnym od stanu.
    /// Średnia liczba obsługiwanych zgłoszeń wyznaczana jest na podstawie rozkładów dla systemu z procesem przyjmowania zgłoszeń zależnym od stanu
    /// </summary>
    public class rGammaSigmaYm1 : rGammaSigma
    {
        public double[][] yi;

        #region konstruktory
        public rGammaSigmaYm1(Wiazka rozWiazka, trClass klZgloszen)
            : base(rozWiazka, klZgloszen)
        {
        }
        public rGammaSigmaYm1(Rozklad kopiowany)
            : base(kopiowany)
        {
        }
        #endregion konstruktory

        #region Agregacja

        public void liczYprocMarkow(sigmaPrzyjmZgl sigma, int V, int lIteracji)
        {
            yi = new double[zagregowaneKlasy.Count][];
            foreach (trClass klasa in zagregowaneKlasy)
            {
                int i = zagregowaneKlasy.IndexOf(klasa);
                yi[i] = new double[V + 1];
                for (int x = 0; x < lIteracji; x++)
                {
                    for (int n = 0; n <= V; n++)
                    {
                        if (n >= klasa.t)
                            yi[i][n] = this[n - klasa.t] * klasa.sigmaZgl(yi[i][n-klasa.t]) * sigma[klasa, n-klasa.t] * klasa.a / this[n];
                    }
                }
            }
        }

        public void Agreguj(rGammaSigmaYm1 agregowany, int V)
        {
            this.liczYprocMarkow(wiazka.sigmy, V, 1);
            agregowany.liczYprocMarkow(wiazka.sigmy, V, 1);

            double[][] prDopKomb = rGammaSigmaYm1.wyznGamma(this, agregowany, wiazka.sigmy, V, yi, agregowany.yi);
            double[] stany = new double[V + 1];
            double suma = 0;
            for (int n = 0; n <= V; n++)
            {
                for (int lC = 0; lC <= n; lC++)
                {
                    double tmp = this[lC] * agregowany[n - lC] * prDopKomb[lC][n - lC];
                    suma += tmp;
                    stany[n] += tmp;
                }
            }
            foreach (trClass klasaR in agregowany.zagregowaneKlasy)
                zagregowaneKlasy.Add(klasaR);
            if (this.V != V)
                zmienDlugosc(V, false);
            for (int n = 0; n <= V; n++)
                this[n] = stany[n] / suma;
        }

        /// <summary>
        /// Określa prawdopodobieństwa dopuszczenia kombinacji.
        /// Każdy element tablicy [lC][lD] odpowiada prawdopodobieńśtwu dopuszczenia kombinacji (lC, lD).
        /// </summary>
        /// <param name="rC">Zagregowany rozkład zajętości klas ze zbioru C</param>
        /// <param name="rD">Zagregowany rozkład zajętości klas ze zbioru D</param>
        /// <param name="sigmy">Tablica z prawdopodobieńśtwami warunkowych przejść dla wszystkich klas</param>
        /// <param name="V">Pojemność systemu</param>
        /// <returns>Dwuwymiarowa tablica z prawdopodobieńśtwami dopuszczenia kombinacji</returns>
        public static double[][] wyznGamma(rGammaSigma rC, rGammaSigma rD, sigmaPrzyjmZgl sigmy, int V, double [][]yC, double [][]yD)
        {
            double[][] Gamma = new double[V + 1][];
            for (int l = 0; l <= V; l++)
                Gamma[l] = new double[V - l + 1];

            int lC = 0;
            int lD = 0;
            while (lD <= V)
            {
                double GammaClicznik = 0;
                double GammaDlicznik = 0;

                double GammaCmianownik = 0;
                double GammaDmianownik = 0;

                foreach (trClass klasa in rC.zagregowaneKlasy)
                {
                    if (rC[lC - klasa.t] == 0)
                        continue;

                    int i = rC.zagregowaneKlasy.IndexOf(klasa);
                    GammaClicznik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC - klasa.t][lD] * rC[lC - klasa.t];
                    GammaCmianownik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC - klasa.t] * rC[lC - klasa.t];
                }
                foreach (trClass klasa in rD.zagregowaneKlasy)
                {
                    if (rD[lD - klasa.t] == 0)
                        continue;

                    int i = rD.zagregowaneKlasy.IndexOf(klasa);

                    GammaDlicznik += klasa.sigmaZgl(yD[i][lD]) * klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC][lD - klasa.t] * rD[lD - klasa.t];
                    GammaDmianownik += klasa.sigmaZgl(yD[i][lD]) * klasa.at * sigmy[klasa, lD - klasa.t] * rD[lD - klasa.t];
                }

                double GammaC = (GammaCmianownik == 0) ? 0 : GammaClicznik / GammaCmianownik;
                double GammaD = (GammaDmianownik == 0) ? 0 : GammaDlicznik / GammaDmianownik;

                if (lC + lD == 0)
                    Gamma[0][0] = 1;
                else
                    Gamma[lC][lD] = (lC * GammaC + lD * GammaD) / (lC + lD);

                lC++;
                if (lC + lD > V)
                {
                    lD++;
                    lC = 0;
                }
            }
            return Gamma;
        }
        #endregion Agregacja
    }

    /// <summary>
    /// Uogólniony rozkład zajętości dla systemu z procesem przyjmowania i napływania zgłoszeń zależnym od stanu.
    /// Średnia liczba obsługiwanych zgłoszeń wyznaczana jest na podstawie rozkładów dla systemu z procesem
    /// przyjmowania zgłoszeń niezależnym od stanu oraz współczynników transformacji.
    /// Klasa zawiera dwa rozkłady - rozkład dla systemy z procesem przyjmowania zależnym i niezależnym od stanu.
    /// Rezultaty splotu są dokładnie takie same, jak dla rGammaYm1
    /// </summary>
    public class rGammaSigmaYm2 : rGammaSigma
    {
        public double[][] yi;
        private Rozklad P;


        #region konstruktory
        public rGammaSigmaYm2(Wiazka rozWiazka, trClass klZgloszen)
            : base(rozWiazka, klZgloszen)
        {
            P = new Rozklad(rozWiazka, klZgloszen); 
        }
        public rGammaSigmaYm2(Rozklad kopiowany)
            : base(kopiowany)
        {
            P = new Rozklad(kopiowany);
        }
        #endregion konstruktory


        public double delta(int n)
        {
            return this[n] / P[n];
        }

        #region Agregacja

        public void liczYprocMarkow(sigmaPrzyjmZgl sigma, int V, int lIteracji)
        {
            yi = new double[zagregowaneKlasy.Count][];
            foreach (trClass klasa in zagregowaneKlasy)
            {
                int i = zagregowaneKlasy.IndexOf(klasa);
                yi[i] = new double[V + 1];
                for (int x = 0; x < lIteracji; x++)
                {
                    for (int n = 0; n <= V; n++)
                    {
                        if (n >= klasa.t)
                            yi[i][n] = delta(n-klasa.t) * P[n - klasa.t] * klasa.sigmaZgl(yi[i][n - klasa.t]) * sigma[klasa, n - klasa.t] * klasa.a / (delta(n) * P[n]);
                    }
                }
            }
        }

        public void Agreguj(rGammaSigmaYm2 agregowany, int V)
        {
            P.Agreguj(agregowany.P);
            this.liczYprocMarkow(wiazka.sigmy, V, 1);
            agregowany.liczYprocMarkow(wiazka.sigmy, V, 1);

            double[][] prDopKomb = rGammaSigmaYm2.wyznGamma(this, agregowany, wiazka.sigmy, V, yi, agregowany.yi);
            double[] stany = new double[V + 1];
            double suma = 0;
            for (int n = 0; n <= V; n++)
            {
                for (int lC = 0; lC <= n; lC++)
                {
                    double tmp = this[lC] * agregowany[n - lC] * prDopKomb[lC][n - lC];
                    suma += tmp;
                    stany[n] += tmp;
                }
            }
            foreach (trClass klasaR in agregowany.zagregowaneKlasy)
                zagregowaneKlasy.Add(klasaR);
            if (this.V != V)
                zmienDlugosc(V, false);
            for (int n = 0; n <= V; n++)
                this[n] = stany[n] / suma;
        }

        /// <summary>
        /// Określa prawdopodobieństwa dopuszczenia kombinacji.
        /// Każdy element tablicy [lC][lD] odpowiada prawdopodobieńśtwu dopuszczenia kombinacji (lC, lD).
        /// </summary>
        /// <param name="rC">Zagregowany rozkład zajętości klas ze zbioru C</param>
        /// <param name="rD">Zagregowany rozkład zajętości klas ze zbioru D</param>
        /// <param name="sigmy">Tablica z prawdopodobieńśtwami warunkowych przejść dla wszystkich klas</param>
        /// <param name="V">Pojemność systemu</param>
        /// <returns>Dwuwymiarowa tablica z prawdopodobieńśtwami dopuszczenia kombinacji</returns>
        public static double[][] wyznGamma(rGammaSigma rC, rGammaSigma rD, sigmaPrzyjmZgl sigmy, int V, double[][] yC, double[][] yD)
        {
            double[][] Gamma = new double[V + 1][];
            for (int l = 0; l <= V; l++)
                Gamma[l] = new double[V - l + 1];

            int lC = 0;
            int lD = 0;
            while (lD <= V)
            {
                double GammaClicznik = 0;
                double GammaDlicznik = 0;

                double GammaCmianownik = 0;
                double GammaDmianownik = 0;

                foreach (trClass klasa in rC.zagregowaneKlasy)
                {
                    if (rC[lC - klasa.t] == 0)
                        continue;

                    int i = rC.zagregowaneKlasy.IndexOf(klasa);
                    GammaClicznik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC - klasa.t][lD] * rC[lC - klasa.t];
                    GammaCmianownik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC - klasa.t] * rC[lC - klasa.t];
                }
                foreach (trClass klasa in rD.zagregowaneKlasy)
                {
                    if (rD[lD - klasa.t] == 0)
                        continue;

                    int i = rD.zagregowaneKlasy.IndexOf(klasa);

                    GammaDlicznik += klasa.sigmaZgl(yD[i][lD]) * klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC][lD - klasa.t] * rD[lD - klasa.t];
                    GammaDmianownik += klasa.sigmaZgl(yD[i][lD]) * klasa.at * sigmy[klasa, lD - klasa.t] * rD[lD - klasa.t];
                }

                double GammaC = (GammaCmianownik == 0) ? 0 : GammaClicznik / GammaCmianownik;
                double GammaD = (GammaDmianownik == 0) ? 0 : GammaDlicznik / GammaDmianownik;

                if (lC + lD == 0)
                    Gamma[0][0] = 1;
                else
                    Gamma[lC][lD] = (lC * GammaC + lD * GammaD) / (lC + lD);

                lC++;
                if (lC + lD > V)
                {
                    lD++;
                    lC = 0;
                }
            }
            return Gamma;
        }
        #endregion Agregacja
    }


    /// <summary>
    /// Bardzo duża złożoność obliczeniowa. Prawdopodobnie najdokłądniejszy algorytm
    /// </summary>
    public class rGammaYc1: Rozklad
    {
        /// <summary>
        /// Zagregowane rozkłady wszystkich klas ze zbioru zagregowanych klas za wyjątkiem klasy 1, 2, ...
        /// </summary>
        private rGammaYc1[] Qmi;
        private Rozklad[] qi;

        public double[][] yi;

        #region konstruktory

        public rGammaYc1(Wiazka rWiazka): base(rWiazka, rWiazka.V)
        {
            Qmi = null;
            qi = null;
        }

        public rGammaYc1(Wiazka rWiazka, trClass klasa): base(rWiazka, klasa, true)
        {
            zagregowaneKlasy = new List<trClass>();
            zagregowaneKlasy.Add(klasa);
            this.wiazka = rWiazka;

            qi = new Rozklad[1];
            qi[0] = new Rozklad(rWiazka, zagregowaneKlasy[0], true);
            Qmi = new rGammaYc1[1];
            Qmi[0] = new rGammaYc1(rWiazka); //Ten rozkład jest pusty.
        }

        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="rozklad">Rozkład kopiowany</param>
        public rGammaYc1(rGammaYc1 kopiowany): base(kopiowany)
        {
            qi = kopiowany.qi;
            Qmi = kopiowany.Qmi;
        }

        #endregion konstruktory

        #region Agregacja

        public void liczYspl(sigmaPrzyjmZgl sigma, int V)
        {
            yi = new double[zagregowaneKlasy.Count][];
            if (zagregowaneKlasy.Count == 1)
            {
                yi[0] = new double[V+1];
                for (int n = 0; n <= V; n += zagregowaneKlasy[0].t)
                    yi[0][n] = (double)(n) / (double)(zagregowaneKlasy[0].t);
                return;
            }

            for (int i = 0; i < zagregowaneKlasy.Count; i++)
            {
                trClass klasa = zagregowaneKlasy[i];
                Qmi[i].liczYspl(sigma, V); //było to już wyliczone na potrzeby splotu

                double[][] prDopKomb = rGammaYc1.wyznGamma(Qmi[i], qi[i], klasa, wiazka.sigmy, V, Qmi[i].yi);
                yi[i] = new double[V + 1];

                for (int n = 0; n <= V; n++)
                {
                    double P_n = 0;
                    double Ex_n = 0;

                    for (int l = 0; l <= n; l+=klasa.t)
                    {
                        P_n += (Qmi[i][n - l] * qi[i][l] * prDopKomb[n - l][l]);
                        Ex_n += (l / klasa.t * Qmi[i][n - l] * qi[i][l] * prDopKomb[n-l][l]);
                    }
                    yi[i][n] = Ex_n / P_n;
                }
            }
        }

        /// <summary>
        /// Wyznacza nowy zagregowany rozkład na podstawie istniejącego oraz nowej klasy zgłoszeń
        /// </summary>
        /// <param name="klasa">Nowa, agregowana klasa zgłoszeń</param>
        /// <returns></returns>
        public override Rozklad zagregujKlase(trClass klasa)
        {
            if (zagregowaneKlasy.Count == 0)
            {
                return new rGammaYc1(wiazka, klasa);
            }

            rGammaYc1 wynik = new rGammaYc1(this);
            Rozklad q = new Rozklad(wiazka, klasa, true);

            // Wyznaczanie średniej liczby obsługiwanych zgłoszeń
            liczYspl(wiazka.sigmy, wiazka.V);

            //Operacja splotu

            double[][] prDopKomb = rGammaYc1.wyznGamma(this, q, klasa, wiazka.sigmy, V, yi);
            double[] stany = new double[V + 1];
            double suma = 0;

            for (int n = 0; n <= V; n++)
            {
                for (int lC = 0; lC <= n; lC++)
                {
                    double tmp = this[lC] * q[n - lC] * prDopKomb[lC][n - lC];
                    suma += tmp;
                    stany[n] += tmp;
                }
            }

            for (int n = 0; n <= V; n++)
                wynik[n] = stany[n] / suma;

            // Uaktualnianie rozkładów pomocniczych

            wynik.qi = new Rozklad[zagregowaneKlasy.Count + 1];
            for (int i = 0; i < zagregowaneKlasy.Count; i++)
                wynik.qi[i] = this.qi[i];
            wynik.qi[zagregowaneKlasy.Count] = q;

            wynik.Qmi = new rGammaYc1[zagregowaneKlasy.Count + 1];
            for (int i = 0; i < zagregowaneKlasy.Count; i++)
                wynik.Qmi[i] = Qmi[i].zagregujKlase(klasa) as rGammaYc1;
            wynik.Qmi[zagregowaneKlasy.Count] = this;

            wynik.zagregowaneKlasy.Add(klasa);

            return wynik;
        }

        /// <summary>
        /// Określa prawdopodobieństwa dopuszczenia kombinacji.
        /// Każdy element tablicy [lC][lD] odpowiada prawdopodobieńśtwu dopuszczenia kombinacji (lC, lD).
        /// </summary>
        /// <param name="rC">Zagregowany rozkład zajętości klas ze zbioru C dla systemu z procesem przyjmowania zgłoszeń zależnym od stanu</param>
        /// <param name="q">Rozkład pojedynczej klasy</param>
        /// <param name="sigmy">Tablica z prawdopodobieńśtwami warunkowych przejść dla wszystkich klas</param>
        /// <param name="V">Pojemność systemu</param>
        /// <returns>Dwuwymiarowa tablica z prawdopodobieńśtwami dopuszczenia kombinacji</returns>
        public static double[][] wyznGamma(Rozklad rC, Rozklad q, trClass klasa_q, sigmaPrzyjmZgl sigmy, int V, double[][] yC)
        {
            double[] yq = new double[V + 1];
            for (int n = 0; n <= V; n += klasa_q.t)
                yq[n] = n / klasa_q.t;

            double[][] Gamma = new double[V + 1][];
            for (int l = 0; l <= V; l++)
                Gamma[l] = new double[V - l + 1];

            int lC = 0;
            int lD = 0;
            while (lD <= V)
            {
                double GammaClicznik = 0;
                double GammaDlicznik = 0;

                double GammaCmianownik = 0;
                double GammaDmianownik = 0;

                foreach (trClass klasa in rC.zagregowaneKlasy)
                {
                    if (rC[lC - klasa.t] == 0)
                        continue;

                    int i = rC.zagregowaneKlasy.IndexOf(klasa);
                    GammaClicznik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC - klasa.t][lD] * rC[lC - klasa.t];
                    GammaCmianownik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC - klasa.t] * rC[lC - klasa.t];
                }

                if (q[lD - klasa_q.t] != 0)
                {
                    GammaDlicznik += klasa_q.sigmaZgl(q[lD]) * klasa_q.at * sigmy[klasa_q, lC + lD - klasa_q.t] * Gamma[lC][lD - klasa_q.t] * q[lD - klasa_q.t];
                    GammaDmianownik += klasa_q.sigmaZgl(q[lD]) * klasa_q.at * sigmy[klasa_q, lD - klasa_q.t] * q[lD - klasa_q.t];
                }

                double GammaC = (GammaCmianownik == 0) ? 0 : GammaClicznik / GammaCmianownik;
                double GammaD = (GammaDmianownik == 0) ? 0 : GammaDlicznik / GammaDmianownik;

                if (lC + lD == 0)
                    Gamma[0][0] = 1;
                else
                    Gamma[lC][lD] = (lC * GammaC + lD * GammaD) / (lC + lD);

                lC++;
                if (lC + lD > V)
                {
                    lD++;
                    lC = 0;
                }
            }
            return Gamma;
        }
        #endregion Agregacja
    }

    /// <summary>
    /// Uproszczone podejście do opreacji splotu rGammaYc1. Minimalny wpływ na pogorszenie dokładności.
    /// </summary>
    public class rGammaYc2 : Rozklad
    {
        /// <summary>
        /// Zagregowane rozkłady wszystkich klas ze zbioru zagregowanych klas za wyjątkiem klasy 1, 2, ...
        /// </summary>
        private Rozklad[] Pmi;
        private Rozklad[] pi;

        public double[][] yi;

        #region konstruktory

        public rGammaYc2(Wiazka rWiazka)
            : base(rWiazka, rWiazka.V)
        {
            Pmi = null;
            pi = null;
        }

        public rGammaYc2(Wiazka rWiazka, trClass klasa)
            : base(rWiazka, klasa, true)
        {
            zagregowaneKlasy = new List<trClass>();
            zagregowaneKlasy.Add(klasa);
            this.wiazka = rWiazka;

            pi = new Rozklad[1];
            pi[0] = new Rozklad(rWiazka, zagregowaneKlasy[0], true);
            Pmi = new Rozklad[1];
            Pmi[0] = new Rozklad(rWiazka, rWiazka.V); //Ten rozkład jest pusty.
        }

        /// <summary>
        /// Konstruktor kopiujący
        /// </summary>
        /// <param name="rozklad">Rozkład kopiowany</param>
        public rGammaYc2(rGammaYc2 kopiowany)
            : base(kopiowany)
        {
            pi = kopiowany.pi;
            Pmi = kopiowany.Pmi;
        }

        #endregion konstruktory

        #region Agregacja

        public void liczYspl(sigmaPrzyjmZgl sigma, int V)
        {
            yi = new double[zagregowaneKlasy.Count][];
            if (zagregowaneKlasy.Count == 1)
            {
                yi[0] = new double[V + 1];
                for (int n = 0; n <= V; n += zagregowaneKlasy[0].t)
                    yi[0][n] = (double)(n) / (double)(zagregowaneKlasy[0].t);
                return;
            }

            for (int i = 0; i < zagregowaneKlasy.Count; i++)
            {
                trClass klasa = zagregowaneKlasy[i];
                
                yi[i] = new double[V + 1];

                for (int n = 0; n <= V; n++)
                {
                    double P_n = 0;
                    double Ex_n = 0;

                    for (int l = 0; l <= n; l += klasa.t)
                    {
                        P_n += (Pmi[i][n - l] * pi[i][l]);
                        Ex_n += (l / klasa.t * Pmi[i][n - l] * pi[i][l]);
                    }
                    yi[i][n] = Ex_n / P_n;
                }
            }
        }

        /// <summary>
        /// Wyznacza nowy zagregowany rozkład na podstawie istniejącego oraz nowej klasy zgłoszeń
        /// </summary>
        /// <param name="klasa">Nowa, agregowana klasa zgłoszeń</param>
        /// <returns></returns>
        public override Rozklad zagregujKlase(trClass klasa)
        {
            if (zagregowaneKlasy.Count == 0)
            {
                return new rGammaYc2(wiazka, klasa);
            }

            rGammaYc2 wynik = new rGammaYc2(this);
            Rozklad p = new Rozklad(wiazka, klasa, true);

            // Wyznaczanie średniej liczby obsługiwanych zgłoszeń
            liczYspl(wiazka.sigmy, wiazka.V);

            //Operacja splotu

            double[][] prDopKomb = rGammaYc2.wyznGamma(this, p, klasa, wiazka.sigmy, V, yi);
            double[] stany = new double[V + 1];
            double suma = 0;

            for (int n = 0; n <= V; n++)
            {
                for (int lC = 0; lC <= n; lC++)
                {
                    double tmp = this[lC] * p[n - lC] * prDopKomb[lC][n - lC];
                    suma += tmp;
                    stany[n] += tmp;
                }
            }

            for (int n = 0; n <= V; n++)
                wynik[n] = stany[n] / suma;

            // Uaktualnianie rozkładów pomocniczych

            wynik.pi = new Rozklad[zagregowaneKlasy.Count + 1];
            for (int i = 0; i < zagregowaneKlasy.Count; i++)
                wynik.pi[i] = this.pi[i];
            wynik.pi[zagregowaneKlasy.Count] = p;

            wynik.Pmi = new Rozklad[zagregowaneKlasy.Count + 1];
            for (int i = 0; i < zagregowaneKlasy.Count; i++)
                wynik.Pmi[i] = Pmi[i].zagregujKlase(klasa);
            wynik.Pmi[zagregowaneKlasy.Count] = Pmi[0].zagregujKlase(wiazka.ListaKlasRuchu[0]);

            wynik.zagregowaneKlasy.Add(klasa);

            return wynik;
        }

        /// <summary>
        /// Określa prawdopodobieństwa dopuszczenia kombinacji.
        /// Każdy element tablicy [lC][lD] odpowiada prawdopodobieńśtwu dopuszczenia kombinacji (lC, lD).
        /// </summary>
        /// <param name="rC">Zagregowany rozkład zajętości klas ze zbioru C dla systemu z procesem przyjmowania zgłoszeń zależnym od stanu</param>
        /// <param name="q">Rozkład pojedynczej klasy</param>
        /// <param name="sigmy">Tablica z prawdopodobieńśtwami warunkowych przejść dla wszystkich klas</param>
        /// <param name="V">Pojemność systemu</param>
        /// <returns>Dwuwymiarowa tablica z prawdopodobieńśtwami dopuszczenia kombinacji</returns>
        public static double[][] wyznGamma(Rozklad rC, Rozklad q, trClass klasa_q, sigmaPrzyjmZgl sigmy, int V, double[][] yC)
        {
            double[] yq = new double[V + 1];
            for (int n = 0; n <= V; n += klasa_q.t)
                yq[n] = n / klasa_q.t;

            double[][] Gamma = new double[V + 1][];
            for (int l = 0; l <= V; l++)
                Gamma[l] = new double[V - l + 1];

            int lC = 0;
            int lD = 0;
            while (lD <= V)
            {
                double GammaClicznik = 0;
                double GammaDlicznik = 0;

                double GammaCmianownik = 0;
                double GammaDmianownik = 0;

                foreach (trClass klasa in rC.zagregowaneKlasy)
                {
                    if (rC[lC - klasa.t] == 0)
                        continue;

                    int i = rC.zagregowaneKlasy.IndexOf(klasa);
                    GammaClicznik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC - klasa.t][lD] * rC[lC - klasa.t];
                    GammaCmianownik += klasa.sigmaZgl(yC[i][lC]) * klasa.at * sigmy[klasa, lC - klasa.t] * rC[lC - klasa.t];
                }

                if (q[lD - klasa_q.t] != 0)
                {
                    GammaDlicznik += klasa_q.sigmaZgl(q[lD]) * klasa_q.at * sigmy[klasa_q, lC + lD - klasa_q.t] * Gamma[lC][lD - klasa_q.t] * q[lD - klasa_q.t];
                    GammaDmianownik += klasa_q.sigmaZgl(q[lD]) * klasa_q.at * sigmy[klasa_q, lD - klasa_q.t] * q[lD - klasa_q.t];
                }

                double GammaC = (GammaCmianownik == 0) ? 0 : GammaClicznik / GammaCmianownik;
                double GammaD = (GammaDmianownik == 0) ? 0 : GammaDlicznik / GammaDmianownik;

                if (lC + lD == 0)
                    Gamma[0][0] = 1;
                else
                    Gamma[lC][lD] = (lC * GammaC + lD * GammaD) / (lC + lD);

                lC++;
                if (lC + lD > V)
                {
                    lD++;
                    lC = 0;
                }
            }
            return Gamma;
        }
        #endregion Agregacja
    }
    /// <summary>
    /// Rozkład dla algorytmu Sigma Yt. 
    /// Zostanie os usunięty z kodu, gdy zostanie wykazane, że algorytm ten jest szczególnym przypadkiem algorytmu Gamma Y3
    /// </summary>
    public class rSigmaYt : Rozklad
    {
        private Rozklad niezalezny;                 //Rozkład dla systemu niezależnego od stanu.

        #region konstruktory
        public rSigmaYt(Wiazka rozWiazka, trClass klZgloszen)
            : base(rozWiazka, klZgloszen)
        {
            niezalezny = new Rozklad(rozWiazka, klZgloszen);
        }
        public rSigmaYt(Rozklad kopiowany)
            : base(kopiowany)
        {
            niezalezny = new Rozklad(kopiowany);
        }
        #endregion konstruktory


        public void Agreguj(rSigmaYt agregowany, int V)
        {
            double[][] prDopKomb = rSigmaYt.wyznGamma(this, agregowany, wiazka.sigmy, V);
            niezalezny.Agreguj(agregowany.niezalezny);
            double[] stany = new double[V + 1];
            double suma = 0;
            for (int n = 0; n <= V; n++)
            {
                for (int lC = 0; lC <= n; lC++)
                {
                    double tmp = this[lC] * agregowany[n - lC] * prDopKomb[lC][n - lC];
                    suma += tmp;
                    stany[n] += tmp;
                }
            }
            foreach (trClass klasaR in agregowany.zagregowaneKlasy)
                zagregowaneKlasy.Add(klasaR);
            if (this.V != V)
                zmienDlugosc(V, false);
            for (int n = 0; n <= V; n++)
                this[n] = stany[n] / suma;
        }



        /// <summary>
        /// Określa prawdopodobieństwa dopuszczenia kombinacji.
        /// Każdy element tablicy [lC][lD] odpowiada prawdopodobieńśtwu dopuszczenia kombinacji (lC, lD).
        /// </summary>
        /// <param name="rC">Zagregowany rozkład zajętości klas ze zbioru C</param>
        /// <param name="rD">Zagregowany rozkład zajętości klas ze zbioru D</param>
        /// <param name="sigmy">Tablica z prawdopodobieńśtwami warunkowych przejść dla wszystkich klas</param>
        /// <param name="V">Pojemność systemu</param>
        /// <returns>Dwuwymiarowa tablica z prawdopodobieńśtwami dopuszczenia kombinacji</returns>
        public static double[][] wyznGamma(rSigmaYt rC, rSigmaYt rD, sigmaPrzyjmZgl sigmy, int V)
        {
            double[][] Gamma = new double[V + 1][];
            for (int l = 0; l <= V; l++)
                Gamma[l] = new double[V - l + 1];

            int lC = 0;
            int lD = 0;
            while (lD <= V)
            {
                double GammaClicznik = 0;
                double GammaDlicznik = 0;

                double GammaCmianownik = 0;
                double GammaDmianownik = 0;

                foreach (trClass klasa in rC.zagregowaneKlasy)
                {
                    if (rC[lC - klasa.t] == 0)
                        continue;

                    GammaClicznik += klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC - klasa.t][lD] * rC[lC - klasa.t];
                    GammaCmianownik += klasa.at * sigmy[klasa, lC - klasa.t] * rC[lC - klasa.t];
                }
                foreach (trClass klasa in rD.zagregowaneKlasy)
                {
                    if (rD[lD - klasa.t] == 0)
                        continue;

                    GammaDlicznik += klasa.at * sigmy[klasa, lC + lD - klasa.t] * Gamma[lC][lD - klasa.t] * rD[lD - klasa.t];
                    GammaDmianownik += klasa.at * sigmy[klasa, lD - klasa.t] * rD[lD - klasa.t];
                }

                double GammaC = (GammaCmianownik == 0) ? 0 : GammaClicznik / GammaCmianownik;
                double GammaD = (GammaDmianownik == 0) ? 0 : GammaDlicznik / GammaDmianownik;

                if (lC + lD == 0)
                    Gamma[0][0] = 1;
                else
                    Gamma[lC][lD] = (lC * GammaC + lD * GammaD) / (lC + lD);

                lC++;
                if (lC + lD > V)
                {
                    lD++;
                    lC = 0;
                }
            }
            return Gamma;
        }
        //#endregion Agregacja

        /// <summary>
        /// Zwraca wartość współczynnika transformacji.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public double delta(int n)
        {
            if (n > V)
                throw new Exception("Przekroczenie zakresu");
            if (niezalezny[n] == 0)
            {
                if (this[n] == 0)
                    return 1;
                else
                    throw new Exception("Błąd wyznaczania współczynnika transformacji");
            }
            return this[n] / niezalezny[n];
        }
    }

    public class rSigma : Rozklad
    {
        public rSigma() : base() { }
        public rSigma(Wiazka rozWiazka, trClass klZgloszen) : base(rozWiazka, klZgloszen) { }
        public rSigma(Rozklad kopiowany) : base(kopiowany) { }
        public override void Agreguj(Rozklad rB)
        {
            _v = (_v > rB.V) ? _v : rB.V;
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= this.wiazka.q + 1; n++)
            {
                noweStany[n] = 0;
                for (int l = 0; l <= n; l++)
                    noweStany[n] += this[n - l] * rB[l];
            }
            for (int n = wiazka.q + 2; n <= _v; n++)
            {
                noweStany[n] = 0;
                for (int l = 0; l <= n; l++)
                {
                    double wspSigma = ((n - l) * OblGamma(this, n, (n - l)) + l * OblGamma(rB, n, l)) / (double)n;
                    noweStany[n] += wspSigma * this[n - l] * rB[l];
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];

            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
        public override void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy)
        {
            _v = (_v > rB.V) ? _v : rB.V;
            double[] noweStany = new double[_v + 1];


            noweStany[0] = this[0] * rB[0];
            for (int n = 1; n <= _v; n++)
            {
                noweStany[n] = 0;
                for (int l = 0; l <= n; l++)
                {
                    double sigmaA = OblGamma(this, n, (n - l), sigmy);
                    double sigmaB = OblGamma(rB, n, l, sigmy);
                    double wspSigma = ((n - l) * sigmaA + l * sigmaB) / (double)n;
                    noweStany[n] += wspSigma * this[n - l] * rB[l];
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];

            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }

        /// <summary>
        /// Wyznacza prawdopodobieństwo dopuszczenia warunkowej kombinacji (l_C, l_D | C)
        /// dla wiązki pełnodostępnej z rezerwacją
        /// </summary>
        /// <param name="zrodlo">Rozkład zajętości klas ze zbioru C </param>
        /// <param name="nStan">l_C + l_D</param>
        /// <param name="lZajPJP">l_C</param>
        /// <returns>prawdopodobieńśtwo dopuszczenia warunkowej kombinacji</returns>
        public virtual double OblGamma(Rozklad zrodlo, int nStan, int lZajPJP)
        {
            int x = nStan - zrodlo.wiazka.q;
            if (x > zrodlo.tMax)
                return 0;
            return 1;
        }
        public virtual double OblGamma(Rozklad zrodlo, int nStan, int lZajPJP, sigmaPrzyjmZgl sigmy)
        {
            return 1;
        }
        /// <summary>
        /// Zamienia rozkład dla systemu z procesem przyjmowania zgłoszeń niezależnym od stanu
        /// na rozkład dla systemu z procesem przyjmowania zgłoszeń zależnym od stanu
        /// </summary>
        /// <param name="sigmy">sigmy opisujące zależność procesu przyjmowania zgłoszeń od stanu</param>
        /// <param name="nrKlasy">numer klasy</param>
        public void zamienNaZaleznyOdStanu(sigmaPrzyjmZgl sigmy, int nrKlasy)
        {
            int t = wiazka.ListaKlasRuchu[nrKlasy].t;
            for (int n = t; n <= this._v; n++)
            {
                double tmp = 1;
                for (int l = n - t; l >= 0; l -= t)
                {
                    if (sigmy[nrKlasy, l] == 1)
                        break;
                    tmp *= sigmy[nrKlasy, l];
                }
                this[n] *= tmp;
            }
            this.normalizacja();
        }
    }
    public class rSigma01 : rSigma
    {
        public rSigma01(Wiazka rozWiazka, trClass klZgloszen)
            : base(rozWiazka, klZgloszen)
        {

        }
        public rSigma01(Rozklad kopiowany) : base(kopiowany) { }
        public static rSigma01 operator *(rSigma01 r1, Rozklad r2)
        {
            rSigma01 wynik = new rSigma01(r1.wiazka, null);
            int nowaDlugosc = (r1.V + r2.V > r1.wiazka.V) ? r1.wiazka.V : r1.V + r2.V;
            wynik.Splataj(r1, r2, nowaDlugosc);
            return wynik;
        }
    }
    public class rSigmaLambdaT : rSigma
    {
        public rSigmaLambdaT(Wiazka rozWiazka, trClass klZgloszen) : base(rozWiazka, klZgloszen) { }
        public rSigmaLambdaT(Rozklad kopiowany) : base(kopiowany) { }

        public override double OblGamma(Rozklad zrodlo, int nStan, int lZajPJP)
        {
            double licznik = 0;
            double mianownik = 0;

            int x = nStan - zrodlo.wiazka.q;
            foreach (trClass kRuchu in zrodlo.zagregowaneKlasy)
            {
                double UdzKlasy = kRuchu.PodajIntZgl(0) * kRuchu.t;
                //                double UdzKlasy = kRuchu.PodajIntZgl(lZajPJP) * kRuchu.t;
                if ((lZajPJP - kRuchu.t <= zrodlo.wiazka.q) && (lZajPJP >= kRuchu.t))
                {
                    mianownik += UdzKlasy;
                    if (kRuchu.t >= x)
                        licznik += UdzKlasy;
                }
            }
            if (mianownik == 0)
                return 0;
            return licznik / mianownik;
        }
        public override double OblGamma(Rozklad zrodlo, int nStan, int lZajPJP, sigmaPrzyjmZgl sigmy)
        {
            double licznik = 0;
            double mianownik = 0;

            int x = nStan - zrodlo.wiazka.q;
            foreach (trClass kRuchu in zrodlo.zagregowaneKlasy)
            {
                int globNrKlasy = zrodlo.wiazka.ListaKlasRuchu.IndexOf(kRuchu);
                double UdzKlasy = kRuchu.PodajIntZgl(lZajPJP) * kRuchu.t;

                if (lZajPJP >= kRuchu.t)
                    licznik += (UdzKlasy * sigmy[globNrKlasy, lZajPJP - kRuchu.t] * sigmy[globNrKlasy, nStan - kRuchu.t]);
                if (lZajPJP >= kRuchu.t)
                    mianownik += (UdzKlasy * sigmy[globNrKlasy, lZajPJP - kRuchu.t]);
            }
            if (mianownik == 0)
                return 0;
            return licznik / mianownik;
        }
    }
}
