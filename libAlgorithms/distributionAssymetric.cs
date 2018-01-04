using System;
using System.Collections.Generic;
using System.Text;
using Algorithms;
using ModelGroup;

namespace Distributions
{
    public class rAsymetryczny : Rozklad
    {
        public rAsymetryczny(Rozklad kopiowany)
            : base(kopiowany)
        {

        }
    }
    public class rAsMin : rAsymetryczny
    {
        public rAsMin(Rozklad kopiowany)
            : base(kopiowany)
        {

        }
        public void AgregujR(Rozklad rB)
        {
            double[] noweStany = new double[_v + 1];
            for (int lA = 0; lA <= _v; lA++)
            {
                if (lA > wiazka.q + tMin)
                    break;
                for (int lB = 0; lA + lB <= _v; lB++)
                {
                    if (lA + lB > wiazka.q + rB.tMin)
                        break;
                    noweStany[lA + lB] += (stany[lA] * rB[lB]);
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
        public override void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy)
        {
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                double minSigma = 1;
                for (int i = 0; i < rB.zagregowaneKlasy.Count; i++)
                {
                    int nrKlasy = wiazka.ListaKlasRuchu.IndexOf(rB.zagregowaneKlasy[i]);
                    trClass klasa =wiazka.ListaKlasRuchu[nrKlasy];
                    if (n >= klasa.t)
                    {
                        if (sigmy[nrKlasy, n - klasa.t] < minSigma)
                            minSigma = sigmy[nrKlasy, n - klasa.t];
                    }
                }
                noweStany[n] = 0;
                {
                    for (int l = 0; l <= n; l++)
                    {
                        noweStany[n] += (stany[n-l] * rB[l]);
                    }
                }
                noweStany[n] *= minSigma;
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
    }
    public class rAsMax : rAsymetryczny
    {
        public rAsMax(Rozklad kopiowany)
            : base(kopiowany)
        {
        }
        public void AgregujR(Rozklad rB)
        {
            double[] noweStany = new double[_v + 1];
            for (int lA = 0; lA <= _v; lA++)
            {
                if (lA > wiazka.q + tMax)
                    break;
                for (int lB = 0; lA + lB <= _v; lB++)
                {
                    if (lA + lB > wiazka.q + rB.tMax)
                        break;
                    noweStany[lA + lB] += (stany[lA] * rB[lB]);
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
        public override void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy)
        {
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                double maxSigma = 0;
                for (int i = 0; i < rB.zagregowaneKlasy.Count; i++)
                {
                    int nrKlasy = wiazka.ListaKlasRuchu.IndexOf(rB.zagregowaneKlasy[i]);
                    trClass klasa = wiazka.ListaKlasRuchu[nrKlasy];
                    if (n >= klasa.t)
                    {
                        if (sigmy[nrKlasy, n - klasa.t] > maxSigma)
                            maxSigma = sigmy[nrKlasy, n - klasa.t];
                    }
                    else
                        maxSigma = 1;
                }
                noweStany[n] = 0;
                {
                    for (int l = 0; l <= n; l++)
                    {
                        noweStany[n] += (stany[n - l] * rB[l]);
                    }
                }
                noweStany[n] *= maxSigma;
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
    }

    public class rAsSa3 : rAsymetryczny
    {
        public rAsSa3(Rozklad kopiowany)
            : base(kopiowany)
        {
        }
        public void AgregujR(Rozklad rB)
        {
            double[] noweStany = new double[_v + 1];
            for (int lA = 0; lA <= _v; lA++)
            {
                if (lA > wiazka.q + tMax)
                    break;
                for (int lB = 0; lA + lB <= _v; lB++)
                {
                    if (lA + lB > wiazka.q + rB.tMax)
                        break;
                    noweStany[lA + lB] += (stany[lA] * rB[lB]);
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
        public override void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy)
        {
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                double maxSigma = 0;
                for (int i = 0; i < rB.zagregowaneKlasy.Count; i++)
                {
                    int nrKlasy = wiazka.ListaKlasRuchu.IndexOf(rB.zagregowaneKlasy[i]);
                    trClass klasa = wiazka.ListaKlasRuchu[nrKlasy];
                    if (n >= klasa.t)
                    {
                        if (sigmy[nrKlasy, n - klasa.t] > maxSigma)
                            maxSigma = sigmy[nrKlasy, n - klasa.t];
                    }
                }
                noweStany[n] = 0;
                {
                    for (int l = 0; l <= n; l++)
                    {
                        noweStany[n] += (stany[n - l] * rB[l]);
                    }
                }
                noweStany[n] *= maxSigma;
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
        public void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy, int nrKlasy)
        {
            int t = wiazka.ListaKlasRuchu[nrKlasy].t;
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                noweStany[n] = 0;
                {
                    for (int l = 0; l <= n; l++)
                    {
                        double tmp = (stany[l] * rB[n - l] * sigmy[nrKlasy, n - t]);
                        noweStany[n] += tmp;
                    }
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
    }

    public class rAsYT : rAsymetryczny
    {
        public Rozklad pomocniczy;
        public rAsYT(Rozklad kopiowany)
            : base(kopiowany)
        {
            pomocniczy = new Rozklad(kopiowany);
        }

        public double OblGamma(int lC, int lD, List<trClass> zbiorD, sigmaPrzyjmZgl sigmy, Rozklad PD)
        {
            double licznik = 0;
            double mianownik = 0;
            int n = lC + lD;
            foreach (trClass klasa in zbiorD)
            {
                if (lD >= klasa.t)
                {
                    double p_lZajPJP = PD[lD];
                    if (p_lZajPJP == 0)
                        continue;
                    double p_pop_lZajPJP = PD[lD - klasa.t];
                    double lambda = klasa.PodajIntZgl(0);
                    double yPrim = p_pop_lZajPJP * lambda / (p_lZajPJP * klasa.mu);
                    double y = PD[lD - klasa.t] * klasa.PodajIntZgl(yPrim) /
                        (PD[lD] * klasa.mu);
                    double UdzKlasy = y * klasa.t;

                    licznik += (sigmy[klasa, n-klasa.t]  * UdzKlasy);
                    mianownik += (sigmy[klasa, lD - klasa.t] * UdzKlasy);
                }
            }

            if (mianownik == 0)
                return 0;
            return licznik / mianownik;
        }

        public void Agreguj(Rozklad rB, Rozklad rPB, sigmaPrzyjmZgl sigmy)
        {
            pomocniczy.Agreguj(rB);
            double[] noweStany = new double[_v + 1];
            for (int lA = 0; lA <= _v; lA++)
            {
                if (lA > wiazka.q + tMax)
                    break;
                for (int lB = 0; lA + lB <= _v; lB++)
                {
                    double gamma = OblGamma(lA, lB, rB.zagregowaneKlasy, sigmy, rPB);
                    noweStany[lA + lB] += (stany[lA] * rB[lB] * gamma);
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
    }


    public class rAsLambdaT : rAsymetryczny
    {
        public rAsLambdaT(Rozklad kopiowany)
            : base(kopiowany)
        {
        }

        public double OblGamma(int lC, int lD, List<trClass> zbiorD, sigmaPrzyjmZgl sigmy)
        {
            double licznik = 0;
            double mianownik = 0;
            int n = lC + lD;
            foreach (trClass klasa in zbiorD)
            {
                double UdzKlasy = klasa.PodajIntZgl(lD) * klasa.t;
                if (lD >= klasa.t)
                {
                    licznik += (UdzKlasy * sigmy[klasa, lD - klasa.t] * sigmy[klasa, n - klasa.t]);
                    mianownik += (UdzKlasy * sigmy[klasa, lD - klasa.t]);
                }
            }

            if (mianownik == 0)
                return 0;
            return licznik / mianownik;
        }

        public override void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy)
        {
            double[] noweStany = new double[_v + 1];
            for (int lA = 0; lA <= _v; lA++)
            {
                if (lA > wiazka.q + tMax)
                    break;
                for (int lB = 0; lA + lB <= _v; lB++)
                {
                    noweStany[lA + lB] += (stany[lA] * rB[lB] * OblGamma(lA, lB, rB.zagregowaneKlasy, sigmy));
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
    }
    /// <summary>
    /// Uogolniony splot asymetryczny
    /// </summary>
    public class rAsUogolniony : rAsymetryczny
    {
        public rAsUogolniony(Rozklad kopiowany)
            : base(kopiowany)
        {
        }
        public void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy, int nrKlasy)
        {
            int t = wiazka.ListaKlasRuchu[nrKlasy].t;
            int tMaxMinusI = this.tMax;
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                noweStany[n] = 0;
                {
                    for (int l = 0; l <= n; l++)
                    {
                        double gamma = sigmy[nrKlasy, n - t] * sigmy[MaxInd, l - tMaxMinusI];
                        double tmp = gamma * stany[l] * rB[n - l];
                        noweStany[n] += tmp;
                    }
                }
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
        }
    }
    }