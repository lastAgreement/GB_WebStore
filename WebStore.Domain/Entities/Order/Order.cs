using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Entities.Order
{
    public class Order: NamedEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    }
}
