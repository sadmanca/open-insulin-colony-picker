using BioHack.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace BioHack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Dictionary<LabwareEnums, string> Strings = Labwares.GetSDictionary();

        private Dictionary<string, LabwareEnums> Enums = Labwares.GetEDictionary();

        private RootObject protocol;
        private User user;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UserBox.ItemsSource = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Users.json"));

            ProtocolList.ItemsSource = Directory.GetFiles("Protocols", "*.py").Select(x => new { Name = x.Replace(".py", "").Replace("Protocols\\", ""), Path = x, });
        }

        private async void ProtocolList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (ProtocolList.SelectedItem != null)
                {
                    var jsonPath = ((string)(((dynamic)ProtocolList.SelectedItem).Path)).Replace(".py", ".json");

                    if (File.Exists(jsonPath))
                    {
                        ResetGui();

                        codeEditor.LoadDataFromFile(((dynamic)ProtocolList.SelectedItem).Path);

                        // read file into a string and deserialize JSON to a type
                        protocol = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(jsonPath));

                        AmountBox.Text = protocol.Amount.ToString();

                        LabwareList.ItemsSource = protocol.Labware;


                        LiquidList.ItemsSource = protocol.Liquids.Select(x => new Liquid() { name = x.name, Slot = x.Slot, Labware = protocol.Labware.ElementAtOrDefault(Convert.ToInt32(x.Labware)).name });

                        foreach (var item in protocol.Labware)
                        {
                            SetSlot(item);
                        }

                    }
                    else
                    {
                        await this.ShowMessageAsync("Warning", "The configuration file is missing!");
                    }
                }

                message.Text = (string)(((dynamic)ProtocolList.SelectedItem).Name) + " was loaded successfully!";

            }
            catch (Exception ex)
            {
                message.Text = "Failed to load protocol " + (string)(((dynamic)ProtocolList.SelectedItem).Name);

                ResetGui();

            }
        }

        private void SetSlot(Labware item)
        {
            var labwareEnum = Enums[item.labware];
            switch (labwareEnum)
            {
                case LabwareEnums.corning_6_wellplate_16_8ml_flat:
                    break;
                case LabwareEnums.corning_12_wellplate_6_9ml_flat:
                    break;
                case LabwareEnums.corning_24_wellplate_3_4ml_flat:
                    break;
                case LabwareEnums.corning_48_wellplate_1_6ml_flat:
                    break;
                case LabwareEnums.corning_384_wellplate_112ul_flat:
                    SetSlot(item.Slot, new Plate384());
                    break;
                case LabwareEnums.usascientific_96_wellplate_2_4ml_deep:
                    SetSlot(item.Slot, new DeepWell96());
                    break;
                case LabwareEnums.corning_96_wellplate_360ul_flat:
                    SetSlot(item.Slot, new Flat96());
                    break;
                case LabwareEnums.biorad_96_wellplate_200ul_pcr:
                    SetSlot(item.Slot, new PCR96());
                    break;
                case LabwareEnums.opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0_2ml_pcr_strip:
                    break;
                case LabwareEnums.opentrons_24_aluminumblock_generic_2ml_screwcap:
                    break;
                case LabwareEnums.opentrons_96_aluminumblock_biorad_wellplate_200ul:
                    break;
                case LabwareEnums.opentrons_96_aluminumblock_generic_pcr_strip_200ul:
                    break;
                case LabwareEnums.opentrons_96_tiprack_300ul:
                    SetSlot(item.Slot, new TipRack300ul());
                    break;
                case LabwareEnums.opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap:
                    SetSlot(item.Slot, new TubeRack1_5ML());
                    break;
                case LabwareEnums.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical:
                    break;
                case LabwareEnums.opentrons_15_tuberack_falcon_15ml_conical:
                    SetSlot(item.Slot, new TubeRack15ML());
                    break;
                case LabwareEnums.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap:
                    SetSlot(item.Slot, new TubeRack2ML());
                    break;
                case LabwareEnums.opentrons_24_tuberack_generic_2ml_screwcap:
                    break;
                case LabwareEnums.opentrons_6_tuberack_falcon_50ml_conical:
                    SetSlot(item.Slot, new TubeRack50ML());
                    break;
                case LabwareEnums.opentrons_96_tiprack_10ul:
                    SetSlot(item.Slot, new TipRack10ul());
                    break;
                case LabwareEnums.tipone_96_tiprack_200ul:
                    SetSlot(item.Slot, new TipRack200ul());
                    break;
                case LabwareEnums.opentrons_96_tiprack_1000ul:
                    SetSlot(item.Slot, new TipRack1000ul());
                    break;
                case LabwareEnums.agilent_1_reservoir_290ml:
                    break;
                case LabwareEnums.usascientific_12_reservoir_22ml:
                    SetSlot(item.Slot, new Trough12Row());
                    break;
                case LabwareEnums.opentrons_24_tuberack_generic_0_75ml_snapcap_acrylic:
                    SetSlot(item.Slot, new TubeRack075());
                    break;
                case LabwareEnums.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic:
                    SetSlot(item.Slot, new TubeRack15_2ML());
                    break;
                case LabwareEnums.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic:
                    SetSlot(item.Slot, new TubeRack15_50ML());
                    break;
                default:
                    break;
            }
        }

        private void ResetGui()
        {
            AmountBox.Text = null;
            LabwareList.ItemsSource = null;
            LiquidList.ItemsSource = null;
            codeEditor.TextArea.Text = null;

            test.Text = "";

            ResetDeck();
        }

        private void ResetDeck()
        {
            ResetSlot(Slot1, 1);
            ResetSlot(Slot2, 2);
            ResetSlot(Slot3, 3);
            ResetSlot(Slot4, 4);
            ResetSlot(Slot5, 5);
            ResetSlot(Slot6, 6);
            ResetSlot(Slot7, 7);
            ResetSlot(Slot8, 8);
            ResetSlot(Slot9, 9);
            ResetSlot(Slot10, 10);
            ResetSlot(Slot11, 11);
        }

        private void ResetSlot(Grid slot, int content)
        {
            slot.Children.Clear();
            slot.Children.Add(new Slot() { Content = content });
        }

        private void SetSlot(int num, UIElement content)
        {
            Grid slot;

            switch (num)
            {
                case 1:
                    slot = Slot1;
                    break;
                case 2:
                    slot = Slot2;
                    break;
                case 3:
                    slot = Slot3;
                    break;
                case 4:
                    slot = Slot4;
                    break;
                case 5:
                    slot = Slot5;
                    break;
                case 6:
                    slot = Slot6;
                    break;
                case 7:
                    slot = Slot7;
                    break;
                case 8:
                    slot = Slot8;
                    break;
                case 9:
                    slot = Slot9;
                    break;
                case 10:
                    slot = Slot10;
                    break;
                case 11:
                    slot = Slot11;
                    break;
                default:
                    return;
            }

            slot.Children.Clear();
            slot.Children.Add(content);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetGui();

            message.Text = "Ready ...";

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (UserBox.SelectedItem != null)
            {
                var login = (User)UserBox.SelectedItem;

                if (login.Password == Security.EncryptText(PasswordBox.Password))
                {
                    LoginGrid.Visibility = Visibility.Collapsed;
                    user = login;
                    this.Title = "GUIde User \"" + user.Name + "\"";
                    message.Text = "Welcome back " + user.Name + " ...";
                }
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            //opentrons_simulate

            var path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User).Split(new char[] { ';' });

            if (path.Count(x => x.EndsWith("\\Python36\\")) > 0)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = path.FirstOrDefault(x => x.EndsWith("\\Python36\\")) + @"python.exe";
                start.Arguments = " Run.py";
                start.WorkingDirectory = "temp";
                start.UseShellExecute = false;// Do not use OS shell
                start.CreateNoWindow = true; // We don't need new window
                start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
                start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                        string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    }
                }

                message.Text = "Protocol Exited with no Errors ...";

                this.Dispatcher.InvokeAsync(() =>
                {
                    test.Text = "";
                    var lines = File.ReadLines("temp\\Results.txt");
                    foreach (var line in lines)
                    {
                        test.Text += line + "\n";
                    }
                });
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ProtocolList.ItemsSource = Directory.GetFiles("Protocols", "*.py").Select(x => new { Name = x.Replace(".py", "").Replace("Protocols\\", ""), Path = x, });
        }
    }
}
