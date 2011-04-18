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
        private readonly IOrderProcessor _orderProcessor;
        private readonly IGetObjectService<Order> _getOrderService;

        public OrderController(IGetProductService getProductService,
            IHttpSession session, IOrderProcessor orderProcessor,
            IGetObjectService<Order> getOrderService)
        {
            _session = session;
            _orderProcessor = orderProcessor;
            _getOrderService = getOrderService;
            _getProductService = getProductService;
        }

        public ActionResult Index()
        {
            var model = new OrderIndexViewModel();
            model.AvailableProducts = _getProductService.GetAvailableProducts();

            var orderId = (int?) _session["OrderId"];
            if (orderId != null)
            {
                var order = _getOrderService.Get(orderId.Value);
                model.ProductsInOrder = order.Products;
                model.SubtotalBeforeTaxAndShipping = _orderProcessor.SubtotalBeforeTaxAndShipping(order);
                model.ShippingCharges = _orderProcessor.ShippingCharges(order);
                model.Tax = _orderProcessor.Tax(order);
                model.TotalPrice = _orderProcessor.TotalPrice(order);
            }
            else
            {
                model.ProductsInOrder = new List<Product>();
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddItemToOrder(int productId)
        {
            return null;
        }

    }
}