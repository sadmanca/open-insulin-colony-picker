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
    /// Interaction logic for TubeRack1_5ML.xaml
    /// </summary>
    public partial class TubeRack1_5ML : UserControl
    {
        public static LabwareEnums labwareEnum = LabwareEnums.opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap;


        public TubeRack1_5ML()
        {
            InitializeComponent();
        }
    }
}
