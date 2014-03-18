using System;
using Clx.EventoNG.Common.Models.Functions;
using Clx.EventoNG.Common.Models.SearchFilter;
using Fn = Clx.EventoNG.Public.Backend.Logic.Function;

namespace Clx.EventoNG.Custom.Extension.Suchfilter
{
    /// <summary>
    /// Vorlage für SearchFilter-Funktion
    /// </summary>
    public class StudierendeMitMehrAlsXAnmeldugnen : FunctionSearchFilterSqlBasedBase
    {
        /// <summary>
        /// todo: Eröffnen der neuen ID in CustomFunctionEnum
        /// </summary>
        public override int Id
        {
            get
            {
                return (int)CustomFunctionEnum.StudiereneMitMehrAlsXAnmeldungen;
            }
        }


        /// <summary>
        /// Name des Suchfilters (benutzerlesbar)
        /// </summary>
        public override string Name
        {
            //todo: Name, erscheint im Baum "Datenprüfungen"
            get { return "Studierende mit mehr als X Anmeldungen"; }
        }

        /// <summary>
        /// Beschreibung des Suchfilters (benutzerlesbar). Dies kann ein Text mit HMTL-Tags sein.
        /// </summary>
        public override string Description
        {
            //todo: Beschreibungstext (HTML)
            get { return @"Kurzbeschreibung<br /><a href='file://[File]'>Weiterführendes Dokument</a>"; }
        }

        /// <summary>
        /// Kategorie (benutzerlesbar) ==> Kategorie-Knoten in Baum "Datenprüfungen"
        /// </summary>
        public override string Category
        {
            get { return "Demo"; }
        }

        /// <summary>
        /// Entitätstyp für die Ergebnisliste
        /// </summary>
        public override SearchFilterEntityType EntityType
        {
            get { return SearchFilterEntityType.Person; }
        }




        /// <summary>
        /// vom Benutzer setzbare Parameter für Suchfilter
        /// </summary>
        public override SearchFilterParameterCollection Parameters
        {
            get
            {

                return new SearchFilterParameterCollection(


                    // beispiel: einfacher int-Paramter mit Standardwert = 10
                    new SearchFilterParameter("AnzTot", "Mindestanzahl Anmeldungen an Anlass:", 10)
            );

            }
        }


        /// <summary>
        /// gibt den SQL-Befehl für die Suchabfrage zurück.
        /// Der SQL Befehl muss die Keys zurückgeben (1 oder 2 INT-Werte)
        /// Der SQL Befehl muss definierte Parameterwerte als @-Platzhalter enthalten
        /// </summary>
        public override string SqlCommand
        {

            get
            {
                return @"
SELECT p.IdPerson
FROM tblPerson p
WHERE (SELECT COUNT(*)
       FROM tblPersonenAnmeldung as pa
       join tblAnmeldung as a on a.idanmeldung = pa.idanmeldung
       join tblAnlass anl on anl.idanlass = a.idanlass
       where pa.idperson=p.idperson) > @AnzTot
";
            }
        }

        private const string GEBURTSJAHR = "GEBURTSJAHR";

        public override SearchFilterCalculatedColumnCollection CalculatedColumns
        {
            get
            {
                return new SearchFilterCalculatedColumnCollection
                {
                    new SearchFilterCalculatedColumn(1,
                        GEBURTSJAHR,
                        typeof(int?),
                        100,
                        SearchFilterCalculatedColumnType.ListColumn,
                        "Geburtsjahr",
                        persistListColumnValue:true,
                        tooltip:"Dies ist das Geburtsjahr")
                };
            }
        }

        

        protected override SearchFilterCalculatedColumnResult GetCalculatedColumnResult(object dataKey, Type resultType, string columnId)
        {
            switch (columnId)
            {
                case GEBURTSJAHR:   

                return Fn.RunSqlCommandScalar<int?>("select YEAR(PersonGebDatum) from tblPerson where idPerson ={0}", dataKey);

                //DateTime? geburtsdatum = Fn.GetNullableDateTimeValuePerson(dataKey, "PersonGebDatum");
                //if (geburtsdatum != null)
                //{
                //    int geburtsJahr = geburtsdatum.Value.Year;
                //    return geburtsJahr;
                //}
                //return null;

                default:
                throw new Exception("invalid columId:" + columnId);
            }
        }

        /// <summary>
        /// Der Parameter wird validiert.
        /// Falls kein Fehler vorliegt, geben Sie null zurück, ansonsten die Fehlermeldung
        /// </summary>
        /// <param name="parameterId">id des Parameters, der validiert werden soll</param>
        /// <param name="value">Wert des Parameters, der validiert werden soll</param>
        /// <returns></returns>
        protected override string ValidateParameter(string parameterId, object value)
        {
            //note: mit GetParameterValue kann der Wert eines anderen Parameters geholt werden
            //note: mit Logic.Function.RunSqlCommandScalar kann ein Wert von einer Datenbank geholt werden

            switch (parameterId)
            {
                //todo: Switch-Statement pro zu validierendem Parameter mit einem "case" ergänzen
                /*  Beispiele
                case "AnzTot":

                    var mindestAnzahl = 10;
                    if (GetParameterValue<int>("IdStudiengang") == 0)
                    {
                        mindestAnzahl = 50;
                    }

                    if (((int)value) < mindestAnzahl)
                        return "es muss mindestens " + mindestAnzahl + " erfasst werden.";
                    break;

                case "IdStudiengang":

                    // IdStudiengang: Anlassbezeichnung darf nicht mit "A" beginnen
                    var idStg = (int)value;
                    if (idStg != 0)
                    {
                        var bez = Logic.Function.RunSqlCommandScalar<string>("select AnlassBezeichnung FROM Anlass WHERE IDAnlass=" + idStg);
                        if (bez.StartsWith("A"))
                            return "Studiengang darf nicth mit A beginnen!";
                    }
                    break;
                */

            }

            return null;
        }

    }
}
