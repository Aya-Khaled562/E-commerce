
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //I use this controller for not found errors (route not found)
    [Route("errors/{code}")]
    // I Add this line to make swagger ignore this controller becouse Error
    // Action don't have any explicit bindings
    [ApiExplorerSettings(IgnoreApi = true)] 
    public class ErrorController: BaseApiController
    {
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}