using System;
using System.Collections.Generic;
using System.Text;
using Algorithms;
using ModelGroup;

namespace Distributions
{
    public class Rozklad
    {
        protected int _v;
        protected double[] stany;

        /// <summary>
        /// Lista klas ruchu zagregowanych w rozkładzie
        /// </summary>
        public List<trClass> zagregowaneKlasy;

        /// <summary>
        /// Wiązka związana z rozkładem
        /// </summary>
        public Wiazka wiazka;

        #region konstruktory
        public Rozklad()
        {
            stany = null;
            zagregowaneKlasy = new List<trClass>();
        }
        public Rozklad(Wiazka rozWiazka, trClass klZgloszen)
        {
            zagregowaneKlasy = new List<trClass>();
            wiazka = rozWiazka;
            _v = rozWiazka.V;
            if (klZgloszen != null)
            {
                zagregowaneKlasy.Add(klZgloszen);

                if (klZgloszen.progiKlasy == null)
                {
                    stany = klZgloszen.RozkladStanow(rozWiazka, false);
                }
                else
                {
                    Rozklad[] temp = new Rozklad[klZgloszen.progiKlasy.liczbaPrzedziałow];
                    for (int i = 0; i < klZgloszen.progiKlasy.liczbaPrzedziałow; i++)
                    {
                        temp[i] = new Rozklad();
                        temp[i].zagregowaneKlasy = this.zagregowaneKlasy;
                        temp[i]._v = rozWiazka.V;
                        temp[i].stany = klZgloszen.RozkladStanow(rozWiazka, i);
                        temp[i].wiazka = rozWiazka;
                    }

                    Rozklad wynik = new Rozklad(temp[0]);
                    for (int i = 1; i < klZgloszen.progiKlasy.liczbaPrzedziałow; i++)
                        wynik.Agreguj(temp[i]);
                    this.stany = wynik.stany;
                }
            }
            else
            {
                stany = new double[_v + 1];
                stany[0] = 1;
                for (int i = 1; i <= _v; i++)
                    stany[i] = 0;
            }
        }

        public Rozklad(Wiazka rozWiazka, trClass klZgloszen, bool procPrzyjmZglZalOdStanu)
        {
            zagregowaneKlasy = new List<trClass>();
            wiazka = rozWiazka;
            _v = rozWiazka.V;
            if (klZgloszen != null)
            {
                zagregowaneKlasy.Add(klZgloszen);

                if (klZgloszen.progiKlasy == null)
                {
                    stany = klZgloszen.RozkladStanow(rozWiazka, procPrzyjmZglZalOdStanu);
                }
                else
                {
                    Rozklad[] temp = new Rozklad[klZgloszen.progiKlasy.liczbaPrzedziałow];
                    for (int i = 0; i < klZgloszen.progiKlasy.liczbaPrzedziałow; i++)
                    {
                        temp[i] = new Rozklad();
                        temp[i].zagregowaneKlasy = this.zagregowaneKlasy;
                        temp[i]._v = rozWiazka.V;
                        temp[i].stany = klZgloszen.RozkladStanow(rozWiazka, i);
                        temp[i].wiazka = rozWiazka;
                    }

                    Rozklad wynik = new Rozklad(temp[0]);
                    for (int i = 1; i < klZgloszen.progiKlasy.liczbaPrzedziałow; i++)
                        wynik.Agreguj(temp[i]);
                    this.stany = wynik.stany;
                }
            }
            else
            {
                stany = new double[_v + 1];
                stany[0] = 1;
                for (int i = 1; i <= _v; i++)
                    stany[i] = 0;
            }
        }

        
        public Rozklad(Wiazka rozWiazka, int V)
        {
            zagregowaneKlasy = new List<trClass>();
            wiazka = rozWiazka;
            _v = (V == 0) ? rozWiazka.V : V;
            stany = new double[_v + 1];
            stany[0] = 1;
            for (int i = 1; i <= _v; i++)
                stany[i] = 0;
        }
        public Rozklad(Rozklad kopiowany)
        {
            zagregowaneKlasy = new List<trClass>();
            foreach (trClass kopiowana in kopiowany.zagregowaneKlasy)
                zagregowaneKlasy.Add(kopiowana);
            wiazka = kopiowany.wiazka;
            _v = kopiowany.wiazka.V;
            stany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
                stany[n] = kopiowany[n];
        }
        public Rozklad(Rozklad kopiowany, int V, bool normalizacja)
        {
            zagregowaneKlasy = new List<trClass>();
            foreach (trClass kopiowana in kopiowany.zagregowaneKlasy)
                zagregowaneKlasy.Add(kopiowana);
            wiazka = kopiowany.wiazka;
            _v = kopiowany.wiazka.V;
            stany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
                stany[n] = kopiowany[n];
            zmienDlugosc(V, normalizacja);
        }
        public Rozklad(Wiazka rozWiazka, trClass podstKlasa, double[] prStanow, int indV)
        {
            zagregowaneKlasy = new List<trClass>();
            zagregowaneKlasy.Add(podstKlasa);
            stany = prStanow;
            _v = indV;
            wiazka = rozWiazka;
        }
        #endregion konstruktory

