using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ModelGroup;
using Algorithms;
using Simulation;
using ZedGraph;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using BazaDanych;

namespace GUI
{
    enum scriptCode
    {
        unknown,
        newScr,               /// kasowanie wszystkich ustawień
        addTrClass,           /// dodawanie nowej klasy
        addSubGroup,          /// dodawanie podgrupy
        addAlgorithm,         /// dodawanie algorytmu
        chooseTraffiCclass,   /// wybieranie klasy
        saveTable,            /// zapis wyników do tabeli TEXa
        saveChart,            /// zapis wykresu do skryptu dla Gnuplota
        reservationSettings   /// ustawianie granicy rezerwacji dla wiązki pełnodostępnej
    };

    public partial class FormMainWindow : Form
    {
        private DebugOptions debug;
        TextReader myScript;

        BazaDanych.DataBase myDataBase;

        Regex separations = new Regex(@"\s+");
        public Wiazka myGroup;
        //modelowana wiązka


        /// <summary>
        /// Określa, czy wszystkie wymagane elementy systemu zostały już wprowadzone
        /// </summary>
        private bool KompletnyModelSystemu
        {
            get
            {
                if ((myGroup.V == 0) || (myGroup.ListaKlasRuchu.Count == 0))
                    return false;
                return true;

            }
        }

        private bool PotrzebaModelowania
        {
            get
            {
                if (myGroup.badaSystem)
                    return false;
                foreach (Algorytm sprawdzany in myGroup.ListaAlgorytmow)
                {
                    if (!sprawdzany.Wybrany)
                        continue;
                    if ((sprawdzany.mozliwy) || (sprawdzany.wymuszalny) && (wymStosNeiodpAlgToolStripMenuItem.Checked))
                        if ((sprawdzany.Obliczony == false))
                            return true;
                } 
                return false;
            }
        }



        public FormMainWindow()
        {
            myDataBase = new BazaDanych.DataBase(@"server=mysql-475253.vipserv.org;userid=makgywer_wiazka;password=nukanuka;database=makgywer_wiazka", WypiszLog, WypiszLog);

            myGroup = new Wiazka(myDataBase);
            myGroup.PokazWyniki = new Wiazka.PokazWynikiDel(PokazRezultatyPoZakBadania);
            myGroup.PostepObliczen = new Wiazka.UaktPost(WyswietlPostep);
            myGroup.wypDeb = new Wiazka.Debug(WypiszDebug);

            debug = new DebugOptions(myGroup);
            myGroup.debug = this.debug.opcje;
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            List<DataBaseSimulPar> parametrySymulacji = myDataBase.dodajPredefiniowaneParametrySymulacji();
            foreach (DataBaseSimulPar tmp in parametrySymulacji)
                comboBoxParametrySymulacji.Items.Add(tmp);
            comboBoxParametrySymulacji.SelectedItem = comboBoxParametrySymulacji.Items[0];

            List<BDsystem> systemy = myDataBase.dodajPredefiniowaneSystemy();
            foreach (BDsystem tmp in systemy)
                comboBoxSystemy.Items.Add(tmp);
            comboBoxSystemy.SelectedItem = comboBoxSystemy.Items[0];


            DataBaseSimulPar parametry = comboBoxParametrySymulacji.SelectedItem as DataBaseSimulPar;
            if (parametry != null)
            {
                myGroup.lSymUtrZgl = parametry.lUtrZgl;
                myGroup.lSerii = parametry.lSeri;
                myGroup.CzStartu = parametry.czStartu;
            }
            else
            {
                MessageBox.Show("Nie można wczytać parametrów symulacji");
                Close();
            }
            tabPageModelSystemu.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic).Invoke(tabPageModelSystemu, new object[] {
                   ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true });

            zedGraphControlWykrE.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic).Invoke(zedGraphControlWykrE, new object[] {
                   ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true });

