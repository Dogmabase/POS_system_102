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
    /// Interaction logic for MenuControl.xaml
    /// 
    /// This control makes changes to the whichWindow variable of the
    /// OrderController class, and invokes the PopulateButtons() method of
    /// the controller as well. The ShowMod variable of the controller is 
    /// modified here as well. 
    /// 
    /// 
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public static OrderController oc;
        public static Order o;
        public MenuControl()
        {
            InitializeComponent();

        }

        public MenuControl(OrderController oController)
        {
            oc = oController;
            o = oc.currentOrder; // not used
        }

        // if beverage button is clicked, show beverage buttons and do not show mods
        private void btnBev_Click(object sender, RoutedEventArgs e)
        {
            oc.whichWindow = 1;
            oc.ShowMod = false;
            oc.PopulateButtons();
        }

        // if sandwich button is clicked, show sandwich buttons but no mods
        private void btnSand_Click(object sender, RoutedEventArgs e)
        {
            oc.whichWindow = 0;
            oc.ShowMod = false;
            oc.PopulateButtons();
        }

        // if the dessert button is clicked, show dessert buttons but no mods
        private void btnDst_Click(object sender, RoutedEventArgs e)
        {
            oc.whichWindow = 2;
            oc.ShowMod = false;
            oc.PopulateButtons();
        }

        // if the modifiers button is clicked, show mods (logic for mod button population is in the controller)
        private void btnMod_Click(object sender, RoutedEventArgs e)
        {
            oc.ShowMod = true;
        }
    }
}
