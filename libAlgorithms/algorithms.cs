using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;

namespace Algorithms
{
    public class DebugPar
    {
        Wiazka mWiazka;

        #region Logowanie dla algorytmów iteracyjnych
        int iteracjaNr;
        List<double[]> IterRozklad;
        List<double[,]> IterY;
        List<double[,]> IterBladWzgl;
        List<double[,]> IterSigma;
        List<double[]> SigmaStr;
        List<double[]> SigmaStrat;
        //List<double[]> SigmaProgi;
        List<double> IterEpsilon;

        private bool _logIterY;
        private bool _logIterRozkl;
        private bool _logIterBladWzgl;
        private bool _logSigmaStr;
        private bool _logSigmaStrat;

        private bool _logIterSigma;
        private bool _logEpsilon;

        /// <summary>
        /// Ustawia opcję zapisywania logowania wartości Y dla każdej z iteracji
        /// </summary>
        public bool logIterYzap
        {
            set
            {
                _logIterY = value;
            }
        }
        /// <summary>
        /// Ustawia opcję zapisywania logowania wartości Sigma dla zażdej z iteracji
        /// </summary>
        public bool logIterSigmaZap
        {
            set
            {
                _logIterSigma = value;
                if (IterSigma == null)
                    IterSigma = new List<double[,]>();
            }
        }

        /// <summary>
        /// Ustawia opcję zapisywania logowania wartości Rozkładu
        /// </summary>
        public bool logIterRozkladZap
        {
            set
            {
                _logIterRozkl = value;
            }
        }

        /// <summary>
        /// Ustawia opcję zapisywania logowania wartości Błędów bezwzględnych
        /// </summary>
        public bool logIterBladWzglZap
        {
            set
            {
                _logIterBladWzgl = value;
            }
        }

        public bool logSigmaStr
        {
            set
            {
                _logSigmaStr = value;
            }
        }

        public bool logSigmaStrat
        {
            set
            {
                _logSigmaStrat = value;
            }
        }

        public DebugPar(Wiazka mWiazka)
        {
            IterRozklad = new List<double[]>();
            IterY = new List<double[,]>();
            IterBladWzgl = new List<double[,]>();
            IterSigma = new List<double[,]>();
            IterEpsilon = new List<double>();

            SigmaStr = new List<double[]>();
            SigmaStrat = new List<double[]>();

            iteracjaNr = 0;
            this.mWiazka = mWiazka;

            _logIterY = true;
            _logIterRozkl = true;
            _logIterBladWzgl = false;
            _logIterSigma = true;
            _logEpsilon = true;


            _logSigmaStr = true;
            _logSigmaStrat = true;
        }

        public void logIterAlgorytm(Algorytm algorytm, int nrBad)
        {
            iteracjaNr = 0;
            IterBladWzgl.Clear();
            IterRozklad.Clear();
            IterSigma.Clear();
            IterY.Clear();
            IterEpsilon.Clear();
        }

        public void logIterRozklad(double[] rozklad)
        {
            if (_logIterRozkl)
            {
                double[] nowy = new double[mWiazka.V+1];
                if (rozklad != null)
                    rozklad.CopyTo(nowy, 0);
                this.IterRozklad.Add(nowy);
            }
        }
        public void logIterY(double[,] Y)
        {
            if (_logIterY)
            {
                double[,] nowy = new double[mWiazka.m, mWiazka.V + 1];
                for (int w = 0; w < mWiazka.m; w++)
                {
                    for (int k = 0; k <= mWiazka.V; k++)
                        nowy[w, k] = Y[w, k];
                } 
                this.IterY.Add(nowy);
            }
        }
        public void logIterBladWzgl(double[,] bladWzgl)
        {
            if (_logIterBladWzgl)
            {
                double[,] nowy = new double[mWiazka.m, mWiazka.V + 1];
                for (int w = 0; w < mWiazka.m; w++)
                {
                    for (int k = 0; k <= mWiazka.V; k++)
                        nowy[w, k] = bladWzgl[w,k];
                } 
                this.IterBladWzgl.Add(nowy);
            }
        }

        public void logSigmaStrukt(double []sigmy)
        {
            if (_logSigmaStr)
            {
                double[] nowy = new double[mWiazka.V + 1];
                sigmy.CopyTo(nowy, 0);
                SigmaStr.Add(nowy);
            }
        }

        public void logSigmaSrategia(double []sigmy)
        {
            if (_logSigmaStrat)
            {
                double[] nowy = new double[mWiazka.V + 1];
                sigmy.CopyTo(nowy, 0);
                SigmaStrat.Add(nowy);
            }
        }

