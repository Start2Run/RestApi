using System.Collections.Generic;

namespace Common.Models.RestApi
{
    public class Root
    {
        public List<Datum> data { get; set; }
        public int count { get; set; }
    }
}