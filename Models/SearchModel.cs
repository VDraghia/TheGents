
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class SearchModel
    {
        public string Name { get; set; }

        public string Uploader { get; set; }

        public string DateUploaded { get; set; }

        public string Client { get; set; }

        public string Location { get; set; }

        public bool Success { get; set; }

    }
}
