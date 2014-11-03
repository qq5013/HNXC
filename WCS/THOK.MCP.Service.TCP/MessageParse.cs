using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.MCP.Service.TCP
{
    public class MessageParse : THOK.MCP.IProtocolParse
    {
        public Message Parse(string msg)
        {
            Message result = null;
            THOK.TCP.Util.MessageParse parse = new THOK.TCP.Util.MessageParse();
            try
            {
                THOK.TCP.Util.Message message = parse.Parse(msg);
                if(message==null)
                    result = new Message(msg);
                else if(message.Command.ToString().Length<=0)
                    result = new Message(message.Msg);
                else
                    result = new Message(true, msg, message.Command, message.Parameters);
            }
            catch(Exception e)
            {
                result = new Message(msg + ";ERROR:" + e.Message);
            }
            return result;
        }
    }
}