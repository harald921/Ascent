using System.Collections;
using System.Collections.Generic;

public static class Constants
{
    public const int BOOL_SIZE_IN_BITS = 8;

    public static class Directory
    {
        public static readonly string BASE;
        public static readonly string DATA;
        public static readonly string SPECIES;

        static Directory()
        {
            BASE       = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            DATA       = BASE + @"Data";
            SPECIES    = BASE + @"Data\Characters";
        }
    }

    public static class Networking
    {
        public const int PORT = 12345;
    }
}