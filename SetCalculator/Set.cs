using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCalculator
{
    class Set
    {
        public List<int> collection { get; }

        public Set()
        {
            collection = new List<int>();
        }

        public void Add(int n)
        {
            if (collection.IndexOf(n) == -1)
            {
                collection.Add(n);
                collection.Sort();
            }
        }

        // === static part ===
        static public Set Merge(Set set1, Set set2)
        {
            return null;
        }
    }
}
