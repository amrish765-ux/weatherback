using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using static ForecastWebApi2.Models.WeatherInfo;
using Nancy.Json;
using System.Net;

namespace ForecastWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        
        [HttpGet]
        public  List<ResultViewModel> WeatherDetail(string City)
        {
            List<ResultViewModel> lists = new List<ResultViewModel>();
            //Assign API KEY which received from OPENWEATHERMAP.ORG  
            string appId = "8113fcc5a7494b0518bd91ef3acc074f";

            //API path with CITY parameter and other parameters.  
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);               

                RootObject root = (new JavaScriptSerializer()).Deserialize<RootObject>(json);
                
                lists.Add(new ResultViewModel { Country = root.sys.country, City = root.name, Lat = Convert.ToString(root.coord.lat), Lon = Convert.ToString(root.coord.lon), Description = root.weather[0].description, Humidity = Convert.ToString(root.main.humidity), Temp = Convert.ToString(root.main.temp), TempFeelsLike = Convert.ToString(root.main.feels_like), TempMax = Convert.ToString(root.main.temp_max), TempMin = Convert.ToString(root.main.temp_min), WeatherIcon = root.weather[0].icon });
             
                return lists;
            }

        }
    }
}
