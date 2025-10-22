using GymManagmentBLL.ViewModels.Analyticsviewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IAnalyticservice
    {

        AnalyticViewModel? GetAnalyticsData(); 


    }
}
