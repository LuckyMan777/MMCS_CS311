using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class CommonlyUsedVarVisitor : AutoVisitor
    {
        public string mostCommonlyUsedVar()
        {
            int max = 0;
            string id = "";
            foreach (var item in d.Keys)
            {
                if (d[item] > max)
                {
                    max = d[item];
                    id = item;
                }
            }
            return id;
        }

        public string Id = "";
        public Dictionary<string, int> d = new Dictionary<string, int>();

        public override void VisitIdNode(IdNode id)
        {
            if (d.ContainsKey(id.Name))
                ++d[id.Name];
            else
                d.Add(id.Name, 1);
        }
    }
}
