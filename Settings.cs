using System.Collections.Generic;
using System.Windows.Forms;

namespace Futurez.XrmToolBox
{
    /// <summary>
    /// This class can help you to store settings for your plugin
    /// </summary>
    /// <remarks>
    /// This class must be XML serializable
    /// </remarks>
    public class Settings
    {
        public string LastUsedOrganizationWebappUrl { get; set; }

        public string EntityListFilter { get; set; }
        public List<string> CheckedEntityNames { get; set; }

        public int ListSortColumn { get; set; }
        public SortOrder ListSortOrder { get; set; }

    }
}