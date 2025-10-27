using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberViewModel
{
    public class HealthRecordViewModel
    {


        [Required(ErrorMessage = "Hieght Is Required")]
        [Range(0.1, 300, ErrorMessage = "Hieght Must Be Between 0.1 and 300 ")]
        public decimal Hieght { get; set; }

        [Required(ErrorMessage = "Weight Is Required")]
        [Range(0.1, 300, ErrorMessage = "Weight Must Be Between 0.1 and 300 ")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "BloodType Is Required")]
        [StringLength(3, ErrorMessage = "BloodType Must Be Be Less Than 3 Char")]
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; } = null!;


    }
}
