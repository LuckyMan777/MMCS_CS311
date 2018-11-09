using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class ExprComplexityVisitor : AutoVisitor
    {
        // список должен содержать сложность каждого выражения, встреченного при обычном порядке обхода AST
        public List<int> getComplexityList()
        {
            throw new NotImplementedException();
        }

        public int CurrCompl = 0;
        public int CurrNest = 0;
        public List<int> list = new List<int>();
        public bool NowIsExpr = false;

        public override void VisitBinOpNode(BinOpNode binop)
        {
            CurrNest += 1;
            if (CurrNest > 0)
                CurrCompl += 
                    (binop.Op == '+') || (binop.Op == '-') ? 1 : 3;   
            binop.Left.Visit(this);
            binop.Right.Visit(this);
            CurrNest -= 1;
            if (CurrNest == 0)
            {
                if (CurrCompl != 0)
                    list.Add(CurrCompl);
                CurrCompl = 0;
            }
        }

        public override void VisitIdNode(IdNode id)
        {
            if (NowIsExpr)
            {
                if (CurrNest == 0)
                {
                    list.Add(0);
                }
            }
        }

        public override void VisitIntNumNode(IntNumNode id)
        {
            if (NowIsExpr)
            {
                if (CurrNest == 0)
                {
                    list.Add(0);
                }
            }
        }
    }
}
