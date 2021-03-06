using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModelGroup;

namespace GUI
{
    public partial class progiFmt : Form
    {
        /// <summary>Liczba przedziałów, o jeden większa niż liczba progów.</summary>
        private int liczbaPrzedzialow;
        private trClass nowaKlasa;

        private System.Windows.Forms.Label[] labelNrProgu;
        private System.Windows.Forms.Label[] labelWspIntensywnosci;
        private System.Windows.Forms.Label[] labelT;
        private System.Windows.Forms.Label[] labelMu;
        private System.Windows.Forms.Label[] labelMaksN;
        private System.Windows.Forms.NumericUpDown[] numericUpDownT;
        private System.Windows.Forms.NumericUpDown[] numericUpDownWspIntensywnosci;
        private System.Windows.Forms.NumericUpDown[] numericUpDownMu;
        private System.Windows.Forms.NumericUpDown[] numericUpDownMaksN;

        /// <summary>Główna formatka</summary>
        /// <remarks>Tam jest metoda, która dodaje nową klasę</remarks>
        private FormMainWindow Master;
        private bool dodano;

        /// <summary>
        /// Wczytywanie parametrów każdego z progów
        /// </summary>
        /// <param name="nowaKlasa">Nowa klasa, która Zostanie dodana, gdy zostaną podane odpowiednio progi</param>
        /// <param name="lProgow">Liczba Progów. Zostanie utworzona o jeden większa liczba przedziałów</param>
        /// <param name="parent">Główna klasa formatki</param>
        /// <param name="dodanoKlase">Informacja o tym, czy klasa została już wcześneij dodana, czy nie</param>
        public progiFmt(trClass nowaKlasa, int lProgow, FormMainWindow parent, bool dodanoKlase)
        {
            Master = parent;
            dodano = dodanoKlase;

            this.nowaKlasa = nowaKlasa;
            if (dodano == true)
                lProgow = nowaKlasa.progiKlasy.liczbaPrzedziałow -1;

            liczbaPrzedzialow = lProgow + 1;
            if (dodanoKlase == false)
                nowaKlasa.progiKlasy = new progiKlasy(lProgow);
            InitializeComponent();
            labelNrProgu = new Label[liczbaPrzedzialow];
            labelWspIntensywnosci = new Label[liczbaPrzedzialow];
            numericUpDownWspIntensywnosci = new NumericUpDown[liczbaPrzedzialow];
            labelT = new Label[liczbaPrzedzialow];
            numericUpDownT = new NumericUpDown[liczbaPrzedzialow];
            labelMu = new Label[liczbaPrzedzialow];
            numericUpDownMu = new NumericUpDown[liczbaPrzedzialow];
            labelMaksN = new Label[liczbaPrzedzialow];
            numericUpDownMaksN = new NumericUpDown[liczbaPrzedzialow];

            this.Height = 125 + 50 * liczbaPrzedzialow;

            for (int i = 0; i < liczbaPrzedzialow; i++)
            {
                int nrProgu = i + 1;

                labelNrProgu[i] = new Label();
                labelNrProgu[i].AutoSize = true;
                labelNrProgu[i].Location = new System.Drawing.Point(10, 100 + 50 * i);
                labelNrProgu[i].Name = "Przedział nr " + nrProgu.ToString();
                labelNrProgu[i].Size = new System.Drawing.Size(110, 13);
                labelNrProgu[i].TabIndex = 10 * i + 10;
                labelNrProgu[i].Text = "Próg nr " + nrProgu.ToString();
                this.Controls.Add(this.labelNrProgu[i]);

                labelWspIntensywnosci[i] = new Label();
                labelWspIntensywnosci[i].AutoSize = true;
                labelWspIntensywnosci[i].Location = new System.Drawing.Point(15, 120 + 50 * i);
                labelWspIntensywnosci[i].Size = new System.Drawing.Size(110, 13);
                labelWspIntensywnosci[i].TabIndex = 10 * i + 11;
                labelWspIntensywnosci[i].Text = "mnożnik A";
                this.Controls.Add(this.labelWspIntensywnosci[i]);

                numericUpDownWspIntensywnosci[i] = new NumericUpDown();
                numericUpDownWspIntensywnosci[i].Location = new Point(75, 117 + 50 * i);
                numericUpDownWspIntensywnosci[i].Size = new Size(50, 16);
                numericUpDownWspIntensywnosci[i].Minimum = 0;
                numericUpDownWspIntensywnosci[i].Maximum = 10;
                numericUpDownWspIntensywnosci[i].Value = (decimal) nowaKlasa.progiKlasy[i].mnoznikIntensywnosci;
                numericUpDownWspIntensywnosci[i].DecimalPlaces = 2;
                numericUpDownWspIntensywnosci[i].Increment = 0.05M;
                numericUpDownWspIntensywnosci[i].TabIndex = 10 * i + 12;

                this.Controls.Add(this.numericUpDownWspIntensywnosci[i]);

                labelT[i] = new Label();
                labelT[i].AutoSize = true;
                labelT[i].Location = new System.Drawing.Point(130, 120 + 50 * i);
                labelT[i].Size = new System.Drawing.Size(110, 13);
                labelT[i].TabIndex = 10 * i + 13;
                labelT[i].Text = "t";
                this.Controls.Add(this.labelT[i]);

                numericUpDownT[i] = new NumericUpDown();
                numericUpDownT[i].Location = new Point(140, 117 + 50 * i);
                numericUpDownT[i].Size = new Size(50, 16);
                numericUpDownT[i].Minimum = 1;
                numericUpDownT[i].Maximum = 100;
                numericUpDownT[i].Value = nowaKlasa.progiKlasy[i].t;
                numericUpDownT[i].Increment = 1;
                numericUpDownT[i].TabIndex = 10 * i + 14;
                this.Controls.Add(this.numericUpDownT[i]);

                labelMu[i] = new Label();
                labelMu[i].AutoSize = true;
                labelMu[i].Location = new System.Drawing.Point(190, 120 + 50 * i);
                labelMu[i].Size = new System.Drawing.Size(110, 13);
                labelMu[i].TabIndex = 10 * i + 15;
                labelMu[i].Text = "μ";
                this.Controls.Add(this.labelMu[i]);

                numericUpDownMu[i] = new NumericUpDown();
                numericUpDownMu[i].Location = new Point(215, 117 + 50 * i);
                numericUpDownMu[i].Size = new Size(50, 16);
                numericUpDownMu[i].Minimum = 0.0001M;
                numericUpDownMu[i].Maximum = 10;
                numericUpDownMu[i].Value = (decimal) nowaKlasa.progiKlasy[i]._mu;
                numericUpDownMu[i].DecimalPlaces = 2;
                numericUpDownMu[i].Increment = 0.05M;
                numericUpDownMu[i].TabIndex = 10 * i + 16;
                this.Controls.Add(this.numericUpDownMu[i]);

                if (i != liczbaPrzedzialow - 1)
                {
                    labelMaksN[i] = new Label();
                    labelMaksN[i].AutoSize = true;
                    labelMaksN[i].Location = new System.Drawing.Point(270, 170 + 50 * i);
                    labelMaksN[i].Size = new System.Drawing.Size(110, 13);
                    labelMaksN[i].TabIndex = 10 * i + 17;
                    labelMaksN[i].Text = "próg od";
                    this.Controls.Add(this.labelMaksN[i]);

                    numericUpDownMaksN[i] = new NumericUpDown();
                    numericUpDownMaksN[i].Location = new Point(330, 167 + 50 * i);
                    numericUpDownMaksN[i].Size = new Size(50, 16);
                    numericUpDownMaksN[i].Minimum = 1;
                    numericUpDownMaksN[i].Maximum = 10000;
                    numericUpDownMaksN[i].Value = 1;
                    numericUpDownMaksN[i].DecimalPlaces = 0;
                    numericUpDownMaksN[i].Increment = 1;
                    numericUpDownMaksN[i].TabIndex = 10 * i + 18;
                    numericUpDownMaksN[i].ValueChanged += new System.EventHandler(this.numericUpDownMaksNValueChanged);

                    this.Controls.Add(this.numericUpDownMaksN[i]);
                }
            }
            radioButtonAconst.Checked = true;
            this.Refresh();
        }

