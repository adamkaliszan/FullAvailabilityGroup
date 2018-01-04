using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Simulation;
using Algorithms;
using Algorithms.hybrid;
using Algorithms.convolution;
using Algorithms.convolution.Sigma;
using Algorithms.convolution.assymetric;
using Algorithms.reccurence;
using Algorithms.convolution.stateDependent;
using Algorithms.gamma;

using algorithms.old;

namespace ModelGroup
{
    public enum reservationAlgorithm
    {
        none  = 0,
        R1_R2 = 1,
        R3    = 3
    };

    public enum subgroupChooseAlgorithm
    {
        random = 0,
        randomCapacityProportional = 1,
        randomOccupancyProportional = 2,
        sequence = 3,
        RR = 4
    };

    public class Wiazka
    {
        public IBDwynsymulacji bazaDanych;

        public sigmaPrzyjmZgl sigmy;
        public DebugPar debug;
        public delegate void UaktPost(int Post);
        public delegate void Debug(string napis);
        public delegate void PokazWynikiDel();

        private List<PodgrupaWiazek> _ListaPodgrupLaczy;
        private int _q;
        private subgroupChooseAlgorithm _aWybPodgr;
        private reservationAlgorithm _AlgorytmRezerwacji;
        private List<trClass> _ListaKlasRuchu;
        private List<Algorytm> _ListaAlgorytmow;

        public override string ToString()
        {
            string opis = "V = [" + V.ToString();
            foreach (Subgroup podgrWyp in ListaPodgrupLaczy)
                opis += " (k=" + podgrWyp.k.ToString() + ", v=" + podgrWyp.v.ToString()+")";
            opis += " ]";
            return opis;
        }

        public double aStart;
        public double aKonc;
        public double aDelta;


        public int lBadan;
        public int lSerii;
        public double CzStartu;
        public int lSymUtrZgl;
        private double PostJedBad;
        private double PostCalk;


        public UaktPost PostepObliczen;
        public Debug wypDeb;
        public PokazWynikiDel PokazWyniki;

        public void PrzerwijBadanieSystemu()
        {
            if (watekBadanieSystemu != null)
            {
                watekBadanieSystemu.Abort();
                watekBadanieSystemu = null;
            }
            foreach (Algorytm alg in ListaAlgorytmow)
            {
                alg.PrzerwijWymiarowanieSystemu();
            }
        }

        Thread watekBadanieSystemu;
        public bool badaSystem
        {
            get 
            { 
                return (watekBadanieSystemu != null) ? (watekBadanieSystemu.ThreadState != ThreadState.Stopped): false; 
            } 
        }

        private int _V;                         //określa liczbę stanowisk obsługi PJP w całej wiązce
        private bool _VjestAktualne = false;    //określa czy suma łączy _V we wszystkich podgrupach jest aktualna
        public int parSymulacji;
        public int sysNo;

        /// <summary>
        /// Obliczca pojemność podgrup wiązki kolejnego wyboru.
        /// Numeracja podgrup zaczyna się od 1.
        /// </summary>
        /// <param name="nrPodgrupy">Numer pierwszej podgrupy. Jej pojemność i kolejnych podgrup zostanie zsumowana.</param>
        /// <returns>Pojemność podgrup wiązki kolejnego wyboru.</returns>
        public int pojOdPodgrupy(int nrPodgrupy)
        {
            int wynik = 0;
            for (int k = nrPodgrupy; k <= sumaK; k++)
                wynik += PojPodgr(k);
            return wynik;
        }

        public int V
        {
            get
            {
                if (_VjestAktualne)
                    return _V;

                _V = 0;
                foreach (PodgrupaWiazek pPodgr in _ListaPodgrupLaczy)
                {
                    _V+= pPodgr.Suma;
                }
                _VjestAktualne = true;
                return _V;
            }
        }

