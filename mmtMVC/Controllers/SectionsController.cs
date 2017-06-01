using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mmtMVC.Models;
using System.Xml;
using System.Xml.Linq;

namespace mmtMVC.Controllers
{
    public class SectionsController : Controller
    {
        private MMT_ConstEntities db = new MMT_ConstEntities();

        // GET: Sections
        public ActionResult Listing(int? id)
        {

            if (id != null)
            {
                Session["examid"] = id;
            }
            else
            {
                if (Session["examid"] != null)
                {
                    id = (int)Session["examid"];
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            var mmtsections = (from e in db.mmtsections where e.examid == id select e).AsEnumerable();
            return View(mmtsections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mmtsection mmtsection = db.mmtsections.Find(id);

            if (mmtsection.sectionname == "-----")
            {
                mmtsection.sectionname = "";
                mmtsection.configured = false;
            }
            else
            {
                mmtsection.configured = true;
            }

            if (mmtsection == null)
            {
                return HttpNotFound();
            }
            return View(mmtsection);
        }


        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {

            if (Session["examid"] == null)
            {

            }
            string vartxtSectionName = "-----";
            int sectioncount = 0;
            string vartxtDesc = "";
            string vartxtTime = "00:00:00";
            string vardrpShowintro = "0";
            string vardrpSectionTitle = "1";
            string vardrpMark = "1";
            string vardrpReview = "0";
            string vardrpQuesCompu = "0";
            string vardrpOptRandom = "0";
            mmtexam std = null;
            using (var c = new MMT_ConstEntities())
            {
                int eid = Convert.ToInt32(Session["examid"]);
                sectioncount = (from e in c.mmtsections where e.examid == eid select e).Count();
                sectioncount++;

                std = (from s in db.mmtexams
                       where s.examid == eid
                       select s).FirstOrDefault<mmtexam>();

            }

            string value = Request.Form["ddlsections"];
            if (value != "")
            {
                mmtsection section = new mmtsection();

                if (value == "1")
                {
                    section.type = type.MCQ;

                }
                else if (value == "6")
                {
                    section.type = type.SUBJECTIVE;

                }
                else
                {
                    section.type = type.CODEASSESS;

                }

                section.examid = (int)Session["examid"];
                section.sectionname = "-----";
                section.sectionid = sectioncount;
                section.active = true;
                db.mmtsections.Add(section);
                db.SaveChanges();
                int secid = section.secid;



                XmlNode xmlRoot;
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(std.masterxmlfile);
                xmlRoot = xmldoc.SelectSingleNode("//test//sections");


                XmlElement sectionx = xmldoc.CreateElement("section");
                sectionx.InnerText = "";
                xmlRoot.AppendChild(sectionx);

                XmlAttribute xmlsectionId = xmldoc.CreateAttribute("id");
                xmlsectionId.Value = sectioncount.ToString();
                sectionx.Attributes.Append(xmlsectionId);

                XmlAttribute sectype = xmldoc.CreateAttribute("type");
                sectype.Value = value;
                sectionx.Attributes.Append(sectype);

                XmlAttribute auto = xmldoc.CreateAttribute("auto");
                auto.Value = "0";
                sectionx.Attributes.Append(auto);

                XmlAttribute xmlcount = xmldoc.CreateAttribute("count");
                xmlcount.Value = "";
                sectionx.Attributes.Append(xmlcount);

                XmlAttribute xmlintro = xmldoc.CreateAttribute("showintro");
                xmlintro.Value = vardrpShowintro;
                sectionx.Attributes.Append(xmlintro);

                XmlAttribute xmltitle = xmldoc.CreateAttribute("showtitle");
                xmltitle.Value = vardrpSectionTitle;
                sectionx.Attributes.Append(xmltitle);

                XmlAttribute xmlmark = xmldoc.CreateAttribute("showmark");
                xmlmark.Value = vardrpMark;
                sectionx.Attributes.Append(xmlmark);

                XmlAttribute xmlreview = xmldoc.CreateAttribute("showreview");
                xmlreview.Value = vardrpReview;
                sectionx.Attributes.Append(xmlreview);

                XmlAttribute xmlvarquestioncompulsory = xmldoc.CreateAttribute("questioncompulsory");
                xmlvarquestioncompulsory.Value = vardrpQuesCompu;
                sectionx.Attributes.Append(xmlvarquestioncompulsory);

                XmlAttribute xmlvarqrandom = xmldoc.CreateAttribute("randomizeoptions");
                xmlvarqrandom.Value = vardrpOptRandom;
                sectionx.Attributes.Append(xmlvarqrandom);

                string negscore = "0";

                //if (DropDownList1.SelectedValue == "1")
                //{
                //    negscore = txtnegative.Text;
                //}

                XmlAttribute isnegative = xmldoc.CreateAttribute("isnegative");
                //   isnegative.Value = DropDownList1.SelectedValue;
                isnegative.Value = "0";
                sectionx.Attributes.Append(isnegative);

                XmlAttribute negativescore = xmldoc.CreateAttribute("negativescore");
                negativescore.Value = negscore;
                sectionx.Attributes.Append(negativescore);


                XmlElement vartitle = xmldoc.CreateElement("section_title");
                vartitle.InnerText = vartxtSectionName;
                sectionx.AppendChild(vartitle);

                XmlElement vardescsec = xmldoc.CreateElement("section_description");
                vardescsec.InnerText = vartxtDesc;
                sectionx.AppendChild(vardescsec);

                XmlElement vartime1 = xmldoc.CreateElement("Time");
                if (value == "8")
                {

                    vartime1.InnerText = "00:00:00";

                }
                else
                {

                    vartime1.InnerText = vartxtTime;

                }

                if (value == "3")
                {
                    XmlAttribute rectime = xmldoc.CreateAttribute("rectime");
                    rectime.Value = "";
                    vartime1.Attributes.Append(rectime);
                }

                sectionx.AppendChild(vartime1);

                XmlElement quesratio = xmldoc.CreateElement("QuestionRatio");
                quesratio.InnerText = "";
                sectionx.AppendChild(quesratio);

                XmlAttribute sttype = xmldoc.CreateAttribute("type");
                sttype.Value = "1";
                quesratio.Attributes.Append(sttype);

                XmlAttribute secstatus = xmldoc.CreateAttribute("status");
                secstatus.Value = "";
                quesratio.Attributes.Append(secstatus);

                XmlAttribute seclevel1 = xmldoc.CreateAttribute("level1");
                seclevel1.Value = "";
                quesratio.Attributes.Append(seclevel1);

                XmlAttribute seclevel2 = xmldoc.CreateAttribute("level2");
                seclevel2.Value = "";
                quesratio.Attributes.Append(seclevel2);

                XmlAttribute seclevel3 = xmldoc.CreateAttribute("level3");
                seclevel3.Value = "";
                quesratio.Attributes.Append(seclevel3);

                XmlAttribute secquestioncount = xmldoc.CreateAttribute("questioncount");
                secquestioncount.Value = "";
                quesratio.Attributes.Append(secquestioncount);

                XmlElement topics = xmldoc.CreateElement("topics");
                topics.InnerText = "";
                sectionx.AppendChild(topics);

                mmttopic checktop = (from t in db.mmttopics
                                     where t.examid != section.examid && t.secid == section.secid
                                     select t).FirstOrDefault<mmttopic>();
                if (checktop == null)
                {
                    mmttopic newtopic = new mmttopic();
                    newtopic.topicname = "New Topic";
                    newtopic.secid = section.secid;
                    newtopic.sectionid = section.sectionid ?? 0;
                    newtopic.examid = section.examid ?? 0;
                    newtopic.active = true;
                    db.mmttopics.Add(newtopic);
                    db.SaveChanges();
                    int topicid = newtopic.topicid;

                    XmlElement topic = xmldoc.CreateElement("topic");
                    topic.InnerText = "New Topic";
                    topics.AppendChild(topic);

                    XmlAttribute xmlid = xmldoc.CreateAttribute("id");
                    xmlid.Value = "1";
                    topic.Attributes.Append(xmlid);

                    XmlAttribute xmlsectionIdx = xmldoc.CreateAttribute("sectionid");
                    xmlsectionIdx.Value = section.sectionid.ToString();
                    topic.Attributes.Append(xmlsectionIdx);

                    XmlAttribute xmlcountx = xmldoc.CreateAttribute("topicid");
                    xmlcountx.Value = topicid.ToString();
                    topic.Attributes.Append(xmlcountx);

                    XmlAttribute xmlparagraph_id = xmldoc.CreateAttribute("level1");
                    xmlparagraph_id.Value = "";
                    topic.Attributes.Append(xmlparagraph_id);

                    XmlAttribute level2 = xmldoc.CreateAttribute("level2");
                    level2.Value = "";
                    topic.Attributes.Append(level2);


                    XmlAttribute level3 = xmldoc.CreateAttribute("level3");
                    level3.Value = "";
                    topic.Attributes.Append(level3);

                    XmlAttribute questioncount = xmldoc.CreateAttribute("questioncount");
                    questioncount.Value = "";
                    topic.Attributes.Append(questioncount);
                }
                //if (value == "3")
                //{
                //    XmlElement codeassess = xmldoc.CreateElement("codeassess");
                //    codeassess.InnerText = "";
                //    sectionx.AppendChild(codeassess);

                //    XmlNode SubNode;
                //    SubNode = xmldoc.CreateElement("exam");


                //    XmlAttribute Subnode_attribute_examid = xmldoc.CreateAttribute("examid");
                //    Subnode_attribute_examid.Value = Session["customexamid"].ToString();
                //    SubNode.Attributes.Append(Subnode_attribute_examid);


                //    XmlAttribute Subnode_attribute_langcode = xmldoc.CreateAttribute("langcode");
                //    string[] arr = DDLProgLang.SelectedValue.Split('-');
                //    Subnode_attribute_langcode.Value = arr[0].ToString();
                //    SubNode.Attributes.Append(Subnode_attribute_langcode);


                //    XmlAttribute Subnode_attribute_lang = xmldoc.CreateAttribute("lang");
                //    Subnode_attribute_lang.Value = arr[1].ToString();
                //    SubNode.Attributes.Append(Subnode_attribute_lang);

                //    //XmlAttribute Subnode_attribute_downtime = xmldoc.CreateAttribute("downtime");
                //    //Subnode_attribute_downtime.Value = txtExamid.Text;
                //    //SubNode.Attributes.Append(Subnode_attribute_downtime);


                //    XmlAttribute Subnode_attribute_sublangcodes = xmldoc.CreateAttribute("sublangcodes");
                //    string arr_sublang = "";

                //    if (LB_subLang.GetSelectedIndices().Count() > 0)
                //    {
                //        for (int k = 0; k < LB_subLang.Items.Count; k++)
                //        {
                //            if (LB_subLang.Items[k].Selected == true)
                //                arr_sublang += Convert.ToInt32(LB_subLang.Items[k].Value) + ",";
                //        }
                //        arr_sublang = arr_sublang.Remove(arr_sublang.Length - 1, 1);
                //    }
                //    else
                //        arr_sublang = "0";
                //    Subnode_attribute_sublangcodes.Value = arr_sublang;
                //    SubNode.Attributes.Append(Subnode_attribute_sublangcodes);


                //    XmlAttribute Subnode_attribute_showtestcases = xmldoc.CreateAttribute("showtestcases");
                //    string is_TestCase = "N";

                //    Subnode_attribute_showtestcases.Value = is_TestCase;
                //    SubNode.Attributes.Append(Subnode_attribute_showtestcases);


                //    XmlAttribute Subnode_attribute_sectionaltest = xmldoc.CreateAttribute("sectionaltest");
                //    string is_sectionaltest = "Y";
                //    Subnode_attribute_sectionaltest.Value = is_sectionaltest;
                //    SubNode.Attributes.Append(Subnode_attribute_sectionaltest);
                //    codeassess.AppendChild(SubNode);
                //}


                XmlElement autoset = xmldoc.CreateElement("autosettings");
                autoset.InnerText = "";
                sectionx.AppendChild(autoset);
                xmlRoot.AppendChild(sectionx);


                std.masterxmlfile = xmldoc.OuterXml;
                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();

                Utils.CACheckXML.CheckXML(section.examid ?? 0);
            }
            else
            {

                TempData["ErrorMessage"] = " (Please select section to add)";
                return RedirectToAction("Listing", "Sections", new { id = (int)Session["examid"] });
            }

            //    db.mmtsections.Add(mmtsection);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");


            //ViewBag.examid = new SelectList(db.mmtexams, "examid", "examname", mmtsection.examid);
            //return View(mmtsection);


            return RedirectToAction("Listing", "Sections", new { id = (int)Session["examid"] });
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(string secid , string examid)
        {
            if (secid == null || secid == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (examid == null || examid == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            mmtsection mmtsection = db.mmtsections.Find(Convert.ToInt32(secid));

            if (mmtsection.sectionname == "-----")
            {
                mmtsection.sectionname = "";
                mmtsection.configured = false;
            }
            else
            {
                mmtsection.configured = true;
            }

            if (mmtsection == null)
            {
                return HttpNotFound();
            }

            int eid = Convert.ToInt32(examid);

            mmtexam std = (from s in db.mmtexams
                           where s.examid == eid
                           select s).FirstOrDefault<mmtexam>();
            

            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlRoot;
            xmldoc.LoadXml(std.masterxmlfile);
            xmlRoot = xmldoc.SelectSingleNode("//test//sections//section[@id=" + mmtsection.sectionid.ToString() + "]");
            mmtsection.review = Convert.ToInt32(xmlRoot.Attributes["showreview"].Value);
            mmtsection.compulsory = Convert.ToInt32(xmlRoot.Attributes["questioncompulsory"].Value);
            mmtsection.random  = Convert.ToInt32(xmlRoot.Attributes["randomizeoptions"].Value);
            mmtsection.auto =  Convert.ToInt32(xmlRoot.Attributes["auto"].Value);

            

            ViewBag.examid = new SelectList(db.mmtexams, "examid", "examname", mmtsection.examid);
            return View(mmtsection);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( mmtsection mmtsection)
        {
            if (ModelState.IsValid)
            {
                mmtsection.active = true;
                db.Entry(mmtsection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listing", "Sections", new { id = (int)Session["examid"] });
            }

            mmtsection mmtrebind = db.mmtsections.Find(Convert.ToInt32(mmtsection.secid));
            mmtsection.type = mmtrebind.type;
            ViewBag.examid = new SelectList(db.mmtexams, "examid", "examname", mmtsection.examid);
            return View(mmtsection);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mmtsection mmtsection = db.mmtsections.Find(id);
            if (mmtsection == null)
            {
                return HttpNotFound();
            }
            return View(mmtsection);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mmtsection mmtsection = db.mmtsections.Find(id);
            db.mmtsections.Remove(mmtsection);
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
