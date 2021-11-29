using System;
using System.Collections.Generic;
using System.Text;

namespace Clasterization.Location
{
    public class Cluster<T> where T : IPoint
    {
        public List<T> Items;

        public Cluster()
        {
            Items = new List<T>();
        }
    }
}
