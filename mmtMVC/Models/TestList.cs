using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mmtMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace mmtMVC.Models
{
    /// <summary>
    /// Added on : 26-05:2017
    /// Author : Gaurav Sood
    /// Returns list of tests added by user.
    /// </summary>
    public class TestList
    {
        [Key]
        public int examid { get; set; }
        public string examname { get; set; }
        public string syllabus { get; set; }
        public bool randomques { get; set; }
        public Nullable<System.DateTime> uploaddate { get; set; }
        public virtual IEnumerable<FlagInfo> FlagInfo { get; set; }
    }

    public class FlagInfo
    {
        [Key]
        public int flagid { get; set; }
        public int flagcount { get; set; }
        public string flagerror { get; set; }
    }
}