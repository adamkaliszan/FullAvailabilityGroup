using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;
using Algorithms;

namespace Algorithms.convolution.Sigma
{
    // Szczególny przypadek algorytmu sigma dla wiązki pełnodostępnej z rezerwacją
    public class aSigma01 : aSplotowyUv2
    {
        public aSigma01(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Iversen σ01";
            SkrNazwaAlg = "σ01";
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
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
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
            P = new rSigma01(p[0]);
            for (int i = 1; i < aWiazka.m; i++)
            {
                P.Agreguj(p[i]);
                if (normalizacja)
                    P.normalizacja();
            }
        }
    }

    // Szczególny przypadek algorytmu sigma dla wiązki pełnodostępnej z rezerwacją
    public class aSigmaLambdaT : aSplotowyUv2
    {
        public aSigmaLambdaT(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "sigma λt";
            SkrNazwaAlg = "σλt";
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
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
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
            P = new rSigmaLambdaT(p[0]);
            P.normalizacja();
            for (int i = 1; i < aWiazka.m; i++)
            {
                P.Agreguj(p[i]);
                if (normalizacja)
                    P.normalizacja();
            }
            if (!normalizacja)
                P.normalizacja();
        }
    }

    // Szczególny przypadek algorytmu sigma dla wiązki pełnodostępnej z rezerwacją
    public class aSigmaYt : aSplotowyUv2
    {
        public aSigmaYt(Wiazka wAlg)
            : base(wAlg)
        {
            NazwaAlg = "Iversen σyt";
            SkrNazwaAlg = "σyt";
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
                if (aWiazka.AlgorytmRezerwacji == reservationAlgorithm.none)         //zmienić
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
            rGammaSigma Ps = new rGammaSigma(p[0]);
            Ps.normalizacja();
            for (int i = 1; i < aWiazka.m; i++)
            {
                p[i].normalizacja();
                //Ps.Agreguj(p[i], p[i]);
                if (normalizacja)
                    Ps.normalizacja();
            }
            P = new rGammaSigma(Ps);
            P.normalizacja();
        }
    }
}
