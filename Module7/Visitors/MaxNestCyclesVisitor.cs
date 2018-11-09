using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class MaxNestCyclesVisitor : AutoVisitor
    {
        public int MaxNest = 0;
        public int CurrNest = 0;


        public override void VisitCycleNode(CycleNode c)
        {
            CurrNest += 1;
            if (CurrNest > MaxNest)
                MaxNest = CurrNest;
            c.Expr.Visit(this);
            c.Stat.Visit(this);
            CurrNest -= 1;
        }
    }
}
