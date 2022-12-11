using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using SharedModels.Model;
using X_BonusBackEndAzureFunction.Logic;

namespace X_BonusBackEndAzureFunction;

public static class FunctionAddUpdateBookmark
{
    [FunctionName("AddUpdateBookmark")]
    public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
    {
        try
        {
            var userIdController = new UserIdController();
            var userId = userIdController.GetUserId(req);
            var blobController = new BlobController(userId);
            var currentBookmarks = blobController.GetBookmarks();

            var payloadAsJson = await new StreamReader(req.Body).ReadToEndAsync();

            var sentBookmark = JsonConvert.DeserializeObject<Bookmark>(payloadAsJson);
            var existingBookmark = currentBookmarks.FirstOrDefault(x => x.Guid == sentBookmark.Guid);
            if (existingBookmark != null)
            {
                existingBookmark.Title = sentBookmark.Title;
                existingBookmark.Description = sentBookmark.Description;
                existingBookmark.Url = sentBookmark.Url;
            }
            else
            {
                currentBookmarks.Add(sentBookmark);
            }
            blobController.UpdateBlob(JsonConvert.SerializeObject(currentBookmarks));

            return new OkObjectResult("OK");
        }
        catch (Exception e)
        {
            return new BadRequestErrorMessageResult(e.ToString());
        }
    }
}