using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;
using Algorithms.convolution;
using Algorithms.reccurence;

namespace algorithms.old
{
    #region eksperymentalne
    public class aSplWogrDostLosTest : aSplotowyUv2
    {
        public aSplWogrDostLosTest(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Spl los Test";
            SkrNazwaAlg = "Los_test";
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny { get { return false; } }

        protected override void Krok3(int nrBad)
        {
            Rozklad[] Pj = new Rozklad[aWiazka.sumaK];
            int stan = 0;
            for (int j = 0; j < aWiazka.sumaK; j++)
            {
                int f_j = aWiazka.PojPodgr(j + 1);
                Pj[j] = new Rozklad(aWiazka, f_j);

                Pj[j][0] = 0;
                for (int l = 0; l <= stan; l++)
                    Pj[j][0] += P[l];

                for (int l = 1; l <= f_j; l++)
                {
                    Pj[j][l] = P[stan + l];
                }
                stan += f_j;
                Pj[j].normalizacja();
            }
            Rozklad P2 = new Rozklad(Pj[0]);
            for (int j = 1; j < aWiazka.sumaK; j++)
                P2.Agreguj(Pj[j]);

            //            int f = aWiazka.pojOdPodgrupy(aWiazka.sumaK);
            //            P2.zmienDlugosc(f, true);
            P2.normalizacja(aWiazka.V);
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                double E = 0;
                int poczatek = aWiazka.V - aWiazka.sumaK * (t - 1);
                for (int n = poczatek; n <= aWiazka.V; n++)
                    E += P2[n];
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], E);
            }
        }
    }
    public class aSplWogrDostLosTest2 : aSplotowyUv2
    {
        public aSplWogrDostLosTest2(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Spl los Test2";
            SkrNazwaAlg = "Los_test2";
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny { get { return false; } }

        protected override void Krok3(int nrBad)
        {
            Rozklad[] Pj = new Rozklad[2];

            int fK = aWiazka.pojOdPodgrupy(aWiazka.sumaK);
            int stan = aWiazka.V - fK;

            Pj[0] = new Rozklad(aWiazka, stan);
            Pj[1] = new Rozklad(aWiazka, fK);

            double suma = 0;
            for (int n = 0; n <= stan; n++)
            {
                Pj[0][n] = P[n];
                suma += P[n];
            }
            Pj[1][0] = suma;

            for (int l = 1; l <= fK; l++)
                Pj[1][l] += P[l + stan];

            Rozklad P2 = new Rozklad(Pj[0]);
            P2.Agreguj(Pj[1]);
            P2.normalizacja();

            //            int f = aWiazka.pojOdPodgrupy(aWiazka.sumaK);
            //            P2.zmienDlugosc(f, true);
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                double E = 0;
                for (int n = P2.V - t + 1; n <= P2.V; n++)
                    E += P2[n];
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], E);
            }
        }
    }

    public class aSplWogrDostLos1 : aSplotowyUv2
    {
        public aSplWogrDostLos1(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Spl los 1";
            SkrNazwaAlg = "Spl_los1";
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny { get { return false; } }

        /// <summary>
        /// Wyznacza rozkład niewykorzystanych PJP w podgrupie
        /// </summary>
        /// <param name="maxT">liczba PJP żądanych przez zgłoszenie najstarszej klasy</param>
        /// <returns>Rozkład prawdopodobieństw wolnych niewykorzystanych PJP w podgrupie</returns>
        protected virtual Rozklad OkrRozklNiewPJPpojPodgr(int maxT)
        {
            Rozklad wynik = new Rozklad(aWiazka, maxT);// (aWiazka, aWiazka.ListaKlasRuchu[nrKlasy], stany, t - 1);
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                for (int i = 0; i < klasa.t; i++)
                {
                    if (klasa.t <= maxT)
                    {
                        //wynik[i] += klasa.atProp;
                        wynik[i] += klasa.aProp;
                    }
                }
            }
            wynik.normalizacja();
            return wynik;
        }

        protected override void Krok3(int nrBad)
        {
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                Rozklad z = OkrRozklNiewPJPpojPodgr(aWiazka.tMax);
                //                Rozklad z = OkrRozklNiewPJPpojPodgr(t);
                Rozklad P2 = new Rozklad(P);

                for (int j = 1; j < aWiazka.sumaK; j++)
                {
                    //                    P2.zmienDlugosc(P2.V + aWiazka.tMax -1, false);
                    P2.Agreguj(z);
                }
                P2.zmienDlugosc(aWiazka.V + (aWiazka.sumaK - 1) * (aWiazka.tMax - t), true);
                double E = 0;
                for (int n = aWiazka.V - t + 1; n <= aWiazka.V; n++)
                    E += P2[n];
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], E);
            }
        }
    }
    public class aSplWogrDostLos2 : aSplotowyUv2
    {
        public aSplWogrDostLos2(Wiazka wAlg)
            : base(wAlg)
        {
            normalizowac = false;
            NazwaAlg = "Spl los 2";
            SkrNazwaAlg = "Spl_los2";
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny { get { return false; } }

        /// <summary>
        /// Wyznacza rozkład niewykorzystanych PJP w podgrupie
        /// </summary>
        /// <param name="maxT">liczba PJP żądanych przez zgłoszenie najstarszej klasy</param>
        /// <returns>Rozkład prawdopodobieństw wolnych niewykorzystanych PJP w podgrupie</returns>
        protected virtual Rozklad OkrRozklNiewPJPpojPodgr(int maxT)
        {
            Rozklad wynik = new Rozklad(aWiazka, maxT);// (aWiazka, aWiazka.ListaKlasRuchu[nrKlasy], stany, t - 1);
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                for (int i = 0; i < klasa.t; i++)
                {
                    if (klasa.t <= maxT)
                    {
                        //wynik[i] += klasa.atProp;
                        wynik[i] += klasa.aProp;
                    }
                }
            }
            wynik.normalizacja();
            return wynik;
        }

        protected virtual Rozklad OkrRozklBonusPJPpojPodgr(int maxT)
        {
            Rozklad wynik = new Rozklad(aWiazka, maxT);// (aWiazka, aWiazka.ListaKlasRuchu[nrKlasy], stany, t - 1);
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                for (int i = 1; i <= klasa.t - maxT; i++)
                {
                    if (klasa.t > maxT)
                    {
                        //wynik[i] += klasa.atProp;
                        wynik[i] += klasa.aProp;
                    }
                }
            }
            wynik.normalizacja();
            return wynik;
        }

        protected override void Krok3(int nrBad)
        {
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                Rozklad z = OkrRozklNiewPJPpojPodgr(t);
                //                Rozklad z = OkrRozklNiewPJPpojPodgr(t);
                Rozklad P2 = new Rozklad(P);

                for (int j = 1; j < aWiazka.sumaK; j++)
                {
                    P2.zmienDlugosc(P2.V + aWiazka.tMax - 1, false);
                    P2.Agreguj(z);
                }
                P2.zmienDlugosc(aWiazka.V + (aWiazka.sumaK - 1) * (aWiazka.tMax - t), true);
                double E = 0;
                for (int n = P2.V - t + 1; n <= P2.V; n++)
                    E += P2[n];
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], E);
            }
        }
    }

    public class aSplSigmaSpl : aSplotowyUv2
    {
        public aSplSigmaSpl(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Iversen Sigma Spl";
            SkrNazwaAlg = "SigmaSpl";
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny
        {
            get
            {
                return false;
            }
        }
        protected virtual Rozklad OkrRozklNiewPJPpojPodgr(int nrKlasy)
        {
            int t = aWiazka.ListaKlasRuchu[nrKlasy].t;
            double[] stany = new double[t];

            foreach (trClass kl in aWiazka.ListaKlasRuchu)
                for (int n = 0; n < kl.t; n++)
                    stany[n % t] += (kl.aProp);
            for (int n = 0; n < t; n++)
                stany[n] /= aWiazka.sumaPropAT;

            Rozklad wynik = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[nrKlasy], stany, t - 1);
            return wynik;
        }
        protected virtual Rozklad OkrRozklNiewPJPwszystkichPodgr(Rozklad pojRozkl)
        {
            Rozklad wynik = new Rozklad(pojRozkl);
            for (int k = 1; k < aWiazka.sumaK - 1; k++)
                wynik = wynik * pojRozkl;
            return wynik;
        }
    }
    public class aSplSigmaSpl2 : aSplSigmaSpl
    {
        public aSplSigmaSpl2(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Iversen Sigma Spl2";
            SkrNazwaAlg = "SigmaSpl2";
        }
        public override bool eksperymentalny
        {
            get
            {
                return false;
            }
        }
        protected override Rozklad OkrRozklNiewPJPpojPodgr(int nrKlasy)
        {
            int t = aWiazka.ListaKlasRuchu[nrKlasy].t;
            double[] stany = new double[t];

            for (int n = 0; n < t; n++)
                stany[n] = 1;
            Rozklad wynik = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[nrKlasy], stany, t - 1);
            wynik.normalizacja();
            return wynik;
        }
        protected override Rozklad OkrRozklNiewPJPwszystkichPodgr(Rozklad pojRozkl)
        {
            Rozklad wynik = new Rozklad(pojRozkl);
            for (int k = 1; k < aWiazka.sumaK - 1; k++)
                wynik = wynik * pojRozkl;
            return wynik;
        }

        protected override void Krok3(int nrBad)
        {
            double[,] sigmy = new double[aWiazka.m, aWiazka.V + 1];
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                Rozklad warunki = OkrRozklNiewPJPwszystkichPodgr(OkrRozklNiewPJPpojPodgr(i));

                sigmy[i, aWiazka.V] = warunki[0];
                for (int n = aWiazka.V - 1; n >= 0; n--)
                {
                    sigmy[i, n] = sigmy[i, n + 1] + warunki[aWiazka.V - n];
                }
            }
            double[] sigmaZb = new double[aWiazka.V + 1];
            for (int n = 0; n <= aWiazka.V; n++)
            {
                sigmaZb[n] = 0;
                for (int i = 0; i < aWiazka.m; i++)
                    sigmaZb[n] += (aWiazka.ListaKlasRuchu[i].atProp * sigmy[i, n]);
                sigmaZb[n] /= aWiazka.sumaPropAT;
                P[n] = P[n] * sigmaZb[n];
            }
            P.normalizacja();
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                double E = 0;
                for (int n = 0; n <= aWiazka.V - t; n++)
                    E += P[n] * (1 - sigmy[i, n + t]);
                for (int n = aWiazka.V - t + 1; n <= aWiazka.V; n++)
                    E += P[n];
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], E);
            }
        }
    }
    public class aSplSigmaSpl3 : aSplSigmaSpl
    {
        public aSplSigmaSpl3(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Iversen Sigma Spl3";
            SkrNazwaAlg = "SigmaSpl3";
        }
        public override bool eksperymentalny
        {
            get
            {
                return true;
            }
        }
        protected override Rozklad OkrRozklNiewPJPpojPodgr(int nrKlasy)
        {
            int t = aWiazka.ListaKlasRuchu[nrKlasy].t;
            double[] stany = new double[t];

            for (int n = 0; n < t; n++)
                stany[n] = 1;
            Rozklad wynik = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[nrKlasy], stany, t - 1);
            wynik.normalizacja();
            return wynik;
        }
        protected override Rozklad OkrRozklNiewPJPwszystkichPodgr(Rozklad pojRozkl)
        {
            Rozklad wynik = new Rozklad(pojRozkl);
            for (int k = 1; k < aWiazka.sumaK - 1; k++)
                wynik = wynik * pojRozkl;
            return wynik;
        }

        protected override void Krok3(int nrBad)
        {
            double[,] sigmy = new double[aWiazka.m, aWiazka.V + 1];
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                Rozklad warunki = OkrRozklNiewPJPwszystkichPodgr(OkrRozklNiewPJPpojPodgr(i));

                for (int n = 0; n <= aWiazka.V; n++)
                {
                    sigmy[i, n] = 0;
                    for (int l = 0; l <= aWiazka.V - n - t; l++)
                        sigmy[i, n] += warunki[l];
                }
            }
            double[] sigmaZb = new double[aWiazka.V + 1];
            sigmaZb[0] = 1;
            for (int n = 1; n <= aWiazka.V; n++)
            {
                double licznik = 0;
                double mianownik = 0;
                for (int i = 0; i < aWiazka.m; i++)
                {
                    int t = aWiazka.ListaKlasRuchu[i].t;
                    trClass klasa = aWiazka.ListaKlasRuchu[i];
                    if (n - t >= 0)
                    {
                        licznik += sigmy[i, n - t] * klasa.at * sigmaZb[n - t] * P[n - t];
                        mianownik += klasa.at * P[n - t];
                    }
                }
                if (mianownik == 0)
                    sigmaZb[n] = 1;
                else
                    sigmaZb[n] = licznik / mianownik;
            }
            for (int n = 0; n <= aWiazka.V; n++)
                P[n] = P[n] * sigmaZb[n];

            P.normalizacja();
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                double E = 0;
                for (int n = 0; n <= aWiazka.V - t; n++)
                    E += P[n] * (1 - sigmy[i, n]);
                for (int n = aWiazka.V - t + 1; n <= aWiazka.V; n++)
                    E += P[n];
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], E);
            }
        }
    }
    #endregion

    public class aRobertsY : aKaufmanRoberts
    {
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK != 1)                             //nie jest to wiązka pełnodostęna
                    return false;
                //                if (aWiazka.AlgorytmRezerwacji == AlgRez.brak)      //w wiązce jest nie ma mechanizm rezerwacji
                //                    return false;                                   //wystarczy algorytm Kaufmana-Robertsa
                return true;
            }
        }
        public aRobertsY(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Roberts SISM";
            SkrNazwaAlg = "RobSISM";
        }
        protected override double pobSigma(int stan, int nrKlasy)
        {
            if (stan <= aWiazka.q)
                return 1;
            return 0;
        }
        protected override void okrE(int nrBad)
        {
            foreach (trClass pojKlasa in aWiazka.ListaKlasRuchu)
            {
                double E = 0;
                int stStan = (aWiazka.q < aWiazka.V - pojKlasa.t) ? aWiazka.q : aWiazka.V - pojKlasa.t;
                for (int n = stStan + 1; n <= aWiazka.V; n++)
                    E += stany[n];
                wynikiAlg.UstawE(nrBad, pojKlasa, E);
            }
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            okrRozklad();
            okrE(nrBad);
        }
        protected override void okrRozklad()
        {
            base.okrRozklad();
            double[] stanyY = new double[aWiazka.V + 1];
            stanyY[0] = 1;
            double suma = 1;

            for (int n = 1; n <= aWiazka.V; n++)
            {
                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass zKlasa = aWiazka.ListaKlasRuchu[i];
                    if (n - zKlasa.t >= 0)
                    {
                        double y = 0;
                        if ((n - 2 * zKlasa.t >= 0) && (stany[n - zKlasa.t] != 0))
                        {
                            y = stany[n - 2 * zKlasa.t] * zKlasa.PodajIntZgl(0)
                                / (stany[n - zKlasa.t] * zKlasa.mu);
                        }
                        stanyY[n] += (zKlasa.t * zKlasa.PodajIntZgl(y) / zKlasa.mu
                            * stanyY[n - zKlasa.t] * pobSigma(n - zKlasa.t, i));
                    }
                }
                stanyY[n] /= n;
                suma += stanyY[n];
            }

            for (int n = 0; n <= aWiazka.V; n++)
                stany[n] = stanyY[n] / suma;
        }
    }

    public class aRobertsRezerwacja : aKaufmanRoberts
    {
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK != 1)                             //nie jest to wiązka pełnodostęna
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)      //w wiązce jest nie ma mechanizm rezerwacji
                    return false;                                   //wystarczy algorytm Kaufmana-Robertsa
                return true;
            }
        }
        public aRobertsRezerwacja(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Roberts Org";
            SkrNazwaAlg = "RobOrg";
        }
        protected override double pobSigma(int stan, int nrKlasy)
        {
            if (stan <= aWiazka.q)
                return 1;
            return 0;
        }
        protected override void okrE(int nrBad)
        {
            foreach (trClass pojKlasa in aWiazka.ListaKlasRuchu)
            {
                double E = 0;
                int stStan = (aWiazka.q < aWiazka.V - pojKlasa.t) ? aWiazka.q : aWiazka.V - pojKlasa.t;
                for (int n = stStan + 1; n <= aWiazka.V; n++)
                    E += stany[n];
                wynikiAlg.UstawE(nrBad, pojKlasa, E);
            }
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            okrRozklad();
            okrE(nrBad);
        }
    }
    public abstract class aPrzel : aSplotowyUv1
    {
        public aPrzel(Wiazka wAlg)
            : base(wAlg) { }
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny
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
            p[0].normalizacja();

            Rozklad []Ppodgr = new Rozklad[aWiazka.sumaK];

            for (int nrPodgr = 1; nrPodgr <= aWiazka.sumaK; nrPodgr++)
            {
                Krok2(aWiazka.pojOdPodgrupy(nrPodgr), true);
                Ppodgr[nrPodgr - 1] = P_minusI[0] * p[0];
                Ppodgr[nrPodgr - 1].zmienDlugosc(aWiazka.PojPodgr(nrPodgr), true);
                
                if (nrPodgr != aWiazka.sumaK)        //przy ostatniej iteracji jest to zbędne
                    Krok2b(aWiazka.PojPodgr(nrPodgr));
            }
            foreach (trClass i in aWiazka.ListaKlasRuchu)
            {
                double B_i = 0;
                int f_j = aWiazka.PojPodgr(aWiazka.sumaK);
                for (int n = f_j - i.t + 1; n <= f_j; n++)
                {
                    B_i += Ppodgr[aWiazka.sumaK-1][n];
                }
                wynikiAlg.UstawE(nrBad, i, B_i);
            }
//            Krok3(nrBad, aWiazka.pojOdPodgrupy(aWiazka.sumaK));
        }

        /// <summary>
        /// Prawdopodobieńśtwo zajętości n_j PJP w podgrupie j, 
        /// pod warunkiem, że w podgrupach j, j+1, ... zgłoszenia klasy i zajmują l_i PJP,
        /// a zgłoszenia pozostałych kla l PJP
        /// </summary>
        /// <param name="n_j">Liczba zajętych PJP w podgrupie j przez zgłoszenia klasy i</param>
        /// <param name="n">Liczba zajętych PJP w podgrupach j, j+1, ... przez zgłoszenia klasy i</param>
        /// <param name="l">Liczba zajętych PJP w podgrupach j, j+1, ... przez zgłoszenia pozostałych klas</param>
        /// <param name="f_j">Pojemność podgrup j</param>
        /// <param name="t_i">liczba żądanych PJP przez zgłoszenia klasy i</param>
        /// <returns>Prawdopodobieństwo warunkowe</returns>
        public abstract double w(int n_j, int n, int l, int f_j, int t_i);

        public Rozklad OblRozklRuchSpl(Rozklad pojKlasa, Rozklad pozKlasy, int f_j, int pojCalk)
        {
            pozKlasy.normalizacja();
            int t = pojKlasa.tMax;

            //double[,] w = new double[f_i + 1, pojCalk + 1];
            
            double []stany = new double[pojCalk - f_j + 1];

            for (int n = 0; n < stany.Length; n++)
                stany[n] = 0;

            int granica = pojCalk - f_j;
            for (int n = 0; n <= granica; n += t)
            {
                //n_j licaba PJP w podgrupie j zajętych przez zgłoszenia klasy i
                for (int n_j = 0; n_j <= f_j; n_j += t)
                {
                    double temp = 0;
                    int granica2 = pojCalk - n - n_j;
                    granica2 = pojCalk;
                    //granica2 = pojCalk - n_j;
                    for (int l = 0; l <= granica2; l++)
                        temp += w(n_j, n, l, f_j, t) * pozKlasy[l];
                    stany[n] += temp * pojKlasa[n + n_j];
                }
            }

            Rozklad wynik = new Rozklad(pojKlasa.wiazka, pojKlasa.zagregowaneKlasy[0], stany, stany.Length - 1);
            wynik.normalizacja();
            return wynik;
        }
        public void Krok2b(int pojPodgr)
        {
            for (int i = 0; i < aWiazka.m; i++)
                p[i] = OblRozklRuchSpl(p[i], P_minusI[i], pojPodgr, aWiazka.V);
        }
    }
    public class aPrzelUpr : aPrzel
    {
        public aPrzelUpr(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Iversen Przel. UPR";
            SkrNazwaAlg = "p_Upr";
        }

        public override double w(int n_j, int n, int l, int f_j, int t_i)
        {
            if (n_j > f_j)
                return 0;
            if (f_j >= n_j + n + l)
            {
                if (n == 0)
                    return 1;
                else
                    return 0;
            }
            if ((n == 0) && (l == 0))
                return 1;

            double result = (double)(n * (f_j - n_j)) / (double) (n_j);
    
            if ((int)result == l)
                return 1;
            return 0;
        }
    }
    public class aPrzelBernoully : aPrzel
    {
        public aPrzelBernoully(Wiazka wAlg) : base(wAlg)
        {
            dwumian = new dwumianNewtona(30);
            NazwaAlg = "Iversen Przel. Bernoully";
            SkrNazwaAlg = "przel_bern";
        }

        public override double w(int n_j, int n, int l, int f_j, int t_i)
        {
            if (f_j >= n_j+n + l)
            {
                if (n == 0)
                    return 1;
                else
                    return 0;
            }
            int lProb = (int)((f_j)/t_i);
            int lUdanychProb = (int)(n_j / t_i);
            double prProby = (double)(n_j + n) / (double)(n_j + n + l);
//          prProby = Math.Pow(prProby, t_i); // to źle działa

            double wynik = dwumian.Dwumian(lProb, lUdanychProb) * Math.Pow(prProby, lUdanychProb) * Math.Pow((1 - prProby), lProb - lUdanychProb);

            //double suma = 0;
            //int grainca = ((int)(f_j / t_i) + 1) * t_i;
            //for (int x = grainca; x < n_j + n; x += t_i)
            //{
            //    int lUdPr = (int)(x / t_i);
            //    suma += dwumian.Dwumian(lProb, lUdPr) * Math.Pow(prProby, lUdPr) * Math.Pow((1 - prProby), lProb - lUdPr);
            //}
            return wynik;
        }
    }

    public class aPrzelMs : aSplotowyUv1
    {
        double[] wartosciOczekiwane;
        double[] prKlas;
        public aPrzelMs(Wiazka wAlg)
            : base(wAlg)
        {
            dwumian = new dwumianNewtona(30);
            NazwaAlg = "Kolejnosciowy";
            SkrNazwaAlg = "alg. kol.";
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny
        {
            get
            {
                return false;
            }
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            wynikiAlg.UstawA(nrBad, aOf);
            foreach (trClass inicjowana in aWiazka.ListaKlasRuchu)
                inicjowana.ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
            aWiazka.DodPost();

            wartosciOczekiwane = new double[aWiazka.m];
            prKlas = new double[aWiazka.m];

            Krok1();
            for (int nrPodgr = 1; nrPodgr <= aWiazka.sumaK; nrPodgr++)
            {
                int V_j = aWiazka.pojOdPodgrupy(nrPodgr);
                Krok2(V_j, true);

                if (nrPodgr != aWiazka.sumaK)                           // Przy ostatniej iteracji jest to zbędne.
                {
                    Krok2b(aWiazka.PojPodgr(nrPodgr), V_j);
                }
            }
            int V = aWiazka.PojPodgr(aWiazka.sumaK);
            Krok3(nrBad);
        }
        protected override void Krok3(int nrBad)
        {
            Rozklad P = new Rozklad(P_minusI[0]);
            P.Agreguj(p[0]);
            P.normalizacja(aWiazka.V);
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasa);
                double E = 0;
                //int grBlokady = aWiazka.V - klasa.t + 1;

                for (int n = aWiazka.V - klasa.t + 1; n <= aWiazka.V; n++)
                    E += P[n];
                wynikiAlg.UstawE(nrBad, klasa, E);
            }
        }
        /// <summary>
        /// Oblicza prawdopodobieństwo pojawienia się zgłoszenia klasy i względem pozostałych klas
        /// </summary>
        /// <param name="p">rozkład zajętości klasy i oferowany wiązce pierwotnej</param>
        /// <param name="P">rozkład zajętości wszystkich klas (łącznie z klasą i) oferowany wiązce pierwotnej</param>
        /// <param name="V_j">pojemność wiązki pierwotnej</param>
        /// <returns>Prawdopodobieństwo pojawienia się zgłoszenia klasy i</returns>
        private double obliczP(Rozklad p, Rozklad P, int V_j)
        {
            double exp_i = 0;
            double exp = 0;
            for (int i = 1; i <= V_j; i++)
            {
                exp_i += i * p[i];
                exp += i * P[i];
            }
            return exp_i / exp;
        }
        public Rozklad OblRozklRuchSpl(Rozklad pojKlasa, Rozklad P_bez_i, int f_j, int V_j)
        {
            int t = pojKlasa.zagregowaneKlasy[0].t;
            P_bez_i.normalizacja(V_j);
            int lProb = f_j / t;

            double licznik = 0;
            double mianownik = 0;
            double[] Rw_j = new double[lProb + 1];
            for (int l1 = 0; l1 <= f_j; l1++)
            {
                for (int l2 = 0; l1 + l2 <= f_j; l2++)
                {
                    licznik += (l1 * pojKlasa[l1] * P_bez_i[l2]);
                    mianownik += ((l1 + l2) * pojKlasa[l1] * P_bez_i[l2]);
                    Rw_j[(f_j - l1 - l2)/t] += (pojKlasa[l1] * P_bez_i[l2]);
                }
            }
            double sumaRw = 0;
            for (int x = 1; x <= lProb; x++)
            {
                sumaRw += Rw_j[x];
            }
            Rw_j[0] = 1 - sumaRw;

            double Pi = licznik / mianownik;

            //            int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(pojKlasa.zagregowaneKlasy[0]);
            //            double Pi = prKlas[nrKlasy];

            double[] Rn_j = new double[lProb + 1];
            for (int x = 0; x <= lProb; x++)
            {
              //Rn_j[x] = rozkladDwumianowyUjemny(x, lProb ,Pi);
                Rn_j[x] = rozkladDwumianowy(x, lProb, Pi);
            }
            double[] R_j = new double[lProb + 1];

            for (int n = 0; n <= lProb; n++)
            {
                for (int l = 0; l <= lProb; l++)
                {
                    if (l+n > lProb)
                        R_j[lProb] = Rn_j[n] * Rw_j[l];
                    else
                        R_j[l+n] = Rn_j[n] * Rw_j[l];
                }
            }

            double[] stany = new double[V_j - f_j + 1];

            for (int nTot = 0; nTot <= V_j; nTot += t)
            {
                for (int x = 0; x <= lProb; x++)
                {
                    int n = nTot - x * t;
                    if (n < 0)
                        n = 0;
                    if ((n <= V_j - f_j) && (n >= 0))
                        stany[n] += pojKlasa[nTot] * Rn_j[x];
                }
            }
            Rozklad wynik = new Rozklad(pojKlasa.wiazka, pojKlasa.zagregowaneKlasy[0], stany, stany.Length - 1);
            wynik.normalizacja();
            return wynik;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="K">liczba udanych prób</param>
        /// <param name="lProb">Liczba prób</param>
        /// <param name="p">Prawdopodobieństwo sukcesu pojedynczej próby.</param>
        /// <returns></returns>
        private double rozkladDwumianowy(int K, int lProb, double p)
        {
            return dwumian.Dwumian(lProb, K) * Math.Pow(p, K) * Math.Pow((1 - p), lProb - K);
        }
        private double rozkladDwumianowyUjemny(int K, int lProb, double p)
        {
            return dwumian.Dwumian(lProb, K + lProb - 1) * Math.Pow(p, K);
        }
        public void Krok2b(int f_j, int V_j)
        {
            double sumaWO = 0;
            for (int i = 0; i < aWiazka.m; i++)
            {
                wartosciOczekiwane[i] = p[i].wartOczekiwana(V_j);
                sumaWO += wartosciOczekiwane[i];
            }
            for (int i = 0; i < aWiazka.m; i++)
                prKlas[i] = wartosciOczekiwane[i] / sumaWO;

            for (int i = 0; i < aWiazka.m; i++)
                p[i] = OblRozklRuchSpl(p[i], P_minusI[i], f_j, V_j);
        }
    }

    public class aPrzelMs2 : aSplotowyUv2
    {
        double[] wartosciOczekiwane;
        double[] prKlas;
        public aPrzelMs2(Wiazka wAlg)
            : base(wAlg)
        {
            dwumian = new dwumianNewtona(30);
            NazwaAlg = "Kolejnosciowy2";
            SkrNazwaAlg = "alg. kol2.";
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
                if (aWiazka.sumaK <= 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)         //Tylko dla wiązki bez rezerwacji
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny
        {
            get
            {
                return false;
            }
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            wynikiAlg.UstawA(nrBad, aOf);
            foreach (trClass inicjowana in aWiazka.ListaKlasRuchu)
                inicjowana.ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
            aWiazka.DodPost();

            wartosciOczekiwane = new double[aWiazka.m];
            prKlas = new double[aWiazka.m];

            Krok1();
            for (int nrPodgr = 1; nrPodgr <= aWiazka.sumaK; nrPodgr++)
            {
                int V_j = aWiazka.pojOdPodgrupy(nrPodgr);
                Krok2(V_j, true);

                if (nrPodgr != aWiazka.sumaK)                           // Przy ostatniej iteracji jest to zbędne.
                {
                    Krok2b(aWiazka.PojPodgr(nrPodgr), V_j);
                }
            }
            int V = aWiazka.PojPodgr(aWiazka.sumaK);
            Krok3(nrBad);
        }
        protected override void Krok3(int nrBad)
        {
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(klasa);
                double E = 0;
                //int grBlokady = aWiazka.V - klasa.t + 1;

                for (int n = aWiazka.V-klasa.t+1 ; n <= aWiazka.V; n++)
                    E += P[n];
                wynikiAlg.UstawE(nrBad, klasa, E);
            }
        }
        /// <summary>
        /// Oblicza prawdopodobieństwo pojawienia się zgłoszenia klasy i względem pozostałych klas
        /// </summary>
        /// <param name="p">rozkład zajętości klasy i oferowany wiązce pierwotnej</param>
        /// <param name="P">rozkład zajętości wszystkich klas (łącznie z klasą i) oferowany wiązce pierwotnej</param>
        /// <param name="V_j">pojemność wiązki pierwotnej</param>
        /// <returns>Prawdopodobieństwo pojawienia się zgłoszenia klasy i</returns>
        private double obliczP(Rozklad p, Rozklad P, int V_j)
        {
            double exp_i = 0;
            double exp = 0;
            for (int i = 1; i <= V_j; i++)
            {
                exp_i += i * p[i];
                exp += i * P[i];
            }
            return exp_i / exp;
        }      
        public Rozklad OblRozklRuchSpl(Rozklad pojKlasa, Rozklad P, int f_j, int V_j)
        {
            int t = pojKlasa.zagregowaneKlasy[0].t;
            P.normalizacja(V_j);

            int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(pojKlasa.zagregowaneKlasy[0]);
            double Pi = prKlas[nrKlasy];

            int tmp_n_j = (int)(Math.Ceiling(Pi * f_j));

            int lProb = f_j / t;
            double[] Rn_j = new double[lProb + 1];

            for (int x = 0; x <= lProb; x++)
            {
                Rn_j[x] = rozkladDwumianowy(x, lProb, Pi);
            }

            double[] stany = new double[V_j - f_j + 1];
//            for (int nTot = 0; nTot <= V_j; nTot += t)
//            {
//                for (int x = 0; x <= lProb; x++)
//                {
//                    int n = nTot - x * t;
//                    if (n < 0)
//                        n = 0;
//                    if ((n <= V_j - f_j) && (n >= 0))
//                        stany[n] += pojKlasa[nTot] * Rn_j[x];
//                }
//            }
            for (int n = 0; n <= V_j; n++)
            {
                int x = n - tmp_n_j;
                if (x < 0)
                {
                    x = 0;
                }
                if (x < V_j - f_j)
                    stany[x] = pojKlasa[n];
            }
            Rozklad wynik = new Rozklad(pojKlasa.wiazka, pojKlasa.zagregowaneKlasy[0], stany, stany.Length - 1);
            wynik.normalizacja();
            return wynik;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="K">liczba udanych prób</param>
        /// <param name="lProb">Liczba prób</param>
        /// <param name="p">Prawdopodobieństwo sukcesu pojedynczej próby.</param>
        /// <returns></returns>
        private double rozkladDwumianowy(int K, int lProb, double p)
        {
            return dwumian.Dwumian(lProb, K)*Math.Pow(p, K) * Math.Pow((1-p), lProb-K) ;
        }
        public void Krok2b(int f_j, int V_j)
        {
            double sumaWO = 0;
            for (int i = 0; i < aWiazka.m; i++)
            {
                wartosciOczekiwane[i] = p[i].wartOczekiwana(V_j);
                sumaWO += wartosciOczekiwane[i];
            }
            for (int i = 0; i < aWiazka.m; i++)
                prKlas[i] = wartosciOczekiwane[i] / sumaWO;

            for (int i = 0; i < aWiazka.m; i++)
                p[i] = OblRozklRuchSpl(p[i], P, f_j, V_j);
        }
    }
    public class aPrzelHipergeometryczny : aPrzel
    {
        public aPrzelHipergeometryczny(Wiazka wAlg)
            : base(wAlg)
        {
            dwumian = new dwumianNewtona(30);
            NazwaAlg = "Iversen Przel. Hipergeometr";
            SkrNazwaAlg = "przel_hip";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="K">liczba udanych prób</param>
        /// <param name="n">liczba pożądanych elementów</param>
        /// <param name="l">liczba niepożądanych elementów</param>
        /// <param name="lProb">Liczba prób</param>
        /// <returns></returns>
        private double rozkladHipergeometryczny(int K, int n, int l, int lProb)
        {
            double a = dwumian.Dwumian(n, K);
            double b = dwumian.Dwumian(l, lProb - K);
            double c = dwumian.Dwumian(n + l, lProb);
            double wynik = a * b / c;
            return wynik;

        }

        public override double w(int n_j, int n, int l, int f_j, int t_i)
        {
            if (n < 0)
                return 0;
            if (n_j % t_i != 0)
                return 0;
            if (n % t_i != 0)
                return 0;

            if (n_j + l < f_j)
            {
                if (n == 0)
                    return 1;
                else
                    return 0;
            }
            if (n_j > f_j)
                return 0;


            int liczbaWybranychPozodanychElementow = n_j;  // (int)(n_j / t_i);
            int liczbaPorzadanychElementow = n + n_j;      // (int)((n + n_j) / t_i);
            int liczbaZlychElementow = l;
            int liczbaProb = f_j;
            double wynik = rozkladHipergeometryczny(liczbaWybranychPozodanychElementow, liczbaPorzadanychElementow, liczbaZlychElementow, liczbaProb);

            double suma = 0;
            for (int k = 0; k <= f_j; k+=t_i)
            {
                suma += rozkladHipergeometryczny(k, liczbaPorzadanychElementow, liczbaZlychElementow, liczbaProb);
            }
            //if (suma < 0.5)
            //    System.Console.WriteLine("suma = {0}", suma);
            return wynik / suma;
            //_____________________________________________________________
            //double suma = 0;
            //for (int x = n_j; x < n_j + t_i; x++)
            //    suma += rozkladHipergeometryczny(x, n + n_j, l, f_j);
            //return suma;

            //_____________________________________________________________
            //double suma = 0;
            //for (int x=0; x<=liczbaProb; x++)
            //{
            //    suma += _w(x, liczbaPorzadanychElementow, liczbaZlychElementow, liczbaProb);
            //}
            //double wNiezn = _w(liczbaWybranychPozodanychElementow, liczbaPorzadanychElementow, liczbaZlychElementow, liczbaProb);
            //return wNiezn / suma;
        }
    }
}
