//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mmtMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mmtgroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mmtgroup()
        {
            this.mmtquestionbanks = new HashSet<mmtquestionbank>();
        }
    
        public int groupid { get; set; }
        public string groupdesc { get; set; }
        public Nullable<int> examid { get; set; }
        public int sectionid { get; set; }
        public int secid { get; set; }
        public int passageid { get; set; }
    
        public virtual mmtexam mmtexam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mmtquestionbank> mmtquestionbanks { get; set; }
        public virtual mmtsection mmtsection { get; set; }
    }
}