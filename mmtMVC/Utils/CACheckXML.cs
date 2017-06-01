using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Xml;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Data.SqlClient;
using System.Reflection;
using mmtMVC.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

/// <summary>
/// Summary description for MMTCheckXML
/// </summary>
/// 
namespace mmtMVC.Utils
{
    public class CACheckXML
    {

        public static string CheckXML(int id)
        {
            var db = new MMT_ConstEntities();


            mmtexam std = (from s in db.mmtexams
                           where s.examid == id
                           select s).FirstOrDefault<mmtexam>();



            XmlDocument xmldoc = new XmlDocument();
            bool flag = false;
            int flagcount = 0;
            string errorstring = "<ul>";
            string testtime = "";
            if (std.masterxmlfile != "" || std.masterxmlfile != null)
            {
                xmldoc.LoadXml(std.masterxmlfile);
                XDocument xmldocMaster = XDocument.Parse(std.masterxmlfile);


                var result = (from tutorial in xmldocMaster.Descendants("test")
                              select new
                              {
                                  vartime = tutorial.Element("Time").Value,
                                  vartimeshow = (string)tutorial.Element("Time").Attribute("show")
                              }).FirstOrDefault();

                if (result.vartimeshow.ToString() == "1")
                {
                    testtime = "true";

                    if (result.vartime.ToString() == "")
                    {
                        flagcount++;
                        flag = true;
                        errorstring += "<span style='font-size:20px;color:blue;'><b>Exam Time Missing </b></span>" + "";
                    }
                    else
                    {
                        DateTime temp1;
                        if (DateTime.TryParse(result.vartime.ToString().ToString(), out temp1))
                        {

                        }
                        else
                        {
                            flagcount++;
                            flag = true;
                            errorstring += "<li><strong>F" + flagcount + ": </strong>Invalid Time , Please enter in  (HH:MM:SS) format.</li>";
                        }
                    }
                }
                Int32 totsections = (from tutorial in xmldocMaster.Descendants("section") select tutorial).Count();


                if (totsections == 0)
                {
                    flagcount++;
                    flag = true;
                    errorstring += "<li><strong>F" + flagcount + ": </strong>Add Section to Exam.</li>";
                }

                if (totsections > 0)
                {
                    for (int k = 1; k <= Convert.ToInt32(totsections); k++)
                    {
                        bool secis = false;
                        XmlNode xmlRoottop;
                        xmlRoottop = xmldoc.SelectSingleNode("//test//sections//section[@id=" + k.ToString() + "]");
                        string sectime = xmlRoottop.ChildNodes[2].InnerText.ToString();
                        string secname = xmlRoottop.ChildNodes[0].InnerText.ToString();
                        bool autosec = false;
                        try
                        {
                            if (xmlRoottop.Attributes["auto"].Value.ToString() == "1")
                            {
                                autosec = true;
                            }
                        }
                        catch
                        {

                        }

                        if (secname.Trim() == "" || secname.Trim() == "-----")
                        {
                            flagcount++;
                            flag = true;

                            if (secis == false)
                            {
                                errorstring += "<span style='font-size:20px;color:blue;'><b>Section" + k.ToString() + "</b></span>" + "";
                                secis = true;
                            }
                            errorstring += "<li><strong>F" + flagcount + ": </strong>Section Name is missing.</li>";
                        }

                        if (testtime != "true")
                        {

                            if (sectime == "")
                            {
                                flagcount++;
                                flag = true;
                                if (secis == false)
                                {
                                    errorstring += "<span style='font-size:20px;color:blue;'><b>Section" + k.ToString() + "</b></span>" + "";
                                    secis = true;
                                }
                                errorstring += "<li><strong>F" + flagcount + ": </strong>Time for section is missing.</li>";
                            }
                            else
                            {
                                DateTime temp1;
                                if (DateTime.TryParse(sectime.ToString(), out temp1))
                                {

                                }
                                else
                                {
                                    flagcount++;
                                    flag = true;
                                    if (secis == false)
                                    {
                                        errorstring += "<span style='font-size:20px;color:blue;'><b>Section " + k.ToString() + "</b></span>" + "";
                                        secis = true;
                                    }
                                    errorstring += "<li><strong>F" + flagcount + ": </strong>Invalid Time , Please enter in  (HH:MM:SS) format.</li>";
                                }
                            }
                        }

                        if (autosec == true)
                        {
                            Int32 totset = 0;
                            try
                            {
                                totset = (from tutorial in xmldocMaster.Descendants("setting")
                                          where tutorial.Attribute("sectionid").Value == k.ToString()
                                          select tutorial.Attribute("id").Value).Count();
                            }
                            catch
                            {
                                totset = 0;
                            }

                            if (totset == 0)
                            {
                                flagcount++;
                                flag = true;
                                if (secis == false)
                                {
                                    errorstring += "<span style='font-size:20px;color:blue;'><b>Section" + k.ToString() + "</b></span>" + "";
                                    secis = true;
                                }

                                errorstring += "<li><strong>F" + flagcount + ": </strong>Please add settings to auto section.</li>";
                            }
                        }

                        Int32 questionno = (from tutorial in xmldocMaster.Descendants("question") where tutorial.Attribute("sectionid").Value == k.ToString() select tutorial).Count();
                        if (questionno == 0)
                        {
                            if (autosec == false)
                            {
                                flagcount++;
                                flag = true;
                                if (secis == false)
                                {
                                    errorstring += "<span style='font-size:20px;color:blue;'><b>Section " + k.ToString() + "</b></span>" + "";
                                    secis = true;
                                }



                                errorstring += "<li><strong>F" + flagcount + ": </strong>Please add questions to this section.</li>";
                            }
                        }

                        XmlNode xmlRootQuesCnt;
                        xmlRootQuesCnt = xmldoc.SelectSingleNode("//test//sections//section[@id=" + k.ToString() + "]//QuestionRatio");
                        string secquescnt = xmlRootQuesCnt.Attributes["questioncount"].Value.ToString();
                        if (secquescnt == "")
                        {
                            //flagcount++;
                            //flag = true;
                            //if (secis == false)
                            //{
                            //    errorstring += "<span style='font-size:20px;color:blue;'><b>Section" + k.ToString() + "</b></span>" + "";
                            //    secis = true;
                            //}
                            //errorstring += "<li><strong>F" + flagcount + ": </strong>Question Show Count is empty, please update mastersettings</li>";
                        }
                        else
                        {

                            if (Convert.ToInt32(secquescnt) > questionno)
                            {
                                if (autosec == false)
                                {
                                    flagcount++;
                                    flag = true;
                                    if (secis == false)
                                    {
                                        errorstring += "<span style='font-size:20px;color:blue;'><b>Section" + k.ToString() + "</b></span>" + "";
                                        secis = true;
                                    }

                                    errorstring += "<li><strong>F" + flagcount + ": </strong>Question Show Count is more than the questions added to section</li>";
                                }
                            }
                        }
                    }
                }

                errorstring += "</ul>";
               

                if (flag == true)
                {

                    mmtexamflag existFlags = (from t in db.mmtexamflags
                                              where t.examid == std.examid
                                              select t).FirstOrDefault<mmtexamflag>();
                  
                    if (existFlags != null)
                    {
                        existFlags.flagcount = flagcount;
                        existFlags.flagerror = errorstring;
                        std.flagged = true;
                        db.Entry(std).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {

                        mmtexamflag flags = new mmtexamflag { flagcount = flagcount, flagerror = errorstring, examid = id };
                        std.mmtexamflags.Add(flags);
                        std.flagged = true;
                        db.Entry(std).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    mmtexamflag existFlags = (from t in db.mmtexamflags
                                              where t.examid == std.examid
                                              select t).FirstOrDefault<mmtexamflag>();
                    if (existFlags != null)
                    {
                        existFlags.flagcount = 0;
                        existFlags.flagerror = "";
                        std.flagged = true;
                        db.Entry(std).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        mmtexamflag flags = new mmtexamflag { flagcount = 0, flagerror = "", examid = id };
                        std.mmtexamflags.Add(flags);
                        std.flagged = false;
                        db.Entry(std).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }


            }

            return "true";
        }

        public static string AsciiValues(string varCheck)
        {

            varCheck = varCheck.Replace("'", "&#39;");

            return varCheck;
        }
    }
}