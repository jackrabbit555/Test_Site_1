using System;

namespace Test_Site_1.Domain.Entities.Commons
{
    public abstract class BaseEntityNotId  
    {
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
    }
}
