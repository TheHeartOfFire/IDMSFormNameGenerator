﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IDMSFormNameGenerator.DataTypes
{
    public class PromptDataSettings
    {
        public PromptType Type { get; set; }
        public bool IsExpression { get; set; }
        public bool Required { get; set; }
        public int Length { get; set; }
        public int DecimalPlaces { get; set; }
        public string Delimiter { get; set; }
        public bool AllowNegative { get; set; }
        public bool ForceUpperCase { get; set; }
        public bool MakeBuyerVars { get; set; }
        public bool IncludeNoneAsOption { get; set; }

        internal void GenerateXml(XmlWriter xml)
        {
            xml.WriteAttributeString("type", GetPromptType(Type));
            xml.WriteAttributeString("promptIsExpression", IsExpression.ToString().ToLowerInvariant());
            xml.WriteAttributeString("required", Required.ToString().ToLowerInvariant());
            xml.WriteAttributeString("leftSize", Length.ToString());
            xml.WriteAttributeString("rightSize", DecimalPlaces.ToString());
            xml.WriteAttributeString("choicesDelimiter", Delimiter);
            xml.WriteAttributeString("allowNegatives", AllowNegative.ToString().ToLowerInvariant());
            xml.WriteAttributeString("forceUpperCase", ForceUpperCase.ToString().ToLowerInvariant());
            xml.WriteAttributeString("makeBuyerVars", MakeBuyerVars.ToString().ToLowerInvariant());
            xml.WriteAttributeString("includeNoneAsOption", IncludeNoneAsOption.ToString().ToLowerInvariant());

        }

        public enum PromptType
        {
            OneTwoThree,
            OneTwoThreeFour,
            CheckBox,
            Date,
            Decimal,
            Dropdown,
            Integer,
            Label,
            LabelNumber,
            Money,
            RadioButtons,
            Separator,
            StateCode,
            Text,
            VIN,
            YesNo,
            ZIP5,
            ZIP10
        }
        public PromptDataSettings(XmlAttributeCollection attributes)
        {
            Type = GetPromptType(attributes[0].Value);

            if (bool.TryParse(attributes[1].Value, out bool parsedBool))
                IsExpression = parsedBool;

            if (bool.TryParse(attributes[2].Value, out parsedBool))
                Required = parsedBool;

            if (int.TryParse(attributes[3].Value, out int parsedInt))
                Length = parsedInt;

            if (int.TryParse(attributes[4].Value, out parsedInt))
                DecimalPlaces = parsedInt;

            Delimiter = attributes[5].Value;

            if (bool.TryParse(attributes[6].Value, out parsedBool))
                AllowNegative = parsedBool;

            if (bool.TryParse(attributes[7].Value, out parsedBool))
                ForceUpperCase = parsedBool;

            if (bool.TryParse(attributes[8].Value, out parsedBool))
                MakeBuyerVars = parsedBool;

            if (bool.TryParse(attributes[9].Value, out parsedBool))
                IncludeNoneAsOption = parsedBool;

        }
        public static PromptType GetPromptType(string type) => type switch
        {
            "BuyerCoBoth" => PromptType.OneTwoThree,
            "BuyerCoBothOther" => PromptType.OneTwoThreeFour,
            "Checkbox" => PromptType.CheckBox,
            "Date" => PromptType.Date,
            "Decimal" => PromptType.Decimal,
            "Dropdown" => PromptType.Dropdown,
            "Integer" => PromptType.Integer,
            "InstructionLine" => PromptType.Label,
            "LabelNumber" => PromptType.LabelNumber,
            "Money" => PromptType.Money,
            "RadioButtons" => PromptType.RadioButtons,
            "Separator" => PromptType.Separator,
            "StateCode" => PromptType.StateCode,
            "Text" => PromptType.Text,
            "VIN" => PromptType.VIN,
            "YesNo" => PromptType.YesNo,
            "Zip5" => PromptType.ZIP5,
            "Zip10" => PromptType.ZIP10,
            _ => PromptType.Text
        };

        public static string GetPromptType(PromptType type) => type switch
        {
            PromptType.OneTwoThree => "BuyerCoBoth",
            PromptType.OneTwoThreeFour => "BuyerCoBothOther",
            PromptType.CheckBox => "Checkbox",
            PromptType.Date => "Date",
            PromptType.Decimal => "Decimal",
            PromptType.Dropdown => "Dropdown",
            PromptType.Integer => "Integer",
            PromptType.Label => "InstructionLine",
            PromptType.LabelNumber => "LabelNumber",
            PromptType.Money => "Money",
            PromptType.RadioButtons => "RadioButtons",
            PromptType.Separator => "Separator",
            PromptType.StateCode => "StateCode",
            PromptType.Text => "Text",
            PromptType.VIN => "VIN",
            PromptType.YesNo => "YesNo",
            PromptType.ZIP5 => "Zip5",
            PromptType.ZIP10 => "Zip10",
            _ => "Text"
        };

        public static string PromptDescriptor(PromptType type) => type switch
        {
            PromptType.OneTwoThree => "123",
            PromptType.OneTwoThreeFour => "1234",
            PromptType.CheckBox => "CHK",
            PromptType.Date => "DATE",
            PromptType.Decimal => "1.0",
            PromptType.Dropdown => "DROP",
            PromptType.Integer => "100",
            PromptType.Label => "LBL",
            PromptType.LabelNumber => "LBL#",
            PromptType.Money => "$1.00",
            PromptType.RadioButtons => "RDO",
            PromptType.Separator => "SEP",
            PromptType.StateCode => "ST",
            PromptType.Text => "ABC",
            PromptType.VIN => "VIN",
            PromptType.YesNo => "Y/N",
            PromptType.ZIP5 => "Zip5",
            PromptType.ZIP10 => "Zip10",
            _ => "ABC",
        };
    }

}
