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
        //[HttpGet]
        //public List<ResultViewModel> WeatherDetail(string City)
        //{

        //    //Assign API KEY which received from OPENWEATHERMAP.ORG  
        //    string appId = "8113fcc5a7494b0518bd91ef3acc074f";

        //    //API path with CITY parameter and other parameters.  
        //    string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

        //    using (WebClient client = new WebClient())
        //    {
        //        string json = client.DownloadString(url);


        //        //********************//  
        //        //     JSON RECIVED   
        //        //********************//  
        //        //{"coord":{ "lon":72.85,"lat":19.01},  
        //        //"weather":[{"id":711,"main":"Smoke","description":"smoke","icon":"50d"}],  
        //        //"base":"stations",  
        //        //"main":{"temp":31.75,"feels_like":31.51,"temp_min":31,"temp_max":32.22,"pressure":1014,"humidity":43},  
        //        //"visibility":2500,  
        //        //"wind":{"speed":4.1,"deg":140},  
        //        //"clouds":{"all":0},  
        //        //"dt":1578730750,  
        //        //"sys":{"type":1,"id":9052,"country":"IN","sunrise":1578707041,"sunset":1578746875},  
        //        //"timezone":19800,  
        //        //"id":1275339,  
        //        //"name":"Mumbai",  
        //        //"cod":200}  

        //        //Converting to OBJECT from JSON string.  
        //        RootObject weatherInfo = JsonSerializer.Deserialize<RootObject>(json);

        //        //Special VIEWMODEL design to send only required fields not all fields which received from   
        //        //www.openweathermap.org api  
        //        ResultViewModel rslt = new ResultViewModel();

        //        rslt.Country = weatherInfo.sys.country;
        //        rslt.City = weatherInfo.name;
        //        rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
        //        rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
        //        rslt.Description = weatherInfo.weather[0].description;
        //        rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
        //        rslt.Temp = Convert.ToString(weatherInfo.main.temp);
        //        rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
        //        rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
        //        rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
        //        rslt.WeatherIcon = weatherInfo.weather[0].icon;

        //        //Converting OBJECT to JSON String   
        //        var jsonstring = JsonSerializer.Serialize(rslt).ToList();

        //        //Return JSON string.  
        //        return jsonstring;
        //    }

        //}
        //[HttpGet]
        //public async Task<List<ResultViewModel>> FindData(string city)
        //{
        //    List<ResultViewModel> lists = new List<ResultViewModel>();
        //    using HttpClient client = new()
        //    {
        //        BaseAddress = new Uri("https://api.openweathermap.org")
        //    };



            //var client = new RestClient("https://api.openweathermap.org");
            //var request = new RestRequest("/data/2.5/weather?q=saharanpur&lat=44.34&lon=10.99&appid=aa8ba225e914649778f77d6e2ce41b64", Method.Get);
            //request.AddHeader("X-RapidAPI-Key", "aa8ba225e914649778f77d6e2ce41b64");
            //request.AddHeader("X-RapidAPI-Host", "api.openweathermap.org");
            //RestResponse response = client.Execute(request);


            // Get the user information.
            //RootObject? user =  client.GetFromJsonAsync<RootObject>("users/1");
            //var client = new RestClient("https://api.openweathermap.org");
            //var request = new RestRequest("/data/2.5/weather?q=saharanpur&lat=44.34&lon=10.99&appid=aa8ba225e914649778f77d6e2ce41b64", Method.Get);
            //request.AddHeader("X-RapidAPI-Key", "aa8ba225e914649778f77d6e2ce41b64");
            //request.AddHeader("X-RapidAPI-Host", "api.openweathermap.org");
            //RestResponse response = client.Execute(request);
        //    RootObject? root = await client.GetFromJsonAsync<RootObject>("data/2.5/weather?q=mumbai&lat=44.34&lon=10.99&appid=aa8ba225e914649778f77d6e2ce41b64");

        //    lists.Add(new ResultViewModel { Country = root.sys.country, City = root.name, Lat = Convert.ToString(root.coord.lat), Lon = Convert.ToString(root.coord.lon), Description = root.weather[0].description, Humidity = Convert.ToString(root.main.humidity), Temp = Convert.ToString(root.main.temp), TempFeelsLike = Convert.ToString(root.main.feels_like), TempMax = Convert.ToString(root.main.temp_max), TempMin = Convert.ToString(root.main.temp_min), WeatherIcon = root.weather[0].icon });
        //    return lists;
        //}
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

                //********************//  
                //     JSON RECIVED   
                //********************//  
                //{"coord":{ "lon":72.85,"lat":19.01},  
                //"weather":[{"id":711,"main":"Smoke","description":"smoke","icon":"50d"}],  
                //"base":"stations",  
                //"main":{"temp":31.75,"feels_like":31.51,"temp_min":31,"temp_max":32.22,"pressure":1014,"humidity":43},  
                //"visibility":2500,  
                //"wind":{"speed":4.1,"deg":140},  
                //"clouds":{"all":0},  
                //"dt":1578730750,  
                //"sys":{"type":1,"id":9052,"country":"IN","sunrise":1578707041,"sunset":1578746875},  
                //"timezone":19800,  
                //"id":1275339,  
                //"name":"Mumbai",  
                //"cod":200}  

                //Converting to OBJECT from JSON string.  
                RootObject root = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                //Special VIEWMODEL design to send only required fields not all fields which received from   
                //www.openweathermap.org api  
                //ResultViewModel rslt = new ResultViewModel();

                //rslt.Country = weatherInfo.sys.country;
                //rslt.City = weatherInfo.name;
                //rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                //rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                //rslt.Description = weatherInfo.weather[0].description;
                //rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                //rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                //rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                //rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                //rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                //rslt.WeatherIcon = weatherInfo.weather[0].icon;
                //lists.Add(rslt.Country, rslt.City, rslt.Lat, rslt.Lon, rslt.Description, 
                //    rslt.Humidity, rslt.Temp, rslt.TempFeelsLike, rslt.TempMax,
                //    rslt.TempMin, rslt.WeatherIcon);
                lists.Add(new ResultViewModel { Country = root.sys.country, City = root.name, Lat = Convert.ToString(root.coord.lat), Lon = Convert.ToString(root.coord.lon), Description = root.weather[0].description, Humidity = Convert.ToString(root.main.humidity), Temp = Convert.ToString(root.main.temp), TempFeelsLike = Convert.ToString(root.main.feels_like), TempMax = Convert.ToString(root.main.temp_max), TempMin = Convert.ToString(root.main.temp_min), WeatherIcon = root.weather[0].icon });
                //Converting OBJECT to JSON String   
                //var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                //Return JSON string.  
                return lists;
            }

        }
    }
}
