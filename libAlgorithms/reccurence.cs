using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;
using Algorithms.convolution;

namespace Algorithms.reccurence
{
    public class aKaufmanRoberts : Algorytm
    {
        protected double[] stany;
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
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)      //w wiązce jest mechanizm rezerwacji
                    return false;
                if (aWiazka.tylkoStrPoissona == false)              //Nie-poissonowskie strumienie zgłoszeń
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
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK == 0)                             //nie jest to wiązka pełnodostęna
                    return false;
                return true;
            }
        }

        protected virtual double pobSigma(int stan, int nrKlasy) { return 1; }

        public aKaufmanRoberts(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Kaufman-Roberts";
            SkrNazwaAlg = "K-R";
        }

        //public void wypSigmy()
        //{
        //    if (!aWiazka.debugowanie)
        //        return;
        //    aWiazka.wypDeb(this.ToString() + "\n");
        //    int nStart = aWiazka.V - (aWiazka.m * (aWiazka.tMax - 1));
        //    for (int i = 0; i < aWiazka.m; i++)
        //    {
        //        for (int n = nStart; n <= aWiazka.V; n++)
        //            aWiazka.wypDeb(pobSigma(n, i).ToString("f3") + "\t");
        //        aWiazka.wypDeb("\n");
        //    }
        //}

        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            base.Inicjacja(LiczbaBadan, LiczbaSerii);
            stany = new double[aWiazka.V + 1];
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            aWiazka.debug.logIterAlgorytm(this, nrBad);
            okrRozklad();
            okrE(nrBad);
        }

        protected virtual void okrRozklad()
        {
            stany[0] = 1;
            for (int i = 1; i <= aWiazka.V; i++)
                stany[i] = 0;
            double suma = 1;

            for (int n = 1; n <= aWiazka.V; n++)
            {
                for (int i = 0; i < aWiazka.m; i++)
                {
                    int PopStan = n - aWiazka.ListaKlasRuchu[i].t;
                    if (PopStan >= 0)
                        stany[n] += (aWiazka.ListaKlasRuchu[i].at * stany[PopStan] * pobSigma(PopStan, i));
                }
                stany[n] /= n;
                suma += stany[n];
            }
            for (int i = 0; i <= aWiazka.V; i++)
                stany[i] /= suma;
        }

        protected virtual void okrE(int nrBad)
        {
            foreach (trClass pojKlasa in aWiazka.ListaKlasRuchu)
            {
                double E = 0;
                for (int n = aWiazka.V - pojKlasa.t + 1; n <= aWiazka.V; n++)
                    E += stany[n];
                wynikiAlg.UstawE(nrBad, pojKlasa, E);
                System.Console.WriteLine("E=%f", E);
            }
        }
    }
    public class aRobertsPlik : aKaufmanRoberts
    {
        bool _poprawnieZainicjalizowany;
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                return true;
            }
        }
        public override bool eksperymentalny
        {
            get { return true; }
        }
        public double[,] sigmy;
        public aRobertsPlik(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Roberts DefSigma";
            SkrNazwaAlg = "RobPlik";
        }
        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            base.Inicjacja(LiczbaBadan, LiczbaSerii);
            FileStream plik = new FileStream("sigmy.txt", FileMode.Open);
            TextReader plik3 = new StreamReader(plik);

            sigmy = new double[aWiazka.m, aWiazka.V + 1];

            for (int i = 0; i < aWiazka.m; i++)
            {
                string linia = plik3.ReadLine();
                string[] pola = linia.Split('\t');
                if (aWiazka.V + 1 > pola.Length)
                {
                    _poprawnieZainicjalizowany = false;
                    return;
                }
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    sigmy[i, n] = double.Parse(pola[n]);
                }
            }
            _poprawnieZainicjalizowany = true;
        }
        protected override double pobSigma(int stan, int nrKlasy)
        {
            return sigmy[nrKlasy, stan];
        }
        protected override void okrE(int nrBad)
        {
            for (int i = 0; i < aWiazka.m; i++)
            {
                double E = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                    E += stany[n] * (1 - sigmy[i, n]);
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], E);
            }
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            if (!_poprawnieZainicjalizowany)
                return;
            base.BadajWiazke(nrBad, aOf);
            okrRozklad();
            okrE(nrBad);
        }
    }

    public class aRobertsIteracyjny : Algorytm
    {
        protected Rozklad stany;
        protected double[] stareStany;
        protected int lIteracji;
        private int _iteracja;
        private double epsilon;

        /// <summary>
        /// Zależność procesu obsługi od stanu
        /// </summary>
        private sigmaPrzyjmZgl sigmaProcObslugi;
        private liczbaY Y;


        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.sumaK < 1)
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.R3)
                    return false;
                if (aWiazka.tylkoStrPoissona)                       //Nie ma potrzeby stosować algorytmu iteracyjnego, wystarczy Roberts
                    return false;
                return true;
            }
        }
        public bool iterowac
        {
            get
            {
                _iteracja++;
                if (lIteracji > 0)
                {
                    return (lIteracji >= _iteracja) ? true : false;
                }
                if (_iteracja == 1001)
                    return false;

               // for (int n = aWiazka.V - aWiazka.tMax; n <= aWiazka.V; n++)
                if (Y.epsilon > epsilon)
                {
                    return true;
                }
                return false;
            }
        }
        public void utworzOpis()
        {
            switch (lIteracji)
            {
                case 0:
                    NazwaAlg = "Roberts MISM ε " + epsilon.ToString();
                    SkrNazwaAlg = "Mism ε " + epsilon.ToString();
                    break;
                case 1:
                    NazwaAlg = "Roberts SISM";
                    SkrNazwaAlg = "RobSISM";
                    break;
                default:
                    NazwaAlg = "Roberts " + lIteracji.ToString() + "ISM";
                    SkrNazwaAlg = "Rob" + lIteracji.ToString() + "Ism";
                    break;
            }
        }

        public aRobertsIteracyjny(Wiazka wAlg, double epsilon, int liczbaIteracji)
            : base(wAlg)
        {
            lIteracji = liczbaIteracji;
            this.epsilon = epsilon;
            utworzOpis();
        }
        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            lSerii = LiczbaSerii;
            Y = new liczbaY(aWiazka);
            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, true, false);
            sigmaProcObslugi = new sigmaPrzyjmZgl(aWiazka);
        }

        protected void okrE(int nrBad, liczbaY Y)
        {
            foreach (trClass pojKlasa in aWiazka.ListaKlasRuchu)
            {
                int i = aWiazka.ListaKlasRuchu.IndexOf(pojKlasa);
                double E = 0;

                double licznik = 0;
                double mianownik = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    licznik += stany[n] * (1 - sigmaProcObslugi[i, n]);
                    mianownik += stany[n];
                }
                E = licznik / mianownik;
                wynikiAlg.UstawE(nrBad, pojKlasa, E);
            }
        }
        protected void okrB(int nrBad, liczbaY Y)
        {
            double[,] sigmy = new double[aWiazka.m, aWiazka.V + 1];

            foreach (trClass pojKlasa in aWiazka.ListaKlasRuchu)
            {
                double licznikB = 0;
                double mianownikB = 0;

//                int stStan = (aWiazka.q < aWiazka.V - pojKlasa.t) ? aWiazka.q : aWiazka.V - pojKlasa.t;

                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(pojKlasa);
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    double sStr = sigmaProcObslugi[nrKlasy, n];
                    sigmy[nrKlasy, n] = pojKlasa.sigmaZgl(Y[nrKlasy, n]);
                    mianownikB += stany[n] * pojKlasa.sigmaZgl(Y[nrKlasy, n]);
//                    if (n > stStan)
//                        licznikB += stany[n] * pojKlasa.sigmaZgl(Y[nrKlasy, n]);
//                    else
                        licznikB += stany[n] * pojKlasa.sigmaZgl(Y[nrKlasy, n]) * (1 - sStr);
                }
                wynikiAlg.UstawB(nrBad, pojKlasa, licznikB / mianownikB);
            }
            aWiazka.debug.logIterSigma(sigmy);
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            aWiazka.debug.logIterAlgorytm(this, nrBad);
            base.BadajWiazke(nrBad, aOf);

            stany = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[0], new double[aWiazka.V + 1], aWiazka.V);
            for (int i = 1; i < aWiazka.m; i++)
                stany.zagregowaneKlasy.Add(aWiazka.ListaKlasRuchu[i]);

            Y.Inicjalizacja();

