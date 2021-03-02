using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class SearchViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string UploaderEmail { get; set; }

        public DateTime? DateRangeMin { get; set; }

        public DateTime? DateRangeMax { get; set; }

        [MaxLength(50)]
        public string Client { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        public string Success { get; set; }

        public IEnumerable<Document> Documents { get; set; }

        public IEnumerable<Factor> MustHaveFactors { get; set; }

        public IEnumerable<Factor> NotHaveFactors { get; set; }
    }
}