        public void logIterSigma(double[,] sigmy)
        {
            if (_logIterSigma)
            {
                double[,] nowy = new double[mWiazka.m, mWiazka.V + 1];
                if (sigmy != null)
                {
                    for (int w = 0; w < mWiazka.m; w++)
                    {
                        for (int k = 0; k <= mWiazka.V; k++)
                            nowy[w, k] = sigmy[w, k];
                    }
                }
                this.IterSigma.Add(nowy);
            }
        }

        public void logIterEpsilon(double Epsilon)
        {
            if (_logEpsilon)
                IterEpsilon.Add(Epsilon);
        }
        /// <summary>
        /// Przygotowanie danych do nowej iteracji
        /// </summary>
        public void nowaIteracja()
        {
            iteracjaNr++;
        }

        public bool wypisywanyStan(int n)
        {
            return true;
        }

        public string naglowekTabeli
        {
            get
            {
                string wynik = "iter";
                for (int n = 0; n <= mWiazka.V; n++)
                {
                    if (wypisywanyStan(n) == true)
                        wynik+=string.Format("\t{0}", n);
                }
                wynik += "\n";
                return wynik;
            }
        }
        private string wierszIteracji(int iterNr, double[] wartosci)
        {
            string wynik = iterNr.ToString();
            for (int n = 0; n <= mWiazka.V; n++)
            {
                if (wypisywanyStan(n) == true)
                {
                    wynik += string.Format("\t{0}", wartosci[n]);
                }
            }
            wynik += "\n";
            return wynik;
        }
        private string wierszIteracjiKl(int iterNr, int klasaNr, double[,] wartosci)
        {
            string wynik = iterNr.ToString();
            for (int n = 0; n <= mWiazka.V; n++)
            {
                if (wypisywanyStan(n) == true)
                {
                    wynik += string.Format("\t{0}", wartosci[klasaNr, n]);
                }
            }
            wynik += "\n";
            return wynik;
        }

        public void logSigmy()
        {
            if (mWiazka.systemProgowy)
            {
                mWiazka.wypDeb("Progi dla poszczególnych klas\n");
                foreach (trClass kl in mWiazka.ListaKlasRuchu)
                {
                    if (kl.progiKlasy != null)
                    {
                        mWiazka.wypDeb(string.Format("Klasa {0}\n{1}", kl.ToString(), kl.progiKlasy.wypiszSigmyProgow(mWiazka.V)));
                    }
                }
                
            }
            sigmaPrzyjmZgl sigmyTemp = new sigmaPrzyjmZgl(mWiazka);

            if (_logSigmaStr)
            {
                mWiazka.wypDeb("Sigma Struktury systemu dla poszczególnych klas\n");
                mWiazka.wypDeb(sigmyTemp.wypiszSigmaStrukturySystemu());
            }
            if (_logSigmaStrat)
            {
                mWiazka.wypDeb("Sigma Strategii przyjmowania zgłoszeń w systemie dla poszczególnych klas\n");
                mWiazka.wypDeb(sigmyTemp.wypiszSigmaPolPrzyjmowaniaZlg());
            }
        }

