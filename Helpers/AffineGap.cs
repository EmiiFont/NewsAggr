using System;


namespace AngularAggr.Helpers
{
    public class AffineGap
    {
        private float startValue;
        private float gapValue;

        /**
         * Constructs a constant gap function that assigns a penalty of
         * <code>startValue + gapValue * gapLenght </code> to a gap. .
         * 
         * @param startValue
         *            a non-positive initial penalty for creating a gap
         * @param gapValue
         *            a non-positive constant gap value
         */
        public AffineGap(float startValue, float gapValue)
        {
            //checkArgument(startValue <= 0.0f);
            //checkArgument(gapValue <= 0.0f);

            this.startValue = startValue;
            this.gapValue = gapValue;
        }

        public float value(int fromIndex, int toIndex)
        {
            // checkArgument(fromIndex < toIndex, "fromIndex must be before toIndex");
            return startValue + gapValue * (toIndex - fromIndex - 1);
        }

        public float max()
        {
            return startValue;
        }

        public float min()
        {
            return float.MinValue;
        }

        public String toString()
        {
            return "AffineGap [startValue=" + startValue + ", gapValue=" + gapValue
                    + "]";
        }

    }
}