using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    // базовая логика обхода без действий
    // Если нужны действия или другая логика обхода, то соответствующие методы надо переопределять
    // При переопределении методов для задания действий необходимо не забывать обходить подузлы
    public class AutoVisitor: Visitor
    {
        public override void VisitBinOpNode(BinOpNode binop) 
        {
            binop.Left.Visit(this);
            binop.Right.Visit(this);
        }
        public override void VisitAssignNode(AssignNode a) 
        {
            // для каких-то визиторов порядок может быть обратный - вначале обойти выражение, потом - идентификатор
            a.Id.Visit(this);
            (a.Expr as ExprNode).Visit(this);
        }
        public override void VisitCycleNode(CycleNode c) 
        {
            c.Expr.Visit(this);
            c.Stat.Visit(this);
        }
        public override void VisitBlockNode(BlockNode bl) 
        {
            foreach (var st in bl.StList)
                st.Visit(this);
        }
        public override void VisitWriteNode(WriteNode w) 
        {
            w.Expr.Visit(this);
        }
        public override void VisitVarDefNode(VarDefNode w) 
        {
            foreach (var v in w.vars)
                v.Visit(this);
        }
        public override void VisitIfNode(IfNode c)
        {
            c.Expr.Visit(this);
            c.StTrue.Visit(this);
            if (!(c.StFalse is null))
                c.StFalse.Visit(this);
        }
        public override void VisitExprNode(ExprNode w)
        {
            if (w is IntNumNode)
                (w as IntNumNode).Visit(this);
            if (w is IdNode)
                (w as IdNode).Visit(this);
            if (w is BinOpNode)
                (w as BinOpNode).Visit(this);
        }
    }
}
