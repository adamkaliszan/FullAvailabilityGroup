using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms;
using Algorithms.convolution;
using ModelGroup;
using Distributions;

namespace Algorithms.gamma
{
    public class gamma:Algorytm
    {
        //rGamma[] q;
        //rGamma Q;
        public gamma(Wiazka wAlg): base(wAlg)
        {
            this.NazwaAlg = "Gamma D";
            this.SkrNazwaAlg = "GammaD";
        }

        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;

                return true;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            rGamma[] q = new rGamma[aWiazka.m];
            for (int i = 0; i < aWiazka.m; i++)
            {
                //aWiazka.ListaKlasRuchu[i].ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
                q[i] = new rGamma(aWiazka, i, aWiazka.ListaKlasRuchu[i].RozkladStanow(aWiazka, false));
            }
            rGamma Q = new rGamma(aWiazka);

            for (int i = 0; i < aWiazka.m; i++)
                Q.Agreguj(q[i]);

            //wynikiAlg.UstawA(nrBad, aOf);
            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass pojKlasa = aWiazka.ListaKlasRuchu[i];
                double E = 0;
                double mianownik = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    mianownik += Q.q[n];
                    E += (Q.q[n] * (1 - aWiazka.sigmy[i, n]));
                }
                wynikiAlg.UstawE(nrBad, pojKlasa, E/mianownik);
            }
        }
    }
    public class gammaV1 : Algorytm
    {
        //rGammaV1[] q;
        //rGamma Q;
        public gammaV1(Wiazka wAlg)
            : base(wAlg)
        {
            this.NazwaAlg = "Gamma Dv1";
            this.SkrNazwaAlg = "GammaDv1";
        }

        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;

                return true;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            rGammaV1[] q = new rGammaV1[aWiazka.m];
            for (int i = 0; i < aWiazka.m; i++)
            {
                //aWiazka.ListaKlasRuchu[i].ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
                q[i] = new rGammaV1(aWiazka, i, aWiazka.ListaKlasRuchu[i].RozkladStanow(aWiazka, false));
            }
            rGammaV1 Q = new rGammaV1(aWiazka);

            for (int i = 0; i < aWiazka.m; i++)
                Q.Agreguj(q[i]);

            //wynikiAlg.UstawA(nrBad, aOf);
            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass pojKlasa = aWiazka.ListaKlasRuchu[i];
                double E = 0;
                double mianownik = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    mianownik += Q.q[n];
                    E += (Q.q[n] * (1 - aWiazka.sigmy[i, n]));
                }
                wynikiAlg.UstawE(nrBad, pojKlasa, E / mianownik);
            }
        }
    }
    /// <summary>
    /// Algorytm, w którym zamiast A jest mu
    /// </summary>
    public class gammaV2 : Algorytm
    {
        //rGammaV2[] q;
        //rGamma Q;
        public gammaV2(Wiazka wAlg)
            : base(wAlg)
        {
            this.NazwaAlg = "Gamma Dv1 /u";
            this.SkrNazwaAlg = "GammaDv1 /u";
        }

        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;

                return true;
            }
        }

        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            rGammaV2[] q = new rGammaV2[aWiazka.m];
            for (int i = 0; i < aWiazka.m; i++)
            {
                //aWiazka.ListaKla2Ruchu[i].ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
                q[i] = new rGammaV2(aWiazka, i, aWiazka.ListaKlasRuchu[i].RozkladStanow(aWiazka, false));
            }
            rGammaV2 Q = new rGammaV2(aWiazka);

            for (int i = 0; i < aWiazka.m; i++)
                Q.Agreguj(q[i]);

            //wynikiAlg.UstawA(nrBad, aOf);
            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass pojKlasa = aWiazka.ListaKlasRuchu[i];
                double E = 0;
                double mianownik = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    mianownik += Q.q[n];
                    E += (Q.q[n] * (1 - aWiazka.sigmy[i, n]));
                }
                wynikiAlg.UstawE(nrBad, pojKlasa, E / mianownik);
            }
        }
    }

    /// <summary>
    /// Algorytm, w którym zamiast A jest mu
    /// </summary>
    public class gammaV3 : Algorytm
    {
        //rGammaV3[] q;
        //rGamma Q;
        public gammaV3(Wiazka wAlg)
            : base(wAlg)
        {
            this.NazwaAlg = "Gamma Dv1 *u";
            this.SkrNazwaAlg = "GammaDv1 *u";
        }

        public override bool mozliwy
        {
            get
            {
                if (aWiazka == null)                                   //Nie utworzono jeszcze wiązki
                    return false;
                if (aWiazka.ListaKlasRuchu.Count == 0)                 //Wiązce nie jest oferowana żadna klasa
                    return false;

                return true;
            }
        }
        public override void BadajWiazke(int nrBad, double aOf)
        {
            base.BadajWiazke(nrBad, aOf);
            rGammaV3[] q = new rGammaV3[aWiazka.m];
            for (int i = 0; i < aWiazka.m; i++)
            {
                //aWiazka.ListaKlasRuchu[i].ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
                q[i] = new rGammaV3(aWiazka, i, aWiazka.ListaKlasRuchu[i].RozkladStanow(aWiazka, false));
            }
            rGammaV3 Q = new rGammaV3(aWiazka);

            for (int i = 0; i < aWiazka.m; i++)
                Q.Agreguj(q[i]);

            //wynikiAlg.UstawA(nrBad, aOf);
            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass pojKlasa = aWiazka.ListaKlasRuchu[i];
                double E = 0;
                double mianownik = 0;
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    mianownik += Q.q[n];
                    E += (Q.q[n] * (1 - aWiazka.sigmy[i, n]));
                }
                wynikiAlg.UstawE(nrBad, pojKlasa, E / mianownik);
            }
        }
    }
}
