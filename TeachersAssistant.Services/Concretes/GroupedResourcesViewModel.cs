using System.Collections.Generic;
using System.Linq;
using TeachersAssistant.Domain.Model.Models;

namespace TeachersAssistant.Services.Concretes
{
    public class GroupedResourcesViewModel
    {
        public string GroupedByRole { get; set; }
        public IEnumerable<Dictionary<string, IGrouping<int, StudentResource>>> GroupedBySubject { get; set; }
        public IEnumerable<IGrouping<int,StudentResource>> IndividualResources { get; set; }
    }
}