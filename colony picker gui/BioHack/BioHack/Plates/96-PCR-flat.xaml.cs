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
    /// Interaction logic for PCR96.xaml
    /// </summary>
    public partial class PCR96 : UserControl
    {
        public static LabwareEnums labwareEnum = LabwareEnums.biorad_96_wellplate_200ul_pcr;



        public PCR96()
        {
            InitializeComponent();
        }
    }
}