//            double [,] temp;

            aWiazka.debug.logIterY(Y.y);
            aWiazka.debug.logIterEpsilon(-1);
            aWiazka.debug.logIterSigma(null);
            aWiazka.debug.logIterRozklad(null);

            _iteracja = 1;
            do
            {
                aWiazka.debug.nowaIteracja();
                double[,] sigmy = okrRozklad(stany, Y);
                stany.normalizacja();

                aWiazka.debug.logIterSigma(sigmy);
//                aWiazka.debug.logIterRozklad(stany.stany);

                Y.ObliczWartosciKR(stany, sigmaProcObslugi);
                aWiazka.debug.logIterY(Y.y);
                aWiazka.debug.logIterEpsilon(Y.epsilon);
            }
            while (iterowac);

            okrE(nrBad, Y);
            okrB(nrBad, Y);
        }

        protected double[,] okrRozklad(Rozklad stany, liczbaY Y)
        {
            double[,] sigmy = new double[aWiazka.m, aWiazka.V + 1];
            stany[0] = 1;
            double suma = 1;
            for (int n = 1; n <= aWiazka.V; n++)
            {
                stany[n] = 0;

                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass klasaTemp = aWiazka.ListaKlasRuchu[i];
                    if (klasaTemp.progiKlasy == null)
                    {
                        int t = aWiazka.ListaKlasRuchu[i].t;
                        int PopStan = n - t;
                        if (PopStan >= 0)
                        {
                            sigmy[i, PopStan] = aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]);
                            double temp = stany[PopStan] * klasaTemp.at;
                            temp *= sigmaProcObslugi[i, PopStan];
                            temp *= aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]);
                            stany[n] += temp;
                        }
                    }
                    else
                    {
                        for (int nrPrzedz = 0; nrPrzedz < klasaTemp.progiKlasy.liczbaPrzedziałow; nrPrzedz++)
                        {
                            int t = aWiazka.ListaKlasRuchu[i].progiKlasy[nrPrzedz].t;
                            int PopStan = n - t;

                            if (PopStan >= 0)
                            {
                                if (klasaTemp.progiKlasy.nrPrzedzialu(PopStan) == nrPrzedz)
                                {
                                    sigmy[i, PopStan] = aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]);
                                    double at = klasaTemp.atProgi(PopStan);
                                    stany[n] += (at * stany[PopStan] * aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]) * sigmaProcObslugi[i, PopStan]);
                                }
                            }
                        }
                    }

                }
                stany[n] /= n;
                suma += stany[n];
            }
            for (int n = 0; n <= aWiazka.V; n++)
                stany[n] = stany[n] / suma;
            return sigmy;
        }
    }

    public class aRoberts : aKaufmanRoberts
    {
        /// <summary>
        /// Zależność procesu obsługi od stanu
        /// </summary>
        sigmaPrzyjmZgl sigmaProcPrzyjmowaniaZgloszen;
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK == 0)
                    return false;
                if ((aWiazka.sumaK == 1) && (aWiazka.systemProgowy == false))      //Jest jedna podgrupa lub brak
                    return false;
                if (aWiazka.tylkoStrPoissona == false)              //Strumienie zgłoszeń nie są poissonowskie
                    return false;
                return true;
            }
        }
        public aRoberts(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Roberts";
            SkrNazwaAlg = "Rob";
            sigmaProcPrzyjmowaniaZgloszen = new sigmaPrzyjmZgl(aWiazka);
        }

        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            base.Inicjacja(LiczbaBadan, LiczbaSerii);
            sigmaProcPrzyjmowaniaZgloszen.obliczSigmy();

        }
        protected override void okrRozklad()
        {
            stany[0] = 1;
            for (int i = 1; i <= aWiazka.V; i++)
                stany[i] = 0;
            double suma = 1;

            for (int n = 1; n <= aWiazka.V; n++)
            {
                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass klasaTemp = aWiazka.ListaKlasRuchu[i];
                    if (klasaTemp.progiKlasy == null)
                    {
                        int t = aWiazka.ListaKlasRuchu[i].t;
                        int PopStan = n - t;
                        if (PopStan >= 0)
                        {
                            stany[n] += (aWiazka.ListaKlasRuchu[i].at * stany[PopStan] * sigmaProcPrzyjmowaniaZgloszen[i, n - t]);
                        }
                    }
                    else
                    {
                        for (int nrPrzedz = 0; nrPrzedz < klasaTemp.progiKlasy.liczbaPrzedziałow; nrPrzedz++)
                        {
                            int t = aWiazka.ListaKlasRuchu[i].progiKlasy[nrPrzedz].t;
                            int PopStan = n - t;

                            if (PopStan >= 0)
                                if (klasaTemp.progiKlasy.nrPrzedzialu(PopStan) == nrPrzedz)
                                {
                                    double at = klasaTemp.atProgi(PopStan);
                                    stany[n] += (at * stany[PopStan] * sigmaProcPrzyjmowaniaZgloszen[i, PopStan]);

                                }
                        }
                    }
                }
                stany[n] /= n;
                suma += stany[n];
            }
            for (int i = 0; i <= aWiazka.V; i++)
                stany[i] /= suma;
        }


        protected override void okrE(int nrBad)
        {
            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass pojKlasa = aWiazka.ListaKlasRuchu[i];
                double E = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                    E += (stany[n] * (1 - sigmaProcPrzyjmowaniaZgloszen[i,n]));
                wynikiAlg.UstawE(nrBad, pojKlasa, E);
            }
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            okrRozklad();
            okrE(nrBad);
