using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace PDAWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPDAService" in both code and config file together.
    [ServiceContract]
    public interface IPDAService
    {
        [OperationContract]
        DataSet GetBarcodeInfo(string Barcode);
        [OperationContract]
        int SetBarcodeInfo(string Barcode, string Checker, string Result, string ChannelNo);
        [OperationContract]
        bool ValidateUser(string userName, string password);        
    }

}
