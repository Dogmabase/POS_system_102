using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_102
{
    public class ButtonContent
    {
        private string _Content;

        public ButtonContent(string content)
        {
            Content = content;
        }

        public string Content {
            get { return _Content; }
            set {
                if (_Content != value)
                    _Content = value;
            }
        }
    }
}
