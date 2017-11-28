using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Interface;
using System.Configuration;
using GuildCars.Datas;

namespace GuildCars.Datas
{
    public class CarFactory
    {
        public static ICar Create()
        {
            try
            {
                string mode = ConfigurationSettings.AppSettings["Mode"].ToString();
                switch (mode)
                {
                    case "Mock":
                        return new CarRepositoryMock();
                    default:
                        return new CarRepositoryEF();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
