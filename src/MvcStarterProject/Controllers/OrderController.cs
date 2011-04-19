using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly ISaveObjectService<Order> _saveOrderService;

        public OrderController(IGetProductService getProductService,
            IHttpSession session, IOrderProcessor orderProcessor,
            IGetObjectService<Order> getOrderService,
            ISaveObjectService<Order> saveOrderService)
        {
            _session = session;
            _orderProcessor = orderProcessor;
            _getOrderService = getOrderService;
            _saveOrderService = saveOrderService;
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

        public ActionResult AddToOrder(int productId)
        {
            var product = _getProductService.Get(productId);
            var orderId = (int?) _session["OrderId"];
            if (orderId != null)
            {
                var order = _getOrderService.Get(orderId.Value);
                order.Products.Add(product);
                _saveOrderService.Update(order);
            }
            else
            {
                var order = new Order();
                order.Products.Add(product);
                _saveOrderService.Create(order);
                _session["OrderId"] = order.OrderId;
            }
            return RedirectToAction("Index");
        }

        public ActionResult NewOrder()
        {
            _session["OrderId"] = null;
            return RedirectToAction("Index");
        }
    }
}