using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Web.Hosting;

namespace mmtMVC.Models
{
    public class CommonOperations
    {
        static MMT_ConstEntities db = new MMT_ConstEntities();
        /// <summary>
        /// GET XML PATH
        /// </summary>
        /// <param name="examid"></param>
        /// <returns></returns>
        public static string getXML(string examid)
        {
            XmlDocument xmldoc = new XmlDocument();

            string path = HostingEnvironment.MapPath("/");
            string filename = examid.ToString();
            string mainpath1 = path.Replace(GlobalVar.pathphotouploadfind, "\\xmlfiles\\" + GlobalVar.mastersetmain + "\\" + GlobalVar.mastersetin + "\\");
            string strFilename = mainpath1 + filename + ".xml";

            return strFilename;
        }

        /// <summary>
        /// Convert minutes to HH:MM:SS format
        /// </summary>
        /// <param name="timeinmin">time in minutes</param>
        /// <returns></returns>
        public static string gettime(int timeinmin)
        {
            try
            {
                int min = timeinmin;
                TimeSpan output = new TimeSpan(0, min, 0);
                string str = output.ToString().Replace(".", ":");
                return str;
            }
            catch
            {
                return "00:00:00";
            }
        }
        /// <summary>
        /// Convert HH:MM:SS to minutes
        /// </summary>
        /// <param name="timeinmin">HH:MM:SS format</param>
        /// <returns></returns>
        public static int gettimeinmin(string timeinmin)
        {
            try
            {
                double min = TimeSpan.Parse(timeinmin).TotalMinutes;
                return Convert.ToInt32(min);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Check if exam is added by user
        /// </summary>
        /// <param name="testid"></param>
        /// <returns></returns>
        public static bool checkuserexam(int testid, int userid)
        {

            mmtexam checkexam = (from n in db.mmtexams where n.addedby == userid && n.examid == testid select n).SingleOrDefault();

            if (checkexam == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}