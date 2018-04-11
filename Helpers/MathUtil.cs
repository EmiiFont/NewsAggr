using System;

namespace AngularAggr.Helpers
{
    public static class MathUtil
    {
        public static float Max(float a, float b, float c)
        {
            return Math.Max(Math.Max(a, b), c);
        }

        public static int Max(int a, int b, int c)
        {
            return Math.Max(Math.Max(a, b), c);
        }

        public static float Max(float a, float b, float c, float d)
        {
            return Math.Max(
                    Math.Max(Math.Max(a, b), c), d);
        }


        public static int Max(int a, int b, int c, int d)
        {
            return Math.Max(
                    Math.Max(Math.Max(a, b), c), d);
        }

        public static float Min(float a, float b, float c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        public static int Min(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        public static float Min(float a, float b, float c,
                float d)
        {
            return Math.Min(
                    Math.Min(Math.Min(a, b), c), d);
        }

        public static int Min(int a, int b, int c, int d)
        {
            return Math.Min(
                    Math.Min(Math.Min(a, b), c), d);
        }
    }
}