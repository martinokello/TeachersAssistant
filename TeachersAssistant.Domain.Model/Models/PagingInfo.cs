using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachersAssistant.Domain.Model.Models
{
    public class PagingInfo
    {
        public int CurrentPageIndex { get; set; }
        public int NumberOfPagesToDisplay { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfItemsPerPage { get; set; }
        public bool PreviousPageLinkEnabled { get; set; }
        public bool NextPageLinkEnabled { get; set; }
        public bool PreviousGroupSetEnabled { get; set; }
        public bool NextGroupSetEnabled { get; set; }
        public int CurrentPage { get; set; }
    }
}