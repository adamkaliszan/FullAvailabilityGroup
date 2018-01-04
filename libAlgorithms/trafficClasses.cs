using System;
using System.Collections.Generic;
using System.Text;
using Simulation;

namespace ModelGroup
{   
    public class SigmaStruktury
    {
        private int _V;
        private int[] _zglStraconeW_stanieN;
        private int[] _zglObsluzoneW_stanieN;

        public SigmaStruktury(int V)
        {
            this._V = V;
            _zglStraconeW_stanieN = new int[_V+1];
            _zglObsluzoneW_stanieN = new int[_V+1];

        }
        public void PrzyjZgl(int nrStanu)
        {
            _zglObsluzoneW_stanieN[nrStanu]++;
        }
        public void StrZgl(int nrStanu)
        {
            _zglStraconeW_stanieN[nrStanu]++;
        }
        public double this[int index]
        {
            get
            {
                if (index <= _V)
                {
                    if ((_zglObsluzoneW_stanieN[index] + _zglStraconeW_stanieN[index]) > 0)
                        return (double)((double)_zglObsluzoneW_stanieN[index] / (double)(_zglObsluzoneW_stanieN[index] + _zglStraconeW_stanieN[index]));
                    else
                    {
                        if (index == 0)
                            return 1;
                        return this[index - 1];
                    }
                }
                else
                    return -2;

            }
        }
        public void wyczysc()
        {
            for (int i = 0; i <= _V; i++)
            {
                _zglStraconeW_stanieN[i] = 0;
                _zglObsluzoneW_stanieN[i] = 0;
            }
        }
    }
    /// <summary>
    /// Eksperymentalna klasa. Nie wiadomo jak przeliczać proporcje w algorytmie splotowym
    /// </summary>
    public class progiKlasy
    {
        public string wypiszSigmyProgow(int V)
        {
            string wynik = "";
            for (int i = 0; i < _lPrzedzialow; i++)
            {
                string opis = this.listaPrzedzialow[i].Opis(V);
                wynik += string.Format("Próg {0} ({1})\t", i, opis); 
                for (int n=0; n <= V; n++)
                {

                    wynik += string.Format("\t{0}", listaPrzedzialow[i].sigma(n));
                }
                wynik += "\n";
            }
            return wynik;
        }

        /// <summary>Lista przedziałów progowych</summary>
        private przedzial[] listaPrzedzialow;
        /// <summary>Liczba przedziałów. Jest ona równa liczbie progów plus 1</summary>
        protected int _lPrzedzialow;

        /// <summary>
        /// Lista z informacjami o progach klasy. W momencie tworzenia listy, przedziały mają przypisane domyślne wartości
        /// </summary>
        /// <param name="liczbaProgow">Liczba przedziałów progowych</param>
        public progiKlasy(int liczbaProgow)
        {
            _lPrzedzialow = liczbaProgow + 1;
            listaPrzedzialow = new przedzial[_lPrzedzialow];
            for (int i = 0; i < _lPrzedzialow; i++)
            {
                listaPrzedzialow[i] = new przedzial();
            }
        }
        public przedzial this[int index]
        {
            get
            {
                if ((index >= _lPrzedzialow) || (index < 0))
                    throw new ArgumentException("Klasa ma " + listaPrzedzialow.ToString() + ". Zapytanie o próg nr " + index.ToString() + ".");
                return listaPrzedzialow[index];
            }
        }
        /// <summary>
        /// Zwraca numer przedziału progowego, któremu odpowiada podany stan. Jeśli, żaden przedział nie ma takiego stanu, to funkcja zwraca najstarszy stan
        /// </summary>
        /// <param name="stan">Stan zajętości systemu</param>
        /// <returns>Numer przedziału, któremu odpowiada podany stan</returns>
        public int nrPrzedzialu(int stan)
        {
            for (int i=0; i<liczbaPrzedziałow; i++)
                if (this[i].zawieraTenStan(stan))
                    return i;
            return liczbaPrzedziałow - 1;
        }

        /// <summary>
        /// Zwraca numer przedziału progowego, któremu odpowiada podany stan. Jeśli, żaden przedział nie ma takiego stanu, to funkcja zwraca najstarszy stan
        /// </summary>
        /// <param name="stan">Stan zajętości systemu</param>
        /// <returns>Numer przedziału, któremu odpowiada podany stan</returns>
        public przedzial Przedzial(int stan)
        {
            for (int i = 0; i < liczbaPrzedziałow; i++)
                if (this[i].zawieraTenStan(stan))
                    return this[i];
            return null;
        }

        /// <summary>
        /// Liczba przedziałów, o jeden większa niż liczba progów.
        /// </summary>
        public int liczbaPrzedziałow
        {
            get
            {
                return _lPrzedzialow;
            }
        }


        public class przedzial
        {
            public przedzial()
            {
                t = 1;
                _mu = 1;
                _okrMin = false;
                _okrMax = false;
                mnoznikIntensywnosci = 1;
            }