        private void numericUpDownMaksNValueChanged(object sender, EventArgs e)
        {
            for (int i = 1; i < liczbaPrzedzialow-1; i++)
                if (numericUpDownMaksN[i].Value < numericUpDownMaksN[i - 1].Value)
                    numericUpDownMaksN[i].Value = numericUpDownMaksN[i - 1].Value;
        }

        private void radioButtonAconstMiTconst(object sender, EventArgs e)
        {
            for (int i = 0; i < liczbaPrzedzialow; i++)
            {
                numericUpDownWspIntensywnosci[i].Enabled = false;
                numericUpDownWspIntensywnosci[i].Value = 1;
            }
        }

        private void radioButtonANotConstans(object sender, EventArgs e)
        {
            for (int i = 0; i < liczbaPrzedzialow; i++)
            {
                numericUpDownWspIntensywnosci[i].Enabled = true;
            }
        }

        private void buttonProgiDodaj_Click(object sender, EventArgs e)
        {
            int MinProg = 0;
            for (int i = 0; i < liczbaPrzedzialow; i++)
            {
                if (i != 0)
                    nowaKlasa.progiKlasy[i].minN = MinProg;

                if (i != liczbaPrzedzialow - 1)
                {
                    MinProg = (int)numericUpDownMaksN[i].Value;
                    nowaKlasa.progiKlasy[i].maksN = (int)numericUpDownMaksN[i].Value;
                }

                nowaKlasa.progiKlasy[i]._mu = (double)numericUpDownMu[i].Value;
                nowaKlasa.progiKlasy[i].mnoznikIntensywnosci = (double)numericUpDownWspIntensywnosci[i].Value;
                nowaKlasa.progiKlasy[i].t = (int)numericUpDownT[i].Value;
            }
            if (dodano == false)
                Master.addTrClass(nowaKlasa);
            Master.clearResults();
            Master.updateButtonsStates();
            Master.updateListOfTrClasses();
            this.Close();
        }

        private void buttonProgiAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}