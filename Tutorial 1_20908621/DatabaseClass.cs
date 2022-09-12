using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial_1_20908621
{
    public class DatabaseClass
    {
        List<DatabaseStorage> datalist;

        public DatabaseClass()
        {
            datalist = new List<DatabaseStorage>();

            String[] firstNameList = { "Emma", "Asha", "Nathan", "Shay", "Shermon", "Laila" };
            String[] lastNameList = { "Lawrance", "Dias", "Taylor", "Williams", "Rogers", "Anderson" };
            String[] imagePool = { "h8w18Fc/1.jpg", "CW74FzT/3.jpg", "BzNNgcz/2.png", "n09VtZz/5.jpg", "HGf3WQ4/4.jpg", "NVtdRYg/6.jpg", "2q6nxf0/7.png", "wYz9JSp/8.png" };

            Random random = new Random(1);

            for (int i = 0; i < 100000; i++)
            {
                DatabaseStorage client = new DatabaseStorage();
                client.firstName = firstNameList[random.Next(0, firstNameList.Length)];
                client.lastName = lastNameList[random.Next(0, lastNameList.Length)];
                client.acctNo = (uint)random.Next(0000000001, 1000000000);
                client.pin = (uint)random.Next(1000, 100000);
                client.balance = random.Next(0, 99999999);
                client.image = imagePool[random.Next(0, imagePool.Length)];

                datalist.Add(client);
            }

        }

        public String GetFirstNameByIndex(int index)
        {
            return datalist[index - 1].firstName;
        }

        public String GetLastNameByIndex(int index)
        {
            return datalist[index - 1].lastName;
        }

        public uint GetAcctNoByIndex(int index)
        {
            return datalist[index - 1].acctNo;
        }

        public uint GetPinByIndex(int index)
        {
            return datalist[index - 1].pin;
        }

        public int GetBalanceByIndex(int index)
        {
            return datalist[index - 1].balance;
        }

        public int GetNumRecords()
        {
            return datalist.Count;
        }

        public string GetImagegByIndex(int index)
        {
            return datalist[index - 1].image;
        }





    }
}