            public string Opis(int V)
            {
                int min = (minN > 0) ? minN+1 : 0;
                int max = (maksN <= V) ? maksN : V;
                
                return string.Format("<{0} - {1}> t={2}, µ={3}", min, max, t, _mu);  
            }

            /// <summary>Liczba żądanych PJP przez zgłoszenie danej klasy w tym progu.</summary>
            public int t;
            /// <summary>Współczynnik zmiany intensywnoci strumienia zgłoszeń dla poszczególnych progów</summary>
            /// <remarks>Współczynniki zgłoszeń obliczane są na podstawie proporcji ruchu klas i średniego ruchu oferowanego PJP, 
            /// zatem nie można w progach podawać konkretnej wartości lambda lub gamma.</remarks>
            public double mnoznikIntensywnosci;
            public double _mu;
            /// <summary>Dolna granica progu (zawarta w progu)</summary>
            private int _minN;
            /// <summary>Górna granica progu (nie zawarta w progu)</summary>
            private int _maksN;

            private bool _okrMin;
            private bool _okrMax;

            public int minN
            {
                set 
                {
                    _minN = value;
                    _okrMin = true;
                }
                get 
                {
                    if (_okrMin == true)
                        return _minN;
                    return -1;
                }
            }
            public int maksN
            {
                set 
                {
                    _maksN = value;
                    _okrMax = true;
                }
                get 
                {
                    if (_okrMax == true)
                        return _maksN;
                    return int.MaxValue;
                }
            }

            /// <summary>
            /// Funkcja określa, czy n nalezy do przedziału danego przedziału
            /// </summary>
            /// <param name="stan"></param>
            /// <returns></returns>
            public double sigma(int stan)
            {
                if ((minN < stan) && (stan <= maksN))
                    return 1;
                return 0;
            }
            public bool zawieraTenStan(int stan)
            {
                if ((minN < stan) && (stan <= maksN))
                    return true;
                return false;
            }
        }
    }
    
    public abstract class trClass : IComparable<trClass>
    {
        #region atrybuty
        /// <summary>
        /// Proporcje at danej klasy w porównaniu do innych klas. Podstawowy parametr wejściowy a1t1:a2t2:...
        /// </summary>
        protected int _atProp;
        /// <summary>Intensywność obsługi</summary>
        protected double _mu;
        /// <summary>Liczba żądanych PJP przez pojedyncze zgłoszenie.</summary>
        protected int _t;
        protected int _tMax = -1;
        protected int _tMin = -1;
        protected double _muMin = -1;
        protected double _muMax = -1;

        /// <summary>
        /// Intensywność strumienia zgłoszeń
        /// </summary>
        protected double _lambdaZero;

        public enum typKlasy
        {
            ERLANG,
            ENGSET,
            PASCAL
        }

        public typKlasy typ;

        /// <summary>Progi klasy. Występują w systemach progowych</summary>
        public progiKlasy progiKlasy = null;

        public SigmaStruktury sigmyStanow;
        public double[] y;

        protected Wiazka _wiazkaObsl;
        public bool uprzywilejowana=false;
        public SimClass []symKlasy;

        public bool wybrana;

        #endregion atrybuty

        #region Konstruktory i inicjalizacja
        /// <summary>
        /// Konstruktor klasy ruchu
        /// </summary>
        /// <param name="wiazkaKlasy">Wiązka, której oferowana jest ta klasa ruchu</param>
        /// <param name="at">Udział w proporcjach ruchu a_it_i oferowanych klas</param>
        /// <param name="t">Liczba żądanych PJP na obsługę zgłoszenia</param>
        /// <param name="mi">Intensywność obsługi zgłoszeń</param>
        /// <param name="uprzyw">Określa uprzywilejowanie lub nie klasy</param>
        public trClass(Wiazka wiazkaKlasy, int at, int t, double mi, bool uprzyw)
        {
            uprzywilejowana = uprzyw;
            _wiazkaObsl = wiazkaKlasy;
            atProp = at;
            this._t = t;
            this._mu = mi;
            this.wybrana = false;
        }

        /// <summary>
        /// Wznacza parametry klasy
        /// </summary>
        /// <param name="aOfWiazce">Całkowity ruch A=a*V oferowany wiązce</param>
        /// <param name="sAt">Suma proporcji ruchów oferowanych przez klasy</param>
        /// <param name="V">Pojemność wiązki</param>
        public virtual void ObliczParametry(double aOfWiazce, double sAt, int V) { }
        
        /// <summary>
        /// Wyznacza ruch oferowany klasy
        /// </summary>
        /// <param name="aOfWiazce">Całkowity ruch A = a*V oferowany wiązce</param>
        /// <param name="sAt">Suma proporcji ruchu oferowanego a_it_i przez klasy</param>
        /// <param name="V">Pojemność wiązki</param>
        /// <returns>Ruch oferowany klasy</returns>
        protected double ObliczRuchOferowanyKlasy(double aOfWiazce, double sAt, int V)
        {
            if (progiKlasy == null)
                return aOfWiazce * V * (atProp / sAt) / t;
            return aOfWiazce * V * (atProp / sAt) / progiKlasy[0].t;
        }

