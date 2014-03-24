
namespace Clx.EventoNG.Custom.Extension
{
    /// <summary>
    /// Kundenspezifische Funktions Identifikation
    /// <remarks>Id immer grösser 100000</remarks>
    /// </summary>
    public enum CustomFunctionEnum
    {
        /// <summary>
        /// Kundenspezifische Funktion für Defaultwert, die nichts macht [Vorlage]
        /// </summary>
        CustomDefaultvalueTemplate = 999996,
        /// <summary>
        /// Kundenspezifische Funktion für Statuswechsel, die nichts macht [Vorlage]
        /// </summary>
        CustomStatuswechselTemplate = 999997,
        /// <summary>
        /// Kundenspezifische Funktion für Preisregel, die nichts macht [Vorlage]
        /// </summary>
        CustomPreisregelTemplate = 999998,
        /// <summary>
        /// Kundenspezifischer Suchfilter für die Datenprüfung
        /// </summary>
        CustomSearchFilterTemplate = 999999,


        CustomField_AnzahlKursanmeldungen   = 100001,
        CustomField_AlleAnmeldungen         = 100002,
        StudiereneMitMehrAlsXAnmeldungen    = 100003,
        CustomField_AusgeführteWBFunktion   = 100004,
        CustomField_Dummy_Test_GitHub_RCH   = 999999,

    }
}
