using ConsoleIOLib;
using Lab10Lib.Entities;

namespace Lab10Lib.Utils
{
    public class PersonGenerator
    {
        private static readonly Random random = new((int)DateTimeOffset.Now.ToUnixTimeMilliseconds());

        public static Person[] GetRandomPerson(int count)
        {
            Person[] persons = new Person[count];
            for (int index = 0; index < count; index++)
                persons[index] = (random.Next() % 4) switch
                {
                    0 => Person.RandomInit(),
                    1 => Pupil.RandomInit(),
                    2 => Student.RandomInit(),
                    _ => PartTimeStudent.RandomInit(),
                };
            return persons;
        }

        public static Person[] GetPerson(int count)
        {
            Person[] persons = new Person[count];
            for (int index = 0; index < count; index++)

                persons[index] = InputPersonType() switch
                {
                    1 => Person.Init(),
                    2 => Pupil.Init(),
                    3 => Student.Init(),
                    _ => PartTimeStudent.Init(),
                };
            return persons;
        }

        private static int InputPersonType() => ConsoleIO.Input<int>(
            "Person types:\n1. Person\n2. Pupil\n3. Student\n4. PartTimeStudent\nChoice Person type: ",
            v => v < 1 || v > 4 ? "Invalid value." : null
        );
    }
}