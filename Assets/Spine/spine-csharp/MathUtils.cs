using System;

namespace Spine
{
    public static class MathUtils
    {
        public const float PI = 3.1415927f;
        public const float PI2 = PI * 2;
        public const float RadDeg = 180f / PI;
        public const float DegRad = PI / 180;

        const int SIN_BITS = 14; // 16KB. Adjust for accuracy.
        const int SIN_MASK = ~(-1 << SIN_BITS);
        const int SIN_COUNT = SIN_MASK + 1;
        const float RadFull = PI * 2;
        const float DegFull = 360;
        const float RadToIndex = SIN_COUNT / RadFull;
        const float DegToIndex = SIN_COUNT / DegFull;
        static float[] sin = new float[SIN_COUNT];

        static MathUtils()
        {
            for (int i = 0; i < SIN_COUNT; i++)
                sin[i] = (float)Math.Sin((i + 0.5f) / SIN_COUNT * RadFull);
            for (int i = 0; i < 360; i += 90)
                sin[(int)(i * DegToIndex) & SIN_MASK] = (float)Math.Sin(i * DegRad);
        }

        /// <summary>Returns the sine in radians from a lookup table.</summary>
        static public float Sin(float radians)
        {
            return sin[(int)(radians * RadToIndex) & SIN_MASK];
        }

        /// <summary>Returns the cosine in radians from a lookup table.</summary>
        static public float Cos(float radians)
        {
            return sin[(int)((radians + PI / 2) * RadToIndex) & SIN_MASK];
        }

        /// <summary>Returns the sine in radians from a lookup table.</summary>
        static public float SinDeg(float degrees)
        {
            return sin[(int)(degrees * DegToIndex) & SIN_MASK];
        }

        /// <summary>Returns the cosine in radians from a lookup table.</summary>
        static public float CosDeg(float degrees)
        {
            return sin[(int)((degrees + 90) * DegToIndex) & SIN_MASK];
        }

        /// <summary>Returns atan2 in radians, faster but less accurate than Math.Atan2. Average error of 0.00231 radians (0.1323
        /// degrees), largest error of 0.00488 radians (0.2796 degrees).</summary>
        static public float Atan2(float y, float x)
        {
            if (x == 0f)
            {
                if (y > 0f) return PI / 2;
                if (y == 0f) return 0f;
                return -PI / 2;
            }
            float atan, z = y / x;
            if (Math.Abs(z) < 1f)
            {
                atan = z / (1f + 0.28f * z * z);
                if (x < 0f) return atan + (y < 0f ? -PI : PI);
                return atan;
            }
            atan = PI / 2 - z / (z * z + 0.28f);
            return y < 0f ? atan - PI : atan;
        }

        static public float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
