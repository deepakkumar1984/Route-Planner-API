using RoutePlanner.GoogleAPIHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace RoutePlanner.GoogleAPIHelper
{
    public class GoogleAPIHelper
    {
        public static DistanceMatrixResponse GetDistanceMatrix(string addressList, string mode, string avoid, string unit)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(string.Format("api/distancematrix/json?origins={0}&destinations={0}&mode={1}&avoid={2}&units={3}&key={4}", addressList, mode, avoid, unit, CacheManager.ApiKey)).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonresponse = response.Content.ReadAsStringAsync().Result;
                DistanceMatrixResponse result = Newtonsoft.Json.JsonConvert.DeserializeObject<DistanceMatrixResponse>(jsonresponse);
                return result;
            }
            else
            {
                return null;
            }
        }

        public static DirectionsResponse GetDirections(string startAddress, string endAddress, string wayPoints, string mode, string avoid, string unit, bool optimize)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(string.Format("api/directions/json?origin={0}&destination={1}&waypoints=optimize:{7}|{2}&mode={3}&avoid={4}&units={5}&key={6}",
                                                startAddress, endAddress, wayPoints, mode, avoid, unit, CacheManager.ApiKey, optimize.ToString().ToLowerInvariant())).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonresponse = response.Content.ReadAsStringAsync().Result;
                DirectionsResponse result = Newtonsoft.Json.JsonConvert.DeserializeObject<DirectionsResponse>(jsonresponse);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}