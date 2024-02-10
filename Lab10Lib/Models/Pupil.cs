using ConsoleIOLib;
using Lab10Lib.Exceptions;
using Lab10Lib.Utils;

namespace Lab10Lib.Models
{
    public class Pupil : Person, IEquatable<Pupil>, IComparable<Pupil>, ICloneable
    {
        public const float MinRating = 0;
        public const float MaxRating = 5;

        private float rating;
        private uint schoolID;

        public Pupil(string firstName, string lastName, byte age, float rating, uint schoolID) : base(firstName, lastName, age)
        {
            Rating = rating;
            SchoolID = schoolID;
        }

        public Pupil() { }

        public float Rating
        {
            get => rating;
            set => rating = value < MinRating || value > MaxRating ? throw new InvalidFieldValueException() : value;
        }
        public uint SchoolID
        {
            get => schoolID;
            set => schoolID = value == 0 ? throw new InvalidFieldValueException() : value;
        }

        public override string ToString() => $"Pupil#{GetHashCode()}(lastName: \"{LastName}\", firstName: \"{FirstName}\", age: {Age}, rating: {Rating}, schoolID: {SchoolID})";

        public override int GetHashCode() => (FirstName, LastName, Age, Rating, SchoolID).GetHashCode();

        public override bool Equals(object? obj) => obj is Pupil other && Equals(other);

        public bool Equals(Pupil? other) => other is not null && CompareTo(other) == 0;

        public int CompareTo(Pupil? other)
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

            c = Rating.CompareTo(other.Rating);
            if (c != 0) return c;

            c = SchoolID.CompareTo(other.SchoolID);
            if (c != 0) return c;

            return 0;
        }

        public new object Clone() => new Pupil(FirstName, LastName, Age, Rating, SchoolID);

        public static bool operator ==(Pupil left, Pupil right) => left is null ? right is null : left.Equals(right);

        public static bool operator !=(Pupil left, Pupil right) => !(left == right);

        public static bool operator <(Pupil left, Pupil right) => left.CompareTo(right) < 0;

        public static bool operator >(Pupil left, Pupil right) => left.CompareTo(right) > 0;

        public static bool operator <=(Pupil left, Pupil right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Pupil left, Pupil right) => left.CompareTo(right) >= 0;

        public static new Pupil Init() => new()
        {
            FirstName = ConsoleIO.InputRaw("Введите имя: "),
            LastName = ConsoleIO.InputRaw("Введите фамилию: "),
            Age = ConsoleIO.Input<byte>(
                $"Введите возраст ({MinAge} <= значение <= {MaxAge}): ",
                v => v >= MinAge && v <= MaxAge
                    ? null
                    : "Недопустимое значение!"
            ),
            Rating = ConsoleIO.Input<float>(
                $"Введите рейтинг ({MinRating} <= значение <= {MaxRating}): ",
                v => v >= MinRating && v <= MaxRating
                    ? null
                    : "Недопустимое значение!"
            ),
            SchoolID = ConsoleIO.Input<uint>(
                "Введите идентификатор школы (число > 0): ",
                v => v > 0
                    ? null
                    : "Недопустимое значение!"
            ),
        };

        public static new Pupil RandomInit() => new()
        {
            FirstName = RandomFieldGenerator.FirstName(),
            LastName = RandomFieldGenerator.LastName(),
            Age = RandomFieldGenerator.Age(MinAge, MaxAge),
            Rating = RandomFieldGenerator.Rating(MinRating, MaxRating),
            SchoolID = RandomFieldGenerator.SchoolID(),
        };
    }
}