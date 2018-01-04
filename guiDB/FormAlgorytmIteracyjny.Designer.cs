namespace GUI
{
    partial class FormAlgorytmIteracyjny
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonWybranoEpsilon = new System.Windows.Forms.RadioButton();
            this.radioButtonWybranoIteracje = new System.Windows.Forms.RadioButton();
            this.numericUpDownEpsilon = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLiczbaIteracji = new System.Windows.Forms.NumericUpDown();
            this.buttonDodajIteracyjny = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiczbaIteracji)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonWybranoEpsilon);
            this.groupBox1.Controls.Add(this.radioButtonWybranoIteracje);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Warunek zakoñczenia iteracji";
            // 
            // radioButtonWybranoEpsilon
            // 
            this.radioButtonWybranoEpsilon.AutoSize = true;
            this.radioButtonWybranoEpsilon.Location = new System.Drawing.Point(6, 39);
            this.radioButtonWybranoEpsilon.Name = "radioButtonWybranoEpsilon";
            this.radioButtonWybranoEpsilon.Size = new System.Drawing.Size(219, 17);
            this.radioButtonWybranoEpsilon.TabIndex = 1;
            this.radioButtonWybranoEpsilon.Text = "maksymalny b³¹d by³ mniejszy od Epsilon";
            this.radioButtonWybranoEpsilon.UseVisualStyleBackColor = true;
            // 
            // radioButtonWybranoIteracje
            // 
            this.radioButtonWybranoIteracje.AutoSize = true;
            this.radioButtonWybranoIteracje.Checked = true;
            this.radioButtonWybranoIteracje.Location = new System.Drawing.Point(6, 16);
            this.radioButtonWybranoIteracje.Name = "radioButtonWybranoIteracje";
            this.radioButtonWybranoIteracje.Size = new System.Drawing.Size(186, 17);
            this.radioButtonWybranoIteracje.TabIndex = 0;
            this.radioButtonWybranoIteracje.TabStop = true;
            this.radioButtonWybranoIteracje.Text = "wykonano okreœlon¹ liczbê iteracji";
            this.radioButtonWybranoIteracje.UseVisualStyleBackColor = true;
            // 
            // numericUpDownEpsilon
            // 
            this.numericUpDownEpsilon.DecimalPlaces = 6;
            this.numericUpDownEpsilon.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownEpsilon.Location = new System.Drawing.Point(129, 109);
            this.numericUpDownEpsilon.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEpsilon.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            589824});
            this.numericUpDownEpsilon.Name = "numericUpDownEpsilon";
            this.numericUpDownEpsilon.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownEpsilon.TabIndex = 3;
            this.numericUpDownEpsilon.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // numericUpDownLiczbaIteracji
            // 
            this.numericUpDownLiczbaIteracji.Location = new System.Drawing.Point(129, 83);
            this.numericUpDownLiczbaIteracji.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLiczbaIteracji.Name = "numericUpDownLiczbaIteracji";
            this.numericUpDownLiczbaIteracji.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownLiczbaIteracji.TabIndex = 2;
            this.numericUpDownLiczbaIteracji.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // buttonDodajIteracyjny
            // 
            this.buttonDodajIteracyjny.Location = new System.Drawing.Point(226, 83);
            this.buttonDodajIteracyjny.Name = "buttonDodajIteracyjny";
            this.buttonDodajIteracyjny.Size = new System.Drawing.Size(45, 46);
            this.buttonDodajIteracyjny.TabIndex = 1;
            this.buttonDodajIteracyjny.Text = "OK";
            this.buttonDodajIteracyjny.UseVisualStyleBackColor = true;
            this.buttonDodajIteracyjny.Click += new System.EventHandler(this.buttonDodajIteracyjny_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "liczba iteracji";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Epsilon";
            // 
            // FormAlgorytmIteracyjny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 136);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownLiczbaIteracji);
            this.Controls.Add(this.numericUpDownEpsilon);
            this.Controls.Add(this.buttonDodajIteracyjny);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAlgorytmIteracyjny";
            this.Text = "FormAlgorytmIteracyjny";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiczbaIteracji)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonWybranoEpsilon;
        private System.Windows.Forms.RadioButton radioButtonWybranoIteracje;
        private System.Windows.Forms.NumericUpDown numericUpDownEpsilon;
        private System.Windows.Forms.NumericUpDown numericUpDownLiczbaIteracji;
        private System.Windows.Forms.Button buttonDodajIteracyjny;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}