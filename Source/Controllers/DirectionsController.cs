using RoutePlanner.GoogleAPIHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoutePlanner.GoogleAPIHelper.Controllers
{
    public class DirectionsController : ApiController
    {
        [ActionName("available")]
        [HttpGet]
        public bool Available()
        {
            double elapsedSeconds = (DateTime.Now - CacheManager.DirectionsLastProcessedTime).TotalSeconds;
            return (elapsedSeconds > 0.5);
        }

        [ActionName("requestcountleft")]
        [HttpGet]
        public int RequestCountLeft()
        {
            return CacheManager.DirectionsRequestCountLeft;
        }

        [ActionName("run")]
        [HttpGet]
        public DirectionsResponse Run(string ID, string Start, string End, string WayPoints, bool AvoidHighways, bool AvoidTolls, bool AvoidFerries, string Mode, string Unit, bool Optimize)
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

            DirectionsResponse result = GoogleAPIHelper.GetDirections(Start, End, WayPoints, Mode.ToLowerInvariant(), avoid, Unit.ToLowerInvariant(), Optimize);
            return result;
        }
    }
}
