using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class ListFactorDescriptorModel
    {

        public int FactorId { get; set; }

        public int Position { get; set; }

        public string MainCategoryDesc { get; set; }

        public string SubCategoryDesc { get; set; }

        public bool Checked { get; set; }
    }
}
