using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;
using Algorithms.convolution;

namespace Algorithms.hybrid
{
    public class aHybrydowy : aSplotowyUv2
    {
        private delta delta;
        public aHybrydowy(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Hybrydowy";
            SkrNazwaAlg = "Hybrydowy";
            delta = new delta(wAlg);
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
                if (aWiazka.sumaK == 0)
                    return false;
                if ((aWiazka.sumaK ==1) && (aWiazka.systemProgowy == false) && (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none))
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.R3)
                    return false;
                return true;
            }
        }

        protected override void Krok3(int nrBad)
        {
            delta.kasujSigmy();
            delta.Oblicz(P);

            double[] R = new double[aWiazka.V + 1];
            for (int n = 0; n <= aWiazka.V; n++)
                R[n] = P[n] * delta[n];

            for (int i = 0; i < aWiazka.m; i++)
            {
                double licznikE = 0;
                double mianownikE = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    mianownikE += R[n];
                    licznikE += (R[n] * (1 - delta.sigmaStruktury[i, n]));
                }
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], licznikE / mianownikE);
            }
        }
    }

    public class aHybrydowyMISM : aSplotowyUv2
    {
        private delta delta;
        private double _epsilon;
        private int _iteracja;
        private int _maxIteracja;

        public aHybrydowyMISM(Wiazka wAlg, double eps, int maxLiczbaIteracji)
            : base(wAlg)
        {
            _epsilon = eps;

            if (_epsilon != 0)
            {
                NazwaAlg = "Hybrydowy ε " + _epsilon.ToString();
                SkrNazwaAlg = "Hybr ε " + _epsilon.ToString();
            }
            else
            {
                if (maxLiczbaIteracji == 1)
                {
                    NazwaAlg = "Hybrydowy SISM";
                    SkrNazwaAlg = "Hybr SISM";
                }
                else
                {
                    NazwaAlg = "Hybrydowy " + maxLiczbaIteracji.ToString() + " ISM";
                    SkrNazwaAlg = "Hybr" + maxLiczbaIteracji.ToString() + "Ism";
                }
            }

            delta = new delta(wAlg);

            if (_epsilon != 0)
                _maxIteracja = 10000;
            else
                _maxIteracja = maxLiczbaIteracji;
        }
        public override void Inicjacja(int LiczbaBadan, int LiczbaSerii)
        {
            lBadan = LiczbaBadan;
            wynikiAlg = new WynikiKlas(lBadan, aWiazka.ListaKlasRuchu, false, false);
            _iteracja = 0;
        }
        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK < 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.R3)
                    return false;
                if (aWiazka.tylkoStrPoissona)                          //Nie ma potrzeby stosować algorytmu iteracyjnego, wystarczy Roberts
                    return false;
                return true;
            }
        }

        protected override void Krok3(int nrBad)
        {
            delta.kasujSigmy();
            delta.Oblicz(P);

            Rozklad R = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[0], new double[aWiazka.V + 1], aWiazka.V);
            for (int i = 0; i < aWiazka.m; i++)
                R.zagregowaneKlasy.Add(aWiazka.ListaKlasRuchu[i]);

            for (int n = 0; n <= aWiazka.V; n++)
                R[n] = P[n] * delta[n];
            R.normalizacja();
            
            //aWiazka.wypDeb("Y:\n" + delta.Y.ToString()); TODO
            //aWiazka.wypDeb(string.Format("R=\t {0}\n", R));

            _iteracja = 1;

            double blad;
            while ((blad = delta.ObliczY(R)) > _epsilon)
            {
                if (_iteracja > _maxIteracja)
                    break;
                delta.ObliczDeltaZy(P);
                
                for (int n = 0; n <= aWiazka.V; n++)
                    R[n] = P[n] * delta[n];
                R.normalizacja();
                //aWiazka.wypDeb(_iteracja.ToString() + "\t" + blad.ToString() + "\r\n");
                //aWiazka.wypDeb("Y:\n"+delta.Y.ToString());
                //aWiazka.wypDeb(string.Format("R= {0}\n", R)); TODO
                _iteracja++;
            }

            //aWiazka.wypDeb(string.Format("Maksymalny błąd względny Y {0}\n", blad.ToString())); TODO

            for (int i = 0; i < aWiazka.m; i++)
            {
                double licznikE = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    licznikE += (R[n] * (1 - delta.sigmaStruktury[i, n]));
                }
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], licznikE);
            }
        }
    }
    public class aHybrydowyY : aSplotowyUv1
    {
        private delta delta;
        public aHybrydowyY(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Hybrydowy Yspl";
            SkrNazwaAlg = "Hybr Yspl";
            delta = new delta(wAlg);
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
                if (aWiazka.sumaK < 1)
                    return false;
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.R3)
                    return false;
                if (aWiazka.tylkoStrPoissona)                          //Nie ma potrzeby stosować algorytmu iteracyjnego, wystarczy Roberts
                    return false;
                return true;
            }
        }

        protected override void Krok3(int nrBad)
        {
            Rozklad P = p[0] * P_minusI[0];
            P.normalizacja();
            delta.kasujSigmy();

            delta.ObliczYspl(P_minusI, p, P);


            for (int i = 0; i < aWiazka.m; i++)
            {
                aWiazka.wypDeb(string.Format("rozkład $[p]^{{\\{{{0}\\}}}}$\t{1}\n", i, p[i])); 
            }
            for (int i = 0; i < aWiazka.m; i++)
            {
                aWiazka.wypDeb(string.Format("rozkład $[P]^{{\\setminus \\{{{0}\\}}}}$\t{1}\n", i, P_minusI[i]));
            }

            aWiazka.wypDeb(string.Format("Rozkład P\t{0}\n", P.ToString()));
            for (int i = 0; i < aWiazka.m; i++)
            {
                aWiazka.wypDeb(string.Format("n_{{{0}}}\t", i));
                for (int n=0; n<=aWiazka.V; n++)
                {
                    aWiazka.wypDeb(string.Format("{0}\t", delta.Y[i, n]));
                }
                aWiazka.wypDeb("\n");
            }
            aWiazka.wypDeb("delta\t");
            for (int n=0; n<= aWiazka.V; n++)
            {
                aWiazka.wypDeb(string.Format("{0}\t", delta[n]));
            }
            aWiazka.wypDeb("\n");
            double[] R = new double[aWiazka.V + 1];
            for (int n = 0; n <= aWiazka.V; n++)
                R[n] = P[n] * delta[n];

            aWiazka.wypDeb("Rozkład R\t");
            for (int n = 0; n <= aWiazka.V; n++)
            {
                aWiazka.wypDeb(string.Format("{0}\t", R[n]));
            }
            aWiazka.wypDeb("\n");
            for (int i = 0; i < aWiazka.m; i++)
            {
                double licznikE = 0;
                double mianownikE = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    mianownikE += R[n];
                    licznikE += (R[n] * (1 - delta.sigmaStruktury[i, n]));
                }
                wynikiAlg.UstawE(nrBad, aWiazka.ListaKlasRuchu[i], licznikE / mianownikE);
            }
        }
    }
}