//            wypSigmy(); TODO
        }
    }

    public class aRobertsOgrDostSplotowy : aKaufmanRoberts
    {
        double[,] sigmy;
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK <= 1)                             //podgrupy mają różną pojemność lub nie ma podgrup
                    return false;
                if (aWiazka.AlgorytmRezerwacji != reservationAlgorithm.none)      //Alg tylko dl awiązki z ogra dost
                    return false;
                return true;
            }
        }
        public aRobertsOgrDostSplotowy(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Rob Splotowy";
            SkrNazwaAlg = "RobSpl";
            sigmy = null;
        }

        protected override double pobSigma(int stan, int nrKlasy)
        {
            return sigmy[nrKlasy, stan];
        }

        protected override void okrRozklad()
        {
            sigmy = new double[aWiazka.m, aWiazka.V + 1];
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                double[] stany = new double[t];

                foreach (trClass kl in aWiazka.ListaKlasRuchu)
                {
                    int zakres = kl.t;
                    for (int n = 0; n < zakres; n++)
                        stany[n % t] += (kl.aProp);
                }
                //              for (int n = 0; n < t; n++)
                //                  stany[n] /= aWiazka.sumaPropAT;

                Rozklad x = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[i], stany, t - 1);
                x.normalizacja();

                Rozklad X = new Rozklad(x);
                for (int k = 1; k < aWiazka.sumaK - 1; k++)
                    X = X * x;

                for (int n = aWiazka.V; n >= 0; n--)
                {
                    if (aWiazka.V - n - t < 0)
                        sigmy[i, n] = 0;
                    else
                        sigmy[i, n] = sigmy[i, n + 1] + X[aWiazka.V - n - t];
                }
            }

            base.okrRozklad();
        }

        protected override void okrE(int nrBad)
        {
            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass pojKlasa = aWiazka.ListaKlasRuchu[i];
                double E = 0;

                int stStan = (aWiazka.q < aWiazka.V - pojKlasa.t) ? aWiazka.q : aWiazka.V - pojKlasa.t;
                for (int n = 0; n <= stStan; n++)
                    E += (stany[n] * (1 - pobSigma(n, i)));
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
//            wypSigmy(); TODO
        }
    }
    /// <summary>
    /// Algorytm iteracyjno-rekurencyjny, w którym równania stanów 
    /// wyznaczane są na podstawie przejść w przód i w tył.
    /// Współczynniki sigma są takie same jak w alg. Robertsa
    /// </summary>
    public class aRekBackForward : Algorytm
    {
        protected Rozklad stany;
        protected double[] stareStany;
        protected int lIteracji;
        private int _iteracja;
        private double epsilon;

        /// <summary>
        /// Zależność procesu przyjmowania od stanu
        /// </summary>
        private sigmaPrzyjmZgl sigmaProcPrzyjmZgl;
        private liczbaY Y;


        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.sumaK < 1)
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.R3)
                    return false;
                if (aWiazka.tylkoStrPoissona)                       //Nie ma potrzeby stosować algorytmu iteracyjnego, wystarczy Roberts
                    return false;
                return true;
            }
        }
        public bool iterowac
        {
            get
            {
                _iteracja++;
                if (lIteracji > 0)
                {
                    return (lIteracji >= _iteracja) ? true : false;
                }
                if (_iteracja == 10)
                    return false;

                // for (int n = aWiazka.V - aWiazka.tMax; n <= aWiazka.V; n++)
                if (Y.epsilon > epsilon)
                {
                    return true;
                }
                return false;
            }
        }
        public void utworzOpis()
        {
            switch (lIteracji)
            {
                case 0:
                    NazwaAlg = "BackFoward MISM ε " + epsilon.ToString();
                    SkrNazwaAlg = "BF ε " + epsilon.ToString();
                    break;
                case 1:
                    NazwaAlg = "BackFoward SISM";
                    SkrNazwaAlg = "BF SISM";
                    break;
                default:
                    NazwaAlg = "Back Forward " + lIteracji.ToString() + "ISM";
                    SkrNazwaAlg = "BF" + lIteracji.ToString() + "Ism";
                    break;
            }
        }

        public aRekBackForward(Wiazka wAlg, double epsilon, int liczbaIteracji)
            : base(wAlg)
        {
            lIteracji = liczbaIteracji;
            this.epsilon = epsilon;
            utworzOpis();
        }
        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            lSerii = LiczbaSerii;
            Y = new liczbaY(aWiazka);
            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, true, false);
            sigmaProcPrzyjmZgl = new sigmaPrzyjmZgl(aWiazka);
        }

        protected void okrE(int nrBad, liczbaY Y)
        {
            foreach (trClass pojKlasa in aWiazka.ListaKlasRuchu)
            {
                int i = aWiazka.ListaKlasRuchu.IndexOf(pojKlasa);
                double E = 0;

                double licznik = 0;
                double mianownik = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    licznik += stany[n] * (1 - sigmaProcPrzyjmZgl[i, n]);
                    mianownik += stany[n];
                }
                E = licznik / mianownik;
                wynikiAlg.UstawE(nrBad, pojKlasa, E);
            }
        }
        protected void okrB(int nrBad, liczbaY Y)
        {
            double[,] sigmy = new double[aWiazka.m, aWiazka.V + 1];

            foreach (trClass pojKlasa in aWiazka.ListaKlasRuchu)
            {
                double licznikB = 0;
                double mianownikB = 0;

                int nrKlasy = aWiazka.ListaKlasRuchu.IndexOf(pojKlasa);
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    double sStr = sigmaProcPrzyjmZgl[nrKlasy, n];
                    sigmy[nrKlasy, n] = pojKlasa.sigmaZgl(Y[nrKlasy, n]);
                    mianownikB += stany[n] * pojKlasa.sigmaZgl(Y[nrKlasy, n]);
                    licznikB += stany[n] * pojKlasa.sigmaZgl(Y[nrKlasy, n]) * (1 - sStr);
                }
                wynikiAlg.UstawB(nrBad, pojKlasa, licznikB / mianownikB);
            }
            aWiazka.debug.logIterSigma(sigmy);
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            aWiazka.debug.logIterAlgorytm(this, nrBad);
            base.BadajWiazke(nrBad, aOf);

            stany = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[0], new double[aWiazka.V + 1], aWiazka.V);
            for (int i = 1; i < aWiazka.m; i++)
                stany.zagregowaneKlasy.Add(aWiazka.ListaKlasRuchu[i]);

            Y.Inicjalizacja();

