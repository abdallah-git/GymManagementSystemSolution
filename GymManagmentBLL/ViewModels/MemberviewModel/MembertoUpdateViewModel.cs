using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberviewModel
{
    internal class MembertoUpdateViewModel
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is Requierd")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 ")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Requierd")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid Phone format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone must be Egyption number")]
        public string Phone { get; set; } = null!;


        [Required(ErrorMessage = "BuildingNumber is Requierd")]
        [Range(1, 1000, ErrorMessage = "Buildingnumber must between 1 and 1000")]
        public int Buildingnumber { get; set; }

        [Required(ErrorMessage = "Street is Requierd")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "streer must be between 2 and 30 ")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "city is Requierd")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "city must be between 2 and 30 ")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "city can contain only letters a to z ")]
        public string City { get; set; } = null!;



        public string? Photo { get; set; } 










    }
}
