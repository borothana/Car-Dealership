using GuildCars.Datas;
using GuildCars.Models;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GuildCars.UI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RESTController : ApiController
    {
        ICar _repo = CarFactory.Create();


        //[Route("car/{id}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult FilterCarFilter(string id)
        //{
        //    return Ok(_repo.GetCarVMList());
        //}


        //[Route("car/{filter}/{minPrice}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult FilterCarFilter2(string filter, int minPrice)
        //{
        //    return Ok(_repo.GetCarVMList());
        //}

        [Route("car/{filter}/{minPrice}/{maxPrice}/{minYear}/{maxYear}/{type}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult FilterCar(string filter, int minPrice, int maxPrice, int minYear, int maxYear, string type)
        {
            return Ok(_repo.GetCarVMList(filter, minPrice, maxPrice, minYear, maxYear, type));
        }

        [Route("showcar")]
        [AcceptVerbs("GET")]
        public IHttpActionResult ShowCar()
        {
            return Ok(_repo.GetCarVMList().Where(c=>c.IsFeature == true && c.IsPurchase == false));
        }

        [Route("car/{carId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult FilterCar(int carId)
        {
            return Ok(_repo.GetCarVMList().FirstOrDefault(c=>c.CarId == carId));
        }


        [Route("cars")]
        [AcceptVerbs("GET")]
        public IHttpActionResult All()
        {
            return Ok(_repo.GetCarVMList());
        }

        [Route("sale")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Purchase(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.AddSale(sale);
            return Created($"/sale/{sale.SaleId}", sale);
        }
            
        [Route("makes/{makeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetMake(int makeId)
        {
            return Ok(_repo.GetModelList().Where(m=>m.MakeId == makeId));
        }
    }
}