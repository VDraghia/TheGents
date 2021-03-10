using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class UploadDocumentModel
    {

        public string DocumentName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public bool Success { get; set; }

        public IFormFile File { get; set; }

        public IList<ListFactorDescriptorModel> ListFactorDesc { get; set; }


        public bool Error { get; set; }

        public string Message { get; set; }



    }
}