//            aWiazka.debug.logIterY(Y.y);
//            aWiazka.debug.logIterEpsilon(-1);
//            aWiazka.debug.logIterSigma(null);
//            aWiazka.debug.logIterRozklad(null);


//            aWiazka.debug.nowaIteracja();
            double[,] sigmy = okrRozklad(stany, Y);
            stany.normalizacja();

//            aWiazka.debug.logIterSigma(sigmy);
//            aWiazka.debug.logIterRozklad(stany.stany);

            Y.ObliczWartosciKR(stany, sigmaProcPrzyjmZgl);
//            aWiazka.debug.logIterY(Y.y);
//            aWiazka.debug.logIterEpsilon(Y.epsilon);

            
            _iteracja = 1;
            do
            {
//                aWiazka.debug.nowaIteracja();
                sigmy = okrRozkladBF(stany, Y);
                stany.normalizacja();

//                aWiazka.debug.logIterSigma(sigmy);
//                aWiazka.debug.logIterRozklad(stany.stany);

                Y.ObliczWartosciBF(stany, sigmaProcPrzyjmZgl);
//                aWiazka.debug.logIterY(Y.y);
//                aWiazka.debug.logIterEpsilon(Y.epsilon);
            }
            while (iterowac);

            okrE(nrBad, Y);
            okrB(nrBad, Y);
