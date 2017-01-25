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
    /// ModifiersControl.xaml
    ///     Displayed in the MainWindow
    ///     DataContext is OrderController
    ///     
    /// </summary>
    public partial class ModifiersControl : UserControl
    {

        public static OrderController oc;
        public static Order o;

        public string mod_name;
        public ModifiersControl()
        {
            InitializeComponent();
        }

        
        // I moved all logic from here to the central controller. 
        /* Here, the mod_name is used to add text to a new Modifier. This allows
         * for the qualifiers "NO, SUB" etc to be added directly to the Modifier 
         * so that they will not be lost, and can be updated in real time in
         * the OrderListControl panel. 
         * The list of modifiers displayed is handled in the controller, as the 
         * list is populated differently depending on which categroy of MenuItems is
         * displayed in the ItemDisplay, and the CurrentSelectedItem. 
         * The Click events are here instead of in the controller, both because delocalized
         * click events are out of my scope of knowledge, and for readability of the controller.
         */
        public ModifiersControl(OrderController oController)
        {
            oc = oController; // allows for controller to be used by other methods, oc is declared static
            o = oController.currentOrder; 

        }

        //past attempts:
        ////int CatCode = oController.SelectedMenuItem.catCode;  // would need to catch for null SelectedMenuItem !? todo
            //int CatCode = oController.whichWindow;
            //List<MenuItem> items = new List<MenuItem>();
            //List<Modifier> currentModifiers = new List<Modifier>();

            //// for each modifier whose modCode matches the windowCode, add that modifier to the list of currentModifiers
            //// note: whichWindow, modCode, and catCode are all derived from the MenuItem property int catCode
            //foreach(Modifier m in modList) {
            //    if (m.ModCode == CatCode)
            //        currentModifiers.Add(m);
            //}

            //// if currentModifiers is not null, create new button and give it data. Then add new button to button list, ModCollection. 
            //// as alternate, add this new button (itemButton) to Grid programatically
            //if (currentModifiers != null) {
            //    foreach (Modifier mod in currentModifiers) {
            //        Button modButton = new Button();
            //        modButton.Click += ModButton_Click;
            //        modButton.Name = "btn" + mod.ModCode.ToString() + mod.ModName; // do not change, important to function
            //        modButton.Content = mod.ModName;
            //        modButton.Margin = new Thickness(10, 10, 30, 10);
            //        modButton.Height = 40;
            //        modButton.Width = 60;

            //        ModCollection.Add(modButton);
            //       // modButtonsCodeBehind.Children.Add(new Button() { Name = "new button" });

            //        //MyCollection.Add(new Button() { Name = "btn" + item.itemName.ToString(), Content = item.itemName.ToString(), Margin = new Thickness(5, 5, 5, 5), Height = 25, Width = 5 });
            //        //itemButton = null;
            //    }
            //}

        private void ModButton_Click(object sender, RoutedEventArgs e)
        {
            // new Button named clicked, given value of sender
            Button clicked = new Button();
            clicked = sender as Button; // is this necessary or should I use sender.Name etc !?  Answered: yes, sender.Name is not available but clicked.Name is. sender is generic obj, clicked is Button

            string modName = clicked.Content.ToString();
            mod_name += modName;


            // extract modifier name from Button.Content
            // use it to find the appropriate modifier code
            int code = 10;
            foreach (Modifier md in oc.getMods()
                .Where(mo => mo.ModName == modName)) {
                code = md.ModCode;
            }

            if (code != 10) {
                Modifier modd = new Modifier(mod_name, code);
                oc.AddModtoSelectedItem(modd);

                // Since I haven't been able to change the XAML to reflect the modifiers as well as the Menu items themselves in the OrderListControl, 
                // I create a new "menuItem" with a bogus categoryCode, and price 0.00
                // Here, I add the bogus Modifier-MenuItem to the OrderList.
                MenuItem modItem = new MenuItem(mod_name, 5, 0m);
                oc.AddModtoOrder(modItem);
            } 

            // clear the mod_name property and hide Mod Control
            mod_name = "";
            oc.ShowMod = false;
            oc.LastEventWasMod = true;


        }

        // past attempts
        // past attemts logic:
        // Here the list of all modifiers and the OrderController instance, are passed 
        // as parameters. This allows for the use of a two way binding between this
        // user control and the OrderController. <= note: not true!  When an instance of ModifiersControl 
        // is implemented, the whichWindow value dictates which set of modifiers is
        // displayed. When the categoryCode is 0, the sandwiches modifiers are displayed,
        // when the categoryCode is 1 drinks modifiers are displayed.
        // On buttons No, Sub, Add, and Allergy OnClick events, the word "no/add" will be
        // added to the a local class property ModName, to be added to the ModName field
        // during the OnClick event of the modButton_Click, via the 
        // oc.SelectedMenuItem.AddtoModifier(mod_name, mod_code)  !? bad phrasing
        //private void ItemButton_Click(object sender, RoutedEventArgs e)
        //{


        // code came from whichWindow? apprently
        //    //switch (code) {
        //    //    case 0:
        //    //        selectedItem = o.getSandwich(modName);
        //    //        break;
        //    //    case 1:
        //    //        selectedItem = o.getDrink(modName);
        //    //        break;
        //    //    default:
        //    //        selectedItem = null;
        //    //        Console.WriteLine("catCode not extracted from Modifiers Button properly. Thrown from general Item_button Click.");
        //    //        break;
        //    //}

        //    if (selectedItem != null) {
        //        selectedItem.AddtoModList()
        //        oc.SelectedMenuItem = selectedItem;
        //        oc.orderItems.Add(selectedItem);

        //    }


        //Guid g = Guid.NewGuid();
        //ApplicationState.SetValue(g, clicked);
        //getItem based on name and then add it to oController.OrderList (add new MenuItem)
        //AppState.AddtoList(clicked);
        //}


        // When the "NO" button is clicked, the "name" of the button should be extracted from the 
        // button.Name property. 
        // This value is then added to the mod_name property, for later use upon the ModButton_Click event
        // The _Click events below are identical, and should be combined into a general _Click event 





        /*ModActionButtons:
         These buttons add the text extracted from the clicked button to the mod_name local class variable.
         This variable is then used to create a new modifier, above in ModButton_Click. This was 
         a sort of work around, as I wanted the qualifiers "no, add, sub" to be part of the modifier
         itself, so they could be displayed along with the MenuItem in the "OrderListContol" panel.*/

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            mod_name = ""; // clears previous text within the mod_name. This is to prevent duplicate "no no no" or "no sub add" text 
            Button clicked = new Button();
            clicked = sender as Button;

            string cmd = clicked.Content.ToString() + " ";
            mod_name = cmd;
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            mod_name = "";
            Button clicked = new Button();
            clicked = sender as Button;

            string cmd = clicked.Content.ToString() +" ";
            mod_name = cmd;
        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            mod_name = "";
            Button clicked = new Button();
            clicked = sender as Button;

            //string name = clicked.Name.Substring(3, clicked.Name.Length - 3);            // gets string from Button.Name, buttons were named intentionally to allow for this function // removed!
            string cmd = clicked.Content.ToString() + " ";
            mod_name = cmd;
        }

        private void btnAllergy_Click(object sender, RoutedEventArgs e)
        {
            // the mod_name clear here and in the "no" click could be altered, so that a combination of the NO and !Allergy! could be possible. 
            mod_name = "";
            Button clicked = new Button();
            clicked = sender as Button;

            //string name = clicked.Name.Substring(3, clicked.Name.Length - 3);            // gets string from Button.Name, buttons were named intentionally to allow for this function
            string cmd = clicked.Content.ToString() + " ";
            mod_name = cmd;
        }



    }
}
