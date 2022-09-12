using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial_1_20908621
{
    internal class DatabaseGenerator:DatabaseStorage
    {
        private string GetFirstname
        {
            get { return firstName; }
        }

        private string GetLastname { 
            get { return lastName; } 
        }

        private uint GetPIN
        {
            get { return pin; }
        }

        private uint GetAccNo
        {
            get { return acctNo; }
        }

        private int GetBalance
        {
            get { return balance; }
        }

        private string GetImage
        {
            get { return image; }
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string
lastName, out int balance, out string image)
        {
            firstName = GetFirstname;
            lastName = GetLastname;
            acctNo = GetAccNo;
            balance = GetBalance;
            pin = GetPIN;
            image = GetImage;
        }




    }
}

