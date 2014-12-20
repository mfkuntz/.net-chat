using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace aspChat.Controllers.Custom
{
    public class CamelCaseActionResult : ActionResult
    {
        private Object Data;
        public CamelCaseActionResult(object toSerialize)
        {
            Data = toSerialize;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            
            var response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/json; charset=utf-8";

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            response.Write(JsonConvert.SerializeObject(Data, jsonSerializerSettings));
            response.End();
        }
    }
}