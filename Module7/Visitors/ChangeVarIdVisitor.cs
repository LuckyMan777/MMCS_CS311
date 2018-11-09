using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class ChangeVarIdVisitor : PrettyPrintVisitor
    {
        private string from, to;

        public ChangeVarIdVisitor(string _from, string _to)
        {
            from = _from;
            to = _to;
        }

      
        public override void VisitIdNode(IdNode id)
        {
            Text += (id.Name == from) ? to : id.Name;
        }
       
        public override void VisitVarDefNode(VarDefNode w)
        {
            Text += IndentStr() + "var ";
            Text += (w.vars[0].Name == from) ? to : w.vars[0].Name;
            for (int i = 1; i < w.vars.Count; i++)
            {
                Text += ',';
                Text += (w.vars[i].Name == from) ? to : w.vars[i].Name;
            }//w.vars[i].Name;
        }
    }
}
