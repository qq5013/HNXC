using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.Barcode2;
using System.ServiceModel;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.IO;
using System.Reflection;

namespace WCSBarcode
{
    public partial class Form1 : Form
    {

        string WcfIP = "127.0.0.1";
        string WcfPort = "90";
        string WcfHttp = "http://";
        string WcfService = "PDAService.svc";
        string SocketIP = "127.0.0.1";
        string SocketPort = "7000";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //ReadXml();
            ReadTxt();
            //this.txtScheduleNo.Text = WcfHttp;
            //this.txtOutBillNo.Text = SocketIP;
            //this.txtProductNo.Text = SocketPort;
            this.WindowState = FormWindowState.Maximized;
            
        }
        private void ReadXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            XmlNodeList nodeWcf = doc.GetElementsByTagName("WcfServer");
            if (nodeWcf.Count != 0)
            {
                XmlNode xmlNode = nodeWcf[0];
                WcfIP = xmlNode.Attributes["IP"].Value.ToString();
                WcfPort = xmlNode.Attributes["Port"].Value.ToString();
                WcfHttp += WcfIP + ":" + WcfPort + @"/" + WcfService;
            }

            XmlNodeList nodeTcp = doc.GetElementsByTagName("TCPServer");
            if (nodeTcp.Count != 0)
            {
                XmlNode xmlNode = nodeTcp[0];
                SocketIP = xmlNode.Attributes["IP"].Value.ToString();
                SocketPort = xmlNode.Attributes["Port"].Value.ToString();
            }
            
        }
        private void ReadTxt()
        {
            string strFilePath  = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + @"\Config.txt";
            StreamReader sr = File.OpenText(strFilePath);
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                if (nextLine.Substring(0, 3).ToLower() == "wcf")
                    WcfHttp = nextLine.Substring(4);
                else if(nextLine.Substring(0, 2).ToLower() == "ip")
                    SocketIP = nextLine.Substring(3);
                else if (nextLine.Substring(0, 4).ToLower() == "port")
                    SocketPort = nextLine.Substring(5);
            }
            sr.Close(); 
        }
        private void barcode21_OnScan(Symbol.Barcode2.ScanDataCollection scanDataCollection)
        {
            if (this.InvokeRequired)
            {
                // Executes the OnScan delegate asynchronously on the main thread
                //scanDataCollection.GetFirst.ReaderDataLengths = ReaderDataLengths.MaximumLabel;
                this.BeginInvoke(new Symbol.Barcode2.Design.Barcode2.OnScanEventHandler(barcode21_OnScan), new object[] { scanDataCollection });
            }
            else
            {
                ScanData scanData = scanDataCollection.GetFirst;
                
                if (scanData.Result == Results.SUCCESS)
                {
                    // Write the scanned data and type (symbology) to the list box
                    this.txtBarCode.Text = scanData.Text;
                    BindData(this.txtBarCode.Text);
                }
            }
        }

        private void barcode21_OnStatus(Symbol.Barcode2.StatusData statusData)
        {
            // Checks if the BeginInvoke method is required because the OnStatus delegate is called by a different thread
            if (this.InvokeRequired)
            {
                // Executes the OnStatus delegate asynchronously on the main thread
                this.BeginInvoke(new Symbol.Barcode2.Design.Barcode2.OnStatusEventHandler(barcode21_OnStatus), new object[] { statusData });
            }
            else
            {
                this.txtBarCode.Focus();
            }
        }
        private void BindData(string Barcode)
        {
            System.ServiceModel.Channels.Binding bind = PDAServiceClient.CreateDefaultBinding();
            string remoteAddress = PDAServiceClient.EndpointAddress.Uri.ToString();
            //EndpointAddress endpoint = new EndpointAddress("http://192.168.1.206:90/PDAService.svc");
            EndpointAddress endpoint = new EndpointAddress(WcfHttp);

            PDAServiceClient client = new PDAServiceClient(bind, endpoint);
            DataSet ds = client.GetBarcodeInfo(Barcode);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                this.txtScheduleNo.Text = dt.Rows[0]["SCHEDULE_NO"].ToString();
                this.txtOutBillNo.Text = dt.Rows[0]["BILL_NO"].ToString();
                this.txtInBillNo.Text = dt.Rows[0]["INBILL_NO"].ToString();
                this.txtChannel.Text = dt.Rows[0]["CHANNEL_NAME"].ToString();
                this.txtLine.Text = dt.Rows[0]["LINE_NAME"].ToString();
                this.txtCigarette.Text = dt.Rows[0]["CIGARETTE_NAME"].ToString();
                this.txtFormulaNo.Text = dt.Rows[0]["FORMULA_NAME"].ToString();
                this.txtProductNo.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
                this.txtGrade.Text = dt.Rows[0]["GRADE_NAME"].ToString();
                this.txtOriginal.Text = dt.Rows[0]["ORIGINAL_NAME"].ToString();
                this.txtYear.Text = dt.Rows[0]["YEARS"].ToString();
                this.txtStyle.Text = dt.Rows[0]["STYLE_NAME"].ToString();
            }
            else
            {
                this.txtScheduleNo.Text = "";
                this.txtOutBillNo.Text = "";
                this.txtInBillNo.Text = "";
                this.txtChannel.Text = "";
                this.txtLine.Text = "";
                this.txtCigarette.Text = "";
                this.txtFormulaNo.Text = "";
                this.txtProductNo.Text = "";
                this.txtGrade.Text = "";
                this.txtOriginal.Text = "";
                this.txtYear.Text = "";
                this.txtStyle.Text = "";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            barcode21.EnableScanner = false;
            this.Close();
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            barcode21.EnableScanner = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //string ip = "192.168.1.206";
                //string port = "7000";

                IPAddress serverIp = IPAddress.Parse(SocketIP);

                int serverPort = Convert.ToInt32(SocketPort);

                IPEndPoint iep = new IPEndPoint(serverIp, serverPort);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(iep);

                //触发192.168.1.206 主机的接收事件

                TcpClient client = new TcpClient();
                client.Connect(iep);

                NetworkStream ns = new NetworkStream(socket);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(ns);
                writer.WriteLine("<10001MOVE01CRAN30ACP0100000804300000000000ULULULUL155000>");
                writer.Flush();

                //WCS接收消息为NULL
                //NetworkStream nsClient = client.GetStream();
                //byte[] dataBuffer = Encoding.Default.GetBytes("HELLO");
                //nsClient.Write(dataBuffer, 0, dataBuffer.Length);

                //发送数据
                //nsClient.Flush();

                //socket.Shutdown(SocketShutdown.Both);
                //int rec = socket.Receive(byteMessage);
                //if (rec > 0)
                //{
                //    string sTime = DateTime.Now.ToShortTimeString();

                //    string msg = sTime + ":" + "Message from:";

                //    msg += socket.RemoteEndPoint.ToString() + Encoding.Default.GetString(byteMessage,0,byteMessage.Length);

                //    //ReturnStr = msg;
                //}
                //socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
    }
}