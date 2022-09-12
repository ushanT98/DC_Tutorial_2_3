using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


namespace BusinessTier
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        int GetNumEntries();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out
        string fName, out string lName, out string image);

        [OperationContract]
        void GetValuesForSearch(string search, out uint acctNo, out uint pin, out int bal, out
        string fName, out string lName, out string image);
    }
}







