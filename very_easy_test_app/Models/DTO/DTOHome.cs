using System;
using very_easy_test_app.Models.Enum;

namespace very_easy_test_app.Models.DTO
{
    public class DTOHome:DTOBase
    {
        public Guid HomeOwnerID { get; set; }
        public string regNo { get; set; }
        public string metrcis { get; set; }
        public string address { get; set; }
        public HousePosition housePosition { get; set; }

    }
}