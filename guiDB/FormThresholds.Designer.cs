namespace GUI
{
    partial class progiFmt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonAconst = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonANotConst = new System.Windows.Forms.RadioButton();
            this.buttonProgiDodaj = new System.Windows.Forms.Button();
            this.buttonProgiAnuluj = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonAconst
            // 
            this.radioButtonAconst.AutoSize = true;
            this.radioButtonAconst.Location = new System.Drawing.Point(6, 19);
            this.radioButtonAconst.Name = "radioButtonAconst";
            this.radioButtonAconst.Size = new System.Drawing.Size(174, 17);
            this.radioButtonAconst.TabIndex = 2;
            this.radioButtonAconst.TabStop = true;
            this.radioButtonAconst.Text = "λ lub γ stałe dla każdego progu";
            this.radioButtonAconst.UseVisualStyleBackColor = true;
            this.radioButtonAconst.CheckedChanged += new System.EventHandler(this.radioButtonAconstMiTconst);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonANotConst);
            this.groupBox1.Controls.Add(this.radioButtonAconst);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 68);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "relacje: a, μ, t";
            // 
            // radioButtonANotConst
            // 
            this.radioButtonANotConst.AutoSize = true;
            this.radioButtonANotConst.Location = new System.Drawing.Point(6, 42);
            this.radioButtonANotConst.Name = "radioButtonANotConst";
            this.radioButtonANotConst.Size = new System.Drawing.Size(140, 17);
            this.radioButtonANotConst.TabIndex = 3;
            this.radioButtonANotConst.TabStop = true;
            this.radioButtonANotConst.Text = "λ lub γ zależne od progu";
            this.radioButtonANotConst.UseVisualStyleBackColor = true;
            this.radioButtonANotConst.CheckedChanged += new System.EventHandler(this.radioButtonANotConstans);
            // 
            // buttonProgiDodaj
            // 
            this.buttonProgiDodaj.Location = new System.Drawing.Point(307, 54);
            this.buttonProgiDodaj.Name = "buttonProgiDodaj";
            this.buttonProgiDodaj.Size = new System.Drawing.Size(73, 26);
            this.buttonProgiDodaj.TabIndex = 4;
            this.buttonProgiDodaj.Text = "Dodaj";
            this.buttonProgiDodaj.UseVisualStyleBackColor = true;
            this.buttonProgiDodaj.Click += new System.EventHandler(this.buttonProgiDodaj_Click);
            // 
            // buttonProgiAnuluj
            // 
            this.buttonProgiAnuluj.Location = new System.Drawing.Point(307, 12);
            this.buttonProgiAnuluj.Name = "buttonProgiAnuluj";
            this.buttonProgiAnuluj.Size = new System.Drawing.Size(73, 26);
            this.buttonProgiAnuluj.TabIndex = 5;
            this.buttonProgiAnuluj.Text = "Anuluj";
            this.buttonProgiAnuluj.UseVisualStyleBackColor = true;
            this.buttonProgiAnuluj.Click += new System.EventHandler(this.buttonProgiAnuluj_Click);
            // 
            // progiFmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 127);
            this.Controls.Add(this.buttonProgiAnuluj);
            this.Controls.Add(this.buttonProgiDodaj);
            this.Controls.Add(this.groupBox1);
            this.Name = "progiFmt";
            this.Text = "Edycja progów";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonAconst;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonANotConst;
        private System.Windows.Forms.Button buttonProgiDodaj;
        private System.Windows.Forms.Button buttonProgiAnuluj;
    }
}