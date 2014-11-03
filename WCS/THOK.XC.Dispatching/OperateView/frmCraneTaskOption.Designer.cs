﻿namespace THOK.XC.Dispatching.OperateView
{
    partial class frmCraneTaskOption
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCrane = new System.Windows.Forms.ComboBox();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.colAssignmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFromStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colToStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBILL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBTYPE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCIGARETTE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFORMULA_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFORMULA_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBATCH_WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPRODUCT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPRODUCT_BARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGRADE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colORIGINAL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYEARS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSTYLE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOption = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCRQ = new System.Windows.Forms.Button();
            this.btnSYN = new System.Windows.Forms.Button();
            this.btnDER = new System.Windows.Forms.Button();
            this.btnReassign = new System.Windows.Forms.Button();
            this.btnARQ = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.comboBox1);
            this.pnlTool.Controls.Add(this.btnARQ);
            this.pnlTool.Controls.Add(this.btnReassign);
            this.pnlTool.Controls.Add(this.btnDER);
            this.pnlTool.Controls.Add(this.btnSYN);
            this.pnlTool.Controls.Add(this.btnCRQ);
            this.pnlTool.Controls.Add(this.btnOption);
            this.pnlTool.Controls.Add(this.cmbCrane);
            this.pnlTool.Controls.Add(this.label1);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(800, 41);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Location = new System.Drawing.Point(0, 41);
            this.pnlContent.Size = new System.Drawing.Size(800, 279);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(800, 320);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "堆垛机编号";
            // 
            // cmbCrane
            // 
            this.cmbCrane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCrane.FormattingEnabled = true;
            this.cmbCrane.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06"});
            this.cmbCrane.Location = new System.Drawing.Point(74, 8);
            this.cmbCrane.Name = "cmbCrane";
            this.cmbCrane.Size = new System.Drawing.Size(66, 20);
            this.cmbCrane.TabIndex = 41;
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAssignmentID,
            this.colFromStation,
            this.colToStation,
            this.colBILL_NO,
            this.colBTYPE_NAME,
            this.colCIGARETTE_NAME,
            this.colFORMULA_CODE,
            this.colFORMULA_NAME,
            this.colBATCH_WEIGHT,
            this.PRODUCT_CODE,
            this.colPRODUCT_NAME,
            this.colPRODUCT_BARCODE,
            this.colGRADE_NAME,
            this.colORIGINAL_NAME,
            this.colYEARS,
            this.colSTYLE_NAME,
            this.colErrDesc,
            this.colTaskID,
            this.Column1});
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.RowHeadersWidth = 30;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(800, 279);
            this.dgvMain.TabIndex = 12;
            // 
            // colAssignmentID
            // 
            this.colAssignmentID.DataPropertyName = "ASSIGNMENT_ID";
            this.colAssignmentID.HeaderText = "堆垛机任务号";
            this.colAssignmentID.Name = "colAssignmentID";
            this.colAssignmentID.ReadOnly = true;
            // 
            // colFromStation
            // 
            this.colFromStation.DataPropertyName = "FROM_STATION";
            this.colFromStation.HeaderText = "起始位置";
            this.colFromStation.Name = "colFromStation";
            this.colFromStation.ReadOnly = true;
            // 
            // colToStation
            // 
            this.colToStation.DataPropertyName = "TO_STATION";
            this.colToStation.HeaderText = "目的地址";
            this.colToStation.Name = "colToStation";
            this.colToStation.ReadOnly = true;
            // 
            // colBILL_NO
            // 
            this.colBILL_NO.DataPropertyName = "BILL_NO";
            this.colBILL_NO.HeaderText = "单号";
            this.colBILL_NO.Name = "colBILL_NO";
            this.colBILL_NO.ReadOnly = true;
            // 
            // colBTYPE_NAME
            // 
            this.colBTYPE_NAME.DataPropertyName = "BTYPE_NAME";
            this.colBTYPE_NAME.HeaderText = "单据类型";
            this.colBTYPE_NAME.Name = "colBTYPE_NAME";
            this.colBTYPE_NAME.ReadOnly = true;
            // 
            // colCIGARETTE_NAME
            // 
            this.colCIGARETTE_NAME.DataPropertyName = "CIGARETTE_NAME";
            this.colCIGARETTE_NAME.HeaderText = "牌号";
            this.colCIGARETTE_NAME.Name = "colCIGARETTE_NAME";
            this.colCIGARETTE_NAME.ReadOnly = true;
            // 
            // colFORMULA_CODE
            // 
            this.colFORMULA_CODE.DataPropertyName = "FORMULA_CODE";
            this.colFORMULA_CODE.HeaderText = "配方编码";
            this.colFORMULA_CODE.Name = "colFORMULA_CODE";
            this.colFORMULA_CODE.ReadOnly = true;
            // 
            // colFORMULA_NAME
            // 
            this.colFORMULA_NAME.DataPropertyName = "FORMULA_NAME";
            this.colFORMULA_NAME.HeaderText = "配方名称";
            this.colFORMULA_NAME.Name = "colFORMULA_NAME";
            this.colFORMULA_NAME.ReadOnly = true;
            // 
            // colBATCH_WEIGHT
            // 
            this.colBATCH_WEIGHT.DataPropertyName = "BATCH_WEIGHT";
            this.colBATCH_WEIGHT.HeaderText = "配方重量";
            this.colBATCH_WEIGHT.Name = "colBATCH_WEIGHT";
            this.colBATCH_WEIGHT.ReadOnly = true;
            // 
            // PRODUCT_CODE
            // 
            this.PRODUCT_CODE.DataPropertyName = "PRODUCT_CODE";
            this.PRODUCT_CODE.HeaderText = "产品编码";
            this.PRODUCT_CODE.Name = "PRODUCT_CODE";
            this.PRODUCT_CODE.ReadOnly = true;
            // 
            // colPRODUCT_NAME
            // 
            this.colPRODUCT_NAME.DataPropertyName = "PRODUCT_NAME";
            this.colPRODUCT_NAME.HeaderText = "产品名称";
            this.colPRODUCT_NAME.Name = "colPRODUCT_NAME";
            this.colPRODUCT_NAME.ReadOnly = true;
            // 
            // colPRODUCT_BARCODE
            // 
            this.colPRODUCT_BARCODE.DataPropertyName = "PRODUCT_BARCODE";
            this.colPRODUCT_BARCODE.HeaderText = "产品条码";
            this.colPRODUCT_BARCODE.Name = "colPRODUCT_BARCODE";
            this.colPRODUCT_BARCODE.ReadOnly = true;
            // 
            // colGRADE_NAME
            // 
            this.colGRADE_NAME.DataPropertyName = "GRADE_NAME";
            this.colGRADE_NAME.HeaderText = "产品等级";
            this.colGRADE_NAME.Name = "colGRADE_NAME";
            this.colGRADE_NAME.ReadOnly = true;
            // 
            // colORIGINAL_NAME
            // 
            this.colORIGINAL_NAME.HeaderText = "原产地";
            this.colORIGINAL_NAME.Name = "colORIGINAL_NAME";
            this.colORIGINAL_NAME.ReadOnly = true;
            // 
            // colYEARS
            // 
            this.colYEARS.DataPropertyName = "YEARS";
            this.colYEARS.HeaderText = "产品年份";
            this.colYEARS.Name = "colYEARS";
            this.colYEARS.ReadOnly = true;
            // 
            // colSTYLE_NAME
            // 
            this.colSTYLE_NAME.DataPropertyName = "STYLE_NAME";
            this.colSTYLE_NAME.HeaderText = "产品形态";
            this.colSTYLE_NAME.Name = "colSTYLE_NAME";
            this.colSTYLE_NAME.ReadOnly = true;
            // 
            // colErrDesc
            // 
            this.colErrDesc.DataPropertyName = "DESCRIPTION";
            this.colErrDesc.HeaderText = "错误描述";
            this.colErrDesc.Name = "colErrDesc";
            this.colErrDesc.ReadOnly = true;
            // 
            // colTaskID
            // 
            this.colTaskID.DataPropertyName = "TASK_ID";
            this.colTaskID.HeaderText = "作业ID";
            this.colTaskID.Name = "colTaskID";
            this.colTaskID.ReadOnly = true;
            this.colTaskID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CRANE_NO";
            this.Column1.HeaderText = "堆垛机编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // btnOption
            // 
            this.btnOption.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOption.Location = new System.Drawing.Point(751, 4);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(48, 28);
            this.btnOption.TabIndex = 42;
            this.btnOption.Text = "操作";
            this.btnOption.UseVisualStyleBackColor = true;
            this.btnOption.Visible = false;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(644, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(61, 28);
            this.btnExit.TabIndex = 38;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(146, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(61, 28);
            this.btnRefresh.TabIndex = 37;
            this.btnRefresh.Text = "查询";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCRQ
            // 
            this.btnCRQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCRQ.Location = new System.Drawing.Point(582, 4);
            this.btnCRQ.Name = "btnCRQ";
            this.btnCRQ.Size = new System.Drawing.Size(61, 28);
            this.btnCRQ.TabIndex = 43;
            this.btnCRQ.Text = "发送CRQ";
            this.btnCRQ.UseVisualStyleBackColor = true;
            this.btnCRQ.Click += new System.EventHandler(this.btnCRQ_Click);
            // 
            // btnSYN
            // 
            this.btnSYN.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSYN.Location = new System.Drawing.Point(707, 4);
            this.btnSYN.Name = "btnSYN";
            this.btnSYN.Size = new System.Drawing.Size(61, 28);
            this.btnSYN.TabIndex = 44;
            this.btnSYN.Text = "发送SYN";
            this.btnSYN.UseVisualStyleBackColor = true;
            this.btnSYN.Visible = false;
            this.btnSYN.Click += new System.EventHandler(this.btnSYN_Click);
            // 
            // btnDER
            // 
            this.btnDER.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDER.Location = new System.Drawing.Point(209, 4);
            this.btnDER.Name = "btnDER";
            this.btnDER.Size = new System.Drawing.Size(99, 28);
            this.btnDER.TabIndex = 45;
            this.btnDER.Text = "删除堆垛机任务";
            this.btnDER.UseVisualStyleBackColor = true;
            this.btnDER.Click += new System.EventHandler(this.btnDER_Click);
            // 
            // btnReassign
            // 
            this.btnReassign.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReassign.Location = new System.Drawing.Point(310, 4);
            this.btnReassign.Name = "btnReassign";
            this.btnReassign.Size = new System.Drawing.Size(88, 28);
            this.btnReassign.TabIndex = 46;
            this.btnReassign.Text = "重新分配货位";
            this.btnReassign.UseVisualStyleBackColor = true;
            this.btnReassign.Click += new System.EventHandler(this.btnReassign_Click);
            // 
            // btnARQ
            // 
            this.btnARQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnARQ.Location = new System.Drawing.Point(464, 4);
            this.btnARQ.Name = "btnARQ";
            this.btnARQ.Size = new System.Drawing.Size(117, 28);
            this.btnARQ.TabIndex = 47;
            this.btnARQ.Text = "下达任务给堆垛机";
            this.btnARQ.UseVisualStyleBackColor = true;
            this.btnARQ.Click += new System.EventHandler(this.btnARQ_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "放货",
            "取货"});
            this.comboBox1.Location = new System.Drawing.Point(402, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(54, 20);
            this.comboBox1.TabIndex = 48;
            // 
            // frmCraneTaskOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 320);
            this.Name = "frmCraneTaskOption";
            this.Text = "堆垛机任务操作";
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Button btnRefresh;
        protected System.Windows.Forms.Button btnOption;
        private System.Windows.Forms.ComboBox cmbCrane;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMain;
        protected System.Windows.Forms.Button btnCRQ;
        protected System.Windows.Forms.Button btnSYN;
        protected System.Windows.Forms.Button btnReassign;
        protected System.Windows.Forms.Button btnDER;
        protected System.Windows.Forms.Button btnARQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssignmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFromStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBILL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBTYPE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCIGARETTE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFORMULA_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFORMULA_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBATCH_WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPRODUCT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPRODUCT_BARCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGRADE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colORIGINAL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYEARS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTYLE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}