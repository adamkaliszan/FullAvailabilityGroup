using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Distributions;

namespace Algorithms
{
    /// <summary>
    /// Zależność procesu przyjmowania zgloszen od stanu
    /// </summary>
    public class sigmaPrzyjmZgl
    {
        double[,] sigmy;
        double[,] sigmyStr;     //Współczynniki sigma wynikające ze struktury systemu
        double[,] sigmyPol;     //Współczynniki sigma wynikające z polityki przyjmowania nowych zgłoszeń;

        public string wypiszSigmaPolPrzyjmowaniaZlg()
        {
            string wynik = "";
            if (obliczone == false)
                obliczSigmy();

            for (int i=0; i<aWiazka.m; i++)
            {
                wynik += string.Format("Klasa {0}", aWiazka.ListaKlasRuchu[i].ToStringBezProgow());
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    wynik += string.Format("\t{0}", sigmyPol[i, n]);
                }
                wynik += "\n";
            }
            return wynik;
        }

        public string wypiszSigmaStrukturySystemu()
        {
            string wynik = "";
            if (obliczone == false)
                obliczSigmy();

            for (int i = 0; i < aWiazka.m; i++)
            {
                string wiersz = string.Format("Klasa {0}", aWiazka.ListaKlasRuchu[i].ToStringBezProgow());
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    wiersz += string.Format("\t{0}", sigmyStr[i, n]);
                }
                wynik += (wiersz + "\n");
            }
            return wynik;
        }

        Wiazka aWiazka;
        bool obliczone;

        public sigmaPrzyjmZgl(Wiazka mWiazka)
        {
            aWiazka = mWiazka;
            obliczone = false;
        }

        #region ObliczSigmy
        public void obliczSigmy()
        {
            sigmy = new double[aWiazka.m, aWiazka.V + 1];

            //Wiązka pełnodostępna
            if (aWiazka.sumaK == 1)
                sigmyStr = obliczSigmyFAG();
            else
            {
                if (aWiazka.liczbaKlasPodgrup == 1)
                    sigmyStr = obliczSigmyOgrDost();
                else
                    sigmyStr = obliczSigmyOgrDostUogolniony();
            }

            switch (aWiazka.AlgorytmRezerwacji)
            {
                case reservationAlgorithm.none:
                    sigmyPol = obliczSigmyFAG();
                    break;
                case reservationAlgorithm.R1_R2:
                    sigmyPol = obliczSigmyR1_R2();
                    break;
                default:
                    sigmyPol = obliczSigmyFAG();
                    break;
            }

            for (int i = 0; i < aWiazka.m; i++)
                for (int n = 0; n <= aWiazka.V; n++)
                    sigmy[i, n] = sigmyPol[i, n] * sigmyStr[i, n];
            obliczone = true;
        }
        private double[,] obliczSigmyFAG()
        {
            double[,] sigmyTemp = new double[aWiazka.m, aWiazka.V + 1];
            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass klasaTemp = aWiazka.ListaKlasRuchu[i];
                if (klasaTemp.progiKlasy == null)
                {
                    for (int n = 0; n <= aWiazka.V; n++)
                    {
                        if (aWiazka.V - n >= klasaTemp.t)
                            sigmyTemp[i, n] = 1;
                        else
                            sigmyTemp[i, n] = 0;
                    }
                }
                else
                {
                    for (int n = 0; n <= aWiazka.V; n++)
                    {
                        if (aWiazka.V - n >= klasaTemp.progiKlasy.Przedzial(n).t)
                            sigmyTemp[i, n] = 1;
                        else
                            sigmyTemp[i, n] = 0;
                    }
                }
            }
            return sigmyTemp;
        }
        private double[,] obliczSigmyR1_R2()                        //W tym mechaniźmie wyznaczono obszar rezerwacji. Zależy on stanu zajętości całego systemu.
        {
            double[,] sigmyTemp = new double[aWiazka.m, aWiazka.V + 1];
            for (int i = 0; i < aWiazka.m; i++)
            {
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    if ((aWiazka.V - n >= aWiazka.ListaKlasRuchu[i].t) && ((n <= aWiazka.q) || (aWiazka.ListaKlasRuchu[i].uprzywilejowana)))
                        sigmyTemp[i, n] = 1;
                    else
                        sigmyTemp[i, n] = 0;
                }
            }
            return sigmyTemp;
        }
        private double[,] obliczSigmyOgrDost()
        {
            double[,] sigmyTemp = new double[aWiazka.m, aWiazka.V + 1];
            dwumianNewtona dwumian = new dwumianNewtona(aWiazka.V);
            int k = aWiazka.sumaK;
            for (int i = 0; i < aWiazka.m; i++)
            {
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    int t = (aWiazka.ListaKlasRuchu[i].progiKlasy == null) ? aWiazka.ListaKlasRuchu[i].t : aWiazka.ListaKlasRuchu[i].progiKlasy.Przedzial(n).t;
                    int x = aWiazka.V - n;
                    if (x > (t - 1) * k)
                        sigmyTemp[i, n] = 1;
                    else
                        sigmyTemp[i, n] = 1 - dwumian.F(x, k, t - 1, 0) / dwumian.F(x, k, aWiazka.ListaPodgrupLaczy[0].v, 0);
                }
            }
            return sigmyTemp;
        }
        private double[,] obliczSigmyOgrDostUogolniony()
        {
            double[,] sigmyTemp = new double[aWiazka.m, aWiazka.V + 1];
            dwumianNewtona dwumian = new dwumianNewtona(1);
            for (int i = 0; i < aWiazka.m; i++)
            {
                int t = aWiazka.ListaKlasRuchu[i].t;
                Rozklad[] rozkladyAlfa = new Rozklad[aWiazka.ListaPodgrupLaczy.Count];
                Rozklad[] rozkladyBeta = new Rozklad[aWiazka.ListaPodgrupLaczy.Count];
                for (int nrPodgr = 0; nrPodgr < aWiazka.ListaPodgrupLaczy.Count; nrPodgr++)
                {

                    int v = aWiazka.ListaPodgrupLaczy[nrPodgr].v;
                    int k = aWiazka.ListaPodgrupLaczy[nrPodgr].k;
                    double[] stanyAlfa = new double[v + 1];
                    double[] stanyBeta = new double[v + 1];
                    for (int l = 0; l <= v; l++)
                    {
                        stanyAlfa[l] = dwumian.F(l, k, v, 0);
                        stanyBeta[l] = dwumian.F(l, k, t - 1, 0);
                    }
                    rozkladyAlfa[nrPodgr] = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[0], stanyAlfa, v);
                    rozkladyBeta[nrPodgr] = new Rozklad(aWiazka, aWiazka.ListaKlasRuchu[0], stanyBeta, v);
                }
                Rozklad rAlfa = new Rozklad(rozkladyAlfa[0]);
                Rozklad rBeta = new Rozklad(rozkladyBeta[0]);

                for (int j = 1; j < aWiazka.ListaPodgrupLaczy.Count; j++)
                {
                    rAlfa = rAlfa * rozkladyAlfa[j];
                    rBeta = rBeta * rozkladyBeta[j];
                }
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    if ((aWiazka.V - aWiazka.sumaK * (aWiazka.tMax - 1) > n) || (rAlfa[aWiazka.V - n] == 0))
                        sigmyTemp[i, n] = 1;
                    else
                        sigmyTemp[i, n] = (rAlfa[aWiazka.V - n] - rBeta[aWiazka.V - n]) / rAlfa[aWiazka.V - n];
                }
            }
            return sigmyTemp;
        }
        #endregion

        /// <summary>
        /// Zwraca zależność procesu obsługi od stanu
        /// </summary>
        /// <param name="nrKlasy">Numer klasy</param>
        /// <param name="nrStanu">Stan zajętości systemu</param>
        /// <returns></returns>
        public double this[int nrKlasy, int nrStanu]
        {
            get
            {
                if (obliczone == false)
                    obliczSigmy();
                if ((nrKlasy > aWiazka.m) || (nrKlasy < 0))
                    throw new InvalidCastException("Zly indeks Klasy");
                if (nrStanu > aWiazka.V+1)
                    return 0;
                if (nrStanu < 0)
                    return 1;
                return sigmy[nrKlasy, nrStanu];
            }
            set
            {
                throw new InvalidCastException("Nie można ustawiać sigm ręcznie");
            }
        }

        public double this[trClass klasa, int nrStanu]
        {
            get
            {
                if (obliczone == false)
                    obliczSigmy();
                if (aWiazka.ListaKlasRuchu.Contains(klasa) == false)
                    throw new InvalidCastException(string.Format("Wiazka nie zawiera takiej klasy: {0}", klasa));
                if (nrStanu > aWiazka.V + 1)
                    return 0;
                if (nrStanu < 0)
                    return 1;
                return sigmy[aWiazka.ListaKlasRuchu.IndexOf(klasa), nrStanu];
            }
            set
            {
                throw new InvalidCastException("Nie można ustawiać sigm ręcznie");
            }
        }
    }

    public class delta
    {
        sigmaPrzyjmZgl sStruktury;
        bool oszacowana;
        double[] _delta;
        Wiazka aWiazka;
        liczbaY _Y;

        public liczbaY Y
        {
            get { return _Y; }
            //set { _Y = value; }
        }


        public delta(Wiazka mWiazka)
        {
            aWiazka = mWiazka;
            sStruktury = new sigmaPrzyjmZgl(mWiazka);
            oszacowana = false;
            _Y = new liczbaY(mWiazka);
        }

        public sigmaPrzyjmZgl sigmaStruktury
        {
            get { return sStruktury; }
        }

        public void kasujSigmy()
        {
            oszacowana = false;
        }

        public double Oblicz(Rozklad P)
        {
            _Y.Inicjalizacja();
            
            _delta = new double[aWiazka.V + 1];
            oszacowana = true;
            sStruktury.obliczSigmy();
            
            for (int n = 0; n <= aWiazka.V; n++)
            {
                _delta[n] = 0;
                double mian = 0;
                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass klasaTemp = aWiazka.ListaKlasRuchu[i];
                    if (klasaTemp.progiKlasy == null)
                    {
                        int t = klasaTemp.t;
                        if (n - t >= 0)
                        {
                            _delta[n] += (_delta[n - t] * klasaTemp.at * sStruktury[i, n - t] * P[n - t]);
                            mian += (klasaTemp.at * P[n - t]);
                        }
                        else
                        {
                            _delta[n] += (klasaTemp.at);
                            mian += (klasaTemp.at);
                        }
                    }
                    else
                    {
                        for (int nrPrzedz = 0; nrPrzedz < klasaTemp.progiKlasy.liczbaPrzedziałow; nrPrzedz++)
                        {
                            int t = klasaTemp.progiKlasy[nrPrzedz].t;
                            double at = klasaTemp.atProgiPrzedzialu(nrPrzedz);
                            if (n - t >= 0)
                            {
                                //jeśli przedziały są różne, to dla tego przedziału nie ma przejścia
                                if (klasaTemp.progiKlasy.nrPrzedzialu(n-t)==nrPrzedz)
                                    _delta[n] += (_delta[n - t] * at * sStruktury[i, n - t] * P[n - t]);
                                mian += (at * P[n - t]);
                            }
                            else
                            {
                                if (klasaTemp.progiKlasy.nrPrzedzialu(n-t) == nrPrzedz)
                                    _delta[n] += (at);
                                mian += (at);
                            }
                        }
                    }
                }
                if (mian != 0)
                    _delta[n] /= mian;
                else
                    _delta[n] = 0;
            }
            return _Y.epsilon;
        }
        public double ObliczY(Rozklad R)
        {
            if (oszacowana == false)
            {
                _delta = new double[aWiazka.V + 1];
                sStruktury.obliczSigmy();
                oszacowana = true;
            }
            _Y.ObliczWartosciHybr(R, sStruktury);
            return _Y.epsilon;
        }

        public void ObliczDeltaZy(Rozklad P)
        {
            if (oszacowana == false)
            {
                _delta = new double[aWiazka.V + 1];
                sStruktury.obliczSigmy();
                oszacowana = true;
            }
            if (_Y == null)
            {
                throw (new Exception("Brak Y"));
            }

            for (int n = 0; n <= aWiazka.V; n++)
            {
                _delta[n] = 0;
                double mian = 0;
                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass klasaTemp = aWiazka.ListaKlasRuchu[i];
                    if (klasaTemp.progiKlasy == null)
                    {
                        int t = aWiazka.ListaKlasRuchu[i].t;
                        if (n - t >= 0)
                        {
                            _delta[n] += (_delta[n - t] * klasaTemp.at * klasaTemp.sigmaZgl(_Y[i, n - t]) * sStruktury[i, n - t] * P[n - t]);
                            mian += (klasaTemp.at * klasaTemp.sigmaZgl(_Y[i, n - t]) * P[n - t]);
                        }
                        else
                        {
                            _delta[n] += (klasaTemp.at);
                            mian += (klasaTemp.at);
                        }
                    }
                    else
                    {
                        for (int nrPrzedz = 0; nrPrzedz < klasaTemp.progiKlasy.liczbaPrzedziałow; nrPrzedz++)
                        {
                            int t = klasaTemp.progiKlasy[nrPrzedz].t;
                            double at = klasaTemp.atProgiPrzedzialu(nrPrzedz);
                            if (n - t >= 0)
                            {
                                //jeśli przedziały są różne, to dla tego przedziału nie ma przejścia
                                double sigma_T = klasaTemp.sigmaZgl(_Y[i, n - t]);
                                double sigma_S = sStruktury[i, n - t];
                                if (klasaTemp.progiKlasy.nrPrzedzialu(n - t) == nrPrzedz)
                                    _delta[n] += (_delta[n - t] * at * sigma_T * sigma_S * P[n - t]);
                                mian += (at * P[n - t] * sigma_T);
                            }
                            else
                            {
                                if (klasaTemp.progiKlasy.nrPrzedzialu(n - t) == nrPrzedz)
                                    _delta[n] += (at);
                                mian += (at);
                            }
                        }
                    }
                }
                if (mian != 0)
                    _delta[n] /= mian;
                else
                    _delta[n] = 1;
            }
        }

        public void ObliczYspl(Rozklad[] P_minusI, Rozklad[] p, Rozklad P)
        {
            _Y.Inicjalizacja();
            _Y.ObliczWartossciZeSplotu(P_minusI, p);

            if (oszacowana == false)
            {
                _delta = new double[aWiazka.V + 1];
                sStruktury.obliczSigmy();
                oszacowana = true;
            }
            double licznik;
            double mianownik;
            double sigmaZgl;
            double sigmaStr;
            for (int n = 0; n <= aWiazka.V; n++)
            {
                mianownik = 0;
                licznik = 0;
                for (int i = 0; i < aWiazka.m; i++)
                {
                    trClass klasa = aWiazka.ListaKlasRuchu[i];
                    if (klasa.progiKlasy == null)
                    {
                        int t = klasa.t;
                        if (n - t > 0)
                        {
                            sigmaZgl = klasa.sigmaZgl(_Y[i, n - t]);
                            sigmaStr = sStruktury[i, n - t];

                            licznik += (_delta[n - t] * klasa.at * sigmaZgl * sigmaStr * P[n - t]);
                            mianownik += (aWiazka.ListaKlasRuchu[i].at * P[n - t] * sigmaZgl);
                        }
                        else
                        {
                            licznik += (aWiazka.ListaKlasRuchu[i].at);
                            mianownik += (aWiazka.ListaKlasRuchu[i].at);
                        }
                    }
                    else
                    {
                        int t = klasa.progiKlasy.Przedzial(n).t;
                        if (n - t > 0)
                        {
                            licznik += (_delta[n - t] * klasa.atProgi(n-t) * klasa.sigmaZgl(_Y[i, n - t]) * sStruktury[i, n - t] * P[n - t]);
                            mianownik += (klasa.atProgi(n-t) * P[n - t] * klasa.sigmaZgl(_Y[i, n - t]));
                        }
                        else
                        {
                            licznik += (klasa.atProgi(n-t));
                            mianownik += (klasa.atProgi(n-t));
                        }
                    }
                }
                _delta[n] = licznik / mianownik;
                if (mianownik == 0)
                    _delta[n] = 1;
            }

        }

        public double this[int n]
        {
            get
            {
                if (oszacowana == false)
                    return -1;
                if ((n >= 0) && (n <= aWiazka.V))
                    return _delta[n];
                if (n < 0)
                    return 1;
                if (n > aWiazka.V)
                    return 0;
                return -1;
            }
            set
            {
                if ((n >= 0) && (n <= aWiazka.V))
                    _delta[n] = value;
            }
        }
    }

    public class liczbaY
    {
        public double[,] y;
        Wiazka aWiazka;
        bool zainicjalizowany;
        double maksBladY;

        public liczbaY(Wiazka mWiazka)
        {
            aWiazka = mWiazka;
            zainicjalizowany = false;
            maksBladY = 1;
        }

        public override string ToString()
        {
            string wynik="";
            if (y != null)
            {
                for (int wiersz = 0; wiersz < aWiazka.m; wiersz++)
                {
                    for (int n = aWiazka.V-4; n < aWiazka.V; n++)
                        wynik += string.Format("{0} ", y[wiersz, n]);
                    wynik += string.Format("{0}\n", y[wiersz, aWiazka.V].ToString());
                }
            }
            else
            {
                wynik = "null";
            }
            return wynik;
        }

        public void Inicjalizacja()
        {
            y = new double[aWiazka.m, aWiazka.V + 1];
            for (int i = 0; i < aWiazka.m; i++)
                for (int n = 0; n <= aWiazka.V; n++)
                    y[i, n] = 0;
            zainicjalizowany = true;
        }

        public void ObliczWartossciZeSplotu(Rozklad[] P_minusI, Rozklad[] p)
        {
            for (int i = 0; i < aWiazka.m; i++)
                for (int n = 0; n <= aWiazka.V; n++)
                {
                    double licznik = 0;
                    double mianownik = 0;
                    for (int l = 0; l <= n; l++)
                    {
                        licznik += (l * p[i][l] * P_minusI[i][n - l]);
                        mianownik += (p[i][l] * P_minusI[i][n - l]);
                    }
                    if (mianownik != 0)
                    {
                        if (aWiazka.ListaKlasRuchu[i].progiKlasy == null)
                            y[i, n] = licznik / (mianownik * aWiazka.ListaKlasRuchu[i].t);
                        else
                            y[i, n] = licznik / (mianownik * aWiazka.ListaKlasRuchu[i].progiKlasy.Przedzial(n).t);
                    }
                    else
                        y[i, n] = 0;
                }
        }
        /// <summary>
        /// Wyznacza Y (dla każdej klasy i ze zbioru M średnią liczbę obsługiwanych zgłoszeń
        /// gdy system jest w stanie 0, ..., V. Starsze y_i(n) wyznaczane sa na podstawie y_i(n-t_i)
        /// </summary>
        /// <param name="R">Rozkład zajętości dla systemu z procewsem obsługi i napływania zgłoszeń zależnym od stanu</param>
        /// <param name="sigmyStruktury">Współczynniki warunkowego rpzejścia wynikające ze struktury systemu</param>
        public void ObliczWartosciHybr(Rozklad R, sigmaPrzyjmZgl sigmyStruktury)
        {
            maksBladY = 0;
            double staryY;

            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass tempKlasa = aWiazka.ListaKlasRuchu[i];
                int tStart = (tempKlasa.progiKlasy == null) ? tempKlasa.t : tempKlasa.progiKlasy[0].t;

                //for (int n = aWiazka.V; n >= tStart; n--)
                for (int n = tStart; n <= aWiazka.V; n++)
                {
                    staryY = y[i, n];
                    y[i, n] = 0;
                    if (tempKlasa.progiKlasy == null)
                    {
                        int t = tempKlasa.t;
                        double a = aWiazka.ListaKlasRuchu[i].a;
                        switch (tempKlasa.typ)
                        {
                            case trClass.typKlasy.ERLANG:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                            case trClass.typKlasy.ENGSET:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                            case trClass.typKlasy.PASCAL:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                        }
                    }
                    else
                    {
                        for (int prNr = 0; prNr < tempKlasa.progiKlasy.liczbaPrzedziałow; prNr++)
                        {
                            int t = tempKlasa.progiKlasy[prNr].t;
                            int popStan = n - t;
                            if (tempKlasa.progiKlasy.nrPrzedzialu(popStan) == prNr)
                            {
                                double a = tempKlasa.atProgi(popStan) / t;
                                y[i, n] += R[popStan] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, popStan]) * sigmyStruktury[i, popStan];
                            }
                        }
                    }
                    
                    double bladY = Math.Abs((y[i, n] - staryY) / y[i, n]);
                    if (bladY > maksBladY)
                        maksBladY = bladY;
                }
            }
        }

        /// <summary>
        /// Wyznacza Y (dla każdej klasy i ze zbioru M średnią liczbę obsługiwanych zgłoszeń
        /// gdy system jest w stanie 0, ..., V. y_i(n) wyznaczane sa na podstawie y(n-t_i) orzymanych w poprzedniej iteracji
        /// </summary>
        /// <param name="R">Rozkład zajętości dla systemu z procewsem obsługi i napływania zgłoszeń zależnym od stanu</param>
        /// <param name="sigmyStruktury">Współczynniki warunkowego rpzejścia wynikające ze struktury systemu</param>
        public void ObliczWartosciKR(Rozklad R, sigmaPrzyjmZgl sigmyStruktury)
        {
            maksBladY = 0;
            double staryY;

            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass tempKlasa = aWiazka.ListaKlasRuchu[i];
                int tStart = (tempKlasa.progiKlasy == null) ? tempKlasa.t : tempKlasa.progiKlasy[0].t;

                //for (int n = aWiazka.V; n >= tStart; n--)
                for (int n = tStart; n <= aWiazka.V; n++)
                {
                    staryY = y[i, n];
                    y[i, n] = 0;
                    if (tempKlasa.progiKlasy == null)
                    {
                        int t = tempKlasa.t;
                        double a = aWiazka.ListaKlasRuchu[i].a;
                        switch (tempKlasa.typ)
                        {
                            case trClass.typKlasy.ERLANG:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                            case trClass.typKlasy.ENGSET:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                            case trClass.typKlasy.PASCAL:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                        }
                    }
                    else
                    {
                        for (int prNr = 0; prNr < tempKlasa.progiKlasy.liczbaPrzedziałow; prNr++)
                        {
                            int t = tempKlasa.progiKlasy[prNr].t;
                            int popStan = n - t;
                            if (tempKlasa.progiKlasy.nrPrzedzialu(popStan) == prNr)
                            {
                                double a = tempKlasa.atProgi(popStan) / t;
                                y[i, n] += R[popStan] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, popStan]) * sigmyStruktury[i, popStan];
                            }
                        }
                    }

                    double bladY = Math.Abs((y[i, n] - staryY) / y[i, n]);
                    if (bladY > maksBladY)
                        maksBladY = bladY;
                }
            }
        }

        public void ObliczWartosciBF(Rozklad R, sigmaPrzyjmZgl sigmyStruktury)
        {
            maksBladY = 0;
            double staryY;

            for (int i = 0; i < aWiazka.m; i++)
            {
                trClass tempKlasa = aWiazka.ListaKlasRuchu[i];
                int tStart = (tempKlasa.progiKlasy == null) ? tempKlasa.t : tempKlasa.progiKlasy[0].t;

                //for (int n = aWiazka.V; n >= tStart; n--)
                for (int n = tStart; n <= aWiazka.V; n++)
                {
                    staryY = y[i, n];
                    y[i, n] = 0;
                    if (tempKlasa.progiKlasy == null)
                    {
                        int t = tempKlasa.t;
                        double a = aWiazka.ListaKlasRuchu[i].a;
                        switch (tempKlasa.typ)
                        {
                            case trClass.typKlasy.ERLANG:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                            case trClass.typKlasy.ENGSET:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                            case trClass.typKlasy.PASCAL:
                                y[i, n] = R[n - t] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, n - t]) * sigmyStruktury[i, n - t];
                                break;
                        }
                    }
                    else
                    {
                        for (int prNr = 0; prNr < tempKlasa.progiKlasy.liczbaPrzedziałow; prNr++)
                        {
                            int t = tempKlasa.progiKlasy[prNr].t;
                            int popStan = n - t;
                            if (tempKlasa.progiKlasy.nrPrzedzialu(popStan) == prNr)
                            {
                                double a = tempKlasa.atProgi(popStan) / t;
                                y[i, n] += R[popStan] / R[n] * a * aWiazka.ListaKlasRuchu[i].sigmaZgl(y[i, popStan]) * sigmyStruktury[i, popStan];
                            }
                        }
                    }

                    double bladY = Math.Abs((y[i, n] - staryY) / y[i, n]);
                    if (bladY > maksBladY)
                        maksBladY = bladY;
                }
            }
        }

        public double this[int nrKlasy, int stan]
        {
            get 
            {
                if (zainicjalizowany == false)
                    return -1;
                if ((nrKlasy < 0) || (nrKlasy > aWiazka.m))
                    throw new InvalidCastException("Zły numer klasy");
                if ((stan < 0) || (stan > aWiazka.V + 1))
                    throw new InvalidCastException("Zły numer stan"); 
                return y[nrKlasy, stan]; 
            }
        }

        public double epsilon
        {
            get { return maksBladY; }
        }
    }
}
