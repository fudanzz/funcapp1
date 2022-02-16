using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace openhack
{
    public static class helloworld
    {
        [FunctionName("product")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string productID = req.Query["productID"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            productID = productID ?? data?.productID;

            string responseMessage = string.IsNullOrEmpty(productID)
                ? "pls provide product id for your query!"
                : $"The product name for your product id {productID} is Starfruit Explosion123";

            return new OkObjectResult(responseMessage);
        }
    }
}
