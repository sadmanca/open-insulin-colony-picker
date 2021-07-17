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
    /// Interaction logic for TubeRack15_50ML.xaml
    /// </summary>
    public partial class TubeRack15_50ML : UserControl
    {
        public static LabwareEnums labwareEnum = LabwareEnums.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic;


        public TubeRack15_50ML()
        {
            InitializeComponent();
        }
    }
}
