using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Versioning;
using very_easy_test_app.Models.Enum;

namespace very_easy_test_app.Models.Entities
{
    [Table("Home")]
    public class HomeEntity:EntityBase
    {
        public Guid HomeOwnerID { get; set; }
        public string regNo { get; set; }
        public string metrcis { get; set; }
        public string address { get; set; }
        public HousePosition housePosition { get; set; }

        public HomeOwenerEntity HomeOwener { get; set; }
    }
}