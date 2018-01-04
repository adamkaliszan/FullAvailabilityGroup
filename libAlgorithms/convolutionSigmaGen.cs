using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;

namespace Algorithms.convolution.Sigma
{
    public class aSigmaGen : aSplotowyUv2
    {
        #region konfiguracja
        public aSigmaGen(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "SigmaGen";
            SkrNazwaAlg = "SigmaGen";
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
        #endregion konfiguracja
        #region AlgorytmObl
        protected override void Krok2(int V, bool normalizacja)
        {
            rGammaSigma rC = new rGammaSigma(p[0]);
            for (int i = 1; i < aWiazka.m; i++)
            {
                rGammaSigma rD = new rGammaSigma(p[i]);
                rC.Agreguj(rD, aWiazka.V);
            }
            P = new Rozklad(rC);
        }
        #endregion AlgorytmObl
    }

    /// <summary>
    /// Algorytm Gamma dla systemów z procesem napływania zgłoszeń zależnym od stanu.
    /// A(y(n)) wyznaczane jest na podstawie równania opisującego przejścia w łańcuchu Markowa.
    /// Przyjęto, że system jest odwracalny. 
    /// Równanie nie jest uwikłane i nie ma potrzeby stosowania iteracji.
    /// </summary>
    public class aSigmaGammaYm1 : aSplotowyUv2
    {
        #region konfiguracja
        public aSigmaGammaYm1(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Sigma Gamma Y Markow 1";
            SkrNazwaAlg = "SigmaGamma Ym1";
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
        #endregion konfiguracja
        #region AlgorytmObl
        protected override void Krok2(int V, bool normalizacja)
        {
            rGammaSigmaYm1 rC = new rGammaSigmaYm1(p[0]);

            for (int i = 1; i < aWiazka.m; i++)
            {
                rGammaSigmaYm1 rD = new rGammaSigmaYm1(p[i]);
                rC.Agreguj(rD, aWiazka.V);
            }
          
            P = new Rozklad(rC);
        }
        #endregion AlgorytmObl
    }

    public class aSigmaGammaYm2 : aSplotowyUv2
    {
        #region konfiguracja
        public aSigmaGammaYm2(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Gamma Y Markow 2";
            SkrNazwaAlg = "Gamma Ym2";
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
        #endregion konfiguracja
        #region AlgorytmObl
        protected override void Krok2(int V, bool normalizacja)
        {
            rGammaSigmaYm2 rC = new rGammaSigmaYm2(p[0]);

            for (int i = 1; i < aWiazka.m; i++)
            {
                rGammaSigmaYm2 rD = new rGammaSigmaYm2(p[i]);
                rC.Agreguj(rD, aWiazka.V);
            }

            P = new Rozklad(rC);
        }
        #endregion AlgorytmObl
    }

    public class aGammaYc1 : aSplotowyUv2
    {
        #region konfiguracja
        public aGammaYc1(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Sigma Gamma Y Convolution 1";
            SkrNazwaAlg = "SigmaGamma Yc1";
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
        #endregion konfiguracja
        #region AlgorytmObl

        protected override void Krok2(int V, bool normalizacja)
        {
            rGammaYc1 []Q  = new rGammaYc1[aWiazka.m];
            Q[0] = new rGammaYc1(aWiazka, aWiazka.ListaKlasRuchu[0]);


            for (int i = 1; i < aWiazka.m; i++)
            {
                Q[i] = Q[i-1].zagregujKlase(aWiazka.ListaKlasRuchu[i]) as rGammaYc1;
            }
          
            P = new Rozklad(Q[aWiazka.m-1]);
        }
        #endregion AlgorytmObl

    }

    public class aSigmaGammaYc2 : aSplotowyUv2
    {
        #region konfiguracja
        public aSigmaGammaYc2(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Sigma Gamma Y Convolution 2";
            SkrNazwaAlg = "SigmaGamma Yc2";
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
        #endregion konfiguracja
        #region AlgorytmObl

        protected override void Krok2(int V, bool normalizacja)
        {
            rGammaYc2[] Q = new rGammaYc2[aWiazka.m];
            Q[0] = new rGammaYc2(aWiazka, aWiazka.ListaKlasRuchu[0]);


            for (int i = 1; i < aWiazka.m; i++)
            {
                Q[i] = Q[i - 1].zagregujKlase(aWiazka.ListaKlasRuchu[i]) as rGammaYc2;
            }

            P = new Rozklad(Q[aWiazka.m - 1]);
        }
        #endregion AlgorytmObl

    }
 
    public class aGenSigmaLambdaT : aSplotowyUv2
    {

        public aGenSigmaLambdaT(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "symetryczny σλt";
            SkrNazwaAlg = "sym σλt";
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

        protected override void Krok2(int V, bool normalizacja)
        {
            
            sigmaPrzyjmZgl sigmy = new sigmaPrzyjmZgl(aWiazka);
            //P = new rSigmaLambdaT(p[0]);
            rSigmaLambdaT Ps = new rSigmaLambdaT(p[0]);
            Ps.zamienNaZaleznyOdStanu(sigmy, 0);
            
            for (int i = 1; i < aWiazka.m; i++)
            {
                rSigmaLambdaT temp = new rSigmaLambdaT(p[i]);
                temp.zamienNaZaleznyOdStanu(sigmy, i);
                Ps.Agreguj(temp, sigmy);
                if (normalizacja)
                    Ps.normalizacja();
            }
            P = new Rozklad(Ps);
        }
    }
}
