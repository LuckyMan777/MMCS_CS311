using System;
using NUnit.Framework;
using SimpleScanner;
using SimpleParser;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestASTParser
{
    public class ASTParserTests
    {
        public static JObject Parse(string text)
        {
            Scanner scanner = new Scanner();
            scanner.SetSource(text, 0);

            Parser parser = new Parser(scanner);

            var b = parser.Parse();
            if (!b)
                Assert.Fail("программа не распознана");
            else
            {
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                jsonSettings.TypeNameHandling = TypeNameHandling.All;
                string output = JsonConvert.SerializeObject(parser.root, jsonSettings);
                return JObject.Parse(output);
            }

            return null;

        }
    }
    
    [TestFixture]
    public class WhileTests
    {
        
        [Test]
        public void TestWhile()
        {
            var tree = ASTParserTests.Parse("begin while 2 do a:=2 end");
            Assert.AreEqual("ProgramTree.WhileNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);   
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("2", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["Stat"]["$type"]);
        }
    }
    
    [TestFixture]
    public class RepeatTests
    {
        
        [Test]
        public void TestRepeat()
        {
            var tree = ASTParserTests.Parse("begin repeat a:=10 until 8 end");
            Assert.AreEqual("ProgramTree.RepeatNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);   
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("8", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.BlockNode, SimpleLang", (string)tree["StList"]["$values"][0]["Stat"]["$type"]);
        }
    }
    
    [TestFixture]
    public class ForTests
    {
        
        [Test]
        public void TestFor()
        {
            var tree = ASTParserTests.Parse("begin for i:=2 to 10 do a:=2 end");
            Assert.AreEqual("ProgramTree.ForNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["Ass"]["$type"]);
            Assert.AreEqual("2", ((string)tree["StList"]["$values"][0]["Ass"]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["Stat"]["$type"]);
        }
    }
    
    [TestFixture]
    public class WriteTests
    {
        
        [Test]
        public void TestWrite()
        {
            var tree = ASTParserTests.Parse("begin write(2) end");
            Assert.AreEqual("ProgramTree.WriteNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("2", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
        }
    }

    [TestFixture]
    public class IfTests
    {

        [Test]
        public void TestIf()
        {
            var tree = ASTParserTests.Parse("begin if 3 then a:= 5 end");
            Assert.AreEqual("ProgramTree.IfNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("3", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["StTrue"]["$type"]);
        }

        [Test]
        public void TestIfElse()
        {
            var tree = ASTParserTests.Parse("begin if 7 then a:= 5 else a:= 9 end");
            Assert.AreEqual("ProgramTree.IfNode, SimpleLang", (string)tree["StList"]["$values"][0]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("7", ((string)tree["StList"]["$values"][0]["Expr"]["Num"]).Trim());
            Assert.AreEqual("ProgramTree.AssignNode, SimpleLang", (string)tree["StList"]["$values"][0]["StFalse"]["$type"]);
        }
    }

    [TestFixture]
    public class BinaryTests
    {

        [Test]
        public void TestBinary()
        {
            var tree = ASTParserTests.Parse("begin a := (5*2) * (9-1) end");
            Assert.AreEqual("ProgramTree.BinaryNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["$type"]);
            Assert.AreEqual("ProgramTree.BinaryNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Left"]["$type"]);
            Assert.AreEqual("ProgramTree.IntNumNode, SimpleLang", (string)tree["StList"]["$values"][0]["Expr"]["Left"]["Left"]["$type"]);
            Assert.AreEqual("5", ((string)tree["StList"]["$values"][0]["Expr"]["Left"]["Left"]["Num"]).Trim());
        }
    }
}