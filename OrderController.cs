using POS_102.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/* Caitlin Boake - POS system 102
 */

    /// <summary>
    /// This is the controller for this project. 
    /// It holds the current states of the Order and selectedItem objects. 
    /// 
    /// The buttons displayed in the ItemDisplayControl are populated based on which 
    /// button of the MenuControl was clicked, though beverages are displayed on start.
    /// 
    /// Modifiers are displayed in the same area as ItemDisplayControl
    /// </summary>

namespace POS_102
{
    public class OrderController : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private static DataSource _dataSource; // has instance in MainWindow.cs
        private MenuItem _selectedItem; // exists here only, is the currently selectedItem 
        private Order _currentOrder;
        private int _currentWindow; // used to update button controls

        private bool _showMod; // value used to determine whether modifier buttons should be shown or not

        private string _itemNameRegister; // not really necesary, but allows for parameterless call to AddtoOrder()
        private decimal _finalPrice;

        // The public version of this is orderItems, which is the list that is used to bind items selected to the OrderList in the OrderListControl 
        // This is not the ideal ObservableCollection 
        private ObservableCollection<MenuItem> _orderItems; //list of MenuItems within the order, actually is duplicate to the list within Order
        


        private ObservableCollection<ButtonContent> _buttons; // menuItem buttons are given data via the ButtonContent model, though I could have easily used the MenuItem.itemName. Somehow the use of another model with only one property seemed more efficient.
        public ObservableCollection<ButtonContent> buttons { get { return _buttons; } } 


        private ObservableCollection<Modifier> _modifiersButtons; // mod buttons are given data via the Modifier model.

        private List<string> _breadList;
        public List<string> BreadList { get { return _breadList; } }

        public bool bringWater;
        public bool LastEventWasMod { get; set; } // used to determine whether the last even was a modifier

        public List<string> finalOrder; // final order is sloppily put into a List of strings.
        public decimal finalPrice { get { return _finalPrice; } }



