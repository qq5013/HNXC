﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.OperateView
{
    public partial class StockInWorkQueryDialog : Form
    {
        public string strWhere;
        public StockInWorkQueryDialog()
        {
            InitializeComponent();
        }

        private DataTable dtFormula;
        private DataTable dtCigarette;
        private DataTable dtBillType;

        private Dictionary<string, string> CigaretteFields = new Dictionary<string, string>();
        private Dictionary<string, string> FormulaFields = new Dictionary<string, string>();


        private void StockInWorkQueryDialog_Load(object sender, EventArgs e)
        {
            this.dtpStartBillDate.Value = DateTime.Now;
            this.dtpEndBillDate.Value = DateTime.Now;

           Process.Dal.BillDal dal=new Process.Dal.BillDal();

            dtBillType=dal.GetBillByType("1");

            cbBillType.DataSource =dtBillType;

            CigaretteFields.Add("CIGARETTE_CODE", "牌号编码");
            CigaretteFields.Add("CIGARETTE_NAME", "牌号名称");
            CigaretteFields.Add("CIGARETTE_MEMO", "牌号描述");

            FormulaFields.Add("FORMULA_CODE", "配方编码");
            FormulaFields.Add("FORMULA_NAME", "配方名称");
            FormulaFields.Add("CIGARETTE_CODE", "牌号编码");
            FormulaFields.Add("CIGARETTE_NAME", "牌号名称");
            FormulaFields.Add("BATCH_WEIGHT", "批次重量");
            FormulaFields.Add("USE_COUNT", "使用次数");

            dtCigarette = dal.GetCigarette();
            dtFormula = dal.GetFormula();
        }

        private void btnCigaretteCode_Click(object sender, EventArgs e)
        {
            SelectDialog selectDialog = new SelectDialog(dtCigarette, CigaretteFields, false);
            if (selectDialog.ShowDialog() == DialogResult.OK)
            {
                txtCigaretteCode.Text = selectDialog["CIGARETTE_NAME"];
                txtCigaretteCode.Tag = selectDialog["CIGARETTE_CODE"];
            }
        }

        private void btnFormulaCode_Click(object sender, EventArgs e)
        {
            DataTable dt = dtFormula;
            if (txtCigaretteCode.Text.Trim()!= "")
            {
                DataView dv = dtFormula.DefaultView;
                dv.RowFilter = string.Format("CIGARETTE_CODE='{0}'", txtCigaretteCode.Tag);
                dt = dv.ToTable(true, new string[] { "FORMULA_CODE", "FORMULA_NAME", "CIGARETTE_CODE", "CIGARETTE_NAME", "BATCH_WEIGHT", "USE_COUNT" });
            }
            
            SelectDialog selectDialog = new SelectDialog(dt, FormulaFields, false);
            if (selectDialog.ShowDialog() == DialogResult.OK)
            {
                txtFormulaCode.Text = selectDialog["FORMULA_NAME"];
                 txtFormulaCode.Tag = selectDialog["FORMULA_CODE"];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strWhere = "";
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            strWhere="1=1";
            if (this.txtBillNo.Text.Trim() != "")
            {
                strWhere += string.Format(" AND A.BIllNO like '%{0}%'", this.txtBillNo.Text.Trim());
            }

            //strWhere += string.Format(" AND A.TASK_DATE BETWEEN TO_DATE('{0}','yyyy/mm/dd') AND TO_DATE('{1}','yyyy/mm/dd')", this.dtpStartBillDate.Value.ToString("yyyy/MM/dd"), this.dtpEndBillDate.Value.ToString("yyyy/MM/dd"));
            strWhere += string.Format(" AND A.TASK_DATE BETWEEN TO_DATE('{0}','yyyy/mm/dd hh24:mi:ss') AND TO_DATE('{1} 23:59:59','yyyy/mm/dd hh24:mi:ss')", this.dtpStartBillDate.Value.ToString("yyyy/MM/dd"), this.dtpEndBillDate.Value.ToString("yyyy/MM/dd"));

            strWhere += string.Format(" AND A.BTYPE_CODE='{0}'", cbBillType.SelectedValue);
            if(this.cbState.Text.Trim()!="")
                strWhere += string.Format(" AND B.STATE='{0}'", this.cbState.SelectedIndex);
            if (cbBillMethod.Text.Trim() != "")
                strWhere += string.Format(" AND A.BILL_METHOD='{0}'", cbBillMethod.SelectedIndex);

            if (this.txtBarCode.Text.Trim() != "")
            {
                strWhere += string.Format(" AND B.PRODUCT_BARCODE LIKE '{0}'", this.txtBarCode.Text.Trim());
            }

            if (this.txtCigaretteCode.Text != "")
            {
                strWhere += string.Format(" AND A.CIGARETTE_CODE='{0}'", txtCigaretteCode.Tag);
            }
            if (this.txtFormulaCode.Text != "")
            {
                strWhere += string.Format(" AND A.FORMULA_CODE='{0}'", txtFormulaCode.Tag);
            }
            this.DialogResult = DialogResult.OK;
        }         
    }
}