        #region wlasciwosci
        /// <summary>
        /// Prawdopodobieństwa zajętości
        /// </summary>
        /// <param name="index">nr stanu</param>
        /// <returns>Prawdopodobisństwo stanu</returns>
        public double this [int index]
        {
            get
            {
                if ((index >= 0) && (index <= _v))
                    return stany[index];
                return 0;
            }
            set
            {
                if ((index >= 0) && (index <= _v))
                    stany[index] = value;
            }
        }
        
        public int V
        {
            get
            {
                return _v;
            }
            set
            {
                _v = value;
            }
        }
        
        /// <summary>
        /// Najstarsza zagregowana klasa
        /// </summary>
        public trClass Max
        {
            get
            {
                trClass wynik = zagregowaneKlasy[0];
                foreach (trClass zagrKlasa in zagregowaneKlasy)
                    wynik = (zagrKlasa.t > wynik.t) ? zagrKlasa : wynik;
                return wynik;
            }
        }

        /// <summary>
        /// Najmłodsza zagregowana klasa
        /// </summary>
        public trClass Min
        {
            get
            {
                trClass wynik = zagregowaneKlasy[0];
                foreach (trClass zagrKlasa in zagregowaneKlasy)
                    wynik = (zagrKlasa.t < wynik.t) ? zagrKlasa : wynik;
                return wynik;
            }
        }

        /// <summary>
        /// Indeks ze zbioru zagregowanych klas, klasy żądającej największej liczby PJP
        /// </summary>
        public int MaxInd
        {
            get
            {
                if (zagregowaneKlasy.Count == 0)
                    return -1;
                int t = zagregowaneKlasy[0].t;
                int wynik = 0;
                for (int i = 1; i < zagregowaneKlasy.Count; i++)
                {
                    if (zagregowaneKlasy[i].t > t)
                    {
                        t = zagregowaneKlasy[i].t;
                        wynik = i;
                    }
                }
                return wynik;
            }
        }
        public int MinInd
        {
            get
            {
                if (zagregowaneKlasy.Count == 0)
                    return -1;
                int t = zagregowaneKlasy[0].t;
                int wynik = 0;
                for (int i = 1; i < zagregowaneKlasy.Count; i++)
                {
                    if (zagregowaneKlasy[i].t < t)
                    {
                        t = zagregowaneKlasy[i].t;
                        wynik = i;
                    }
                }
                return wynik;
            }
        }
        public int tMax
        {
            get
            {
                int wynik = 0;
                foreach (trClass zagrKlasa in zagregowaneKlasy)
                    wynik = (zagrKlasa.t > wynik) ? zagrKlasa.t : wynik;
                return wynik;
            }
        }
        public int tMin
        {
            get
            {
                int wynik = _v;
                foreach (trClass zagrKlasa in zagregowaneKlasy)
                    wynik = (zagrKlasa.t < wynik) ? zagrKlasa.t : wynik;
                return wynik;
            }
        }
        public double sumaAt
        {
            get
            {
                double wynik = 0;
                foreach (trClass zKlasa in zagregowaneKlasy)
                    wynik += zKlasa.at;
                return wynik;
            }
        }

