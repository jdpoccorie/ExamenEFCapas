using Infraestructure.Context;
using Domain.Models;
using APICodigoEFC.Request;
using APICodigoEFC.Response;
using Services.Services;
using APICodigoEFC.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace APICodigoEFC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CodigoContext _context;
        private CustomersService _service;

        public CustomersController(CodigoContext context)
        {
            _context = context;
            _service = new CustomersService(_context);
        }

        [HttpGet]
        public List<Customer> GetByFilters(string? name,string? documentNumber )
        {          
            var customers = _service.GetByFilters(name, documentNumber);
            return customers;           
        }

        [HttpPost]
        public void Insert([FromBody] Customer customer)
        {
            _service.Insert(customer);

        }
        [HttpPut]
        public void Update([FromBody] Customer customer)
        {
            _service.Update(customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }

        [HttpPut]
        public ResponseBase UpdateName([FromBody] CustomerUpdateRequest request)
        {
            ResponseBase response = new ResponseBase();
            int code = 0;

            try
            {
                response.Code = 0;
                response.Message = "Registro exitoso";

                code=_service.UpdateName(request.Id, request.Name);

                if (code!=0)
                {
                    response.Message = "Error Controlado";
                    response.Code = code;                   
                }                                 
                return response;

            }
            catch (Exception ex)
            {
                //Write log
                response.Message = Validations.GeneralError;
                response.Code = -1000;
                return response;
            }


        }





    }
}
