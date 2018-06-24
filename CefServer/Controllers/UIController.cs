using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CefServer
{
    /// <summary>
    /// Provider ui files
    /// </summary>
    public class UIController : ApiController
    {
        private const string AppName = "UI";
        private string FilePath = AppDomain.CurrentDomain.BaseDirectory;//exe path
        private string[] DefaultHtmlVariants = {"index.html", "default.html", "index.htm", "default.htm"};

        /// <summary>
        /// Get Index file
        /// </summary>
        /// <returns></returns>
        [Route("ui")]
        public IHttpActionResult GetIndex()
        {
            var path = Path.Combine(FilePath, AppName);
            var indexFile = GetIndexFilePath(path);

            return !String.IsNullOrWhiteSpace(indexFile) ? GetFile(indexFile) : FileNotFound();
        }

        /// <summary>
        /// Get other files
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("ui/{id}")]
        public IHttpActionResult GetOthers(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return GetIndex();

            var filePath = Path.Combine(FilePath, AppName, id.Replace('/', '\\'));

            return !String.IsNullOrWhiteSpace(filePath) ? GetFile(filePath) : FileNotFound();
        }

        /// <summary>
        /// Return html page with 404 file not find
        /// </summary>
        /// <returns></returns>
        private IHttpActionResult FileNotFound()
        {
            return StreamResponse(null, MediaTypeHeaderValue.Parse("text/html;charset=iso-8859-1"), HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Create http response with specific file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private IHttpActionResult GetFile(string filePath)
        {
            HttpContent data;
            MediaTypeHeaderValue contentType = null;
            var extendedPath = Path.GetExtension(filePath);

            if(Request.Method == HttpMethod.Get && extendedPath != null)
            {
                data = new StreamContent(File.OpenRead(filePath));
                contentType = MediaTypeHeaderValue.Parse(MimeMapping.GetMimeMapping(extendedPath ?? string.Empty));
            }else
            {
                data = new ByteArrayContent(new byte[0]);
            }

            return StreamResponse(data, contentType);
        }

        /// <summary>
        /// Get index file from folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetIndexFilePath(string path)
        {
            return DefaultHtmlVariants.Select(file => Path.Combine(path, file)).FirstOrDefault(File.Exists);
        }

        /// <summary>
        /// Create http response with file stream
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private IHttpActionResult StreamResponse(HttpContent content, MediaTypeHeaderValue contentType, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new HttpResponseMessage(status)
            {
                Content =content
            };

            response.Content.Headers.ContentType = contentType;

            //Force web api to not add header cache-control: no-cache to response
            response.Headers.CacheControl = new CacheControlHeaderValue();

            return ResponseMessage(response);
        }
    }
}
