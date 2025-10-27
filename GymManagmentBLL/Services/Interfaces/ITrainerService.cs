using GymManagmentBLL.ViewModels.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface ITrainerService
    {

        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        IEnumerable<TrainerViewModel> GetAllTrainers();
        TrainerViewModel? GetTrainerDetails(int id);
        TrianerToUpdateViewModel? GetTrainerToUpdate(int id); 
        bool RemoveTrainer(int id);
        bool UpdateTrainer(int id, TrianerToUpdateViewModel updateTrianer);





    }
}
