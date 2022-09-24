namespace indiatour_webapi_master.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("booking")]
    public partial class booking
    {
        [Key]
        public int Booking_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Bookingdate { get; set; }

        public int Cust_Id { get; set; }

        public int Package_Id { get; set; }

        public int Passangers { get; set; }

        public double Touramount { get; set; }

        public double Taxes { get; set; }

        public double Totalamount { get; set; }

        public int? Flag { get; set; }
    }
}
