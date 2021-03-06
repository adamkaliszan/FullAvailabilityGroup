namespace GUI
{
    partial class FormMainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainWindow));
            this.listBoxKlasy = new System.Windows.Forms.ListBox();
            this.labelKlasy = new System.Windows.Forms.Label();
            this.listBoxAlgorytmy = new System.Windows.Forms.ListBox();
            this.labelMozliweAlgorytmy = new System.Windows.Forms.Label();
            this.labelMaxRuchOf = new System.Windows.Forms.Label();
            this.labelPrzyrostRuchu = new System.Windows.Forms.Label();
            this.buttonStartAddStop = new System.Windows.Forms.Button();
            this.numericUpDownMaxRuchOf = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPrzyrRuchOf = new System.Windows.Forms.NumericUpDown();
            this.labelPojWiazki = new System.Windows.Forms.Label();
            this.labelLiPodgrup = new System.Windows.Forms.Label();
            this.textBoxPojCalkowita = new System.Windows.Forms.TextBox();
            this.textBoxLiczbaPodgrup = new System.Windows.Forms.TextBox();
            this.labelLiczbaBadan = new System.Windows.Forms.Label();
            this.textBoxLBad = new System.Windows.Forms.TextBox();
            this.buttonUsunKlase = new System.Windows.Forms.Button();
            this.listBoxPodgrupy = new System.Windows.Forms.ListBox();
            this.buttonUsunPodgrupyLaczy = new System.Windows.Forms.Button();
            this.buttonDodajWiazke = new System.Windows.Forms.Button();
            this.numericUpDownGranicaRezerwacji = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPojemnoscLacza = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLiczbaPodgrupLzczy = new System.Windows.Forms.NumericUpDown();
            this.labelLiczbaDodPodgrup = new System.Windows.Forms.Label();
            this.labelPojDodWiazki = new System.Windows.Forms.Label();
            this.labelGrRezerwacji = new System.Windows.Forms.Label();
            this.radioButtonBrakRezerwacji = new System.Windows.Forms.RadioButton();
            this.groupBoxAlgRezerwacji = new System.Windows.Forms.GroupBox();
            this.radioButtonR3 = new System.Windows.Forms.RadioButton();
            this.radioButtonR1 = new System.Windows.Forms.RadioButton();
            this.groupBoxAlgWybPodgrupy = new System.Windows.Forms.GroupBox();
            this.radioButtonAlgLosowyProporcjonalnyWolny = new System.Windows.Forms.RadioButton();
            this.radioButtonAlgCykliczny = new System.Windows.Forms.RadioButton();
            this.radioButtonAlgKolejnosciowy = new System.Windows.Forms.RadioButton();
            this.radioButtonAlgLosowyProporcjonalny = new System.Windows.Forms.RadioButton();
            this.radioButtonAlgLosowy = new System.Windows.Forms.RadioButton();
            this.tabControlPrezentacja = new System.Windows.Forms.TabControl();
            this.tabPageModelSystemu = new System.Windows.Forms.TabPage();
            this.tabPageE = new System.Windows.Forms.TabPage();
            this.tabControlE = new System.Windows.Forms.TabControl();
            this.tabPageEwykres = new System.Windows.Forms.TabPage();
            this.zedGraphControlWykrE = new ZedGraph.ZedGraphControl();
            this.tabPageEtabela = new System.Windows.Forms.TabPage();
            this.labelLMiejscPoPrzecinkuE = new System.Windows.Forms.Label();
            this.numericUpDownLMiejscE = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxTabelaE = new System.Windows.Forms.RichTextBox();
            this.tabPageB = new System.Windows.Forms.TabPage();
            this.tabControlB = new System.Windows.Forms.TabControl();
            this.tabPageBwykres = new System.Windows.Forms.TabPage();
            this.zedGraphControlWykrB = new ZedGraph.ZedGraphControl();
            this.tabPageBtabela = new System.Windows.Forms.TabPage();
            this.labelLMiejscPoPrzecinkuB = new System.Windows.Forms.Label();
            this.numericUpDownLMiejscB = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxTabelaB = new System.Windows.Forms.RichTextBox();
            this.tabPageEBwykres = new System.Windows.Forms.TabPage();
            this.zedGraphControlWykrEB = new ZedGraph.ZedGraphControl();
            this.tabPageBladWzgl = new System.Windows.Forms.TabPage();
            this.tabControlBladWzgledny = new System.Windows.Forms.TabControl();
            this.tabPageBlWzglWykres = new System.Windows.Forms.TabPage();
            this.zedGraphControlBladWzgl = new ZedGraph.ZedGraphControl();
            this.tabPageBlWzglTabela = new System.Windows.Forms.TabPage();
            this.labelAlgReferencyjny = new System.Windows.Forms.Label();
            this.listBoxAlgReferencyjny = new System.Windows.Forms.ListBox();
            this.tabPagePU = new System.Windows.Forms.TabPage();
            this.tabControlPU = new System.Windows.Forms.TabControl();
            this.tabPagePUwykresE = new System.Windows.Forms.TabPage();
            this.zedGraphControlPU_E = new ZedGraph.ZedGraphControl();
            this.tabPagePUtabelaE = new System.Windows.Forms.TabPage();
            this.labelPU_E = new System.Windows.Forms.Label();
            this.numericUpDownPU_E = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxPU_E = new System.Windows.Forms.RichTextBox();
            this.tabPagePUwykresB = new System.Windows.Forms.TabPage();
            this.zedGraphControlPU_B = new ZedGraph.ZedGraphControl();
            this.tabPagePUtabelaB = new System.Windows.Forms.TabPage();
            this.labelPU_B = new System.Windows.Forms.Label();
            this.numericUpDownPU_B = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxPU_B = new System.Windows.Forms.RichTextBox();
            this.tabPagePUwykresEB = new System.Windows.Forms.TabPage();
            this.zedGraphControlPU_EiB = new ZedGraph.ZedGraphControl();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.richTextBoxDebug = new System.Windows.Forms.RichTextBox();
            this.checkBoxUprzywilejowana = new System.Windows.Forms.CheckBox();
            this.buttonDodajKlase = new System.Windows.Forms.Button();
            this.labelNumberOfTrSourcess = new System.Windows.Forms.Label();
            this.numericUpDownLiczbaZrRuchu = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMi = new System.Windows.Forms.NumericUpDown();
            this.labelMi = new System.Windows.Forms.Label();
            this.numericUpDownT = new System.Windows.Forms.NumericUpDown();
            this.labelT = new System.Windows.Forms.Label();
            this.numericUpDownAt = new System.Windows.Forms.NumericUpDown();
            this.labelPropAt = new System.Windows.Forms.Label();
            this.radioButtonErlang = new System.Windows.Forms.RadioButton();
            this.radioButtonPascal = new System.Windows.Forms.RadioButton();
            this.radioButtonEngset = new System.Windows.Forms.RadioButton();
            this.labelMinRuchOf = new System.Windows.Forms.Label();
            this.numericUpDownMinRuchOf = new System.Windows.Forms.NumericUpDown();
            this.labelKlasyPodgrup = new System.Windows.Forms.Label();
            this.groupBoxNowePodgrupy = new System.Windows.Forms.GroupBox();
            this.progressBarPost = new System.Windows.Forms.ProgressBar();
            this.labelParametrySymulacji = new System.Windows.Forms.Label();
            this.menuStripMenuProg = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzSkryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszWykresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszWynikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszWykresBGnuPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszTabeleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugowanieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorytmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajAlgorytmMismToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajAlgorytmHyrydowyMISMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wymStosNeiodpAlgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EkspAlgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogSkrypt = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogGnuPlot = new System.Windows.Forms.SaveFileDialog();
            this.tabControlDodawanieKlasy = new System.Windows.Forms.TabControl();
            this.tabPageKlasaBezProgow = new System.Windows.Forms.TabPage();
            this.tabPageKlasaZprogrami = new System.Windows.Forms.TabPage();
            this.labelProgiS = new System.Windows.Forms.Label();
            this.checkBoxProgiUprzywilejowana = new System.Windows.Forms.CheckBox();
            this.numericUpDownProgiS = new System.Windows.Forms.NumericUpDown();
            this.buttonProgiUstaw = new System.Windows.Forms.Button();
            this.numericUpDownProgiLiczba = new System.Windows.Forms.NumericUpDown();
            this.labelProgiLiczbaProgow = new System.Windows.Forms.Label();
            this.numericUpDownProgiPropAt = new System.Windows.Forms.NumericUpDown();
            this.labelProgiPropAt = new System.Windows.Forms.Label();
            this.radioButtonProgiPascal = new System.Windows.Forms.RadioButton();
            this.radioButtonProgiEngset = new System.Windows.Forms.RadioButton();
            this.radioButtonProgiErlang = new System.Windows.Forms.RadioButton();
            this.textBoxPojemnoscKolejki = new System.Windows.Forms.TextBox();
            this.labelPojKolejki = new System.Windows.Forms.Label();
            this.textBoxLiczbaKolejek = new System.Windows.Forms.TextBox();
            this.labelLKolejek = new System.Windows.Forms.Label();
            this.comboBoxParametrySymulacji = new System.Windows.Forms.ComboBox();
            this.comboBoxSystemy = new System.Windows.Forms.ComboBox();
            this.labelModelSystemu = new System.Windows.Forms.Label();
            this.labelPostep = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxRuchOf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrzyrRuchOf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGranicaRezerwacji)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPojemnoscLacza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiczbaPodgrupLzczy)).BeginInit();
            this.groupBoxAlgRezerwacji.SuspendLayout();
            this.groupBoxAlgWybPodgrupy.SuspendLayout();
            this.tabControlPrezentacja.SuspendLayout();
            this.tabPageE.SuspendLayout();
            this.tabControlE.SuspendLayout();
            this.tabPageEwykres.SuspendLayout();
            this.tabPageEtabela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLMiejscE)).BeginInit();
            this.tabPageB.SuspendLayout();
            this.tabControlB.SuspendLayout();
            this.tabPageBwykres.SuspendLayout();
            this.tabPageBtabela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLMiejscB)).BeginInit();
            this.tabPageEBwykres.SuspendLayout();
            this.tabPageBladWzgl.SuspendLayout();
            this.tabControlBladWzgledny.SuspendLayout();
            this.tabPageBlWzglWykres.SuspendLayout();
            this.tabPagePU.SuspendLayout();
            this.tabControlPU.SuspendLayout();
            this.tabPagePUwykresE.SuspendLayout();
            this.tabPagePUtabelaE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPU_E)).BeginInit();
            this.tabPagePUwykresB.SuspendLayout();
            this.tabPagePUtabelaB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPU_B)).BeginInit();
            this.tabPagePUwykresEB.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiczbaZrRuchu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinRuchOf)).BeginInit();
            this.groupBoxNowePodgrupy.SuspendLayout();
            this.menuStripMenuProg.SuspendLayout();
            this.tabControlDodawanieKlasy.SuspendLayout();
            this.tabPageKlasaBezProgow.SuspendLayout();
            this.tabPageKlasaZprogrami.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgiS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgiLiczba)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgiPropAt)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxKlasy
            // 
            this.listBoxKlasy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxKlasy.FormattingEnabled = true;
            this.listBoxKlasy.Location = new System.Drawing.Point(7, 519);
            this.listBoxKlasy.Name = "listBoxKlasy";
            this.listBoxKlasy.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxKlasy.Size = new System.Drawing.Size(159, 82);
            this.listBoxKlasy.TabIndex = 14;
            this.listBoxKlasy.SelectedIndexChanged += new System.EventHandler(this.listBoxKlasy_SelectedIndexChanged);
            this.listBoxKlasy.DoubleClick += new System.EventHandler(this.listBoxKlasy_DoubleClick);
            // 
            // labelKlasy
            // 
            this.labelKlasy.AutoSize = true;
            this.labelKlasy.Location = new System.Drawing.Point(10, 498);
            this.labelKlasy.Name = "labelKlasy";
            this.labelKlasy.Size = new System.Drawing.Size(65, 13);
            this.labelKlasy.TabIndex = 2;
            this.labelKlasy.Text = "Klasy ruchu:";
            // 
            // listBoxAlgorytmy
            // 
            this.listBoxAlgorytmy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxAlgorytmy.FormattingEnabled = true;
            this.listBoxAlgorytmy.Location = new System.Drawing.Point(172, 441);
            this.listBoxAlgorytmy.Name = "listBoxAlgorytmy";
            this.listBoxAlgorytmy.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxAlgorytmy.Size = new System.Drawing.Size(145, 160);
            this.listBoxAlgorytmy.TabIndex = 4;
            this.listBoxAlgorytmy.SelectedIndexChanged += new System.EventHandler(this.listBoxAlgorytmy_SelectedIndexChanged);
            // 
            // labelMozliweAlgorytmy
            // 
            this.labelMozliweAlgorytmy.AutoSize = true;
            this.labelMozliweAlgorytmy.Location = new System.Drawing.Point(175, 423);
            this.labelMozliweAlgorytmy.Name = "labelMozliweAlgorytmy";
            this.labelMozliweAlgorytmy.Size = new System.Drawing.Size(135, 13);
            this.labelMozliweAlgorytmy.TabIndex = 5;
            this.labelMozliweAlgorytmy.Text = "Możliwe do zast. algorytmy:";
            // 
            // labelMaxRuchOf
            // 
            this.labelMaxRuchOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaxRuchOf.AutoSize = true;
            this.labelMaxRuchOf.Location = new System.Drawing.Point(325, 602);
            this.labelMaxRuchOf.Name = "labelMaxRuchOf";
            this.labelMaxRuchOf.Size = new System.Drawing.Size(111, 13);
            this.labelMaxRuchOf.TabIndex = 14;
            this.labelMaxRuchOf.Text = "Końc. ruch oferowany";
            // 
            // labelPrzyrostRuchu
            // 
            this.labelPrzyrostRuchu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPrzyrostRuchu.AutoSize = true;
            this.labelPrzyrostRuchu.Location = new System.Drawing.Point(510, 575);
            this.labelPrzyrostRuchu.Name = "labelPrzyrostRuchu";
            this.labelPrzyrostRuchu.Size = new System.Drawing.Size(74, 13);
            this.labelPrzyrostRuchu.TabIndex = 15;
            this.labelPrzyrostRuchu.Text = "Przyrost ruchu";
            // 
            // buttonStartDodajStop
            // 
            this.buttonStartAddStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartAddStop.Enabled = false;
            this.buttonStartAddStop.Location = new System.Drawing.Point(656, 569);
            this.buttonStartAddStop.Name = "buttonStartDodajStop";
            this.buttonStartAddStop.Size = new System.Drawing.Size(132, 46);
            this.buttonStartAddStop.TabIndex = 16;
            this.buttonStartAddStop.Text = "Rozpocznij modelowanie systemu";
            this.buttonStartAddStop.UseVisualStyleBackColor = true;
            this.buttonStartAddStop.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // numericUpDownMaxRuchOf
            // 
            this.numericUpDownMaxRuchOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMaxRuchOf.DecimalPlaces = 1;
            this.numericUpDownMaxRuchOf.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMaxRuchOf.Location = new System.Drawing.Point(459, 599);
            this.numericUpDownMaxRuchOf.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownMaxRuchOf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMaxRuchOf.Name = "numericUpDownMaxRuchOf";
            this.numericUpDownMaxRuchOf.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownMaxRuchOf.TabIndex = 21;
            this.numericUpDownMaxRuchOf.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.numericUpDownMaxRuchOf.ValueChanged += new System.EventHandler(this.numericUpDownMaxRuchOf_ValueChanged);
            // 
            // numericUpDownPrzyrRuchOf
            // 
            this.numericUpDownPrzyrRuchOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPrzyrRuchOf.DecimalPlaces = 2;
            this.numericUpDownPrzyrRuchOf.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownPrzyrRuchOf.Location = new System.Drawing.Point(605, 573);
            this.numericUpDownPrzyrRuchOf.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownPrzyrRuchOf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownPrzyrRuchOf.Name = "numericUpDownPrzyrRuchOf";
            this.numericUpDownPrzyrRuchOf.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownPrzyrRuchOf.TabIndex = 22;
            this.numericUpDownPrzyrRuchOf.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownPrzyrRuchOf.ValueChanged += new System.EventHandler(this.numericUpDownPrzyrRuchOf_ValueChanged);
            // 
            // labelPojWiazki
            // 
            this.labelPojWiazki.AutoSize = true;
            this.labelPojWiazki.Location = new System.Drawing.Point(13, 38);
            this.labelPojWiazki.Name = "labelPojWiazki";
            this.labelPojWiazki.Size = new System.Drawing.Size(91, 13);
            this.labelPojWiazki.TabIndex = 1;
            this.labelPojWiazki.Text = "Pojemność wiązki";
            // 
            // labelLiPodgrup
            // 
            this.labelLiPodgrup.AutoSize = true;
            this.labelLiPodgrup.Location = new System.Drawing.Point(13, 64);
            this.labelLiPodgrup.Name = "labelLiPodgrup";
            this.labelLiPodgrup.Size = new System.Drawing.Size(80, 13);
            this.labelLiPodgrup.TabIndex = 5;
            this.labelLiPodgrup.Text = "Liczba podgrup";
            // 
            // textBoxPojCalkowita
            // 
            this.textBoxPojCalkowita.Location = new System.Drawing.Point(118, 35);
            this.textBoxPojCalkowita.Name = "textBoxPojCalkowita";
            this.textBoxPojCalkowita.ReadOnly = true;
            this.textBoxPojCalkowita.Size = new System.Drawing.Size(42, 20);
            this.textBoxPojCalkowita.TabIndex = 2;
            this.textBoxPojCalkowita.TabStop = false;
            this.textBoxPojCalkowita.Text = "0";
            // 
            // textBoxLiczbaPodgrup
            // 
            this.textBoxLiczbaPodgrup.Location = new System.Drawing.Point(118, 61);
            this.textBoxLiczbaPodgrup.Name = "textBoxLiczbaPodgrup";
            this.textBoxLiczbaPodgrup.ReadOnly = true;
            this.textBoxLiczbaPodgrup.Size = new System.Drawing.Size(42, 20);
            this.textBoxLiczbaPodgrup.TabIndex = 6;
            this.textBoxLiczbaPodgrup.TabStop = false;
            this.textBoxLiczbaPodgrup.Text = "0";
            // 
            // labelLiczbaBadan
            // 
            this.labelLiczbaBadan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLiczbaBadan.AutoSize = true;
            this.labelLiczbaBadan.Location = new System.Drawing.Point(510, 602);
            this.labelLiczbaBadan.Name = "labelLiczbaBadan";
            this.labelLiczbaBadan.Size = new System.Drawing.Size(80, 13);
            this.labelLiczbaBadan.TabIndex = 37;
            this.labelLiczbaBadan.Text = "L. mod. ruchów";
            // 
            // textBoxLBad
            // 
            this.textBoxLBad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLBad.Location = new System.Drawing.Point(605, 599);
            this.textBoxLBad.Name = "textBoxLBad";
            this.textBoxLBad.ReadOnly = true;
            this.textBoxLBad.Size = new System.Drawing.Size(45, 20);
            this.textBoxLBad.TabIndex = 38;
            this.textBoxLBad.Text = "11";
            // 
            // buttonUsunKlase
            // 
            this.buttonUsunKlase.Location = new System.Drawing.Point(124, 494);
            this.buttonUsunKlase.Name = "buttonUsunKlase";
            this.buttonUsunKlase.Size = new System.Drawing.Size(42, 20);
            this.buttonUsunKlase.TabIndex = 15;
            this.buttonUsunKlase.Text = "Usuń";
            this.buttonUsunKlase.UseVisualStyleBackColor = true;
            this.buttonUsunKlase.Click += new System.EventHandler(this.buttonRemoveTrClass_Click);
            // 
            // listBoxPodgrupy
            // 
            this.listBoxPodgrupy.FormattingEnabled = true;
            this.listBoxPodgrupy.Location = new System.Drawing.Point(7, 441);
            this.listBoxPodgrupy.Name = "listBoxPodgrupy";
            this.listBoxPodgrupy.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxPodgrupy.Size = new System.Drawing.Size(159, 43);
            this.listBoxPodgrupy.TabIndex = 7;
            // 
            // buttonUsunPodgrupyLaczy
            // 
            this.buttonUsunPodgrupyLaczy.Location = new System.Drawing.Point(6, 103);
            this.buttonUsunPodgrupyLaczy.Name = "buttonUsunPodgrupyLaczy";
            this.buttonUsunPodgrupyLaczy.Size = new System.Drawing.Size(147, 24);
            this.buttonUsunPodgrupyLaczy.TabIndex = 8;
            this.buttonUsunPodgrupyLaczy.Text = "Usuń podgrupy";
            this.buttonUsunPodgrupyLaczy.UseVisualStyleBackColor = true;
            this.buttonUsunPodgrupyLaczy.Click += new System.EventHandler(this.buttonRemoveGroup_Click);
            // 
            // buttonDodajWiazke
            // 
            this.buttonDodajWiazke.Location = new System.Drawing.Point(6, 71);
            this.buttonDodajWiazke.Name = "buttonDodajWiazke";
            this.buttonDodajWiazke.Size = new System.Drawing.Size(147, 26);
            this.buttonDodajWiazke.TabIndex = 13;
            this.buttonDodajWiazke.Text = "Dodaj podgrypę / podgrupy";
            this.buttonDodajWiazke.UseVisualStyleBackColor = true;
            this.buttonDodajWiazke.Click += new System.EventHandler(this.buttonDodajWiazke_Click);
            // 
            // numericUpDownGranicaRezerwacji
            // 
            this.numericUpDownGranicaRezerwacji.Enabled = false;
            this.numericUpDownGranicaRezerwacji.Location = new System.Drawing.Point(275, 118);
            this.numericUpDownGranicaRezerwacji.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownGranicaRezerwacji.Name = "numericUpDownGranicaRezerwacji";
            this.numericUpDownGranicaRezerwacji.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownGranicaRezerwacji.TabIndex = 4;
            this.numericUpDownGranicaRezerwacji.ValueChanged += new System.EventHandler(this.numericUpDownGranicaRezerwacji_ValueChanged);
            // 
            // numericUpDownPojemnoscLacza
            // 
            this.numericUpDownPojemnoscLacza.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownPojemnoscLacza.Location = new System.Drawing.Point(111, 19);
            this.numericUpDownPojemnoscLacza.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownPojemnoscLacza.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPojemnoscLacza.Name = "numericUpDownPojemnoscLacza";
            this.numericUpDownPojemnoscLacza.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownPojemnoscLacza.TabIndex = 10;
            this.numericUpDownPojemnoscLacza.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numericUpDownLiczbaPodgrupLzczy
            // 
            this.numericUpDownLiczbaPodgrupLzczy.Location = new System.Drawing.Point(111, 45);
            this.numericUpDownLiczbaPodgrupLzczy.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownLiczbaPodgrupLzczy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLiczbaPodgrupLzczy.Name = "numericUpDownLiczbaPodgrupLzczy";
            this.numericUpDownLiczbaPodgrupLzczy.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownLiczbaPodgrupLzczy.TabIndex = 12;
            this.numericUpDownLiczbaPodgrupLzczy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelLiczbaDodPodgrup
            // 
            this.labelLiczbaDodPodgrup.AutoSize = true;
            this.labelLiczbaDodPodgrup.Location = new System.Drawing.Point(3, 47);
            this.labelLiczbaDodPodgrup.Name = "labelLiczbaDodPodgrup";
            this.labelLiczbaDodPodgrup.Size = new System.Drawing.Size(80, 13);
            this.labelLiczbaDodPodgrup.TabIndex = 11;
            this.labelLiczbaDodPodgrup.Text = "Liczba podgrup";
            // 
            // labelPojDodWiazki
            // 
            this.labelPojDodWiazki.AutoSize = true;
            this.labelPojDodWiazki.Location = new System.Drawing.Point(3, 22);
            this.labelPojDodWiazki.Name = "labelPojDodWiazki";
            this.labelPojDodWiazki.Size = new System.Drawing.Size(106, 13);
            this.labelPojDodWiazki.TabIndex = 9;
            this.labelPojDodWiazki.Text = "Pojemność podgrupy";
            // 
            // labelGrRezerwacji
            // 
            this.labelGrRezerwacji.AutoSize = true;
            this.labelGrRezerwacji.Enabled = false;
            this.labelGrRezerwacji.Location = new System.Drawing.Point(175, 120);
            this.labelGrRezerwacji.Name = "labelGrRezerwacji";
            this.labelGrRezerwacji.Size = new System.Drawing.Size(94, 13);
            this.labelGrRezerwacji.TabIndex = 3;
            this.labelGrRezerwacji.Text = "Granica rezerwacji";
            // 
            // radioButtonBrakRezerwacji
            // 
            this.radioButtonBrakRezerwacji.AutoSize = true;
            this.radioButtonBrakRezerwacji.Checked = true;
            this.radioButtonBrakRezerwacji.Location = new System.Drawing.Point(6, 22);
            this.radioButtonBrakRezerwacji.Name = "radioButtonBrakRezerwacji";
            this.radioButtonBrakRezerwacji.Size = new System.Drawing.Size(46, 17);
            this.radioButtonBrakRezerwacji.TabIndex = 60;
            this.radioButtonBrakRezerwacji.TabStop = true;
            this.radioButtonBrakRezerwacji.Text = "brak";
            this.radioButtonBrakRezerwacji.UseVisualStyleBackColor = true;
            this.radioButtonBrakRezerwacji.CheckedChanged += new System.EventHandler(this.radioButtonBrakRezerwacji_CheckedChanged);
            // 
            // groupBoxAlgRezerwacji
            // 
            this.groupBoxAlgRezerwacji.Controls.Add(this.radioButtonR3);
            this.groupBoxAlgRezerwacji.Controls.Add(this.radioButtonR1);
            this.groupBoxAlgRezerwacji.Controls.Add(this.radioButtonBrakRezerwacji);
            this.groupBoxAlgRezerwacji.Enabled = false;
            this.groupBoxAlgRezerwacji.Location = new System.Drawing.Point(172, 24);
            this.groupBoxAlgRezerwacji.Name = "groupBoxAlgRezerwacji";
            this.groupBoxAlgRezerwacji.Size = new System.Drawing.Size(145, 88);
            this.groupBoxAlgRezerwacji.TabIndex = 61;
            this.groupBoxAlgRezerwacji.TabStop = false;
            this.groupBoxAlgRezerwacji.Text = "Algorytm rezerwacji";
            // 
            // radioButtonR3
            // 
            this.radioButtonR3.AutoSize = true;
            this.radioButtonR3.Location = new System.Drawing.Point(6, 68);
            this.radioButtonR3.Name = "radioButtonR3";
            this.radioButtonR3.Size = new System.Drawing.Size(39, 17);
            this.radioButtonR3.TabIndex = 63;
            this.radioButtonR3.Text = "R3";
            this.radioButtonR3.UseVisualStyleBackColor = true;
            this.radioButtonR3.CheckedChanged += new System.EventHandler(this.radioButtonR3_CheckedChanged);
            // 
            // radioButtonR1
            // 
            this.radioButtonR1.AutoSize = true;
            this.radioButtonR1.Location = new System.Drawing.Point(6, 45);
            this.radioButtonR1.Name = "radioButtonR1";
            this.radioButtonR1.Size = new System.Drawing.Size(58, 17);
            this.radioButtonR1.TabIndex = 61;
            this.radioButtonR1.Text = "R1/R2";
            this.radioButtonR1.UseVisualStyleBackColor = true;
            this.radioButtonR1.CheckedChanged += new System.EventHandler(this.radioButtonR1_R2_CheckedChanged);
            // 
            // groupBoxAlgWybPodgrupy
            // 
            this.groupBoxAlgWybPodgrupy.Controls.Add(this.radioButtonAlgLosowyProporcjonalnyWolny);
            this.groupBoxAlgWybPodgrupy.Controls.Add(this.radioButtonAlgCykliczny);
            this.groupBoxAlgWybPodgrupy.Controls.Add(this.radioButtonAlgKolejnosciowy);
            this.groupBoxAlgWybPodgrupy.Controls.Add(this.radioButtonAlgLosowyProporcjonalny);
            this.groupBoxAlgWybPodgrupy.Controls.Add(this.radioButtonAlgLosowy);
            this.groupBoxAlgWybPodgrupy.Enabled = false;
            this.groupBoxAlgWybPodgrupy.Location = new System.Drawing.Point(172, 143);
            this.groupBoxAlgWybPodgrupy.Name = "groupBoxAlgWybPodgrupy";
            this.groupBoxAlgWybPodgrupy.Size = new System.Drawing.Size(145, 133);
            this.groupBoxAlgWybPodgrupy.TabIndex = 64;
            this.groupBoxAlgWybPodgrupy.TabStop = false;
            this.groupBoxAlgWybPodgrupy.Text = "Algorytm wyb. podgrupy";
            // 
            // radioButtonAlgLosowyProporcjonalnyWolny
            // 
            this.radioButtonAlgLosowyProporcjonalnyWolny.AutoSize = true;
            this.radioButtonAlgLosowyProporcjonalnyWolny.Location = new System.Drawing.Point(6, 65);
            this.radioButtonAlgLosowyProporcjonalnyWolny.Name = "radioButtonAlgLosowyProporcjonalnyWolny";
            this.radioButtonAlgLosowyProporcjonalnyWolny.Size = new System.Drawing.Size(132, 17);
            this.radioButtonAlgLosowyProporcjonalnyWolny.TabIndex = 64;
            this.radioButtonAlgLosowyProporcjonalnyWolny.Text = "Losowy prop. do stanu";
            this.radioButtonAlgLosowyProporcjonalnyWolny.UseVisualStyleBackColor = true;
            this.radioButtonAlgLosowyProporcjonalnyWolny.CheckedChanged += new System.EventHandler(this.radioButtonAlgLosowyProporcjonalnyWolny_CheckedChanged);
            // 
            // radioButtonAlgCykliczny
            // 
            this.radioButtonAlgCykliczny.AutoSize = true;
            this.radioButtonAlgCykliczny.Location = new System.Drawing.Point(6, 111);
            this.radioButtonAlgCykliczny.Name = "radioButtonAlgCykliczny";
            this.radioButtonAlgCykliczny.Size = new System.Drawing.Size(69, 17);
            this.radioButtonAlgCykliczny.TabIndex = 63;
            this.radioButtonAlgCykliczny.Text = "Cykliczny";
            this.radioButtonAlgCykliczny.UseVisualStyleBackColor = true;
            this.radioButtonAlgCykliczny.CheckedChanged += new System.EventHandler(this.radioButtonAlgCykliczny_CheckedChanged);
            // 
            // radioButtonAlgKolejnosciowy
            // 
            this.radioButtonAlgKolejnosciowy.AutoSize = true;
            this.radioButtonAlgKolejnosciowy.Location = new System.Drawing.Point(6, 88);
            this.radioButtonAlgKolejnosciowy.Name = "radioButtonAlgKolejnosciowy";
            this.radioButtonAlgKolejnosciowy.Size = new System.Drawing.Size(92, 17);
            this.radioButtonAlgKolejnosciowy.TabIndex = 62;
            this.radioButtonAlgKolejnosciowy.Text = "Kolejnościowy";
            this.radioButtonAlgKolejnosciowy.UseVisualStyleBackColor = true;
            this.radioButtonAlgKolejnosciowy.CheckedChanged += new System.EventHandler(this.radioButtonAlgKolejnosciowy_CheckedChanged);
            // 
            // radioButtonAlgLosowyProporcjonalny
            // 
            this.radioButtonAlgLosowyProporcjonalny.AutoSize = true;
            this.radioButtonAlgLosowyProporcjonalny.Location = new System.Drawing.Point(6, 42);
            this.radioButtonAlgLosowyProporcjonalny.Name = "radioButtonAlgLosowyProporcjonalny";
            this.radioButtonAlgLosowyProporcjonalny.Size = new System.Drawing.Size(133, 17);
            this.radioButtonAlgLosowyProporcjonalny.TabIndex = 61;
            this.radioButtonAlgLosowyProporcjonalny.Text = "Losowy proporcjonalny";
            this.radioButtonAlgLosowyProporcjonalny.UseVisualStyleBackColor = true;
            this.radioButtonAlgLosowyProporcjonalny.CheckedChanged += new System.EventHandler(this.radioButtonAlgLosowyProporcjonalny_CheckedChanged);
            // 
            // radioButtonAlgLosowy
            // 
            this.radioButtonAlgLosowy.AutoSize = true;
            this.radioButtonAlgLosowy.Checked = true;
            this.radioButtonAlgLosowy.Location = new System.Drawing.Point(6, 19);
            this.radioButtonAlgLosowy.Name = "radioButtonAlgLosowy";
            this.radioButtonAlgLosowy.Size = new System.Drawing.Size(61, 17);
            this.radioButtonAlgLosowy.TabIndex = 60;
            this.radioButtonAlgLosowy.TabStop = true;
            this.radioButtonAlgLosowy.Text = "Losowy";
            this.radioButtonAlgLosowy.UseVisualStyleBackColor = true;
            this.radioButtonAlgLosowy.CheckedChanged += new System.EventHandler(this.radioButtonAlgLosowy_CheckedChanged);
            // 
            // tabControlPrezentacja
            // 
            this.tabControlPrezentacja.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPrezentacja.Controls.Add(this.tabPageModelSystemu);
            this.tabControlPrezentacja.Controls.Add(this.tabPageE);
            this.tabControlPrezentacja.Controls.Add(this.tabPageB);
            this.tabControlPrezentacja.Controls.Add(this.tabPageEBwykres);
            this.tabControlPrezentacja.Controls.Add(this.tabPageBladWzgl);
            this.tabControlPrezentacja.Controls.Add(this.tabPagePU);
            this.tabControlPrezentacja.Controls.Add(this.tabPageDebug);
            this.tabControlPrezentacja.Location = new System.Drawing.Point(323, 27);
            this.tabControlPrezentacja.Name = "tabControlPrezentacja";
            this.tabControlPrezentacja.SelectedIndex = 0;
            this.tabControlPrezentacja.ShowToolTips = true;
            this.tabControlPrezentacja.Size = new System.Drawing.Size(469, 540);
            this.tabControlPrezentacja.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControlPrezentacja.TabIndex = 65;
            this.tabControlPrezentacja.Click += new System.EventHandler(this.tabControlPrezentacja_Click);
            // 
            // tabPageModelSystemu
            // 
            this.tabPageModelSystemu.AccessibleDescription = "Model systemu";
            this.tabPageModelSystemu.Location = new System.Drawing.Point(4, 22);
            this.tabPageModelSystemu.Name = "tabPageModelSystemu";
            this.tabPageModelSystemu.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageModelSystemu.Size = new System.Drawing.Size(461, 514);
            this.tabPageModelSystemu.TabIndex = 0;
            this.tabPageModelSystemu.Text = "Mod. Syst.";
            this.tabPageModelSystemu.UseVisualStyleBackColor = true;
            this.tabPageModelSystemu.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPageModelSystemu_Paint);
            // 
            // tabPageE
            // 
            this.tabPageE.Controls.Add(this.tabControlE);
            this.tabPageE.Location = new System.Drawing.Point(4, 22);
            this.tabPageE.Name = "tabPageE";
            this.tabPageE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageE.Size = new System.Drawing.Size(461, 514);
            this.tabPageE.TabIndex = 1;
            this.tabPageE.Text = "E(t)";
            this.tabPageE.UseVisualStyleBackColor = true;
            // 
            // tabControlE
            // 
            this.tabControlE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlE.Controls.Add(this.tabPageEwykres);
            this.tabControlE.Controls.Add(this.tabPageEtabela);
            this.tabControlE.Location = new System.Drawing.Point(3, 3);
            this.tabControlE.Name = "tabControlE";
            this.tabControlE.SelectedIndex = 0;
            this.tabControlE.Size = new System.Drawing.Size(450, 508);
            this.tabControlE.TabIndex = 1;
            // 
            // tabPageEwykres
            // 
            this.tabPageEwykres.Controls.Add(this.zedGraphControlWykrE);
            this.tabPageEwykres.Location = new System.Drawing.Point(4, 22);
            this.tabPageEwykres.Name = "tabPageEwykres";
            this.tabPageEwykres.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEwykres.Size = new System.Drawing.Size(442, 482);
            this.tabPageEwykres.TabIndex = 0;
            this.tabPageEwykres.Text = "Wykres";
            this.tabPageEwykres.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlWykrE
            // 
            this.zedGraphControlWykrE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlWykrE.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControlWykrE.Name = "zedGraphControlWykrE";
            this.zedGraphControlWykrE.ScrollGrace = 0D;
            this.zedGraphControlWykrE.ScrollMaxX = 0D;
            this.zedGraphControlWykrE.ScrollMaxY = 0D;
            this.zedGraphControlWykrE.ScrollMaxY2 = 0D;
            this.zedGraphControlWykrE.ScrollMinX = 0D;
            this.zedGraphControlWykrE.ScrollMinY = 0D;
            this.zedGraphControlWykrE.ScrollMinY2 = 0D;
            this.zedGraphControlWykrE.Size = new System.Drawing.Size(430, 470);
            this.zedGraphControlWykrE.TabIndex = 0;
            // 
            // tabPageEtabela
            // 
            this.tabPageEtabela.Controls.Add(this.labelLMiejscPoPrzecinkuE);
            this.tabPageEtabela.Controls.Add(this.numericUpDownLMiejscE);
            this.tabPageEtabela.Controls.Add(this.richTextBoxTabelaE);
            this.tabPageEtabela.Location = new System.Drawing.Point(4, 22);
            this.tabPageEtabela.Name = "tabPageEtabela";
            this.tabPageEtabela.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEtabela.Size = new System.Drawing.Size(442, 482);
            this.tabPageEtabela.TabIndex = 1;
            this.tabPageEtabela.Text = "Tabela";
            this.tabPageEtabela.UseVisualStyleBackColor = true;
            // 
            // labelLMiejscPoPrzecinkuE
            // 
            this.labelLMiejscPoPrzecinkuE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelLMiejscPoPrzecinkuE.AutoSize = true;
            this.labelLMiejscPoPrzecinkuE.Location = new System.Drawing.Point(6, 458);
            this.labelLMiejscPoPrzecinkuE.Name = "labelLMiejscPoPrzecinkuE";
            this.labelLMiejscPoPrzecinkuE.Size = new System.Drawing.Size(108, 13);
            this.labelLMiejscPoPrzecinkuE.TabIndex = 80;
            this.labelLMiejscPoPrzecinkuE.Text = "l. miejsc po przecinku";
            // 
            // numericUpDownLMiejscE
            // 
            this.numericUpDownLMiejscE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDownLMiejscE.Location = new System.Drawing.Point(125, 456);
            this.numericUpDownLMiejscE.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownLMiejscE.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownLMiejscE.Name = "numericUpDownLMiejscE";
            this.numericUpDownLMiejscE.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownLMiejscE.TabIndex = 79;
            this.numericUpDownLMiejscE.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // richTextBoxTabelaE
            // 
            this.richTextBoxTabelaE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxTabelaE.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxTabelaE.Name = "richTextBoxTabelaE";
            this.richTextBoxTabelaE.Size = new System.Drawing.Size(430, 444);
            this.richTextBoxTabelaE.TabIndex = 1;
            this.richTextBoxTabelaE.Text = "";
            // 
            // tabPageB
            // 
            this.tabPageB.Controls.Add(this.tabControlB);
            this.tabPageB.Location = new System.Drawing.Point(4, 22);
            this.tabPageB.Name = "tabPageB";
            this.tabPageB.Size = new System.Drawing.Size(461, 514);
            this.tabPageB.TabIndex = 2;
            this.tabPageB.Text = "B(n)";
            this.tabPageB.UseVisualStyleBackColor = true;
            // 
            // tabControlB
            // 
            this.tabControlB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlB.Controls.Add(this.tabPageBwykres);
            this.tabControlB.Controls.Add(this.tabPageBtabela);
            this.tabControlB.Location = new System.Drawing.Point(3, 3);
            this.tabControlB.Name = "tabControlB";
            this.tabControlB.SelectedIndex = 0;
            this.tabControlB.Size = new System.Drawing.Size(450, 508);
            this.tabControlB.TabIndex = 1;
            // 
            // tabPageBwykres
            // 
            this.tabPageBwykres.Controls.Add(this.zedGraphControlWykrB);
            this.tabPageBwykres.Location = new System.Drawing.Point(4, 22);
            this.tabPageBwykres.Name = "tabPageBwykres";
            this.tabPageBwykres.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBwykres.Size = new System.Drawing.Size(442, 482);
            this.tabPageBwykres.TabIndex = 0;
            this.tabPageBwykres.Text = "Wykres";
            this.tabPageBwykres.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlWykrB
            // 
            this.zedGraphControlWykrB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlWykrB.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControlWykrB.Name = "zedGraphControlWykrB";
            this.zedGraphControlWykrB.ScrollGrace = 0D;
            this.zedGraphControlWykrB.ScrollMaxX = 0D;
            this.zedGraphControlWykrB.ScrollMaxY = 0D;
            this.zedGraphControlWykrB.ScrollMaxY2 = 0D;
            this.zedGraphControlWykrB.ScrollMinX = 0D;
            this.zedGraphControlWykrB.ScrollMinY = 0D;
            this.zedGraphControlWykrB.ScrollMinY2 = 0D;
            this.zedGraphControlWykrB.Size = new System.Drawing.Size(430, 470);
            this.zedGraphControlWykrB.TabIndex = 0;
            // 
            // tabPageBtabela
            // 
            this.tabPageBtabela.Controls.Add(this.labelLMiejscPoPrzecinkuB);
            this.tabPageBtabela.Controls.Add(this.numericUpDownLMiejscB);
            this.tabPageBtabela.Controls.Add(this.richTextBoxTabelaB);
            this.tabPageBtabela.Location = new System.Drawing.Point(4, 22);
            this.tabPageBtabela.Name = "tabPageBtabela";
            this.tabPageBtabela.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBtabela.Size = new System.Drawing.Size(442, 482);
            this.tabPageBtabela.TabIndex = 1;
            this.tabPageBtabela.Text = "Tabela";
            this.tabPageBtabela.UseVisualStyleBackColor = true;
            // 
            // labelLMiejscPoPrzecinkuB
            // 
            this.labelLMiejscPoPrzecinkuB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelLMiejscPoPrzecinkuB.AutoSize = true;
            this.labelLMiejscPoPrzecinkuB.Location = new System.Drawing.Point(6, 458);
            this.labelLMiejscPoPrzecinkuB.Name = "labelLMiejscPoPrzecinkuB";
            this.labelLMiejscPoPrzecinkuB.Size = new System.Drawing.Size(108, 13);
            this.labelLMiejscPoPrzecinkuB.TabIndex = 81;
            this.labelLMiejscPoPrzecinkuB.Text = "l. miejsc po przecinku";
            // 
            // numericUpDownLMiejscB
            // 
            this.numericUpDownLMiejscB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDownLMiejscB.Location = new System.Drawing.Point(125, 456);
            this.numericUpDownLMiejscB.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownLMiejscB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownLMiejscB.Name = "numericUpDownLMiejscB";
            this.numericUpDownLMiejscB.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownLMiejscB.TabIndex = 80;
            this.numericUpDownLMiejscB.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // richTextBoxTabelaB
            // 
            this.richTextBoxTabelaB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxTabelaB.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxTabelaB.Name = "richTextBoxTabelaB";
            this.richTextBoxTabelaB.Size = new System.Drawing.Size(430, 444);
            this.richTextBoxTabelaB.TabIndex = 1;
            this.richTextBoxTabelaB.Text = "";
            // 
            // tabPageEBwykres
            // 
            this.tabPageEBwykres.Controls.Add(this.zedGraphControlWykrEB);
            this.tabPageEBwykres.Location = new System.Drawing.Point(4, 22);
            this.tabPageEBwykres.Name = "tabPageEBwykres";
            this.tabPageEBwykres.Size = new System.Drawing.Size(461, 514);
            this.tabPageEBwykres.TabIndex = 3;
            this.tabPageEBwykres.Text = "E(t) i B(n)";
            this.tabPageEBwykres.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlWykrEB
            // 
            this.zedGraphControlWykrEB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlWykrEB.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControlWykrEB.Name = "zedGraphControlWykrEB";
            this.zedGraphControlWykrEB.ScrollGrace = 0D;
            this.zedGraphControlWykrEB.ScrollMaxX = 0D;
            this.zedGraphControlWykrEB.ScrollMaxY = 0D;
            this.zedGraphControlWykrEB.ScrollMaxY2 = 0D;
            this.zedGraphControlWykrEB.ScrollMinX = 0D;
            this.zedGraphControlWykrEB.ScrollMinY = 0D;
            this.zedGraphControlWykrEB.ScrollMinY2 = 0D;
            this.zedGraphControlWykrEB.Size = new System.Drawing.Size(461, 511);
            this.zedGraphControlWykrEB.TabIndex = 0;
            // 
            // tabPageBladWzgl
            // 
            this.tabPageBladWzgl.Controls.Add(this.tabControlBladWzgledny);
            this.tabPageBladWzgl.Controls.Add(this.labelAlgReferencyjny);
            this.tabPageBladWzgl.Controls.Add(this.listBoxAlgReferencyjny);
            this.tabPageBladWzgl.Location = new System.Drawing.Point(4, 22);
            this.tabPageBladWzgl.Name = "tabPageBladWzgl";
            this.tabPageBladWzgl.Size = new System.Drawing.Size(461, 514);
            this.tabPageBladWzgl.TabIndex = 7;
            this.tabPageBladWzgl.Text = "Błąd względny";
            this.tabPageBladWzgl.UseVisualStyleBackColor = true;
            // 
            // tabControlBladWzgledny
            // 
            this.tabControlBladWzgledny.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlBladWzgledny.Controls.Add(this.tabPageBlWzglWykres);
            this.tabControlBladWzgledny.Controls.Add(this.tabPageBlWzglTabela);
            this.tabControlBladWzgledny.Location = new System.Drawing.Point(6, 3);
            this.tabControlBladWzgledny.Name = "tabControlBladWzgledny";
            this.tabControlBladWzgledny.SelectedIndex = 0;
            this.tabControlBladWzgledny.Size = new System.Drawing.Size(447, 417);
            this.tabControlBladWzgledny.TabIndex = 7;
            // 
            // tabPageBlWzglWykres
            // 
            this.tabPageBlWzglWykres.Controls.Add(this.zedGraphControlBladWzgl);
            this.tabPageBlWzglWykres.Location = new System.Drawing.Point(4, 22);
            this.tabPageBlWzglWykres.Name = "tabPageBlWzglWykres";
            this.tabPageBlWzglWykres.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBlWzglWykres.Size = new System.Drawing.Size(439, 391);
            this.tabPageBlWzglWykres.TabIndex = 0;
            this.tabPageBlWzglWykres.Text = "Wykres";
            this.tabPageBlWzglWykres.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlBladWzgl
            // 
            this.zedGraphControlBladWzgl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlBladWzgl.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControlBladWzgl.Name = "zedGraphControlBladWzgl";
            this.zedGraphControlBladWzgl.ScrollGrace = 0D;
            this.zedGraphControlBladWzgl.ScrollMaxX = 0D;
            this.zedGraphControlBladWzgl.ScrollMaxY = 0D;
            this.zedGraphControlBladWzgl.ScrollMaxY2 = 0D;
            this.zedGraphControlBladWzgl.ScrollMinX = 0D;
            this.zedGraphControlBladWzgl.ScrollMinY = 0D;
            this.zedGraphControlBladWzgl.ScrollMinY2 = 0D;
            this.zedGraphControlBladWzgl.Size = new System.Drawing.Size(427, 379);
            this.zedGraphControlBladWzgl.TabIndex = 0;
            // 
            // tabPageBlWzglTabela
            // 
            this.tabPageBlWzglTabela.Location = new System.Drawing.Point(4, 22);
            this.tabPageBlWzglTabela.Name = "tabPageBlWzglTabela";
            this.tabPageBlWzglTabela.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBlWzglTabela.Size = new System.Drawing.Size(439, 391);
            this.tabPageBlWzglTabela.TabIndex = 1;
            this.tabPageBlWzglTabela.Text = "Tabela";
            this.tabPageBlWzglTabela.UseVisualStyleBackColor = true;
            // 
            // labelAlgReferencyjny
            // 
            this.labelAlgReferencyjny.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAlgReferencyjny.AutoSize = true;
            this.labelAlgReferencyjny.Location = new System.Drawing.Point(3, 426);
            this.labelAlgReferencyjny.Name = "labelAlgReferencyjny";
            this.labelAlgReferencyjny.Size = new System.Drawing.Size(101, 13);
            this.labelAlgReferencyjny.TabIndex = 6;
            this.labelAlgReferencyjny.Text = "Algorytm referncyjny";
            // 
            // listBoxAlgReferencyjny
            // 
            this.listBoxAlgReferencyjny.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAlgReferencyjny.FormattingEnabled = true;
            this.listBoxAlgReferencyjny.Location = new System.Drawing.Point(3, 442);
            this.listBoxAlgReferencyjny.MultiColumn = true;
            this.listBoxAlgReferencyjny.Name = "listBoxAlgReferencyjny";
            this.listBoxAlgReferencyjny.Size = new System.Drawing.Size(211, 69);
            this.listBoxAlgReferencyjny.TabIndex = 5;
            this.listBoxAlgReferencyjny.Click += new System.EventHandler(this.listBoxAlgReferencyjny_Click);
            // 
            // tabPagePU
            // 
            this.tabPagePU.Controls.Add(this.tabControlPU);
            this.tabPagePU.Location = new System.Drawing.Point(4, 22);
            this.tabPagePU.Name = "tabPagePU";
            this.tabPagePU.Size = new System.Drawing.Size(461, 514);
            this.tabPagePU.TabIndex = 4;
            this.tabPagePU.Text = "Przediały ufności";
            this.tabPagePU.UseVisualStyleBackColor = true;
            // 
            // tabControlPU
            // 
            this.tabControlPU.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPU.Controls.Add(this.tabPagePUwykresE);
            this.tabControlPU.Controls.Add(this.tabPagePUtabelaE);
            this.tabControlPU.Controls.Add(this.tabPagePUwykresB);
            this.tabControlPU.Controls.Add(this.tabPagePUtabelaB);
            this.tabControlPU.Controls.Add(this.tabPagePUwykresEB);
            this.tabControlPU.Location = new System.Drawing.Point(7, 6);
            this.tabControlPU.Name = "tabControlPU";
            this.tabControlPU.SelectedIndex = 0;
            this.tabControlPU.Size = new System.Drawing.Size(446, 505);
            this.tabControlPU.TabIndex = 1;
            // 
            // tabPagePUwykresE
            // 
            this.tabPagePUwykresE.Controls.Add(this.zedGraphControlPU_E);
            this.tabPagePUwykresE.Location = new System.Drawing.Point(4, 22);
            this.tabPagePUwykresE.Name = "tabPagePUwykresE";
            this.tabPagePUwykresE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePUwykresE.Size = new System.Drawing.Size(438, 479);
            this.tabPagePUwykresE.TabIndex = 0;
            this.tabPagePUwykresE.Text = "Wykres E";
            this.tabPagePUwykresE.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlPU_E
            // 
            this.zedGraphControlPU_E.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlPU_E.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControlPU_E.Name = "zedGraphControlPU_E";
            this.zedGraphControlPU_E.ScrollGrace = 0D;
            this.zedGraphControlPU_E.ScrollMaxX = 0D;
            this.zedGraphControlPU_E.ScrollMaxY = 0D;
            this.zedGraphControlPU_E.ScrollMaxY2 = 0D;
            this.zedGraphControlPU_E.ScrollMinX = 0D;
            this.zedGraphControlPU_E.ScrollMinY = 0D;
            this.zedGraphControlPU_E.ScrollMinY2 = 0D;
            this.zedGraphControlPU_E.Size = new System.Drawing.Size(426, 467);
            this.zedGraphControlPU_E.TabIndex = 1;
            // 
            // tabPagePUtabelaE
            // 
            this.tabPagePUtabelaE.Controls.Add(this.labelPU_E);
            this.tabPagePUtabelaE.Controls.Add(this.numericUpDownPU_E);
            this.tabPagePUtabelaE.Controls.Add(this.richTextBoxPU_E);
            this.tabPagePUtabelaE.Location = new System.Drawing.Point(4, 22);
            this.tabPagePUtabelaE.Name = "tabPagePUtabelaE";
            this.tabPagePUtabelaE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePUtabelaE.Size = new System.Drawing.Size(438, 479);
            this.tabPagePUtabelaE.TabIndex = 1;
            this.tabPagePUtabelaE.Text = "Tabela E";
            this.tabPagePUtabelaE.UseVisualStyleBackColor = true;
            // 
            // labelPU_E
            // 
            this.labelPU_E.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelPU_E.AutoSize = true;
            this.labelPU_E.Location = new System.Drawing.Point(2, 458);
            this.labelPU_E.Name = "labelPU_E";
            this.labelPU_E.Size = new System.Drawing.Size(108, 13);
            this.labelPU_E.TabIndex = 84;
            this.labelPU_E.Text = "l. miejsc po przecinku";
            // 
            // numericUpDownPU_E
            // 
            this.numericUpDownPU_E.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDownPU_E.Location = new System.Drawing.Point(121, 456);
            this.numericUpDownPU_E.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownPU_E.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownPU_E.Name = "numericUpDownPU_E";
            this.numericUpDownPU_E.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownPU_E.TabIndex = 83;
            this.numericUpDownPU_E.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // richTextBoxPU_E
            // 
            this.richTextBoxPU_E.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxPU_E.Location = new System.Drawing.Point(3, 6);
            this.richTextBoxPU_E.Name = "richTextBoxPU_E";
            this.richTextBoxPU_E.Size = new System.Drawing.Size(429, 444);
            this.richTextBoxPU_E.TabIndex = 2;
            this.richTextBoxPU_E.Text = "";
            // 
            // tabPagePUwykresB
            // 
            this.tabPagePUwykresB.Controls.Add(this.zedGraphControlPU_B);
            this.tabPagePUwykresB.Location = new System.Drawing.Point(4, 22);
            this.tabPagePUwykresB.Name = "tabPagePUwykresB";
            this.tabPagePUwykresB.Size = new System.Drawing.Size(438, 479);
            this.tabPagePUwykresB.TabIndex = 2;
            this.tabPagePUwykresB.Text = "Wykres B";
            this.tabPagePUwykresB.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlPU_B
            // 
            this.zedGraphControlPU_B.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlPU_B.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControlPU_B.Name = "zedGraphControlPU_B";
            this.zedGraphControlPU_B.ScrollGrace = 0D;
            this.zedGraphControlPU_B.ScrollMaxX = 0D;
            this.zedGraphControlPU_B.ScrollMaxY = 0D;
            this.zedGraphControlPU_B.ScrollMaxY2 = 0D;
            this.zedGraphControlPU_B.ScrollMinX = 0D;
            this.zedGraphControlPU_B.ScrollMinY = 0D;
            this.zedGraphControlPU_B.ScrollMinY2 = 0D;
            this.zedGraphControlPU_B.Size = new System.Drawing.Size(426, 467);
            this.zedGraphControlPU_B.TabIndex = 2;
            // 
            // tabPagePUtabelaB
            // 
            this.tabPagePUtabelaB.Controls.Add(this.labelPU_B);
            this.tabPagePUtabelaB.Controls.Add(this.numericUpDownPU_B);
            this.tabPagePUtabelaB.Controls.Add(this.richTextBoxPU_B);
            this.tabPagePUtabelaB.Location = new System.Drawing.Point(4, 22);
            this.tabPagePUtabelaB.Name = "tabPagePUtabelaB";
            this.tabPagePUtabelaB.Size = new System.Drawing.Size(438, 479);
            this.tabPagePUtabelaB.TabIndex = 3;
            this.tabPagePUtabelaB.Text = "Tabela B";
            this.tabPagePUtabelaB.UseVisualStyleBackColor = true;
            // 
            // labelPU_B
            // 
            this.labelPU_B.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelPU_B.AutoSize = true;
            this.labelPU_B.Location = new System.Drawing.Point(2, 458);
            this.labelPU_B.Name = "labelPU_B";
            this.labelPU_B.Size = new System.Drawing.Size(108, 13);
            this.labelPU_B.TabIndex = 82;
            this.labelPU_B.Text = "l. miejsc po przecinku";
            // 
            // numericUpDownPU_B
            // 
            this.numericUpDownPU_B.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDownPU_B.Location = new System.Drawing.Point(121, 456);
            this.numericUpDownPU_B.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownPU_B.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownPU_B.Name = "numericUpDownPU_B";
            this.numericUpDownPU_B.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownPU_B.TabIndex = 81;
            this.numericUpDownPU_B.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // richTextBoxPU_B
            // 
            this.richTextBoxPU_B.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxPU_B.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxPU_B.Name = "richTextBoxPU_B";
            this.richTextBoxPU_B.Size = new System.Drawing.Size(432, 444);
            this.richTextBoxPU_B.TabIndex = 3;
            this.richTextBoxPU_B.Text = "";
            // 
            // tabPagePUwykresEB
            // 
            this.tabPagePUwykresEB.Controls.Add(this.zedGraphControlPU_EiB);
            this.tabPagePUwykresEB.Location = new System.Drawing.Point(4, 22);
            this.tabPagePUwykresEB.Name = "tabPagePUwykresEB";
            this.tabPagePUwykresEB.Size = new System.Drawing.Size(438, 479);
            this.tabPagePUwykresEB.TabIndex = 4;
            this.tabPagePUwykresEB.Text = "Wykres E i B";
            this.tabPagePUwykresEB.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlPU_EiB
            // 
            this.zedGraphControlPU_EiB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlPU_EiB.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControlPU_EiB.Name = "zedGraphControlPU_EiB";
            this.zedGraphControlPU_EiB.ScrollGrace = 0D;
            this.zedGraphControlPU_EiB.ScrollMaxX = 0D;
            this.zedGraphControlPU_EiB.ScrollMaxY = 0D;
            this.zedGraphControlPU_EiB.ScrollMaxY2 = 0D;
            this.zedGraphControlPU_EiB.ScrollMinX = 0D;
            this.zedGraphControlPU_EiB.ScrollMinY = 0D;
            this.zedGraphControlPU_EiB.ScrollMinY2 = 0D;
            this.zedGraphControlPU_EiB.Size = new System.Drawing.Size(426, 467);
            this.zedGraphControlPU_EiB.TabIndex = 3;
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.Controls.Add(this.richTextBoxDebug);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Size = new System.Drawing.Size(461, 514);
            this.tabPageDebug.TabIndex = 6;
            this.tabPageDebug.Text = "Deb";
            this.tabPageDebug.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDebug
            // 
            this.richTextBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDebug.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxDebug.Name = "richTextBoxDebug";
            this.richTextBoxDebug.Size = new System.Drawing.Size(460, 577);
            this.richTextBoxDebug.TabIndex = 0;
            this.richTextBoxDebug.Text = "";
            // 
            // checkBoxUprzywilejowana
            // 
            this.checkBoxUprzywilejowana.AutoSize = true;
            this.checkBoxUprzywilejowana.Location = new System.Drawing.Point(114, 51);
            this.checkBoxUprzywilejowana.Name = "checkBoxUprzywilejowana";
            this.checkBoxUprzywilejowana.Size = new System.Drawing.Size(105, 17);
            this.checkBoxUprzywilejowana.TabIndex = 27;
            this.checkBoxUprzywilejowana.Text = "Uprzywilejowana";
            this.checkBoxUprzywilejowana.UseVisualStyleBackColor = true;
            // 
            // buttonDodajKlase
            // 
            this.buttonDodajKlase.Location = new System.Drawing.Point(253, 6);
            this.buttonDodajKlase.Name = "buttonDodajKlase";
            this.buttonDodajKlase.Size = new System.Drawing.Size(47, 60);
            this.buttonDodajKlase.TabIndex = 28;
            this.buttonDodajKlase.Text = "Dodaj nową klasę";
            this.buttonDodajKlase.UseVisualStyleBackColor = true;
            this.buttonDodajKlase.Click += new System.EventHandler(this.buttonAddTrClass_Click);
            // 
            // labelLiczbaZrRuchu
            // 
            this.labelNumberOfTrSourcess.AutoSize = true;
            this.labelNumberOfTrSourcess.Location = new System.Drawing.Point(108, 31);
            this.labelNumberOfTrSourcess.Name = "labelLiczbaZrRuchu";
            this.labelNumberOfTrSourcess.Size = new System.Drawing.Size(14, 13);
            this.labelNumberOfTrSourcess.TabIndex = 25;
            this.labelNumberOfTrSourcess.Text = "S";
            this.labelNumberOfTrSourcess.Visible = false;
            // 
            // numericUpDownLiczbaZrRuchu
            // 
            this.numericUpDownLiczbaZrRuchu.Location = new System.Drawing.Point(133, 29);
            this.numericUpDownLiczbaZrRuchu.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLiczbaZrRuchu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLiczbaZrRuchu.Name = "numericUpDownLiczbaZrRuchu";
            this.numericUpDownLiczbaZrRuchu.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownLiczbaZrRuchu.TabIndex = 26;
            this.numericUpDownLiczbaZrRuchu.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownLiczbaZrRuchu.Visible = false;
            // 
            // numericUpDownMi
            // 
            this.numericUpDownMi.DecimalPlaces = 2;
            this.numericUpDownMi.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownMi.Location = new System.Drawing.Point(204, 29);
            this.numericUpDownMi.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownMi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownMi.Name = "numericUpDownMi";
            this.numericUpDownMi.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownMi.TabIndex = 24;
            this.numericUpDownMi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelMi
            // 
            this.labelMi.AutoSize = true;
            this.labelMi.Location = new System.Drawing.Point(182, 31);
            this.labelMi.Name = "labelMi";
            this.labelMi.Size = new System.Drawing.Size(13, 13);
            this.labelMi.TabIndex = 23;
            this.labelMi.Text = "μ";
            // 
            // numericUpDownT
            // 
            this.numericUpDownT.Location = new System.Drawing.Point(133, 3);
            this.numericUpDownT.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownT.Name = "numericUpDownT";
            this.numericUpDownT.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownT.TabIndex = 22;
            this.numericUpDownT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelT
            // 
            this.labelT.AutoSize = true;
            this.labelT.Location = new System.Drawing.Point(111, 8);
            this.labelT.Name = "labelT";
            this.labelT.Size = new System.Drawing.Size(10, 13);
            this.labelT.TabIndex = 21;
            this.labelT.Text = "t";
            // 
            // numericUpDownAt
            // 
            this.numericUpDownAt.Location = new System.Drawing.Point(204, 3);
            this.numericUpDownAt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownAt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAt.Name = "numericUpDownAt";
            this.numericUpDownAt.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownAt.TabIndex = 20;
            this.numericUpDownAt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelPropAt
            // 
            this.labelPropAt.AutoSize = true;
            this.labelPropAt.Location = new System.Drawing.Point(182, 8);
            this.labelPropAt.Name = "labelPropAt";
            this.labelPropAt.Size = new System.Drawing.Size(16, 13);
            this.labelPropAt.TabIndex = 19;
            this.labelPropAt.Text = "at";
            // 
            // radioButtonErlang
            // 
            this.radioButtonErlang.AutoSize = true;
            this.radioButtonErlang.Checked = true;
            this.radioButtonErlang.Location = new System.Drawing.Point(6, 6);
            this.radioButtonErlang.Name = "radioButtonErlang";
            this.radioButtonErlang.Size = new System.Drawing.Size(93, 17);
            this.radioButtonErlang.TabIndex = 16;
            this.radioButtonErlang.TabStop = true;
            this.radioButtonErlang.Text = "Model Erlanga";
            this.radioButtonErlang.UseVisualStyleBackColor = true;
            this.radioButtonErlang.CheckedChanged += new System.EventHandler(this.radioButtonErlang_CheckedChanged);
            // 
            // radioButtonPascal
            // 
            this.radioButtonPascal.AutoSize = true;
            this.radioButtonPascal.Location = new System.Drawing.Point(6, 52);
            this.radioButtonPascal.Name = "radioButtonPascal";
            this.radioButtonPascal.Size = new System.Drawing.Size(95, 17);
            this.radioButtonPascal.TabIndex = 18;
            this.radioButtonPascal.Text = "Model Pascala";
            this.radioButtonPascal.UseVisualStyleBackColor = true;
            this.radioButtonPascal.CheckedChanged += new System.EventHandler(this.radioButtonPascal_CheckedChanged);
            // 
            // radioButtonEngset
            // 
            this.radioButtonEngset.AutoSize = true;
            this.radioButtonEngset.Location = new System.Drawing.Point(6, 29);
            this.radioButtonEngset.Name = "radioButtonEngset";
            this.radioButtonEngset.Size = new System.Drawing.Size(96, 17);
            this.radioButtonEngset.TabIndex = 17;
            this.radioButtonEngset.Text = "Model Engseta";
            this.radioButtonEngset.UseVisualStyleBackColor = true;
            this.radioButtonEngset.CheckedChanged += new System.EventHandler(this.radioButtonEngset_CheckedChanged);
            // 
            // labelMinRuchOf
            // 
            this.labelMinRuchOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMinRuchOf.AutoSize = true;
            this.labelMinRuchOf.Location = new System.Drawing.Point(325, 575);
            this.labelMinRuchOf.Name = "labelMinRuchOf";
            this.labelMinRuchOf.Size = new System.Drawing.Size(110, 13);
            this.labelMinRuchOf.TabIndex = 13;
            this.labelMinRuchOf.Text = "Pocz. ruch oferowany";
            // 
            // numericUpDownMinRuchOf
            // 
            this.numericUpDownMinRuchOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMinRuchOf.DecimalPlaces = 1;
            this.numericUpDownMinRuchOf.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMinRuchOf.Location = new System.Drawing.Point(459, 573);
            this.numericUpDownMinRuchOf.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownMinRuchOf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMinRuchOf.Name = "numericUpDownMinRuchOf";
            this.numericUpDownMinRuchOf.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownMinRuchOf.TabIndex = 20;
            this.numericUpDownMinRuchOf.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownMinRuchOf.ValueChanged += new System.EventHandler(this.numericUpDownNimRuchOf_ValueChanged);
            // 
            // labelKlasyPodgrup
            // 
            this.labelKlasyPodgrup.AutoSize = true;
            this.labelKlasyPodgrup.Location = new System.Drawing.Point(10, 423);
            this.labelKlasyPodgrup.Name = "labelKlasyPodgrup";
            this.labelKlasyPodgrup.Size = new System.Drawing.Size(52, 13);
            this.labelKlasyPodgrup.TabIndex = 67;
            this.labelKlasyPodgrup.Text = "Podgrupy";
            // 
            // groupBoxNowePodgrupy
            // 
            this.groupBoxNowePodgrupy.Controls.Add(this.labelPojDodWiazki);
            this.groupBoxNowePodgrupy.Controls.Add(this.numericUpDownPojemnoscLacza);
            this.groupBoxNowePodgrupy.Controls.Add(this.buttonDodajWiazke);
            this.groupBoxNowePodgrupy.Controls.Add(this.numericUpDownLiczbaPodgrupLzczy);
            this.groupBoxNowePodgrupy.Controls.Add(this.labelLiczbaDodPodgrup);
            this.groupBoxNowePodgrupy.Controls.Add(this.buttonUsunPodgrupyLaczy);
            this.groupBoxNowePodgrupy.Location = new System.Drawing.Point(7, 143);
            this.groupBoxNowePodgrupy.Name = "groupBoxNowePodgrupy";
            this.groupBoxNowePodgrupy.Size = new System.Drawing.Size(159, 133);
            this.groupBoxNowePodgrupy.TabIndex = 68;
            this.groupBoxNowePodgrupy.TabStop = false;
            this.groupBoxNowePodgrupy.Text = "Nowe podgrupy";
            // 
            // progressBarPost
            // 
            this.progressBarPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarPost.Location = new System.Drawing.Point(459, 681);
            this.progressBarPost.Name = "progressBarPost";
            this.progressBarPost.Size = new System.Drawing.Size(321, 19);
            this.progressBarPost.TabIndex = 69;
            this.progressBarPost.UseWaitCursor = true;
            // 
            // labelParametrySymulacji
            // 
            this.labelParametrySymulacji.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelParametrySymulacji.AutoSize = true;
            this.labelParametrySymulacji.Location = new System.Drawing.Point(324, 657);
            this.labelParametrySymulacji.Name = "labelParametrySymulacji";
            this.labelParametrySymulacji.Size = new System.Drawing.Size(99, 13);
            this.labelParametrySymulacji.TabIndex = 70;
            this.labelParametrySymulacji.Text = "Parametry symulacji";
            // 
            // menuStripMenuProg
            // 
            this.menuStripMenuProg.GripMargin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStripMenuProg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.opcjeToolStripMenuItem,
            this.algorytmToolStripMenuItem});
            this.menuStripMenuProg.Location = new System.Drawing.Point(0, 0);
            this.menuStripMenuProg.Name = "menuStripMenuProg";
            this.menuStripMenuProg.Size = new System.Drawing.Size(792, 24);
            this.menuStripMenuProg.TabIndex = 76;
            this.menuStripMenuProg.Text = "Menu";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otwórzSkryptToolStripMenuItem,
            this.zapiszWykresToolStripMenuItem,
            this.zapiszTabeleToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // otwórzSkryptToolStripMenuItem
            // 
            this.otwórzSkryptToolStripMenuItem.Name = "otwórzSkryptToolStripMenuItem";
            this.otwórzSkryptToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.otwórzSkryptToolStripMenuItem.Text = "Otwórz skrypt";
            this.otwórzSkryptToolStripMenuItem.Click += new System.EventHandler(this.otworzSkryptToolStripMenuItem_Click);
            // 
            // zapiszWykresToolStripMenuItem
            // 
            this.zapiszWykresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapiszWynikiToolStripMenuItem,
            this.zapiszWykresBGnuPlotToolStripMenuItem});
            this.zapiszWykresToolStripMenuItem.Name = "zapiszWykresToolStripMenuItem";
            this.zapiszWykresToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.zapiszWykresToolStripMenuItem.Text = "Zapisz wykres";
            // 
            // zapiszWynikiToolStripMenuItem
            // 
            this.zapiszWynikiToolStripMenuItem.Name = "zapiszWynikiToolStripMenuItem";
            this.zapiszWynikiToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.zapiszWynikiToolStripMenuItem.Text = "E GnuPlot";
            this.zapiszWynikiToolStripMenuItem.Click += new System.EventHandler(this.zapiszWynikiToolStripMenuItem_Click);
            // 
            // zapiszWykresBGnuPlotToolStripMenuItem
            // 
            this.zapiszWykresBGnuPlotToolStripMenuItem.Name = "zapiszWykresBGnuPlotToolStripMenuItem";
            this.zapiszWykresBGnuPlotToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.zapiszWykresBGnuPlotToolStripMenuItem.Text = "B GnuPlot";
            this.zapiszWykresBGnuPlotToolStripMenuItem.Click += new System.EventHandler(this.zapiszWykresBGnuPlotToolStripMenuItem_Click_1);
            // 
            // zapiszTabeleToolStripMenuItem
            // 
            this.zapiszTabeleToolStripMenuItem.Name = "zapiszTabeleToolStripMenuItem";
            this.zapiszTabeleToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.zapiszTabeleToolStripMenuItem.Text = "Zapisz Tabele";
            this.zapiszTabeleToolStripMenuItem.Click += new System.EventHandler(this.zapiszTabeleToolStripMenuItem_Click);
            // 
            // opcjeToolStripMenuItem
            // 
            this.opcjeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugowanieToolStripMenuItem});
            this.opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            this.opcjeToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.opcjeToolStripMenuItem.Text = "Opcje";
            // 
            // debugowanieToolStripMenuItem
            // 
            this.debugowanieToolStripMenuItem.Name = "debugowanieToolStripMenuItem";
            this.debugowanieToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.debugowanieToolStripMenuItem.Text = "Debugowanie";
            this.debugowanieToolStripMenuItem.Click += new System.EventHandler(this.debugowanieToolStripMenuItem_Click);
            // 
            // algorytmToolStripMenuItem
            // 
            this.algorytmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajAlgorytmMismToolStripMenuItem,
            this.dodajAlgorytmHyrydowyMISMToolStripMenuItem,
            this.wymStosNeiodpAlgToolStripMenuItem,
            this.EkspAlgToolStripMenuItem});
            this.algorytmToolStripMenuItem.Name = "algorytmToolStripMenuItem";
            this.algorytmToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.algorytmToolStripMenuItem.Text = "Algorytm";
            // 
            // dodajAlgorytmMismToolStripMenuItem
            // 
            this.dodajAlgorytmMismToolStripMenuItem.Name = "dodajAlgorytmMismToolStripMenuItem";
            this.dodajAlgorytmMismToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.dodajAlgorytmMismToolStripMenuItem.Text = "Dodaj algorytm rekurencyjny MISM";
            this.dodajAlgorytmMismToolStripMenuItem.Click += new System.EventHandler(this.dodajAlgorytmMismToolStripMenuItem_Click);
            // 
            // dodajAlgorytmHyrydowyMISMToolStripMenuItem
            // 
            this.dodajAlgorytmHyrydowyMISMToolStripMenuItem.Name = "dodajAlgorytmHyrydowyMISMToolStripMenuItem";
            this.dodajAlgorytmHyrydowyMISMToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.dodajAlgorytmHyrydowyMISMToolStripMenuItem.Text = "Dodaj algorytm hyrydowy MISM";
            this.dodajAlgorytmHyrydowyMISMToolStripMenuItem.Click += new System.EventHandler(this.dodajAlgorytmHyrydowyMISMToolStripMenuItem_Click);
            // 
            // wymStosNeiodpAlgToolStripMenuItem
            // 
            this.wymStosNeiodpAlgToolStripMenuItem.CheckOnClick = true;
            this.wymStosNeiodpAlgToolStripMenuItem.Name = "wymStosNeiodpAlgToolStripMenuItem";
            this.wymStosNeiodpAlgToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.wymStosNeiodpAlgToolStripMenuItem.Text = "Wymuszaj stosowanie neiodpowiednich algorytmów";
            this.wymStosNeiodpAlgToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.wymuszajStosowanieNeiodpowiednichAlgorytmówToolStripMenuItem_CheckStateChanged);
            // 
            // EkspAlgToolStripMenuItem
            // 
            this.EkspAlgToolStripMenuItem.CheckOnClick = true;
            this.EkspAlgToolStripMenuItem.Name = "EkspAlgToolStripMenuItem";
            this.EkspAlgToolStripMenuItem.Size = new System.Drawing.Size(332, 22);
            this.EkspAlgToolStripMenuItem.Text = "Pokaż eksperymentalne algorytmy";
            this.EkspAlgToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.EkspAlgToolStripMenuItem_CheckStateChanged);
            // 
            // openFileDialogSkrypt
            // 
            this.openFileDialogSkrypt.Title = "Otwórz skrypt";
            // 
            // tabControlDodawanieKlasy
            // 
            this.tabControlDodawanieKlasy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlDodawanieKlasy.Controls.Add(this.tabPageKlasaBezProgow);
            this.tabControlDodawanieKlasy.Controls.Add(this.tabPageKlasaZprogrami);
            this.tabControlDodawanieKlasy.Location = new System.Drawing.Point(0, 607);
            this.tabControlDodawanieKlasy.Name = "tabControlDodawanieKlasy";
            this.tabControlDodawanieKlasy.SelectedIndex = 0;
            this.tabControlDodawanieKlasy.Size = new System.Drawing.Size(311, 97);
            this.tabControlDodawanieKlasy.TabIndex = 77;
            // 
            // tabPageKlasaBezProgow
            // 
            this.tabPageKlasaBezProgow.Controls.Add(this.labelMi);
            this.tabPageKlasaBezProgow.Controls.Add(this.numericUpDownMi);
            this.tabPageKlasaBezProgow.Controls.Add(this.numericUpDownAt);
            this.tabPageKlasaBezProgow.Controls.Add(this.labelNumberOfTrSourcess);
            this.tabPageKlasaBezProgow.Controls.Add(this.labelPropAt);
            this.tabPageKlasaBezProgow.Controls.Add(this.checkBoxUprzywilejowana);
            this.tabPageKlasaBezProgow.Controls.Add(this.buttonDodajKlase);
            this.tabPageKlasaBezProgow.Controls.Add(this.labelT);
            this.tabPageKlasaBezProgow.Controls.Add(this.radioButtonErlang);
            this.tabPageKlasaBezProgow.Controls.Add(this.radioButtonEngset);
            this.tabPageKlasaBezProgow.Controls.Add(this.numericUpDownLiczbaZrRuchu);
            this.tabPageKlasaBezProgow.Controls.Add(this.radioButtonPascal);
            this.tabPageKlasaBezProgow.Controls.Add(this.numericUpDownT);
            this.tabPageKlasaBezProgow.Location = new System.Drawing.Point(4, 22);
            this.tabPageKlasaBezProgow.Name = "tabPageKlasaBezProgow";
            this.tabPageKlasaBezProgow.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKlasaBezProgow.Size = new System.Drawing.Size(303, 71);
            this.tabPageKlasaBezProgow.TabIndex = 0;
            this.tabPageKlasaBezProgow.Text = "Nowa klasa";
            this.tabPageKlasaBezProgow.UseVisualStyleBackColor = true;
            // 
            // tabPageKlasaZprogrami
            // 
            this.tabPageKlasaZprogrami.Controls.Add(this.labelProgiS);
            this.tabPageKlasaZprogrami.Controls.Add(this.checkBoxProgiUprzywilejowana);
            this.tabPageKlasaZprogrami.Controls.Add(this.numericUpDownProgiS);
            this.tabPageKlasaZprogrami.Controls.Add(this.buttonProgiUstaw);
            this.tabPageKlasaZprogrami.Controls.Add(this.numericUpDownProgiLiczba);
            this.tabPageKlasaZprogrami.Controls.Add(this.labelProgiLiczbaProgow);
            this.tabPageKlasaZprogrami.Controls.Add(this.numericUpDownProgiPropAt);
            this.tabPageKlasaZprogrami.Controls.Add(this.labelProgiPropAt);
            this.tabPageKlasaZprogrami.Controls.Add(this.radioButtonProgiPascal);
            this.tabPageKlasaZprogrami.Controls.Add(this.radioButtonProgiEngset);
            this.tabPageKlasaZprogrami.Controls.Add(this.radioButtonProgiErlang);
            this.tabPageKlasaZprogrami.Location = new System.Drawing.Point(4, 22);
            this.tabPageKlasaZprogrami.Name = "tabPageKlasaZprogrami";
            this.tabPageKlasaZprogrami.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKlasaZprogrami.Size = new System.Drawing.Size(303, 71);
            this.tabPageKlasaZprogrami.TabIndex = 1;
            this.tabPageKlasaZprogrami.Text = "Nowa klasa z progami";
            this.tabPageKlasaZprogrami.UseVisualStyleBackColor = true;
            // 
            // labelProgiS
            // 
            this.labelProgiS.AutoSize = true;
            this.labelProgiS.Location = new System.Drawing.Point(111, 8);
            this.labelProgiS.Name = "labelProgiS";
            this.labelProgiS.Size = new System.Drawing.Size(14, 13);
            this.labelProgiS.TabIndex = 5;
            this.labelProgiS.Text = "S";
            this.labelProgiS.Visible = false;
            // 
            // checkBoxProgiUprzywilejowana
            // 
            this.checkBoxProgiUprzywilejowana.AutoSize = true;
            this.checkBoxProgiUprzywilejowana.Location = new System.Drawing.Point(114, 51);
            this.checkBoxProgiUprzywilejowana.Name = "checkBoxProgiUprzywilejowana";
            this.checkBoxProgiUprzywilejowana.Size = new System.Drawing.Size(105, 17);
            this.checkBoxProgiUprzywilejowana.TabIndex = 11;
            this.checkBoxProgiUprzywilejowana.Text = "Uprzywilejowana";
            this.checkBoxProgiUprzywilejowana.UseVisualStyleBackColor = true;
            // 
            // numericUpDownProgiS
            // 
            this.numericUpDownProgiS.Location = new System.Drawing.Point(131, 3);
            this.numericUpDownProgiS.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownProgiS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownProgiS.Name = "numericUpDownProgiS";
            this.numericUpDownProgiS.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownProgiS.TabIndex = 6;
            this.numericUpDownProgiS.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownProgiS.Visible = false;
            // 
            // buttonProgiUstaw
            // 
            this.buttonProgiUstaw.Location = new System.Drawing.Point(253, 6);
            this.buttonProgiUstaw.Name = "buttonProgiUstaw";
            this.buttonProgiUstaw.Size = new System.Drawing.Size(47, 60);
            this.buttonProgiUstaw.TabIndex = 9;
            this.buttonProgiUstaw.Text = "Dodaj nową klasę";
            this.buttonProgiUstaw.UseVisualStyleBackColor = true;
            this.buttonProgiUstaw.Click += new System.EventHandler(this.buttonProgiUstaw_Click);
            // 
            // numericUpDownProgiLiczba
            // 
            this.numericUpDownProgiLiczba.Location = new System.Drawing.Point(204, 29);
            this.numericUpDownProgiLiczba.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownProgiLiczba.Name = "numericUpDownProgiLiczba";
            this.numericUpDownProgiLiczba.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownProgiLiczba.TabIndex = 8;
            this.numericUpDownProgiLiczba.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelProgiLiczbaProgow
            // 
            this.labelProgiLiczbaProgow.AutoSize = true;
            this.labelProgiLiczbaProgow.Location = new System.Drawing.Point(111, 31);
            this.labelProgiLiczbaProgow.Name = "labelProgiLiczbaProgow";
            this.labelProgiLiczbaProgow.Size = new System.Drawing.Size(76, 13);
            this.labelProgiLiczbaProgow.TabIndex = 7;
            this.labelProgiLiczbaProgow.Text = "Liczba progów";
            // 
            // numericUpDownProgiPropAt
            // 
            this.numericUpDownProgiPropAt.Location = new System.Drawing.Point(204, 3);
            this.numericUpDownProgiPropAt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownProgiPropAt.Name = "numericUpDownProgiPropAt";
            this.numericUpDownProgiPropAt.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownProgiPropAt.TabIndex = 4;
            this.numericUpDownProgiPropAt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelProgiPropAt
            // 
            this.labelProgiPropAt.AutoSize = true;
            this.labelProgiPropAt.Location = new System.Drawing.Point(182, 8);
            this.labelProgiPropAt.Name = "labelProgiPropAt";
            this.labelProgiPropAt.Size = new System.Drawing.Size(16, 13);
            this.labelProgiPropAt.TabIndex = 3;
            this.labelProgiPropAt.Text = "at";
            // 
            // radioButtonProgiPascal
            // 
            this.radioButtonProgiPascal.AutoSize = true;
            this.radioButtonProgiPascal.Location = new System.Drawing.Point(6, 52);
            this.radioButtonProgiPascal.Name = "radioButtonProgiPascal";
            this.radioButtonProgiPascal.Size = new System.Drawing.Size(95, 17);
            this.radioButtonProgiPascal.TabIndex = 2;
            this.radioButtonProgiPascal.Text = "Model Pascala";
            this.radioButtonProgiPascal.UseVisualStyleBackColor = true;
            this.radioButtonProgiPascal.CheckedChanged += new System.EventHandler(this.radioButtonProgiPascal_CheckedChanged);
            // 
            // radioButtonProgiEngset
            // 
            this.radioButtonProgiEngset.AutoSize = true;
            this.radioButtonProgiEngset.Location = new System.Drawing.Point(6, 29);
            this.radioButtonProgiEngset.Name = "radioButtonProgiEngset";
            this.radioButtonProgiEngset.Size = new System.Drawing.Size(96, 17);
            this.radioButtonProgiEngset.TabIndex = 1;
            this.radioButtonProgiEngset.Text = "Model Engseta";
            this.radioButtonProgiEngset.UseVisualStyleBackColor = true;
            this.radioButtonProgiEngset.CheckedChanged += new System.EventHandler(this.radioButtonProgiEngset_CheckedChanged);
            // 
            // radioButtonProgiErlang
            // 
            this.radioButtonProgiErlang.AutoSize = true;
            this.radioButtonProgiErlang.Checked = true;
            this.radioButtonProgiErlang.Location = new System.Drawing.Point(6, 6);
            this.radioButtonProgiErlang.Name = "radioButtonProgiErlang";
            this.radioButtonProgiErlang.Size = new System.Drawing.Size(93, 17);
            this.radioButtonProgiErlang.TabIndex = 0;
            this.radioButtonProgiErlang.TabStop = true;
            this.radioButtonProgiErlang.Text = "Model Erlanga";
            this.radioButtonProgiErlang.UseVisualStyleBackColor = true;
            this.radioButtonProgiErlang.CheckedChanged += new System.EventHandler(this.radioButtonProgiErlang_CheckedChanged);
            // 
            // textBoxPojemnoscKolejki
            // 
            this.textBoxPojemnoscKolejki.Location = new System.Drawing.Point(118, 87);
            this.textBoxPojemnoscKolejki.Name = "textBoxPojemnoscKolejki";
            this.textBoxPojemnoscKolejki.ReadOnly = true;
            this.textBoxPojemnoscKolejki.Size = new System.Drawing.Size(42, 20);
            this.textBoxPojemnoscKolejki.TabIndex = 78;
            this.textBoxPojemnoscKolejki.TabStop = false;
            this.textBoxPojemnoscKolejki.Text = "0";
            // 
            // labelPojKolejki
            // 
            this.labelPojKolejki.AutoSize = true;
            this.labelPojKolejki.Location = new System.Drawing.Point(13, 90);
            this.labelPojKolejki.Name = "labelPojKolejki";
            this.labelPojKolejki.Size = new System.Drawing.Size(92, 13);
            this.labelPojKolejki.TabIndex = 79;
            this.labelPojKolejki.Text = "Pojemność kolejki";
            // 
            // textBoxLiczbaKolejek
            // 
            this.textBoxLiczbaKolejek.Location = new System.Drawing.Point(118, 113);
            this.textBoxLiczbaKolejek.Name = "textBoxLiczbaKolejek";
            this.textBoxLiczbaKolejek.ReadOnly = true;
            this.textBoxLiczbaKolejek.Size = new System.Drawing.Size(42, 20);
            this.textBoxLiczbaKolejek.TabIndex = 80;
            this.textBoxLiczbaKolejek.TabStop = false;
            this.textBoxLiczbaKolejek.Text = "0";
            // 
            // labelLKolejek
            // 
            this.labelLKolejek.AutoSize = true;
            this.labelLKolejek.Location = new System.Drawing.Point(13, 116);
            this.labelLKolejek.Name = "labelLKolejek";
            this.labelLKolejek.Size = new System.Drawing.Size(75, 13);
            this.labelLKolejek.TabIndex = 81;
            this.labelLKolejek.Text = "Liczba kolejek";
            // 
            // comboBoxParametrySymulacji
            // 
            this.comboBoxParametrySymulacji.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxParametrySymulacji.FormattingEnabled = true;
            this.comboBoxParametrySymulacji.Location = new System.Drawing.Point(459, 654);
            this.comboBoxParametrySymulacji.Name = "comboBoxParametrySymulacji";
            this.comboBoxParametrySymulacji.Size = new System.Drawing.Size(321, 21);
            this.comboBoxParametrySymulacji.TabIndex = 82;
            this.comboBoxParametrySymulacji.SelectedValueChanged += new System.EventHandler(this.comboBoxParametrySymulacji_SelectedValueChanged);
            // 
            // comboBoxSystemy
            // 
            this.comboBoxSystemy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSystemy.FormattingEnabled = true;
            this.comboBoxSystemy.Location = new System.Drawing.Point(459, 627);
            this.comboBoxSystemy.Name = "comboBoxSystemy";
            this.comboBoxSystemy.Size = new System.Drawing.Size(321, 21);
            this.comboBoxSystemy.TabIndex = 83;
            this.comboBoxSystemy.SelectedValueChanged += new System.EventHandler(this.comboBoxSystemy_SelectedValueChanged);
            // 
            // labelModelSystemu
            // 
            this.labelModelSystemu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelModelSystemu.AutoSize = true;
            this.labelModelSystemu.Location = new System.Drawing.Point(325, 630);
            this.labelModelSystemu.Name = "labelModelSystemu";
            this.labelModelSystemu.Size = new System.Drawing.Size(77, 13);
            this.labelModelSystemu.TabIndex = 84;
            this.labelModelSystemu.Text = "Model systemu";
            // 
            // labelPostep
            // 
            this.labelPostep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPostep.AutoSize = true;
            this.labelPostep.Location = new System.Drawing.Point(325, 687);
            this.labelPostep.Name = "labelPostep";
            this.labelPostep.Size = new System.Drawing.Size(40, 13);
            this.labelPostep.TabIndex = 85;
            this.labelPostep.Text = "Postęp";
            // 
            // FormMainWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 711);
            this.Controls.Add(this.labelPostep);
            this.Controls.Add(this.labelModelSystemu);
            this.Controls.Add(this.comboBoxSystemy);
            this.Controls.Add(this.comboBoxParametrySymulacji);
            this.Controls.Add(this.labelLKolejek);
            this.Controls.Add(this.textBoxLiczbaKolejek);
            this.Controls.Add(this.labelPojKolejki);
            this.Controls.Add(this.textBoxPojemnoscKolejki);
            this.Controls.Add(this.tabControlDodawanieKlasy);
            this.Controls.Add(this.labelParametrySymulacji);
            this.Controls.Add(this.progressBarPost);
            this.Controls.Add(this.groupBoxNowePodgrupy);
            this.Controls.Add(this.labelKlasyPodgrup);
            this.Controls.Add(this.tabControlPrezentacja);
            this.Controls.Add(this.groupBoxAlgWybPodgrupy);
            this.Controls.Add(this.groupBoxAlgRezerwacji);
            this.Controls.Add(this.labelGrRezerwacji);
            this.Controls.Add(this.numericUpDownGranicaRezerwacji);
            this.Controls.Add(this.listBoxPodgrupy);
            this.Controls.Add(this.buttonUsunKlase);
            this.Controls.Add(this.textBoxLBad);
            this.Controls.Add(this.labelLiczbaBadan);
            this.Controls.Add(this.textBoxLiczbaPodgrup);
            this.Controls.Add(this.textBoxPojCalkowita);
            this.Controls.Add(this.labelLiPodgrup);
            this.Controls.Add(this.labelPojWiazki);
            this.Controls.Add(this.numericUpDownPrzyrRuchOf);
            this.Controls.Add(this.numericUpDownMaxRuchOf);
            this.Controls.Add(this.numericUpDownMinRuchOf);
            this.Controls.Add(this.buttonStartAddStop);
            this.Controls.Add(this.labelPrzyrostRuchu);
            this.Controls.Add(this.labelMaxRuchOf);
            this.Controls.Add(this.labelMinRuchOf);
            this.Controls.Add(this.labelMozliweAlgorytmy);
            this.Controls.Add(this.listBoxAlgorytmy);
            this.Controls.Add(this.labelKlasy);
            this.Controls.Add(this.listBoxKlasy);
            this.Controls.Add(this.menuStripMenuProg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMenuProg;
            this.MinimumSize = new System.Drawing.Size(800, 738);
            this.Name = "FormMainWindow";
            this.Text = "MultiConvTool BD © Adam Kaliszan";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.glOkno_FormClosed);
            this.Load += new System.EventHandler(this.glOkno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxRuchOf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrzyrRuchOf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGranicaRezerwacji)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPojemnoscLacza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiczbaPodgrupLzczy)).EndInit();
            this.groupBoxAlgRezerwacji.ResumeLayout(false);
            this.groupBoxAlgRezerwacji.PerformLayout();
            this.groupBoxAlgWybPodgrupy.ResumeLayout(false);
            this.groupBoxAlgWybPodgrupy.PerformLayout();
            this.tabControlPrezentacja.ResumeLayout(false);
            this.tabPageE.ResumeLayout(false);
            this.tabControlE.ResumeLayout(false);
            this.tabPageEwykres.ResumeLayout(false);
            this.tabPageEtabela.ResumeLayout(false);
            this.tabPageEtabela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLMiejscE)).EndInit();
            this.tabPageB.ResumeLayout(false);
            this.tabControlB.ResumeLayout(false);
            this.tabPageBwykres.ResumeLayout(false);
            this.tabPageBtabela.ResumeLayout(false);
            this.tabPageBtabela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLMiejscB)).EndInit();
            this.tabPageEBwykres.ResumeLayout(false);
            this.tabPageBladWzgl.ResumeLayout(false);
            this.tabPageBladWzgl.PerformLayout();
            this.tabControlBladWzgledny.ResumeLayout(false);
            this.tabPageBlWzglWykres.ResumeLayout(false);
            this.tabPagePU.ResumeLayout(false);
            this.tabControlPU.ResumeLayout(false);
            this.tabPagePUwykresE.ResumeLayout(false);
            this.tabPagePUtabelaE.ResumeLayout(false);
            this.tabPagePUtabelaE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPU_E)).EndInit();
            this.tabPagePUwykresB.ResumeLayout(false);
            this.tabPagePUtabelaB.ResumeLayout(false);
            this.tabPagePUtabelaB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPU_B)).EndInit();
            this.tabPagePUwykresEB.ResumeLayout(false);
            this.tabPageDebug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiczbaZrRuchu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinRuchOf)).EndInit();
            this.groupBoxNowePodgrupy.ResumeLayout(false);
            this.groupBoxNowePodgrupy.PerformLayout();
            this.menuStripMenuProg.ResumeLayout(false);
            this.menuStripMenuProg.PerformLayout();
            this.tabControlDodawanieKlasy.ResumeLayout(false);
            this.tabPageKlasaBezProgow.ResumeLayout(false);
            this.tabPageKlasaBezProgow.PerformLayout();
            this.tabPageKlasaZprogrami.ResumeLayout(false);
            this.tabPageKlasaZprogrami.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgiS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgiLiczba)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgiPropAt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxKlasy;
        private System.Windows.Forms.Label labelKlasy;
        private System.Windows.Forms.ListBox listBoxAlgorytmy;
        private System.Windows.Forms.Label labelMozliweAlgorytmy;
        private System.Windows.Forms.Label labelMaxRuchOf;
        private System.Windows.Forms.Label labelPrzyrostRuchu;
        private System.Windows.Forms.Button buttonStartAddStop;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxRuchOf;
        private System.Windows.Forms.NumericUpDown numericUpDownPrzyrRuchOf;
        private System.Windows.Forms.Label labelPojWiazki;
        private System.Windows.Forms.Label labelLiPodgrup;
        private System.Windows.Forms.TextBox textBoxPojCalkowita;
        private System.Windows.Forms.TextBox textBoxLiczbaPodgrup;
        private System.Windows.Forms.Label labelLiczbaBadan;
        private System.Windows.Forms.TextBox textBoxLBad;
        private System.Windows.Forms.Button buttonUsunKlase;
        private System.Windows.Forms.ListBox listBoxPodgrupy;
        private System.Windows.Forms.Button buttonUsunPodgrupyLaczy;
        private System.Windows.Forms.Button buttonDodajWiazke;
        private System.Windows.Forms.NumericUpDown numericUpDownGranicaRezerwacji;
        private System.Windows.Forms.NumericUpDown numericUpDownPojemnoscLacza;
        private System.Windows.Forms.NumericUpDown numericUpDownLiczbaPodgrupLzczy;
        private System.Windows.Forms.Label labelLiczbaDodPodgrup;
        private System.Windows.Forms.Label labelPojDodWiazki;
        private System.Windows.Forms.Label labelGrRezerwacji;
        private System.Windows.Forms.RadioButton radioButtonBrakRezerwacji;
        private System.Windows.Forms.GroupBox groupBoxAlgRezerwacji;
        private System.Windows.Forms.RadioButton radioButtonR3;
        private System.Windows.Forms.RadioButton radioButtonR1;
        private System.Windows.Forms.GroupBox groupBoxAlgWybPodgrupy;
        private System.Windows.Forms.RadioButton radioButtonAlgCykliczny;
        private System.Windows.Forms.RadioButton radioButtonAlgKolejnosciowy;
        private System.Windows.Forms.RadioButton radioButtonAlgLosowyProporcjonalny;
        private System.Windows.Forms.RadioButton radioButtonAlgLosowy;
        private System.Windows.Forms.RadioButton radioButtonAlgLosowyProporcjonalnyWolny;
        private System.Windows.Forms.TabControl tabControlPrezentacja;
        private System.Windows.Forms.TabPage tabPageModelSystemu;
        private System.Windows.Forms.TabPage tabPageE;
        private ZedGraph.ZedGraphControl zedGraphControlWykrE;
        private System.Windows.Forms.TabPage tabPagePU;
        private System.Windows.Forms.TabPage tabPageB;
        private System.Windows.Forms.TabPage tabPageEBwykres;
        private ZedGraph.ZedGraphControl zedGraphControlWykrB;
        private ZedGraph.ZedGraphControl zedGraphControlWykrEB;
        private System.Windows.Forms.RadioButton radioButtonErlang;
        private System.Windows.Forms.RadioButton radioButtonPascal;
        private System.Windows.Forms.RadioButton radioButtonEngset;
        private System.Windows.Forms.NumericUpDown numericUpDownLiczbaZrRuchu;
        private System.Windows.Forms.Label labelNumberOfTrSourcess;
        private System.Windows.Forms.NumericUpDown numericUpDownMi;
        private System.Windows.Forms.Label labelMi;
        private System.Windows.Forms.NumericUpDown numericUpDownT;
        private System.Windows.Forms.Label labelT;
        private System.Windows.Forms.NumericUpDown numericUpDownAt;
        private System.Windows.Forms.Label labelPropAt;
        private System.Windows.Forms.Label labelMinRuchOf;
        private System.Windows.Forms.NumericUpDown numericUpDownMinRuchOf;
        private System.Windows.Forms.Button buttonDodajKlase;
        private System.Windows.Forms.Label labelKlasyPodgrup;
        private System.Windows.Forms.GroupBox groupBoxNowePodgrupy;
        private System.Windows.Forms.CheckBox checkBoxUprzywilejowana;
        private System.Windows.Forms.ProgressBar progressBarPost;
        private System.Windows.Forms.Label labelParametrySymulacji;
        private System.Windows.Forms.MenuStrip menuStripMenuProg;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogSkrypt;
        private System.Windows.Forms.ToolStripMenuItem otwórzSkryptToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageDebug;
        private System.Windows.Forms.ToolStripMenuItem debugowanieToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxDebug;
        private System.Windows.Forms.ToolStripMenuItem algorytmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajAlgorytmMismToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wymStosNeiodpAlgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EkspAlgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajAlgorytmHyrydowyMISMToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogGnuPlot;
        private System.Windows.Forms.TabControl tabControlDodawanieKlasy;
        private System.Windows.Forms.TabPage tabPageKlasaBezProgow;
        private System.Windows.Forms.TabPage tabPageKlasaZprogrami;
        private System.Windows.Forms.RadioButton radioButtonProgiPascal;
        private System.Windows.Forms.RadioButton radioButtonProgiEngset;
        private System.Windows.Forms.RadioButton radioButtonProgiErlang;
        private System.Windows.Forms.Label labelProgiLiczbaProgow;
        private System.Windows.Forms.NumericUpDown numericUpDownProgiS;
        private System.Windows.Forms.Label labelProgiS;
        private System.Windows.Forms.NumericUpDown numericUpDownProgiPropAt;
        private System.Windows.Forms.Label labelProgiPropAt;
        private System.Windows.Forms.Button buttonProgiUstaw;
        private System.Windows.Forms.NumericUpDown numericUpDownProgiLiczba;
        private System.Windows.Forms.CheckBox checkBoxProgiUprzywilejowana;
        private System.Windows.Forms.ToolStripMenuItem zapiszWykresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszWynikiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszWykresBGnuPlotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszTabeleToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageBladWzgl;
        private System.Windows.Forms.Label labelAlgReferencyjny;
        private System.Windows.Forms.ListBox listBoxAlgReferencyjny;
        private ZedGraph.ZedGraphControl zedGraphControlBladWzgl;
        private System.Windows.Forms.TextBox textBoxPojemnoscKolejki;
        private System.Windows.Forms.Label labelPojKolejki;
        private System.Windows.Forms.TextBox textBoxLiczbaKolejek;
        private System.Windows.Forms.Label labelLKolejek;
        private System.Windows.Forms.ComboBox comboBoxParametrySymulacji;
        private System.Windows.Forms.ComboBox comboBoxSystemy;
        private System.Windows.Forms.Label labelModelSystemu;
        private System.Windows.Forms.Label labelPostep;
        private System.Windows.Forms.TabControl tabControlE;
        private System.Windows.Forms.TabPage tabPageEwykres;
        private System.Windows.Forms.TabPage tabPageEtabela;
        private System.Windows.Forms.RichTextBox richTextBoxTabelaE;
        private System.Windows.Forms.TabControl tabControlB;
        private System.Windows.Forms.TabPage tabPageBwykres;
        private System.Windows.Forms.TabPage tabPageBtabela;
        private System.Windows.Forms.RichTextBox richTextBoxTabelaB;
        private System.Windows.Forms.TabControl tabControlBladWzgledny;
        private System.Windows.Forms.TabPage tabPageBlWzglWykres;
        private System.Windows.Forms.TabPage tabPageBlWzglTabela;
        private System.Windows.Forms.NumericUpDown numericUpDownLMiejscE;
        private System.Windows.Forms.Label labelLMiejscPoPrzecinkuE;
        private System.Windows.Forms.NumericUpDown numericUpDownLMiejscB;
        private System.Windows.Forms.Label labelLMiejscPoPrzecinkuB;
        private System.Windows.Forms.TabControl tabControlPU;
        private System.Windows.Forms.TabPage tabPagePUwykresE;
        private System.Windows.Forms.TabPage tabPagePUtabelaE;
        private ZedGraph.ZedGraphControl zedGraphControlPU_E;
        private System.Windows.Forms.TabPage tabPagePUwykresB;
        private ZedGraph.ZedGraphControl zedGraphControlPU_B;
        private System.Windows.Forms.TabPage tabPagePUtabelaB;
        private System.Windows.Forms.TabPage tabPagePUwykresEB;
        private ZedGraph.ZedGraphControl zedGraphControlPU_EiB;
        private System.Windows.Forms.Label labelPU_E;
        private System.Windows.Forms.NumericUpDown numericUpDownPU_E;
        private System.Windows.Forms.RichTextBox richTextBoxPU_E;
        private System.Windows.Forms.Label labelPU_B;
        private System.Windows.Forms.NumericUpDown numericUpDownPU_B;
        private System.Windows.Forms.RichTextBox richTextBoxPU_B;
    }
}