        public virtual string ToSkrString()
        {
            string wynik = "kl";
            if (progiKlasy != null)
                wynik += ("_pr:" + progiKlasy.liczbaPrzedziałow.ToString());
            wynik += (uprzywilejowana == true) ? "+ " : " ";
            wynik += (atProp.ToString() + ", " + t.ToString() + ", " + _mu.ToString());
            return wynik;
        }

        public abstract string ToSkrStringFS();

        public abstract string ToStringBezProgow();

        #endregion Konstruktory i inicjalizacja

        public int CompareTo(trClass rhs)
        {
            if (progiKlasy == null)
            {
                if (rhs.progiKlasy == null)
                {
                    int wynik = this.t.CompareTo(rhs.t);
                    if (wynik == 0)
                    {
                        wynik = this.atProp.CompareTo(rhs.atProp);
                        if (wynik == 0)
                            wynik = this._mu.CompareTo(rhs._mu);
                    }
                    return wynik;
                }
                else
                {
                    int wynik = this.t.CompareTo(rhs.progiKlasy[0].t);
                    if (wynik == 0)
                    {
                        wynik = this.atProp.CompareTo(rhs.atProp);
                        if (wynik == 0)
                            wynik = this._mu.CompareTo(rhs.progiKlasy[0]._mu);
                    }
                    return wynik;
                }
            }
            else
            {
                if (rhs.progiKlasy == null)
                {
                    int wynik = this.progiKlasy[0].t.CompareTo(rhs.t);
                    if (wynik == 0)
                    {
                        wynik = this.atProp.CompareTo(rhs.atProp);
                        if (wynik == 0)
                            wynik = this.progiKlasy[0]._mu.CompareTo(rhs._mu);
                    }
                    return wynik;
                }
                else
                {
                    int wynik = this.progiKlasy[0].t.CompareTo(rhs.progiKlasy[0].t);
                    if (wynik == 0)
                    {
                        wynik = this.atProp.CompareTo(rhs.atProp);
                        if (wynik == 0)
                            wynik = this.progiKlasy[0]._mu.CompareTo(rhs.progiKlasy[0]._mu);
                    }
                    return wynik;
                }
            }
        }

        #region wlasciwosci
        public Wiazka wiazkaObsl { get { return _wiazkaObsl; } }

        public int atProp
        {
            get
            {
                return _atProp;
            }
            set
            {
                _atProp = value;
            }
        }
        public double aProp
        {
            get
            {
                return (double)(_atProp)/(double)(t);
            }
        }
        public int t
        {
            get
            {
                if (progiKlasy != null)
                {
                    return _t;
                    //throw (new Exception("nie można czytać t dla klas z progami"));
                }
                return _t;
            }
            set
            {
                _t = value;
            }
        }

        /// <summary>
        /// Liczba źródeł ruchu
        /// </summary>
        public virtual int S
        {
            get
            {
                return -1;
            }
            set
            {
                ;
            }
        }

        public double a
        {
            get { return _lambdaZero / _mu; }
        }
        public double at
        {
            get { return a * t; }
        }

        /// <summary>
        /// Intensywność obsługi zgłoszeń
        /// </summary>
        public double mu
        {
            get
            {
                if (progiKlasy != null)
                {
                    throw (new Exception("nie można czytać mu dla klas z progami"));
                }
                return _mu;
            }
            set
            {
                _mu = value;
            }
        }


        #endregion wlasciwosci

        #region progi

        /// <summary>
        /// Liczba żądanych PJP przez zgłoszenie dla odpowiedniego progu,
        /// gdy rozważamy przejście ze stanu n do stanu n+t
        /// </summary>
        /// <param name="stan">Stan zajętości systemu z którego przechodzimy do kolejnego stanu</param>
        /// <returns>Liczba żądanych PJP przez zgłoszenie dla odpowiedniego progu</returns>
        public int tOdStanu(int stan)
        {
            if (progiKlasy == null)
                return _t;
            return (progiKlasy.Przedzial(stan).t);
        }

        /// <summary>
        /// Maksymalna liczba żądanych PJP ze wszystkich przedziałów progowych klasy
        /// </summary>
        public int tMax
        {
            get
            {
                if (progiKlasy == null)
                    return _t;
                else
                {
                    if (_tMax > 0)
                        return _tMax;
                    for (int i=0; i<progiKlasy.liczbaPrzedziałow; i++)
                        if (progiKlasy[i].t > _tMax)
                            _tMax = progiKlasy[i].t;
                    return _tMax;
                }
            }
        }

        /// <summary>
        /// Minimalna liczba żądanych PJP ze wszystkich przedziałów progowych klasy
        /// </summary>
        public int tMin
        {
            get
            {
                if (progiKlasy == null)
                    return _t;
                else
                {
                    if (_tMin > 0)
                        return _tMin;
                    _tMin = progiKlasy[0].t;
                    for (int i = 1; i < progiKlasy.liczbaPrzedziałow; i++)
                        if (progiKlasy[i].t < _tMin)
                            _tMin = progiKlasy[i].t;
                    return _tMin;
                }
            }
        }

