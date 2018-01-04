using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Algorithms;
using ModelGroup;

namespace GUI
{
    public partial class DebugOptions : Form
    {
        Wiazka mWiazka;
        public DebugOptions(Wiazka mWiazka)
        {
            InitializeComponent();
            this.mWiazka = mWiazka;
        }

        public DebugPar opcje
        {
            get
            {
                DebugPar wynik = new DebugPar(mWiazka);
                return wynik;
            }
        }
    }
}