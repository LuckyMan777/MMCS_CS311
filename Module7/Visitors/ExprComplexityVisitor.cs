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
        public int CurrNestExprs = 0;

        public override void VisitBinOpNode(BinOpNode binop)
        {
            CurrNest += 1;
            CurrCompl += 
                (binop.Op == '+') || (binop.Op == '-') ? 1 : 3;
            binop.Left.Visit(this);
            binop.Right.Visit(this);
            CurrNest -= 1;
            if (CurrNest == 0)
            {
                //if (CurrCompl != 0)
                list.Add(CurrCompl);
                CurrCompl = 0;
            }
        }

        public override void VisitIdNode(IdNode id)
        {
            if (CurrNestExprs > 0)
            {
                if (CurrNest == 0)
                {
                    list.Add(0);
                }
            }
        }

        public override void VisitIntNumNode(IntNumNode id)
        {
            if (CurrNestExprs > 0)
            {
                if (CurrNest == 0)
                {
                    list.Add(0);
                }
            }
        }

        public override void VisitAssignNode(AssignNode a)
        {
            // для каких-то визиторов порядок может быть обратный - вначале обойти выражение, потом - идентификатор
            a.Id.Visit(this);
            ++CurrNestExprs;
            (a.Expr as ExprNode).Visit(this);
            --CurrNestExprs;
        }
        public override void VisitCycleNode(CycleNode c)
        {
            ++CurrNestExprs;
            c.Expr.Visit(this);
            --CurrNestExprs;
            c.Stat.Visit(this);
        }
        public override void VisitWriteNode(WriteNode w)
        {
            ++CurrNestExprs;
            w.Expr.Visit(this);
            --CurrNestExprs;
        }
        public override void VisitIfNode(IfNode c)
        {
            ++CurrNestExprs;
            c.Expr.Visit(this);
            --CurrNestExprs;
            c.StTrue.Visit(this);
            if (!(c.StFalse is null))
                c.StFalse.Visit(this);
        }
    }
}
