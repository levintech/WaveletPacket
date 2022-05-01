namespace WaveletPacket
{
    partial class Main
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
            this.btnStart = new System.Windows.Forms.Button();
            this.picResult = new System.Windows.Forms.PictureBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDbNum = new System.Windows.Forms.ComboBox();
            this.btnImgCompress = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBright = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stegoHidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stegoExtractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.btnInv = new System.Windows.Forms.Button();
            this.cmbEntropy = new System.Windows.Forms.ComboBox();
            this.txtEntropy = new System.Windows.Forms.TextBox();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnKeyGen = new System.Windows.Forms.Button();
            this.lblKey = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDecryptTime = new System.Windows.Forms.Label();
            this.lblDecompressTime = new System.Windows.Forms.Label();
            this.lblCompressTime = new System.Windows.Forms.Label();
            this.lblEncryptTime = new System.Windows.Forms.Label();
            this.lblDecrypted = new System.Windows.Forms.Label();
            this.lblDecompressed = new System.Windows.Forms.Label();
            this.lblCompressed = new System.Windows.Forms.Label();
            this.lblCipherText = new System.Windows.Forms.Label();
            this.lblPlainText = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDecrypted = new System.Windows.Forms.RichTextBox();
            this.txtDecompressed = new System.Windows.Forms.RichTextBox();
            this.txtCompressed = new System.Windows.Forms.RichTextBox();
            this.txtCypherText = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnTxtDecrypt = new System.Windows.Forms.Button();
            this.btnTxtDecompress = new System.Windows.Forms.Button();
            this.btnTxtCompress = new System.Windows.Forms.Button();
            this.btnTxtEncrypt = new System.Windows.Forms.Button();
            this.groupEnc = new System.Windows.Forms.GroupBox();
            this.txtPlainText = new System.Windows.Forms.RichTextBox();
            this.picLL = new System.Windows.Forms.PictureBox();
            this.picHL = new System.Windows.Forms.PictureBox();
            this.picHH = new System.Windows.Forms.PictureBox();
            this.picLH = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupEnc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLH)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(1170, 269);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(81, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Analyzer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // picResult
            // 
            this.picResult.Location = new System.Drawing.Point(12, 27);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(512, 512);
            this.picResult.TabIndex = 1;
            this.picResult.TabStop = false;
            // 
            // cmbType
            // 
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "haar",
            "db"});
            this.cmbType.Location = new System.Drawing.Point(1170, 173);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(81, 24);
            this.cmbType.TabIndex = 2;
            this.cmbType.TextChanged += new System.EventHandler(this.cmbType_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1180, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wavelet";
            // 
            // cmbDbNum
            // 
            this.cmbDbNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDbNum.FormattingEnabled = true;
            this.cmbDbNum.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbDbNum.Location = new System.Drawing.Point(1262, 173);
            this.cmbDbNum.Name = "cmbDbNum";
            this.cmbDbNum.Size = new System.Drawing.Size(79, 24);
            this.cmbDbNum.TabIndex = 4;
            // 
            // btnImgCompress
            // 
            this.btnImgCompress.Enabled = false;
            this.btnImgCompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgCompress.Location = new System.Drawing.Point(1170, 309);
            this.btnImgCompress.Name = "btnImgCompress";
            this.btnImgCompress.Size = new System.Drawing.Size(81, 23);
            this.btnImgCompress.TabIndex = 5;
            this.btnImgCompress.Text = "Compress";
            this.btnImgCompress.UseVisualStyleBackColor = true;
            this.btnImgCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(1170, 476);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(81, 23);
            this.btnEnd.TabIndex = 6;
            this.btnEnd.Text = "End";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1222, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "DataSize";
            // 
            // txtSize
            // 
            this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSize.Location = new System.Drawing.Point(1170, 56);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(173, 22);
            this.txtSize.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1190, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Bright";
            // 
            // cmbBright
            // 
            this.cmbBright.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBright.FormattingEnabled = true;
            this.cmbBright.Items.AddRange(new object[] {
            "1.0",
            "2.0",
            "3.0",
            "4.0",
            "5.0"});
            this.cmbBright.Location = new System.Drawing.Point(1170, 121);
            this.cmbBright.Name = "cmbBright";
            this.cmbBright.Size = new System.Drawing.Size(81, 24);
            this.cmbBright.TabIndex = 9;
            this.cmbBright.Text = "1";
            this.cmbBright.TextChanged += new System.EventHandler(this.cmbBright_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1364, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.stegoHidToolStripMenuItem,
            this.stegoExtractToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // stegoHidToolStripMenuItem
            // 
            this.stegoHidToolStripMenuItem.Name = "stegoHidToolStripMenuItem";
            this.stegoHidToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.stegoHidToolStripMenuItem.Text = "stego hid ";
            this.stegoHidToolStripMenuItem.Click += new System.EventHandler(this.stegoHidToolStripMenuItem_Click);
            // 
            // stegoExtractToolStripMenuItem
            // 
            this.stegoExtractToolStripMenuItem.Name = "stegoExtractToolStripMenuItem";
            this.stegoExtractToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.stegoExtractToolStripMenuItem.Text = "stego extract ";
            this.stegoExtractToolStripMenuItem.Click += new System.EventHandler(this.stegoExtractToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1282, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Level";
            // 
            // cmbLevel
            // 
            this.cmbLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.cmbLevel.Location = new System.Drawing.Point(1262, 121);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(81, 24);
            this.cmbLevel.TabIndex = 12;
            this.cmbLevel.Text = "2";
            this.cmbLevel.TextChanged += new System.EventHandler(this.cmbLevel_TextChanged);
            // 
            // btnInv
            // 
            this.btnInv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInv.Location = new System.Drawing.Point(1260, 269);
            this.btnInv.Name = "btnInv";
            this.btnInv.Size = new System.Drawing.Size(81, 23);
            this.btnInv.TabIndex = 14;
            this.btnInv.Text = "Inverse";
            this.btnInv.UseVisualStyleBackColor = true;
            this.btnInv.Click += new System.EventHandler(this.btnInv_Click);
            // 
            // cmbEntropy
            // 
            this.cmbEntropy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEntropy.FormattingEnabled = true;
            this.cmbEntropy.Items.AddRange(new object[] {
            "shannon",
            "threshold",
            "norm"});
            this.cmbEntropy.Location = new System.Drawing.Point(1170, 227);
            this.cmbEntropy.Name = "cmbEntropy";
            this.cmbEntropy.Size = new System.Drawing.Size(81, 24);
            this.cmbEntropy.TabIndex = 15;
            this.cmbEntropy.Text = "shannon";
            // 
            // txtEntropy
            // 
            this.txtEntropy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntropy.Location = new System.Drawing.Point(1262, 227);
            this.txtEntropy.Name = "txtEntropy";
            this.txtEntropy.Size = new System.Drawing.Size(79, 22);
            this.txtEntropy.TabIndex = 16;
            this.txtEntropy.Text = "1";
            // 
            // btnDecompress
            // 
            this.btnDecompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecompress.Location = new System.Drawing.Point(1262, 308);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(93, 23);
            this.btnDecompress.TabIndex = 17;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.UseVisualStyleBackColor = true;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // btnKeyGen
            // 
            this.btnKeyGen.Location = new System.Drawing.Point(12, 545);
            this.btnKeyGen.Name = "btnKeyGen";
            this.btnKeyGen.Size = new System.Drawing.Size(91, 30);
            this.btnKeyGen.TabIndex = 21;
            this.btnKeyGen.Text = "Generate Key";
            this.btnKeyGen.UseVisualStyleBackColor = true;
            this.btnKeyGen.Click += new System.EventHandler(this.btnKeyGen_Click);
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(143, 554);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(0, 13);
            this.lblKey.TabIndex = 20;
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTime.Location = new System.Drawing.Point(715, 167);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(0, 17);
            this.lblTotalTime.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(715, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "Total Time:";
            // 
            // lblDecryptTime
            // 
            this.lblDecryptTime.AutoSize = true;
            this.lblDecryptTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecryptTime.Location = new System.Drawing.Point(230, 258);
            this.lblDecryptTime.Name = "lblDecryptTime";
            this.lblDecryptTime.Size = new System.Drawing.Size(0, 17);
            this.lblDecryptTime.TabIndex = 28;
            // 
            // lblDecompressTime
            // 
            this.lblDecompressTime.AutoSize = true;
            this.lblDecompressTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecompressTime.Location = new System.Drawing.Point(528, 258);
            this.lblDecompressTime.Name = "lblDecompressTime";
            this.lblDecompressTime.Size = new System.Drawing.Size(0, 17);
            this.lblDecompressTime.TabIndex = 27;
            // 
            // lblCompressTime
            // 
            this.lblCompressTime.AutoSize = true;
            this.lblCompressTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompressTime.Location = new System.Drawing.Point(528, 113);
            this.lblCompressTime.Name = "lblCompressTime";
            this.lblCompressTime.Size = new System.Drawing.Size(0, 17);
            this.lblCompressTime.TabIndex = 26;
            // 
            // lblEncryptTime
            // 
            this.lblEncryptTime.AutoSize = true;
            this.lblEncryptTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncryptTime.Location = new System.Drawing.Point(230, 113);
            this.lblEncryptTime.Name = "lblEncryptTime";
            this.lblEncryptTime.Size = new System.Drawing.Size(0, 17);
            this.lblEncryptTime.TabIndex = 25;
            // 
            // lblDecrypted
            // 
            this.lblDecrypted.AutoSize = true;
            this.lblDecrypted.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecrypted.Location = new System.Drawing.Point(77, 278);
            this.lblDecrypted.Name = "lblDecrypted";
            this.lblDecrypted.Size = new System.Drawing.Size(0, 17);
            this.lblDecrypted.TabIndex = 24;
            // 
            // lblDecompressed
            // 
            this.lblDecompressed.AutoSize = true;
            this.lblDecompressed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecompressed.Location = new System.Drawing.Point(378, 278);
            this.lblDecompressed.Name = "lblDecompressed";
            this.lblDecompressed.Size = new System.Drawing.Size(0, 17);
            this.lblDecompressed.TabIndex = 23;
            // 
            // lblCompressed
            // 
            this.lblCompressed.AutoSize = true;
            this.lblCompressed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompressed.Location = new System.Drawing.Point(681, 99);
            this.lblCompressed.Name = "lblCompressed";
            this.lblCompressed.Size = new System.Drawing.Size(0, 17);
            this.lblCompressed.TabIndex = 22;
            // 
            // lblCipherText
            // 
            this.lblCipherText.AutoSize = true;
            this.lblCipherText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCipherText.Location = new System.Drawing.Point(378, 99);
            this.lblCipherText.Name = "lblCipherText";
            this.lblCipherText.Size = new System.Drawing.Size(0, 17);
            this.lblCipherText.TabIndex = 21;
            // 
            // lblPlainText
            // 
            this.lblPlainText.AutoSize = true;
            this.lblPlainText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlainText.Location = new System.Drawing.Point(77, 133);
            this.lblPlainText.Name = "lblPlainText";
            this.lblPlainText.Size = new System.Drawing.Size(0, 17);
            this.lblPlainText.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 554);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Key:";
            // 
            // txtDecrypted
            // 
            this.txtDecrypted.Location = new System.Drawing.Point(24, 103);
            this.txtDecrypted.Name = "txtDecrypted";
            this.txtDecrypted.Size = new System.Drawing.Size(193, 47);
            this.txtDecrypted.TabIndex = 19;
            this.txtDecrypted.Text = "";
            // 
            // txtDecompressed
            // 
            this.txtDecompressed.Location = new System.Drawing.Point(321, 100);
            this.txtDecompressed.Name = "txtDecompressed";
            this.txtDecompressed.Size = new System.Drawing.Size(193, 50);
            this.txtDecompressed.TabIndex = 18;
            this.txtDecompressed.Text = "";
            // 
            // txtCompressed
            // 
            this.txtCompressed.Location = new System.Drawing.Point(619, 41);
            this.txtCompressed.Name = "txtCompressed";
            this.txtCompressed.Size = new System.Drawing.Size(193, 54);
            this.txtCompressed.TabIndex = 17;
            this.txtCompressed.Text = "";
            // 
            // txtCypherText
            // 
            this.txtCypherText.Location = new System.Drawing.Point(321, 41);
            this.txtCypherText.Name = "txtCypherText";
            this.txtCypherText.Size = new System.Drawing.Size(193, 54);
            this.txtCypherText.TabIndex = 16;
            this.txtCypherText.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(616, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Compressed Text";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(318, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Cipher Text";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Plain Text";
            // 
            // btnTxtDecrypt
            // 
            this.btnTxtDecrypt.Enabled = false;
            this.btnTxtDecrypt.Location = new System.Drawing.Point(233, 114);
            this.btnTxtDecrypt.Name = "btnTxtDecrypt";
            this.btnTxtDecrypt.Size = new System.Drawing.Size(73, 27);
            this.btnTxtDecrypt.TabIndex = 11;
            this.btnTxtDecrypt.Text = "<- Decrypt";
            this.btnTxtDecrypt.UseVisualStyleBackColor = true;
            this.btnTxtDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnTxtDecompress
            // 
            this.btnTxtDecompress.Enabled = false;
            this.btnTxtDecompress.Location = new System.Drawing.Point(531, 114);
            this.btnTxtDecompress.Name = "btnTxtDecompress";
            this.btnTxtDecompress.Size = new System.Drawing.Size(89, 27);
            this.btnTxtDecompress.TabIndex = 10;
            this.btnTxtDecompress.Text = "<- Decompress";
            this.btnTxtDecompress.UseVisualStyleBackColor = true;
            this.btnTxtDecompress.Click += new System.EventHandler(this.btnTxtDecompress_Click);
            // 
            // btnTxtCompress
            // 
            this.btnTxtCompress.Enabled = false;
            this.btnTxtCompress.Location = new System.Drawing.Point(531, 55);
            this.btnTxtCompress.Name = "btnTxtCompress";
            this.btnTxtCompress.Size = new System.Drawing.Size(73, 27);
            this.btnTxtCompress.TabIndex = 9;
            this.btnTxtCompress.Text = "Compress ->";
            this.btnTxtCompress.UseVisualStyleBackColor = true;
            this.btnTxtCompress.Click += new System.EventHandler(this.btnTxtCompress_Click);
            // 
            // btnTxtEncrypt
            // 
            this.btnTxtEncrypt.Enabled = false;
            this.btnTxtEncrypt.Location = new System.Drawing.Point(233, 55);
            this.btnTxtEncrypt.Name = "btnTxtEncrypt";
            this.btnTxtEncrypt.Size = new System.Drawing.Size(73, 27);
            this.btnTxtEncrypt.TabIndex = 8;
            this.btnTxtEncrypt.Text = "Encrypt ->";
            this.btnTxtEncrypt.UseVisualStyleBackColor = true;
            this.btnTxtEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // groupEnc
            // 
            this.groupEnc.Controls.Add(this.lblTotalTime);
            this.groupEnc.Controls.Add(this.label5);
            this.groupEnc.Controls.Add(this.lblDecryptTime);
            this.groupEnc.Controls.Add(this.lblDecompressTime);
            this.groupEnc.Controls.Add(this.lblCompressTime);
            this.groupEnc.Controls.Add(this.lblEncryptTime);
            this.groupEnc.Controls.Add(this.lblDecrypted);
            this.groupEnc.Controls.Add(this.lblDecompressed);
            this.groupEnc.Controls.Add(this.lblCompressed);
            this.groupEnc.Controls.Add(this.lblCipherText);
            this.groupEnc.Controls.Add(this.lblPlainText);
            this.groupEnc.Controls.Add(this.txtDecrypted);
            this.groupEnc.Controls.Add(this.txtDecompressed);
            this.groupEnc.Controls.Add(this.txtCompressed);
            this.groupEnc.Controls.Add(this.txtCypherText);
            this.groupEnc.Controls.Add(this.txtPlainText);
            this.groupEnc.Controls.Add(this.label7);
            this.groupEnc.Controls.Add(this.label8);
            this.groupEnc.Controls.Add(this.label9);
            this.groupEnc.Controls.Add(this.btnTxtDecrypt);
            this.groupEnc.Controls.Add(this.btnTxtDecompress);
            this.groupEnc.Controls.Add(this.btnTxtCompress);
            this.groupEnc.Controls.Add(this.btnTxtEncrypt);
            this.groupEnc.Location = new System.Drawing.Point(12, 581);
            this.groupEnc.Name = "groupEnc";
            this.groupEnc.Size = new System.Drawing.Size(822, 162);
            this.groupEnc.TabIndex = 19;
            this.groupEnc.TabStop = false;
            this.groupEnc.Text = "Encryption/Decryption";
            // 
            // txtPlainText
            // 
            this.txtPlainText.Location = new System.Drawing.Point(24, 41);
            this.txtPlainText.Name = "txtPlainText";
            this.txtPlainText.Size = new System.Drawing.Size(193, 54);
            this.txtPlainText.TabIndex = 15;
            this.txtPlainText.Text = "";
            // 
            // picLL
            // 
            this.picLL.Location = new System.Drawing.Point(543, 27);
            this.picLL.Name = "picLL";
            this.picLL.Size = new System.Drawing.Size(301, 257);
            this.picLL.TabIndex = 23;
            this.picLL.TabStop = false;
            // 
            // picHL
            // 
            this.picHL.Location = new System.Drawing.Point(543, 282);
            this.picHL.Name = "picHL";
            this.picHL.Size = new System.Drawing.Size(301, 257);
            this.picHL.TabIndex = 24;
            this.picHL.TabStop = false;
            // 
            // picHH
            // 
            this.picHH.Location = new System.Drawing.Point(850, 282);
            this.picHH.Name = "picHH";
            this.picHH.Size = new System.Drawing.Size(301, 257);
            this.picHH.TabIndex = 27;
            this.picHH.TabStop = false;
            // 
            // picLH
            // 
            this.picLH.Location = new System.Drawing.Point(850, 29);
            this.picLH.Name = "picLH";
            this.picLH.Size = new System.Drawing.Size(301, 257);
            this.picLH.TabIndex = 26;
            this.picLH.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 743);
            this.Controls.Add(this.picHH);
            this.Controls.Add(this.picLH);
            this.Controls.Add(this.picHL);
            this.Controls.Add(this.picLL);
            this.Controls.Add(this.btnKeyGen);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupEnc);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.txtEntropy);
            this.Controls.Add(this.cmbEntropy);
            this.Controls.Add(this.btnInv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbBright);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnImgCompress);
            this.Controls.Add(this.cmbDbNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.picResult);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupEnc.ResumeLayout(false);
            this.groupEnc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox picResult;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDbNum;
        private System.Windows.Forms.Button btnImgCompress;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBright;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Button btnInv;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbEntropy;
        private System.Windows.Forms.TextBox txtEntropy;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnKeyGen;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDecryptTime;
        private System.Windows.Forms.Label lblDecompressTime;
        private System.Windows.Forms.Label lblCompressTime;
        private System.Windows.Forms.Label lblEncryptTime;
        private System.Windows.Forms.Label lblDecrypted;
        private System.Windows.Forms.Label lblDecompressed;
        private System.Windows.Forms.Label lblCompressed;
        private System.Windows.Forms.Label lblCipherText;
        private System.Windows.Forms.Label lblPlainText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txtDecrypted;
        private System.Windows.Forms.RichTextBox txtDecompressed;
        private System.Windows.Forms.RichTextBox txtCompressed;
        private System.Windows.Forms.RichTextBox txtCypherText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTxtDecrypt;
        private System.Windows.Forms.Button btnTxtDecompress;
        private System.Windows.Forms.Button btnTxtCompress;
        private System.Windows.Forms.Button btnTxtEncrypt;
        private System.Windows.Forms.GroupBox groupEnc;
        private System.Windows.Forms.RichTextBox txtPlainText;
        private System.Windows.Forms.PictureBox picLL;
        private System.Windows.Forms.PictureBox picHL;
        private System.Windows.Forms.PictureBox picHH;
        private System.Windows.Forms.PictureBox picLH;
        private System.Windows.Forms.ToolStripMenuItem stegoHidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stegoExtractToolStripMenuItem;
    }
}

