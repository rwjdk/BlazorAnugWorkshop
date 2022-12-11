using Microsoft.AspNetCore.Http;

namespace X_BonusBackEndAzureFunction.Logic;

public class UserIdController
{
    public string GetUserId(HttpRequest req)
    {
        return req.Headers["UserId"].ToString();
    }
}