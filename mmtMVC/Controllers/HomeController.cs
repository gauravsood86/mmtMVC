using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mmtMVC.Models;

namespace mmtMVC.Controllers
{
    public class HomeController : Controller
    {
        private MMT_ConstEntities db = new MMT_ConstEntities();

        // GET: Home
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            


            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "examid_asc" : "";
            ViewBag.MasterSortParm = sortOrder == "uploaddate_desc" ? "uploaddate_asc" : "uploaddate_desc";
            ViewBag.NameSortParm = sortOrder == "examname_desc" ? "examname_asc" : "examname_desc";

            var c = (from e in db.mmtexams where e.addedby==2 select e).AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                c = c.Where(s => s.examname.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "examid_asc":
                    c = c.OrderBy(s => s.examid);
                    break;
                case "uploaddate_desc":
                    c = c.OrderBy(s => s.uploaddate);
                    break;
                case "examname_desc":
                    c = c.OrderByDescending(s => s.examname);
                    break;
                default:
                    c = c.OrderByDescending(s => s.examid);
                    break;
            }

            IList<TestList> results = c.Select(x => new TestList()
            {
                examid = x.examid,
                examname = x.examname,
                randomques = x.randomtest ?? false,
                uploaddate = x.uploaddate,
                FlagInfo = (from r in db.mmtexamflags where r.examid == x.examid select r).Select(t => new FlagInfo()
                {
                    flagerror = t.flagerror,
                    flagcount = t.flagcount ?? 0,
                }).ToList()

            }).ToList();
           
            return View(results.ToList());
        }
    }
}