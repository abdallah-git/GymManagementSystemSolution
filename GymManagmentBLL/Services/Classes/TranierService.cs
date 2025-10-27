using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class TranierService : ITrainerService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper1; 
        public TranierService(IUnitOfWork unitOfWork , IMapper mapper ) 
        {
            _unitOfWork = unitOfWork;
            mapper1 = mapper; 
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                if (createTrainer is null) return false;


                if (IsEmailExists(createTrainer.Email) || IsPhoneExists(createTrainer.Phone)) return false;

                var trainer = mapper1.Map<CreateTrainerViewModel, Trainer>(createTrainer); 
               
                _unitOfWork.GetRepository<Trainer>().Add(trainer);

                return _unitOfWork.Savechanges() > 0;
            }
            catch
            {
                return false;
            }

        }



        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            if (Trainers is null || !Trainers.Any()) return [];

            return mapper1.Map<IEnumerable<TrainerViewModel>>(Trainers); 

        }

        public TrainerViewModel? GetTrainerDetails(int id)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(id);


            if (trainer is null) return null;


            return new TrainerViewModel()
            {
                Name= trainer.Name , 

                Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.City} - {trainer.Address.Street}" , 

                Email = trainer.Email , 

                Phone = trainer.Phone , 

                Specialties = trainer .Specialties.ToString() , 

                DateOfBirth = trainer.DateOfBirth.ToShortDateString () , 
            };  

            
            
      
        }

        public TrianerToUpdateViewModel? GetTrainerToUpdate(int id)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(id);

            if (trainer is null) return null;

            return mapper1.Map<Trainer, TrianerToUpdateViewModel>(trainer); 
           
        }

        public bool RemoveTrainer(int id)
        {
            try
            {

                var trainerRepo = _unitOfWork.GetRepository<Trainer>();
                var trainer = trainerRepo.GetById(id);

                if (trainer == null) return false;

                var hasFutureSession = _unitOfWork.GetRepository<Session>().GetAll(S => S.TrainerId == trainer.Id && S.StartDate > DateTime.Now).Any();

                if (hasFutureSession) return false;


                trainerRepo.Delete(trainer);
                return _unitOfWork.Savechanges() > 0;

            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTrainer(int id, TrianerToUpdateViewModel updateTrianer)
        {
            try
            {
                if (updateTrianer == null) return false;
                var TrainerRepo = _unitOfWork.GetRepository<Trainer>();
                var trainer = TrainerRepo.GetById(id);

                var Emailexist = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == updateTrianer.Email && x.Id != id).Any();
                var phoneexist = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == updateTrianer.Phone && x.Id != id).Any();

                if (trainer is null || Emailexist || phoneexist) return false;


                mapper1.Map(trainer, updateTrianer); 

                TrainerRepo.Update(trainer);

                return _unitOfWork.Savechanges() > 0; 

            }
            catch
            {
                return false;
            }
        }






        #region Helper Methods

        bool IsEmailExists(string email) => _unitOfWork.GetRepository<Trainer>().GetAll(T => T.Email == email).Any();
        bool IsPhoneExists(string phone) => _unitOfWork.GetRepository<Trainer>().GetAll(T => T.Phone == phone).Any();





        #endregion



    }
}
