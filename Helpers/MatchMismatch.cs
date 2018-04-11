using System;

namespace AngularAggr.Helpers
{
    public class MatchMismatch
    {
        private float matchValue;
        private float mismatchValue;

        /**
	     * Constructs a new match-mismatch substitution function. When two
	     * characters are equal a score of <code>matchValue</code> is assigned. In
	     * case of a mismatch a score of <code>mismatchValue</code>. The
	     * <code>matchValue</code> must be strictly greater then
	     * <code>mismatchValue</code>
	     * 
	     * @param matchValue
	     *            value when characters are equal
	     * @param mismatchValue
	     *            value when characters are not equal
	     */
        public MatchMismatch(float matchValue, float mismatchValue)
        {
            this.matchValue = matchValue;
            this.mismatchValue = mismatchValue;
        }

        public float Compare(String a, int aIndex, String b, int bIndex)
        {
            return a[aIndex] == b[bIndex] ? matchValue
                    : mismatchValue;
        }

        public float Max()
        {
            return matchValue;
        }

        public float Min()
        {
            return mismatchValue;
        }

        public override string ToString()
        {
            return "MatchMismatch [matchCost=" + matchValue + ", mismatchCost="
                    + mismatchValue + "]";
        }

    }
}