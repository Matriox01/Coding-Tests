using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RainfallApi.Models;
using RainfallAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RainfallAPI.Controllers
{
    namespace RainfallApi.Controllers
    {
        [ApiController]
        [Route("rainfall")]
        public class RainfallController : ControllerBase
        {
            private readonly IHttpClientFactory _clientFactory;

            public RainfallController(IHttpClientFactory clientFactory)
            {
                _clientFactory = clientFactory;
            }

            [HttpGet("id/stations/{stationId}/readings")]
            public async Task<IActionResult> GetRainfallReadings(string stationId, [FromQuery] int count = 10)
            {
                try
                {
                    var client = _clientFactory.CreateClient("RainfallApi");
                    var response = await client.GetAsync($"id/stations/{stationId}/readings?count={count}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var readings = JsonConvert.DeserializeObject<List<RainfallReadingModel>>(content);

                        var responseModel = new RainfallReadingResponseModel { Readings = readings };
                        return Ok(responseModel);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return NotFound(new ErrorResponseModel { Message = "No readings found for the specified stationId" });
                    }
                    else
                    {
                        return StatusCode(500, new ErrorResponseModel { Message = "Internal server error" });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new ErrorResponseModel { Message = "Invalid request", Details = new List<ErrorDetailModel> { new ErrorDetailModel { PropertyName = "Exception", Message = ex.Message } } });
                }
            }
        }
    }
}