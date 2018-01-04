using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using ModelGroup;
using Algorithms;

namespace Simulation
{
    public class agendaType6 : agenda
    {
        private int _MaxSize;
        private int _count;
        private processSym[] _tab;
        private float przesuniecieCzasu;         // W celu przyspieszenia działania eksperymentu symulacyjnego, 


        public agendaType6(aSymulationTyp6 alg)
        {
            this.AlgSym = alg;
            _count = 0;
            _MaxSize = 1024;
            _tab = new processSym[_MaxSize];

            zajeta = new Mutex();
        }

        public override void DodajProces(processSym dodawany)
        {
            dodawany.czas = (float)dodawany.czas + przesuniecieCzasu;
            dodawany.idx = _count;
            incCount();
            _tab[_count - 1] = dodawany;


            przepychajWgore(_count - 1);
        }

        public override processSym PobierzPierwszy()
        {
            processSym wynik = _tab[0];
            _tab[0] = _tab[--_count];
            _tab[_count] = null;                          /// Należy usunąć referencję do tego obiektu, by Garbage Collector mógł usunąć ten obiekt
            _tab[0].idx = 0;

            naprawKopiec(0);
            wynik.czas = ((float)wynik.czas) - przesuniecieCzasu;

            return wynik;
        }

        public void CofnijCzasy(float cofnocO)
        {
            przesuniecieCzasu += cofnocO;

            if (przesuniecieCzasu > 1000)
            {
                for (int idx = 0; idx < _count; idx++)
                    _tab[idx].czas = (float)((float)_tab[idx].czas - przesuniecieCzasu);
                przesuniecieCzasu = 0;
            }
        }

        public override void UsunProces(processSym usuwany)
        {
            usunElement(usuwany.idx);
        }

        private void przepychajWgore(int tmpIdx)
        {
            int tmpParIdx;
            while (tmpIdx > 0)
            {
                tmpParIdx = (int)((tmpIdx + 1) / 2) - 1;
                if (_tab[tmpIdx].czas < _tab[tmpParIdx].czas)
                    swap(tmpParIdx, tmpIdx);
                else
                    break;
                tmpIdx = tmpParIdx;
            }
        }
        private void swap(int idx1, int idx2)
        {
            processSym tmp = _tab[idx1];
            _tab[idx1] = _tab[idx2];
            _tab[idx2] = tmp;
            _tab[idx1].idx = idx1;
            _tab[idx2].idx = idx2;
        }
        private void incCount()
        {
            _count++;
            if (_count > _MaxSize)
            {
                processSym[] newTab = new processSym[2 * _MaxSize];
                Array.Copy(_tab, newTab, _MaxSize);
                _tab = newTab;
                _MaxSize *= 2;
            }
        }
        private void usunElement(int index)
        {
            _count--;
            _tab[index] = _tab[_count];
            _tab[_count] = null;
            if (index == _count)
                return;

            _tab[index].idx = index;
            naprawKopiec(index);
            przepychajWgore(index);
        }
        private void naprawKopiec(int index)
        {
            int najmnIdx = index;
            int tmpIdx;

            int idxL;
            int idxR;

            do
            {
                tmpIdx = najmnIdx;
                idxL = 2 * najmnIdx + 1;
                idxR = 2 * najmnIdx + 2;

                if ((idxL < _count) && (_tab[najmnIdx].czas > _tab[idxL].czas))
                    najmnIdx = idxL;

                if ((idxR < _count) && (_tab[najmnIdx].czas > _tab[idxR].czas))
                    najmnIdx = idxR;

                swap(tmpIdx, najmnIdx);
            }
            while (najmnIdx != tmpIdx);
        }
        private void sprKopiec()
        {
            double tmp = _tab[0].czas;
            for (int i = 0; i < _count; i++)
            {
                tmp = _tab[i].czas;
                int tmpIdxL = 2 * i + 1;
                int tmpIdxR = 2 * i + 2;

                if (tmpIdxL < _count)
                {
                    if (tmp > _tab[tmpIdxL].czas)
                        throw new Exception(string.Format("Błąd w kopcu w lewym węźle (tab[{0}] = {1}, tab[{2}] = {3})", i, tmp, tmpIdxL, _tab[tmpIdxL].czas));
                }
                if (tmpIdxR < _count)
                {
                    if (tmp > _tab[tmpIdxR].czas)
                        throw new Exception(string.Format("Błąd w kopcu w prawym węźle (tab[{0}] = {1}, tab[{2}] = {3})", i, tmp, tmpIdxR, _tab[tmpIdxR].czas));
                }
            }

        }
    }


    /// <summary>
    /// Symulacja z bezwzględnym czasem i zoptymalizowaną implementacją kolejki
    /// </summary>
    public class aSymulationTyp6 : aSimulation
    {
        public aSymulationTyp6(Wiazka wSymulowana)
            : base(wSymulowana, 6, wSymulowana.bazaDanych, "bezwzględny czas typu float, statystyki na zmiennych typu double, dodawane bez optymalizacji")
        {
            SkrNazwaAlg = "Sym6";
            NazwaAlg = "Symulacja typ 6";
            _symulacja = true;
        }

        public override bool eksperymentalny
        {
            get
            {
                return true;
            }
        }


        const int lWatkow = 1; //liczba jednoczesnych wątków z symulacją jak jest większa liczba, to nie są wpisywane wszystkie wyniki. Program działa stabilnie

        protected override void InicjacjaNowegoBadaniaSymulacji(double aOf, int nrBadania)
        {
            lZdarzen[nrBadania] = new agendaType6(this);
            sWiazka[nrBadania] = new SimGroup(aWiazka);
            foreach (trClass kOfer in aWiazka.ListaKlasRuchu)
            {
                kOfer.ObliczParametry(aOf, aWiazka.sumaPropAT, aWiazka.V);
                kOfer.InicjacjaKolejnegoBadaniaSymulacji(sWiazka[nrBadania], lZdarzen[nrBadania], this, nrBadania);
                kOfer.sigmyStanow = new SigmaStruktury(aWiazka.V + 1);
                kOfer.sigmyStanow.wyczysc();
            }
            lZdarzen[nrBadania].czasOczekiwania = aWiazka.CzStartu;
        }

        protected override void SymulujWiazke(int nrBadania, int nrSerii)
        {
            agendaType6 agendaSym = lZdarzen[nrBadania] as agendaType6;
            SimGroup wiazka = sWiazka[nrBadania];

            agendaSym.zajeta.WaitOne();
            NowaSeria(nrBadania, false);
            while (symulowac(wiazka))
            {
                processSym pierwszy = agendaSym.PobierzPierwszy();
                if (pierwszy.czas < 0)
                {
                    if (pierwszy.czas > -0.00001)
                        pierwszy.czas = 0;
                    else
                        throw new Exception(string.Format("Ujemny czas {0}", pierwszy.czas));
                }
                agendaSym.CofnijCzasy((float)pierwszy.czas);
                if (agendaSym.zapStatystyk)
                    wiazka.Podlicz(pierwszy.czas);
                else
                    agendaSym.czasOczekiwania -= pierwszy.czas;
                pierwszy.Obsluz();
            }

            KoniecSerii(nrBadania, nrSerii);
            lZdarzen[nrBadania].zajeta.ReleaseMutex();
            ZapiszWynikiSerii(nrBadania, nrSerii);
            aWiazka.DodPost();
        }
    }
}