        public double muMax
        {
            get
            {
                if (progiKlasy == null)
                    return _mu;
                if (_muMax > 0)
                    return _muMax;
                _muMax = progiKlasy[0]._mu;
                for (int i = 1; i < progiKlasy.liczbaPrzedziałow; i++)
                    if (progiKlasy[i]._mu > _muMax)
                        _muMax = progiKlasy[i]._mu;
                return _muMax;
            }
        }

        public double muMin
        {
            get
            {
                if (progiKlasy == null)
                    return _mu;
                if (_muMin > 0)
                    return _muMin;
                _muMin = progiKlasy[0]._mu;
                for (int i = 1; i < progiKlasy.liczbaPrzedziałow; i++)
                    if (progiKlasy[i]._mu < _muMin)
                        _muMin = progiKlasy[i]._mu;
                return _muMin;
            }
        }

        /// <summary>
        /// Podaje wartość at obliczoną na podstawie: średniego ruchu oferowanego na PJP, proporcji ruchu klas, i przedziału progowego w zależności od stanu zajętości systemu
        /// </summary>
        /// <param name="stan">Stan zajętości systemu</param>
        /// <returns>at dla systemów progowych</returns>
        public double atProgi(int stan)
        {
            return progiKlasy.Przedzial(stan).mnoznikIntensywnosci * _lambdaZero * progiKlasy.Przedzial(stan).t / progiKlasy.Przedzial(stan)._mu;
        }

        /// <summary>
        /// Podaje wartość at obliczoną na podstawie: średniego ruchu oferowanego na PJP,
        /// proporcji ruchu klas, i przedziału progowego w zależności od przedziału.
        /// </summary>
        /// <param name="stan">Stan zajętości systemu</param>
        /// <param name="nrPrzedz">Numer progu</param>
        /// <returns>at dla systemów progowych</returns>
        public double atProgiPrzedzialu(int nrPrzedz)
        {
            return progiKlasy[nrPrzedz].mnoznikIntensywnosci * _lambdaZero * progiKlasy[nrPrzedz].t / progiKlasy[nrPrzedz]._mu;
        }

        #endregion progi

        #region Zależność od liczby obsługiwanych zgłoszeń
        
        /// <summary>
        /// Zależność procesu napływania zgłoszeń od stanu
        /// </summary>
        /// <param name="y">liczba obsługiwanych zgłoszeń (zależy ona od stanu, ale nie wynika z niego bezpośrednio)</param>
        /// <returns>sigma_T</returns>
        public abstract double sigmaZgl(double y);
        
        public virtual double PodajIntZgl(int lObslZgl) { return 0; }
        
        public virtual double PodajIntZgl(double lObslZgl) { return 0; }

        /// <summary>
        /// Podaje średnią liczbę obsługiwanych zgłoszeń
        /// </summary>
        /// <param name="popY">Średnia liczba obsługiwanych zgłoszeń w poprzedzającym stanie</param>
        /// <param name="prPopStanu">Prawdopodobieństwo poprzedzającego stanu</param>
        /// <param name="prStanu">Prawdopodobieństwo stanu, dla którego wyznaczamy średnią liczbę obsługiwanych zgłoszeń</param>
        /// <param name="sigmaPrzyjmZgloszen">Prawdopodobieństwo przyjęcia zgłoszenia w stanie poprzedzającym aktualny stan</param>
        /// <returns>Średnia liczba obsługiwanych zgłoszeń w aktualnym stanie</returns>
        //public abstract double podajY(double popY, double prPopStanu, double prStanu, double sigmaPrzyjmZgloszen);

        #endregion Zależność od liczby obsługiwanych zgłoszeń

        #region Obliczanie rozkładów

        /// <summary>
        /// Wyznacza rozkład stanów
        /// </summary>
        /// <param name="WiazkaKlasy">System dla którego wyznaczany jest rozkład</param>
        /// <param name="procPrzyjmZal">Określa czy proces przyjmowania zgłoszeń ma być zależny od stanu</param>
        /// <returns>Rozkład zajętości systemu</returns>
        public abstract double[] RozkladStanow(Wiazka WiazkaKlasy, bool procPrzyjmZal);

        /// <summary>
        /// Wyznacza rozkład stanów
        /// </summary>
        /// <param name="WiazkaKlasy">System dla którego wyznaczany jest rozkład</param>
        /// <param name="nrProgu">Numer progu, dla któego wyznaczany jest rozkład. Zależy on od całkowitej liczby zajętych PJP</param>
        /// <returns>Rozkład zajętości systemu</returns>
        public abstract double[] RozkladStanow(Wiazka WiazkaKlasy, int nrProgu);

