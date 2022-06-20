using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace IDMSFormNameGenerator.Pages
{
    /// <summary>
    /// Interaction logic for FileNameGenerator.xaml
    /// </summary>
    public partial class FileNameGenerator : UserControl
    {
        readonly List<string> StateCodes = new() { "MULTI", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY" };
        readonly List<string> DocTypes = new() { "Internal Form", "Third Party Form", "Multi-State / Federal Form", "Retail Installment Contract", "Vendor Form" };
        readonly Dictionary<string, string> Abbreviations = new() 
        {
            { "Retail Installment Contract", "RISC" },
            { "RetailInstallmentContract", "RISC" },
            { "Agreement To Provide Insurance", "AgrmntToProviceIns" },
            { "AgreementToProvideInsurance", "AgrmntToProviceIns" },
            { "Purchase Agreement", "RPA" },
            { "PurchaseAgreement", "RPA" },
            { "Odometer", "Odom" },
            { "Disclosure", "Disc" },
            { "Application", "App" },
            { "Statement", "Stmnt" },
            { "Insurance", "Ins" }
        };
        public FileNameGenerator()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cboStates.ItemsSource = StateCodes;
            cboDocType.ItemsSource = DocTypes;
            cboDocType.SelectedIndex = 0;
        }

        private static string JoinParts(params string[] parts) => string.Join('_', parts.Where(c => !string.IsNullOrEmpty(c)));
        private void UpdateFileName()
        {
            string name = cboDocType.Text switch
            {
                "Internal Form" => JoinParts(txtIDMS.Text, txtFormName.Text, txtDate.Text, txtVariation.Text),
                "Third Party Form" => JoinParts(txtDlrName.Text, GetAbbreviation(txtFormName.Text), txtCode.Text, txtDate.Text, txtVariation.Text),
                "Multi-State / Federal Form" => JoinParts(cboStates.Text, txtFormName.Text, txtCode.Text, txtDate.Text, txtVariation.Text),
                "Retail Installment Contract" => JoinParts(cboStates.Text, txtCode.Text, txtDate.Text, GetAbbreviation(txtFormName.Text), txtVariation.Text, (tglV2.IsOn == true ? "IDMS" : "")),
                "Vendor Form" => JoinParts(cboStates.Text, txtCode.Text, txtDate.Text, GetAbbreviation(txtFormName.Text), txtVariation.Text, (tglV2.IsOn == true ? "IDMS" : "")),
                _ => "Unreachable"
            };

            name = name.Replace(" ", "");

            if (name.Length > 50)
                name = GetAbbreviation(name);
            
            if (name.Length > 50)
                txtFileName.Foreground = new SolidColorBrush(Colors.Red);
            else
                txtFileName.Foreground = new SolidColorBrush(Colors.White);

            txtFileName.Text = name;
        }


        private void UpdateDocName()
        {
            string name = cboDocType.Text switch
            {
                "Internal Form" => JoinParts(txtDlrName.Text, txtFormName.Text),
                "Third Party Form" => JoinParts(txtDlrName.Text, txtFormName.Text, txtDate.Text, txtVariation.Text),
                "Multi-State / Federal Form" => JoinParts(cboStates.Text, txtFormName.Text, txtDate.Text, txtVariation.Text),
                "Retail Installment Contract" => JoinParts(txtFormName.Text, cboStates.Text, txtVariation.Text, txtDate.Text, (tglV2.IsOn == true ? "IDMS" : "")),
                "Vendor Form" => JoinParts(txtDlrName.Text, txtFormName.Text, cboStates.Text, txtVariation.Text, txtDate.Text, (tglV2.IsOn == true ? "IDMS" : "")),
                _ => "Unreachable"
            };

            if (name.Length > 50)
                name = GetAbbreviation(name);
            
            if (name.Length > 50)
                txtFileName_Copy.Foreground = new SolidColorBrush(Colors.Red);
            else
                txtFileName_Copy.Foreground = new SolidColorBrush(Colors.White);

            txtFileName_Copy.Text = name;
        }

        private string GetAbbreviation(string input)
        {
            foreach (var item in Abbreviations)
            {
                input = input.Replace(item.Key, item.Value);
            }

            return input;
        }
        
            private void UpdateFileName(object sender, EventArgs e)
        {
            UpdateFileName();
            UpdateDocName();
        }

        private void UpdateFileName(object sender, MouseButtonEventArgs e)
        {
            UpdateFileName();
            UpdateDocName();

        }

        private void txtOutput_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFileName.Text))
            {
                txtFileName.SelectAll();
            }
        }

        private void txtOutput_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var mouseDownEvent = new MouseButtonEventArgs(
                Mouse.PrimaryDevice,
                Environment.TickCount,
                MouseButton.Right)
            {
                RoutedEvent = Mouse.MouseUpEvent,
                Source = txtFileName,
            };


            InputManager.Current.ProcessInput(mouseDownEvent);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cboStates.Text = string.Empty;
            //txtOEMDealer.Text = string.Empty;
            //txtCoBank.Text = string.Empty;
            txtFormName.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtDate.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtFileName.Text);
        }
    }
}
