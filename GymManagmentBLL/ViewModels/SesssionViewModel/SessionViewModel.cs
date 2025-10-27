﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.SesssionViewModel
{
    public class SessionViewModel
    {

        public int Id { get; set; }

        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public TimeSpan Duration { get; set; }

        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region ComputedProperties 

        public string DateDisplay => $"{StartDate: MMM dd yyyy}";


        public string TimeDisplay => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";


        public string Statues
        {
            get
            {
                if (StartDate > DateTime.Now)
                    return "Upcoming";
                else if (StartDate <= DateTime.Now && EndDate > DateTime.Now)
                    return "ongoing";
                else
                    return "Completed";

            }
        }


       






        #endregion





 
    }
}  
