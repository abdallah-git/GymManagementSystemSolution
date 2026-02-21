using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.BookingViewModel;
using GymManagmentBLL.ViewModels.MembershipViewModel;
using GymManagmentBLL.ViewModels.SesssionViewModel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class BookingService : IBookingService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<MemberForSessionViewModel> GetAllMembersForUpcomingSessions(int id)


        {

            var BookingRepo = _unitOfWork.BookingRepository;
            var MemberForSessions = BookingRepo.GetSessionById(id);

            var memberForBookingVm = _mapper.Map<IEnumerable<MemberForSessionViewModel>>(MemberForSessions);
            return memberForBookingVm;



        }
       
        public IEnumerable<SessionViewModel> GetAllSeesionsWithTrainerAndCategory()
        {


            var sessionRepo = _unitOfWork.sessionRepository;
            var sessions = sessionRepo.GetAllsesionswithtrainersandcategories();

            var sessionsVms = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

            foreach (var sessionVm in sessionsVms)
                sessionVm.AvailableSlots = sessionVm.Capacity- sessionRepo.GetCountofbookslots(sessionVm.Id);


            return sessionsVms;


        }

        public IEnumerable<MemberForSessionViewModel> GetAllMembersForOngoingSessions(int id)
        {

            var BookingRepo = _unitOfWork.BookingRepository;
            var MembersForSession = BookingRepo.GetSessionById(id);
            var memberForBookingVm = _mapper.Map<IEnumerable<MemberForSessionViewModel>>(MembersForSession);
            return memberForBookingVm;
        }

       

        public bool CreateBooking(CreateBookingViewModel model)
        {

            var session = _unitOfWork.sessionRepository.GetById(model.SessionId);
            if (session is null || session.StartDate <= DateTime.UtcNow)
                return false;

            var membershipRepo = _unitOfWork.MembershipRepository;
            var activeMembership = membershipRepo.GetFirstOrDefault(m => m.MemberId == model.MemberId && m.Statues == "Active");

            if (activeMembership is null)
                return false;


            var sessionRepo = _unitOfWork.sessionRepository;
            var bookedSlots = sessionRepo.GetCountofbookslots(model.SessionId);

            var availableSlots = session.Capacity - bookedSlots;
            if (availableSlots == 0)
                return false;


            var booking = _mapper.Map<Membersession>(model);
            // BUSINESS RULE #7: When a booking is created, IsAttended is always set to false by default.

            booking.IsAttend = false;
            _unitOfWork.BookingRepository.Add(booking);


            return _unitOfWork.Savechanges() > 0;


        }




       


        public bool CancelBooking(MemberAttendOrCancelViewModel model)
        {
            try
            {
                var session = _unitOfWork.sessionRepository.GetById(model.SessionId);
                if (session is null || session.StartDate <= DateTime.Now) return false;

                // BUSINESS RULE #5: A booking can only be cancelled for future sessions. Once the session has started, cancellation is not allowed.
                var Booking = _unitOfWork.BookingRepository.GetAll(X => X.MemberId == model.MemberId && X.SessionId == model.SessionId)
                                                           .FirstOrDefault();
                if (Booking is null) return false;
                _unitOfWork.BookingRepository.Delete(Booking);
                return _unitOfWork.Savechanges() > 0;
            }
            catch
            {
                return false;
            }
        }










        #region Helper Methods

        public IEnumerable<MemberForSelectListViewModel> GetMembersForDropdown(int id)
        {
            var bookingRepo = _unitOfWork.BookingRepository;
            var bookedMemberIds = bookingRepo.GetAll(s => s.Id == id)
                                                      .Select(s => s.MemberId)
                                                      .ToList();

            var availableMembersToBook = _unitOfWork.GetRepository<Member>().GetAll(m => !bookedMemberIds.Contains(m.Id));

            var memberSelectListViewModel = _mapper.Map<IEnumerable<MemberForSelectListViewModel>>(availableMembersToBook);

            return memberSelectListViewModel;




        }


        #endregion


    }
}
