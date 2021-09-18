using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCalculator
{
    class Set
    {
        public List<int> Collection { get; }

        static public int UniversumMax = 20;
        static public int UniversumMin = -20;

        public Set()
        {
            Collection = new List<int>();
        }

        public Set(List<int> collection)
        {
            Collection = new List<int>();
            for (int i = 0; i < collection.Count; i++)
            {
                this.Add(collection[i]);
            }
        }

        public override string ToString()
        {
            string res = "";
            foreach (var elem in Collection)
            {
                res += elem.ToString() + " ";
            }
            // delete last space
            if (res.Length > 0) res = res.Remove(res.Length - 1);
            return res;
        }

        public void Add(int n)
        {
            if (Collection.IndexOf(n) == -1)
            {
                Collection.Add(n);
                Collection.Sort();
            }
        }

        // === static part ===
        static public Set Addition(Set set)
        {
            Set res = new Set();
            for (int i = UniversumMin; i <= UniversumMax; i++)
            {
                if (set.Collection.IndexOf(i) == -1)
                    res.Add(i);
            }
            return res;
        }

        static public Set Merge(Set set1, Set set2)
        {
            Set res = new Set();
            for (int i = 0; i < set1.Collection.Count; i++)
            {
                res.Add(set1.Collection[i]);
            }
            for (int i = 0; i < set2.Collection.Count; i++)
            {
                res.Add(set2.Collection[i]);
            }
            return res;
        }

        static public Set Crossing(Set set1, Set set2)
        {
            Set res = new Set();
            for (int i = 0; i < set1.Collection.Count; i++)
            {
                if (set2.Collection.IndexOf(set1.Collection[i]) != -1)
                    res.Add(set1.Collection[i]);
            }
            for (int i = 0; i < set2.Collection.Count; i++)
            {
                if (set1.Collection.IndexOf(set2.Collection[i]) != -1)
                    res.Add(set2.Collection[i]);
            }
            return res;
        }

        static public Set Difference(Set set1, Set set2)
        {
            Set res = new Set();
            for (int i = 0; i < set1.Collection.Count; i++)
            {
                if (set2.Collection.IndexOf(set1.Collection[i]) == -1)
                {
                    res.Add(set1.Collection[i]);
                }
            }
            return res;
        }

        static public Set SymetricDifference(Set set1, Set set2)
        {
            Set res = new Set();
            res = Set.Merge(Set.Difference(set1, set2), Set.Difference(set2, set1));
            return res;
        }
    }
}
