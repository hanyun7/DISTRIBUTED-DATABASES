//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLVT.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class KHO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHO()
        {
            this.PHATSINHs = new HashSet<PHATSINH>();
        }
    
        public string MAKHO { get; set; }
        public string TENKHO { get; set; }
        public string DIACHI { get; set; }
        public string MACN { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual CHINHANH CHINHANH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHATSINH> PHATSINHs { get; set; }
    }
}
