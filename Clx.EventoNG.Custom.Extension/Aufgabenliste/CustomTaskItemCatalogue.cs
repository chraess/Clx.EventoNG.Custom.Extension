
using System.Collections.ObjectModel;
using Clx.EventoNG.Common.Models.Functions;
using Clx.EventoNG.Common.Models.Shared;
using Clx.EventoNG.Custom.Extension.Properties;
using Clx.Tools;


namespace Clx.EventoNG.Custom.Extension.Aufgabenliste
{
    /// <summary>
    /// Identifikation der Aufgabe
    /// </summary>
    public enum CustomTaskItemId
    {
        /// <summary>
        /// Unbekannte Frage
        /// </summary>
        None = 0,
        /// <summary>
        /// Beispiel Frage
        /// </summary>
        Example = 1,
        /// <summary>
        /// Information ohne Frage [100001]
        /// </summary>
        Info = 2,
        /// <summary>
        /// Warnung ohne Frage [100002]
        /// </summary>
        Warning = 3,
        /// <summary>
        /// Fehlermeldung ohne Frage [100003]
        /// </summary>
        Error = 4
    }

    /// <summary>
    /// Aufgabenliste die eine Liste aller Aufgaben als Keyed Collection aufbewahrt
    /// </summary>
    public class TaskItemCollection : KeyedCollection<CustomTaskItemId, TaskListItem>
    {
        protected override CustomTaskItemId GetKeyForItem(TaskListItem item)
        {
            return (CustomTaskItemId)item.Id;
        }
    }

    /// <summary>
    /// Kundenspezifischer Fragenkatalog für Aufgabenliste
    /// </summary>
    public class CustomTaskItemCatalogue
    {

        private static readonly TaskItemCollection _TaskItems;

        static CustomTaskItemCatalogue()
        {
            _TaskItems = new TaskItemCollection();

        }

        /// <summary>
        /// Alle Fragen des Katalogs hier einfügen
        /// </summary>
        private static void LoadTaskItemCatalogue()
        {
            var allItems = new TaskListItem[]
            {
                new FreeTextQuestionItem{Title = Resources.Das_ist_eine_Beispielfrage_ohne_Funktion,Id = (int) CustomTaskItemId.Example},
                new ErrorItem{Id = (int) CustomTaskItemId.Error,Title = Resources.ErrorTitle},
                new WarningItem{Id = (int) CustomTaskItemId.Warning,Title = Resources.WarningTitle}
                //Fügen Sie weitere Fragen, Fehler, Warnungen, Infos dem Katalog hinzu
            };
            _TaskItems.AddRange(allItems);

        }

        /// <summary>
        /// Gibt bestimmten Aufgabenlisten-Eintrag vom Katalog zurück
        /// </summary>
        /// <param name="customTaskItemId">Identifikation des Taskitems</param>
        /// <param name="text">Text für die Gruppierung des TasklistItems (optional)</param>
        /// <param name="data">Betroffenes Objekt für Aufgabenliste</param>
        /// <returns>Instanz der Frage mit Text,Typ und Standardantwort(en)</returns>
        public static TaskListItem GetTaskItemInstance(int customTaskItemId, string text, object data)
        {
            TaskListItem returnItem = null;
            // Fragen beim ersten Aufruf laden
            if (_TaskItems.Count == 0)
            {
                lock (_TaskItems)
                {
                    if (_TaskItems.Count == 0)
                    {
                        LoadTaskItemCatalogue();
                    }
                }
            }

            if (_TaskItems.Contains((CustomTaskItemId)customTaskItemId))
            {   // Kopie zurückgeben
                returnItem = _TaskItems[(CustomTaskItemId)customTaskItemId].Copy();
                returnItem.Data = data;
                if (text.IsNullOrEmpty() == false)
                {
                    returnItem.Text = text;
                }

            }
            return returnItem;
        }
    }
}
