using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Knightrunner.Library.ExpressionParsing
{
    /// <summary>
    /// A lexical analyzer.
    /// </summary>
    public class Lex
    {
        private Dictionary<string, Token> words = new Dictionary<string, Token>();

        private TextReader reader;
        private StringBuilder lexeme = new StringBuilder();
        private Token token;
        private bool consumeSign;
        private CultureInfo cultureInfo;
        private string errorMsg;

        private int currentLine;
        private int currentColumn;

        private readonly char[] escapeSources = new char[] { '\'', '"', '\\', '0', 'a', 'b', 'f', 'n', 'r', 't', 'v' };
        private readonly char[] escapeDestinations = new char[] { '\'', '"', '\\', '\0', '\a', '\b', '\f', '\n', '\r', '\t', '\v' };

        private class SymbolToken
        {
            public string Symbol { get; set; }
            public Token Token1 { get; set; }
            public Token Token2 { get; set; }
        }

        private readonly SymbolToken[] symbolTokens = new SymbolToken[]
		{
        	new SymbolToken { Symbol = "||", Token1 = Token.BitwiseOr, Token2 = Token.Or },
        	new SymbolToken { Symbol = "&&", Token1 = Token.BitwiseAnd, Token2 = Token.And },
        	new SymbolToken { Symbol = "~", Token1 = Token.BitwiseNot },
        	new SymbolToken { Symbol = "+", Token1 = Token.Plus },
        	new SymbolToken { Symbol = "-", Token1 = Token.Minus },
        	new SymbolToken { Symbol = "**", Token1 = Token.Multiply, Token2 = Token.Power },
        	new SymbolToken { Symbol = "/", Token1 = Token.Divide },
        	new SymbolToken { Symbol = "%", Token1 = Token.Modulus },
        	new SymbolToken { Symbol = "=", Token1 = Token.Equal },
        	new SymbolToken { Symbol = "!=", Token1 = Token.Not, Token2 = Token.NotEqual },
        	new SymbolToken { Symbol = ">=", Token1 = Token.Greater, Token2 = Token.GreaterOrEqual },
        	new SymbolToken { Symbol = "<=", Token1 = Token.Less, Token2 = Token.LessOrEqual },
            new SymbolToken { Symbol = "(", Token1 = Token.OpenParentheses },
            new SymbolToken { Symbol = ")", Token1 = Token.ClosedParentheses },
            new SymbolToken { Symbol = "[", Token1 = Token.OpenBracket },
            new SymbolToken { Symbol = "]", Token1 = Token.ClosedBracket },
            new SymbolToken { Symbol = ".", Token1 = Token.Dot },
		};

        public Lex()
        {
            // Use the Invariant culture as default
            this.cultureInfo = CultureInfo.InvariantCulture;

            AddKeywords();
        }

        private void Reset()
        {
            lexeme.Length = 0;
            errorMsg = null;
            token = Token.None;
            currentLine = 1;
            currentColumn = 1;
        }


        private void AddKeywords()
        {
            //AddWord("using", Token.Using);
            //AddWord("namespace", Token.Namespace);
            //AddWord("static", Token.Static);
            //AddWord("abstract", Token.Abstract);
            //AddWord("sealed", Token.Sealed);
            //AddWord("class", Token.Class);
            //AddWord("interface", Token.Interface);
            //AddWord("enum", Token.Enum);
            //AddWord("public", Token.Public);
            //AddWord("internal", Token.Internal);
            //AddWord("protected", Token.Protected);
            //AddWord("private", Token.Private);
            //AddWord("get", Token.Get);
            //AddWord("set", Token.Set);
            //AddWord("int", Token.Int);
            //AddWord("bool", Token.Bool);
            //AddWord("string", Token.String);
            //AddWord("float", Token.Float);
            //AddWord("void", Token.Void);
            //AddWord("ref", Token.Ref);
            //AddWord("out", Token.Out);
            //AddWord("return", Token.Return);
            //AddWord("true", Token.True);
            //AddWord("false", Token.False);
        }


        public void AddWord(string word, Token token)
        {
            word = word.ToLower(cultureInfo);
            words.Add(word, token);
        }


        public CultureInfo CultureInfo
        {
            get { return cultureInfo; }
            set { cultureInfo = value; }
        }

        public TextReader TextReader
        {
            get { return reader; }
            set
            {
                reader = value;
                Reset();
            }
        }

        public string Lexeme
        {
            get { return lexeme.ToString(); }
        }

        public Token Token
        {
            get { return token; }
        }

        public string ErrorMsg
        {
            get { return errorMsg; }
        }

        public int Line
        {
            get { return currentLine; }
        }

        public int Column
        {
            get { return currentColumn; }
        }

        public Token Next()
        {
            return Next(true);
        }

        public Token Next(bool consumeSign)
        {
            this.consumeSign = consumeSign;
            Token t;

            while ((t = DoNext()) == Token.Comments)
                ;

            return t;
        }
        
        public string GetTokenString(Token token)
        {
            foreach (var symbolToken in this.symbolTokens)
            {
                if (symbolToken.Token1 == token)
                {
                    return symbolToken.Symbol[0].ToString();
                }
                else if (symbolToken.Token2 == token)
                {
                    return symbolToken.Symbol[1].ToString();
                }
            }

            foreach (var entry in this.words)
            {
                if (entry.Value == token)
                {
                    return entry.Key;
                }
            }

            return null;
        }

        public Token DoNext()
        {
            if (reader == null)
            {
                throw new InvalidOperationException("The TextReader property has not been set");
            }

            // Eat whitespace
            int ch;
            while ((ch = reader.Peek()) != -1 && char.IsWhiteSpace((char)ch))
            {
                Accept();
            }

            if (ch == -1)
                return SetToken(Token.EndOfSource, null);

            lexeme.Length = 0;

            bool signConsumed = false;
            char c = (char)ch;

            // Symbols
            {
                Token token = GetSymbolToken(ref c);
                if (token != Token.None)
                {
                    if (consumeSign)
                    {
                        if (token == Token.Minus || token == Token.Plus)
                        {
                            signConsumed = true;
                            ch = reader.Peek();
                            if (ch != -1)
                            {
                                c = (char)ch;
                                token = Token.None;     // signal that we want to continue
                            }
                        }
                    }

                    if (token != Token.None)
                    {
                        return SetToken(token);
                    }
                }
            }

            if (char.IsDigit(c))
            {
                Accept();

                // IntLiteral or FloatLiteral
                bool isFloat = false;

                // RegEx: [0-9]+\.?[0-9]+([eE][-+]?[0-9]+)? 
                while (Match(x => char.IsDigit(x)))
                {
                    Accept();
                }

                // Decimal point
                if (Match(x => IsNumberDecimalSeparator(x)))
                {
                    Accept();
                    isFloat = true;
                    if (Match(x => char.IsDigit(x)))
                    {
                        Accept();
                        while (Match(x => char.IsDigit(x)))
                        {
                            Accept();
                        }
                    }
                    else
                    {
                        // Missing digit after decimal separator
                        return Error("Invalid number format");
                    }
                }

                // Scientific format
                if (Match(new char[] { 'e', 'E' }))
                {
                    Accept();
                    isFloat = true;
                    if (Match(new char[] { '+', '-' }))
                    {
                        Accept();
                    }

                    if (Match(x => char.IsDigit(x)))
                    {
                        Accept();
                        while (Match(x => char.IsDigit(x)))
                        {
                            Accept();
                        }

                    }
                    else
                    {
                        return Error("Invalid number format");
                    }
                }

                if (!isFloat)
                    return SetToken(Token.IntLiteral);
                else
                    return SetToken(Token.FloatLiteral);
            }

            if (signConsumed)
            {
                return Error("Unexpected sign");
            }

            if (c == '"')
            {
                // StringLiteral
                Accept();

                StringBuilder sb = new StringBuilder();

                bool endFound = false;
                while ((ch = reader.Peek()) != -1 && !endFound)
                {
                    c = (char)ch;

                    if (c == '\\')
                    {
                        // Escape sequence

                        Accept();

                        string errorMsg;
                        char theChar;
                        if (!ParseEscapeSequence(out theChar, out errorMsg))
                        {
                            return Error(errorMsg);
                        }
                        sb.Append(theChar);
                    }
                    else if (c == '"')
                    {
                        Accept();
                        endFound = true;
                    }
                    else if (c == '\n')
                    {
                        return Error("Newline in string literal not allowed");
                    }
                    else
                    {
                        sb.Append(c);
                        Accept();
                    }
                }

                if (!endFound)
                {
                    return Error("String literal not terminated");
                }
                else
                {
                    return SetToken(Token.StringLiteral, sb.ToString());
                }
            }
            else if (c == '\'')
            {
                // Char literal
                Accept();

                ch = reader.Peek();
                if (ch == -1)
                    return Error("Invalid char literal");

                c = (char)ch;

                char theChar;

                if (c == '\\')
                {
                    // Escape sequence
                    Accept();

                    string errorMsg;
                    if (!ParseEscapeSequence(out theChar, out errorMsg))
                    {
                        return Error(errorMsg);
                    }
                }
                else if (c == '\'' || c == '\n')
                {
                    return Error("Invalid char literal");
                }
                else
                {
                    theChar = c;
                    Accept();
                }

                ch = reader.Peek();
                if (ch == -1)
                {
                    return Error("Char literal not terminated");
                }

                c = (char)ch;
                if (c != '\'')
                {
                    return Error("Char literal not terminated");
                }
                Accept();

                return SetToken(Token.CharLiteral, theChar.ToString());

            }
            else if (c == '_' || char.IsLetter(c))
            {
                // Identifier or keyword

                do
                {
                    Accept();

                    ch = reader.Peek();
                    if (ch != -1)
                    {
                        c = (char)ch;
                    }
                } while (ch != -1 && (char.IsLetterOrDigit(c) || c == '_'));

                string word = lexeme.ToString();

                Token token;
                if (words.TryGetValue(word, out token))
                {
                    return SetToken(token);
                }
                else
                {
                    return SetToken(Token.Identifier);
                }
            }
            else
            {
                return Error("Unknown lexeme");
            }
        }

        private ExpressionParsing.Token GetSymbolToken(ref char c)
        {
            for (int i = 0; i < symbolTokens.Length; i++)
            {
                SymbolToken symbolToken = symbolTokens[i];
                if (c == symbolToken.Symbol[0])
                {
                    Accept();
                    if (symbolToken.Symbol.Length == 1)
                    {
                        return symbolToken.Token1;
                    }

                    int ch = reader.Peek();
                    if (ch == -1)
                        return Error("Unknown symbol");

                    c = (char)ch;
                    if (c == symbolToken.Symbol[1])
                    {
                        Accept();
                        return symbolToken.Token2;
                    }
                    else if (symbolToken.Token1 != Token.None)
                    {
                        return symbolToken.Token1;
                    }
                    else
                    {
                        return Error("Unknown symbol");
                    }
                }
            }
            return Token.None;
        }

        private bool ParseEscapeSequence(out char theChar, out string errorMsg)
        {
            theChar = '\0';
            errorMsg = null;

            int ch = reader.Peek();
            if (ch == -1)
            {
                errorMsg = "Invalid escape sequence";
                return false;
            }

            char c = (char)ch;
            if (c == 'u' || c == 'U' || c == 'x')
            {
                // Unicode escape sequence
                Accept();
                throw new NotImplementedException("Unicode escape sequences not yet implemented");
            }

            for (int i = 0; i < escapeSources.Length; i++)
            {
                if (c == escapeSources[i])
                {
                    Accept();
                    theChar = escapeDestinations[i];
                    return true;
                }
            }

            errorMsg = "Invalid escape sequence";
            return false;
        }

        private Token Error(string msg)
        {
            return Error(lexeme.ToString(), msg);
        }

        private Token Error(string lex, string msg)
        {
            this.lexeme.Length = 0;
            this.lexeme.Append(lex);
            this.errorMsg = msg;
            this.token = Token.Error;

            return this.token;
        }

        private bool IsNumberDecimalSeparator(char c)
        {
            return c.ToString() == cultureInfo.NumberFormat.NumberDecimalSeparator;
        }

        private Token SetToken(Token token)
        {
            this.token = token;
            return token;
        }

        private Token SetToken(Token token, string theLexeme)
        {
            this.token = token;
            this.lexeme.Length = 0;
            this.lexeme.Append(theLexeme);

            return token;
        }

        private bool Match(char c)
        {
            int ch = reader.Peek();
            return ch != -1 && (char)ch == c;
        }


        private bool Match(char[] chars)
        {
            bool result = false;

            int ch = reader.Peek();
            if (ch != -1)
            {
                foreach (char c in chars)
                {
                    if ((char)ch == c)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private delegate bool CheckDelegate(char c);

        private bool Match(CheckDelegate check)
        {
            bool result = false;

            int ch = reader.Peek();
            if (ch != -1)
            {
                result = check((char)ch);
            }

            return result;
        }



        private void Accept()
        {
            int ch = reader.Read();
            if (ch != -1)
            {
                lexeme.Append((char)ch);
                if ((char)ch == '\n')
                {
                    currentLine++;
                    currentColumn = 1;
                }
                else
                {
                    currentColumn++;
                }
            }
        }

    }
}
