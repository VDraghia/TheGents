using ProjectManagementCollection.Models.DescriptorModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class ViewDocumentModel
    {

        // Document to display
        public Document Document { get; set; }

        //Related Project
        public Project Project { get; set; }

        /*
         * List of factors, Outer list for all factors Inner list for Factor description
         * Index [0] of inner list is Main Category, Index [>0] is Sub category
         */
        public IList<ListFactorDescriptor> Factors { get; set; }

        [NotMapped]
        public Dictionary<string, string> FactorStrings { get; set; }
    }
}