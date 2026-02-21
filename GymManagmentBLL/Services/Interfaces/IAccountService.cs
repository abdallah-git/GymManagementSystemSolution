using GymManagmentBLL.ViewModels.AccountViewModel;
using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IAccountService
    {

        ApplicationUser? ValidateUser(AccouontViewModel accouontViewModel); 



       
    }
}
