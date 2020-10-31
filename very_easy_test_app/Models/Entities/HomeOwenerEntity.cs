using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace very_easy_test_app.Models.Entities
{
    [Table("HomeOwener")]
    public class HomeOwenerEntity : EntityBase
    {
        public HomeOwenerEntity()
        {
            Homes = new HashSet<HomeEntity>();
        }

        public string PhoneNumber { get; set; }
        public ICollection<HomeEntity> Homes { get; set; }
    }
}