        /// <summary>
        /// Wyznacza rozkład klasy, który jest zależny od zajętości systemu przez pozostałe klasy zgłoszeń. 
        /// Taki rozkład ma sens gdy rozważamy system z procesem przyjmowania zgłoszeń zależnym od stanu.
        /// Prawdopodobieństwa tego rozkłądu są składowymi iloczynu prawdopodobieńśtw mikrostanów, w których
        /// suma indeksów pozostałych wymiarów stanowi zależność rozkładu.
        /// </summary>
        /// <param name="WiazkaKlasy">Wiązka, dla któej jest budowany rozkład</param>
        /// <param name="[]warPrzejscia">Prawdopodobieńśtwa warunkowych przejść</param>
        /// <returns></returns>
        public abstract double[][] ZaleznyRozkladStanow(Wiazka WiazkaKlasy);

        #endregion Obliczanie rozkładów

        #region Symulacja
        public virtual void PrzyjetoZgloszenie() { ;}
        public virtual void ZakonczonoZgloszenie() { ;}

        public virtual void InicjacjaKolejnegoBadaniaSymulacji(SimGroup sWiazka, agenda listaZd, aSimulation algSym, int nrBadania) { ;}
        public void inicjacjaSymulacji(int liczbaBadan)
        {
            symKlasy = new SimClass[liczbaBadan];
        }


        #endregion Symulacja
        
    }

    public class trClassErlang: trClass
    {
        public trClassErlang(Wiazka wiazkaKlasy, int at, int t, double mi, bool uprzyw)
            : base(wiazkaKlasy, at, t, mi, uprzyw)
        {
            typ = typKlasy.ERLANG;
        }
        public override void ObliczParametry(double a, double sAt, int V)
        {
            double TempA = ObliczRuchOferowanyKlasy(a, sAt, V);
            if (progiKlasy == null)
                _lambdaZero = TempA * _mu;
            else
                _lambdaZero = TempA * progiKlasy[0]._mu;
        }
        public override double PodajIntZgl(int lObslZgl)
        { 
            return _lambdaZero;
        }
        public override double PodajIntZgl(double lObslZgl)
        {
            return _lambdaZero;
        }

        public override double sigmaZgl(double y)
        {
            return 1;
        }

        public override double[] RozkladStanow(Wiazka WiazkaKlasy, bool procPrzyjmZal)
        {
            double[] stany = new double[WiazkaKlasy.V+1];
            double suma = 1;
            stany[0] = 1;
            for (int n = 1; n < stany.Length; n++)
                stany[n] = 0;
            for (int n = t; n < stany.Length; n+=t)
            {
                if (procPrzyjmZal == true)
                {
                    stany[n] = stany[n - t] * a * WiazkaKlasy.sigmy[this, n-t] / (n / t);
                }
                else
                {
                    stany[n] = stany[n - t] * a / (n / t);
                }
                suma += stany[n];
            }

            for (int n = 0;  n < stany.Length; n += t)
                stany[n] /= suma;

            return stany;
        }


        public override double[][] ZaleznyRozkladStanow(Wiazka WiazkaKlasy)
        {
            double[][] stany = new double[WiazkaKlasy.V + 1][];
            for (int nPoz = 0; nPoz <= WiazkaKlasy.V; nPoz++)
            {
                int ostInd = WiazkaKlasy.V - nPoz;
                stany[nPoz] = new double[ostInd + 1];
            }


            stany[0][0] = 1;
            double suma = 1;
            for (int n = 1; n <= WiazkaKlasy.V; n++)
                stany[n][0] = 0;

            for (int n = t; n <= WiazkaKlasy.V; n += t)
            {
                stany[n][0] = stany[n - t][0] * a / (n / t) * WiazkaKlasy.sigmy[this, n - t];
                suma += stany[n][0];
            }
            for (int n = 0; n <= WiazkaKlasy.V; n += t)
            {
                stany[n][0] /= suma;
            }

            for (int nPoz = 1; nPoz <= WiazkaKlasy.V; nPoz++)
            {
                int ostInd = WiazkaKlasy.V - nPoz;
                stany[0][nPoz] = 1 / suma;

                for (int n = 1; n <= ostInd; n++)
                    stany[n][nPoz] = 0;
                for (int n = t; n <= ostInd; n += t)
                {
                    stany[n][nPoz] = stany[n - t][nPoz] * a / (n / t) * WiazkaKlasy.sigmy[this, n - t + nPoz];
                }
            }
            return stany;
        }

        public override double[] RozkladStanow(Wiazka WiazkaKlasy, int nrProgu)
        {
            double[] stany = new double[WiazkaKlasy.V + 1];
            double suma = 1;
            stany[0] = 1;
            for (int n = 1; n < stany.Length; n++)
                stany[n] = 0;

            for (int n = progiKlasy[nrProgu].t; n < stany.Length; n += progiKlasy[nrProgu].t)
            {
                stany[n] = stany[n - progiKlasy[nrProgu].t] * (_lambdaZero*progiKlasy[nrProgu].mnoznikIntensywnosci / progiKlasy[nrProgu]._mu) / (n / progiKlasy[nrProgu].t);
                suma += stany[n];
            }

            for (int n = 0; n < stany.Length; n ++)
                stany[n] /= suma;

            return stany;
        }

        //public override double podajY(double popY, double prPopStanu, double prStanu, double sigmaPrzyjmZgloszen)
        //{
        //    return prPopStanu * sigmaPrzyjmZgloszen * this.a / prStanu;
        //}


