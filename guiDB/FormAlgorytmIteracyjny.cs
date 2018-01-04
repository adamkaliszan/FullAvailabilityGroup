using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Algorithms.reccurence;
using Algorithms.hybrid;


namespace GUI
{
    public partial class FormAlgorytmIteracyjny : Form
    {
        private FormMainWindow parent;
        private bool algRek;
        public FormAlgorytmIteracyjny(FormMainWindow parent, bool rekurencyjny)
        {
            this.parent = parent;
            algRek = rekurencyjny;
            InitializeComponent();
        }


        private void buttonDodajIteracyjny_Click(object sender, EventArgs e)
        {
            if (algRek)
            {
                aRobertsIteracyjny algorytm;

                if (radioButtonWybranoIteracje.Checked)
                    algorytm = new aRobertsIteracyjny(parent.myGroup, 0, (int)numericUpDownLiczbaIteracji.Value);
                else
                    algorytm = new aRobertsIteracyjny(parent.myGroup, (double)numericUpDownEpsilon.Value, 0);

                parent.dodajNowyAlgorytm(algorytm);
                this.Close();
            }
            else
            {
                aHybrydowyMISM algorytm;

                if (radioButtonWybranoIteracje.Checked)
                    algorytm = new aHybrydowyMISM(parent.myGroup, 0, (int)numericUpDownLiczbaIteracji.Value);
                else
                    algorytm = new aHybrydowyMISM(parent.myGroup, (double)numericUpDownEpsilon.Value, 0);

                parent.dodajNowyAlgorytm(algorytm);
                this.Close();
            }
        }
    }
}