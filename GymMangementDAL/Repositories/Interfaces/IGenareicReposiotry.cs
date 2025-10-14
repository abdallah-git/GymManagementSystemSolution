using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    public interface IGenareicReposiotry <Tentity> where Tentity : BaseEntity , new () 
    {
        IEnumerable <Tentity> GetAll(Func<Tentity,bool>? condition = null);

        Tentity GetById(int id);

        void Add(Tentity entity);

        void Update(Tentity entity);

        void  Delete(Tentity entity); 


    }
}
