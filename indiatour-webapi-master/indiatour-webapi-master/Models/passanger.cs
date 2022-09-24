namespace indiatour_webapi_master.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("passanger")]
    public partial class passanger
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pass_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(45)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(45)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Passport { get; set; }

        [StringLength(100)]
        public string Aadharcard { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(45)]
        public string City { get; set; }

        [Required]
        [StringLength(45)]
        public string State { get; set; }

        public int? Package_Id { get; set; }

        public int? Cust_Id { get; set; }

        public decimal? cost { get; set; }

        public int Pincode { get; set; }

        public int? customer_cust_id { get; set; }

        public byte? flag { get; set; }

        public virtual customer customer { get; set; }
    }
}