        public int q
        {
            get
            {
                if (_AlgorytmRezerwacji == reservationAlgorithm.none)
                    return V;
                return _q;
            }
            set
            {
                _q = value;
            }
        }
        public reservationAlgorithm AlgorytmRezerwacji
        {
            get
            {
                return _AlgorytmRezerwacji;
            }
            set
            {
                if (_AlgorytmRezerwacji != value)
                {
                    KasujWyniki();
                    _AlgorytmRezerwacji = value;
                    if (_AlgorytmRezerwacji == reservationAlgorithm.none)
                        _q = V;
                    if (_AlgorytmRezerwacji == reservationAlgorithm.R1_R2)
                        _q = V - tNieuprzywilejowanyMax;
                    if (_AlgorytmRezerwacji == reservationAlgorithm.R3)
                        _q = V - tMax;
                }
            }
        }
        public int m { get { return _ListaKlasRuchu.Count; } }
        public int sumaK
        {
            get
            {
                int wynik = 0;
                foreach (Subgroup pPodgr in _ListaPodgrupLaczy)
                    wynik += pPodgr.k;
                return wynik;
            }
        }

        public int liczbaKlasPodgrup
        {
            get { return ListaPodgrupLaczy.Count; }
        }
        public int tMax 
        {
            get
            {
                int wynik = 0;
                foreach (trClass przegladana in ListaKlasRuchu)
                {
                    if (przegladana.progiKlasy == null)
                    {
                        if (przegladana.t > wynik)
                            wynik = przegladana.t;
                    }
                    else
                    {
                        for (int pr = 0; pr < przegladana.progiKlasy.liczbaPrzedziałow; pr++)
                            if (przegladana.progiKlasy[pr].t > wynik)
                                wynik = przegladana.progiKlasy[pr].t;
                    }
                } return wynik;
            }
        }
        public int tNieuprzywilejowanyMax
        {
            get
            {
                int wynik = 0;
                foreach (trClass przegladana in ListaKlasRuchu)
                    if ((przegladana.t > wynik)&&(przegladana.uprzywilejowana == false))
                        wynik = przegladana.t;
                return wynik;
            }
        }
        public int maxVi 
        {
            get
            {
                int wynik = 0;
                foreach (Subgroup przegladana in _ListaPodgrupLaczy)
                    if (wynik < przegladana.v)
                        wynik = przegladana.v;
                return wynik;
            }
        }
        public double sumaPropAT
        {
            get
            { 
                double wynik = 0;
                foreach (trClass zliczana in _ListaKlasRuchu)
                    wynik += zliczana.atProp;// *zliczana.lProgow;
                return wynik;
            }
        }
        public bool tylkoStrPoissona
        {
            get
            {
                foreach (trClass klasa in ListaKlasRuchu)
                    if (klasa.sigmaZgl(klasa.S) != 1)
                        return false;
                return true;
            }
        }
        public bool systemProgowy
        {
            get
            {
                foreach (trClass klasa in ListaKlasRuchu)
                    if (klasa.progiKlasy != null)
                        return true;
                return false;
            }
        }
        public List<PodgrupaWiazek> ListaPodgrupLaczy { get { return _ListaPodgrupLaczy; } }
        public List<trClass> ListaKlasRuchu { get { return _ListaKlasRuchu; } }
        public List<Algorytm> ListaAlgorytmow { get { return _ListaAlgorytmow; } }
        public subgroupChooseAlgorithm aWybPodgr
        {
            get { return _aWybPodgr; }
            set { _aWybPodgr = value; }
        }
        /// <summary>
        /// Zwraca pojemność ktej podgrupy. Numeracja podgrup zaczyna się od 1
        /// </summary>
        /// <param name="nrPodgrupy">Numer podgrupy</param>
        /// <returns>Pojeność pojedynczej k-tej podgrupy</returns>
        public int PojPodgr(int k)
        {
            if (k <= 0)
                throw (new ArgumentException(string.Format("nie ma podgrupy o numerze {0}.", k)));

            if (k > sumaK)
                throw (new ArgumentException(string.Format("w wiązce jest {0} podgrup, sprawdzaa jest pojemność {1}-tej podgrupy.", sumaK, k)));

            foreach (Subgroup przeglZespLacz in ListaPodgrupLaczy)
            {
                k -= przeglZespLacz.k;
                if (k < 1)
                    return przeglZespLacz.v;
            }
            throw (new Exception("Błąd w liście podgrup"));
        }

