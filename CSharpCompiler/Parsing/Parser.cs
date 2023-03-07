using CSharpCompiler.Classes;
using CSharpCompiler.Lexer;
using CSharpCompiler.Parsing.Statements;
using CSharpCompiler.Statements;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Parsing
{
    internal class Parser
    {
        static string _LastSource = "\0";
        static List<StatementBase> _LastStmts = new List<StatementBase>();
        List<Token> SourceTokens;
        int index;
        Token ptr
        {
            get
            {
                if (!IsEnd())
                    return SourceTokens[index];
                return null;
            }
        }
        public Parser()
        {
            SourceTokens = new List<Token>();
            index = 0;
        }
        public List<StatementBase> Parsing(string source)
        {
            int cmp = string.Compare(_LastSource, source);
            if (cmp != 0)
            {
                _LastSource = source;
                _LastStmts.Clear();
                SharedData.ucErrors.Clear();
                SharedData.SymbolTable.Clear();
                Scanner scanner = new Scanner(source);
                foreach (Token t in scanner.Scan())
                {
                    if (t.Type == TokenType.WhiteSpace || t.Type == TokenType.NewLine)
                        continue;
                    SourceTokens.Add(t);
                }
                


                List<StatementBase> stmts = new List<StatementBase>();
                while (!IsEnd())
                {
                    stmts.Add(GetStatement());

                }
                _LastStmts.AddRange(stmts);
            }
            return _LastStmts;
        }
        public List<StatementBase> RunTimeCheck(string source)
        {

            Scanner scanner = new Scanner(source);
            foreach (Token t in scanner.Scan())
            {
                if (t.Type == TokenType.WhiteSpace || t.Type == TokenType.NewLine)
                    continue;
                SourceTokens.Add(t);
            }
            
            List<StatementBase> stmts = new List<StatementBase>();
            while (!IsEnd())
            {
                stmts.Add(GetStatement());

            }
            return stmts;

        }
        public List<StatementBase> RunTimeCheck(List<Token> source)
        {

            foreach (Token t in source)
            {
                if (t.Type == TokenType.WhiteSpace || t.Type == TokenType.NewLine)
                    continue;
                SourceTokens.Add(t);
            }
            
            List<StatementBase> stmts = new List<StatementBase>();
            while (!IsEnd())
            {
                stmts.Add(GetStatement());

            }
            return stmts;

        }

        private StatementBase GetStatement()
        {
            switch (ptr.Value)
            {

                case "if":
                    return CheckIfStatement();
                case "for":
                    return CheckForStatement();
                case "while":
                    return CheckWhileStatement();
                default:
                    if (SharedData.IsDataType(ptr.Value))
                        return CheckDeclaringVars();
                    //else if (RegularExp.IsAcceptIdentifier(ptr.Value) && GetNext() != null && RegularExp.IsAssignSymbol(GetNext().Value))
                    else if (IsAssignStatement())
                        return CheckAssignStatement();
                    else
                        return CheckOtherStatement();

            }
        }


        private StatementBase CheckOtherStatement()
        {
            if (ptr.Type == TokenType.Semicolon)
            {
                Token to = ptr;
                Advance();
                return new EmptyStatement(to);
            }
            List<Token> stmt = new List<Token>();
            stmt.AddRange(CheckMathExpression(TokenType.Semicolon));
            if (stmt.Count > 1 && !IsEnd() && ptr.Type == TokenType.Semicolon)
            {
                stmt.Add(ptr);
                Advance();
                return new ExpressionStatement(stmt);
            }
            else if (stmt.Count == 0)
            {
                stmt.Add(ptr);
                Advance();
            }

                return new OtherStatement(stmt);
        }

        private Token GetToken(int indx)
        {
            if (indx >= 0 && indx < SourceTokens.Count)
                return SourceTokens[indx];
            return null;
        }

        private bool IsAssignStatement()
        {
            Token next = GetNext();
            if (next == null)
                return false;
            if (ptr.Type == TokenType.Identifier && RegularExp.IsAssignSymbol(GetNext().Value))
                return true;
            else
            {
                Token last = GetToken(index + 2);
                if ((last == null || ";,)".Contains(last.Value)) && (SharedData.IsIncrmtDecrmtOperator(ptr.Value) || SharedData.IsIncrmtDecrmtOperator(next.Value)))
                    return true;
            }
            return false;

        }
        private StatementBase CheckAssignStatement()
        {

            Token left = ptr;
            Advance();
            Token ass = ptr;
            Advance();
            if (RegularExp.IsAssignSymbol(ass.Value))
            {
                List<Token> tokens = CheckMathExpression(TokenType.Semicolon, TokenType.RightParenthesis);
                if (!IsEnd() && ptr.Type == TokenType.Semicolon)
                {
                    tokens.Add(ptr);
                    Advance();
                }
                return new AssignStatement(left, ass, tokens);
            }
            Token end = null;
            if (!IsEnd() && (ptr.Type == TokenType.Semicolon))
            {
                end = ptr;
                Advance();
            }
            return new IncrmtDecrmtByOneStmt(left, ass, end);
        }

        private List<Token> CheckMathExpression(params TokenType[] stopToken)
        {
            List<Token> exp = new List<Token>();
            while (!IsEnd() && !stopToken.Contains(ptr.Type))
            {
                if (ptr.Type == TokenType.LeftParenthesis)
                {
                    exp.Add(ptr);
                    Advance();
                    exp.AddRange(CheckMathExpression(TokenType.RightParenthesis));
                    if (!IsEnd() && ptr.Type == TokenType.RightParenthesis)
                    {
                        exp.Add(ptr);
                        Advance();
                    }
                }
                else if (RegularExp.IsAcceptValue(ptr.Value) || SharedData.IsOperator(ptr.Value) || SharedData.IsIncrmtDecrmtOperator(ptr.Value)||ptr.Type==TokenType.Error)
                {
                    exp.Add(ptr);
                    Advance();
                }
                else
                {
                    break;
                }
            }
            return exp;

        }
        private StatementBase CheckWhileStatement()
        {

            List<Token> tokens = new List<Token>();

            IfWhileHeade heade = new IfWhileHeade();
            Body body = null;
            tokens.Add(ptr);
            Advance();
            if (!IsEnd() && ptr.Type == TokenType.LeftParenthesis)
            {
                tokens.Add(ptr);
                Advance();
                tokens.AddRange(CheckMathExpression(TokenType.RightParenthesis));
                if (!IsEnd() && ptr.Type == TokenType.RightParenthesis)
                {
                    tokens.Add(ptr);
                    Advance();
                }
                else
                {
                    GenerateError(GetPrevius(), "Missed \')\'");
                }
                heade = new IfWhileHeade(tokens);
                tokens.Clear();
            }
            if (!IsEnd() && ptr.Type == TokenType.LeftBracket)
            {
                SharedData.SymbolTable.AddStartScope(ptr);
                List<StatementBase> stb = new List<StatementBase>();
                Token open = ptr, close = null;
                Advance();
                stb.AddRange(GetBlock());
                if (!IsEnd() && ptr.Type == TokenType.RightBracket)
                {
                    SharedData.SymbolTable.AddEndScope(open, ptr);
                    close = ptr;
                    Advance();
                }
                body = new BlockBody(open, stb, close);
            }
            else if (!IsEnd())
            {
                StatementBase stmt = GetStatement();
                body = new SingleStatementBody(stmt);
                if (stmt.GetEndSymbol() != ";")
                    GenerateError(GetPrevius(), "Missed \';\'");
            }
            else
                GenerateError(GetPrevius(), "Missed \'while body\'");
            return new WhileStatement(heade, body);
        }

        private StatementBase CheckForStatement()
        {
            ForHeade heade = null;
            Body body = null;
            List<Token> h = new List<Token>();
            Token kwrd = ptr;
            Token opP = null, clsP = null;
            Advance();

            if (!IsEnd() && ptr.Type == TokenType.LeftParenthesis)
            {
                opP = ptr;
                SharedData.SymbolTable.AddStartScope(ptr);

                StatementBase cond = null;
                List<StatementBase> initstmt = new List<StatementBase>();
                List<StatementBase> incrmtPart = new List<StatementBase>();
                h.Add(ptr);
                Advance();

                if (IsEnd() || ptr.Type == TokenType.RightParenthesis)
                {
                    GenerateError(GetPrevius(), "Missed \';\'");
                }
                else
                {
                    if (ptr.Type == TokenType.Semicolon)
                        initstmt.Add(GetStatement());
                    else
                        while (!IsEnd() && ptr.Type != TokenType.RightParenthesis)
                        {
                            initstmt.Add(GetStatement());
                            string ends = initstmt[initstmt.Count - 1].GetEndSymbol();
                            if (ends == ";")
                                break;
                            else if (!IsEnd() && ptr.Type == TokenType.Comma)
                                Advance();
                        }
                    if (IsEnd() || ptr.Type == TokenType.RightParenthesis)
                        GenerateError(GetPrevius(), "Missed \';\'");
                    else 
                    { 
                    cond = GetStatement();
                    if (cond.GetEndSymbol() != ";")
                        GenerateError(GetPrevius(), "Missed \';\'");
                    while (!IsEnd() && ptr.Type != TokenType.RightParenthesis)
                    {
                        incrmtPart.Add(GetStatement());
                        if (!(incrmtPart[incrmtPart.Count - 1] is AssignStmt))
                            GenerateError(GetPrevius(), "invalid expression");
                        if (!IsEnd() && ptr.Type == TokenType.Comma)
                            Advance();
                    }
                    if (IsEnd() || ptr.Type != TokenType.RightParenthesis)
                        GenerateError(GetPrevius(), "Missed \')\'");
                    else
                    {
                        clsP = ptr;
                        Advance();
                    }
                }
                }
                heade = new ForHeade(kwrd, opP, initstmt, cond, incrmtPart, clsP);
            }
            if (!IsEnd() && ptr.Type == TokenType.LeftBracket)
            {
                Token open = ptr, close = null;
                SharedData.SymbolTable.AddStartScope(ptr);

                Advance();

                List<StatementBase> stb = new List<StatementBase>();
                stb.AddRange(GetBlock());
                if (!IsEnd() && ptr.Type == TokenType.RightBracket)
                {
                    close = ptr;
                    SharedData.SymbolTable.AddEndScope(ptr);

                    Advance();
                }

                body = new BlockBody(open, stb, close);
            }
            else if (!IsEnd())
            {
                StatementBase stmt = GetStatement();
                body = new SingleStatementBody(stmt);
                if (stmt.GetEndSymbol() != ";")
                    GenerateError(GetPrevius(), "Missed \';\'");
            }
            else
                GenerateError(GetPrevius(), "Missed \'for body\'");
            if(opP!=null)
            SharedData.SymbolTable.AddEndScope(opP,GetPrevius());

            return new ForStatement(heade, body);
        }

        private StatementBase CheckDeclaringVars()
        {
            Token previ = GetPrevius();
            if (previ != null && !";({}".Contains(previ.Value))
                GenerateError(previ, "Messed \';\'");
            List<Token> tokens = new List<Token>();
            tokens.Add(ptr);
            Advance();
            tokens.AddRange(ScanTo(TokenType.Semicolon));
            if (!IsEnd() && ptr.Type == TokenType.Semicolon)
            {
                tokens.Add(ptr);
                Advance();
            }
            return new DeclaringVarsStatement(tokens);
        }

        private List<Token> ScanTo(TokenType stopToken)
        {
            List<Token> tokens = new List<Token>();
            while (!IsEnd() && ptr.Type != stopToken)
            {
                if ((ptr.Type == TokenType.LeftBracket || ptr.Type == TokenType.RightBracket) && (stopToken == TokenType.Semicolon || stopToken == TokenType.RightParenthesis))
                    break;
                if (ptr.Type == TokenType.Keyword && GetPrevius() != null && GetPrevius().Type != TokenType.LeftParenthesis)
                    break;
                if (ptr.Type == TokenType.LeftParenthesis)
                {
                    tokens.Add(ptr);
                    Advance();
                    tokens.AddRange(ScanTo(TokenType.RightParenthesis));
                    if (!IsEnd() && ptr.Type == TokenType.RightParenthesis)
                    {
                        tokens.Add(ptr);
                        Advance();
                    }
                    continue;
                }


                tokens.Add(ptr);
                Advance();
            }

            if (IsEnd() || ptr.Type != stopToken)
            {
                SharedData.ucErrors.AddError(GetPrevius(), $"Missed \'{stopToken}\'");
            }
            return tokens;
        }

        private StatementBase CheckIfStatement()
        {

            List<Token> tokens = new List<Token>();

            IfWhileHeade heade = null;
            Body ifbody = null;
            Body elsebody = null;
            ElsePart elsePart = null;
            tokens.Add(ptr);
            Advance();
            if (!IsEnd() && ptr.Type == TokenType.LeftParenthesis)
            {
                tokens.Add(ptr);
                Advance();
                tokens.AddRange(CheckMathExpression(TokenType.RightParenthesis));
                if (!IsEnd() && ptr.Type == TokenType.RightParenthesis)
                {
                    tokens.Add(ptr);
                    Advance();
                }
                else
                    GenerateError(GetPrevius(), "Missed  \')'");

                heade = new IfWhileHeade(tokens);
                tokens.Clear();
            }
            else
                GenerateError(GetPrevius(), "Missed if heade \'('");

            if (!IsEnd() && ptr.Type == TokenType.LeftBracket)
            {
                Token open = ptr, close = null;
                SharedData.SymbolTable.AddStartScope(open);
                Advance();
                List<StatementBase> stb = new List<StatementBase>();
                stb.AddRange(GetBlock());
                if (!IsEnd() && ptr.Type == TokenType.RightBracket)
                {
                    SharedData.SymbolTable.AddEndScope(open, ptr);
                    close = ptr;
                    Advance();
                }
                ifbody = new BlockBody(open, stb, close);
            }
            else if (!IsEnd())
            {
                StatementBase stmt = GetStatement();
                ifbody = new SingleStatementBody(stmt);
                if (stmt.GetEndSymbol() != ";")
                    GenerateError(GetPrevius(), "Missed \';\'");

            }
            else
                GenerateError(GetPrevius(), "Missed \'if body\'");

            if (!IsEnd() && ptr.Value.ToLower() == "else")
            {
                Advance();
                if (!IsEnd() && ptr.Type == TokenType.LeftBracket)
                {
                    Token open = ptr, close = null;
                    SharedData.SymbolTable.AddStartScope(open);

                    Advance();
                    List<StatementBase> stb = new List<StatementBase>();
                    stb.AddRange(GetBlock());
                    if (!IsEnd() && ptr.Type == TokenType.RightBracket)
                    {
                        SharedData.SymbolTable.AddEndScope(open, ptr);
                        close = ptr;
                        Advance();
                    }

                    elsebody = new BlockBody(open, stb, close);
                }
                else if (!IsEnd())
                {
                    StatementBase stmt = GetStatement();
                    elsebody = new SingleStatementBody(stmt);
                    if (stmt.GetEndSymbol() != ";")
                        GenerateError(GetPrevius(), "Missed \';\'");
                }
                else
                    GenerateError(GetPrevius(), "Missed \'else body\'");
                elsePart = new ElsePart(elsebody);
            }
            return new IfStmt(heade, ifbody, elsePart);
        }

        private List<StatementBase> GetBlock()
        {
            List<StatementBase> block = new List<StatementBase>();
            while (!IsEnd() && ptr.Type != TokenType.RightBracket)
            {
                if (ptr.Type == TokenType.LeftBracket)
                {
                    GenerateError(GetPrevius(), "Missed \'}\'");

                }

                block.Add(GetStatement());
                if (block[block.Count - 1].GetEndSymbol() != ";")
                    GenerateError(GetPrevius(), "Missed \';\'");
            }
            if (IsEnd())
            {
                GenerateError(GetPrevius(), "Missed \'}\'");
            }
            return block;
        }
        private void GenerateError(Token tokenerr, string ditailes)
        {
            SharedData.ucErrors.AddError(tokenerr, ditailes);
        }

        Token GetPrevius()
        {

            if (index - 1 >= 0)
                return SourceTokens[index - 1];

            return null;
        }
        Token GetNext()
        {
            if (index + 1 < SourceTokens.Count)
                return SourceTokens[index + 1];
            return null;

        }

        private void Advance()
        {
            index++;

        }

        bool IsEnd()
        {
            return !(index < SourceTokens.Count);
        }
    }
}
