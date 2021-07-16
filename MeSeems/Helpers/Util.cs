using System;
using System.Collections.Generic;
using System.Text;

namespace MeSeems.Helpers
{
    public static class Util
    {
        public static void Write(string data, char? delimiter)
        {
            var delimiterTemp = string.IsNullOrEmpty(delimiter.ToString()) ? "" : new string(delimiter.Value,80);
            if (string.IsNullOrEmpty(delimiterTemp))
            {
                Console.SetCursorPosition((Console.WindowWidth - data.Length) / 2, Console.CursorTop);
                Console.WriteLine(data);
            }
            else
            {
                Console.SetCursorPosition((Console.WindowWidth - delimiterTemp.Length) / 2, Console.CursorTop);
                Console.WriteLine(delimiterTemp);
                Write(data, null);
            }
        }
    }
}
