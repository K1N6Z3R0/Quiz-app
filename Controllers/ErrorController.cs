using System.Web.Mvc;

namespace YourNamespace.Controllers
{
    public class ErrorController : Controller
    {
        // Handle 404 - Page Not Found
        public ActionResult NotFound()
        {
            Response.StatusCode = 404; // Ensure the status code is set to 404
            return View();
        }

        // Handle 500 - Internal Server Error
        public ActionResult InternalError()
        {
            Response.StatusCode = 500; // Ensure the status code is set to 500
            return View();
        }

        // General Error Handler
        public ActionResult General()
        {
            return View();
        }
    }
}
