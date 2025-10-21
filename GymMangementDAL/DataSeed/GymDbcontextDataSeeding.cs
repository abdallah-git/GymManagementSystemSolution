using GymMangementDAL.Data.Contexts;
using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymMangementDAL.DataSeed
{
   public class GymDbcontextDataSedding
    {


        public static bool SeedData ( GymDbcontext dbcontext)
        {
            try
            {

                var HasPlans = dbcontext.Plans.Any();

                var HasCategories = dbcontext.Categories.Any();


                if (HasPlans && HasCategories) return false;

                if (!HasPlans)
                {

                    var plans = Loaddatafromjsonfile<Plan>("plans.json");

                    if (plans.Any())
                    {
                        dbcontext.AddRange(plans);
                    }

                }

                if (!HasCategories)
                {

                    var categories = Loaddatafromjsonfile<Category>("categories.json");

                    if (categories.Any())
                    {
                        dbcontext.AddRange(categories);
                    }

                }


                return dbcontext.SaveChanges() > 0;
            }
            catch
            {
                return false; 
            }





        }


        private static List<T> Loaddatafromjsonfile<T> (string Filename )
        {


            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", Filename);

            if (!File.Exists(FilePath)) throw new FileNotFoundException();


            string Data = File.ReadAllText(FilePath);


            var Options = new JsonSerializerOptions()
            {

                PropertyNameCaseInsensitive = true


            };


            return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? new List<T>(); 



            



        }




    }
}
