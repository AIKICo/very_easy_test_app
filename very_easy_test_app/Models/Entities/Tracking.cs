using System;
using very_easy_test_app.Models.Enum;

namespace very_easy_test_app.Models.Entities
{
    public class Tracking
    {
        public string IPAddress { get; set; }
        public CRUDType CRUDType { get; set; }
        public DateTime CRUDActionDate { get; set; }
    }
}