            zedGraphControlWykrB.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic).Invoke(zedGraphControlWykrB, new object[] {
                   ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true });

            zedGraphControlWykrEB.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic).Invoke(zedGraphControlWykrEB, new object[] {
                   ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true });
        }


        private void _pokRezBadania()
        {
            try
            {
                prezentujWykresyE_B_EB();
                prezentujWykresyPU();
                prezentujBladWzgledny(listBoxAlgReferencyjny.SelectedItem as Algorytm);
                prezentujTabele(); 
                updateButtonsStates();
                Refresh();
            }
            catch (Exception exc)
            {
                System.Console.WriteLine(exc.ToString());
                Close();
            }
        }

        public delegate void pokWynDel();

        public void PokazRezultatyPoZakBadania()
        {
            if (this.InvokeRequired)
                BeginInvoke(new pokWynDel(_pokRezBadania));
            else
                _pokRezBadania();
        }

        public delegate void wyswPost(int postep);
        private void _wyspPost(int postep)
        {
            progressBarPost.Value = postep;
        }
        public void WyswietlPostep(int postep)
        {
            if (progressBarPost.InvokeRequired)
                BeginInvoke(new wyswPost(_wyspPost), postep);
            else
                progressBarPost.Value = postep;
        }

        public void WypiszDebug(string napis)
        {
            richTextBoxDebug.Text += napis;
        }

        public void WypiszLog(string napis)
        {
            richTextBoxDebug.Text += napis;
        }

        private void UpdateNumberOfInvestigations()
        {
            int lBadan = (int)(((double)numericUpDownMaxRuchOf.Value - (double)numericUpDownMinRuchOf.Value) / (double)numericUpDownPrzyrRuchOf.Value + 1);
            this.textBoxLBad.Text = lBadan.ToString();
        }

        public void updateListOfTrClasses()
        {
            listBoxKlasy.Items.Clear();
            listBoxKlasy.SelectedItems.Clear();

            foreach (trClass przegladana in myGroup.ListaKlasRuchu)
            {
                listBoxKlasy.Items.Add(przegladana);
            }
        }

        private void updateGroupInfo()
        {
            int lPodgrup = myGroup.sumaK;
            int PojCalejWiazki = myGroup.V;

            textBoxPojCalkowita.Text = PojCalejWiazki.ToString();
            textBoxLiczbaPodgrup.Text = lPodgrup.ToString();

            listBoxAlgorytmy.Items.Clear();
            foreach (Algorytm przegladany in myGroup.ListaAlgorytmow)
                if (((przegladany.eksperymentalny == false) || (EkspAlgToolStripMenuItem.Checked == true)) && ((przegladany.mozliwy) ||
                    ((wymStosNeiodpAlgToolStripMenuItem.Checked) && (przegladany.wymuszalny))))
                {
                    listBoxAlgorytmy.Items.Add(przegladany);
                    if (przegladany.Wybrany)
                        listBoxAlgorytmy.SelectedItems.Add(przegladany);
                }
            updateButtonsStates();

            switch (myGroup.AlgorytmRezerwacji)
            {
                case reservationAlgorithm.none:
                    numericUpDownGranicaRezerwacji.Value = myGroup.q;
                    break;
                case reservationAlgorithm.R1_R2:
                    numericUpDownGranicaRezerwacji.Value = myGroup.q;
                    break;
                case reservationAlgorithm.R3:
                    numericUpDownGranicaRezerwacji.Value = myGroup.tNieuprzywilejowanyMax;
                    break;
            }
        }

        const string strStop = "Zatrzymaj";
        const string strDodajNowySystem = "Dodaj nowy system";
        const string strStart = "Rozpocznij modelowanie systemu";


        public void updateButtonsStates()
        {
            if (myGroup.badaSystem)
            {
                buttonStartAddStop.Text = strStop;
                buttonStartAddStop.Enabled = true;
            }
            else if (myGroup.sysNo == -1)
            {
                buttonStartAddStop.Text = strDodajNowySystem;
                buttonStartAddStop.Enabled = KompletnyModelSystemu;
            }
            else
            {
                buttonStartAddStop.Text = strStart;
                buttonStartAddStop.Enabled = PotrzebaModelowania;
            }


            //tabControlPrezentacja.SelectedTab.Refresh();

            groupBoxAlgWybPodgrupy.Enabled = (myGroup.sumaK > 1) ? true : false;
            groupBoxAlgRezerwacji.Enabled = ((myGroup.m > 1) && (myGroup.sumaK > 0)) ? true : false;
            radioButtonR3.Enabled = (myGroup.sumaK > 1) ? true : false;

            //Refresh();
        }
        public void clearResults()
        {
            cancelConnection();
            myGroup.KasujWyniki();
            updateButtonsStates();
            richTextBoxTabelaB.Text = "Brak wyników";
            richTextBoxTabelaE.Text = "Brak wyników";
            progressBarPost.Value = 0;
            this.Refresh();
        }

        #region prezentacja wyników

        private void prezentujTabele()
        {
            prezentujTabeleEB();
            prezentujTabelePU();
        }

        private void prezentujTabelePU()
        {
            Algorytm niepusty = null;
            richTextBoxPU_E.Text = string.Format("Względne przedziały ufności dla prawdopodobieństwa blokady w systemie {0}\n", myGroup.sysNo);
            richTextBoxPU_E.Text += string.Format("Parametry symulacji: {0}\n", myGroup.parSymulacji);
            richTextBoxPU_B.Text = string.Format("Względne przedziały ufności dla prawdopodobieństwa strat w systemie {0}\n", myGroup.sysNo);
            richTextBoxPU_B.Text += string.Format("Parametry symulacji: {0}\n", myGroup.parSymulacji);
            richTextBoxPU_E.Text += "a";
            richTextBoxPU_B.Text += "a";

            for (int kl = 0; kl < myGroup.m; kl++)
            {
                foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                {
                    if ((wypisywany.Obliczony || wypisywany.wynikiAlg != null) && wypisywany.Wybrany && wypisywany.symulacja)
                    {
                        richTextBoxPU_E.Text += ("\t" + wypisywany.ToSkrString());
                        richTextBoxPU_B.Text += ("\t" + wypisywany.ToSkrString());
                        niepusty = wypisywany;
                    }
                }
            }
            if (niepusty == null)
                return;
            richTextBoxPU_E.Text += "\n";
            richTextBoxPU_B.Text += "\n";
            for (int wiersz = 0; wiersz < niepusty.wynikiAlg.lBadan; wiersz++)
            {
                richTextBoxPU_E.Text += niepusty.wynikiAlg.PobA(wiersz).ToString("f2");
                richTextBoxPU_B.Text += niepusty.wynikiAlg.PobA(wiersz).ToString("f2");
                foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
                {
                    foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                    {
                        if ((wypisywany.Obliczony || wypisywany.wynikiAlg != null) && wypisywany.Wybrany && wypisywany.symulacja)
                        {
                            double tmpPuE = wypisywany.wynikiAlg.PobBlE(wiersz, wypKlasa) / wypisywany.wynikiAlg.PobE(wiersz, wypKlasa);
                            double tmpPuB = wypisywany.wynikiAlg.PobBlB(wiersz, wypKlasa) / wypisywany.wynikiAlg.PobB(wiersz, wypKlasa);
                            richTextBoxPU_E.Text += ("\t" + tmpPuE.ToString("f" + numericUpDownPU_E.Value.ToString()));
                            richTextBoxPU_B.Text += ("\t" + tmpPuB.ToString("f" + numericUpDownPU_B.Value.ToString()));
                        }
                    }
                }
                richTextBoxPU_E.Text += "\n";
                richTextBoxPU_B.Text += "\n";
            }
        }

        private void prezentujTabeleEB()
        {
            Algorytm niepusty = null;
            richTextBoxTabelaE.Text = "Prawdopodobieństwo blokady\n";
            richTextBoxTabelaB.Text = "Prawdopodobieństwo strat\n";
            richTextBoxTabelaE.Text += "a";
            richTextBoxTabelaB.Text += "a";

            for (int kl = 0; kl < myGroup.m; kl++)
            {
                foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                {
                    if ((wypisywany.Obliczony || wypisywany.wynikiAlg != null) && (wypisywany.Wybrany))
                    {
                        richTextBoxTabelaE.Text += ("\t" + wypisywany.ToSkrString());
                        {
                            if (wypisywany.wynikiAlg.symulacja)
                                richTextBoxTabelaE.Text += "\t+-";
                        }
                        if (wypisywany.wynikiAlg.pStrat)
                        {
                            richTextBoxTabelaB.Text += ("\t" + wypisywany.ToSkrString());
                            if (wypisywany.wynikiAlg.symulacja)
                                richTextBoxTabelaB.Text += "\t+-";
                        }
                        niepusty = wypisywany;
                    }
                }
            }
            if (niepusty == null)
                return;
            richTextBoxTabelaE.Text += "\n";
            richTextBoxTabelaB.Text += "\n";
            for (int wiersz = 0; wiersz < niepusty.wynikiAlg.lBadan; wiersz++)
            {
                richTextBoxTabelaE.Text += niepusty.wynikiAlg.PobA(wiersz).ToString("f2");
                richTextBoxTabelaB.Text += niepusty.wynikiAlg.PobA(wiersz).ToString("f2");
                foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
                {
                    foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                    {
                        if ((wypisywany.Obliczony || wypisywany.wynikiAlg != null) && (wypisywany.Wybrany))
                        {
                            if (wypisywany.wynikiAlg.symulacja)
                            {
                                richTextBoxTabelaE.Text += ("\t" + wypisywany.wynikiAlg.PobE(wiersz, wypKlasa).ToString("f" + numericUpDownLMiejscE.Value.ToString()) + "\t" + wypisywany.wynikiAlg.PobBlE(wiersz, wypKlasa).ToString("f" + numericUpDownLMiejscE.Value.ToString()));
                            }
                            else
                            {
                                richTextBoxTabelaE.Text += ("\t" + wypisywany.wynikiAlg.PobE(wiersz, wypKlasa).ToString("f" + numericUpDownLMiejscE.Value.ToString()));
                            }
                            if (wypisywany.wynikiAlg.pStrat)
                            {
                                if (wypisywany.wynikiAlg.symulacja)
                                {
                                    richTextBoxTabelaB.Text += ("\t" + wypisywany.wynikiAlg.PobB(wiersz, wypKlasa).ToString("f" + numericUpDownLMiejscB.Value.ToString()) + "\t" + wypisywany.wynikiAlg.PobBlB(wiersz, wypKlasa).ToString("f" + numericUpDownLMiejscB.Value.ToString()));
                                }
                                else
                                {
                                    richTextBoxTabelaB.Text += ("\t" + wypisywany.wynikiAlg.PobB(wiersz, wypKlasa).ToString("f" + numericUpDownLMiejscB.Value.ToString()));
                                }
                            }
                        }
                    }
                }
                richTextBoxTabelaE.Text += "\n";
                richTextBoxTabelaB.Text += "\n";
            }
        }

        public Color[] kolorWykr = {Color.Blue, Color.Red, Color.Green, Color.Black, Color.Orange, Color.Purple};
        public DashStyle[] lStyle = {DashStyle.Solid, DashStyle.Dot, DashStyle.Dash, DashStyle.DashDot, DashStyle.DashDotDot};

        private void prezentujWykresyPU()
        {           
            GraphPane myPanePU_E = zedGraphControlPU_E.GraphPane;
            GraphPane myPanePU_B = zedGraphControlPU_B.GraphPane;
            GraphPane myPanePU_EiB = zedGraphControlPU_EiB.GraphPane;
            if (myPanePU_E == null) throw new Exception("Form was closed");

            myPanePU_E.XAxis.Scale.Min = (double)numericUpDownMinRuchOf.Value;
            myPanePU_E.XAxis.Scale.Max = (double)numericUpDownMaxRuchOf.Value;
            myPanePU_E.Title.Text = "MultiConvTool © Adam Kaliszan";
            myPanePU_E.XAxis.Title.Text = "ruch oferowany przypadający na 1 PJP";
            myPanePU_E.YAxis.Title.Text = "Względna szerokość przedziału ufności dla blokady";
            myPanePU_E.Title.FontSpec.Size = 14;
            myPanePU_E.Legend.Border.IsVisible = false;
            myPanePU_E.Legend.FontSpec.Size = 10;
            myPanePU_E.Legend.Position = LegendPos.Right;

            myPanePU_B.XAxis.Scale.Min = (double)numericUpDownMinRuchOf.Value;
            myPanePU_B.XAxis.Scale.Max = (double)numericUpDownMaxRuchOf.Value;
            myPanePU_B.Title.Text = "MultiConvTool © Adam Kaliszan";
            myPanePU_B.XAxis.Title.Text = "ruch oferowany przypadający na 1 PJP";
            myPanePU_B.YAxis.Title.Text = "Względna szerokość przedziału ufności dla strat";
            myPanePU_B.Title.FontSpec.Size = 14;
            myPanePU_B.Legend.Border.IsVisible = false;
            myPanePU_B.Legend.FontSpec.Size = 10;
            myPanePU_B.Legend.Position = LegendPos.Right;

            myPanePU_EiB.XAxis.Scale.Min = (double)numericUpDownMinRuchOf.Value;
            myPanePU_EiB.XAxis.Scale.Max = (double)numericUpDownMaxRuchOf.Value;
            myPanePU_EiB.Title.Text = "MultiConvTool © Adam Kaliszan";
            myPanePU_EiB.XAxis.Title.Text = "ruch oferowany przypadający na 1 PJP";
            myPanePU_EiB.YAxis.Title.Text = "Względna szerokość przedziału ufności dla blokady i strat";
            myPanePU_EiB.Title.FontSpec.Size = 14;
            myPanePU_EiB.Legend.Border.IsVisible = false;
            myPanePU_EiB.Legend.FontSpec.Size = 10;
            myPanePU_EiB.Legend.Position = LegendPos.Right;

            myPanePU_E.CurveList.Clear();
            myPanePU_B.CurveList.Clear();
            myPanePU_EiB.CurveList.Clear();

            int indeksKoloru = 0;
            int indeksStylu = 0;

            double maxPU_E = 0;
            double maxPU_B = 0;

            foreach (Algorytm alg in myGroup.ListaAlgorytmow)
            {
                if ((alg.mozliwy || (alg.wymuszalny && wymStosNeiodpAlgToolStripMenuItem.Checked)) && (alg.Obliczony || alg.wynikiAlg != null) && alg.symulacja && alg.Wybrany)
                {
                    foreach (trClass klasa in listBoxKlasy.SelectedItems)
                    {
                        PointPairList przebiegPU_E = new PointPairList();
                        PointPairList przebiegPU_B = new PointPairList();
                        for (int i = 0; i < myGroup.lBadan; i++)
                        {
                            double aDod = alg.wynikiAlg.PobA(i);

                            double wDodE = alg.wynikiAlg.PobE(i, klasa);
                            double wDodE_bl = alg.wynikiAlg.PobBlE(i, klasa);

                            double yE = wDodE_bl/wDodE;
                            przebiegPU_E.Add(aDod, yE);
                            if (yE > maxPU_E)
                                maxPU_E = yE;
                            
                            double wDodB = alg.wynikiAlg.PobB(i, klasa);
                            double dDodB_bl = alg.wynikiAlg.PobBlB(i, klasa);
                            double yB = dDodB_bl / wDodB;
                            przebiegPU_B.Add(aDod, yB);
                            if (yB > maxPU_B)
                                maxPU_B = yB;
                        }

                        LineItem myCurveE = myPanePU_E.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegPU_E, kolorWykr[indeksKoloru], SymbolType.XCross);
                        myCurveE.Line.Style = lStyle[indeksStylu];


                        LineItem myCurveB = myPanePU_B.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegPU_B, kolorWykr[indeksKoloru], SymbolType.Plus);
                        myCurveB.Line.Style = lStyle[indeksStylu];

                        LineItem myCurveE1 = myPanePU_EiB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegPU_E, kolorWykr[indeksKoloru], SymbolType.XCross);
                        myCurveE1.Line.Style = lStyle[indeksStylu];
                        LineItem myCurveB1 = myPanePU_EiB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegPU_B, kolorWykr[indeksKoloru], SymbolType.Plus);
                        myCurveB1.Line.Style = lStyle[indeksStylu];

                        indeksStylu = (indeksStylu + 1) % lStyle.Length;
                    }
                    indeksStylu = 0;
                    indeksKoloru = (indeksKoloru + 1) % kolorWykr.Length;
                }
            }


            myPanePU_E.YAxis.Scale.Max = maxPU_E;
            myPanePU_B.YAxis.Scale.Max = maxPU_B;
            myPanePU_EiB.YAxis.Scale.Max = Math.Max(maxPU_E, maxPU_B);

            zedGraphControlPU_E.AxisChange();
            zedGraphControlPU_B.AxisChange();
            zedGraphControlPU_EiB.AxisChange();
            tabPagePU.Refresh();
        }

        private void prezentujWykresyE_B_EB()
        {

            GraphPane myPaneE = zedGraphControlWykrE.GraphPane;
            GraphPane myPaneB = zedGraphControlWykrB.GraphPane;
            GraphPane myPaneEB = zedGraphControlWykrEB.GraphPane;

            if ((myPaneE == null) || (myPaneB == null) || (myPaneEB == null))
                throw new Exception("Form was closed");

            myPaneE.XAxis.Scale.Min = (double)numericUpDownMinRuchOf.Value;
            myPaneE.XAxis.Scale.Max = (double)numericUpDownMaxRuchOf.Value;
            myPaneB.XAxis.Scale.Min = (double)numericUpDownMinRuchOf.Value;
            myPaneB.XAxis.Scale.Max = (double)numericUpDownMaxRuchOf.Value;
            myPaneEB.XAxis.Scale.Min = (double)numericUpDownMinRuchOf.Value;
            myPaneEB.XAxis.Scale.Max = (double)numericUpDownMaxRuchOf.Value;
            myPaneE.YAxis.Scale.Max = 1;
            myPaneB.YAxis.Scale.Max = 1;
            myPaneEB.YAxis.Scale.Max = 1;

            myPaneE.Title.Text = "MultiConvTool © Adam Kaliszan";
            myPaneE.Title.FontSpec.Size = 14;
            myPaneE.Legend.Border.IsVisible = false;
            myPaneE.Legend.FontSpec.Size = 14;
            myPaneE.Legend.Position = LegendPos.InsideBotRight;
            myPaneE.XAxis.Title.Text = "ruch oferowany przypadający na 1 PJP";
            myPaneE.XAxis.Title.FontSpec.Size = 14;
            myPaneE.YAxis.Title.Text = "prawdopodobieństwo blokady";
            myPaneE.YAxis.Title.FontSpec.Size = 14;
            myPaneE.YAxis.Type = AxisType.Log;

            myPaneB.Title.Text = "MultiConvTool © Adam Kaliszan";
            myPaneB.Title.FontSpec.Size = 14;
            myPaneB.Legend.Border.IsVisible = false;
            myPaneB.Legend.FontSpec.Size = 14;
            myPaneB.Legend.Position = LegendPos.InsideBotRight;
            myPaneB.XAxis.Title.Text = "ruch oferowany przypadający na 1 PJP";
            myPaneB.XAxis.Title.FontSpec.Size = 14;
            myPaneB.YAxis.Title.Text = "prawdopodobieństwo strat";
            myPaneB.YAxis.Title.FontSpec.Size = 14;
            myPaneB.YAxis.Type = AxisType.Log;

            myPaneEB.Title.Text = "MultiConvTool © Adam Kaliszan";
            myPaneEB.Title.FontSpec.Size = 14;
            myPaneEB.Legend.Border.IsVisible = false;
            myPaneEB.Legend.FontSpec.Size = 14;
            myPaneEB.Legend.Position = LegendPos.InsideBotRight;
            myPaneEB.XAxis.Title.Text = "ruch oferowany przypadający na 1 PJP";
            myPaneEB.XAxis.Title.FontSpec.Size = 14;
            myPaneEB.YAxis.Title.Text = "prawdopodobieństwa blokady i strat";
            myPaneEB.YAxis.Title.FontSpec.Size = 14;
            myPaneEB.YAxis.Type = AxisType.Log;

            myPaneE.CurveList.Clear();
            myPaneB.CurveList.Clear();
            myPaneEB.CurveList.Clear();

            int indeksKoloru = 0;
            int indeksStylu = 0;

            foreach (Algorytm alg in myGroup.ListaAlgorytmow)
            {
                if (((alg.mozliwy) || ((alg.wymuszalny) && (wymStosNeiodpAlgToolStripMenuItem.Checked))) && ((alg.Obliczony)||(alg.wynikiAlg!= null)))
                {
                    if (alg.Wybrany)
                    {
                        foreach (trClass klasa in listBoxKlasy.SelectedItems)
                        {
                            PointPairList przebiegE = new PointPairList();
                            PointPairList przebiegB = new PointPairList();
                            for (int i = 0; i < myGroup.lBadan; i++)
                            {
                                double aDod = alg.wynikiAlg.PobA(i);
                                double wDodE = alg.wynikiAlg.PobE(i, klasa);
                                przebiegE.Add(aDod, wDodE);
                                if (alg.wynikiAlg.pStrat)
                                {
                                    double wDodB = alg.wynikiAlg.PobB(i, klasa);
                                    przebiegB.Add(aDod, wDodB);
                                }
                            }
                            if (alg.wynikiAlg.symulacja)
                            {
                                PointPairList przebiegBladE = new PointPairList();
                                PointPairList przebiegBladB = new PointPairList();
                                for (int i = 0; i < myGroup.lBadan; i++)
                                {
                                    double aDod = alg.wynikiAlg.PobA(i);
                                    double wDodBlEMinus = alg.wynikiAlg.PobE(i, klasa) - alg.wynikiAlg.PobBlE(i, klasa);
                                    double wDodBlEPlus = alg.wynikiAlg.PobE(i, klasa) + alg.wynikiAlg.PobBlE(i, klasa);
                                    przebiegBladE.Add(aDod, wDodBlEMinus, wDodBlEPlus);
                                    if (alg.wynikiAlg.pStrat)
                                    {
                                        double wDodBlBMinus = alg.wynikiAlg.PobB(i, klasa) - alg.wynikiAlg.PobBlB(i, klasa);
                                        double wDodBlBPlus = alg.wynikiAlg.PobB(i, klasa) + alg.wynikiAlg.PobBlB(i, klasa);
                                        przebiegBladB.Add(aDod, wDodBlBMinus, wDodBlBPlus);
                                    }
                                }

                                LineItem myCurveE = myPaneE.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegE, kolorWykr[indeksKoloru], SymbolType.XCross);
                                myCurveE.Line.IsVisible = false;
                                myCurveE.Line.Style = lStyle[indeksStylu];

                                myPaneE.AddErrorBar("", przebiegBladE, kolorWykr[indeksKoloru]);

                                if (alg.wynikiAlg.pStrat)
                                {
                                    LineItem myCurveB = myPaneB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegB, kolorWykr[indeksKoloru], SymbolType.Plus);
                                    myPaneB.AddErrorBar("", przebiegBladB, kolorWykr[indeksKoloru]);
                                    myCurveB.Line.IsVisible = false;

                                    LineItem myCurveE1 = myPaneEB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegE, kolorWykr[indeksKoloru], SymbolType.XCross);
                                    LineItem myCurveB1 = myPaneEB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegB, kolorWykr[indeksKoloru], SymbolType.Plus);
                                    myCurveE1.Line.IsVisible = false;
                                    myCurveB1.Line.IsVisible = false;
                                }
                            }
                            else
                            {
                                LineItem myCurveE = myPaneE.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegE, kolorWykr[indeksKoloru], SymbolType.None);
                                myCurveE.Line.Width = 2;
                                myCurveE.Line.Style = lStyle[indeksStylu];
                                if (alg.wynikiAlg.pStrat)
                                {
                                    LineItem myCurveB = myPaneB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegB, kolorWykr[indeksKoloru], SymbolType.None);
                                    LineItem myCurveE1 = myPaneEB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegE, kolorWykr[indeksKoloru], SymbolType.None);
                                    LineItem myCurveB1 = myPaneEB.AddCurve(alg.ToString() + ": " + klasa.ToString(), przebiegB, kolorWykr[indeksKoloru], SymbolType.Circle);
                                    myCurveB.Line.Style = lStyle[indeksStylu];
                                    myCurveE1.Line.Style = lStyle[indeksStylu];
                                    myCurveB1.Line.Style = lStyle[indeksStylu];
                                    myCurveB.Line.Width = 2;
                                    myCurveE1.Line.Width = 2;
                                    myCurveB1.Line.Width = 2;
                                }
                            }
                            indeksKoloru = (indeksKoloru + 1) % kolorWykr.Length;
                        }
                        indeksKoloru = 0;
                    }
                    indeksStylu = (indeksStylu + 1) % lStyle.Length;
                }
            }
            zedGraphControlWykrE.AxisChange();
            tabPageEwykres.Refresh();
            zedGraphControlWykrB.AxisChange();
            tabPageBwykres.Refresh();
            zedGraphControlWykrEB.AxisChange();
            tabPageEBwykres.Refresh();
        }
        private void prezentujBladWzgledny(Algorytm aReferencyjny)
        {
            if (aReferencyjny == null)
                return;

            GraphPane myPaneDelta = zedGraphControlBladWzgl.GraphPane;

            myPaneDelta.XAxis.Scale.Min = (double)numericUpDownMinRuchOf.Value;
            myPaneDelta.XAxis.Scale.Max = (double)numericUpDownMaxRuchOf.Value;

            //myPaneDelta.YAxis.Scale.Max = 1;

            myPaneDelta.Title.Text = "MultiConvTool © Adam Kaliszan";
            myPaneDelta.Title.FontSpec.Size = 14;
            myPaneDelta.Legend.Border.IsVisible = false;
            myPaneDelta.Legend.FontSpec.Size = 14;
            myPaneDelta.Legend.Position = LegendPos.InsideBotRight;
            myPaneDelta.XAxis.Title.Text = "ruch oferowany przypadający na 1 PJP";
            myPaneDelta.XAxis.Title.FontSpec.Size = 14;
            myPaneDelta.YAxis.Title.Text = "błąd względny";
            myPaneDelta.YAxis.Title.FontSpec.Size = 14;
            myPaneDelta.YAxis.Type = AxisType.Linear;


            myPaneDelta.CurveList.Clear();

            int lKolorow = 6;
            Color[] kolorWykr = new Color[lKolorow];
            kolorWykr[0] = new Color(); kolorWykr[0] = Color.Red;
            kolorWykr[1] = new Color(); kolorWykr[1] = Color.Blue;
            kolorWykr[2] = new Color(); kolorWykr[2] = Color.Green;
            kolorWykr[3] = new Color(); kolorWykr[3] = Color.Black;
            kolorWykr[4] = new Color(); kolorWykr[4] = Color.Orange;
            kolorWykr[5] = new Color(); kolorWykr[5] = Color.Purple;
            int indeksKoloru = 0;

            int lStyliLin = 5;
            DashStyle[] lStyle = new DashStyle[lStyliLin];
            lStyle[0] = new DashStyle(); lStyle[0] = DashStyle.Solid;
            lStyle[1] = new DashStyle(); lStyle[1] = DashStyle.Dot;
            lStyle[2] = new DashStyle(); lStyle[2] = DashStyle.Dash;
            lStyle[3] = new DashStyle(); lStyle[3] = DashStyle.DashDot;
            lStyle[4] = new DashStyle(); lStyle[4] = DashStyle.DashDotDot;

            if (aReferencyjny.wynikiAlg.symulacja)
            {
                indeksKoloru = 0;
                foreach (trClass klasa in listBoxKlasy.SelectedItems)
                {
                    PointPairList przebiegBlWzgl_plus = new PointPairList();
                    PointPairList przebiegBlWzgl_minus = new PointPairList();

                    for (int i = 0; i < myGroup.lBadan; i++)
                    {
                        double aDod = aReferencyjny.wynikiAlg.PobA(i);
                        double wDodBlEMinus = 0 - ((aReferencyjny.wynikiAlg.PobBlE(i, klasa)) / aReferencyjny.wynikiAlg.PobE(i, klasa));
                        double wDodBlEPlus = aReferencyjny.wynikiAlg.PobBlE(i, klasa) / aReferencyjny.wynikiAlg.PobE(i, klasa);

                        przebiegBlWzgl_plus.Add(aDod, wDodBlEPlus);
                        przebiegBlWzgl_minus.Add(aDod, wDodBlEMinus);
                    }

                    LineItem myCurveBlSymPlus = myPaneDelta.AddCurve(aReferencyjny.ToString(), przebiegBlWzgl_plus, Color.White, SymbolType.XCross);
                    LineItem myCurveBlSymMinus = myPaneDelta.AddCurve(null, przebiegBlWzgl_minus, Color.White, SymbolType.XCross);

                    myCurveBlSymMinus.Line.Fill = new Fill(Color.FromArgb(90, kolorWykr[indeksKoloru]), Color.FromArgb(20, kolorWykr[indeksKoloru]), 90F);
                    myCurveBlSymPlus.Line.Fill = new Fill(Color.FromArgb(90, kolorWykr[indeksKoloru]), Color.FromArgb(20, kolorWykr[indeksKoloru]), 270F);

                    //myCurveBlSymPlus.Line.IsSmooth = true;
                    //myCurveBlSymMinus.Line.IsSmooth = true;
                    indeksKoloru = (indeksKoloru + 1) % lKolorow;
                }
            }

            int indeksStylu = 0;
            foreach (Algorytm alg in myGroup.ListaAlgorytmow)
            {
                if (alg == aReferencyjny)
                    continue;
                if (((alg.mozliwy) || ((alg.wymuszalny) && (wymStosNeiodpAlgToolStripMenuItem.Checked))) && ((alg.Obliczony) || (alg.wynikiAlg != null)))
                {
                    if (alg.Wybrany)
                    {
                        if (listBoxKlasy.SelectedItems.Count > 1)
                            indeksKoloru = 0;                   //Jest więcej niż jedna klasa. Każda klasa ma inny kolor. Każdy algorytm ma inny styl lini
                        foreach (trClass klasa in listBoxKlasy.SelectedItems)
                        {
                            if (alg.wynikiAlg.symulacja)
                            {
                                PointPairList przebiegBladE_plus = new PointPairList();
                                PointPairList przebiegBladE_minus = new PointPairList();
                                PointPairList przebiegBladE = new PointPairList();
                                for (int i = 0; i < myGroup.lBadan; i++)
                                {
                                    double aDod = alg.wynikiAlg.PobA(i);

                                    double wDodBlEMinus = (alg.wynikiAlg.PobE(i, klasa) - alg.wynikiAlg.PobBlE(i, klasa) - aReferencyjny.wynikiAlg.PobE(i, klasa)) / aReferencyjny.wynikiAlg.PobE(i, klasa);
                                    double wDodBlEPlus = (alg.wynikiAlg.PobE(i, klasa) + alg.wynikiAlg.PobBlE(i, klasa) - aReferencyjny.wynikiAlg.PobE(i, klasa)) / aReferencyjny.wynikiAlg.PobE(i, klasa);
                                    double wDodBlE = (alg.wynikiAlg.PobE(i, klasa) - aReferencyjny.wynikiAlg.PobE(i, klasa)) / aReferencyjny.wynikiAlg.PobE(i, klasa);

                                    przebiegBladE_plus.Add(aDod, wDodBlEPlus);
                                    przebiegBladE_minus.Add(aDod, wDodBlEMinus);
                                    przebiegBladE.Add(aDod, wDodBlE);

                                }

                                LineItem myCurveBlWzgl = myPaneDelta.AddCurve(alg.ToString(), przebiegBladE, kolorWykr[indeksKoloru], SymbolType.XCross);
                                LineItem myCurveBlWzglPlus = myPaneDelta.AddCurve(alg.ToString(), przebiegBladE_plus, kolorWykr[indeksKoloru], SymbolType.XCross);
                                LineItem myCurveBlWzglMinus = myPaneDelta.AddCurve(null, przebiegBladE_minus, kolorWykr[indeksKoloru], SymbolType.XCross);

                                if (indeksStylu != 0)
                                {
                                    myCurveBlWzglPlus.Line.Width = 3;   //dla zera jest linia ciągła, która przesłania inne linie się z nią pokrywające
                                    myCurveBlWzglMinus.Line.Width = 3;  //dla zera jest linia ciągła, która przesłania inne linie się z nią pokrywające
                                    myCurveBlWzgl.Line.Width = 3;       //dla zera jest linia ciągła, która przesłania inne linie się z nią pokrywające
                                }
                                else
                                {
                                    myCurveBlWzglPlus.Line.Width = 1;  //dla zera jest linia ciągła, która przesłania inne linie się z nią pokrywające
                                    myCurveBlWzglMinus.Line.Width = 1; //dla zera jest linia ciągła, która przesłania inne linie się z nią pokrywające
                                    myCurveBlWzgl.Line.Width = 2;      //dla zera jest linia ciągła, która przesłania inne linie się z nią pokrywające
                                }
                                myCurveBlWzglPlus.Line.Style = lStyle[indeksStylu];
                                myCurveBlWzglMinus.Line.Style = lStyle[indeksStylu];
                                myCurveBlWzgl.Line.Style = lStyle[indeksStylu];
                            }
                            else
                            {
                                PointPairList przebiegBlWzgl = new PointPairList();
                                for (int i = 0; i < myGroup.lBadan; i++)
                                {
                                    double aDod = alg.wynikiAlg.PobA(i);

                                    double wRef = aReferencyjny.wynikiAlg.PobE(i, klasa);
                                    double wDodE = alg.wynikiAlg.PobE(i, klasa);

                                    przebiegBlWzgl.Add(aDod, (wDodE - wRef) / wRef);
                                }

                                LineItem myCurveE = myPaneDelta.AddCurve(alg.ToString(), przebiegBlWzgl, kolorWykr[indeksKoloru], SymbolType.None);
                                if ((indeksStylu != 0) || (listBoxKlasy.SelectedItems.Count <= 1))
                                    myCurveE.Line.Width = 2; //dla zera jest linia ciągła, która przesłania inne linie się z nią pokrywające
                                myCurveE.Line.Style = lStyle[indeksStylu];


                            }
                            if (listBoxKlasy.SelectedItems.Count > 1)
                                indeksKoloru = (indeksKoloru + 1) % lKolorow;
                        }
                    }
                    if (listBoxKlasy.SelectedItems.Count <= 1)                     //Prezentowana jest tylko jedna klasa. Każdy algorytm wykreślony jest wtedy innym kolorem.
                        indeksKoloru = (indeksKoloru + 1) % lKolorow;
                    else                                                     //Prezentowanych jest wiele klas. Każda klasa ma inny kolor, a algorytm styl linii.
                        indeksStylu = (indeksStylu + 1) % lStyliLin;

                }
            }
            zedGraphControlBladWzgl.AxisChange();
            zedGraphControlBladWzgl.Refresh();
        }

        #endregion prezentacja wyników

        private void glOkno_Load(object sender, EventArgs e)
        {
            zedGraphControlWykrE.IsShowHScrollBar = true;
            zedGraphControlWykrE.IsShowVScrollBar = true;
            zedGraphControlWykrE.IsAutoScrollRange = true;
            zedGraphControlWykrE.IsScrollY2 = true;

            zedGraphControlWykrB.IsShowHScrollBar = true;
            zedGraphControlWykrB.IsShowVScrollBar = true;
            zedGraphControlWykrB.IsAutoScrollRange = true;
            zedGraphControlWykrB.IsScrollY2 = true;

            zedGraphControlWykrEB.IsShowHScrollBar = true;
            zedGraphControlWykrEB.IsShowVScrollBar = true;
            zedGraphControlWykrEB.IsAutoScrollRange = true;
            zedGraphControlWykrEB.IsScrollY2 = true;
        }

        #region Obsługa systemu

        #region obsługa skryptów

        private void CzytajSkrypt()
        {
            scriptCode kodOperacji = scriptCode.unknown;

            string CzKomenda;
            while ((CzKomenda = myScript.ReadLine()) != null)
            {
                string komenda = CzKomenda.Trim();
                if (komenda.Length == 0)
                    continue;
                if (komenda.StartsWith("#"))
                    continue;
                if (komenda.Contains("["))
                {
                    if (komenda.StartsWith("[no"))
                        wyczysc();
                    if (komenda.StartsWith("[ba"))
                    {
                        investigate();
                        prezentujWykresyE_B_EB();
                        prezentujWykresyPU();
                        prezentujTabele();
                        updateButtonsStates();
                    }
                    if (komenda.StartsWith("[po"))
                        kodOperacji = scriptCode.addSubGroup;
                    if (komenda.StartsWith("[rez"))
                        kodOperacji = scriptCode.reservationSettings;
                    if (komenda.StartsWith("[kl"))
                        kodOperacji = scriptCode.addTrClass;
                    if (komenda.StartsWith("[al"))
                        kodOperacji = scriptCode.addAlgorithm;
                    if (komenda.StartsWith("[ta"))
                        kodOperacji = scriptCode.saveTable;
                    if (komenda.StartsWith("[wyk"))
                        kodOperacji = scriptCode.saveChart;
                    if (komenda.StartsWith("[wyb"))
                        kodOperacji = scriptCode.chooseTraffiCclass;
                }
                else
                {
                    switch (kodOperacji)
                    {
                        case scriptCode.addTrClass:
                            dodajKlase(komenda);
                            break;
                        case scriptCode.addSubGroup:
                            dodajWiazke(komenda);
                            break;
                        case scriptCode.reservationSettings:
                            ustawRezerwacje(komenda);
                            break;
                        case scriptCode.addAlgorithm:
                            dodajAlgorytm(komenda);
                            break;
                        case scriptCode.saveTable:
                            zapiszTabele(komenda, true);
                            break;
                        case scriptCode.saveChart:
                            zapiszWykres(komenda);
                            break;
                        case scriptCode.chooseTraffiCclass:
                            wybKlase(komenda);
                            break;
                    }
                }
            }
            myScript.Close();
        }
        private void ustawRezerwacje(string parametry)
        {
            string[] pola = separations.Split(parametry);
            try
            {
                switch (pola[0])
                {
                    case "R1":
                    case "r1":
                    case "R2":
                    case "r2":
                        if (myGroup.AlgorytmRezerwacji != reservationAlgorithm.R1_R2)
                        {
                            myGroup.AlgorytmRezerwacji = reservationAlgorithm.R1_R2;
                            clearResults();
                            updateGroupInfo();
                        }
                        numericUpDownGranicaRezerwacji.Enabled = true;
                        labelGrRezerwacji.Enabled = true;
                        labelGrRezerwacji.Text = "Granica rezerwacji";
                        radioButtonR1.Checked = true;
                        break;

                    case "R3":
                    case "r3":
                        if (myGroup.AlgorytmRezerwacji != reservationAlgorithm.R3)
                        {
                            myGroup.AlgorytmRezerwacji = reservationAlgorithm.R3;
                            clearResults();
                            updateGroupInfo();
                        }
                        numericUpDownGranicaRezerwacji.Enabled = true;
                        labelGrRezerwacji.Enabled = true;
                        labelGrRezerwacji.Text = "Granica rezerwacji";
                        radioButtonR3.Checked = true;
                        break;
                    default:
                        if (myGroup.AlgorytmRezerwacji != reservationAlgorithm.none)
                        {
                            myGroup.AlgorytmRezerwacji = reservationAlgorithm.none;
                            clearResults();
                            updateGroupInfo();
                        }
                        numericUpDownGranicaRezerwacji.Enabled = false;
                        numericUpDownGranicaRezerwacji.Value = myGroup.V;
                        labelGrRezerwacji.Enabled = false;
                        labelGrRezerwacji.Text = "Granica rezerwacji";
                        radioButtonBrakRezerwacji.Checked = true;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Błąd przy próbie wczytania parametrów rezerwacji: " + parametry);
            }

            myGroup.q = int.Parse(pola[1]);
            numericUpDownGranicaRezerwacji.Value = myGroup.q;
            clearResults();
        }
        /// <summary>
        /// Usuwa wszyskie elementy z systemu (klasy i podgrupy)
        /// Metoda wykorzystywana przy skryptach sterujących pracą programu
        /// </summary>
        private void wyczysc()
        {
            updateListOfTrClasses();
            updateSubgroups();
            foreach (PodgrupaWiazek usuwane in listBoxPodgrupy.Items)
                myGroup.UsunPodgrupyLaczy(usuwane);
            listBoxPodgrupy.Items.Clear();
            foreach (trClass usuwana in listBoxKlasy.Items)
                myGroup.ListaKlasRuchu.Remove(usuwana);

            updateListOfTrClasses();
            clearResults();
            updateGroupInfo();
        }
        private void dodajKlase(string klasa)
        {
            string[] pola = separations.Split(klasa);
            string idKlasy = pola[0].ToLower();
            try
            {
                trClass nowa = null;
                if (idKlasy.Contains("er"))
                {
                    nowa = new trClassErlang(myGroup, int.Parse(pola[1]), int.Parse(pola[2]), double.Parse(pola[3]), pola[0].Contains("+"));
                    addTrClass(nowa);
                }
                if (idKlasy.Contains("en"))
                {
                    nowa = new trClassEngset(myGroup, int.Parse(pola[1]), int.Parse(pola[2]), double.Parse(pola[3]), pola[0].Contains("+"), int.Parse(pola[4]));
                    addTrClass(nowa);
                }
                if (idKlasy.Contains("pa"))
                {
                    nowa = new trClassPascal(myGroup, int.Parse(pola[1]), int.Parse(pola[2]), double.Parse(pola[3]), pola[0].Contains("+"), int.Parse(pola[4]));
                    addTrClass(nowa);
                }
                if (nowa != null)
                    if (!idKlasy.Contains("@"))
                    {
                        nowa.wybrana = true;
                        listBoxKlasy.SelectedItems.Add(nowa);
                    }
            }
            catch
            {
                MessageBox.Show("Błąd przy próbie wczytania klasy: " + klasa);
            }

        }
        private void dodajAlgorytm(string nazwaAlg)
        {
            foreach (Algorytm przegladany in myGroup.ListaAlgorytmow)
            {
                string nazwa = przegladany.ToString();
                if (nazwa.Contains(nazwaAlg))
                    przegladany.Wybrany = true;
            }
        }
        private void dodajWiazke(string podgrupy)
        {
            string[] pola = separations.Split(podgrupy);
            int v = int.Parse(pola[0]);
            myGroup.addSubgroup(1, v);
            listBoxPodgrupy.Items.Clear();
            foreach (Subgroup przegladana in myGroup.ListaPodgrupLaczy)
                listBoxPodgrupy.Items.Add(przegladana);
            clearResults();
            updateGroupInfo();
        }

        #endregion obsługa skryptów

        private void investigate()
        {
            myGroup.BadajSystem((double)numericUpDownMinRuchOf.Value, (double)numericUpDownMaxRuchOf.Value, (double)numericUpDownPrzyrRuchOf.Value, wymStosNeiodpAlgToolStripMenuItem.Checked);
        }

        public void dodajNowyAlgorytm(Algorytm dodawany)
        {
            myGroup.ListaAlgorytmow.Add(dodawany);
            updateGroupInfo();
        }
        public void addTrClass(trClass dodawana)
        {
            myGroup.ListaKlasRuchu.Add(dodawana);
            myGroup.ListaKlasRuchu.Sort();
            clearResults();
            updateListOfTrClasses();
            updateGroupInfo();
        }

        #endregion Obsługa systemu

        private void wybKlase(string komenda)
        {
            foreach (trClass temp in myGroup.ListaKlasRuchu)
                temp.wybrana = false;

            try
            {
                string[] WybKlasy = komenda.Split(' ');
                foreach (string str in WybKlasy)
                    myGroup.ListaKlasRuchu[int.Parse(str) - 1].wybrana = true;
            }
            catch
            {
                MessageBox.Show("Zły wybór klas do zapisu tabeli");
            }
        }

        #region Zapis do pliku
        /// <summary>
        /// Zapisywanie tabeli z wynikami prawdopodobieństwa blokady oraz strat
        /// </summary>
        /// <param name="nazwa">Nazwa pliku (zostaje ona rozszerzona o E.tex i B.tex)</param>
        /// <param name="TexMode">Tryb zapisu (true - zapis do TEXa, false - zapis do np. excela)</param>
        private void zapiszTabele(string nazwa, bool TexMode)
        {
            TextWriter wynikiE;
            TextWriter wynikiB;
            if (TexMode)
            {
                try
                {
                    wynikiE = new StreamWriter(nazwa + "E.tex");
                    wynikiB = new StreamWriter(nazwa + "B.tex");
                }
                catch
                {
                    MessageBox.Show("Nieudany zapis tabeli do pliku");
                    return;
                }
            }
            else
            {
                try
                {
                    wynikiE = new StreamWriter(nazwa + "E.txt");
                    wynikiB = new StreamWriter(nazwa + "B.txt");
                }
                catch
                {
                    MessageBox.Show("Nieudany zapis tabeli do pliku");
                    return;
                }
            }

            zapPlikNaglE(wynikiE, TexMode);
            zapPlikNaglB(wynikiB, TexMode);

            zapPlikErow(wynikiE, TexMode, nazwa + "E.dat");
            zapPlikBrow(wynikiB, TexMode);
            wynikiE.Close();
            wynikiB.Close();
        }
        private void zapiszWykres(string nazwa)
        {
            TextWriter wynikiE;
            TextWriter wynikiB;
            try
            {
                wynikiE = new StreamWriter(nazwa + "E.dat");
                wynikiB = new StreamWriter(nazwa + "B.dat");
            }
            catch
            {
                MessageBox.Show("Nieudany zapis tabeli do pliku");
                return;
            }

            zapPlikErow(wynikiE, false, nazwa + "E.dat");
            zapPlikBrow(wynikiB, false);
            wynikiE.Close();
            wynikiB.Close();
        }
        private void zapPlikNaglE(TextWriter plik, bool texTableMode)
        {
            if (texTableMode)
            {
                plik.Write(myGroup.ToString() + "\r\n");
                foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
                    plik.Write("\t" + wypKlasa.ToSkrString() + "\r\n");
                plik.Write(@"\begin{tabular}{|c");
                foreach (trClass wybKl in myGroup.ListaKlasRuchu)
                    if (wybKl.wybrana)
                        foreach (Algorytm alg in myGroup.ListaAlgorytmow)
                            if ((alg.Wybrany) && (alg.Obliczony))
                            {
                                plik.Write("|c");
                                if (alg.wynikiAlg.symulacja)
                                    plik.Write("|c");
                            }
                plik.Write(@"|}" + "\r\n" + @"\hline" + "\r\n");
            }
            plik.Write("a");

            foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
            {
                if (wypKlasa.wybrana)
                    foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                        if ((wypisywany.Obliczony) && (wypisywany.Wybrany == true))
                        {
                            plik.Write("\t");
                            if (texTableMode)//+ wypisywany.ToString());
                            {
                                if (wypisywany.wynikiAlg.symulacja)
                                    plik.Write(@"& " + wypisywany.ToString() + "\t" + @"& $\pm$");
                                else
                                    plik.Write(@"& " + wypisywany.ToString());
                            }
                            else
                            {
                                if (wypisywany.wynikiAlg.symulacja)
                                    plik.Write(wypisywany.ToString() + "\t+-");
                                else
                                    plik.Write(wypisywany.ToString());
                            }
                        }
            }
            if (texTableMode)
                plik.Write(@" \\" + "\r\n" + @"\hline \hline");
            plik.Write("\r\n");
        }
        private void zapPlikNaglB(TextWriter plik, bool texTableMode)
        {
            if (texTableMode)
            {
                plik.Write(@"\begin{tabular}{|c");
                foreach (trClass wybKl in myGroup.ListaKlasRuchu)
                    if (wybKl.wybrana)
                        foreach (Algorytm alg in myGroup.ListaAlgorytmow)
                            if ((alg.Wybrany) && (alg.Obliczony))
                                if (alg.wynikiAlg.pStrat)
                                {
                                    plik.Write("|c");
                                    if (alg.wynikiAlg.symulacja)
                                        plik.Write("|c");
                                }
                plik.Write(@"|}" + "\r\n" + @"\hline" + "\r\n");
            }
            plik.Write("a");

            foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
            {
                if (wypKlasa.wybrana)
                    foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                        if ((wypisywany.Obliczony) && (wypisywany.Wybrany == true))
                            if (wypisywany.wynikiAlg.pStrat)
                            {
                                plik.Write("\t");
                                if (texTableMode)//+ wypisywany.ToString());
                                {
                                    if (wypisywany.wynikiAlg.symulacja)
                                        plik.Write(@"& " + wypisywany.ToString() + "\t" + @"& $\pm$");
                                    else
                                        plik.Write(@"& " + wypisywany.ToString());
                                }
                                else
                                {
                                    if (wypisywany.wynikiAlg.symulacja)
                                        plik.Write(wypisywany.ToString() + "\t" + @"& +-");
                                    else
                                        plik.Write(wypisywany.ToString());
                                }
                            }
            }
            if (texTableMode)
                plik.Write(@" \\" + "\r\n" + @"\hline \hline");
            plik.Write("\r\n");
        }
        private void zapGnuSkrypt(TextWriter plik, double minWart, string EpsFileName, string osX, string osY, bool polski)
        {
            //Encoding encodingIn = Encoding.UTF8;
            //Encoding encodingOut = Encoding.GetEncoding("iso-8859-2");
            IFormatProvider stylDouble = CultureInfo.CreateSpecificCulture("en-US");
            plik.WriteLine("set terminal postscript enhanced \"Helvetica\" 8 ");
            plik.WriteLine("set encoding iso_8859_2");
            plik.WriteLine("set xrange [" + numericUpDownMinRuchOf.Value.ToString(stylDouble) + ":" + numericUpDownMaxRuchOf.Value.ToString(stylDouble) + "]");
            plik.WriteLine("set lmargin 9");
            if (osX != "")
                plik.WriteLine(string.Format("set xlabel \"{0}\"", osX));
            plik.WriteLine("set yrange [" + minWart.ToString(stylDouble) + ":1]");
            plik.WriteLine("set xtics " + numericUpDownPrzyrRuchOf.Value.ToString(stylDouble));
            plik.WriteLine("set xtics nomirror");
            //            plik.WriteLine("set format y \"10^{%L}\"");
            //            plik.WriteLine("#set format y \"%2.0t{/Symbol \\327}10^{%L}\"");
            double wart = minWart;
            if (minWart == 0)
                wart = 0.0000001;
            plik.Write("set ytics (");
            while (wart < 1)
            {
                double wart2 = 2 * wart;
                double wart3 = 3 * wart;
                double wart4 = 4 * wart;
                double wart5 = 5 * wart;
                double wart6 = 6 * wart;
                double wart7 = 7 * wart;
                double wart8 = 8 * wart;
                double wart9 = 9 * wart;

                //plik.Write(string.Format("{0}, \"\" {1}, {2}, \"\" {3}, \"\" {4}, {5},  \"\" {6}, \"\" {7}, \"\" {8}, ", wart, wart2, wart3, wart4, wart5, wart6, wart7, wart8, wart9));
                plik.Write(wart.ToString(stylDouble) + ", " + wart2.ToString(stylDouble) + ", \"\" " + wart3.ToString(stylDouble) + ", \"\" " + wart4.ToString(stylDouble) + ", " + wart5.ToString(stylDouble) + ", \"\" " + wart6.ToString(stylDouble) + ", \"\" " + wart8.ToString(stylDouble) + ", \"\" " + wart9.ToString(stylDouble) + ", ");
                wart *= 10;
            }
            plik.WriteLine("\"1\" 1)");
            plik.WriteLine("set logscale y");
            plik.WriteLine("set ytics nomirror");
            plik.WriteLine("set border 3");
            plik.WriteLine("set size 0.31, 0.44");
            plik.WriteLine("set grid");
            plik.WriteLine("set style line 1 lt 1 lw 0");
            plik.WriteLine("set key box linestyle 1");
            plik.WriteLine("set key right bottom");

            plik.WriteLine(string.Format("set ylabel \"{0}\" offset 1", osY));
            plik.WriteLine(string.Format("set output \"{0}.eps\"", EpsFileName.ToString()));

            int nrKolumny = 2;
            int nrKolumnySym = 3;
            bool ploted = false;

            int pt = 0;
            foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
                if (wypKlasa.wybrana)
                {
                    pt++;
                    int lt = 0;
                    foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                    {
                        if ((wypisywany.Obliczony) && (wypisywany.Wybrany))
                        {
                            lt++;
                            if (wypisywany.wynikiAlg.symulacja)
                            {
                                if (ploted == false)
                                {
                                    plik.Write("plot ");
                                    ploted = true;
                                }
                                else
                                {
                                    plik.Write(", ");
                                }
                                plik.Write(string.Format("\"{0}\" using 1:{1}:{2} notitle with yerrorbars lt 1 pt 0, ", EpsFileName, nrKolumny, nrKolumnySym));
                                plik.Write(string.Format("\"{0}\" using 1:{1} title'{3}:{4}' with points pt {5}", EpsFileName, nrKolumny, nrKolumnySym, wypisywany.ToSkrString(), wypKlasa.ToSkrString(), pt));
                            }
                            else
                            {
                                if (ploted == false)
                                {
                                    plik.Write("plot ");
                                    ploted = true;
                                }
                                else
                                {
                                    plik.Write(", \\\n     ");
                                }
                                plik.Write(string.Format("\"{0}\" using 1:{1} title '{2}:{3}' with lines lt {4}", EpsFileName, nrKolumny.ToString(), wypisywany.ToSkrString(), wypKlasa.ToSkrString(), lt));
                                //                                plik.Write("\"" + EpsFileName + "\" using 1:" + nrKolumny.ToString() + " title '" + wypKlasa.ToSkrString() + ": " + wypisywany.ToSkrString() + "' with lines");
                            }
                            nrKolumny++;
                            if (wypisywany.wynikiAlg.symulacja)
                                nrKolumny++;
                            nrKolumnySym = nrKolumny + 1;
                        }
                    }
                }
            plik.WriteLine("");
            plik.Close();
        }
        private void zapPlikErow(TextWriter plik, bool texTableMode, string nazwaPliku)
        {
            IFormatProvider stylDouble = CultureInfo.CreateSpecificCulture("en-US");
            double minWartosc = 1;
            string formatWyp = "f" + numericUpDownLMiejscE.Value.ToString();
            if (texTableMode == false)
                formatWyp = "f15";
            for (int nrBad = 0; nrBad < myGroup.lBadan; nrBad++)
            {
                bool wypA = false;
                foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                    if ((!wypA) && (wypisywany.Obliczony) && (wypisywany.Wybrany))
                    {
                        wypA = true;
                        if (texTableMode)
                            plik.Write(wypisywany.wynikiAlg.PobA(nrBad).ToString("f2"));
                        else
                            plik.Write(wypisywany.wynikiAlg.PobA(nrBad).ToString("f2", stylDouble));
                        break;
                    }
                foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
                    if (wypKlasa.wybrana)
                    {
                        foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                        {
                            if ((wypisywany.Obliczony) && (wypisywany.Wybrany))
                            {
                                plik.Write("\t");
                                if (texTableMode)
                                {
                                    if (wypisywany.wynikiAlg.symulacja)
                                    {
                                        plik.Write(@"& " + wypisywany.wynikiAlg.PobE(nrBad, wypKlasa).ToString(formatWyp));
                                        plik.Write(@"& " + wypisywany.wynikiAlg.PobBlE(nrBad, wypKlasa).ToString(formatWyp));
                                    }
                                    else
                                    {
                                        plik.Write(@"& " + wypisywany.wynikiAlg.PobE(nrBad, wypKlasa).ToString(formatWyp));
                                    }
                                }
                                else
                                {
                                    if (minWartosc > wypisywany.wynikiAlg.PobE(nrBad, wypKlasa))
                                        minWartosc = wypisywany.wynikiAlg.PobE(nrBad, wypKlasa);
                                    if (wypisywany.wynikiAlg.symulacja)
                                    {
                                        plik.Write(wypisywany.wynikiAlg.PobE(nrBad, wypKlasa).ToString(formatWyp, stylDouble));
                                        plik.Write("\t");
                                        plik.Write(wypisywany.wynikiAlg.PobBlE(nrBad, wypKlasa).ToString(formatWyp, stylDouble));
                                    }
                                    else
                                    {
                                        plik.Write(wypisywany.wynikiAlg.PobE(nrBad, wypKlasa).ToString(formatWyp, stylDouble));
                                    }
                                }
                            }
                        }
                    }
                if (texTableMode)
                    plik.Write(@" \\");
                plik.Write("\r\n");
            }
            if (texTableMode)
                plik.Write(@"\hline" + "\r\n" + @"\end{tabular}" + "\r\n");
            else
            {
                double minLog = 1;
                while (minLog > minWartosc)
                    minLog /= 10;

                TextWriter skrypt = new StreamWriter(nazwaPliku + ".gp", false, System.Text.Encoding.GetEncoding("iso-8859-1"));
                zapGnuSkrypt(skrypt, minLog, nazwaPliku, "ruch oferowany pojedynczej PJP: a", "prawdopodobieństwo blokady: B(t)", true);
            }
            plik.Close();
        }
        private void zapPlikBrow(TextWriter plik, bool texTableMode)
        {
            string formatWyp = "f" + numericUpDownLMiejscB.Value.ToString();
            for (int nrBad = 0; nrBad < myGroup.lBadan; nrBad++)
            {
                bool wypA = false;
                foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                    if ((!wypA) && (wypisywany.Obliczony) && (wypisywany.Wybrany))
                    {
                        wypA = true;
                        plik.Write(wypisywany.wynikiAlg.PobA(nrBad).ToString("f2"));
                        break;
                    }

                int nrKolumny = 2;
                foreach (trClass wypKlasa in myGroup.ListaKlasRuchu)
                    if (wypKlasa.wybrana)
                        foreach (Algorytm wypisywany in myGroup.ListaAlgorytmow)
                        {
                            if ((wypisywany.Obliczony) && (wypisywany.Wybrany))
                            {
                                if (wypisywany.wynikiAlg.pStrat)
                                {
                                    plik.Write("\t");
                                    if (texTableMode)
                                        plik.Write(@"& " + wypisywany.wynikiAlg.PobB(nrBad, wypKlasa).ToString(formatWyp));
                                    else
                                        plik.Write(wypisywany.wynikiAlg.PobB(nrBad, wypKlasa).ToString(formatWyp));

                                    if (wypisywany.wynikiAlg.symulacja)
                                    {
                                        plik.Write("\t");
                                        if (texTableMode)
                                            plik.Write(@"& " + wypisywany.wynikiAlg.PobBlB(nrBad, wypKlasa).ToString(formatWyp));
                                        else
                                            plik.Write(wypisywany.wynikiAlg.PobBlB(nrBad, wypKlasa).ToString(formatWyp));
                                    }
                                    nrKolumny++;
                                    if (wypisywany.wynikiAlg.symulacja)
                                        nrKolumny++;
                                }
                            }
                        }
                if (texTableMode)
                    plik.Write(@" \\");
                plik.Write("\r\n");
            }
            if (texTableMode)
                plik.Write(@"\hline" + "\r\n" + @"\end{tabular}" + "\r\n");
        }
        #endregion Zapis do pliku

        #region Obsługa kontrolek
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStartAddStop.Text == strStop)
            {
                cancelConnection();
            }
            if (buttonStartAddStop.Text == strDodajNowySystem)
            {
                myDataBase.addNewSystem(myGroup);

                List<BDsystem> systemy = myDataBase.dodajPredefiniowaneSystemy();
                comboBoxSystemy.Items.Clear();
                foreach (BDsystem tmp in systemy)
                    comboBoxSystemy.Items.Add(tmp);
                comboBoxSystemy.SelectedItem = comboBoxSystemy.Items[comboBoxSystemy.Items.Count - 1];

            }
            if (buttonStartAddStop.Text == strStart)
            {
                investigate();
            }
            updateButtonsStates();
            return;
        }

        private void buttonAddTrClass_Click(object sender, EventArgs e)
        {
            myGroup.sysNo = -1;
            trClass newTrClass;
            if (radioButtonErlang.Checked)
            {
                newTrClass = new trClassErlang(myGroup, (int)numericUpDownAt.Value, (int)numericUpDownT.Value, (double)numericUpDownMi.Value, checkBoxUprzywilejowana.Checked);
                addTrClass(newTrClass);
            }
            if (radioButtonEngset.Checked)
            {
                newTrClass = new trClassEngset(myGroup, (int)numericUpDownAt.Value, (int)numericUpDownT.Value, (double)numericUpDownMi.Value, checkBoxUprzywilejowana.Checked, (int)numericUpDownLiczbaZrRuchu.Value);
                addTrClass(newTrClass);
            }
            if (radioButtonPascal.Checked)
            {
                newTrClass = new trClassPascal(myGroup, (int)numericUpDownAt.Value, (int)numericUpDownT.Value, (double)numericUpDownMi.Value, checkBoxUprzywilejowana.Checked, (int)numericUpDownLiczbaZrRuchu.Value);
                addTrClass(newTrClass);
            }
        }
        private void buttonRemoveTrClass_Click(object sender, EventArgs e)
        {
            myGroup.sysNo = -1;
            //int nrWybKlasy = this.listBoxKlasy.SelectedIndex;
            foreach (trClass tmpToDelete in listBoxKlasy.SelectedItems)
                myGroup.ListaKlasRuchu.Remove(tmpToDelete);
            clearResults();
            updateListOfTrClasses();
            updateGroupInfo();
        }

        private void updateSubgroups()
        {
            listBoxPodgrupy.Items.Clear();
            foreach (PodgrupaWiazek przegladana in myGroup.ListaPodgrupLaczy)
                listBoxPodgrupy.Items.Add(przegladana);
        }

        private void buttonDodajWiazke_Click(object sender, EventArgs e)
        {
            myGroup.sysNo = -1;
            myGroup.addSubgroup((int)numericUpDownLiczbaPodgrupLzczy.Value, (int)numericUpDownPojemnoscLacza.Value);
            updateSubgroups();
            clearResults();
            updateGroupInfo();
        }

        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            myGroup.sysNo = -1; 
            myGroup.ListaPodgrupLaczy.Clear();
            updateSubgroups();
            updateGroupInfo();
        }

        private void numericUpDownNimRuchOf_ValueChanged(object sender, EventArgs e)
        {
            UpdateNumberOfInvestigations();
            clearResults();
        }
        private void numericUpDownMaxRuchOf_ValueChanged(object sender, EventArgs e)
        {
            UpdateNumberOfInvestigations();
            clearResults();
        }
        private void numericUpDownPrzyrRuchOf_ValueChanged(object sender, EventArgs e)
        {
            UpdateNumberOfInvestigations();
            clearResults();
        }

        private void radioButtonErlang_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownLiczbaZrRuchu.Visible = false;
            labelNumberOfTrSourcess.Visible = false;
        }
        private void radioButtonEngset_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownLiczbaZrRuchu.Visible = true;
            labelNumberOfTrSourcess.Text = "S";
            labelNumberOfTrSourcess.Visible = true;
        }
        private void radioButtonPascal_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownLiczbaZrRuchu.Visible = true;
            labelNumberOfTrSourcess.Text = "N";
            labelNumberOfTrSourcess.Visible = true;
        }
        private void radioButtonAlgLosowy_CheckedChanged(object sender, EventArgs e)
        {
            myGroup.aWybPodgr = subgroupChooseAlgorithm.random;
            clearResults();
            updateGroupInfo();
        }
        private void radioButtonAlgLosowyProporcjonalny_CheckedChanged(object sender, EventArgs e)
        {
            myGroup.aWybPodgr = subgroupChooseAlgorithm.randomCapacityProportional;
            clearResults();
            updateGroupInfo();
        }
        private void radioButtonAlgLosowyProporcjonalnyWolny_CheckedChanged(object sender, EventArgs e)
        {
            myGroup.aWybPodgr = subgroupChooseAlgorithm.randomOccupancyProportional;
            clearResults();
            updateGroupInfo();
        }
        private void radioButtonAlgKolejnosciowy_CheckedChanged(object sender, EventArgs e)
        {
            myGroup.aWybPodgr = subgroupChooseAlgorithm.sequence;
            clearResults();
            updateGroupInfo();
        }
        private void radioButtonAlgCykliczny_CheckedChanged(object sender, EventArgs e)
        {
            myGroup.aWybPodgr = subgroupChooseAlgorithm.RR;
            clearResults();
            updateGroupInfo();
        }
        private void radioButtonBrakRezerwacji_CheckedChanged(object sender, EventArgs e)
        {
            if (myGroup.AlgorytmRezerwacji != reservationAlgorithm.none)
            {
                myGroup.AlgorytmRezerwacji = reservationAlgorithm.none;
                clearResults();
                updateGroupInfo();
            }
            numericUpDownGranicaRezerwacji.Enabled = false;
            numericUpDownGranicaRezerwacji.Value = myGroup.V;
            labelGrRezerwacji.Enabled = false;
            labelGrRezerwacji.Text = "Granica rezerwacji";
        }
        private void radioButtonR1_R2_CheckedChanged(object sender, EventArgs e)
        {
            if (myGroup.AlgorytmRezerwacji != reservationAlgorithm.R1_R2)
            {
                myGroup.AlgorytmRezerwacji = reservationAlgorithm.R1_R2;
                clearResults();
                updateGroupInfo();

            }
            numericUpDownGranicaRezerwacji.Enabled = true;
            labelGrRezerwacji.Enabled = true;
            labelGrRezerwacji.Text = "Granica rezerwacji";
        }
        private void radioButtonR3_CheckedChanged(object sender, EventArgs e)
        {
            if (myGroup.AlgorytmRezerwacji != reservationAlgorithm.R3)
            {
                myGroup.AlgorytmRezerwacji = reservationAlgorithm.R3;
                clearResults();
                updateGroupInfo();
            }
            numericUpDownGranicaRezerwacji.Enabled = false;
            labelGrRezerwacji.Enabled = true;
            labelGrRezerwacji.Text = "Obszar rez. w podgrupie";
        }
        private void tabControlPrezentacja_Click(object sender, EventArgs e)
        {
            if ((PotrzebaModelowania) || (listBoxAlgorytmy.SelectedItems.Count == 0))
            {
                tabPageE.Hide();
                tabPageB.Hide();
                tabPageEBwykres.Hide();
            }
        }

        private void uaktualnijListeAlgRef()
        {
            Algorytm wybrany = listBoxAlgReferencyjny.SelectedItem as Algorytm;
            listBoxAlgReferencyjny.Items.Clear();
            foreach (Algorytm rozwazany in listBoxAlgorytmy.SelectedItems)
            {
                rozwazany.Wybrany = true;
                listBoxAlgReferencyjny.Items.Add(rozwazany);
            }
            if (wybrany != null)
            {
                if (listBoxAlgReferencyjny.Items.Contains(wybrany))
                    listBoxAlgReferencyjny.SelectedItem = wybrany;
                else
                    listBoxAlgReferencyjny.SelectedItems.Clear();
            }
            else
                listBoxAlgReferencyjny.SelectedItems.Clear();

            tabPageBladWzgl.Refresh();
        }

        private void listBoxAlgorytmy_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Algorytm rozwazany in listBoxAlgorytmy.Items)
                rozwazany.Wybrany = false;

            foreach (Algorytm rozwazany in listBoxAlgorytmy.SelectedItems)
            {
                rozwazany.Wybrany = true;
            }
            uaktualnijListeAlgRef();
            prezentujWykresyE_B_EB();
            prezentujWykresyPU();
            prezentujBladWzgledny(listBoxAlgReferencyjny.SelectedItem as Algorytm);
            prezentujTabele(); 
            updateButtonsStates();
        }
        private void listBoxKlasy_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (trClass kl in listBoxKlasy.Items)
                kl.wybrana = false;
            foreach (trClass kl in listBoxKlasy.SelectedItems)
                kl.wybrana = true;
            prezentujWykresyE_B_EB();
            prezentujWykresyPU();
            prezentujBladWzgledny(listBoxAlgReferencyjny.SelectedItem as Algorytm);
            prezentujTabele();
        }
        private void numericUpDownGranicaRezerwacji_ValueChanged(object sender, EventArgs e)
        {
            myGroup.q = (int)numericUpDownGranicaRezerwacji.Value;
            clearResults();
        }

        private void numericUpDownLMiejscB_ValueChanged(object sender, EventArgs e)
        {
            prezentujTabele();
        }

        private void numericUpDownLMiejscE_ValueChanged(object sender, EventArgs e)
        {
            prezentujTabele();
        }

        private void otworzSkryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogSkrypt.ShowDialog();
            if (openFileDialogSkrypt.CheckFileExists)
            {
                try
                {
                    string nazwaPliku = openFileDialogSkrypt.FileName;
                    if (nazwaPliku != "")
                    {
                        myScript = new StreamReader(nazwaPliku);
                        CzytajSkrypt();
                    }
                }
                catch (System.IO.FileNotFoundException exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
        }

        private void dodajAlgorytmMismToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAlgorytmIteracyjny formatka = new FormAlgorytmIteracyjny(this, true);
            formatka.Show();
        }
        private void dodajAlgorytmHyrydowyMISMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAlgorytmIteracyjny formatka = new FormAlgorytmIteracyjny(this, false);
            formatka.Show();
        }

        private void wymuszajStosowanieNeiodpowiednichAlgorytmówToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            updateGroupInfo();
        }

        private void EkspAlgToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            updateGroupInfo();
        }

        private void buttonProgiUstaw_Click(object sender, EventArgs e)
        {
            trClass nowa;
            if (radioButtonProgiErlang.Checked)
            {
                nowa = new trClassErlang(myGroup, (int)numericUpDownProgiPropAt.Value, 0, 0, checkBoxProgiUprzywilejowana.Checked);
                progiFmt temp = new progiFmt(nowa, (int)numericUpDownProgiLiczba.Value, this, false);
                temp.Show();
            }
            if (radioButtonProgiEngset.Checked)
            {
                nowa = new trClassEngset(myGroup, (int)numericUpDownProgiPropAt.Value, 0, 0, checkBoxProgiUprzywilejowana.Checked, (int)numericUpDownProgiS.Value);
                progiFmt temp = new progiFmt(nowa, (int)numericUpDownProgiLiczba.Value, this, false);
                temp.Show();
            }
            if (radioButtonProgiPascal.Checked)
            {
                nowa = new trClassPascal(myGroup, (int)numericUpDownProgiPropAt.Value, 0, 0, checkBoxProgiUprzywilejowana.Checked, (int)numericUpDownProgiS.Value);
                progiFmt temp = new progiFmt(nowa, (int)numericUpDownProgiLiczba.Value, this, false);
                temp.Show();
            }
        }

        private void radioButtonProgiErlang_CheckedChanged(object sender, EventArgs e)
        {
            labelProgiS.Visible = false;
            numericUpDownProgiS.Visible = false;
        }

        private void radioButtonProgiEngset_CheckedChanged(object sender, EventArgs e)
        {
            labelProgiS.Visible = true;
            numericUpDownProgiS.Visible = true;
        }

        private void radioButtonProgiPascal_CheckedChanged(object sender, EventArgs e)
        {
            labelProgiS.Visible = true;
            numericUpDownProgiS.Visible = true;
        }

        private void listBoxKlasy_DoubleClick(object sender, EventArgs e)
        {
            trClass edytowana = (trClass)listBoxKlasy.SelectedItem;
            if (edytowana != null)
            {
                if (edytowana.progiKlasy != null)
                {
                    progiFmt temp = new progiFmt(edytowana, 0, this, true);
                    temp.Show();
                }
                else
                {
                    FormEditTrafficClass klasaFrm = new FormEditTrafficClass(this, edytowana);
                    klasaFrm.Show();
                }
            }
            updateButtonsStates();
        }
        private void zapiszWynikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogGnuPlot.Title = "Zapisz prawdopodobieństwo blokady";

            string nazwaPliku = string.Format("V{0}.k{1}", myGroup.V, myGroup.sumaK);
            foreach (trClass krRuchu in myGroup.ListaKlasRuchu)
                nazwaPliku += "." + krRuchu.ToSkrStringFS();
            saveFileDialogGnuPlot.FileName = nazwaPliku;

            saveFileDialogGnuPlot.ShowDialog();
            string nazwa = saveFileDialogGnuPlot.FileName;
            if (nazwa != "")
            {
                TextWriter plik = new StreamWriter(nazwa, false);
                string[] samPlik = nazwa.Split('\\');
                zapPlikErow(plik, false, samPlik[samPlik.Length - 1]);
            }
        }

        private void zapiszWykresBGnuPlotToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            saveFileDialogGnuPlot.Title = "Zapisz prawdopodobieństwo strat";
            saveFileDialogGnuPlot.DefaultExt = "dat";
            saveFileDialogGnuPlot.ShowDialog();
            string nazwa = saveFileDialogGnuPlot.FileName;
            if (nazwa != "")
            {
                TextWriter plik = new StreamWriter(nazwa, false);
                zapPlikBrow(plik, false);
            }
        }

        private void zapiszTabeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogGnuPlot.Title = "Zapisz tabele z prawdopodobieńśtwem blokady TEX";
            saveFileDialogGnuPlot.DefaultExt = "";
            saveFileDialogGnuPlot.ShowDialog();
            string nazwa = saveFileDialogGnuPlot.FileName;
            if (nazwa != "")
            {
                string[] samPlik = nazwa.Split('\\');
                //zapPlikErow(plik, false, samPlik[samPlik.Length - 1]);
                zapiszTabele(nazwa, true);
            }
        }

        private void debugowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debug.Show();
            debug.Focus();
        }

        private void listBoxAlgReferencyjny_Click(object sender, EventArgs e)
        {
            prezentujBladWzgledny(listBoxAlgReferencyjny.SelectedItem as Algorytm);
        }

        private void tabPageModelSystemu_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;



            Pen myPen = new Pen(Color.Black, 1);
            Pen myPen2 = new Pen(Color.Black, 2);
            double poz_x = 100;
            int wx = tabPageModelSystemu.Width - (int)poz_x - 3;
            double poz_y = 20;
            int wy = tabPageModelSystemu.Height - (int)(2 * poz_y);
            if (myGroup.AlgorytmRezerwacji == reservationAlgorithm.R1_R2)
                wy -= 20;

            int OdstPomKl = 50;
            if (OdstPomKl * myGroup.m > wy - 20)
                OdstPomKl = (int)((wy - 20) / myGroup.m);

            g.Clear(Color.White);
            int nrKlasy = 0;
            foreach (trClass rysowana in myGroup.ListaKlasRuchu)
            {
                g.DrawLine(myPen2, 5, (int)(poz_y + 10 + nrKlasy * OdstPomKl), 95, (int)(poz_y + 10 + nrKlasy * OdstPomKl));
                g.DrawLine(myPen, 95, (int)(poz_y + 10 + nrKlasy * OdstPomKl), 88, (int)(poz_y + 10 + nrKlasy * OdstPomKl) + 3);
                g.DrawLine(myPen, 95, (int)(poz_y + 10 + nrKlasy * OdstPomKl) - 1, 88, (int)(poz_y + 10 + nrKlasy * OdstPomKl) - 4);
                Font fntKl = new Font("Verdana", 10);
                g.DrawString(rysowana.ToSkrString(), fntKl, new SolidBrush(Color.Black), 1, (float)(poz_y - 12 + nrKlasy * OdstPomKl));
                nrKlasy++;
            }

            if (myGroup.maxVi == 0) return;
            int maxVi = myGroup.maxVi;
            int lKolumn = myGroup.sumaK;

            double WysPJP = wy / maxVi;
            int odstPomKol = 10;
            int szerKol = 40;
            int odstep = 50;
            if (lKolumn * odstep > wx)
            {
                double _odstep = wx / lKolumn;
                odstPomKol = (int)(0.2 * _odstep);
                szerKol = (int)(0.8 * _odstep);
                odstep = (int)_odstep;
            }

            foreach (PodgrupaWiazek rysowana in myGroup.ListaPodgrupLaczy)
            {
                for (int i = 0; i < rysowana.k; i++)
                {
                    int stX = (int)(poz_x + odstPomKol);
                    LinearGradientBrush lBrush = new LinearGradientBrush(new Point(stX, 0), new Point(stX + szerKol, 0), Color.LightSkyBlue, Color.LightSteelBlue);
                    LinearGradientBrush lBrushRez = new LinearGradientBrush(new Point(stX, 0), new Point(stX + szerKol, 0), Color.LightCyan, Color.Red);
                    if (WysPJP > 12)
                    {
                        for (int l = 0; l < rysowana.v; l++)
                        {
                            g.FillRectangle(lBrush, stX, (int)(poz_y + l * WysPJP), szerKol, (int)(WysPJP));
                            g.DrawRectangle(myPen, stX, (int)(poz_y + l * WysPJP), szerKol, (int)(WysPJP));
                        }
                        if (myGroup.AlgorytmRezerwacji == reservationAlgorithm.R3)
                        {
                            for (int l = rysowana.v - myGroup.tMax + 1; l < rysowana.v; l++)
                            {
                                g.FillRectangle(lBrushRez, stX, (int)(poz_y + l * WysPJP), szerKol, (int)(WysPJP));
                                g.DrawRectangle(myPen, stX, (int)(poz_y + l * WysPJP), szerKol, (int)(WysPJP));
                            }
                        }
                    }
                    else
                    {
                        g.FillRectangle(lBrush, stX, (int)poz_y, szerKol, (int)(rysowana.v * WysPJP));
                        g.DrawRectangle(myPen, (int)(poz_x + odstPomKol), (int)poz_y, szerKol, (int)(rysowana.v * WysPJP));
                        if (myGroup.AlgorytmRezerwacji == reservationAlgorithm.R3)
                        {
                            g.FillRectangle(lBrushRez, stX, (int)(poz_y + (rysowana.v - myGroup.tMax) * WysPJP), szerKol, (int)(myGroup.tMax * WysPJP));
                        }
                    }
                    if (myGroup.sumaK <= 20)
                    {
                        Font fnt = new Font("Verdana", 16);
                        if (myGroup.sumaK > 12)
                            fnt = new Font("Verdana", 12);
                        if (myGroup.sumaK > 16)
                            fnt = new Font("Verdana", 10);
                        g.DrawString(rysowana.v.ToString(), fnt, new SolidBrush(Color.Black), stX, (float)(poz_y + (rysowana.v) * WysPJP));
                    }
                    poz_x += odstep;
                }
            }
        }

        #endregion Obsługa kontrolek


        private void glOkno_FormClosed(object sender, FormClosedEventArgs e)
        {
            cancelConnection();
        }

        private void cancelConnection()
        {
            myGroup.PrzerwijBadanieSystemu();
        }

        private void comboBoxSystemy_SelectedValueChanged(object sender, EventArgs e)
        {
            wyczysc();
            BDsystem wybrSystem = comboBoxSystemy.SelectedItem as BDsystem;
            int systemId = wybrSystem.id;
            myGroup.sysNo = systemId;

            for (int nrLacza = 0; nrLacza < BDsystem.maxLpodgrup; nrLacza++)
                if (wybrSystem.podgrupy[nrLacza].k > 0)
                    myGroup.addSubgroup(wybrSystem.podgrupy[nrLacza].k, wybrSystem.podgrupy[nrLacza].v);

            myGroup.AlgorytmRezerwacji = wybrSystem.algRez;
            myGroup.aWybPodgr = wybrSystem.algWybPodgr;
            myGroup.q = wybrSystem.q;

            myDataBase.wczytajKlasyZgloszen(wybrSystem, myGroup);
            foreach (BDklasaZgl tmp in wybrSystem.klasy)
                addTrClass(tmp.klasaZgl);

            updateSubgroups();
            clearResults();
            updateGroupInfo();
        }

        private void comboBoxParametrySymulacji_SelectedValueChanged(object sender, EventArgs e)
        {
            DataBaseSimulPar parametrySym = comboBoxParametrySymulacji.SelectedItem as DataBaseSimulPar;
            if (parametrySym != null)
            {
                clearResults();
                myGroup.lSerii = parametrySym.lSeri;
                myGroup.lSymUtrZgl = parametrySym.lUtrZgl;
                myGroup.CzStartu = parametrySym.czStartu;
                myGroup.parSymulacji = parametrySym.id;
            }
        }
    }
}