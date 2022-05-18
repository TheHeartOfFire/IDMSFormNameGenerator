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
        readonly List<string> StateCodes = new(){ "MULTI", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY" };
        readonly List<string> DocTypes = new(){ "Internal Form", "Third Party Form", "Multi-State / Federal Form", "Retail Installment Contract", "Vendor Form" };
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

        private void UpdateFormName()
        {
            //if (tglFormType.IsOn == false && tglLAW.IsOn == true)
            //{
            //    txtOutput.Text = LaserLaw();
            //    return;
            //}
            //if (tglFormType.IsOn == false)
            //{
            //    txtOutput.Text = Laser();
            //    return;
            //}
            //if (tglFormType.IsOn == true && tglLAW.IsOn == true)
            //{
            //    txtOutput.Text = ImpactLaw();
            //    return;
            //}
            //if (tglFormType.IsOn == true)
            //{
            //    txtOutput.Text = Impact();
            //    return;
            //}
        }

        private string LaserLaw()
        {
            string result = "LAW ";
            return result.Replace('/', '-');
        }

        private string Laser()
        {
            string result = "";
            return result.Replace('/', '-');
        }

        private string ImpactLaw()
        {
            string result = "LAW ";
            return result.Replace('/', '-');
        }

        private string Impact()
        {
            string result = "";
            return result.Replace('/', '-');
        }

        private void UpdateFormName(object sender, EventArgs e)
        {
            UpdateFormName();
        }

        private void UpdateFormName(object sender, MouseButtonEventArgs e)
        {
            UpdateFormName();
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
