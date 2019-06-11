using System;using TeacherAssistant.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeachersAssistant.Domain.Model.Models;
using TeacherAssistant.Infrastructure.ExtensionMethods;
using TeacherAssistant.Infrastructure;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Services.Concretes;
using TeacherAssistant.Models;
using System.Text.RegularExpressions;
using UPAEventsPayPal;
using CuttingEdge.Conditions;

namespace TeachersAssistant.Controllers
{
    public class ShoppingController : Controller
    {
        TeachersAssistantRepositoryServices _repositoryServices;
        public ShoppingController()
        {

        }

        public ShoppingController(ICalendarBookingRepositoryMarker calendarRepositoryMarker,
            IClassroomRepositoryMarker classroomRepositoryMarker,
            IFreeDocumentRepositoryMarker freeDocumentRepositoryMarker,
            IFreeDocumentStudentRepositoryMarker freeDocumentStudentRepositoryMarker,
            IFreeVideoRepositoryMarker freeVideoRepositoryMarker,
            IFreeVideoStudentRepositoryMarker freeVideoStudentRepositoryMarker,
            IPaidDocuemtStudentRepositoryMarker paidDocuemtStudentRepositoryMarker,
            IPaidDocumentRepositoryMarker paidDocumentRepositoryMarker,
            IPaidVideoRepositoryMarker paidVideoRepositoryMarker,
            IPaidVideoStudentRepositoryMarker paidVideoStudentRepositoryMarker,
            IStudentRepositoryMarker studentRepositoryMarker,
            IStudentTypeRepositoryMarker studentTypeRepositoryMarker,
            ISubjectRepositoryMarker subjectRepositoryMarker,
            ITeacherRepositoryMarker teacherRepositoryMarker,
            IBookingTimeRepositoryMarker bookingRepositoryMarker,
            IStudentResourceRepositoryMarker studentResourceRepositoryMarker,
            IQAHelpRequestRepositoryMarker qAHelpRequestRepositoryMarker,
            IAssignmentRepositoryMarker assignmentRepositoryMarker,
            IAssignmentSubmissionRepositoryMarker assignmentSubmissionRepositoryMarker,
            ICourseRepositoryMarker courseRepositoryMarker)
        {
            Condition.Requires<ICalendarBookingRepositoryMarker>(calendarRepositoryMarker, "calendarRepositoryMarker").IsNotNull();
            Condition.Requires<IClassroomRepositoryMarker>(classroomRepositoryMarker, "classroomRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeDocumentRepositoryMarker>(freeDocumentRepositoryMarker, "freeDocumentRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeDocumentStudentRepositoryMarker>(freeDocumentStudentRepositoryMarker, "freeDocumentStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeVideoRepositoryMarker>(freeVideoRepositoryMarker, "freeVideoRepositoryMarker").IsNotNull();
            Condition.Requires<IFreeVideoStudentRepositoryMarker>(freeVideoStudentRepositoryMarker, "freeVideoStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidDocuemtStudentRepositoryMarker>(paidDocuemtStudentRepositoryMarker, "paidDocuemtStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidDocumentRepositoryMarker>(paidDocumentRepositoryMarker, "paidDocumentRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidVideoRepositoryMarker>(paidVideoRepositoryMarker, "paidVideoRepositoryMarker").IsNotNull();
            Condition.Requires<IPaidVideoStudentRepositoryMarker>(paidVideoStudentRepositoryMarker, "paidVideoStudentRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentRepositoryMarker>(studentRepositoryMarker, "studentRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentTypeRepositoryMarker>(studentTypeRepositoryMarker, "studentTypeRepositoryMarker").IsNotNull();
            Condition.Requires<ISubjectRepositoryMarker>(subjectRepositoryMarker, "subjectRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentTypeRepositoryMarker>(studentTypeRepositoryMarker, "teacherRepositoryMarker").IsNotNull();
            Condition.Requires<IBookingTimeRepositoryMarker>(bookingRepositoryMarker, "bookingRepositoryMarker").IsNotNull();
            Condition.Requires<IStudentResourceRepositoryMarker>(studentResourceRepositoryMarker, "studentResourceRepositoryMarker").IsNotNull();
            Condition.Requires<IQAHelpRequestRepositoryMarker>(qAHelpRequestRepositoryMarker, "qaHelpRequestRepositoryMarker").IsNotNull();
            Condition.Requires<IAssignmentRepositoryMarker>(assignmentRepositoryMarker, "assignmentSubmissionRepositoryMarker").IsNotNull();
            Condition.Requires<IAssignmentSubmissionRepositoryMarker>(assignmentSubmissionRepositoryMarker, "assignmentSubmissionRepositoryMarker").IsNotNull();
            Condition.Requires<ICourseRepositoryMarker>(courseRepositoryMarker, "courseRepositoryMarker").IsNotNull();
            var unitOfWork = new TeachersAssistantUnitOfWork(calendarRepositoryMarker,
             classroomRepositoryMarker,
             freeDocumentRepositoryMarker,
             freeDocumentStudentRepositoryMarker,
             freeVideoRepositoryMarker,
             freeVideoStudentRepositoryMarker,
             paidDocuemtStudentRepositoryMarker,
             paidDocumentRepositoryMarker,
             paidVideoRepositoryMarker,
             paidVideoStudentRepositoryMarker,
             studentRepositoryMarker,
             studentTypeRepositoryMarker,
             subjectRepositoryMarker,
             teacherRepositoryMarker,
             bookingRepositoryMarker,
             studentResourceRepositoryMarker,
             qAHelpRequestRepositoryMarker,
             assignmentRepositoryMarker,
             assignmentSubmissionRepositoryMarker,
             courseRepositoryMarker);

            unitOfWork.InitializeDbContext(new TeachersAssistant.DataAccess.TeachersAssistantDbContext());
            _repositoryServices = new TeachersAssistantRepositoryServices(unitOfWork);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Basket(int id = 1)
        {
            var page = id;
            ViewBag.Message = "Basket page";
            ViewBag.Selected = "basket";
            ViewBag.Message = "Basket page";
            ViewBag.LoggedIn = ControllerContext.HttpContext.User.Identity.IsAuthenticated;

            System.Web.HttpContext.Current.Session["CurrentPageBasket"] = page;

            var model = (IEnumerable<SHOP_PRODS>)System.Web.HttpContext.Current.Session["ShoppingBasket"];

            if (model == null)
            {
                model = new List<SHOP_PRODS>();
                System.Web.HttpContext.Current.Session["ShoppingBasket"] = model;
            }
            const int numberOfItemsPerPage = 3;
            const int numberOfPagesToDisplay = 3;
            var shopProdss = model as SHOP_PRODS[] ?? model.ToArray();
            var numberOfPages = shopProdss.Count() / numberOfItemsPerPage;
            var modNumberOfPages = shopProdss.Count() % numberOfItemsPerPage;

            if (System.Web.HttpContext.Current.Session["PagingInfo2"] == null)
            {
                if (modNumberOfPages > 0) numberOfPages += 1;
                var pagingMechanism = InitialisePageInfoCurrent(numberOfPages, numberOfPagesToDisplay);
                System.Web.HttpContext.Current.Session["PagingInfo2"] = pagingMechanism;
            }
            else
            {
                if (modNumberOfPages > 0) numberOfPages += 1;
                var pagingMechanism = (Paging)System.Web.HttpContext.Current.Session["PagingInfo2"];
                pagingMechanism.PagingInfoDetails.NumberOfPages = numberOfPages;
            }
            System.Web.HttpContext.Current.Session["CurrentPageBasket"] = System.Web.HttpContext.Current.Session["CurrentPageBasket"] == null ? 1 : (int)System.Web.HttpContext.Current.Session["CurrentPageBasket"];
            var paging = System.Web.HttpContext.Current.Session["PagingInfo2"] as Paging;
            if (paging != null)
            {
                paging.PagingInfoDetails.CurrentPage = (int)System.Web.HttpContext.Current.Session["CurrentPageBasket"];
                paging.PagingInfoDetails.CurrentPageIndex =
                    (int)System.Web.HttpContext.Current.Session["CurrentPageBasket"] - 1;
            }

            var skip = ((System.Web.HttpContext.Current.Session["PagingInfo2"] as Paging).PagingInfoDetails.CurrentPage - 1) * 3;

            var model2 = shopProdss.Skip(skip).Take(((Paging)System.Web.HttpContext.Current.Session["PagingInfo2"]).PagingInfoDetails.NumberOfItemsPerPage);


            ViewBag.Title = "Basket Contents";
            return View(model2.ToArray());


        }

        [System.Web.Mvc.HttpPost]
        public ActionResult BasketRemoveProduct(SHOP_PRODS product)
        {
            ViewBag.Title = "Basket Contents";
            ViewBag.Message = "Basket page";
            ViewBag.Selected = "basket";
            ViewBag.LoggedIn = ControllerContext.HttpContext.User.Identity.IsAuthenticated;

            var model = (IList<SHOP_PRODS>)System.Web.HttpContext.Current.Session["ShoppingBasket"];
            if (model != null)
            {
                if (product != null)
                {
                    var prod = model.SingleOrDefault(p => p.prodId == product.prodId);
                    model.Remove(prod);
                    System.Web.HttpContext.Current.Session["ShoppingBasket"] = model;
                }
            }
            else
            {
                model = new List<SHOP_PRODS>();
                System.Web.HttpContext.Current.Session["ShoppingBasket"] = model;
            }
            return RedirectToAction("Basket");
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult AddToBasket(int productId)
        {
            var product = _repositoryServices.GetShoppingProdsList().FirstOrDefault(p => p.prodId.Equals(productId));
            var basket = (IList<SHOP_PRODS>)System.Web.HttpContext.Current.Session["ShoppingBasket"];
            if (basket == null)
            {
                basket = new List<SHOP_PRODS>();
                System.Web.HttpContext.Current.Session["ShoppingBasket"] = basket;
            }
            if (basket.FirstOrDefault(p => p.prodId.Equals(productId)) == null)
                basket.Add(product);
            System.Web.HttpContext.Current.Session["ShoppingBasket"] = basket;

            ViewBag.Title = "Basket Contents";
            return Json(new { productId = productId, Success = true });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            ViewBag.Title = "Payment";
            ViewBag.Selected = "payment";
            ViewBag.LoggedIn = ControllerContext.HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Message = "Payment";
            
            return View();
        }
        [HttpGet]
        public ActionResult ShopContentListing()
        {
            var shopProds = _repositoryServices.GetShoppingProdsList().ToArray();
            var viewModel = (ProductViewModel[])AutoMapper.Mapper.Map(shopProds, typeof(SHOP_PRODS[]), typeof(ProductViewModel[]));
            return View("ShopContentListing", viewModel);
        }

        [HttpPost]
        public ActionResult ShopContentListing(ProductViewModel boughtProduct)
        {
            var shopProduct = (SHOP_PRODS)AutoMapper.Mapper.Map(boughtProduct, typeof(ProductViewModel), typeof(SHOP_PRODS));
            return AddToBasket(shopProduct.prodId);
        }
        private Paging InitialisePageInfoCurrent(int numberOfPages, int numberOfPagesToDisplay)
        {
            return new Paging(new PagingInfo
            {
                CurrentPage = 1,
                CurrentPageIndex = 0,
                NumberOfPages = numberOfPages,
                NumberOfPagesToDisplay = numberOfPagesToDisplay,
                NumberOfItemsPerPage = 3
            });
        }

        [System.Web.Mvc.Authorize]
        public ActionResult PostToPaypal(FormCollection forms)
        {
            var emailRegEx = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            if (forms["buyerEmail"] == null || !Regex.IsMatch(forms["buyerEmail"], emailRegEx))
            {
                ModelState.AddModelError("", "Your email is not in correct format");
            }
            if (System.Web.HttpContext.Current.Session["ShoppingBasket"] == null) return RedirectToAction("Basket", "Shopping");
            var shoppingBasket = (IList<SHOP_PRODS>)System.Web.HttpContext.Current.Session["ShoppingBasket"];

            var upaProducts = shoppingBasket.Select(item => new { ProductPrice = item.prodPrice, ProductDescription = item.prodDesc, ProductName = item.prodName }).ToList();
            var totalAmount = shoppingBasket.Sum(item => item.prodPrice);

            if (!(totalAmount > 0))
            {
                ModelState.AddModelError("", "Gross total should be more than $0.00");
            }

            if (!ModelState.IsValid)
            {
                return View("Payment");
            }
            var order = new Order();
            order.email = forms["email"];
            order.order_date = DateTime.Now;
            order.username = User.Identity.Name;
            order.status = "Unpaid";
            order.order_gross = totalAmount;

            _repositoryServices.SaveOrUpdateOrders(order);

            System.Web.HttpContext.Current.Session["InvoiceNo"] = order.orderId;
            System.Web.HttpContext.Current.Session["ProductsUPA"] = upaProducts.Select(p => new UPAEventsPayPal.Product { ProductDescription = p.ProductDescription, ProductName = p.ProductName, Ammount = p.ProductPrice, Quantity = 1 }).ToList();
            System.Web.HttpContext.Current.Session["buyerEmail"] = order.email;

            foreach (var product in shoppingBasket)
            {
                _repositoryServices.SaveOrUpdateItemOrders(new ItemOrder { numberOrdered = 1, order_id_fk = order.orderId, product_name = product.prodName, username = User.Identity.Name });
                
            }
            var context = HttpContext;
            //Process Payment
            var paypal = new PayPalHandler(context.ApplicationInstance.Context.Session, System.Configuration.ConfigurationManager.AppSettings["PaypalBaseUrl"],
                System.Configuration.ConfigurationManager.AppSettings["BusinessEmail"],
                System.Configuration.ConfigurationManager.AppSettings["SuccessUrl"],
                System.Configuration.ConfigurationManager.AppSettings["CancelUrl"],
                System.Configuration.ConfigurationManager.AppSettings["NotifyUrl"]);

            paypal.Response = context.ApplicationInstance.Context.Response;


            paypal.RedirectToPayPal();
            return View("PaymentMade");
        }


    }
}