        public override string ToString()
        {
            string wynik = ToStringBezProgow();
            wynik += (uprzywilejowana == true) ? "+:" : "";
            if (progiKlasy != null)
            {
                int lProgow = progiKlasy.liczbaPrzedziałow - 1;
                wynik += (" progi: " + lProgow.ToString());
            }
            return wynik;
        }

        public override string ToStringBezProgow()
        {
            string wynik = "Erlang";
            wynik += (uprzywilejowana == true) ? "+:" : "";
            wynik += "\tat " + atProp.ToString();

            if (progiKlasy != null)
            {
                if (tMin != tMax)
                    wynik += string.Format(", t ({0} - {1})", tMin, tMax);
                else
                    wynik += string.Format(", t {0}", progiKlasy.Przedzial(0).t);

                if (muMin != muMax)
                    wynik += string.Format(", µ ({0} - {1})", muMin, muMax);
                else
                    wynik += string.Format(", µ {0}", progiKlasy.Przedzial(0)._mu);
            }
            else
            {
                wynik += ", t " + t.ToString() + ", µ " + _mu.ToString();
            }
            return wynik;
        }

        public override string ToSkrString()
        {
            string wynik = "Erlang";
            wynik += (uprzywilejowana == true) ? "+ " : " ";
            if (progiKlasy != null)
            {
                int lProgow = progiKlasy.liczbaPrzedziałow - 1;
                wynik += (" pr: " +  lProgow.ToString());
            }
            else
                wynik += (atProp.ToString() + ", " + t.ToString() + ", " + _mu.ToString());
            return wynik;
        }

        public override string ToSkrStringFS()
        {
            string wynik = "Er";
            wynik += (uprzywilejowana == true) ? "+" : "-";
            wynik += ("t" + t.ToString());
            return wynik;
        }

        public override void InicjacjaKolejnegoBadaniaSymulacji(SimGroup sWiazka, agenda listaZd, aSimulation algSym, int nrBadania)
        {
            symKlasy[nrBadania] = new SimClassErlang(this, sWiazka, listaZd, algSym);
        }
    }  
    public class trClassEngset : trClass
    {
        private double _gamma;
        private int _S;
        public trClassEngset(Wiazka wiazkaKlasy, int at, int t, double mi, bool uprzyw, int S)
            : base(wiazkaKlasy, at, t, mi, uprzyw)
        {
            this._S = S;
            typ = typKlasy.ENGSET;
        }
        public override void ObliczParametry(double a, double sAt, int V)
        {
            double TempA = ObliczRuchOferowanyKlasy(a, sAt, V);
            if (progiKlasy == null)
                _lambdaZero = TempA * _mu;
            else
                _lambdaZero = TempA * progiKlasy[0]._mu;
            _gamma = _lambdaZero / _S;
        }

        public override double sigmaZgl(double y)
        {
            if (S > y)
                return (double)(S-y) / (double)S;
            return 0;
        }

        public override int S
        {
            get
            {
                return _S;
            }
            set
            {
                _S = value;
            }
        }

        public override double PodajIntZgl(int lObslZgl)
        {
            if (S - lObslZgl > 0)
                return (S - lObslZgl) * _gamma;
            return 0;
        }
        public override double PodajIntZgl(double lObslZgl)
        {
            if (S - lObslZgl > 0)
                return (S - lObslZgl) * _gamma;
            return 0;
        }

        public override double[] RozkladStanow(Wiazka WiazkaKlasy, bool procPrzyjmZal)
        {
            double[] stany = new double[WiazkaKlasy.V + 1];
            double suma = 1;
            stany[0] = 1;
            for (int n = 1; n <= WiazkaKlasy.V; n++)
                stany[n] = 0;
            for (int n = t; n <= WiazkaKlasy.V; n += t)
            {
                if ((int)(S - (n / t)) >= 0)
                {
                    if (procPrzyjmZal == true)
                    {
                        stany[n] = stany[n - t] * _gamma / _mu * WiazkaKlasy.sigmy[this, n-t] * (S + 1 - (n / t)) / (n / t);
                    }
                    else
                    {
                        stany[n] = stany[n - t] * _gamma / _mu * (S + 1 - (n / t)) / (n / t);
                    } suma += stany[n];
                }
                else
                    stany[n] = 0;
            }
            for (int n = 0; n <= WiazkaKlasy.V; n += t)
                stany[n] /= suma;
            return stany;
        }


        public override double[][] ZaleznyRozkladStanow(Wiazka WiazkaKlasy)
        {
            double[][] stany = new double[WiazkaKlasy.V + 1][];
            for (int nPoz = 0; nPoz < WiazkaKlasy.V; nPoz++)
            {
                int OstInd = WiazkaKlasy.V - nPoz;
                stany[nPoz] = new double[OstInd + 1];

                double suma = 1;
                stany[0][nPoz] = 1;
                for (int n = 1; n <= OstInd; n++)
                    stany[n][nPoz] = 0;
                for (int n = t; n <= OstInd; n += t)
                {
                    if ((int)(S - (n / t)) >= 0)
                    {
                        stany[n][nPoz] = stany[n - t][nPoz] * _gamma / _mu * (S + 1 - (n / t)) / (n / t) * WiazkaKlasy.sigmy[this, n - t + nPoz];
                        suma += stany[n][nPoz];
                    }
                    else
                        stany[n][nPoz] = 0;
                }
                for (int n = 0; n <= OstInd; n += t)
                    stany[n][nPoz] /= suma;
            }
            return stany;
        }


