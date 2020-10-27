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

namespace RealCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    // Custom class
    //Properties



    public class Lots
    {
        //Inisializing variables
        public double rAccountBalance { get; set; }
        public double rPercentageRisk { get; set; }
        public double rPipValue  { get; set; }
        public string sAccountCurrency { get; set; }
        public double rSLinPips { get; set; }
        public String sAccounttype { get; set; }
        //Constructors  
        public double Calc( double rPercentageRisk, double rPipValue, double rSLinPips)
        {
            //Variable to send back lots
            double LotsTraded;

            // Equation to work out Lots & send Lots back to program class 
            LotsTraded = rPercentageRisk / (rPipValue * rSLinPips);
            return LotsTraded;
        }    
    }

    public partial class MainWindow : Window
    {
        public MainWindow() //Code that runs when program starts running
        {
            InitializeComponent();
            edtCurrencyValue.Visibility = Visibility.Hidden;
            lblQuestion.Visibility = Visibility.Hidden;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {   //Clearing all the values of variables before starting the calculations
            double rAccountBalance = 0;
            double rSLinPips = 0;
            double rRisk = 0;
            double rPercentageRisk = 0;
            double rExchangerate = 1;
            double rPricePerPip = 0;
            double rOnePip = 0;
            string sCurrencySymbol = "$";
            Double Result = 0;
            double rAmountatRisk = 0;


            //Adding values from UI to variables
            double rPipValue = 10; 
            rSLinPips = Convert.ToDouble(edtSlPips.Text);//Convert.ToDouble converts string to double
            rAccountBalance = Convert.ToDouble(edtAccountBalance.Text);
            rRisk = Convert.ToDouble(edtPercentageRisk.Text);
            rPercentageRisk = (rAccountBalance * (rRisk / 100));
            String sAccountCurrency = cmbAccCurrency.Text;
            String sAccounttype = cmbAccount.Text;


            //Clear Rich Textbox already existing text
            rtbInformation.Document.Blocks.Clear();

            // Switch statement to get Account type
            switch (sAccounttype)
            {
                case "Standard Account": //Get variable value for final formula depending on the account balance
                    rPipValue = 10;
                    rOnePip = 0.001; //Pip value for price per pip equation
                    break;
                case "Mini Account":
                    rPipValue = 1;
                    rOnePip = 0.0001;
                    break;
                case "Micro Account":
                    rPipValue = 0.1;
                    rOnePip = 0.00001;
                    break;

                default:  // for if no account type was chosen show error message 
                    MessageBox.Show("Message", "MessageBox Title", MessageBoxButton.OKCancel);
                    break; 
            }
            // TO find out what account currency you are using
            switch (sAccountCurrency)
            {
                case "USD":
                    sAccountCurrency = "USD";
                    sCurrencySymbol = "$";
                    break;

                case "ZAR":
                    sAccountCurrency = "ZAR";
                    sCurrencySymbol = "R";
                    break;

                case "NZD":
                    sAccountCurrency = "NZD";
                    sCurrencySymbol = "$";
                    break;

                case "EUR":
                    sAccountCurrency = "EUR";
                    sCurrencySymbol = "€";
                    break;

                case "JPY":
                    sAccountCurrency = "JPY";
                    sCurrencySymbol = "¥";
                    break;

                case "GBP":
                    sAccountCurrency = "GBP";
                    sCurrencySymbol = "£";
                    break;

                case "CHF":
                    sAccountCurrency = "CHF";
                    sCurrencySymbol = "CHf";
                    break;

                case "AUD":
                    sAccountCurrency = "AUD";
                    sCurrencySymbol = "$";
                    break;

                case "CAD":
                    sAccountCurrency = "CAD";
                    sCurrencySymbol = "$";
                    break;
            }
            //Gets exchange rate to multiply with lots if USD isn't Account Balance currency
            if (edtCurrencyValue.Visibility == 0)
            {
                rExchangerate = Convert.ToDouble(edtCurrencyValue.Text);
            }
            // Makes the connection between the lots created class to this class so that I can use its variables in this class
            Lots mylots = new Lots();
        
            // Setting this class variables to the lots class variables so it can do the calculations 
            Result = mylots.Calc(rPercentageRisk, rPipValue, rSLinPips);


            //Only times with Exchange rate if Account Currency is not USD
            if (edtCurrencyValue.Visibility == 0)
            {
               Result = Result * rExchangerate;
            }
            //To display amount the user is risking on this trade. 
            rAmountatRisk = Math.Round(rPercentageRisk);

           //Works out price per pip movement
            rPricePerPip = Math.Round(((rOnePip / rExchangerate) * (Result * 10000)), 2);

            //Round lots to 2 decimal places 
            Result = Math.Round(Result, 2);

            //Customize Rich Text Box
            rtbInformation.FontSize = 30;

            // Write information onto the rich text box
            rtbInformation.AppendText(Environment.NewLine + Environment.NewLine  + "Lots : "  + Convert.ToString(Result));
            rtbInformation.AppendText(Environment.NewLine + "Price per Pip Movement : " + sCurrencySymbol + Convert.ToString(rPricePerPip));
            rtbInformation.AppendText(Environment.NewLine + "Balance At Risk : " + sCurrencySymbol + Convert.ToString(rAmountatRisk));
        }

        //Runs when there is a changed made in the currency combo box
        private void cmbAccCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sAccountCurrency = "";
            switch (cmbAccCurrency.SelectedIndex)  //To find out what currency was chosen
            {
                case 0:
                    sAccountCurrency = "USD";
                    break;
                case 1:
                    sAccountCurrency = "ZAR";
                    break;
                case 2:
                    sAccountCurrency = "NZD";
                    break;
                case 3:
                    sAccountCurrency = "EUR";
                    break;
                case 4:
                    sAccountCurrency = "JPY";
                    break;
                case 5:
                    sAccountCurrency = "GBP";
                    break;
                case 6:
                    sAccountCurrency = "CHF";
                    break;
                case 7:
                    sAccountCurrency = "AUD";
                    break;
                case 8:
                    sAccountCurrency = "CAD";
                    break;
            }


            if (cmbAccCurrency.SelectedIndex > 0 ) //If a currency which is not USD is chosen show hidden label and text box
            {
               edtCurrencyValue.Visibility = Visibility.Visible;
               lblQuestion.Visibility = Visibility.Visible;
               String sSelectedCurrency = cmbTradingPair.Text.ToString();
               string sRemoved = sSelectedCurrency.Remove(0,3); //Remove the frist 3 characters of the Currency pair
               string sCurrencyPair = sAccountCurrency + sRemoved; //Add the Account Balance currency to the beginning of the currency pair
               lblQuestion.Content = sCurrencyPair + " exchange rate ?" ;//Displays the currency we need the exchange rate for
            }
            else //If USD is chosen
            {
               edtCurrencyValue.Visibility = Visibility.Hidden;
               lblQuestion.Visibility = Visibility.Hidden;
            }
        }

        // Code to clear default text of edit boxes
        private void edtAccountBalance_MouseEnter(object sender, MouseEventArgs e)
        {
            if(edtAccountBalance.Text == "Account Balance") 
                {
                    edtAccountBalance.Text = string.Empty;
                }   
        }

        private void edtSlPips_MouseEnter(object sender, MouseEventArgs e)
        {
            if (edtSlPips.Text == "Stop Loss Pips")
            {
                edtSlPips.Text = string.Empty;
            }
        }

        private void edtPercentageRisk_MouseEnter(object sender, MouseEventArgs e)
        {
            if (edtPercentageRisk.Text == "Percentage Risk")
            {
                edtPercentageRisk.Text = string.Empty;
            }
        }
    }
}
