using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace aspChat.Controllers.Custom
{
    public class CamelCaseContent : HttpContent
    {
        private readonly MemoryStream _stream = new MemoryStream();

        public CamelCaseContent(object data)
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var converted = JsonConvert.SerializeObject(data, jsonSerializerSettings);

            StreamWriter writer = new StreamWriter(_stream);
            
                writer.Write("hi");
                writer.Flush();
               
            
             _stream.Position = 0;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return _stream.CopyToAsync(_stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _stream.Length;
            return true;
        }
    }
}