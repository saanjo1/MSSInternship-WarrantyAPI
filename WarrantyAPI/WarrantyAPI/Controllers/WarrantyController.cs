using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarrantyAPI.Repositories;
using WarrantyAPI.Models;
using WarrantyAPI.Contracts;

namespace WarrantyAPI.Controllers
{
    [ApiController]

    public class WarrantyController : ControllerBase
    {
        private readonly IRepository<Warranty> WarrantyRepo;

        public WarrantyController(IRepository<Warranty> _warrantyRepo)
        {
            WarrantyRepo = _warrantyRepo;
        }

        [HttpGet]
        [Route("api/[action]")]

        public async Task<IActionResult> GetWarranties()
        {
            Console.WriteLine("Entering GetWarranities in controller. ");
            var createResponse = await WarrantyRepo.Read();
            return Ok(createResponse);
        }


        [HttpPost]
        [Route("api/[action]")]

        public async Task<IActionResult> PostWarranities(Warranty model)
        {
            Console.WriteLine("Entering PostWarranities in controller.");
            var response = await WarrantyRepo.Create(model);

            return Ok(response);
        }


        [HttpPut]
        [Route("api/[action]")]

        public async Task<IActionResult> PutWarranities(Warranty model)
        {
            Console.WriteLine("Entering PutWarranities in controller.");
            var createResponse = await WarrantyRepo.Update(model);
            return Ok(createResponse);

        }


        [HttpDelete]
        [Route("api/[action]")]

        public async Task<IActionResult> DeleteWarranities(string assetId, string WarrantyId)
        {
            Console.WriteLine("Entering DeleteWarranities in controller.");
            var createResponse = await WarrantyRepo.Delete(WarrantyId, assetId);
            return Ok(createResponse);
        }



        [HttpGet]
        [Route("api/[action]")]
        public async Task<IActionResult> GetWarrantyById(string id)
        {
            var createResponse = await WarrantyRepo.Read(id);
            return Ok(createResponse);
        }



    }
}