        #endregion wlasciwosci
        
        public double wartOczekiwana(int ostIndeks)
        {
            double licznik = 0;
            double mianownik = 0;
            for (int i = 0; i <= ostIndeks; i++)
                mianownik += this[i];
            for (int i = 1; i <= ostIndeks; i++)
            {
                licznik += i * this[i];
            }
            return licznik / mianownik;
        }

        /// <summary>
        /// Zwraca średnią liczbę obsługiwanych zgłoszeń w stanie n
        /// </summary>
        /// <param name="klasa">Klasa, dla któej chcemy wyznaczyć średnią liczbę obsługiwanych zgłoszeń</param>
        /// <param name="n">Stan, dla którego chcemy wyznaczyć średnia liczbę obsługiwanych zgłoszeń</param>
        /// <returns>Średnia liczba obsługiwanych zgłoszeń</returns>
        public double y(trClass klasa, int n)
        {
            int t = klasa.t;
            if (n < t)
                return 0;
            if (this[n] == 0)
                return 0;
            return this[n - t] * klasa.at / this[n];
        }

        public override string ToString()
        {
            if (V == 0)
                return "V=0";
            string wynik = stany[0].ToString();
            for (int n = 1; n <= this.V; n++)
            {
                wynik += string.Format("\t{0}", stany[n]);
            }
            return wynik;
        }

        #region Operacje
        public void normalizacja()
        {
            double suma=0;
            for (int n = 0; n <= _v; n++)
                suma += stany[n];
            for (int n = 0; n <= _v; n++)
                stany[n] = stany[n] / suma;
        }
        public void normalizacja(int OstIndeks)
        {
            double suma = 0;
            for (int n = 0; n <= OstIndeks; n++)
                suma += stany[n];
            for (int n = 0; n <= OstIndeks; n++)
                stany[n] = stany[n] / suma;
            for (int n = OstIndeks + 1; n <= _v; n++)
                stany[n] = 0;
            this.V = V;
        }
        public void przemnoz(double wartosc)
        {
            for (int n = 0; n <= _v; n++)
                stany[n] *= wartosc;
        }
        public void dodaj(Rozklad dodawany)
        {
            for (int n = 0; n <= _v; n++)
                stany[n] += dodawany[n];
        }

        protected virtual void Splataj(Rozklad rA, Rozklad rB, int nowaDlugosc)
        {
            foreach (trClass kRuchu in rA.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);

            _v = nowaDlugosc;
            stany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                stany[n] = 0;
                for (int l = 0; l <= n; l++)
                {
                    if ((l <= rB.V)&&(n-l <= rA.V))
                        stany[n] += rB[l] * rA[n - l];
                }
            }
            wiazka = rA.wiazka;
        }
        public virtual void Agreguj(Rozklad rB)
        {
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);

