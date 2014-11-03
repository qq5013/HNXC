﻿namespace THOK.XC.Dispatching.OperateView
{
    partial class StockOutWorkQueryDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblBillNo = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndBillDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartBillDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBillType = new System.Windows.Forms.Label();
            this.cbBillType = new System.Windows.Forms.ComboBox();
            this.txtCigaretteCode = new System.Windows.Forms.TextBox();
            this.txtFormulaCode = new System.Windows.Forms.TextBox();
            this.btnFormulaCode = new System.Windows.Forms.Button();
            this.btnCigaretteCode = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSCHEDULE_NO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(246, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 35);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(165, 236);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 35);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblBillNo
            // 
            this.lblBillNo.AutoSize = true;
            this.lblBillNo.Location = new System.Drawing.Point(20, 172);
            this.lblBillNo.Name = "lblBillNo";
            this.lblBillNo.Size = new System.Drawing.Size(53, 12);
            this.lblBillNo.TabIndex = 20;
            this.lblBillNo.Text = "单据编号";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(81, 168);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(240, 21);
            this.txtBillNo.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "~";
            // 
            // dtpEndBillDate
            // 
            this.dtpEndBillDate.Location = new System.Drawing.Point(212, 11);
            this.dtpEndBillDate.Name = "dtpEndBillDate";
            this.dtpEndBillDate.Size = new System.Drawing.Size(109, 21);
            this.dtpEndBillDate.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "作业日期";
            // 
            // dtpStartBillDate
            // 
            this.dtpStartBillDate.Location = new System.Drawing.Point(81, 10);
            this.dtpStartBillDate.Name = "dtpStartBillDate";
            this.dtpStartBillDate.Size = new System.Drawing.Size(109, 21);
            this.dtpStartBillDate.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "配方编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "牌    号";
            // 
            // lblBillType
            // 
            this.lblBillType.AutoSize = true;
            this.lblBillType.Location = new System.Drawing.Point(20, 95);
            this.lblBillType.Name = "lblBillType";
            this.lblBillType.Size = new System.Drawing.Size(53, 12);
            this.lblBillType.TabIndex = 33;
            this.lblBillType.Text = "单据类型";
            // 
            // cbBillType
            // 
            this.cbBillType.DisplayMember = "BTYPE_NAME";
            this.cbBillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBillType.FormattingEnabled = true;
            this.cbBillType.Location = new System.Drawing.Point(81, 90);
            this.cbBillType.Name = "cbBillType";
            this.cbBillType.Size = new System.Drawing.Size(109, 20);
            this.cbBillType.TabIndex = 32;
            this.cbBillType.ValueMember = "BTYPE_CODE";
            // 
            // txtCigaretteCode
            // 
            this.txtCigaretteCode.Location = new System.Drawing.Point(81, 36);
            this.txtCigaretteCode.Name = "txtCigaretteCode";
            this.txtCigaretteCode.Size = new System.Drawing.Size(109, 21);
            this.txtCigaretteCode.TabIndex = 36;
            // 
            // txtFormulaCode
            // 
            this.txtFormulaCode.Location = new System.Drawing.Point(81, 62);
            this.txtFormulaCode.Name = "txtFormulaCode";
            this.txtFormulaCode.Size = new System.Drawing.Size(109, 21);
            this.txtFormulaCode.TabIndex = 37;
            // 
            // btnFormulaCode
            // 
            this.btnFormulaCode.Location = new System.Drawing.Point(189, 61);
            this.btnFormulaCode.Name = "btnFormulaCode";
            this.btnFormulaCode.Size = new System.Drawing.Size(31, 22);
            this.btnFormulaCode.TabIndex = 31;
            this.btnFormulaCode.Text = "...";
            this.btnFormulaCode.UseVisualStyleBackColor = true;
            this.btnFormulaCode.Click += new System.EventHandler(this.btnFormulaCode_Click);
            // 
            // btnCigaretteCode
            // 
            this.btnCigaretteCode.Location = new System.Drawing.Point(189, 35);
            this.btnCigaretteCode.Name = "btnCigaretteCode";
            this.btnCigaretteCode.Size = new System.Drawing.Size(31, 22);
            this.btnCigaretteCode.TabIndex = 27;
            this.btnCigaretteCode.Text = "...";
            this.btnCigaretteCode.UseVisualStyleBackColor = true;
            this.btnCigaretteCode.Click += new System.EventHandler(this.btnCigaretteCode_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 39;
            this.label6.Text = "计划单号";
            // 
            // txtSCHEDULE_NO
            // 
            this.txtSCHEDULE_NO.Location = new System.Drawing.Point(81, 141);
            this.txtSCHEDULE_NO.Name = "txtSCHEDULE_NO";
            this.txtSCHEDULE_NO.Size = new System.Drawing.Size(241, 21);
            this.txtSCHEDULE_NO.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "烟包条码";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(81, 198);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(240, 21);
            this.txtBarCode.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 43;
            this.label7.Text = "任务状态";
            // 
            // cbState
            // 
            this.cbState.DisplayMember = "BTYPE_NAME";
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.FormattingEnabled = true;
            this.cbState.Items.AddRange(new object[] {
            "未执行",
            "执行中",
            "完成",
            "取消"});
            this.cbState.Location = new System.Drawing.Point(81, 115);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(109, 20);
            this.cbState.TabIndex = 42;
            this.cbState.ValueMember = "BTYPE_CODE";
            // 
            // StockOutWorkQueryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 283);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSCHEDULE_NO);
            this.Controls.Add(this.txtFormulaCode);
            this.Controls.Add(this.txtCigaretteCode);
            this.Controls.Add(this.lblBillType);
            this.Controls.Add(this.cbBillType);
            this.Controls.Add(this.btnFormulaCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCigaretteCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndBillDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStartBillDate);
            this.Controls.Add(this.lblBillNo);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockOutWorkQueryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出库查询--条件";
            this.Load += new System.EventHandler(this.StockInWorkQueryDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblBillNo;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndBillDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartBillDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCigaretteCode;
        private System.Windows.Forms.Button btnFormulaCode;
        private System.Windows.Forms.Label lblBillType;
        private System.Windows.Forms.ComboBox cbBillType;
        private System.Windows.Forms.TextBox txtCigaretteCode;
        private System.Windows.Forms.TextBox txtFormulaCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSCHEDULE_NO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbState;

        
    }
}