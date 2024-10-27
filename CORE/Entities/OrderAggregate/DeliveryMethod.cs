using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CORE.Entities.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {
        //[Key]
        //public int Id { get; set; }
        public string Shortname { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }



    }
}