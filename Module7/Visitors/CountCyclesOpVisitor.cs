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
        public bool NowInCycle = false;
        public int MidCount()
        {
            return CountOps / CountCycles; 
        }
        
        public override void VisitCycleNode(CycleNode c) 
        {
            if (NowInCycle)
                CountOps += 1;
            CountCycles += 1;
            NowInCycle = true;
            c.Expr.Visit(this);
            c.Stat.Visit(this);
            NowInCycle = false;
        }
        
        public override void VisitAssignNode(AssignNode a) 
        {
            if (NowInCycle)
                CountOps += 1;
            // для каких-то визиторов порядок может быть обратный - вначале обойти выражение, потом - идентификатор
            a.Id.Visit(this);
            a.Expr.Visit(this);
            
        }
        public override void VisitBlockNode(BlockNode bl) 
        {
            if (NowInCycle)
                CountOps += 1;
            foreach (var st in bl.StList)
                st.Visit(this);
        }
        public override void VisitWriteNode(WriteNode w) 
        {
            if (NowInCycle)
                CountOps += 1;
            w.Expr.Visit(this);
        }
        public override void VisitVarDefNode(VarDefNode w) 
        {
            if (NowInCycle)
                CountOps += 1;
            foreach (var v in w.vars)
                v.Visit(this);
        }
    }
}
