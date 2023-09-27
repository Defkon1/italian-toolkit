﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using ItalianToolkit.Transports.Models;

namespace ItalianToolkit.Transports.PlatesIdentifierPlugins
{
    public class DiplomaticCorpsPlateIdentifier : IPlatesIdentifierPlugin
    {
        public PlateType PlateType => PlateType.DiplomaticCorps;

        public string Pattern => @"^[cC][dD][0-9]{3}([A-Za-z]{2})\b$";

        private readonly Dictionary<string, string> _diplomaticCorpsCodes = new Dictionary<string, string>()
        {
            { "AA",  "Albania" },
            {"AC", "Austria"},
            {"AE", "Belgium"},
            { "AG", "Bulgaria"},
            { "AK", "Czech Republic"},
            { "AM", "Cyprus"},
            { "AN", "Denmark"},
            { "AP", "Finland"},
            { "AQ", "France"},
            { "AU", "Germany"},
            { "BC", "Great Britain"},
            { "BF", "Slovenia"},
            { "BG", "Greece"},
            { "BM", "Ireland"},
            { "BN", "Italy (Holy See)"},
            { "BP", "Serbia"},
            { "BQ", "Croatia"},
            { "BR", "Luxembourg"},
            { "BS", "Malta"},
            { "BT", "Monaco"},
            { "BV", "Norway"},
            { "BX", "Netherlands"},
            { "CA", "Poland"},
            { "CC", "Portugal"},
            { "CE", "Romania"},
            { "CG", "San Marino"},
            { "CH", "Spain"},
            { "CQ", "Switzerland"},
            { "CN", "Sweden"},
            { "CR", "Turkey"},
            { "CX", "Hungary"},
            { "DA", "Russian Federation"},
            { "DC", "the Ukraine"},
            { "DD", "Uzbekistan"},
            { "DE", "Vatican City State (Apostolic Nunciature)"},
            { "DF", "Estonia"},
            { "DG", "Macedonia"},
            { "DH", "Bosnia-Herzegovina"},
            { "DL", "Slovakian Republic"},
            { "DM", "Armenia"},
            { "DN", "Georgia"},
            { "DP", "Kazakhstan"},
            { "DQ", "Latvia"},
            { "DR", "Belarus"},
            { "DS", "Lithuania"},
            { "DT", "Moldava"},
            { "DV", "Iceland"},
            { "DZ", "Azerbaijan"},
            { "EA", "Burkina Faso"},
            { "EB", "Dominica"},
            { "EC", "Uganda"},
            { "ED", "Burundi"},
            { "EF", "Rwanda"},
            { "EG", "Zimbawe"},
            { "EH", "Qatar"},
            { "EL", "Chad"},
            { "EM", "Mauritania"},
            { "EN", "Eritrea"},
            { "EP", "Mali"},
            { "ER", "Belize"},
            { "ES", "Equatorial Guinea c/o FAO"},
            { "ET", "Kosovo"},
            { "EU", "Benin"},
            { "GA", "Afghanistan"},
            { "GB", "Saudi Arabia"},
            { "GC", "Bangladesh"},
            { "GD", "Myanmar"},
            { "GE", "Taiwan"},
            { "GF", "China"},
            { "GL", "North Korea"},
            { "GM", "Korea"},
            { "GP", "Arab Emirates"},
            { "GQ", "Philippines"},
            { "GS", "Japan"},
            { "GZ", "Jordan"},
            { "HA", "India"},
            { "HC", "Indonesia"},
            { "HE", "Iran"},
            { "HF", "Iraq"},
            { "HL", "Israel"},
            { "HQ", "Kuwait"},
            { "HR", "Lebanon"},
            { "HS", "Malaysia"},
            { "HT", "Oman"},
            { "HV", "Pakistan"},
            { "HX", "Syria"},
            { "LA", "Sri Lanka"},
            { "LB", "Thailand"},
            { "LE", "Vietnam"},
            { "LF", "Yemen"},
            { "LH", "Montenegro"},
            { "LM", "East Timor"},
            { "NA", "Algeria"},
            { "NC", "Angola"},
            { "ND", "Cameroon"},
            { "NF", "Cape Verde"},
            { "NG", "Central African Republic"},
            { "NH", "Republic of Congo"},
            { "NL", "Ivory Coast"},
            { "NM", "Egypt"},
            { "NR", "Ethiopia"},
            { "NT", "Gabon"},
            { "NX", "Ghana"},
            { "PA", "Guinea"},
            { "PB", "Kenya"},
            { "PC", "Lesotho"},
            { "PD", "Liberia"},
            { "PE", "Libya"},
            { "PL", "Madagascar"},
            { "PN", "Morocco"},
            { "PQ", "Nigeria"},
            { "PS", "Senegal"},
            { "PT", "Sierra Leone"},
            { "PV", "Mozambico"},
            { "PX", "Somalia"},
            { "QA", "South Africa"},
            { "QC", "Sudan"},
            { "QE", "Tanzania"},
            { "QG", "Tunisia"},
            { "QL", "Democratic Republic of Congo"},
            { "QN", "Zambia"},
            { "QP", "Niger"},
            { "SA", "Canada"},
            { "SD", "Mexico"},
            { "SF", "United States of America"},
            { "SH", "United States of America"},
            { "SL", "United States of America"},
            { "SN", "United States of America"},
            { "SQ", "United States of America"},
            { "TA", "Costa Rica"},
            { "TC", "Cuba"},
            { "TE", "Dominican Republic"},
            { "TF", "Ecuador"},
            { "TG", "Jamaica"},
            { "TH", "Guatemala"},
            { "TL", "Haiti"},
            { "TM", "Honduras"},
            { "TP", "Nicaragua"},
            { "TQ", "Panama"},
            { "TS", "El Salvador"},
            { "UA", "Argentina"},
            { "UE", "Bolivia"},
            { "UF", "Brazil"},
            { "UH", "Chile"},
            { "UL", "Colombia"},
            { "UN", "Paraguay"},
            { "UP", "Peru"},
            { "US", "Uruguay"},
            { "UT", "Venezuela"},
            { "XA", "Sovereign Military Order of Malta, Palestine, Arab League"},
            { "XC", "FAO, United Nations, UNIDO, International Organisations, European Union"},
            { "XD", "FAO, United Nations, UNIDO, International Organisations, European Union"},
            { "XE", "FAO, United Nations, UNIDO, International Organisations, European Union"},
            { "XF", "FAO, United Nations, UNIDO, International Organisations, European Union"},
            { "XH", "FAO, United Nations, UNIDO, International Organisations, European Union"},
            { "XG", "Vatican City State"},
            { "ZA", "Australia"},
            { "ZC", "New Zealand "},
        };

        public virtual Plate TryIdentifyPlate(string plateNumber)
        {
            var testPlateNumber = plateNumber.Trim().ToUpper().Replace(" ", "");

            PlateSearchResult result = new PlateSearchResult();

            Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            var match = regex.Match(testPlateNumber);

            if (match.Success && match.Groups.Count > 1)
            {
                var code = match.Groups[1].Value;

                if (_diplomaticCorpsCodes.ContainsKey(code))
                {
                    return new DiplomaticPlate()
                    {
                        PlateNumber = plateNumber,
                        Type = PlateType,
                        DiplomaticDetails = _diplomaticCorpsCodes[code]
                    };
                }
            }

            return null;
        }
    }
}