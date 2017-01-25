using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ItemDisplayControl.xaml
    ///     DataContext is OrderController
    ///     Displayed in MainWindow
    /// </summary>
    public partial class ItemDisplayControl : UserControl
    {

        public static OrderController oc;
        public static Order o;

        public ItemDisplayControl()
        {
            InitializeComponent();
        }

        
        /* Buttons within the ItemDisplayControl are populated in the OrderController, and depend on the 
         * whichWindow property of the OrderController. Any number of items of any MenuItem type (drink, sandwich, modifier, dessert) 
         * are visible here. On click event of any of these buttons, the button name (which is given the value of the menuItem name)
         * is extracted from the button, and AddToOrder() is evoked.
         * The LastEventWasMod variable is then given a false value, indicating to the undo button that no modifier needs to be removed
         * from the selected item.
         */
        public ItemDisplayControl(OrderController oController) 
        {
            oc = oController;
            o = oc.currentOrder;
        }

        // past attempts
        // past attempts logic:
        // This UserControl contains all buttons dataBound to a List<MenuItem>
        // but which list of menu items is dependent on the whichWindow value, 
        // which is a property that lives in the OrderController.
        // The ItemDisplayControl is passed the OrderController instance created in 
        // the MainWindow. This allows for access to data within that instance.
        // On construction, the list of menuItems is fetched from the currentOrder.get
        // functions, depending on which category of items is relevant.
        // A Button object is then constructed, and given data from the List<MenuItem>
        // That button is then added to a list of buttons, BtnCollection, which is the 
        // binding for the <ItemControl in the XAML ui of this User Control. This should be 
        // a two way binding. I would prefer to have the data bound rather than add each
        // button upon creation and population to the Grid.
        //List<MenuItem> items = new List<MenuItem>();
        //Order o = mainOrder;

        //if (catCode == 0) {
        //    items = o.getSandwichList();
        //    Category = "Sandwiches:";
        //}
        //else if (catCode == 1) {
        //    items = o.getDrinkList();
        //    Category = "Drinks:";
        //}
        //else {
        //    items = null;
        //}

        //if (items != null) {
        //    foreach (MenuItem item in items) {
        //        Button itemButton = new Button();
        //        itemButton.Click += ItemButton_Click;
        //        itemButton.Name = "btn" + item.catCode.ToString() + item.itemName; // do not change, important to function
        //        itemButton.Content = item.itemName;
        //        itemButton.Margin = new Thickness(10, 10, 30, 10);
        //        itemButton.Height = 40;
        //        itemButton.Width = 60;

        //        ItemCollection.Add(itemButton);
        //        //MyCollection.Add(new Button() { Name = "btn" + item.itemName.ToString(), Content = item.itemName.ToString(), Margin = new Thickness(5, 5, 5, 5), Height = 25, Width = 5 });
        //        //itemButton = null;
        //    }
        //}
        //=======================
        //DataContext = ItemCollection;
        //=======================
        //=======================
        //=======================
        //MyCollection.Add(new Button() { Name = "btn" + item.itemName.ToString(), Content = item.itemName.ToString(), Margin = new Thickness(5, 5, 5, 5), Height = 25, Width = 5 });
        //itemButton = null;
        //=======================
        //======================
        //ItemCollection = new List<Button>();
        //MenuItem i = new MenuItem();
        //i = itemList.First();
        //if (i.catCode == 0) {
        //    Category = "Sandwiches: ";
        //}
        //else if (i.catCode == 1) {
        //    Category = "Drinks: ";
        //}
        //else {
        //    Category = "Unknown Category: ";
        //}
        //=======================
        //if (itemList != null) {
        //    foreach (MenuItem item in itemList) {
        //        Button itemButton = new Button();
        //        itemButton.Click += ItemButton_Click;
        //        itemButton.Name = "btn" + item.catCode.ToString() + item.itemName; // do not change, important to function !?
        //        itemButton.Content = item.itemName;
        //        itemButton.Margin = new Thickness(10, 10, 30, 10);
        //        itemButton.Height = 40;
        //        itemButton.Width = 60;
        //=======================
        //        ItemCollection.Add(itemButton);
        //        //MyCollection.Add(new Button() { Name = "btn" + item.itemName.ToString(), Content = item.itemName.ToString(), Margin = new Thickness(5, 5, 5, 5), Height = 25, Width = 5 });
        //        //itemButton = null;

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button clicked  = new Button();
            clicked = sender as Button;

            string name = clicked.Content.ToString();
            Console.WriteLine("Button clicked {0}", name);

            oc.AddOrderName_toRegister(name); 
            oc.AddtoOrder();
            oc.LastEventWasMod = false;


            // redundant, moved to controller!
            //if (oc.currentOrder.getDrink(name) == null) {
            //    if (oc.currentOrder.getSandwich(name) != null) {
            //        oc.SelectedMenuItem = oc.currentOrder.getSandwich(name);
            //    }
            //    else {
            //        Console.WriteLine("Error in ItemDisplayControl at ItemButtonClick, could not find item in menu");
            //    }
            //}
            //else {
            //    oc.SelectedMenuItem = oc.currentOrder.getDrink(name);
            //}
            // 
            //AppState.AddtoList(clicked.Name); // will correspond to an item, can use Order.getSandiwch/Drink(name) based on catCode within the button Name
            //// TODO: fix AppState within the MainWindow to change string to MenuItem object
        }



    }
}
