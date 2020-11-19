using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class Search
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Uploader { get; set; }
 
        public DateTime? DateRangeMin { get; set; }

        public DateTime? DateRangeMax { get; set; }

        [MaxLength(50)]
        public string Client { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        public string Success { get; set; }

        public IEnumerable<Project> Projects { get; set; }

    }
}
