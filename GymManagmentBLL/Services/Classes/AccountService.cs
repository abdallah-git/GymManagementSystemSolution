using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.AccountViewModel;
using GymMangementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager; 
        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager; 
            
        }
        public ApplicationUser? ValidateUser(AccouontViewModel accouontViewModel)
        {
            var User = _userManager.FindByEmailAsync(accouontViewModel.Email).Result;
            if (User is null)
            {
                return null; 
            }

            var IsPasswordValid = _userManager.CheckPasswordAsync(User, accouontViewModel.Password).Result;

            return IsPasswordValid ? User : null; 
        }
    }
}
