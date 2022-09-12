using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Windows;
using ServerExecutable;
using Tutorial1_Server;
using System.Threading;
using System.Runtime.CompilerServices;

namespace BusinessTier
{
    internal class BusinessServerImplementation: BusinessServerInterface
    {


        public DataServerInterface foob;
        uint LogValue;
        public BusinessServerImplementation()
        {

            ChannelFactory<DataServerInterface> foobFactory;



            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8100/DataService";




            //Establishing connection and getting the client count

            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);

            foob = foobFactory.CreateChannel();

        }

        public int GetNumEntries()
        {
            try
            {
                return foob.GetNumEntries();
            }
            finally
            {
                Log("Task Count :" + foob.GetNumEntries());
            }

        }


        public void GetValuesForEntry(int index, out uint acct, out uint pin, out int bal, out string fName, out string lName, out string image)
        {
            foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out image);
        }


        public void GetValuesForSearch(string searchText, out uint acct, out uint pin, out int bal, out
        string fName, out string lName, out string image)
        {
            acct = 0;
            pin = 0;
            bal = 0;
            fName = null;
            lName = null;
            image = null;
            int numEntry = foob.GetNumEntries();

            bool SearchValueCheck = false;

            Log("Value Variables Initialized Awating Enumeration Of Data");

            for (int i = 1; i <= numEntry; i++)
            {
                uint AccountNo;
                uint Pin;
                int Balance;
                string FirstName;
                string LastName;
                string Image;
                foob.GetValuesForEntry(i, out AccountNo, out Pin, out Balance, out FirstName, out LastName, out Image);
                if (LastName.ToLower().Contains(searchText.ToLower()))
                {
                    Log("Found the result");
                    SearchValueCheck = true;
                    acct = AccountNo;
                    pin = Pin;
                    bal = Balance;
                    fName = FirstName;
                    lName = LastName;
                    image = Image;

                    MessageBox.Show("Here are your Results !");
                    Log("Displaying the result");
                    break;

                }
               


            }

            if(SearchValueCheck == false)
            {
                MessageBox.Show("No match found !");
          
               
                
            }

           Thread.Sleep(5000);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void Log(string logString)
        {
            LogValue++;
            Console.WriteLine("Task Count:" + LogValue);
            Console.WriteLine(logString);
        }


    }


  
}
