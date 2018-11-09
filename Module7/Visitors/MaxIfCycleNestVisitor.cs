using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;


namespace SimpleLang.Visitors
{
    public class MaxIfCycleNestVisitor : AutoVisitor
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

        public override void VisitIfNode(IfNode c)
        {
            CurrNest += 1;
            if (CurrNest > MaxNest)
                MaxNest = CurrNest;

            c.Expr.Visit(this);
            c.StTrue.Visit(this);
            if (!(c.StFalse is null))
                c.StFalse.Visit(this);

            CurrNest -= 1;
        }
    }


}