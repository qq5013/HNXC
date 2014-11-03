using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP;

namespace THOK.XC.Dispatching.WCS
{
    public partial class MainForm : Form
    {
        private Context context = null;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void CreateDirectory(string directoryName)
        {
            if (!System.IO.Directory.Exists(directoryName))
                System.IO.Directory.CreateDirectory(directoryName);
        }

        private void WriteLoggerFile(string text)
        {
            try
            {
                string path = "";
                CreateDirectory("��־");
                path = "��־";
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 4).Trim();
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString("yyyy-MM-dd").Substring(0, 7).Trim();
                path = path.TrimEnd(new char[] { '-'});
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                System.IO.File.AppendAllText(path, string.Format("{0} {1}", DateTime.Now, text + "\r\n"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void Logger_OnLog(THOK.MCP.LogEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new LogEventHandler(Logger_OnLog), args);
            }
            else
            {
                lock (lbLog)
                {                    
                    if (args.Message.IndexOf("Crane BOX��Connected") >= 0)
                    {
                        Control ctlStart = this.buttonArea.Controls.Find("btnStart", true)[0];
                        Control ctlStop = this.buttonArea.Controls.Find("btnStop", true)[0];
                        Control ctlSimulate = this.buttonArea.Controls.Find("btnSimulate", true)[0];
                        if (!ctlStop.Enabled && !ctlSimulate.Enabled)
                            ctlStart.Enabled = true;
                    }
                    
                    if (args.Message.IndexOf("Crane BOX��Disconnected") >= 0)
                    {
                        Control ctlStop = this.buttonArea.Controls.Find("btnStop", true)[0];
                        Control ctlSimulate = this.buttonArea.Controls.Find("btnSimulate", true)[0];
                        if (ctlStop.Enabled)
                        {
                            //if (context.Processes["CraneProcess"] != null)
                            //    context.Processes["CraneProcess"].Suspend();
                            ctlStop.Enabled = false;
                            ctlSimulate.Enabled = true;
                        }
                    }
                    string msg = string.Format("[{0}] {1} {2}", args.LogLevel, DateTime.Now, args.Message);
                    lbLog.Items.Insert(0, msg);
                    WriteLoggerFile(msg);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Release();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
           
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {                

                Logger.OnLog += new LogEventHandler(Logger_OnLog);
                
                FormDialog.OnDialog += new DialogEventHandler(FormDialog_OnDialog);
                context = new Context();

                ContextInitialize initialize = new ContextInitialize();
                context.RegisterProcessControl(buttonArea);
                initialize.InitializeContext(context);
                context.RegisterProcessControl(monitorView);                

                //context.Processes["DynamicShowProcess"].Resume();
            }
            catch (Exception ee)
            {
                Logger.Error("��ʼ������ʧ���������ã�ԭ��" + ee.Message);
            }
        }

        string FormDialog_OnDialog(DialogEventArgs args)
        {
            string strValue = "";
            if (InvokeRequired)
            {
                return (string)this.Invoke(new DialogEventHandler(FormDialog_OnDialog), args);
            }
            else
            {
                //1:���ϣ�2�����,4:����
                if (args.Message[0] == "1"||args.Message[0] == "2"  || args.Message[0] == "4")
                {
                    THOK.XC.Dispatching.View.StockToStation frm = new View.StockToStation(int.Parse(args.Message[0]), args.dtInfo);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        strValue = frm.strValue;
                    }

                }
                else if (args.Message[0] == "6")//�̵�
                {
                    THOK.XC.Dispatching.View.CheckScan frm = new THOK.XC.Dispatching.View.CheckScan(int.Parse(args.Message[0]), args.dtInfo);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strValue = frm.strValue;
                    }

                }
                else if (args.Message[0] == "3")  //�̰���������
                {
                    THOK.XC.Dispatching.View.ReadBarcode frm = new THOK.XC.Dispatching.View.ReadBarcode(args.Message[1]);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strValue = frm.strBarCode;
                    }

                }
                else if (args.Message[0] == "5")  //��¥���⣬RFID��һ��,ѡ��������
                {
                    THOK.XC.Dispatching.View.frmRFIDCheckResult frm = new THOK.XC.Dispatching.View.frmRFIDCheckResult(args.Message[1], args.Message[2], args.dtInfo);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strValue = frm.strBillNo;
                    }
                }
                else if (args.Message[0] == "7")//�̰���⣬�Ѷ�����ش���,��λ�л�
                {
                    THOK.XC.Dispatching.View.CellError frm = new View.CellError(args.Message[1], args.Message[2]);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strValue = frm.Flag;
                    }
                }
                else if (args.Message[0] == "8")//�̰����⣬�Ѷ�����ش���,��λ�޻�
                {
                    THOK.XC.Dispatching.View.CellNewBillSelect frm = new View.CellNewBillSelect(args.Message[1], args.Message[2], args.dtInfo);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strValue = frm.strBillNo;
                    }
                }
                else if (args.Message[0] == "9")//�̰����⣬�Ѷ�����ش���
                {
                    THOK.XC.Dispatching.View.frmNewCraneBill frm = new View.frmNewCraneBill(args.Message[1], args.Message[2], args.dtInfo, args.Message[3]);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strValue = frm.strBillNo;
                    }
                }
                if (context.Services["Crane"] != null)
                {
                    //context.Processes["CraneProcess"].Stop();
                    //context.Processes["CraneProcess"].Start();
                    context.Services["Crane"].Stop();
                    context.Services["Crane"].Start();
                }
            }
            return strValue;
        }
    }
}