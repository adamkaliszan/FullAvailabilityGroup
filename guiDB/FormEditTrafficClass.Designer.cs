namespace GUI
{
    partial class FormEditTrafficClass
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
            this.buttonEdycjaKlasyAnuluj = new System.Windows.Forms.Button();
            this.buttonEdycjaKlasyZmien = new System.Windows.Forms.Button();
            this.labelEdycjaKlasyAt = new System.Windows.Forms.Label();
            this.labelEdycjaKlasyT = new System.Windows.Forms.Label();
            this.labelEdycjaKlasyMu = new System.Windows.Forms.Label();
            this.labelEdycjaKlasyS = new System.Windows.Forms.Label();
            this.numericUpDownEdycjaKlasyAt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEdycjaKlasyMu = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEdycjaKlasyT = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEdycjaKlasyS = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEdycjaKlasyUprzywilejowana = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyMu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyS)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEdycjaKlasyAnuluj
            // 
            this.buttonEdycjaKlasyAnuluj.Location = new System.Drawing.Point(278, 7);
            this.buttonEdycjaKlasyAnuluj.Name = "buttonEdycjaKlasyAnuluj";
            this.buttonEdycjaKlasyAnuluj.Size = new System.Drawing.Size(75, 23);
            this.buttonEdycjaKlasyAnuluj.TabIndex = 0;
            this.buttonEdycjaKlasyAnuluj.Text = "Anuluj";
            this.buttonEdycjaKlasyAnuluj.UseVisualStyleBackColor = true;
            this.buttonEdycjaKlasyAnuluj.Click += new System.EventHandler(this.buttonEdycjaKlasyAnuluj_Click);
            // 
            // buttonEdycjaKlasyZmien
            // 
            this.buttonEdycjaKlasyZmien.Location = new System.Drawing.Point(278, 45);
            this.buttonEdycjaKlasyZmien.Name = "buttonEdycjaKlasyZmien";
            this.buttonEdycjaKlasyZmien.Size = new System.Drawing.Size(75, 23);
            this.buttonEdycjaKlasyZmien.TabIndex = 1;
            this.buttonEdycjaKlasyZmien.Text = "Zmień";
            this.buttonEdycjaKlasyZmien.UseVisualStyleBackColor = true;
            this.buttonEdycjaKlasyZmien.Click += new System.EventHandler(this.buttonEdycjaKlasyZmien_Click);
            // 
            // labelEdycjaKlasyAt
            // 
            this.labelEdycjaKlasyAt.AutoSize = true;
            this.labelEdycjaKlasyAt.Location = new System.Drawing.Point(12, 9);
            this.labelEdycjaKlasyAt.Name = "labelEdycjaKlasyAt";
            this.labelEdycjaKlasyAt.Size = new System.Drawing.Size(64, 13);
            this.labelEdycjaKlasyAt.TabIndex = 2;
            this.labelEdycjaKlasyAt.Text = "Proporcje at";
            // 
            // labelEdycjaKlasyT
            // 
            this.labelEdycjaKlasyT.AutoSize = true;
            this.labelEdycjaKlasyT.Location = new System.Drawing.Point(206, 9);
            this.labelEdycjaKlasyT.Name = "labelEdycjaKlasyT";
            this.labelEdycjaKlasyT.Size = new System.Drawing.Size(10, 13);
            this.labelEdycjaKlasyT.TabIndex = 3;
            this.labelEdycjaKlasyT.Text = "t";
            // 
            // labelEdycjaKlasyMu
            // 
            this.labelEdycjaKlasyMu.AutoSize = true;
            this.labelEdycjaKlasyMu.Location = new System.Drawing.Point(131, 9);
            this.labelEdycjaKlasyMu.Name = "labelEdycjaKlasyMu";
            this.labelEdycjaKlasyMu.Size = new System.Drawing.Size(13, 13);
            this.labelEdycjaKlasyMu.TabIndex = 4;
            this.labelEdycjaKlasyMu.Text = "μ";
            // 
            // labelEdycjaKlasyS
            // 
            this.labelEdycjaKlasyS.AutoSize = true;
            this.labelEdycjaKlasyS.Location = new System.Drawing.Point(186, 46);
            this.labelEdycjaKlasyS.Name = "labelEdycjaKlasyS";
            this.labelEdycjaKlasyS.Size = new System.Drawing.Size(14, 13);
            this.labelEdycjaKlasyS.TabIndex = 5;
            this.labelEdycjaKlasyS.Text = "S";
            // 
            // numericUpDownEdycjaKlasyAt
            // 
            this.numericUpDownEdycjaKlasyAt.Location = new System.Drawing.Point(82, 7);
            this.numericUpDownEdycjaKlasyAt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEdycjaKlasyAt.Name = "numericUpDownEdycjaKlasyAt";
            this.numericUpDownEdycjaKlasyAt.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownEdycjaKlasyAt.TabIndex = 6;
            this.numericUpDownEdycjaKlasyAt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownEdycjaKlasyMu
            // 
            this.numericUpDownEdycjaKlasyMu.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownEdycjaKlasyMu.Location = new System.Drawing.Point(150, 7);
            this.numericUpDownEdycjaKlasyMu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownEdycjaKlasyMu.Name = "numericUpDownEdycjaKlasyMu";
            this.numericUpDownEdycjaKlasyMu.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownEdycjaKlasyMu.TabIndex = 7;
            this.numericUpDownEdycjaKlasyMu.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownEdycjaKlasyT
            // 
            this.numericUpDownEdycjaKlasyT.Location = new System.Drawing.Point(222, 7);
            this.numericUpDownEdycjaKlasyT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEdycjaKlasyT.Name = "numericUpDownEdycjaKlasyT";
            this.numericUpDownEdycjaKlasyT.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownEdycjaKlasyT.TabIndex = 8;
            this.numericUpDownEdycjaKlasyT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownEdycjaKlasyS
            // 
            this.numericUpDownEdycjaKlasyS.Location = new System.Drawing.Point(209, 44);
            this.numericUpDownEdycjaKlasyS.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownEdycjaKlasyS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEdycjaKlasyS.Name = "numericUpDownEdycjaKlasyS";
            this.numericUpDownEdycjaKlasyS.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownEdycjaKlasyS.TabIndex = 9;
            this.numericUpDownEdycjaKlasyS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxEdycjaKlasyUprzywilejowana
            // 
            this.checkBoxEdycjaKlasyUprzywilejowana.AutoSize = true;
            this.checkBoxEdycjaKlasyUprzywilejowana.Location = new System.Drawing.Point(15, 45);
            this.checkBoxEdycjaKlasyUprzywilejowana.Name = "checkBoxEdycjaKlasyUprzywilejowana";
            this.checkBoxEdycjaKlasyUprzywilejowana.Size = new System.Drawing.Size(103, 17);
            this.checkBoxEdycjaKlasyUprzywilejowana.TabIndex = 10;
            this.checkBoxEdycjaKlasyUprzywilejowana.Text = "uprzywilejowana";
            this.checkBoxEdycjaKlasyUprzywilejowana.UseVisualStyleBackColor = true;
            // 
            // EdycjaKlasy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 74);
            this.Controls.Add(this.checkBoxEdycjaKlasyUprzywilejowana);
            this.Controls.Add(this.numericUpDownEdycjaKlasyS);
            this.Controls.Add(this.numericUpDownEdycjaKlasyT);
            this.Controls.Add(this.numericUpDownEdycjaKlasyMu);
            this.Controls.Add(this.numericUpDownEdycjaKlasyAt);
            this.Controls.Add(this.labelEdycjaKlasyS);
            this.Controls.Add(this.labelEdycjaKlasyMu);
            this.Controls.Add(this.labelEdycjaKlasyT);
            this.Controls.Add(this.labelEdycjaKlasyAt);
            this.Controls.Add(this.buttonEdycjaKlasyZmien);
            this.Controls.Add(this.buttonEdycjaKlasyAnuluj);
            this.Name = "EdycjaKlasy";
            this.Text = "EdycjaKlasy";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyMu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdycjaKlasyS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEdycjaKlasyAnuluj;
        private System.Windows.Forms.Button buttonEdycjaKlasyZmien;
        private System.Windows.Forms.Label labelEdycjaKlasyAt;
        private System.Windows.Forms.Label labelEdycjaKlasyT;
        private System.Windows.Forms.Label labelEdycjaKlasyMu;
        private System.Windows.Forms.Label labelEdycjaKlasyS;
        private System.Windows.Forms.NumericUpDown numericUpDownEdycjaKlasyAt;
        private System.Windows.Forms.NumericUpDown numericUpDownEdycjaKlasyMu;
        private System.Windows.Forms.NumericUpDown numericUpDownEdycjaKlasyT;
        private System.Windows.Forms.NumericUpDown numericUpDownEdycjaKlasyS;
        private System.Windows.Forms.CheckBox checkBoxEdycjaKlasyUprzywilejowana;
    }
}