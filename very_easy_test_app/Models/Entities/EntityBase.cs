using System;

namespace very_easy_test_app.Models.Entities
{
    public abstract class EntityBase
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public bool allowDelete { get; set; }

        protected EntityBase()
        {
            allowDelete = false;
        }
    }
}