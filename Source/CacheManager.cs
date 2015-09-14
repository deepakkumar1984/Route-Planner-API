using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RoutePlanner.GoogleAPIHelper
{
    public class CacheManager
    {
        private static int MaxRequestLimit = 2500;
        public static DateTime DistanceMatrixLastProcessedTime
        {
            get
            {
                DateTime result = DateTime.MinValue;
                if (HttpContext.Current.Cache["DistanceMatrixLastProcessedTime"] != null)
                {
                    result = Convert.ToDateTime(HttpContext.Current.Cache["DistanceMatrixLastProcessedTime"]);
                }

                return result;
            }
            set
            {
                HttpContext.Current.Cache["DistanceMatrixLastProcessedTime"] = value;
            }
        }

        public static int DistanceMatrixRequestCountLeft
        {
            get
            {
                int result = MaxRequestLimit;
                if (HttpContext.Current.Cache["DistanceMatrixRequestCountLeft"] != null)
                {
                    result = Convert.ToInt32(HttpContext.Current.Cache["DistanceMatrixRequestCountLeft"]);
                    result = MaxRequestLimit - result;
                }

                return result;
            }
        }

        public static DateTime DirectionsLastProcessedTime
        {
            get
            {
                DateTime result = DateTime.MinValue;
                if (HttpContext.Current.Cache["DirectionsLastProcessedTime"] != null)
                {
                    result = Convert.ToDateTime(HttpContext.Current.Cache["DirectionsLastProcessedTime"]);
                }

                return result;
            }
            set
            {
                HttpContext.Current.Cache["DirectionsLastProcessedTime"] = value;
            }
        }

        public static int DirectionsRequestCountLeft
        {
            get
            {
                int result = MaxRequestLimit;
                if (HttpContext.Current.Cache["DirectionsRequestCountLeft"] != null)
                {
                    result = Convert.ToInt32(HttpContext.Current.Cache["DirectionsRequestCountLeft"]);
                    result = MaxRequestLimit - result;
                }

                return result;
            }
        }

        public static string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiKey"];
            }
        }

        public static void SetLastProcessingInfo(int requestCount)
        {
            int currentRequestCount = 0;
            if (HttpContext.Current.Cache["RequestCount"] != null)
            {
                currentRequestCount = Convert.ToInt32(HttpContext.Current.Cache["RequestCount"]);
            }

            currentRequestCount += requestCount;
            HttpContext.Current.Cache["RequestCount"] = currentRequestCount;
            HttpContext.Current.Cache["LastProcessedTime"] = DateTime.Now;
        }
    }
}