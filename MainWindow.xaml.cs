using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    // Custom class
    //Properties
     public class Lots
     { 
         
        //Constructors  
        public double Calc(double LotsTraded ,double rPercentageRisk , double rPipValue , double rSLinPips)
        { 
            // Equation to work out Lots & send Lots back to program class   
            LotsTraded = rPercentageRisk / (rPipValue * rSLinPips);
            return  LotsTraded;
        }

         internal static double Calc(double rPercentageRisk, double rPipValue, double rSLinPips)
         {
            throw new NotImplementedException();
         }
     }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Assigning variables to input components
            double rAccountBalance = edtAccountBalance.text;
            double rPercentageRisk = edtPercentageRisk.text;
            double rPipValue = 10;
            string sAccountCurrency = cmbAccCurrency.text;
            double rSLinPips = edtSlPips.text;
            String sAccounttype = cmbAccount.text;

            // Switch statement to get Account type
            switch (sAccounttype)
            {
                case "Standard Account": //Get variable value for final formula depending on the account balance
                    rPipValue = 10;
                    break;
                case "Mini Account":
                    rPipValue = 1;
                    break;

                case "Micro Account":
                    rPipValue = 0.1;
                    break;

                default:  // for if no account type was chosen show error message 
                System.Windows.MessageBox.Show("You Need to pick an Account Type", "Error", (MessageBoxButton)MessageBoxButtons.OKCancel ,
                    (MessageBoxImage)MessageBoxIcon.Warning );
                    break;
            }
            // TO find out what account currency your are using
            switch (sAccountCurrency)
            {
                case "USD":
                    sAccountCurrency = "USD";
                    break;

                case "ZAR":
                    sAccountCurrency = "ZAR";
                    break;

                case "NZD":
                    sAccountCurrency = "NZD";
                    break;

                case "EUR":
                    sAccountCurrency = "EUR";
                    break;

                case "JPY":
                    sAccountCurrency = "JPY";
                    break;

                case "GBP":
                    sAccountCurrency = "GBP";
                    break;

                case "CHF":
                    sAccountCurrency = "CHF";
                    break;

                case "AUD":
                    sAccountCurrency = "AUD";
                    break;

                case "CAD":
                    sAccountCurrency = "CAD";
                    break;
            }

         // Instantiate/Send variables with value to class so the lots can be worked out
            Lots mylots = new Lots();
            double rLots = Lots.Calc(rPercentageRisk, rPipValue, rSLinPips);
            rtbInformation.TextInput = rLots; 
        }
    
}
