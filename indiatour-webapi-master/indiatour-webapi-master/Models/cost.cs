namespace indiatour_webapi_master.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cost")]
    public partial class cost
    {
        [Key]
        public int Cost_Id { get; set; }

        public int? Sectormaster_Id { get; set; }

        public double? Singleoccupancy { get; set; }

        public double? Twinperson { get; set; }

        public double? Triplesharing { get; set; }

        public double? Childwithparents { get; set; }

        public double? Childwithoutparents { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Validfrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Validto { get; set; }

        public virtual sector sector { get; set; }
    }
}
