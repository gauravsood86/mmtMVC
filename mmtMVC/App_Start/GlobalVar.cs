using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public static class GlobalVar
{

    public const string pathphotouploadfind = "makemytestAPI";

    public const string mastersetmain = "MasterSettings";
    public const string MSF_mastersetin = "MSF";
    public const string mastersetin = "fnaukri";
    public const string stylesheetpath = "http://assessments.firstnaukri.com/NaukriAdmin/css/humanity/style.css";
    public const string headerpath = "http://assessments.firstnaukri.com/NaukriAdmin/images/header/firstnaukri.gif";
    public const string redirectutl = "http://assessments.firstnaukri.com/cdm/manualclose.aspx";
    public const string redirectutlseb = "http://assessments.firstnaukri.com/cdm/manualclose.aspx";



    public const string Registerationpathreplace = "wdata";
    public const string ExcelDriver = "Microsoft.ACE.OLEDB.12.0";

    public const string imagepath = "wwwroot";
    public const string footerBharti = "First naukri";
    public const string footerRetailer = "First naukri";
    public const string examfilepath = @"\taketest\firstnaukri\feat";
    public const string surveyfilepath = @"\taketest\firstnaukri\takesurveyxml";

    public const string previenginexml = @"\taketest\firstnaukri\previenginexml";

    static int _globalValue;


    public static int GlobalValue
    {
        get
        {
            return _globalValue;
        }
        set
        {
            _globalValue = value;
        }
    }


    public static bool GlobalBoolean;
}
