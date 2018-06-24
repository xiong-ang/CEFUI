using CefServer.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CefServer
{
    public class OperaController : ApiController
    {
        [Route("Operation")]
        [HttpPost]
        public IHttpActionResult Invoke([FromBody] Operation opera)
        {
            return null;
        }
    }
}
