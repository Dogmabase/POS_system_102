using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_102
{
    /// <summary>
    /// The Modifier class is the model for modifiers data, which are 
    /// used to make changed to MenuItem objects, and are stored within 
    /// MenuItem objects as a list. 
    /// 
    /// Modifiers have names and codes, the code corresponds to the
    /// catCode (category code) of the MenuItems. 0 for sandwich, 1 for drink, 2 for dessert.
    /// </summary>
    public class Modifier
    {
        private string _modName;
        private int _modCode;

        public Modifier()        {        }

        public Modifier(string name, int code)
        {
            ModName = name;
            ModCode = code;
        }

        public string ModName {
            get { return _modName; }
            set {
                if (_modName != value)
                    _modName = value;
            }
        }

        public int ModCode {
            get { return _modCode; }
            set {
                if (_modCode != value)
                    _modCode = value;
            }
        }


    }
}
