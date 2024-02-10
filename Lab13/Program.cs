using ConsoleIOLib;
using Lab10Lib.Utils;

namespace Lab13
{
    public class Program
    {
        public static void Main()
        {
            PersonTree tree = new("tree1");
            Journal journal = new();

            tree.CountChanged += journal.CountChanged;

            var persons = RandomPersonGenerator.GetPerson(10);
            tree.AddAll(persons);
            ConsoleIO.WriteLine(tree);
            tree.RemoveAll(persons[2], persons[5], persons[8]);
            ConsoleIO.WriteLine(tree);

            foreach (var entry in journal)
                ConsoleIO.WriteLine(entry);

        }

    }
}