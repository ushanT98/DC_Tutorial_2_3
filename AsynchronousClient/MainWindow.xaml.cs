using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessTier;
using Tutorial_1_20908621;
using System.ServiceModel;
using System.Runtime.Remoting.Messaging;

namespace AsynchronousClient
{
    public delegate string Search(string value);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;
        private Search search;
        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<BusinessServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8200/BusinessService";

            foobFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();

            TotalNum.Text = foob.GetNumEntries().ToString();
        }


        // this

        public static string Test(int compare)
        {
            if (compare == 0)
                return "equal to";
            else if (compare < 0)
                return "less than";
            else
                return "greater than";
        }






       // public AsyncCallback OnSearchCompletion { get; private set; }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "", image = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            string prefix = "";

            try
            {
                index = Int32.Parse(Index.Text);
            }
            catch
            {
                MessageBox.Show("Please Enter a valid number!");
                return;
            }



            try
            {
                if (index > foob.GetNumEntries() || index <= 0)

                {
                    MessageBox.Show("Index is out of range !");
                    return;
                }
                else
                {
                    foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out image);
                }


                FirstName.Text = fName;
                LastName.Text = lName;
                Balance.Text = bal.ToString("C");
                AcctNo.Text = acct.ToString();
                Pin.Text = pin.ToString("D4");

            }
            catch
            {
                MessageBox.Show("Cannot connect to the server !!!");
                return;
            }
            prefix = "https://i.ibb.co/";

            Uri prefixUri = new Uri(prefix);
            Uri suffixUri = new Uri(image, UriKind.Relative);
            Uri fullUri = new Uri(prefixUri, suffixUri);

            Console.WriteLine(fullUri.ToString());

            ImageCode.Source = new BitmapImage(fullUri);
        }



        // searching

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            if (!SpecialCharacter(SearchBox.Text) && !SearchBox.Text.All(char.IsDigit))
            {
                UI_Disable();
                ChageProgressBarValue(20);
                search = SearchManage;
                AsyncCallback callback;
                callback = this.OnSearchCompletion;

                ChageProgressBarValue(50);
                ChageProgressBarValue(70);
                var result = search.BeginInvoke(SearchBox.Text, callback, null);
                ChageProgressBarValue(100);
            }
            else
            {
                MessageBox.Show("Invalid Search !");
                return;
            }

        }


        public string SearchManage(string value)
        {
            string fName = null;
            string lName = null;
            string image = null;
            int bal = 0;
            uint acct = 0;
            uint pin = 0;
            string prefix = null;

            
            foob.GetValuesForSearch(value, out acct, out pin, out bal, out fName, out lName, out image);
            

            try
            {
                FirstName.Dispatcher.Invoke(new Action(() => FirstName.Text = fName));
                LastName.Dispatcher.Invoke(new Action(() => LastName.Text = lName));
                Balance.Dispatcher.Invoke(new Action(() => Balance.Text = bal.ToString("C")));
                AcctNo.Dispatcher.Invoke(new Action(() => AcctNo.Text = acct.ToString()));
                Pin.Dispatcher.Invoke(new Action(() => Pin.Text = pin.ToString("D4")));

                UI_Enable();

                prefix = "https://i.ibb.co/";

                Uri prefixUri = new Uri(prefix);
                Uri suffixUri = new Uri(image, UriKind.Relative);
                Uri fullUri = new Uri(prefixUri, suffixUri);

                Console.WriteLine(fullUri.ToString());

                ImageCode.Source = new BitmapImage(fullUri);


                
                return "Task Compleated";

            }
            catch
            {
                Console.WriteLine("Error:Failed");
                return "Error";
            }
        }



        private void OnSearchCompletion(IAsyncResult asyncResult)
        {
            /*
            Search searchdel = (Search)asyncResult.AsyncState;
            var result = searchdel.EndInvoke(asyncResult);

            Console.WriteLine("Deligate Has Finished:returned" + result);

            */

            Search searchdel;
            AsyncResult asyncobj = (AsyncResult)asyncResult;
            if (asyncobj.EndInvokeCalled == false)
            {
                searchdel = (Search)asyncobj.AsyncDelegate;
                var result = searchdel.EndInvoke(asyncResult);
            }

            asyncobj.AsyncWaitHandle.Close();




        }


        public void ChageProgressBarValue(int val)
        {
            myProgressBar.Dispatcher.Invoke(new Action(() => myProgressBar.Value = val));
        }

        private void UI_Enable()
        {
            SearchBox.Dispatcher.Invoke(new Action(() => SearchBox.IsReadOnly = false));
            Index.Dispatcher.Invoke(new Action(() => Index.IsReadOnly = false));
            Go.Dispatcher.Invoke(new Action(() => Go.IsEnabled = true));
            Search.Dispatcher.Invoke(new Action(() => Search.IsEnabled = true));

        }

        private void UI_Disable()
        {
            SearchBox.Dispatcher.Invoke(new Action(() => SearchBox.IsReadOnly = true));
            Index.Dispatcher.Invoke(new Action(() => Index.IsReadOnly = true));
            Go.Dispatcher.Invoke(new Action(() => Go.IsEnabled = false));
            Search.Dispatcher.Invoke(new Action(() => Search.IsEnabled = false));
        }


        private bool SpecialCharacter(string inputval)
        {
            return inputval.Any(charr => !Char.IsLetterOrDigit(charr));
        }




    }
}
