using System.Collections.Generic;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class ViewProjectModel
    {

        public Project SelectedProject{ get; set; }

        public IList<ListFactorDescriptor> FactorDescriptiors { get; set; }

    }
}
