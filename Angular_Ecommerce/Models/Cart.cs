using System.ComponentModel.DataAnnotations;

namespace Angular_Ecommerce.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public String Product{ get; set; }
        public string Name  { get; set; }
        public double Price{ get; set; }
        public int Quantity { get; set; }
        public String Img { get; set; }


    }
}
