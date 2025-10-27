using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.PlanViewModel
{
    public class UpdatePlanViewModel
    {
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = "Description is Requierd")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Description must be betwwn 5 and 50 char ")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "DurationDayes is Requierd")]
        [Range(1,365,ErrorMessage ="Duration days between 1 and 365 day " )]
        public int DurationDayes { get; set; }

        [Required(ErrorMessage = "Price is Requierd")]
        [Range(0.1,10000, ErrorMessage = "price must be between 0.1 and 10000 " ) ] 
        public decimal Price { get; set; }




    }
}
