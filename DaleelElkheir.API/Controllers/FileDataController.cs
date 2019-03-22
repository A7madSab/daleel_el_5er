using DaleelElkheir.API.Filter;
using DaleelElkheir.API.Models;
using DaleelElkheir.BLL.Services.FilesData;
using DaleelElkheir.DAL.Domain;
using DaleelElkheir.API.Models.FileData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DaleelElkheir.API.Controllers
{
    public class FileDataController : ApiController
    {
        private readonly FileDataService fileService;

        public FileDataController (FileDataService _fileService)
        {
            this.fileService = _fileService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> InsertFileData()
        {
            string dir = Guid.NewGuid().ToString();
            FileData request = new FileData();
            if (!this.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            root += "/" + dir;
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            else
            {
                Directory.Delete(root, true);
                Directory.CreateDirectory(root);
            }
            var provider = new CustomMultipartFormDataStreamProvider(root);
            var result = await this.Request.Content.ReadAsMultipartAsync(provider);
            var file = provider.FileData.FirstOrDefault();

            var originalName = file.Headers.ContentDisposition.FileName.Substring(1, file.Headers.ContentDisposition.FileName.Count() - 2).ToString();
            request.Name = originalName;
            //request.FileBinary =provider.FileData. ;
            //foreach (var key in provider.FormData.AllKeys)
            //{
            //    foreach (var val in provider.FormData.GetValues(key))
            //    {
            //        if (key == "FileBinary")
            //        {
            //            request.FileBinary =Encoding.ASCII.GetBytes(val);
            //        }

            //    }
            //}
            try
            {
                request.Extenstion = ConfigurationManager.AppSettings["Image_URL"] + "/UploadedFiles/" + dir + "/" + originalName.ToString();
            }
            catch
            {
                request.Extenstion = null;
            }
            fileService.InsertFileData(request);

            var insertedFile = new FileDataModel() {ID=request.ID,Name=request.Name,Extension=request.Extenstion };
            return Ok(new BaseResponse(insertedFile));

        }

        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public CustomMultipartFormDataStreamProvider(string path) : base(path)
            { }

            public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
                return name.Replace("\"", string.Empty);
            }
        }


    }
}
