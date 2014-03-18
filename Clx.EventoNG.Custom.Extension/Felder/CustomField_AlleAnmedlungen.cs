using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clx.EventoNG.Common.Models.Functions;
using Clx.EventoNG.Common.Models.EntityExplorer;

namespace Clx.EventoNG.Custom.Extension.Felder
{
    class CustomField_AlleAnmedlungen : FunctionCustomListFieldBase 
    {
        public override EmbeddedGrid GetGridDefinition()
        {
            var grid = new EmbeddedGrid(
                ';', // Trennzeichen für Felder 
                // Spalten:
                new EmbeddedGridColumn("Analassnummer", 100, typeof(string)),
                new EmbeddedGridColumn("Status", 100, typeof(string))
                );
            return grid;
        }

        public override string Caption
        {
            get { return "Alle Anmeldungen"; }
        }

        public override string Category
        {
            get { return "Demo"; }
        }

        public override FunctionCustomFieldEntityType EntityType
        {
            get { return FunctionCustomFieldEntityType.Person; }
        }

        public override int Id
        {
            get { return (int)CustomFunctionEnum.CustomField_AlleAnmeldungen; }
        }
    }
}