        public override double[] RozkladStanow(Wiazka WiazkaKlasy, int nrProgu)
        {
            double gamma2 = _gamma * progiKlasy[nrProgu].mnoznikIntensywnosci;
            double[] stany = new double[WiazkaKlasy.V + 1];
            double suma = 1;
            stany[0] = 1;
            for (int n = 1; n < stany.Length; n++)
                stany[n] = 0;

            for (int n = progiKlasy[nrProgu].t; n < stany.Length; n += progiKlasy[nrProgu].t)
            {
                stany[n] = stany[n - progiKlasy[nrProgu].t] * (gamma2 * (S + 1 - (n / progiKlasy[nrProgu].t))) / (progiKlasy[nrProgu]._mu * (n / progiKlasy[nrProgu].t));
                suma += stany[n];
            }

            for (int n = 0; n < stany.Length; n+=progiKlasy[nrProgu].t)
                stany[n] /= suma;

            return stany;
        }

        public override string ToString()
        {
            string wynik = ToStringBezProgow();
            if (progiKlasy != null)
            {
                int lProg = progiKlasy.liczbaPrzedziałow - 1;
                wynik += ("progi: " + lProg.ToString());
            }
            return wynik;
        }

        public override string ToStringBezProgow()
        {
            string wynik = "Engset";
            wynik += (uprzywilejowana == true) ? "+:" : "";
            if (progiKlasy == null)
            {
                wynik += "\tat " + atProp.ToString() + ", t " + t.ToString() + ", µ " + _mu.ToString() + ", S " + S;
            }
            else
            {
                wynik += "\tat " + atProp.ToString();
                if (tMin != tMax)
                    wynik += string.Format(", t ({0} - {1})", tMin, tMax);
                else
                    wynik += string.Format(", t {0}", progiKlasy.Przedzial(0).t);

                if (muMin != muMax)
                    wynik += string.Format(", µ ({0} - {1})", muMin, muMax);
                else
                    wynik += string.Format(", µ {0}", progiKlasy.Przedzial(0)._mu);

                wynik += string.Format(", S {0}", S);
                //progiKlasy
            }
            return wynik;
        }

        public override string ToSkrString()
        {
            string wynik = "Engset";
            wynik += (uprzywilejowana == true) ? "+ " : " ";
            if (progiKlasy != null)
            {
                int lProgow = progiKlasy.liczbaPrzedziałow - 1;
                wynik += (" S=" + S.ToString() + " pr: " + lProgow.ToString());
            }
            else
                wynik += (atProp.ToString() + "," + t.ToString() + "," + _mu.ToString() + "," + S.ToString());
            return wynik;
        }

        public override string ToSkrStringFS()
        {
            string wynik = "En";
            wynik += (uprzywilejowana == true) ? "+" : "-";
            if (progiKlasy != null)
            {
                int lProgow = progiKlasy.liczbaPrzedziałow - 1;
                wynik += (" S=" + S.ToString() + " pr: " + lProgow.ToString());
            }
            else
                wynik += ("t" + t.ToString() + ".S" + S.ToString());
            return wynik;
        }

        public override void InicjacjaKolejnegoBadaniaSymulacji(SimGroup sWiazka, agenda listaZd, aSimulation algSym, int nrBadania)
        {
            symKlasy[nrBadania] = new SimClassEngset(this, sWiazka, listaZd, algSym);
        }
    }
    public class trClassPascal : trClass
    {
        private double _gamma;
        private int _S;
        public trClassPascal(Wiazka wiazkaKlasy, int at, int t, double mi, bool uprzyw, int S)
            : base(wiazkaKlasy, at, t, mi, uprzyw)
        {
            typ = typKlasy.PASCAL;
            this._S = S;
        }
        public override void ObliczParametry(double a, double sAt, int V)
        {
            double TempA = ObliczRuchOferowanyKlasy(a, sAt, V);
            if (progiKlasy == null)
                _lambdaZero = TempA * _mu;
            else
                _lambdaZero = TempA * progiKlasy[0]._mu;
            _gamma = _lambdaZero / _S;
        }
        public override int S
        {
            get
            {
                return _S;
            }
            set
            {
                _S = value;
            }
        }
        public override double sigmaZgl(double y)
        {
            return (double)(S + y) / (double)S;
        }

        public override double PodajIntZgl(int lObslZgl)
        {
            return (_S + lObslZgl) * _gamma;
        }
        public override double PodajIntZgl(double lObslZgl)
        {
            return (_S + lObslZgl) * _gamma;
        }

