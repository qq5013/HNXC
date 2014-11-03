using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using THOK.ParamUtil;

namespace THOK.XC.Dispatching
{
    public class Parameter: BaseObject
    {
        private string serverName;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        //private string dbName;

        //[CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        //public string DBName
        //{
        //    get { return dbName; }
        //    set { dbName = value; }
        //}

        private string dbUser;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }



        //����ɨ��ǹʹ��USB�ӿڣ������Ρ�


        //private string scanPortName;

        //[CategoryAttribute("ɨ����ͨ�Ų���"), DescriptionAttribute("ɨ�������ں�"), Chinese("���ں�")]
        //public string ScanPortName
        //{
        //    get { return scanPortName; }
        //    set { scanPortName = value; }
        //}

        //private string scanBaudRate;

        //[CategoryAttribute("ɨ����ͨ�Ų���"), DescriptionAttribute("ɨ����������"), Chinese("������")]
        //public string ScanBaudRate
        //{
        //    get { return scanBaudRate; }
        //    set { scanBaudRate = value; }
        //}

        private string mesWebServiceUrl;
        [CategoryAttribute("MES�������ߵ�ַ"), DescriptionAttribute("WebService URL"), Chinese("��ַ")]
        public string MesWebServiceUrl
        {
            get { return mesWebServiceUrl; }
            set { mesWebServiceUrl = value; }
        }
        private string isOutOrder;
        [CategoryAttribute("���ⰴ�䷽����"), DescriptionAttribute("�����Ƿ�����"), Chinese("����")]
        public string IsOutOrder
        {
            get { return isOutOrder; }
            set { isOutOrder = value; }
        }
        private string ip;

        [CategoryAttribute("�Ѷ��ͨ�Ų���"), DescriptionAttribute("��ַIP"), Chinese("IP")]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private int port;

        [CategoryAttribute("�Ѷ��ͨ�Ų���"), DescriptionAttribute("ͨ�Ŷ˿�"), Chinese("�˿�")]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }


        private string plc1ServerName;
        [CategoryAttribute("һ¥PLCͨ�Ų���"), DescriptionAttribute("��������"), Chinese("��������")]
        public string PLC1ServerName
        {
            get { return plc1ServerName; }
            set { plc1ServerName = value; }
        }

        private string plc1ServerIp;
        [CategoryAttribute("һ¥PLCͨ�Ų���"), DescriptionAttribute("�����ַIP"), Chinese("����IP")]
        public string PLC1ServerIP
        {
            get { return plc1ServerIp; }
            set { plc1ServerIp = value; }
        }


        private string plc1GroupString;
        [CategoryAttribute("һ¥PLCͨ�Ų���"), DescriptionAttribute("һ¥PLCͨѶ��������"), Chinese("��������")]
        public string PLC1GroupString
        {
            get { return plc1GroupString; }
            set { plc1GroupString = value; }
        }

        private int plc1UpdateRate;
        [CategoryAttribute("һ¥PLCͨ�Ų���"), DescriptionAttribute("һ¥PLCˢ��Ƶ��"), Chinese("ˢ��Ƶ��")]
        public int PLC1UpdateRate
        {
            get { return plc1UpdateRate; }
            set { plc1UpdateRate = value; }
        }


        private string plc2ServerName;
        [CategoryAttribute("��¥PLCͨ�Ų���"), DescriptionAttribute("��������"), Chinese("��������")]
        public string PLC2ServerName
        {
            get { return plc2ServerName; }
            set { plc2ServerName = value; }
        }

        private string plc2ServerIp;
        [CategoryAttribute("��¥PLCͨ�Ų���"), DescriptionAttribute("�����ַIP"), Chinese("����IP")]
        public string PLC2ServerIP
        {
            get { return plc2ServerIp; }
            set { plc2ServerIp = value; }
        }


        private string plc2GroupString;
        [CategoryAttribute("��¥PLCͨ�Ų���"), DescriptionAttribute("��¥PLCͨѶ��������"), Chinese("��������")]
        public string PLC2GroupString
        {
            get { return plc2GroupString; }
            set { plc2GroupString = value; }
        }

        private int plc2UpdateRate;
        [CategoryAttribute("��¥PLCͨ�Ų���"), DescriptionAttribute("��¥PLCˢ��Ƶ��"), Chinese("ˢ��Ƶ��")]
        public int PLC2UpdateRate
        {
            get { return plc2UpdateRate; }
            set { plc2UpdateRate = value; }
        }




    }
}
