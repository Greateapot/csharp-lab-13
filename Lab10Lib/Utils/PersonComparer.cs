using Lab10Lib.Entities;

namespace Lab10Lib.Utils
{
    public class PersonComparerIn : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x is null || y is null)
                throw new ArgumentException("Некорректное значение параметра");

            int c;

            c = x.LastName.CompareTo(y.LastName);
            if (c != 0) return c;

            c = x.FirstName.CompareTo(y.FirstName);
            if (c != 0) return c;

            c = x.Age.CompareTo(y.Age);
            if (c != 0) return c;

            return 0;
        }
    }

    public class PersonComparerOut : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x is null || y is null)
                throw new ArgumentException("Некорректное значение параметра");

            int c;

            c = y.LastName.CompareTo(x.LastName);
            if (c != 0) return c;

            c = y.FirstName.CompareTo(x.FirstName);
            if (c != 0) return c;

            c = y.Age.CompareTo(x.Age);
            if (c != 0) return c;

            return 0;
        }
    }
}