using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Models.Redis
{
    public class Record<TValue>
    {
        public string Key { get; set; }
        public TValue Value { get; set; }

        public Record(string key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
