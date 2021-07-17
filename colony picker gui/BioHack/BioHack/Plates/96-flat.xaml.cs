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

namespace BioHack.Controls
{
    /// <summary>
    /// Interaction logic for Flat96.xaml
    /// </summary>
    public partial class Flat96 : UserControl
    {

        public static LabwareEnums labwareEnum = LabwareEnums.corning_96_wellplate_360ul_flat;

        public Flat96()
        {
            InitializeComponent();
        }
    }
}
