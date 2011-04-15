using System.Collections.Generic;
using System.Web.Mvc;
using HttpInterfaces;
using MvcStarterProject.Business;
using MvcStarterProject.Models;

namespace MvcStarterProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IGetProductService _getProductService;
        private IHttpSession _session;

        public OrderController(IGetProductService getProductService,
            IHttpSession session)
        {
            _session = session;
            _getProductService = getProductService;
        }

        public ActionResult Index()
        {
            var model = new OrderIndexViewModel();
            model.AvailableProducts = _getProductService.GetAvailableProducts();
            model.ProductsInOrder = (IList<Product>)_session["ProductsInOrder"];
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddItemToOrder(int productId)
        {
            return null;
        }

    }
}