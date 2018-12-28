using System;
using Random = UnityEngine.Random;

namespace Icarus.Engine.Utilities
{
    public static class RandomUtilities
    {
        public static DateTime RandomDateTime()
        {
            return new DateTime(Random.Range(1900, 2100), Random.Range(1, 12), Random.Range(1, 30), Random.Range(0, 23),
                Random.Range(0, 59), Random.Range(0, 59));
        }
        
        public static DateTime RandomDateTime(int minYear)
        {
            return new DateTime(Random.Range(minYear, 2100), Random.Range(1, 12), Random.Range(1, 30), Random.Range(0, 23),
                Random.Range(0, 59), Random.Range(0, 59));
        }
        
        public static DateTime RandomDateTime(int minYear, int maxYear)
        {
            return new DateTime(Random.Range(minYear, maxYear), Random.Range(1, 12), Random.Range(1, 30), Random.Range(0, 23),
                Random.Range(0, 59), Random.Range(0, 59));
        }
    }
}