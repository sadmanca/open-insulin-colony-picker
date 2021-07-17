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
    /// Interaction logic for PCRtall96.xaml
    /// </summary>
    public partial class Plate384 : UserControl
    {
        public static LabwareEnums labwareEnum = LabwareEnums.corning_384_wellplate_112ul_flat;

        public Plate384()
        {
            InitializeComponent();
        }
    }
}