        public override double[] RozkladStanow(Wiazka WiazkaKlasy, bool procPrzyjmZal)
        {
            double[] stany = new double[WiazkaKlasy.V + 1];
            double suma = 1;
            stany[0] = 1;
            for (int n = 1; n < stany.Length; n++)
                stany[n] = 0;
            int lZajPJP = 0;
            for (int n = t; n < stany.Length; n += t)
            {
                if (procPrzyjmZal == true)
                {
                    stany[n] = stany[n - t] * _gamma / _mu * WiazkaKlasy.sigmy[this, n-t] * (_S + lZajPJP) / (lZajPJP + 1);
                }
                else
                {
                    stany[n] = stany[n - t] * _gamma / _mu * (_S + lZajPJP) / (lZajPJP + 1);
                }
                suma += stany[n];
                lZajPJP++;
            }

            for (int n = 0; n < stany.Length; n += t)
                stany[n] /= suma;

            return stany;
        }

        public override double[][] ZaleznyRozkladStanow(Wiazka WiazkaKlasy)
        {
            double[][] stany = new double[WiazkaKlasy.V + 1][];
            for (int nPoz = 0; nPoz <= WiazkaKlasy.V; nPoz++)
            {
                int ostInd = WiazkaKlasy.V - nPoz;
                stany[nPoz] = new double[ostInd + 1];

                stany[0][nPoz] = 1;
                double suma = 1;

                for (int n = 1; n <= ostInd; n++)
                    stany[n][nPoz] = 0;
                int lZajPJP = 0;
                for (int n = t; n <= ostInd; n += t)
                {
                    stany[n][nPoz] = stany[n - t][nPoz] * _gamma / _mu * (_S + lZajPJP) / (lZajPJP + 1) * WiazkaKlasy.sigmy[this, n - t + nPoz];
                    suma += stany[n][nPoz];
                    lZajPJP++;
                }

                for (int n = 0; n <= ostInd; n += t)
                    stany[n][nPoz] /= suma;
            }
            return stany;
        }

        public override double[] RozkladStanow(Wiazka WiazkaKlasy, int nrProgu)
        {
            double gamma2 = _gamma * progiKlasy[nrProgu].mnoznikIntensywnosci;
            double[] stany = new double[WiazkaKlasy.V + 1];
            double suma = 1;
            stany[0] = 1;
            for (int n = 1; n < stany.Length; n++)
                stany[n] = 0;

            int lZajPJP = 0;
            for (int n = progiKlasy[nrProgu].t; n < stany.Length; n += progiKlasy[nrProgu].t)
            {
                stany[n] = stany[n - progiKlasy[nrProgu].t] * gamma2 * (_S + lZajPJP) / (progiKlasy[nrProgu]._mu * (lZajPJP + 1));
                suma += stany[n];
                lZajPJP++;
            }

            for (int n = 0; n < stany.Length; n+=progiKlasy[nrProgu].t)
                stany[n] /= suma;

            return stany;
        }

        public override string ToString()
        {
            string wynik = ToStringBezProgow();
            if (progiKlasy != null)
            {
                int lProg = progiKlasy.liczbaPrzedziałow - 1;
                wynik += " progi: " + lProg.ToString();
            }
            return wynik;
        }

        public override string ToStringBezProgow()
        {
            string wynik = "Pascal";
            wynik += (uprzywilejowana == true) ? "+:" : "";
            wynik += "\tat " + atProp.ToString();

            if (progiKlasy != null)
            {
                if (tMin != tMax)
                    wynik += string.Format(", t ({0} - {1})", tMin, tMax);
                else
                    wynik += string.Format(", t {0}", progiKlasy.Przedzial(0).t);

                if (muMin != muMax)
                    wynik += string.Format(", µ ({0} - {1})", muMin, muMax);
                else
                    wynik += string.Format(", µ {0}", progiKlasy.Przedzial(0)._mu);
            }
            else
            {
                wynik += ", t " + t.ToString() + ", µ " + _mu.ToString();
            }
            wynik += ", S " + _S;
            return wynik;
        }

        public override string ToSkrString()
        {
            string wynik = "Pascal";
            wynik += (uprzywilejowana == true) ? "+ " : " ";
            if (progiKlasy != null)
            {
                int lProg = progiKlasy.liczbaPrzedziałow - 1;
                wynik += (" S=" + _S.ToString() + " pr: " + lProg.ToString());
            }
            else
                wynik += (atProp.ToString() + "," + t.ToString() + "," + _mu.ToString() + "," + _S.ToString());
            return wynik;
        }

        public override string ToSkrStringFS()
        {
            string wynik = "Ps";
            wynik += (uprzywilejowana == true) ? "+" : "-";
            wynik += ("t" + t.ToString() + ".N" + S.ToString());
            return wynik;
        }
        public override void InicjacjaKolejnegoBadaniaSymulacji(SimGroup sWiazka, agenda listaZd, aSimulation algSym, int nrBadania)
        {
            symKlasy[nrBadania] = new SimClassPascal(this, sWiazka, listaZd, algSym);
        }
    }
}
