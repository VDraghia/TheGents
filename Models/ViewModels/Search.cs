
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class Search
    {
        public string Name { get; set; }

        public string Uploader { get; set; }

        public string DateRangeMin { get; set; }

        public string DateRangeMax { get; set; }

        public string Client { get; set; }

        public string Location { get; set; }

        public string Success { get; set; }

    }
}
