using System;

namespace ImageEditor
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Enter the file name of the image to edit (or 'exit' to quit):");
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                else
                {
                    Editor.Edit(input);
                }
            }
        }
    }
}
