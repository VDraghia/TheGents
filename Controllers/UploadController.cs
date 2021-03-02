using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Controllers
{
    public class UploadController : Controller
    {

        private readonly ILogger<UploadController> _logger;
        private readonly PmcAppDbContext _context;

        public UploadController(PmcAppDbContext context, ILogger<UploadController> logger)
        {
            _logger = logger;
            _context = context;
        }

        //by Tim
        string AWS_accessKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_accessKey"];
        string AWS_secretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_secretKey"];
        string AWS_bucketName = "gentsproject2";


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> UploadF(List<IFormFile> uploadFile, Project project, UserProject userProject)
        {

            ViewBag.result = await UploadFToAWSAsync(uploadFile, project, userProject);
            return RedirectToAction("UploadF");
        }

        protected async Task<string> UploadFToAWSAsync(List<IFormFile> uploadFile, Project project, UserProject userProject)
        {
            var result = "";
            Project proj = new Project();
            proj = await _context.Projects.FirstOrDefaultAsync(p => p.Name == project.Name);
            if (proj == null)
            {
                TempData["message"] = "The project is not existed, Upload failed!!";
                return result;
            }
            projId = proj.ProjectId;
            var subFolder = project.Name;

            try
            {
                var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.CACentral1);
                var bucketName = AWS_bucketName;
                var keyName = "";
                if (!string.IsNullOrEmpty(subFolder))
                    keyName = subFolder.Trim();
                foreach (var uFile in uploadFile)
                {
                    var newKeyName = keyName + "/" + uFile.FileName;
                    var fs = uFile.OpenReadStream();
                    var request = new Amazon.S3.Model.PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = newKeyName,
                        InputStream = fs,
                        ContentType = uFile.ContentType,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    await s3Client.PutObjectAsync(request);

                    result = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, newKeyName);

                    var file = new Document()
                    {
                        Name = uFile.FileName,
                        Url = newKeyName,
                        ProjectDocFk = projId
                    };

                    _context.Documents.Add(file);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Uploaded Successfully!!";

                    var aa = userProject.AreChecked;
                    if (aa != null)
                    {
                        foreach (int a in aa)
                        {
                            var row = _context.ProjectFactorRels.FirstOrDefault(p => p.FactorFk == a && p.ProjectFk == projId);
                            if (row == null)
                            {
                                _context.ProjectFactorRels.Add(new ProjectFactorRel { FactorFk = a, ProjectFk = projId });
                                _context.SaveChanges();
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectCreate([Bind("ProjectId,Name,Uploaded,DateCompleted,Client,Location,Success,Uploader_id")] Project project)
        {

            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                resProject = project.Name;
                return RedirectToAction("Upload");
            }

            return RedirectToAction("ProjectDetail");
        }

        public IActionResult SearchProject(string projectName)
        {

            if (projectName == "")
            {
                return View();
            }

            List<Project> project = _context.Projects.Where(p => p.Name.Contains(projectName)).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject([Bind("Name,Location,Client,Completed")] Project project)
        {

            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return View();
            }
            //List<Project> possibleProjects = from projects in _context.Set<Projects>

            return View(project);
        }
    }
}