            _v = (_v > rB.V) ? _v : rB.V;
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                noweStany[n] = 0;
                for (int l = 0; l <= n; l++)
                    noweStany[n] += this[n - l] * rB[l];
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
        }
        public virtual void Agreguj(Rozklad rB, sigmaPrzyjmZgl sigmy)
        {
            foreach (trClass kRuchu in rB.zagregowaneKlasy)
                zagregowaneKlasy.Add(kRuchu);

            _v = (_v > rB.V) ? _v : rB.V;
            double[] noweStany = new double[_v + 1];
            for (int n = 0; n <= _v; n++)
            {
                noweStany[n] = 0;
                for (int l = 0; l <= n; l++)
                    noweStany[n] += this[n - l] * rB[l];
            }
            for (int n = 0; n <= _v; n++)
                stany[n] = noweStany[n];
        }


        public virtual Rozklad zagregujKlase(trClass klasa)
        {
            if (zagregowaneKlasy.Count == 0)
            {
                return new Rozklad(wiazka, klasa);
            }

            Rozklad wynik = new Rozklad(this);
            Rozklad p = new Rozklad(wiazka, klasa);


            double[] stany = new double[V + 1];
            double suma = 0;

            for (int n = 0; n <= V; n++)
            {
                for (int lC = 0; lC <= n; lC++)
                {
                    double tmp = this[lC] * p[n - lC];
                    suma += tmp;
                    stany[n] += tmp;
                }
            }

            for (int n = 0; n <= V; n++)
                wynik[n] = stany[n] / suma;

            return wynik;
        }


        /// <summary>
        /// Rozplata rozkłady. Od rozkładu rM zostanie odpleciony rozkład ri
        /// </summary>
        /// <param name="rM">Zagregowany rozkład M</param>
        /// <param name="ri">Składowa rozkładu i</param>
        /// <returns>Pozostała składowa rozkładu</returns>
        public static Rozklad Rozplot(Rozklad rM, Rozklad ri)
        {
            Rozklad wynik = new Rozklad(rM);

            foreach (trClass klasa in ri.zagregowaneKlasy)
                wynik.zagregowaneKlasy.Remove(klasa);

            for (int n = 0; n <= wynik.wiazka.V; n++)
            {
                double licznik = rM[n];
                for (int l = 0; l < n; l++)
                    licznik -= (wynik[l] * ri[n-l]);
                wynik[n] = licznik / ri[0];
            }
            wynik.normalizacja();
            return wynik;
        }

        /// <summary>
        /// Wyznacza średnią liczbę PJP zajętych przez obsługiwane zgłoszenia klas zagregowanych w rozkładzie ri dla systemu,
        /// któemu oferowane są klasy zagregowane w rozkładzie rM
        /// </summary>
        /// <param name="rM">Zagregowany rozkład klas obsługiwany w systemie</param>
        /// <param name="ri">rozkład, dla którego chcemy wyznaczyć średnie liczby zajętych PJP</param>
        /// <returns></returns>
        public static double[] liczYt(Rozklad rM, Rozklad ri)
        {
            double[] yt = new double[rM._v];
            Rozklad rMi = Rozklad.Rozplot(rM, ri);
            for (int n = 0; n <= rMi._v; n++)
                for (int l = 0; l <= n; l++)
                    yt[l] += l * ri[l] * rMi[n - l];

            return yt;
        }

        /// <summary>
        /// Zmienia długość rozkładu.
        /// W przypadku, gdy rozkład jest skracany, to zostają usunięte ostatnie wyrazy.
        /// W przypadku, gdy rozkład jest wydłużany ostatnie wyrazy przyjmują wartości 0
        /// </summary>
        /// <param name="ostIndex">Ostatni indeks nowego rozkładu</param>
        /// <param name="normalizacja">Normalizować nowy rozkład</param>
        public void zmienDlugosc(int ostIndex, bool normalizacja)
        {
            //if ((_v < ostIndex) || (ostIndex < 1))
            //    return;         //coś jest nie tak

            double suma = 0;
            double[] noweStany = new double[ostIndex+1];

            int granica = Math.Min(ostIndex, _v);
            for (int n = 0; n <= granica; n++)
            {
                noweStany[n] = stany[n];
                suma += stany[n];
            }
            for (int n = granica + 1; n <= ostIndex; n++)
                noweStany[n] = 0;
            stany = new double[ostIndex+1];
            if (normalizacja)
                for (int n = 0; n <= ostIndex; n++)
                    stany[n] = noweStany[n] / suma;
            else
                for (int n = 0; n <= ostIndex; n++)
                    stany[n] = noweStany[n];
            _v = ostIndex;
        }
        public void obetnijOstanieWyrazy(int lObcWyr, bool normalizacja)
        {
            zmienDlugosc(this.V - lObcWyr, normalizacja);
        }
        public void wyczysc()
        {
            if (stany != null)
            {
                stany[0] = 1;
                for (int i = 1; i <= _v; i++)
                    stany[i] = 0;
            }
        }

        public static Rozklad operator *(Rozklad r1, Rozklad r2)
        {
            Rozklad wynik = new Rozklad();
            int nowaDlugosc = (r1.V + r2.V > r1.wiazka.V) ? r1.wiazka.V : r1.V + r2.V; 
            wynik.Splataj(r1, r2, nowaDlugosc);
            return wynik;
        }
        #endregion operacje
    }
}