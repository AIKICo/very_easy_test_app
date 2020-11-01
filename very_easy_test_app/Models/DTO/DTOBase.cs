using System;
using System.ComponentModel.DataAnnotations.Schema;
using very_easy_test_app.Models.Entities;

namespace very_easy_test_app.Models.DTO
{
    public class DTOBase
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public bool allowDelete { get; set; }
        public Tracking tracking { get; set; }
    }
}