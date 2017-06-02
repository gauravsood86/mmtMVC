using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mmtMVC.Models;
using System.Data.Entity.Validation;
using System.IO;
using System.Xml;
using mmtMVC.Utils;

namespace mmtMVC.Controllers
{
    public class TestController : Controller
    {
        private MMT_ConstEntities db = new MMT_ConstEntities();

        // GET: Test
        public ActionResult Index()
        {
            return View(db.mmtexams.ToList());
        }

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mmtexam mmtexam = db.mmtexams.Find(id);
            if (mmtexam == null)
            {
                return HttpNotFound();
            }
            return View(mmtexam);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(mmtexam mmtexam)
        {
            string logopath = "";
            string varnumbering = "0";
            if (ModelState.IsValid)
            {

                if (mmtexam.islogo == true)
                {
                    if (mmtexam.File != null)
                    {
                        HttpPostedFileBase quesimage = mmtexam.File;
                        if (quesimage.ContentLength != 0)
                        {
                            var fileNameques = Path.GetFileName(quesimage.FileName);
                            string typeques = Path.GetExtension(quesimage.FileName);
                            string path1 = System.Web.HttpContext.Current.Server.MapPath("/");
                            string logname = "img_2_" + typeques;
                            string fullpath = path1 + "/testlogo/" + logname;
                            quesimage.SaveAs(fullpath);
                            logopath = "testlogo/" + logname;
                        }
                    }
                    else
                    {
                        return View();
                    }
                }

                XmlDocument xmldocMaster = new XmlDocument();
                XmlDocument xmldoc = new XmlDocument();

                XmlElement xmlRoot, newelementtest;

                XmlDeclaration declaration = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(declaration);
                xmldoc.AppendChild(xmldoc.CreateElement("", "Customexam", ""));
                xmlRoot = xmldoc.DocumentElement;


                newelementtest = xmldoc.CreateElement("test");

                XmlElement vartest = xmldoc.CreateElement("Numbering");
                vartest.InnerText = varnumbering;
                newelementtest.AppendChild(vartest);

                XmlElement varreview = xmldoc.CreateElement("Review");
                varreview.InnerText = "";

                XmlAttribute indvidual = xmldoc.CreateAttribute("individual");
                indvidual.Value = "1";
                varreview.Attributes.Append(indvidual);

                XmlAttribute typemain = xmldoc.CreateAttribute("type");
                typemain.Value = "";
                varreview.Attributes.Append(typemain);

                XmlAttribute showmain = xmldoc.CreateAttribute("show");
                showmain.Value = "1";
                varreview.Attributes.Append(showmain);

                newelementtest.AppendChild(varreview);

                XmlElement vartimes = xmldoc.CreateElement("Time");
                vartimes.InnerText = "";
                newelementtest.AppendChild(vartimes);

                XmlAttribute timttype = xmldoc.CreateAttribute("show");
                timttype.Value = "1";

                vartimes.Attributes.Append(timttype);

                XmlElement varqueslevel = xmldoc.CreateElement("QuestionRatio");
                varqueslevel.InnerText = "";
                newelementtest.AppendChild(varqueslevel);

                XmlAttribute status = xmldoc.CreateAttribute("status");
                status.Value = "";
                varqueslevel.Attributes.Append(status);

                XmlAttribute quescounttype = xmldoc.CreateAttribute("quescounttype");
                quescounttype.Value = "1";
                varqueslevel.Attributes.Append(quescounttype);

                XmlAttribute level1 = xmldoc.CreateAttribute("level1");
                level1.Value = Request["ratio1"];
                varqueslevel.Attributes.Append(level1);

                XmlAttribute level2 = xmldoc.CreateAttribute("level2");
                level2.Value = Request["ratio2"];
                varqueslevel.Attributes.Append(level2);

                XmlAttribute level3 = xmldoc.CreateAttribute("level3");
                level3.Value = Request["ratio3"];
                varqueslevel.Attributes.Append(level3);

                XmlAttribute questioncount = xmldoc.CreateAttribute("questioncount");
                questioncount.Value = Request["totques"];
                varqueslevel.Attributes.Append(questioncount);

                XmlElement newelement1 = xmldoc.CreateElement("sections");
                newelement1.InnerText = "";
                newelementtest.AppendChild(newelement1);

                XmlElement questions = xmldoc.CreateElement("questions");
                questions.InnerText = "";
                newelementtest.AppendChild(questions);

                xmldoc.DocumentElement.AppendChild(newelementtest);


                mmtexam.uploaddate = DateTime.UtcNow.AddHours(5.5);
                mmtexam.catid = 99;
                mmtexam.addedby = 2;
                mmtexam.islive = false;
                mmtexam.passingmarks = 50;
                mmtexam.examlanguage = "English";
                mmtexam.masterxmlfile = xmldoc.OuterXml;
                mmtexam.path = logopath;
                db.mmtexams.Add(mmtexam);

                try
                {

                    db.SaveChanges();
                    int id = mmtexam.examid;

                    Utils.CACheckXML.CheckXML(id);


                    return RedirectToAction("Listing","Section", new {eid= id });
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
             
            }
            else
            {
                return View();
            }


           
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? eid)
        {
            if (eid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mmtexam mmtexam = db.mmtexams.Find(eid);
            if (mmtexam == null)
            {
                return HttpNotFound();
            }
            return View(mmtexam);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(mmtexam mmtexam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mmtexam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mmtexam);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mmtexam mmtexam = db.mmtexams.Find(id);
            if (mmtexam == null)
            {
                return HttpNotFound();
            }
            return View(mmtexam);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mmtexam mmtexam = db.mmtexams.Find(id);
            db.mmtexams.Remove(mmtexam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
