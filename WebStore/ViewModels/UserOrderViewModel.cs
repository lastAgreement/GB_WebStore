using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class UserOrderViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Address { get; set; }

        public decimal TotalSum { get; set; }
    }
}