        private string wypNaglPionowy
        {
            get
            {
                string wynik = "stan\t";

                if (_logIterY)
                {
                    for (int i = 0; i < mWiazka.m; i++)
                    {
                        if (mWiazka.ListaKlasRuchu[i].S < 0)
                            continue;
                        wynik += "\t";
                    }
                }


                for (int iter = 1; iter <= iteracjaNr; iter++)
                {
                    wynik += string.Format("iteracja {0} ξ={1}", iter, IterEpsilon[iter]);

                    if (_logIterSigma)
                    {
                        for (int i = 0; i < mWiazka.m; i++)
                        {
                            if (mWiazka.ListaKlasRuchu[i].S < 0)
                                continue;
                            wynik += "\t";
                        }
                    }

                    if (_logIterRozkl)
                        wynik += "\t";

                    if (_logIterY)
                    {
                        for (int i = 0; i < mWiazka.m; i++)
                        {
                            if (mWiazka.ListaKlasRuchu[i].S < 0)
                                continue;
                            wynik += "\t";
                        }
                    }

                    if (_logIterBladWzgl)
                    {
                        for (int i = 0; i < mWiazka.m; i++)
                        {
                            if (mWiazka.ListaKlasRuchu[i].S < 0)
                                continue;
                            wynik += "\t";
                        }
                    }

                }
                wynik += "\n";

                wynik += "n\t";
                if (_logIterY)
                {
                    for (int i = 0; i < mWiazka.m; i++)
                    {
                        if (mWiazka.ListaKlasRuchu[i].S < 0)
                            continue;
                        wynik += string.Format("$n_{{{0}}}^{{(0)}}$\t", i + 1);
                    }
                }

                for (int iter = 1; iter <= iteracjaNr; iter++)
                {
                    if (_logIterSigma)
                    {
                        for (int i = 0; i < mWiazka.m; i++)
                        {
                            if (mWiazka.ListaKlasRuchu[i].S < 0)
                                continue;
                            wynik += string.Format("$\\sigma_{{{0}, T}}^{{({1})}}$\t", i+1, iter);
                        }
                    }
                    if (_logIterRozkl)
                        wynik += string.Format("[Q^{{{0}}}]_{{({1})}}\t", iter, mWiazka.V);
                    if (_logIterY)
                    {
                        for (int i = 0; i < mWiazka.m; i++)
                        {
                            if (mWiazka.ListaKlasRuchu[i].S < 0)
                                continue;
                            wynik += string.Format("$n_{{{0}}}^{{({1})}}$\t", i + 1, iter);
                        }
                    }
                    if (_logIterBladWzgl)
                    {
                        for (int i = 0; i < mWiazka.m; i++)
                        {
                            if (mWiazka.ListaKlasRuchu[i].S < 0)
                                continue;
                            wynik += string.Format("\\xi_{{{0}}}^{{({1})}}\t", i + 1, iter);
                        }
                    }
                }
                if (_logIterSigma)
                {
                    for (int i = 0; i < mWiazka.m; i++)
                    {
                        if (mWiazka.ListaKlasRuchu[i].S < 0)
                            continue;
                        wynik += string.Format("$\\sigma_{{{0}, T}}^{{({1})}}$\t", i + 1, iteracjaNr+1);
                    }
                }

                wynik += "\n";
                return wynik;
            }
        }

        #endregion
    }

    public interface IBDwynsymulacji
    {
        int wynikDla(double a, int nrSerii, int parSymulacji, int algSymulacji, Wiazka mWiazka);
        bool odczytEB(double a, int nrSerii, int parSymulacji, int algSymulacji, int nrSystemu, trClass klasa, out double E, out double B);
        bool zapisEB(double a, int nrSerii, int parSymulacji, int algSymulacji, int nrSystemu, trClass klasa, double wartoscE, double wartoscB);
    }

    public abstract class Algorytm
    {
        protected bool _symulacja;
        public bool symulacja { get { return _symulacja; } }

        public class WynikiKlas
        {
            private double[] _a;
            private double[,] _E;
            private double[,] _blE;
            private double[,] _B;
            private double[,] _blB;
            private bool _pstrat;

            private int lKl;
            private int _lBad;
            private bool pUfnosci;
            private List<trClass> listaKlasRuchu;

            private int SzukajNrKlasy(trClass SzukanaKlasa)
            {
                int KlNum = 0;
                foreach (trClass KlasaI in listaKlasRuchu)
                {
                    if (KlasaI == SzukanaKlasa)
                        return KlNum;
                    KlNum++;
                }
                return lKl;
            }

