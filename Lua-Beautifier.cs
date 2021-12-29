using System;
using System.Collections.Generic;

//This a very basic beautifier i made for my project
//This beautifier does not do any indenting or code checking and doesn't work well on already beautified files
//if you want to use this just create and instance and call beautify with your code

public class Beautifier
{
    private class NewLineHelper
    {
        private int Openning = 0;
        private List<char> NonNewLineSymbols = new List<char>()
        {
            '+',
            '-',
            '/',
            '*',
            '<',
            '>',
            '=',
            '%',
            '^',
            ','
        };

        private bool IsNeg(int num)
        {
            if (num + num < num)
                return true;

            return false;
        }

        public int Open() => Openning++;
        public int Close() => Openning--;

        public bool ShouldNewLine(string linedata, int currentPos)
        {
            bool IsClosing = Openning == 0;

            bool HasMathBehind = ((linedata.Length == currentPos + 1) ? false : NonNewLineSymbols.Contains(linedata.Substring(currentPos + 1, 1)[0]));
            bool HasNewLine = linedata.Substring(linedata.Length - 2) == ")\n";

            return IsClosing && !HasMathBehind && !HasNewLine;
        }
        public bool CheckString(string line, int currentPos, string word, int offset = 0)
        {
            if (IsNeg(currentPos + offset))
                return false;

            bool HasSpace = ((currentPos + offset + word.Length) <= line.Length);

            string tmp = "";
            Console.WriteLine($"HasSpace: {HasSpace}, ?{(HasSpace ? (tmp = line.Substring(currentPos + offset, word.Length)) : "No space")}?, {tmp == word}");

            if (HasSpace && line.Substring(currentPos + offset, word.Length) == word)
                return true;

            return false;
        }
    }

    private string FixWhiteSpace(string line)
    {
        if (line.StartsWith(" "))
            return line.Substring(1);

        return line;
    }

    private string CreateNewLines(string code)
    {
        string output = "";

        code = code.Replace("\n", "");

        code = code.Replace("end", "end\n");
        code = code.Replace("then", "then\n");
        code = code.Replace("do", "do\n");
        code = code.Replace("else", "else\n");
        code = code.Replace("\n;", "\n");
        code = code.Replace(";", "\n");
        code = code.Replace(",", ", ");

        string[] lines = code.Split("\n".ToCharArray());

        NewLineHelper helper = new NewLineHelper();

        for(int i = 0; i < lines.Length; i++)
        {
            string line = FixWhiteSpace(lines[i] + "\n");

            int positionsToSkip = 0;

            for(int pos = 0; pos < line.Length; pos++)
            {
                if (positionsToSkip > 0)
                {
                    positionsToSkip--;
                    continue;
                }

                string newData = line[pos].ToString();

                if (line[pos] == '(')
                {
                    helper.Open();
                }

                if (line[pos] == ')')
                {
                    helper.Close();

                    if (helper.ShouldNewLine(line, pos))
                    {
                        newData += "\n";
                    }
                }

                if (line[pos] == 'e' && helper.CheckString(line, pos, " end", -1))
                    newData = "\n" + newData;

                if (line[pos] == 'l' && helper.CheckString(line, pos, "local") && pos > 0)
                    newData = "\n" + newData;


                output += newData;
            }
        }

        return output;
    }

    public Beautifier() { }

    public string Beautify(string code)
    {
        string output = CreateNewLines(code);

        return output;
    }
}
