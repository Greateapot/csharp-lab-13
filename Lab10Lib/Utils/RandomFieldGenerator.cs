namespace Lab10Lib.Utils
{
    public static class RandomFieldGenerator
    {
        private static readonly Random random = new((int)DateTimeOffset.Now.ToUnixTimeMilliseconds());

        public static string FirstName() => RandomFieldData.FirstNames[random.Next() % RandomFieldData.FirstNames.Length];
        public static string LastName() => RandomFieldData.LastNames[random.Next() % RandomFieldData.LastNames.Length];
        public static byte Age(int min = byte.MinValue, int max = byte.MaxValue) => Convert.ToByte(random.Next() % (max - min) + min);
        public static float Rating(float min = float.MinValue, float max = float.MaxValue) => random.NextSingle() % (max - min) + min;
        public static uint SchoolID() => Convert.ToUInt32(random.Next() + 1);
        public static uint UniversityID() => Convert.ToUInt32(random.Next() + 1);
        public static int RandomID() => random.Next();
    }
}