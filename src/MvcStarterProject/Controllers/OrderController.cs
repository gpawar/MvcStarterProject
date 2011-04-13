using System.Web.Mvc;
using HttpInterfaces;

namespace MvcStarterProject.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddItemToOrder(int productId)
        {
            return null;
        }

    }
}