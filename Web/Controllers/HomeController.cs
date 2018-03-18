using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //getcountbook
        //var total = db.Books.Select(q => q.BookId).Count();

        public ActionResult DashBoard()
        {
            var _context = new ApplicationDbContext();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from c in _context.Books select c);
            results.ToList().ForEach(rs => xValue.Add(rs.Title));
            results.ToList().ForEach(rs => yValue.Add(rs.Quantity));

            new Chart(width: 800, height: 400, theme: ChartTheme.Blue)
            .AddTitle("Chart for number of outstaning")
            .AddSeries("Default", chartType: "Column", xValue: xValue, yValues: yValue)
            .Write("bmp");

            return null;

        }
    }
}