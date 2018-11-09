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
        public int NowBinOp = 0;
        public List<int> list = new List<int>();
        public bool ComplAdded = false;

        public override void VisitBinOpNode(BinOpNode binop)
        {
            if (NowBinOp > 0)
                CurrCompl += 
                    (binop.Op == '+') || (binop.Op == '-') ? 1 : 3;
            NowBinOp += 1;
            binop.Left.Visit(this);
            binop.Right.Visit(this);
            NowBinOp -= 1;
            if (NowBinOp == 0)
                CurrCompl = 0;
        }
    }
}
