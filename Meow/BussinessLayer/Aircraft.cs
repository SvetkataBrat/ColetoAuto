using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BussinessLayer
{
    public class Aircraft
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Thrust { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }

        [Required]
        public Model @Model { get; set; }

        public Aircraft()
        {
                
        }

        public Aircraft(double price, int thrust, string description, Model model_)
        {
            Price = price;
            Thrust = thrust;
            Description = description;
            this.@Model = model_;
        }
    }
}