        // used to update Modifier List within MenuItem objects
        public event PropertyChangedEventHandler PropertyChanged;
        // invoked in an attempt to update the OrderListControl view upon change to individual MenuItems via Modifiers
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            PopulateButtons(); // unsure of start up process, but apparently this method is called once on startup. As such, I was getting duplicate buttons the duplicates are gone now.
            PopulateModifiers();
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // alternate suggested solution
        }

       
        // CONSTRUCTOR
        public OrderController(DataSource source)
        {
            // INIT vars and objects
            _dataSource = source;
            _orderItems = new ObservableCollection<MenuItem>();
            _buttons = new ObservableCollection<ButtonContent>();
            _modifiersButtons = new ObservableCollection<Modifier>();
            _breadList = new List<string>();

            _currentOrder = new Order(source); // initialize order object

            _selectedItem = null; // no item is selected

            // item button defaults set, and buttons displayed
            whichWindow = 1; // variable that determines which buttons should be displayed. 1 is drinks, 0 is sandwiches, etc
            PopulateButtons();

            // modifier defaults set, and variables initialized
            PopulateModifiers();
            _showMod = false;
            LastEventWasMod = false;
        }


        /// <summary>
        /// Clears button data, and gives value to the 
        /// _buttons ObservableCollection of type ButtonContent, based
        /// off the value of the whichWindow variable.
        /// </summary>
        public void PopulateButtons()
        {
            // clear buttons and data from ItemDisplayControl object
            ItemDisplayControl itemControl = new ItemDisplayControl(); // technically this should be static, and give value from the object existing in the MainWindow already
            itemControl.DataContext = null;
            itemControl.innerBtnGrid.Children.Clear();
            itemControl.DataContext = this;
            _buttons.Clear();

            // list of current items to be displayed
            List<MenuItem> items = new List<MenuItem>();

            // checks which window is selected, 1/drinks by default.
            // _buttons is a Observable collection of type ButtonContent, which holds only the string value of the ItemName as a property.
            switch (whichWindow) {
                case 0:
                    items = _currentOrder.getSandwichList();
                    foreach (MenuItem s in items) {
                        _buttons.Add(new ButtonContent(s.itemName));
                    }
                    break;
                case 1:
                    items = _currentOrder.getDrinkList();
                    foreach (MenuItem s in items) {
                        _buttons.Add(new ButtonContent(s.itemName));
                    }
                    break;
                case 2:
                    items = _currentOrder.getDessertList();
                    foreach (MenuItem s in items) {
                        _buttons.Add(new ButtonContent(s.itemName));
                    }
                    break;
            }

        }

        /// <summary>
        ///  Clears button data, and gives value to the
        /// _modifiersButtons ObservableCollection of type Modifier, based
        /// off the value of the category code within the _selectedItem object.
        /// </summary>
        public void PopulateModifiers()
        {
            // initialize vars and clear button data 
            ModifiersControl modControl = new ModifiersControl();
            modControl.DataContext = null;
            modControl.innerModGrid.Children.Clear();
            modControl.DataContext = this;
            _modifiersButtons.Clear();

            // if no item is currently selected, show all possible modifiers
            if (_selectedItem == null) {
                foreach (Modifier m in _dataSource.getMods()) {
                    _modifiersButtons.Add(m);
                }
            }            
            else {

                // if a sandwiche is currently selected, show only sandwich modifiers
                if (_selectedItem.catCode == 0) {
                    foreach (Modifier mod in _dataSource.getMods()
                        .Where(m => m.ModCode == 0)) {
                        _modifiersButtons.Add(mod);
                    }
                }
                // if a drink is currently selected, show only drink modifiers
                else if (_selectedItem.catCode == 1) {
                    foreach (Modifier mod in _dataSource.getMods()
                        .Where(m => (m.ModCode == 1))) {
                        _modifiersButtons.Add(mod);
                    }
                }
                // if a dessert is currently selected, no modifiers currently exist, but could easily be given 
                // value and displayed if modFile of DataSource is altered.
            }
        }


        // whichWindow enables the functionality of the variable ButtonPopulate method
        // When this variable is changed, OnPropertyChanged method is triggered
        public int whichWindow {
            get { return _currentWindow; }
            set {
                if (_currentWindow != value)
                    _currentWindow = value;
                OnPropertyChanged("whichWindow");
            }
        }

        // SelectedMenuItem is the public version of the _selectedItem object variable. 
        // This object is given value based on the most recent menuItem selected, and is a MenuItem type.
        // When this value is changed, OnPropertyChanged method is invoked.
        public MenuItem SelectedMenuItem {
            get { return _selectedItem; }
            set {
                if (_selectedItem != value) {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                    //PopulateModifiers(); // unsure if this call is necessary
                }
            }
        }

        // orderItem observable collection. This only has a get method, as 
        // the method AddtoOrder acts as a set for htis variable. 
        public ObservableCollection<MenuItem> orderItems {
            get { return _orderItems; }
            //set { // changed to get only, 
            //    //AddtoOrder(); // added AddOrderName_toRegister method to allow for AddtoOrder to be called here, but now i'm not sure if that's the solution to updating items in _orderItems after an item as been "modified"
            //    OnPropertyChanged("orderItems");
            //}
        }

        /// <summary>
        ///  This method is invoked when a button in the ItemDisplayControl is pressed. That is, 
        /// a sandwich, drink, or dessert is pressed and thus added to the order.
        /// the name is added to a string field, so as to allow for AddtoOrder to be called without
        /// direct knowledge of which button was pressed, aka a parameterless call.
        /// </summary>
        /// <param name="name"></param>
        public void AddOrderName_toRegister(string name)
        {
            _itemNameRegister = name;
        }


        /// <summary>
        /// This method is invoked after a call to the AddOrderName_toRegister method,
        /// which is actually unnecessary but allows for greater accessability to this 
        /// method, because direct knowledge of which button was pressed is not necessary.
        /// 
        /// This method adds to the _orderItems ObservableCollection of MenuItem, 
        /// and to the List of MenuItem object that lives inside the local Order object,
        /// via the Order method .AddtoOrder.
        /// </summary>
        public void AddtoOrder()
        {
            _selectedItem = _currentOrder.getDrink(_itemNameRegister);

            if (_selectedItem == null) {
                _selectedItem = _currentOrder.getSandwich(_itemNameRegister);
            }

            if (_selectedItem == null) {
                Console.WriteLine("Error in OrderController, AddtoOrder.");
            }
            else {
                _orderItems.Add(_selectedItem);
                _currentOrder.AddtoOrder(_selectedItem);
            }
        }

        /// <summary>
        /// This method is invoked on the undo button click event, within the FooterControl.
        /// This method removes both modifiers and menuItems from both 
        /// the local ObservableCollection of MenuItem _orderItems, and the local Order object's 
        /// property _order, which is a List of MenuItem exisiting in the _currentOrder object.
        /// </summary>
        public void DeleteLastinOrder()
        {
            _orderItems.RemoveAt(_orderItems.Count - 1);
            _currentOrder.RemoveLastItem();
        }

        /// <summary>
        ///  This adds a modifier masquerading as a MenuItem 
        ///  to the ObservableCollection of MenuItem called _orderItems.
        ///  h
        /// </summary>
        /// <param name="modItem"></param>
        public void AddModtoOrder(MenuItem modItem)
        {
            if (modItem != null) {
                _orderItems.Add(modItem);
                
            }
        }

        /// <summary>
        /// This Method adds a modifier to the currentlySelectedItem 
        /// </summary>
        /// <param name="addMod"></param>
        public void AddModtoSelectedItem(Modifier addMod)
        {
            _selectedItem.AddMod_toModRegistry(addMod);
            _selectedItem.AddtoModList();
            // delete last item and then re-add updated item to _orderItems

            //_orderItems.RemoveAt(_orderItems.Count() - 1);
            //_orderItems.Add(_selectedItem);       // was unnecessary! and was creating bugs too
        }

        // removes last item from the ModList within the selectedItem menuItem object
        public void RemovefromModList()
        {
            _selectedItem.RemoveLast_fromModList();
        }

        // bound to ModifiersControl, with help of a converter, this triggers the visibility of the modifier buttons
        public bool ShowMod {
            get { return _showMod; }
            set {
                if (_showMod != value) {
                    _showMod = value;
                    OnPropertyChanged("ShowMod");
                }

            }
        }
       
        // not used, intended use in FinalizeOrder method below
        public ObservableCollection<Modifier> modifiersButtons {
            get { return _modifiersButtons; }
            set {
                OnPropertyChanged("modifiers");
            }
        }

        // not used, intended use in FinalizeOrder method below
        public List<string> breadList {
            get { return _breadList; }
        }

        // adds bread to list on CheckedEvent in MainWindow
        public void addBread(string bread)
        {
            if (!(_breadList.Contains(bread))) {
                _breadList.Add(bread);
            }
        }

        // removes bread from list on UnCkeckedEvent in MainWindow
        public void removeBread(string bread)
        {
            _breadList.Remove(bread);
        }

        public Order currentOrder {
            get { return _currentOrder; }
        }

        public List<Modifier> getMods()
        {
            return _dataSource.getMods();
        }


        // this is a poor way to gather all data from relevant OrderController lists, but it does the trick.
        // To properly display the contents of the currentOrder.order list, including the Modifiers within the 
        // menuItems, I would need to change my binding to the OrderListControl as well as changed the propertyChanged events
        // to include an update List Function, or perhaps have a delegate within the MenuItem and within the Order to push the
        // changes up through the MenuItem to the Order to the OrderController. 
        public void FinalizeOrder()
        {
            _finalPrice = 0;
            
            
            //finalOrder = new List<string>();
            string finalOrderString = "";

            if (bringWater) {
                //finalOrder.Add("Bring Water");
                finalOrderString += "Bring Water \n";
            }

            finalOrderString += "        --         \n"; // for formatting

            foreach (string s in _breadList) {
                //finalOrder.Add(s);
                finalOrderString += s + " \n";
            }

            finalOrderString += "        --         \n";

            foreach (MenuItem m in _orderItems) {
                _finalPrice += m.itemPrice;

                //finalOrder.Add(m.itemName + "\n"); // would go and check for item code to get rid of those fake modifiers displayed in the OrderListControl. but ...
                //foreach (Modifier mod in m.modList) {
                //    finalOrder.Add(mod.ModName);
                //}

                finalOrderString += m.itemName + " \n ";
                foreach (Modifier mod in m.modList) {
                    finalOrderString += mod.ModName + " within item! \n"; // causes duplicate, but here to show that the modifiers really are actully in the MenuItem
                }
            }

            MessageBox.Show("-------------Order---------------\n" + _finalPrice.ToString()  + " \n" + finalOrderString);

            //FinalizedOrderWindow finalWindow = new FinalizedOrderWindow();
            //finalWindow.Show();

        }




    }
}
