using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial_1_20908621;
using System.ServiceModel;

namespace Tutorial1_Server
{
    [ServiceBehavior (ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]

    internal class DataServer:DataServerInterface
    {
        public DataServer()
        {

        }
        public int GetNumEntries()
        {
            var databaseInstance = new DatabaseClass();
            return databaseInstance.GetNumRecords();
        }


        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal,
                                            out string fName, out string lName, out string image)
        {
            var databaseInstance = new DatabaseClass();
            fName = databaseInstance.GetFirstNameByIndex(index);
            lName = databaseInstance.GetLastNameByIndex(index);
            acctNo = databaseInstance.GetAcctNoByIndex(index);
            pin = databaseInstance.GetPinByIndex(index);
            bal = databaseInstance.GetBalanceByIndex(index);
            image = databaseInstance.GetImagegByIndex(index);


        }

    }
}