        public Wiazka(IBDwynsymulacji bazaDanych)
        {
            this.bazaDanych = bazaDanych;

            lSerii = 10;
            _AlgorytmRezerwacji=reservationAlgorithm.none;
            _aWybPodgr = subgroupChooseAlgorithm.random;
            _ListaPodgrupLaczy = new List<PodgrupaWiazek>();
            _ListaKlasRuchu = new List<trClass>();
            
            _ListaAlgorytmow = new List<Algorytm>();

//            _ListaAlgorytmow.Add(new aSplotowyZalezny(this));
            _ListaAlgorytmow.Add(new aHybrydowy(this));
            _ListaAlgorytmow.Add(new aHybrydowyMISM(this, 0.001, 0));
            _ListaAlgorytmow.Add(new aHybrydowyMISM(this, 0, 1));
            _ListaAlgorytmow.Add(new aHybrydowyY(this));
            _ListaAlgorytmow.Add(new aKaufmanRoberts(this));
            _ListaAlgorytmow.Add(new aRobertsIteracyjny(this, 0.001, 0));
            _ListaAlgorytmow.Add(new aRobertsIteracyjny(this, 0, 1));

            _ListaAlgorytmow.Add(new aSigmaGen(this));
            _ListaAlgorytmow.Add(new aSigmaGammaYm1(this));
            _ListaAlgorytmow.Add(new aSigmaGammaYm2(this));
            _ListaAlgorytmow.Add(new aGammaYc1(this));
            _ListaAlgorytmow.Add(new aSigmaGammaYc2(this));

            _ListaAlgorytmow.Add(new aSigma01(this));
            _ListaAlgorytmow.Add(new aSigmaLambdaT(this));
            _ListaAlgorytmow.Add(new aSigmaYt(this));
            _ListaAlgorytmow.Add(new aMinR(this));
            _ListaAlgorytmow.Add(new aSa3R(this));
            _ListaAlgorytmow.Add(new aMaxR(this));
            _ListaAlgorytmow.Add(new aRoberts(this));
            _ListaAlgorytmow.Add(new aRobertsOgrDostSplotowy(this));
            _ListaAlgorytmow.Add(new aRekBackForward(this, 0.001, 0));
            _ListaAlgorytmow.Add(new gamma(this));
            _ListaAlgorytmow.Add(new gammaV1(this));
            _ListaAlgorytmow.Add(new gammaV2(this));
            _ListaAlgorytmow.Add(new gammaV3(this));
            _ListaAlgorytmow.Add(new aSimulationType1(this));
            _ListaAlgorytmow.Add(new aSimulationType2(this));
            _ListaAlgorytmow.Add(new aSimulationType3(this));
            _ListaAlgorytmow.Add(new aSimulationType4(this));
            _ListaAlgorytmow.Add(new aSimulationType5(this));
            _ListaAlgorytmow.Add(new aSymulationTyp6(this));
        }
        public void addSubgroup(int lPodgrup, int PojPodgrupy)
        {
            _VjestAktualne = false;
            KasujWyniki();
            foreach (PodgrupaWiazek sprawdzana in _ListaPodgrupLaczy)
            {
                if (sprawdzana.v == PojPodgrupy)
                {
                    sprawdzana.k += lPodgrup;
                    if (_AlgorytmRezerwacji == reservationAlgorithm.R1_R2)
                        q = V - tNieuprzywilejowanyMax;
                    return;
                }
            }
            _ListaPodgrupLaczy.Add(new PodgrupaWiazek(lPodgrup, PojPodgrupy));
            if (_AlgorytmRezerwacji == reservationAlgorithm.R1_R2)
                q = V - tNieuprzywilejowanyMax;
        }

