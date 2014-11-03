namespace THOK.XC.Dispatching.OperateView
{
    partial class frmCarTaskInfo
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClearTask = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemState0 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemState1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemState2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemState3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemTarget1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemTarget2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssignmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colToStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnClearTask);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Size = new System.Drawing.Size(710, 43);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Location = new System.Drawing.Point(0, 43);
            this.pnlContent.Size = new System.Drawing.Size(710, 373);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(710, 416);
            // 
            // btnClearTask
            // 
            this.btnClearTask.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClearTask.Location = new System.Drawing.Point(9, 7);
            this.btnClearTask.Name = "btnClearTask";
            this.btnClearTask.Size = new System.Drawing.Size(61, 28);
            this.btnClearTask.TabIndex = 57;
            this.btnClearTask.Text = "清除任务";
            this.btnClearTask.UseVisualStyleBackColor = true;
            this.btnClearTask.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(70, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(61, 28);
            this.btnExit.TabIndex = 50;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.Column2,
            this.colAssignmentID,
            this.Column3,
            this.colToStation,
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
            this.dgvMain.Size = new System.Drawing.Size(710, 373);
            this.dgvMain.TabIndex = 13;
            this.dgvMain.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMain_CellMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.ToolStripMenuItem2,
            this.ToolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 70);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.ToolStripMenuItem1.Text = "下发任务给输送机";
            this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemState0,
            this.ToolStripMenuItemState1,
            this.ToolStripMenuItemState2,
            this.ToolStripMenuItemState3});
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(172, 22);
            this.ToolStripMenuItem2.Text = "状态切换";
            // 
            // ToolStripMenuItemState0
            // 
            this.ToolStripMenuItemState0.Name = "ToolStripMenuItemState0";
            this.ToolStripMenuItemState0.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemState0.Text = "等待";
            this.ToolStripMenuItemState0.Click += new System.EventHandler(this.ToolStripMenuItemState0_Click);
            // 
            // ToolStripMenuItemState1
            // 
            this.ToolStripMenuItemState1.Name = "ToolStripMenuItemState1";
            this.ToolStripMenuItemState1.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemState1.Text = "执行";
            this.ToolStripMenuItemState1.Click += new System.EventHandler(this.ToolStripMenuItemState1_Click);
            // 
            // ToolStripMenuItemState2
            // 
            this.ToolStripMenuItemState2.Name = "ToolStripMenuItemState2";
            this.ToolStripMenuItemState2.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemState2.Text = "完成";
            this.ToolStripMenuItemState2.Click += new System.EventHandler(this.ToolStripMenuItemState2_Click);
            // 
            // ToolStripMenuItemState3
            // 
            this.ToolStripMenuItemState3.Name = "ToolStripMenuItemState3";
            this.ToolStripMenuItemState3.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemState3.Text = "取消";
            this.ToolStripMenuItemState3.Click += new System.EventHandler(this.ToolStripMenuItemState3_Click);
            // 
            // ToolStripMenuItem3
            // 
            this.ToolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemTarget1,
            this.ToolStripMenuItemTarget2});
            this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
            this.ToolStripMenuItem3.Size = new System.Drawing.Size(172, 22);
            this.ToolStripMenuItem3.Text = "出口切换";
            // 
            // ToolStripMenuItemTarget1
            // 
            this.ToolStripMenuItemTarget1.Name = "ToolStripMenuItemTarget1";
            this.ToolStripMenuItemTarget1.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItemTarget1.Text = "340(默认B、C线)";
            this.ToolStripMenuItemTarget1.Click += new System.EventHandler(this.ToolStripMenuItemTarget1_Click);
            // 
            // ToolStripMenuItemTarget2
            // 
            this.ToolStripMenuItemTarget2.Name = "ToolStripMenuItemTarget2";
            this.ToolStripMenuItemTarget2.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItemTarget2.Text = "360(默认A线)";
            this.ToolStripMenuItemTarget2.Click += new System.EventHandler(this.ToolStripMenuItemTarget2_Click);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "CAR_NO";
            this.Column2.HeaderText = "小车编号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // colAssignmentID
            // 
            this.colAssignmentID.DataPropertyName = "TASK_NO";
            this.colAssignmentID.HeaderText = "任务号";
            this.colAssignmentID.Name = "colAssignmentID";
            this.colAssignmentID.ReadOnly = true;
            this.colAssignmentID.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "TASK_TYPE";
            this.Column3.HeaderText = "货物类型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // colToStation
            // 
            this.colToStation.DataPropertyName = "TO_STATION";
            this.colToStation.HeaderText = "目的地址";
            this.colToStation.Name = "colToStation";
            this.colToStation.ReadOnly = true;
            this.colToStation.Width = 80;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TO_STATION_NAME";
            this.Column1.HeaderText = "目的站台";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // frmCarTaskInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 416);
            this.Name = "frmCarTaskInfo";
            this.Text = "穿梭车操作";
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnClearTask;
        protected System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemState0;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemState1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemState2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemState3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemTarget1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemTarget2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssignmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colToStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}