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
            
                persons[index] = Person.Init();
            return persons;
        }
    }
}