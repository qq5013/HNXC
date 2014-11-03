﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using THOK.OPC;

namespace THOK.MCP.Service.Siemens
{
    public class OPCService: THOK.MCP.AbstractService
    {
        private OPCServer opcServer = null;
        private object locker = new object();
        public override void Initialize(string file)
        {
            opcServer = new OPCServer(Name);

            Config.Configuration config = new Config.Configuration(file);
            opcServer.Connect(config.ProgID, config.ServerName);// opcServer.Connect(config.ConnectionString);
           
            OPCGroup group = opcServer.AddGroup(config.GroupName, config.UpdateRate);
            foreach (Config.ItemInfo item in config.Items)
            {
                group.AddItem(item.ItemName, item.OpcItemName, item.ClientHandler, item.IsActive);
            }
            opcServer.Groups.DefaultGroup.OnDataChanged += new OPCGroup.DataChangedEventHandler(DefaultGroup_OnDataChanged);
        }

        void DefaultGroup_OnDataChanged(object sender, DataChangedEventArgs e)
        {
            DispatchState(e.ItemName, e.States);
        }

        public override void Release()
        {
            opcServer.Release(); 
        }

        public override void Start()
        {
            
        }

        public override void Stop()
        {
         
        }

        public override object Read(string itemName)
        {
            OPCItem item = GetItem(itemName);
            return item.Read();
        }

        public override bool Write(string itemName, object state)
        {
            OPCItem item = GetItem(itemName);
            try
            {
                WriteToLog("ItemName:" + itemName + " Value: " + state.ToString());
            }
            catch { }
            return item.Write(state);
        }

        private OPCItem GetItem(string itemName)
        {
            OPCGroup group = opcServer.Groups.DefaultGroup;
            OPCItem item = item = group.Items[itemName];
            if (item == null)
                throw new Exception(string.Format("未能找到名称为'{0}'的OPC项", itemName));
            return (OPCItem)item;
        }
        private void WriteToLog(string Message)
        {
            lock (locker)
            {
                if (!System.IO.Directory.Exists("Siemens"))
                    System.IO.Directory.CreateDirectory("Siemens");
                string path = "Siemens/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                StreamWriter sw = File.AppendText(path);                
                sw.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Message));
                sw.Close();
                //System.IO.File.AppendAllText(path, string.Format("{0} :  {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), Message + "\r\n"));
            }
        }
    }
}
