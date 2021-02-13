using System;
using System.IO;
using System.Reflection;

namespace Reports
{
    public static class CommonMenu
    {
        public static void Show()
        {
            using (StreamReader streamReader = new StreamReader("../../../MenuTexts/CommonMenuText.txt"))
            {
                Console.WriteLine(streamReader.ReadToEnd());
            }
        }
    }
}