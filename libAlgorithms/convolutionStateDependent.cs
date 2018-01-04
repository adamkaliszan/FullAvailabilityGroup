using System;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions.stateDependent;

namespace Algorithms.convolution.stateDependent
{
    /// <summary>
    /// Algorytm Eksperymentalny. Nie sprawdza się dla wiązki z ograniczoną dostępnością.
    /// Umożliwia modelowania systemów z klasami zgłoszeń zależnymi od siebie.
    /// Brak modeli takich klas zgłoszeń sprawia, że wyniki nie pokrywają się z symulacją.
    /// </summary>
    class aSplotowyZalezny : Algorytm
    {
        protected RozkladZalezny[] p;
        protected RozkladZalezny P;

        public aSplotowyZalezny(Wiazka algWiazka)
            : base(algWiazka)
        {
            NazwaAlg = "Splotowy zależny";
            SkrNazwaAlg = "Spl zal";
        }

        public override bool wymuszalny
        {
            get
            {
                if (aWiazka == null)                                //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)              //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.sumaK == 0)                             //W wiązce nie ma jeszcze podgrup
                    return false;
                return true;
            }
        }

        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;
                if (aWiazka.systemProgowy)
                    return false;
                return true;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            Krok1();
            Krok2();
            Krok3(nrBad);
        }

        /// <summary>
        /// Wyznaczanie rozkładów zajętości pojedynczych klas 1, 2, ..., m
        /// </summary>
        protected void Krok1()
        {
            p = new RozkladZalezny[aWiazka.m];
            for (int i=0; i<aWiazka.m; i++)
            {
                p[i] = new RozkladZalezny(aWiazka, aWiazka.ListaKlasRuchu[i]);
                //p[i].normalizacja();
            }
        }
        /// <summary>
        /// Agregowanie zależnych rozkładów klas
        /// rozkładów zajętości wszystkich klas za wyjątkiem klasy 1,2, ..., m
        /// </summary>
        /// <param name="V">Pojemność wiązki (długość rozkładu V+1)</param>
        /// <param name="normalizacja">Zagregowany rozkład jest znormalizowany</param>
        protected void Krok2()
        {
            P = new RozkladZalezny(p[0]);
            for (int i = 1; i < aWiazka.m; i++)
            {
                P.agregacja(p[i]);
                //P.normalizacja();
            }
        }

        protected virtual void Krok3(int nrBad)
        {
            foreach (trClass klasa in aWiazka.ListaKlasRuchu)
            {
                double E = 0;
                double lE = 0;
                double mE = 0;

                for (int n = 0; n <= aWiazka.V; n++)
                {
                    lE += P[n, 0] * (1 - aWiazka.sigmy[klasa, n]);
                    mE += P[n, 0];
                }

                E = lE / mE;
                wynikiAlg.UstawE(nrBad, klasa, E);
            }
        }
 
    }
}
