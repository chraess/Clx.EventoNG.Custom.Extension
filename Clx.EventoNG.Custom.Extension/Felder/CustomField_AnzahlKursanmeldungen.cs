using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clx.EventoNG.Common.Models.Functions;

namespace Clx.EventoNG.Custom.Extension.Felder
{
    class CustomField_AnzahlKursanmeldungen : FunctionCustomFieldBase 
    {
        public override string Caption
        {
            get { return "Anzahl Anmeldunge"; }
        }

        public override string Category
        {
            get { return "Demo"; }
        }

        public override FunctionCustomFieldEntityType EntityType
        {
            get { return FunctionCustomFieldEntityType.Person; }
        }

        public override Type FieldType
        {
            get { return typeof(int); }
        }

        public override int Id
        {
            get { return (int)CustomFunctionEnum.CustumField_AnzahlKursanmeldungen; }
        }

        public override int Width
        {
            get { return 100; }
        }
    }
}
