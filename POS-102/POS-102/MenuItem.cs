using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_102
{

    /// <summary>
    /// The MenuItem class is a template for the MenuItem objects, and maintains a list
    /// of modifiers that lives within the object. 
    /// 
    /// Each MenuItem has a itemName, an itemPrice, and a category code catCode
    /// which determines to which group an item belongs. 0 sandwich, 1 drink, 2 dessert.
    /// 
    /// The _modifiers is a List of type Modifier, and holds the list of modifiers
    /// which have been applied to each MenuItem individually. 
    /// 
    /// This implements INotifyPropertyChanged, in an attempt to allow for real time changes to be made
    /// to the OrderListControl ItemsControl template. However, the OnPropertyChanged event is too low to 
    /// be able to access that Control, and I am not sure whether this event is useful for the purpose I desired.
    /// 
    /// </summary>
    /// 
    public class MenuItem : INotifyPropertyChanged  
    {
        private string _itemName;
        private decimal _itemPrice;
        private int _catcode;
        private List<Modifier> _modifiers = new List<Modifier>(); // !?
        private Modifier mod;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MenuItem()        {     }

        public MenuItem(string name, int code, decimal price) // List<Modifier> mList or Modifier md
        {
            //Modifier m = new Modifier();
            //_modifiers = new List<Modifier>();
            //_modifiers = mList;

            itemName = name;
            itemPrice = price;
            catCode = code;
        }

        public string itemName {
            get { return _itemName; }
            set {
                if (_itemName != value)
                    _itemName = value;
                OnPropertyChanged("itemName");
            }
        }

        public decimal itemPrice {
            get { return _itemPrice; }
            set {
                if (_itemPrice != value)
                    _itemPrice = value;
                OnPropertyChanged("itemPrice");
            }
        }
        public int catCode {
            get { return _catcode; }
            set {
                if (_catcode != value)
                    _catcode = value;
                OnPropertyChanged("catCode");
            }
        }

        public void AddMod_toModRegistry(Modifier m)
        {
            mod = m;
        }

        public void AddtoModList()
        {
            if (mod != null && !_modifiers.Contains(mod)){
                _modifiers.Add(mod);
                OnPropertyChanged("modList");
            }
            mod = null;
        }

        public void RemoveLast_fromModList()
        {
            if (_modifiers.Count != 0) {
                _modifiers.RemoveAt(_modifiers.Count - 1);
            }
        }

        public List<Modifier> modList {
            get { return _modifiers; }
            
        }

    }
}