        public void UsunPodgrupyLaczy(PodgrupaWiazek usuwane)
        {
            _VjestAktualne = false;
            KasujWyniki();
            _ListaPodgrupLaczy.Remove(usuwane);
        }

//Algortmy
        public void UaktualnijAlgorytmy()
        {
            foreach (Algorytm przegladany in _ListaAlgorytmow)
            {
                ;//przegladany.
            }
        }

        class parametry
        {
            public List<Algorytm> listaAlgorytmow;
            public int lBadan;
            public int lSerii;
            public bool wymuszanie;
        }

        private void BadajSystemProc(object parametrySystemu)
        {
            parametry par = parametrySystemu as parametry;


            foreach (Algorytm inicjowany in par.listaAlgorytmow)
            {
                if ((inicjowany.Wybrany) && ((inicjowany.mozliwy) || ((inicjowany.wymuszalny) && (par.wymuszanie))) && (inicjowany.Obliczony == false))
                {
                    inicjowany.Inicjacja(par.lBadan, par.lSerii);

                    if (inicjowany.symulacja)
                        inicjowany.prezentacjaWyn = new Algorytm.pokWynikObl(PokazWyniki);

                    inicjowany.WymiarujSystem();
                }
            }
            PokazWyniki();
            
        }

        public void BadajSystem(double Start, double Koniec, double Delta, bool wymuszenie)
        {
            aStart=Start;
            aKonc=Koniec;
            aDelta=Delta;
            lBadan = (int)((aKonc - aStart+0.01) / aDelta + 1);

            int lAlg = 0;           
            foreach (Algorytm inicjowany in _ListaAlgorytmow)
            {
                if ((inicjowany.Wybrany)&&((inicjowany.mozliwy)||((inicjowany.wymuszalny)&&(wymuszenie)))&&(inicjowany.Obliczony==false))
                {
                    lAlg++;
                    if (inicjowany.symulacja)
                        lAlg += (lSerii - 1);
                }
            }

            PostCalk = 0;
            PostJedBad = (double)100 / (double)lBadan;
            PostJedBad /= (double)lAlg;

            parametry parametrySystemu = new parametry();
            parametrySystemu.listaAlgorytmow = _ListaAlgorytmow;
            parametrySystemu.lBadan = lBadan;
            parametrySystemu.lSerii = lSerii;
            parametrySystemu.wymuszanie = wymuszenie;

            watekBadanieSystemu = new Thread(BadajSystemProc);
            watekBadanieSystemu.IsBackground = true;
            watekBadanieSystemu.Start(parametrySystemu);
        }

        public void KasujWyniki()
        {
            sigmy = new sigmaPrzyjmZgl(this);
            foreach (Algorytm kasowany in ListaAlgorytmow)
                kasowany.Obliczony = false;
        }

        /// <summary>
        /// Uaktualnianie zmiennej, która jest oedworowywana w wizualizacji postępu obliczeń.
        /// </summary>
        public void DodPost()
        {
            PostCalk += PostJedBad;

            if (PostepObliczen == null)
                return;
            PostepObliczen((int)PostCalk);
        }


    }

    public class Subgroup
    {
        protected int _vi;     // Pojemność pojedynczego zasobu (łącza lub kolejki)
        protected int _k;      // liczba podgrup;

        public int Suma { get { return _k * _vi; } }
        public int k { get { return _k; } set { _k = value; } }
        public int v { get { return _vi; } }

        public Subgroup(int lPodgrup, int PojPojedynczegoLacza)
        {
            _vi = PojPojedynczegoLacza;
            _k = lPodgrup;
        }
        public override string ToString()
        {
            string wynik="k="+this._k.ToString()+"\tv="+this._vi.ToString();
            return wynik;
        }
    }

    public class PodgrupaWiazek: Subgroup
    {
        public PodgrupaWiazek(int lPodgrup, int PojPojedynczegoLacza) : base(lPodgrup, PojPojedynczegoLacza) {}
        public override string ToString()
        {
            string wynik = "podgrupa: k=" + this._k.ToString() + "\tv=" + this._vi.ToString();
            return wynik;
        }
    }
}
