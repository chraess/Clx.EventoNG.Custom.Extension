using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clx.EventoNG.Common.Models.Functions;
using Clx.EventoNG.Common.Models.EntityExplorer;


namespace Clx.EventoNG.Custom.Extension.Felder
{
    class CustomFieldAusgefuehrteWBFunktion : FunctionCustomListFieldBase
    {
        public override EmbeddedGrid GetGridDefinition()
        {
            var grid = new EmbeddedGrid(
                ';', // Trennzeichen für Felder 
                // Spalten:
                new EmbeddedGridColumn("Bezeichnung", 100, typeof(string)),
                new EmbeddedGridColumn("Datum", 100, typeof(string)),
                new EmbeddedGridColumn("Von", 100, typeof(string))
                );
            return grid;
        }

        public override string Caption
        {
            get { return "AusgeführteWBFunktion"; }
        }

        public override string Category
        {
            get { return "WB"; }
        }

        public override FunctionCustomFieldEntityType EntityType
        {
            get { return FunctionCustomFieldEntityType.Anlass ; }
        }

        public override int Id
        {
            get { return (int)CustomFunctionEnum.CustomField_AusgeführteWBFunktion; }
        }
    }
}
