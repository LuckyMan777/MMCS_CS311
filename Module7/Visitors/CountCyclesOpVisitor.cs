using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class CountCyclesOpVisitor : AutoVisitor
    {
        public int CountOps = 0;
        public int CountCycles = 0;
        public int NowCycles = 0;
        public int MidCount()
        {
            if (CountCycles != 0)
                return (int)System.Math.Round((double)CountOps / CountCycles);
            else
                return 0;
        }
        
        public override void VisitCycleNode(CycleNode c) 
        {
            if (NowCycles > 0)
                CountOps += 1;
            CountCycles += 1;
            NowCycles += 1;
            c.Expr.Visit(this);
            c.Stat.Visit(this);
            NowCycles -= 1;
        }
        
        public override void VisitAssignNode(AssignNode a) 
        {
            if (NowCycles > 0)
                CountOps += 1;
            a.Id.Visit(this);
            a.Expr.Visit(this);
            
        }
        /*
        public override void VisitBlockNode(BlockNode bl) 
        {
            if (NowCycles > 0)
                CountOps += 1;
            foreach (var st in bl.StList)
                st.Visit(this);
        }
        
        public override void VisitWriteNode(WriteNode w) 
        {
            if (NowCycles > 0)
                CountOps += 1;
            w.Expr.Visit(this);
        }
        
        public override void VisitVarDefNode(VarDefNode w) 
        {
            if (NowCycles > 0)
                CountOps += 1;
            foreach (var v in w.vars)
                v.Visit(this);
        }*/
    }
}
