//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeSplit.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Journey
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Journey()
        {
            this.Expenditures = new HashSet<Expenditure>();
            this.JImages = new HashSet<JImage>();
            this.Members = new HashSet<Member>();
        }
    
        public int JourneyID { get; set; }
        public string JName { get; set; }
        public Nullable<bool> JStatus { get; set; }
        public Nullable<System.DateTime> JDayStart { get; set; }
        public Nullable<System.DateTime> JDayEnd { get; set; }
        public Nullable<int> DestinationID { get; set; }
        public string JIntroduce { get; set; }
    
        public virtual Destination Destination { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenditure> Expenditures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JImage> JImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
    }
}