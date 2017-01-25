using POS_102.Controls;
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

namespace POS_102
{
    /// <summary>
    /// MainWindow.xaml
    ///     Contains DataSource instance
    ///     Controls DataContext of all controls/windows
    ///     Each instance of the Controls is given the OrderController object, 
    ///     so that data interactions can occur between click events and the controller.
    ///     
    /// </summary>
    public partial class MainWindow : Window
    {
        public OrderController oController;
        public MainWindow()
        {
            InitializeComponent();
                    //this.DataContext = new OrderController(new DataSource("menu_data_assgn2.txt","mods_data_assgn2.txt")); 
            DataSource dSource = new DataSource("menu_data_assgn2.txt", "mods_data_assgn2.txt");

            // Create OrderController Object
            oController = new OrderController(dSource); 

            this.DataContext = oController;


            // create objects of controls, and set all of their DataContexts to the OrderController object
            OrderListControl ticketControl = new OrderListControl();
            ticketControl.DataContext = oController;

            ItemDisplayControl itemDisplay = new ItemDisplayControl(oController);
            itemDisplay.DataContext = oController;
            itemDisplay.DataContextChanged += ItemDisplay_DataContextChanged; // may now be unnecessary

            ModifiersControl modControl = new ModifiersControl(oController); 
            modControl.DataContext = oController;

            MenuControl menuControl = new MenuControl(oController);
            menuControl.DataContext = oController;

            FooterButtonsControl footControl = new FooterButtonsControl(oController);
            footControl.DataContext = oController;

            FinalizedOrderWindow finalWindow = new FinalizedOrderWindow(oController);
            finalWindow.DataContext = oController;

        }

        private void ItemDisplay_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            oController.PopulateButtons(); // unsure if necessary, I think this was part of the duplicate button population problem. probably unnecessary
        }


        /*
         * Events handeled here are items which are not included in Controls. 
         * The bread check box bar which includes the bring water option exist only here in the MainWindow
         * All other controls are initialized separately, and have their events handled within the
         * Control codebehind.
         *  */
        private void ckbWater_Checked(object sender, RoutedEventArgs e)
        {
            oController.bringWater = true;
        }

        // generic bread checked event. Adds String Content of checkbox to the ordered bread list existing in oController.
        private void ckbBread_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = new CheckBox();
            ckb = sender as CheckBox;
            oController.addBread(ckb.Content.ToString());
        }

        private void ckbBread_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = new CheckBox();
            ckb = sender as CheckBox;
            oController.removeBread(ckb.Content.ToString());
        }

        private void ckbWater_Unchecked(object sender, RoutedEventArgs e)
        {
            oController.bringWater = false;
        }

    } // end class MainWindow





    /// <summary>
    ///  unused appstate variables, which were going to be used instead of databinding 
    /// </summary>
    public static class AppState
    {
        private static List<object> _orderList =
            new List<object>();

        public static void AddtoList(object item)
        {
            _orderList.Add(item);
        }

        public static List<object> GetList()
        {
            return _orderList;
        }

    }

    

    public static class ApplicationState
    {
        private static Dictionary<string, object> _orderList =
            new Dictionary<string, object>();

        public static void SetValue(string key, object item)
        {
            _orderList.Add(key, item);
        }

        public static T GetValue<T>(string key)
        {
            return (T)_orderList[key];
        }

        public static void RemoveValue(string key)
        {
            _orderList.Remove(key);
        }
    }
}
