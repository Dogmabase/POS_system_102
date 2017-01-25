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

namespace POS_102.Controls
{
    /// <summary>
    /// Interaction logic for FooterButtonsControl.xaml
    /// 
    /// Footer Control makes changes to the Order Controller fields:orderItems, and ShowMod.
    /// 
    /// Several methods of the OrderController are called here.
    /// </summary>
    public partial class FooterButtonsControl : UserControl
    {
        private static OrderController oc;

        public FooterButtonsControl()
        {
            InitializeComponent();
        }

        public FooterButtonsControl(OrderController oController)
        {
            InitializeComponent();
            oc = oController;
        }

        // if undo is clicked, remove the last MenuItem or false-menuItem-Modifier from the orderItems List in the OrderController object
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {

            if (oc.LastEventWasMod) {
                oc.RemovefromModList(); // remove last mod from the menuItem selected
                oc.DeleteLastinOrder(); // remove last menuItem or modifier from the orderItems list
            }
            else {
                oc.DeleteLastinOrder(); // remove last menuItem or modifier from the orderItems list
            }
        }

        // on btn clear click, ask user if they are sure and then clear ordered items.
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to clear the order?", "Clear Order", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) {
                oc.orderItems.Clear(); // clear menuItem List called orderItems.
                oc.currentOrder.ClearOrder(); // clear actual currentOrder object data, which calls the ClearOrder method of the Order class
            }
        }

        // if mod is clicked, show modifiers
        private void btnMod_Click(object sender, RoutedEventArgs e)
        {
            oc.ShowMod = true; 
        }

        // send order, invokes FinalizeOrder method of the OrderController
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Send order to printer?", "Send Order", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) {
                oc.FinalizeOrder();
            }
        }
    }
}
