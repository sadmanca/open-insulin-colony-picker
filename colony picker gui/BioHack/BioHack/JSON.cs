using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioHack
{
    public class Labware
    {
        public string name { get; set; }
        public string labware { get; set; }
        public int Slot { get; set; }
    }

    public class Liquid
    {
        public string name { get; set; }
        public string Labware { get; set; }
        public string Slot { get; set; }
    }

    public class RootObject
    {
        public int Amount { get; set; }
        public List<Labware> Labware { get; set; }
        public List<Liquid> Liquids { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class Event
    {
        public string Action { get; set; }
        public string User { get; set; }
        public string Time { get; set; }
    }

    public static class Labwares
    {
        public static string corning_6_wellplate_16_8ml_flat = "corning_6_wellplate_16.8ml_flat";
        public static string corning_12_wellplate_6_9ml_flat = "corning_12_wellplate_6.9ml_flat";
        public static string corning_24_wellplate_3_4ml_flat = "corning_24_wellplate_3.4ml_flat";
        public static string corning_48_wellplate_1_6ml_flat = "corning_48_wellplate_1.6ml_flat";
        public static string corning_384_wellplate_112ul_flat = "corning_384_wellplate_112ul_flat";
        public static string usascientific_96_wellplate_2_4ml_deep = "usascientific_96_wellplate_2.4ml_deep";
        public static string corning_96_wellplate_360ul_flat = "corning_96_wellplate_360ul_flat";
        public static string biorad_96_wellplate_200ul_pcr = "biorad_96_wellplate_200ul_pcr";
        public static string opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0_2ml_pcr_strip = "opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0.2ml_pcr_strip";
        public static string opentrons_24_aluminumblock_generic_2ml_screwcap = "opentrons_24_aluminumblock_generic_2ml_screwcap";
        public static string opentrons_96_aluminumblock_biorad_wellplate_200ul = "opentrons_96_aluminumblock_biorad_wellplate_200ul";
        public static string opentrons_96_aluminumblock_generic_pcr_strip_200ul = "opentrons_96_aluminumblock_generic_pcr_strip_200ul";
        public static string opentrons_96_tiprack_300ul = "opentrons_96_tiprack_300ul";
        public static string opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap = "opentrons_24_tuberack_eppendorf_1.5ml_safelock_snapcap";
        public static string opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical = "opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical";
        public static string opentrons_15_tuberack_falcon_15ml_conical = "opentrons_15_tuberack_falcon_15ml_conical";
        public static string opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap = "opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap";
        public static string opentrons_24_tuberack_generic_2ml_screwcap = "opentrons_24_tuberack_generic_2ml_screwcap";
        public static string opentrons_6_tuberack_falcon_50ml_conical = "opentrons_6_tuberack_falcon_50ml_conical";
        public static string opentrons_96_tiprack_10ul = "opentrons_96_tiprack_10ul";
        public static string tipone_96_tiprack_200ul = "tipone_96_tiprack_200ul";
        public static string opentrons_96_tiprack_1000ul = "opentrons_96_tiprack_1000ul";
        public static string agilent_1_reservoir_290ml = "agilent_1_reservoir_290ml";
        public static string usascientific_12_reservoir_22ml = "usascientific_12_reservoir_22ml";
        public static string opentrons_24_tuberack_generic_0_75ml_snapcap_acrylic = "opentrons_24_tuberack_generic_0.75ml_snapcap_acrylic";
        public static string opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic = "opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic";
        public static string opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic = "opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic";

        public static Dictionary<LabwareEnums, string> GetSDictionary()
        {
            var dic = new Dictionary<LabwareEnums, string>();

            dic.Add(LabwareEnums.corning_6_wellplate_16_8ml_flat, Labwares.corning_6_wellplate_16_8ml_flat);
            dic.Add(LabwareEnums.corning_12_wellplate_6_9ml_flat, Labwares.corning_12_wellplate_6_9ml_flat);
            dic.Add(LabwareEnums.corning_24_wellplate_3_4ml_flat, Labwares.corning_24_wellplate_3_4ml_flat);
            dic.Add(LabwareEnums.corning_48_wellplate_1_6ml_flat, Labwares.corning_48_wellplate_1_6ml_flat);
            dic.Add(LabwareEnums.corning_384_wellplate_112ul_flat, Labwares.corning_384_wellplate_112ul_flat);
            dic.Add(LabwareEnums.usascientific_96_wellplate_2_4ml_deep, Labwares.usascientific_96_wellplate_2_4ml_deep);
            dic.Add(LabwareEnums.corning_96_wellplate_360ul_flat, Labwares.corning_96_wellplate_360ul_flat);
            dic.Add(LabwareEnums.biorad_96_wellplate_200ul_pcr, Labwares.biorad_96_wellplate_200ul_pcr);
            dic.Add(LabwareEnums.opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0_2ml_pcr_strip, Labwares.opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0_2ml_pcr_strip);
            dic.Add(LabwareEnums.opentrons_24_aluminumblock_generic_2ml_screwcap, Labwares.opentrons_24_aluminumblock_generic_2ml_screwcap);
            dic.Add(LabwareEnums.opentrons_96_aluminumblock_biorad_wellplate_200ul, Labwares.opentrons_96_aluminumblock_biorad_wellplate_200ul);
            dic.Add(LabwareEnums.opentrons_96_aluminumblock_generic_pcr_strip_200ul, Labwares.opentrons_96_aluminumblock_generic_pcr_strip_200ul);
            dic.Add(LabwareEnums.opentrons_96_tiprack_300ul, Labwares.opentrons_96_tiprack_300ul);
            dic.Add(LabwareEnums.opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap, Labwares.opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap);
            dic.Add(LabwareEnums.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical, Labwares.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical);
            dic.Add(LabwareEnums.opentrons_15_tuberack_falcon_15ml_conical, Labwares.opentrons_15_tuberack_falcon_15ml_conical);
            dic.Add(LabwareEnums.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap, Labwares.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap);
            dic.Add(LabwareEnums.opentrons_24_tuberack_generic_2ml_screwcap, Labwares.opentrons_24_tuberack_generic_2ml_screwcap);
            dic.Add(LabwareEnums.opentrons_6_tuberack_falcon_50ml_conical, Labwares.opentrons_6_tuberack_falcon_50ml_conical);
            dic.Add(LabwareEnums.opentrons_96_tiprack_10ul, Labwares.opentrons_96_tiprack_10ul);
            dic.Add(LabwareEnums.tipone_96_tiprack_200ul, Labwares.tipone_96_tiprack_200ul);
            dic.Add(LabwareEnums.opentrons_96_tiprack_1000ul, Labwares.opentrons_96_tiprack_1000ul);
            dic.Add(LabwareEnums.agilent_1_reservoir_290ml, Labwares.agilent_1_reservoir_290ml);
            dic.Add(LabwareEnums.usascientific_12_reservoir_22ml, Labwares.usascientific_12_reservoir_22ml);
            dic.Add(LabwareEnums.opentrons_24_tuberack_generic_0_75ml_snapcap_acrylic, Labwares.opentrons_24_tuberack_generic_0_75ml_snapcap_acrylic);
            dic.Add(LabwareEnums.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic, Labwares.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic);
            dic.Add(LabwareEnums.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic, Labwares.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic);

            return dic;
        }

        public static Dictionary<string, LabwareEnums> GetEDictionary()
        {
            var dic = new Dictionary<string, LabwareEnums>();

            dic.Add(Labwares.corning_6_wellplate_16_8ml_flat, LabwareEnums.corning_6_wellplate_16_8ml_flat);
            dic.Add(Labwares.corning_12_wellplate_6_9ml_flat, LabwareEnums.corning_12_wellplate_6_9ml_flat);
            dic.Add(Labwares.corning_24_wellplate_3_4ml_flat, LabwareEnums.corning_24_wellplate_3_4ml_flat);
            dic.Add(Labwares.corning_48_wellplate_1_6ml_flat, LabwareEnums.corning_48_wellplate_1_6ml_flat);
            dic.Add(Labwares.corning_384_wellplate_112ul_flat, LabwareEnums.corning_384_wellplate_112ul_flat);
            dic.Add(Labwares.usascientific_96_wellplate_2_4ml_deep, LabwareEnums.usascientific_96_wellplate_2_4ml_deep);
            dic.Add(Labwares.corning_96_wellplate_360ul_flat, LabwareEnums.corning_96_wellplate_360ul_flat);
            dic.Add(Labwares.biorad_96_wellplate_200ul_pcr, LabwareEnums.biorad_96_wellplate_200ul_pcr);
            dic.Add(Labwares.opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0_2ml_pcr_strip, LabwareEnums.opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0_2ml_pcr_strip);
            dic.Add(Labwares.opentrons_24_aluminumblock_generic_2ml_screwcap, LabwareEnums.opentrons_24_aluminumblock_generic_2ml_screwcap);
            dic.Add(Labwares.opentrons_96_aluminumblock_biorad_wellplate_200ul, LabwareEnums.opentrons_96_aluminumblock_biorad_wellplate_200ul);
            dic.Add(Labwares.opentrons_96_aluminumblock_generic_pcr_strip_200ul, LabwareEnums.opentrons_96_aluminumblock_generic_pcr_strip_200ul);
            dic.Add(Labwares.opentrons_96_tiprack_300ul, LabwareEnums.opentrons_96_tiprack_300ul);
            dic.Add(Labwares.opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap, LabwareEnums.opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap);
            dic.Add(Labwares.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical, LabwareEnums.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical);
            dic.Add(Labwares.opentrons_15_tuberack_falcon_15ml_conical, LabwareEnums.opentrons_15_tuberack_falcon_15ml_conical);
            dic.Add(Labwares.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap, LabwareEnums.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap);
            dic.Add(Labwares.opentrons_24_tuberack_generic_2ml_screwcap, LabwareEnums.opentrons_24_tuberack_generic_2ml_screwcap);
            dic.Add(Labwares.opentrons_6_tuberack_falcon_50ml_conical, LabwareEnums.opentrons_6_tuberack_falcon_50ml_conical);
            dic.Add(Labwares.opentrons_96_tiprack_10ul, LabwareEnums.opentrons_96_tiprack_10ul);
            dic.Add(Labwares.tipone_96_tiprack_200ul, LabwareEnums.tipone_96_tiprack_200ul);
            dic.Add(Labwares.opentrons_96_tiprack_1000ul, LabwareEnums.opentrons_96_tiprack_1000ul);
            dic.Add(Labwares.agilent_1_reservoir_290ml, LabwareEnums.agilent_1_reservoir_290ml);
            dic.Add(Labwares.usascientific_12_reservoir_22ml, LabwareEnums.usascientific_12_reservoir_22ml);
            dic.Add(Labwares.opentrons_24_tuberack_generic_0_75ml_snapcap_acrylic, LabwareEnums.opentrons_24_tuberack_generic_0_75ml_snapcap_acrylic);
            dic.Add(Labwares.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic, LabwareEnums.opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic);
            dic.Add(Labwares.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic, LabwareEnums.opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic);

            return dic;
        }
    }

    public enum LabwareEnums
    {
        corning_6_wellplate_16_8ml_flat,
        corning_12_wellplate_6_9ml_flat,
        corning_24_wellplate_3_4ml_flat,
        corning_48_wellplate_1_6ml_flat,
        corning_384_wellplate_112ul_flat,
        usascientific_96_wellplate_2_4ml_deep,
        corning_96_wellplate_360ul_flat,
        biorad_96_wellplate_200ul_pcr,
        opentrons_40_aluminumblock_eppendorf_24x2ml_safelock_snapcap_generic_16x0_2ml_pcr_strip,
        opentrons_24_aluminumblock_generic_2ml_screwcap,
        opentrons_96_aluminumblock_biorad_wellplate_200ul,
        opentrons_96_aluminumblock_generic_pcr_strip_200ul,
        opentrons_96_tiprack_300ul,
        opentrons_24_tuberack_eppendorf_1_5ml_safelock_snapcap,
        opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical,
        opentrons_15_tuberack_falcon_15ml_conical,
        opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap,
        opentrons_24_tuberack_generic_2ml_screwcap,
        opentrons_6_tuberack_falcon_50ml_conical,
        opentrons_96_tiprack_10ul,
        tipone_96_tiprack_200ul,
        opentrons_96_tiprack_1000ul,
        agilent_1_reservoir_290ml,
        usascientific_12_reservoir_22ml,
        opentrons_24_tuberack_generic_0_75ml_snapcap_acrylic,
        opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap_acrylic,
        opentrons_10_tuberack_falcon_4x50ml_6x15ml_conical_acrylic,
    }
}
