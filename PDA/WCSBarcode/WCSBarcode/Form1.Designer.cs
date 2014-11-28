namespace WCSBarcode
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnExit = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtProductNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFormulaNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCigarette = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutBillNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtScheduleNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.barcode21 = new Symbol.Barcode2.Design.Barcode2();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtInBillNo = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(174, 242);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 26);
            this.btnExit.TabIndex = 45;
            this.btnExit.Text = "退   出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtYear
            // 
            this.txtYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYear.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtYear.Location = new System.Drawing.Point(82, 168);
            this.txtYear.Name = "txtYear";
            this.txtYear.ReadOnly = true;
            this.txtYear.Size = new System.Drawing.Size(207, 23);
            this.txtYear.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 18);
            this.label9.Text = "年      份：";
            // 
            // txtGrade
            // 
            this.txtGrade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGrade.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtGrade.Location = new System.Drawing.Point(82, 115);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.ReadOnly = true;
            this.txtGrade.Size = new System.Drawing.Size(207, 23);
            this.txtGrade.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.Text = "等       级：";
            // 
            // txtOriginal
            // 
            this.txtOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOriginal.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtOriginal.Location = new System.Drawing.Point(82, 88);
            this.txtOriginal.Name = "txtOriginal";
            this.txtOriginal.ReadOnly = true;
            this.txtOriginal.Size = new System.Drawing.Size(207, 23);
            this.txtOriginal.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(2, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.Text = "产       地：";
            // 
            // txtProductNo
            // 
            this.txtProductNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProductNo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtProductNo.Location = new System.Drawing.Point(82, 62);
            this.txtProductNo.Name = "txtProductNo";
            this.txtProductNo.ReadOnly = true;
            this.txtProductNo.Size = new System.Drawing.Size(207, 23);
            this.txtProductNo.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(2, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.Text = "产品编号：";
            // 
            // txtFormulaNo
            // 
            this.txtFormulaNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFormulaNo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtFormulaNo.Location = new System.Drawing.Point(82, 37);
            this.txtFormulaNo.Name = "txtFormulaNo";
            this.txtFormulaNo.ReadOnly = true;
            this.txtFormulaNo.Size = new System.Drawing.Size(207, 23);
            this.txtFormulaNo.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.Text = "配方编号：";
            // 
            // txtCigarette
            // 
            this.txtCigarette.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCigarette.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtCigarette.Location = new System.Drawing.Point(82, 12);
            this.txtCigarette.Name = "txtCigarette";
            this.txtCigarette.ReadOnly = true;
            this.txtCigarette.Size = new System.Drawing.Size(207, 23);
            this.txtCigarette.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(2, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.Text = "牌       号：";
            // 
            // txtOutBillNo
            // 
            this.txtOutBillNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutBillNo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtOutBillNo.Location = new System.Drawing.Point(83, 121);
            this.txtOutBillNo.Name = "txtOutBillNo";
            this.txtOutBillNo.ReadOnly = true;
            this.txtOutBillNo.Size = new System.Drawing.Size(205, 23);
            this.txtOutBillNo.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.Text = "出库单号：";
            // 
            // txtScheduleNo
            // 
            this.txtScheduleNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScheduleNo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtScheduleNo.Location = new System.Drawing.Point(83, 96);
            this.txtScheduleNo.Name = "txtScheduleNo";
            this.txtScheduleNo.ReadOnly = true;
            this.txtScheduleNo.Size = new System.Drawing.Size(205, 23);
            this.txtScheduleNo.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.Text = "计划单号：";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarCode.Location = new System.Drawing.Point(83, 6);
            this.txtBarCode.Multiline = true;
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(205, 88);
            this.txtBarCode.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.Text = "条码编号：";
            // 
            // barcode21
            // 
            this.barcode21.Config.DecoderParameters.CODABAR = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.CODABARParams.ClsiEditing = false;
            this.barcode21.Config.DecoderParameters.CODABARParams.NotisEditing = false;
            this.barcode21.Config.DecoderParameters.CODABARParams.Redundancy = true;
            this.barcode21.Config.DecoderParameters.CODE128 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.CODE128Params.EAN128 = true;
            this.barcode21.Config.DecoderParameters.CODE128Params.ISBT128 = true;
            this.barcode21.Config.DecoderParameters.CODE128Params.Other128 = true;
            this.barcode21.Config.DecoderParameters.CODE128Params.Redundancy = false;
            this.barcode21.Config.DecoderParameters.CODE39 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.CODE39Params.Code32Prefix = false;
            this.barcode21.Config.DecoderParameters.CODE39Params.Concatenation = false;
            this.barcode21.Config.DecoderParameters.CODE39Params.ConvertToCode32 = false;
            this.barcode21.Config.DecoderParameters.CODE39Params.FullAscii = false;
            this.barcode21.Config.DecoderParameters.CODE39Params.Redundancy = false;
            this.barcode21.Config.DecoderParameters.CODE39Params.ReportCheckDigit = false;
            this.barcode21.Config.DecoderParameters.CODE39Params.VerifyCheckDigit = false;
            this.barcode21.Config.DecoderParameters.CODE93 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.CODE93Params.Redundancy = false;
            this.barcode21.Config.DecoderParameters.D2OF5 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.D2OF5Params.Redundancy = true;
            this.barcode21.Config.DecoderParameters.EAN13 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.EAN8 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.EAN8Params.ConvertToEAN13 = false;
            this.barcode21.Config.DecoderParameters.I2OF5 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.I2OF5Params.ConvertToEAN13 = false;
            this.barcode21.Config.DecoderParameters.I2OF5Params.Redundancy = true;
            this.barcode21.Config.DecoderParameters.I2OF5Params.ReportCheckDigit = false;
            this.barcode21.Config.DecoderParameters.I2OF5Params.VerifyCheckDigit = Symbol.Barcode2.Design.I2OF5.CheckDigitSchemes.Default;
            this.barcode21.Config.DecoderParameters.KOREAN_3OF5 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.KOREAN_3OF5Params.Redundancy = true;
            this.barcode21.Config.DecoderParameters.MSI = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.MSIParams.CheckDigitCount = Symbol.Barcode2.Design.CheckDigitCounts.Default;
            this.barcode21.Config.DecoderParameters.MSIParams.CheckDigitScheme = Symbol.Barcode2.Design.CheckDigitSchemes.Default;
            this.barcode21.Config.DecoderParameters.MSIParams.Redundancy = true;
            this.barcode21.Config.DecoderParameters.MSIParams.ReportCheckDigit = false;
            this.barcode21.Config.DecoderParameters.UPCA = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.UPCAParams.Preamble = Symbol.Barcode2.Design.Preambles.Default;
            this.barcode21.Config.DecoderParameters.UPCAParams.ReportCheckDigit = true;
            this.barcode21.Config.DecoderParameters.UPCE0 = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.DecoderParameters.UPCE0Params.ConvertToUPCA = false;
            this.barcode21.Config.DecoderParameters.UPCE0Params.Preamble = Symbol.Barcode2.Design.Preambles.Default;
            this.barcode21.Config.DecoderParameters.UPCE0Params.ReportCheckDigit = false;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.AimDuration = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.AimMode = Symbol.Barcode2.Design.AIM_MODE.AIM_MODE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.AimType = Symbol.Barcode2.Design.AIM_TYPE.AIM_TYPE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.BeamTimer = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.DPMMode = Symbol.Barcode2.Design.DPM_MODE.DPM_MODE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.FocusMode = Symbol.Barcode2.Design.FOCUS_MODE.FOCUS_MODE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.FocusPosition = Symbol.Barcode2.Design.FOCUS_POSITION.FOCUS_POSITION_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.IlluminationMode = Symbol.Barcode2.Design.ILLUMINATION_MODE.ILLUMINATION_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.ImageCaptureTimeout = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.ImageCompressionTimeout = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.Inverse1DMode = Symbol.Barcode2.Design.INVERSE1D_MODE.INVERSE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.LinearSecurityLevel = Symbol.Barcode2.Design.LINEAR_SECURITY_LEVEL.SECURITY_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.PicklistMode = Symbol.Barcode2.Design.PICKLIST_MODE.PICKLIST_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.PointerTimer = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.PoorQuality1DMode = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.VFFeedback = Symbol.Barcode2.Design.VIEWFINDER_FEEDBACK.VIEWFINDER_FEEDBACK_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.VFFeedbackTime = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.VFMode = Symbol.Barcode2.Design.VIEWFINDER_MODE.VIEWFINDER_MODE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Bottom = 0;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Left = 0;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Right = 0;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Top = 0;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.AimDuration = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.AimMode = Symbol.Barcode2.Design.AIM_MODE.AIM_MODE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.AimType = Symbol.Barcode2.Design.AIM_TYPE.AIM_TYPE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.BeamTimer = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.BeamWidth = Symbol.Barcode2.Design.BEAM_WIDTH.DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.BidirRedundancy = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.ControlScanLed = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.DBPMode = Symbol.Barcode2.Design.DBP_MODE.DBP_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.KlasseEinsEnable = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.LinearSecurityLevel = Symbol.Barcode2.Design.LINEAR_SECURITY_LEVEL.SECURITY_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.PointerTimer = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.RasterHeight = -1;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.RasterMode = Symbol.Barcode2.Design.RASTER_MODE.RASTER_MODE_DEFAULT;
            this.barcode21.Config.ReaderParameters.ReaderSpecific.LaserSpecific.ScanLedLogicLevel = Symbol.Barcode2.Design.DisabledEnabled.Default;
            this.barcode21.Config.ScanParameters.BeepFrequency = 2670;
            this.barcode21.Config.ScanParameters.BeepTime = 200;
            this.barcode21.Config.ScanParameters.CodeIdType = Symbol.Barcode2.Design.CodeIdTypes.Default;
            this.barcode21.Config.ScanParameters.LedTime = 3000;
            this.barcode21.Config.ScanParameters.ScanType = Symbol.Barcode2.Design.SCANTYPES.Default;
            this.barcode21.Config.ScanParameters.WaveFile = "";
            this.barcode21.DeviceType = Symbol.Barcode2.DEVICETYPES.FIRSTAVAILABLE;
            this.barcode21.EnableScanner = true;
            this.barcode21.OnScan += new Symbol.Barcode2.Design.Barcode2.OnScanEventHandler(this.barcode21_OnScan);
            this.barcode21.OnStatus += new Symbol.Barcode2.Design.Barcode2.OnStatusEventHandler(this.barcode21_OnStatus);
            // 
            // txtStyle
            // 
            this.txtStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStyle.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtStyle.Location = new System.Drawing.Point(82, 141);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(207, 23);
            this.txtStyle.TabIndex = 56;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(2, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 18);
            this.label10.Text = "形       态：";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(68, 242);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 26);
            this.btnOK.TabIndex = 67;
            this.btnOK.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtChannel
            // 
            this.txtChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChannel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtChannel.Location = new System.Drawing.Point(83, 171);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.ReadOnly = true;
            this.txtChannel.Size = new System.Drawing.Size(70, 23);
            this.txtChannel.TabIndex = 79;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(5, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 18);
            this.label11.Text = "缓  存 道：";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(302, 226);
            this.tabControl1.TabIndex = 81;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLine);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtInBillNo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtChannel);
            this.tabPage1.Controls.Add(this.txtBarCode);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtOutBillNo);
            this.tabPage1.Controls.Add(this.txtScheduleNo);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(294, 197);
            this.tabPage1.Text = "出库信息";
            // 
            // txtLine
            // 
            this.txtLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLine.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtLine.Location = new System.Drawing.Point(224, 171);
            this.txtLine.Name = "txtLine";
            this.txtLine.ReadOnly = true;
            this.txtLine.Size = new System.Drawing.Size(64, 23);
            this.txtLine.TabIndex = 88;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(157, 174);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 18);
            this.label13.Text = "制丝线：";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.Text = "入库单号：";
            // 
            // txtInBillNo
            // 
            this.txtInBillNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInBillNo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtInBillNo.Location = new System.Drawing.Point(83, 146);
            this.txtInBillNo.Name = "txtInBillNo";
            this.txtInBillNo.ReadOnly = true;
            this.txtInBillNo.Size = new System.Drawing.Size(205, 23);
            this.txtInBillNo.TabIndex = 82;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtCigarette);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtStyle);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.txtFormulaNo);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtYear);
            this.tabPage2.Controls.Add(this.txtProductNo);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtGrade);
            this.tabPage2.Controls.Add(this.txtOriginal);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(294, 197);
            this.tabPage2.Text = "烟包信息";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(315, 273);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "出库烟包信息";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOriginal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtProductNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFormulaNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCigarette;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutBillNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtScheduleNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label1;
        private Symbol.Barcode2.Design.Barcode2 barcode21;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtInBillNo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Label label13;
    }
}

