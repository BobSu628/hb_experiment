public class Interpreter {
    string program;
    int pos;
    Token token;

    void Init(string text) {
        program = text;
        pos = 0;
        token = null;
    }

    public Interpreter() {
        Init("");
    }

    Token GetNextToken() {
        if (pos > program.Length - 1) {
            return new Token() {type=TokenType.EOF, lexeme=null};
        }
        char ch = program[pos];

        if (char.IsDigit(ch)) {
            pos ++;
            return new Token() {type=TokenType.Integer, lexeme=ch.ToString()};
        } else if (ch == '+') {
            pos ++;
            return new Token() {type=TokenType.Plus, lexeme=ch.ToString()};
        }

        ThrowParseError();
        return null;
    }

    void Consume(TokenType type) {
        if (token.type == type) {
            token = GetNextToken();
        } else {
            ThrowParseError();
        }
    }

    public string Run(string text) {
        Init(text);

        try {
            token = GetNextToken();
            Token left = token;
            Consume(TokenType.Integer);
            Token op = token;
            Consume(TokenType.Plus);
            Token right = token;
            Consume(TokenType.Integer);

            int result = int.Parse(left.lexeme) + int.Parse(right.lexeme);
            return result.ToString();
        } catch (System.Exception e) {
            return e.Message;
        }
    }

    public void ThrowParseError() {
        throw new System.Exception("Error parsing input");
    }
}