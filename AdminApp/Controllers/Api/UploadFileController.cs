using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Upload")]
    public class UploadFileController : Controller
    {
        private IHostingEnvironment hostingEnvironment;

        public UploadFileController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;

            if (!Directory.Exists(Constants.Config.temp_folder))
            {
                Directory.CreateDirectory(Constants.Config.temp_folder);
            }
        }

        // POST: api/Upload
        [HttpPost]
        //[RequestSizeLimit(5242880)]
        public async Task<IActionResult> PostUploadFile(IFormFile file)
        {
            try
            {
                //foreach (IFormFile source in files)
                //{
                //    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                //    filename = this.EnsureCorrectFilename(filename);

                //    using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                //        await source.CopyToAsync(output);
                //}

                //long size = files.Sum(f => f.Length);

                // full path to file in temp location
                var filePath = Path.Combine(Constants.Config.temp_folder, Guid.NewGuid() + ".tmp");
                //var filePath = Path.GetTempFileName();
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                var tempfileName = Path.GetFileName(filePath);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                // process uploaded files
                // Don't rely on or trust the FileName property without validation.
                //return Ok(new { count = files.Count, size, filePath });
                return Json(new { success = true, tempfileName, fileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        
    }
}