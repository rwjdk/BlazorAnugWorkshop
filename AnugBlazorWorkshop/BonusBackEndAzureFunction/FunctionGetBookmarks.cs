using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using X_BonusBackEndAzureFunction.Logic;

namespace X_BonusBackEndAzureFunction
{
    public static class FunctionGetBookmarks
    {
        [FunctionName("GetBookmarks")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            await Task.CompletedTask;
            var userIdController = new UserIdController();
            var userId = userIdController.GetUserId(req);
            var blobController = new BlobController(userId);
            return new OkObjectResult(blobController.GetBlobContent());
        }
    }
}
