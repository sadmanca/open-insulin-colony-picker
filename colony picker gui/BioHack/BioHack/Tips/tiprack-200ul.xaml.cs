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
    /// Interaction logic for TipRack200ul.xaml
    /// </summary>
    public partial class TipRack200ul : UserControl
    {

        public static LabwareEnums labwareEnum = LabwareEnums.tipone_96_tiprack_200ul;


        public TipRack200ul()
        {
            InitializeComponent();
        }
    }
}