//            aWiazka.debug.logIteracje(this, aOf, true);
        }

        protected double[,] okrRozklad(Rozklad stany, liczbaY Y)
        {
            double[,] sigmy = new double[aWiazka.m, aWiazka.V + 1];
            stany[0] = 1;
            double suma = 1;
            for (int n = 1; n <= aWiazka.V; n++)
            {
                stany[n] = 0;

                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass klasaTemp = aWiazka.ListaKlasRuchu[i];
                    if (klasaTemp.progiKlasy == null)
                    {
                        int t = aWiazka.ListaKlasRuchu[i].t;
                        int PopStan = n - t;
                        if (PopStan >= 0)
                        {
                            sigmy[i, PopStan] = aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]);
                            double temp = stany[PopStan] * klasaTemp.at;
                            temp *= sigmaProcPrzyjmZgl[i, PopStan];
                            temp *= aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]);
                            stany[n] += temp;
                        }
                    }
                    else
                    {
                        for (int nrPrzedz = 0; nrPrzedz < klasaTemp.progiKlasy.liczbaPrzedziałow; nrPrzedz++)
                        {
                            int t = aWiazka.ListaKlasRuchu[i].progiKlasy[nrPrzedz].t;
                            int PopStan = n - t;

                            if (PopStan >= 0)
                            {
                                if (klasaTemp.progiKlasy.nrPrzedzialu(PopStan) == nrPrzedz)
                                {
                                    sigmy[i, PopStan] = aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]);
                                    double at = klasaTemp.atProgi(PopStan);
                                    stany[n] += (at * stany[PopStan] * aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]) * sigmaProcPrzyjmZgl[i, PopStan]);
                                }
                            }
                        }
                    }
                }
                stany[n] /= n;
                suma += stany[n];
            }
            for (int n = 0; n <= aWiazka.V; n++)
                stany[n] = stany[n] / suma;
            return sigmy;
        }

        /// <summary>
        /// Określa rozkład zajętości na podstawie przejść w przód i wstecz
        /// </summary>
        /// <param name="stany"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        protected double[,] okrRozkladBF(Rozklad stany, liczbaY Y)
        {
            double[] noweStany = new double[aWiazka.V + 1];
            double[,] sigmy = new double[aWiazka.m, aWiazka.V + 1];
            double suma = 0;

            for (int i = 0; i < aWiazka.m; i++)
            {
                sigmy[i, 0] = aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, 0]);
            }

            for (int n = 0; n <= aWiazka.V; n++)
            {
                double licznik = 0;
                double mianownik = 0;


                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass klasaTemp = aWiazka.ListaKlasRuchu[i];
                    sigmy[i, n] = klasaTemp.sigmaZgl(Y[i, n]);

                    if (klasaTemp.progiKlasy == null)
                    {
                        int t = aWiazka.ListaKlasRuchu[i].t;
                        int PopStan = n - t;
                        int NastStan = n + t;
                        if (PopStan >= 0)
                        {
                            double temp = stany[PopStan] * klasaTemp.PodajIntZgl(0);
                            temp *= sigmaProcPrzyjmZgl[i, PopStan];
                            temp *= klasaTemp.sigmaZgl(Y[i, PopStan]);
                            licznik += temp;
                        }
                        if (NastStan <= aWiazka.V)
                        {
                            double temp = Y[i, NastStan] * klasaTemp.mu * stany[NastStan];
                            licznik += temp;
                            mianownik += klasaTemp.PodajIntZgl(0) * klasaTemp.sigmaZgl(Y[i, n]) * sigmaProcPrzyjmZgl[i, n];
                        }
                        if (n >= t)
                            mianownik += klasaTemp.mu * Y[i, n];
                    }
                    //else
                    //{
                    //    for (int nrPrzedz = 0; nrPrzedz < klasaTemp.progiKlasy.liczbaPrzedziałow; nrPrzedz++)
                    //    {
                    //        int t = aWiazka.ListaKlasRuchu[i].progiKlasy[nrPrzedz].t;
                    //        int PopStan = n - t;

                    //        if (PopStan >= 0)
                    //        {
                    //            if (klasaTemp.progiKlasy.nrPrzedzialu(PopStan) == nrPrzedz)
                    //            {
                    //                sigmy[i, PopStan] = aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]);
                    //                double at = klasaTemp.atProgi(PopStan);
                    //                stany[n] += (at * stany[PopStan] * aWiazka.ListaKlasRuchu[i].sigmaZgl(Y[i, PopStan]) * sigmaProcObslugi[i, PopStan]);
                    //            }
                    //        }
                    //    }
                    //}

                }
                noweStany[n] = licznik / mianownik;
                suma += noweStany[n];
            }
            for (int n = 0; n <= aWiazka.V; n++)
                stany[n] = noweStany[n] / suma;
            return sigmy;
        }
    }

}
