using ConsoleIOLib;
using Lab10Lib.Exceptions;
using Lab10Lib.Utils;

namespace Lab10Lib.Entities
{
    public class Person : IEquatable<Person>, IComparable<Person>, ICloneable
    {
        public const byte MinAge = 1;
        public const byte MaxAge = 120;

        private string? firstName;
        private string? lastName;
        private byte age;

        public Person(string firstName, string lastName, byte age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Person() { }

        public string FirstName
        {
            get => firstName ?? "--";
            set => firstName = value.Length == 0 ? throw new InvalidFieldValueException() : value;
        }
        public string LastName
        {
            get => lastName ?? "--";
            set => lastName = value.Length == 0 ? throw new InvalidFieldValueException() : value;
        }
        public byte Age
        {
            get => age;
            set => age = value < MinAge || value > MaxAge ? throw new InvalidFieldValueException() : value;
        }

        public override string ToString() => $"Person#{GetHashCode()}(lastName: \"{LastName}\", firstName: \"{FirstName}\", age: {Age})";

        public override int GetHashCode() => (FirstName, LastName, Age).GetHashCode();

        public override bool Equals(object? obj) => obj is Person other && Equals(other);

        public bool Equals(Person? other) => other is not null && CompareTo(other) == 0;

        public int CompareTo(Person? other)
        {
            if (other is null) return 1;
            if (ReferenceEquals(other, this)) return 0;

            int c;

            c = FirstName.CompareTo(other.FirstName);
            if (c != 0) return c;

            c = LastName.CompareTo(other.LastName);
            if (c != 0) return c;

            c = Age.CompareTo(other.Age);
            if (c != 0) return c;

            return 0;
        }

        public object Clone() => new Person(FirstName, LastName, Age);

        public static bool operator ==(Person left, Person right) => left is null ? right is null : left.Equals(right);

        public static bool operator !=(Person left, Person right) => !(left == right);

        public static bool operator <(Person left, Person right) => left is null ? right is not null : left.CompareTo(right) < 0;

        public static bool operator <=(Person left, Person right) => left is null || left.CompareTo(right) <= 0;

        public static bool operator >(Person left, Person right) => left is not null && left.CompareTo(right) > 0;

        public static bool operator >=(Person left, Person right) => left is null ? right is null : left.CompareTo(right) >= 0;

        public static Person Init() => new()
        {
            FirstName = ConsoleIO.InputRaw("Введите имя: "),
            LastName = ConsoleIO.InputRaw("Введите фамилию: "),
            Age = ConsoleIO.Input<byte>(
                $"Введите возраст ({MinAge} <= значение <= {MaxAge}): ",
                v => v >= MinAge && v <= MaxAge
                    ? null
                    : "Недопустимое значение!"
            ),
        };

        public static Person RandomInit() => new() {
            FirstName = RandomFieldGenerator.FirstName(),
            LastName = RandomFieldGenerator.LastName(),
            Age = RandomFieldGenerator.Age(MinAge, MaxAge),
        };
    }
}