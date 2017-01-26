using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class is the template for the Order object.
/// 
/// It holds data for both the full menu, full list of modifiers,
/// and the current list of MenuItems in the order.
/// 
/// The Order constructor takes a DataSource obejct as a parameter, this is to enable it to 
/// populate the menu and mods lists.
/// 
/// Functions of this class allow for retrieval of the menu and mods lists, as well
/// as the items in the order currently. Some basic CRUD functionality is written here.
/// </summary>

namespace POS_102
{
    public class Order
    {
        private List<MenuItem> _menu;
        private List<Modifier> _modifiers;
        private List<MenuItem> _order;
        private ObservableCollection<MenuItem> _ocOrder;
        public string message;
            
        public ObservableCollection<MenuItem> order {
            get {
                foreach (MenuItem item in _order) {
                    _ocOrder.Add(item);
                }
                return _ocOrder;
            }
        }

        public Order(DataSource source)
        {
            _menu = new List<MenuItem>();
            _menu = source.getMenu();

            _modifiers = new List<Modifier>();
            _modifiers = source.getMods();

            _order = new List<MenuItem>();
        }

        // moved to dataSource
        //public void ReadFile(string fileName)
        //{
        //    try {
        //        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\owner\Documents\A_WinProg\POS-102\POS-102\" + fileName);
        //        Console.WriteLine("Contents of file: ");
        //        foreach (string line in lines) {
        //            Console.WriteLine("\t" + line);
        //            var result = line.Split(',');
        //            foreach (var r in result) {
        //                Console.WriteLine("r: " + r);
        //                _menu.Add(new MenuItem(r[0].ToString(),r[1],r[2]));
        //            }
        //        }
        //    } // end try
        //    catch (Exception ex) {
        //        Console.WriteLine("Error at DataSource.ReadFile");
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        public List<MenuItem> getSandwichList()
        {
            List<MenuItem> sandwiches = new List<MenuItem>();
            foreach (MenuItem item in _menu) {
                if (item.catCode == 0) {
                    sandwiches.Add(item);
                }
            }
            return sandwiches;
        }

        public List<MenuItem> getDrinkList()
        {
            List<MenuItem> drinks = new List<MenuItem>();
            foreach (MenuItem item in _menu) {
                if (item.catCode == 1) {
                    drinks.Add(item);
                }
            }
            return drinks;
        }

        public List<MenuItem> getDessertList()
        {
            List<MenuItem> dessert = new List<MenuItem>();
            foreach (MenuItem item in _menu) {
                if (item.catCode == 2) {
                    dessert.Add(item);
                }
            }
            return dessert;
        }

        public MenuItem getDessert(string name)
        {
            foreach (MenuItem item in _menu) {
                if (item.itemName == name) {
                    return item;
                }

            }
            return null;
        }

        public MenuItem getSandwich(string name)
        {
            foreach(MenuItem item in _menu) {
                if (item.itemName == name) {
                    return item;
                }

            }
            return null;
        }

        public MenuItem getDrink(string name)
        {
            foreach (MenuItem item in _menu) {
                if (item.itemName == name) {
                    return item;
                }
            }
            return null;
        }

        public List<MenuItem> getMenu()
        {
            return _menu;
        }

        public List<Modifier> getMods()
        {
            return _modifiers;
        }

        public void AddtoOrder(MenuItem item)
        {
            _order.Add(item);
        }

        public List<MenuItem> getOrder()
        {
            return _order;
        }

        public void ClearOrder()
        {
            if (_order.Count != 0) {
                _order.Clear();
            }
        }

        public void RemoveLastItem()
        {
            int count = _order.Count();
            _order.RemoveAt(count -1); 
        }

    }
}
