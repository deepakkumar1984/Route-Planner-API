using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoutePlanner.GoogleAPIHelper.Models
{
    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public List<Element> elements { get; set; }
    }

    public class DistanceMatrixResponse
    {
        public DistanceMatrixResponse()
        {
            destinationAddresses = new List<string>();
            originAddresses = new List<string>();
            rows = new List<Row>();
        }

        public DistanceMatrixResponse(int index)
            : this()
        {
            Index = index;
        }

        public int Index { get; set; }
        public List<string> destinationAddresses { get; set; }
        public List<string> originAddresses { get; set; }
        public List<Row> rows { get; set; }
        public string status { get; set; }
    }
}