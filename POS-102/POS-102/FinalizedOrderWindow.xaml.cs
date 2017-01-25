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
using System.Windows.Shapes;

namespace POS_102
{
    /// <summary>
    /// Interaction logic for FinalizedOrderWindow.xaml
    /// </summary>
    public partial class FinalizedOrderWindow : Window
    {
        private static OrderController oc;

        public FinalizedOrderWindow()
        {
            InitializeComponent();
        }

        public FinalizedOrderWindow(OrderController oController)
        {
            InitializeComponent();
            oc = oController;
        }

    }
}
