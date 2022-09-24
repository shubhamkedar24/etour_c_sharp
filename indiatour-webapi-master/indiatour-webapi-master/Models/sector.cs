namespace indiatour_webapi_master.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sector")]
    public partial class sector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sector()
        {
            costs = new HashSet<cost>();
            itineraries = new HashSet<itinerary>();
            tourpackages = new HashSet<tourpackage>();
        }

        [Key]
        public int Sectormaster_Id { get; set; }

        [StringLength(10)]
        public string Sector_Id { get; set; }

        [StringLength(10)]
        public string Subsector_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Sectorname { get; set; }

        [Required]
        [StringLength(250)]
        public string Imgpath { get; set; }

        public byte? Flag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cost> costs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<itinerary> itineraries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tourpackage> tourpackages { get; set; }
    }
}
