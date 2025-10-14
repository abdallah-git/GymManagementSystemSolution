using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberviewModel
{
    internal class Healthrecordviewmodel
    {
        [Required(ErrorMessage = "hight is Requierd")]
        [Range(0.1, 300, ErrorMessage = "hight must between 0.1  and 300")]
        public decimal Hieght { get; set; }



        [Required(ErrorMessage = "weight is Requierd")]
        [Range(0.1, 500, ErrorMessage = "hight must between 0.1  and 500")]
        public decimal Weight { get; set; }



        [Required(ErrorMessage = "Bloddtype is Requierd")]
        [StringLength(3,  ErrorMessage = "Bloodtype must be between 3 or less  ")]
        public string Bloodtype { get; set; } = null!; 



        public string? Note { get; set; } 







    }
}
