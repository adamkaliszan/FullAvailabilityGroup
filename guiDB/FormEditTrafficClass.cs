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
    public partial class FormEditTrafficClass : Form
    {
        private FormMainWindow master;
        private trClass klasaEdytowana;

        public FormEditTrafficClass(FormMainWindow parent, trClass edytowana)
        {
            master = parent;
            InitializeComponent();
            klasaEdytowana = edytowana;

            numericUpDownEdycjaKlasyAt.Value = (decimal)edytowana.atProp;
            numericUpDownEdycjaKlasyT.Value = (decimal)edytowana.t;
            numericUpDownEdycjaKlasyMu.Value = (decimal)edytowana.mu;

            if (edytowana.typ == trClass.typKlasy.ENGSET)
            {
                labelEdycjaKlasyS.Visible = true;
                numericUpDownEdycjaKlasyS.Visible = true;
                trClassEngset kl = (trClassEngset)klasaEdytowana;
                numericUpDownEdycjaKlasyS.Value = (decimal)kl.S;
            }
            if (edytowana.typ == trClass.typKlasy.PASCAL)
            {
                labelEdycjaKlasyS.Visible = true;
                numericUpDownEdycjaKlasyS.Visible = true;
                trClassPascal kl = (trClassPascal)klasaEdytowana;
                numericUpDownEdycjaKlasyS.Value = (decimal)kl.S;
            }
            if (edytowana.typ == trClass.typKlasy.ERLANG)
            {
                labelEdycjaKlasyS.Visible = false;
                numericUpDownEdycjaKlasyS.Visible = false;
            }
        }

        private void buttonEdycjaKlasyAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdycjaKlasyZmien_Click(object sender, EventArgs e)
        {
            klasaEdytowana.atProp = (int)numericUpDownEdycjaKlasyAt.Value;
            klasaEdytowana.t = (int)numericUpDownEdycjaKlasyT.Value;
            klasaEdytowana.mu = (double)numericUpDownEdycjaKlasyMu.Value;
            if (klasaEdytowana.typ == trClass.typKlasy.ENGSET)
            {
                trClassEngset tmp = (trClassEngset)klasaEdytowana;
                tmp.S = (int)numericUpDownEdycjaKlasyS.Value;
            }
            if (klasaEdytowana.typ == trClass.typKlasy.PASCAL)
            {
                trClassPascal tmp = (trClassPascal)klasaEdytowana;
                tmp.S = (int)numericUpDownEdycjaKlasyS.Value;
            }
            klasaEdytowana.uprzywilejowana = checkBoxEdycjaKlasyUprzywilejowana.Checked;
            master.clearResults();
            master.updateButtonsStates();
            master.updateListOfTrClasses();
            this.Close();
        }
    }
}