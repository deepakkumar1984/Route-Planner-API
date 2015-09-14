using RoutePlanner.GoogleAPIHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RoutePlanner.GoogleAPIHelper.Controllers
{
    public class DistanceMatrixController : ApiController
    {
        [ActionName("available")]
        [HttpGet]
        public bool Available()
        {
            double elapsedSeconds = (DateTime.Now - CacheManager.DistanceMatrixLastProcessedTime).TotalSeconds;
            return (elapsedSeconds > 10);
        }

        [ActionName("requestcountleft")]
        [HttpGet]
        public int RequestCountLeft()
        {
            return CacheManager.DistanceMatrixRequestCountLeft;
        }

        [ActionName("run")]
        [HttpGet]
        public DistanceMatrixResponse Run(string ID, string AddressList, bool AvoidHighways, bool AvoidTolls, bool AvoidFerries, string Mode, string Unit)
        {
            string avoid = "";
            if (AvoidHighways)
                avoid = "highways|";
            if (AvoidTolls)
                avoid = "tolls|";
            if (AvoidFerries)
                avoid = "ferries|";
            if (!string.IsNullOrWhiteSpace(avoid))
            {
                avoid = avoid.Remove(avoid.LastIndexOf('|'));
            }

            DistanceMatrixResponse result = GoogleAPIHelper.GetDistanceMatrix(AddressList, Mode.ToLowerInvariant(), avoid, Unit.ToLowerInvariant());
            return result;
        }
    }
}
