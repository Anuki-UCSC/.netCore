using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {

        public readonly ContactsAPIDbContext dbContext;


        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger, ContactsAPIDbContext contactsAPIDbContext)
        {
            _logger = logger;
            this.dbContext = contactsAPIDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddWeatherRead(AddWeatherRequest addWeatherRequest)
        {
            try
            {
                var weatherRead = new Weather
                {
                    Date = DateTime.Now,
                    TemperatureC = addWeatherRequest.TemperatureC,
                   // Summary = addWeatherRequest.Summary
                    Summary = WeatherName.Warm

                };

                await dbContext.AddAsync(weatherRead);
                await dbContext.SaveChangesAsync();
                return Ok(weatherRead);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message.ToString());
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetWhetherReads()
        {
            try
            {
                var weatherlist = await dbContext.Weather.ToListAsync();
                foreach (var item in weatherlist)
                {
                    var weather = (WeatherName)item.Summary;
                    Console.WriteLine(weather);
                }

                return Ok(weatherlist);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWeatherRead([FromRoute] Guid id)
        {
            var weatherRecord = await dbContext.Weather.FindAsync(id);
            if(weatherRecord != null)
            {
                dbContext.Weather.RemoveRange(weatherRecord);
                dbContext.SaveChanges();
                return Ok(weatherRecord);
            }
            return NotFound();
        }
    }
}