            public bool symulacja { get { return pUfnosci; } }
            public bool pStrat { get { return _pstrat; } }
            public int lBadan { get { return _lBad; } }
            public WynikiKlas(int lBadan, List<trClass> lKlasRuchu, bool prStrat, bool PrzedzUfnosci)
            {
                listaKlasRuchu = lKlasRuchu;
                lKl = listaKlasRuchu.Count;
                _lBad = lBadan;

                _pstrat = prStrat;
                if (_pstrat)
                {
                    _B = new double[_lBad, lKl];
                    if (PrzedzUfnosci == true)
                        _blB = new double[_lBad, lKl];
                }
                _a = new double[_lBad];
                _E = new double[_lBad, lKl];
                if (PrzedzUfnosci == true)
                    _blE = new double[_lBad, lKl];
                pUfnosci = PrzedzUfnosci;
            }
            public double PobA(int nrB)
            {
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    return _a[nrB];
                }
                return 0;
            }
            public void UstawA(int nrB, double wartosc)
            {
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    _a[nrB] = wartosc;
                }
            }
            public double PobE(int nrB, trClass klasa)
            {
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        return _E[nrB, nrKlasy];
                }
                return 0;
            }
            public double PobBlE(int nrB, trClass klasa)
            {
                if (pUfnosci == false)
                    return 0;
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        return _blE[nrB, nrKlasy];
                }
                return 0;
            }
            public void UstawE(int nrB, trClass klasa, double wartosc)
            {
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        _E[nrB, nrKlasy] = wartosc;
                }
            }
            public void UstawBlE(int nrB, trClass klasa, double wartosc)
            {
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        _blE[nrB, nrKlasy] = wartosc;
                }
            }
            public double PobB(int nrB, trClass klasa)
            {
                if (_pstrat == false)
                    return 0;
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        return _B[nrB, nrKlasy];
                }
                return 0;
            }
            public double PobBlB(int nrB, trClass klasa)
            {
                if (pUfnosci == false)
                    return 0;
                if (_pstrat == false)
                    return 0;
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        return _blB[nrB, nrKlasy];
                }
                return 0;
            }
            public void UstawB(int nrB, trClass klasa, double wartosc)
            {
                if (_pstrat == false)
                    return;
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        _B[nrB, nrKlasy] = wartosc;
                }
            }
            public void UstawBlB(int nrB, trClass klasa, double wartosc)
            {
                if (pUfnosci == false)
                    return;
                if (_pstrat == false)
                    return;
                if ((nrB >= 0) && (nrB < _lBad))
                {
                    int nrKlasy = SzukajNrKlasy(klasa);
                    if (nrKlasy < lKl)
                        _blB[nrB, nrKlasy] = wartosc;
                }
            }
        }
        public delegate void pokWynikObl();

        public dwumianNewtona dwumian;
        public pokWynikObl prezentacjaWyn;


        public bool Wybrany = false;
        protected bool _Obliczony = false;
        protected bool _zainicjalizowany = false;

        public virtual bool Obliczony
        {
            get { return _Obliczony; }
            set 
            {
                _Obliczony = value;
                _zainicjalizowany = Obliczony;
            }
        }

        public WynikiKlas wynikiAlg;
        public Wiazka aWiazka;

        public string NazwaAlg = "Algorytm nieznany";
        public string SkrNazwaAlg = "Nieznany";

        protected int lBadan;
        protected int lSerii;

        public Algorytm() { aWiazka = null; _symulacja = false; }
        public Algorytm(Wiazka WiazkaAlg) { aWiazka = WiazkaAlg; _symulacja = false; }
        public virtual bool mozliwy
        {
            get
            {
                if (aWiazka == null)                        //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)     //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.V > 0)                            //Wiązka nie zawiera żadnego łącza
                    return true;
                return false;
            }
        }
        public virtual bool wymuszalny
        {
            get
            {
                return false;
            }
        }
        public virtual bool eksperymentalny
        {
            get { return false; }
        }

        public override string ToString()
        {
            return NazwaAlg;
        }
        public string ToSkrString()
        {
            return SkrNazwaAlg;
        }
        public virtual string TexKol() { return "C|"; }
        public virtual void BadajWiazke(int nrBad, double aOf)
        {
            wynikiAlg.UstawA(nrBad, aOf);
            foreach (trClass inicjowana in aWiazka.ListaKlasRuchu)
                inicjowana.ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
            aWiazka.DodPost();
        }

        public virtual void WymiarujSystem()
        {
            for (int i = 0; i < lBadan; i++)
            {
                BadajWiazke(i, aWiazka.aDelta * i + aWiazka.aStart);
            }

            Obliczony = true;
        }

        public virtual void PrzerwijWymiarowanieSystemu()
        {

        }

        public virtual void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            lSerii = LiczbaSerii;
            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, false, false);
        }
    }
    //Grupa algorytmów rekurencyjnych
    
    public class dwumianNewtona
    {
        private int nObl;
        private List<double[]> wyniki;

        public dwumianNewtona(int n)
        {
            wyniki = new List<double[]>();
            wyniki.Clear();
            wyniki.Add(new double[1]);
            wyniki[0][0] = 1;
            nObl = 0;
            dopiszDo(n);
        }

        private void dopiszDo(int n)
        {
            for (int i = nObl + 1; i <= n; i++)
            {
                double[] wiersz = new double[i + 1];
                wiersz[0] = 1;
                wiersz[i] = 1;
                for (int l = 1; l < i; l++)
                    wiersz[l] = wyniki[i - 1][l - 1] + wyniki[i - 1][l];
                wyniki.Add(wiersz);
            }
            nObl = n;
        }

        public double Dwumian(int n, int k)
        {
            if ((n < k) || (n < 0) || (k < 0))
                return 0;
            if (n > nObl)
            {
                dopiszDo(n);
            }
            return wyniki[n][k];
        }
        public double F(int x, int k, int f, int t)
        {
            double wynik = 0;
            int gr = (int)((x - k * t) / (f - t + 1));
            for (int i = 0; i <= gr; i++)
            {
                int znak = (i % 2 == 1) ? -1 : 1;
                double dw1 = Dwumian(k, i);
                double dw2 = Dwumian(x - k * (t - 1) - 1 - i * (f - t + 1), k - 1);
                wynik += (znak * dw1 * dw2);
            }
            return wynik;
        }
    }

}


