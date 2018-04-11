using System;
using AngularAggr.Helpers;

public class SmithWaterman
    {
        private AffineGap gap;
        private MatchMismatch substitution;
        private int windowSize;

        /**
         * Constructs a new Smith Waterman metric. Uses an affine gap of
         * <code>-5.0 - gapLength</code> a <code>-3.0</code> substitution penalty
         * for mismatches, <code>5.0</code> for matches.
         * 
         */
        public SmithWaterman()
        {
            gap = new AffineGap(-5.0f, -1.0f);
            substitution = new MatchMismatch(5.0f, -3.0f);
            windowSize = Int32.MaxValue;
        }

        /**
         * Constructs a new Smith Waterman metric.
         * 
         * @param gap
         *            a gap function to score gaps by
         * @param substitution
         *            a substitution function to score substitutions by
         * @param windowSize
         *            a non-negative window in which
         */
        public SmithWaterman(AffineGap gap, MatchMismatch substitution, int windowSize)
        {
            //checkNotNull(gap);
            //checkNotNull(substitution);
            //checkArgument(windowSize >= 0);
            this.gap = gap;
            this.substitution = substitution;
            this.windowSize = windowSize;
        }

        public float compare(String a, String b)
        {

            if (String.IsNullOrEmpty(a) && String.IsNullOrEmpty(b))
            {
                return 1.0f;
            }

            if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b))
            {
                return 0.0f;
            }
            float maxDistance = Math.Min(a.Length, b.Length)
                    * Math.Max(substitution.Max(), gap.min());
            return smithWaterman(a, b) / maxDistance;
        }

        private float smithWaterman(String a, String b)
        {
            int n = a.Length;
            int m = b.Length;

            float[][] d = new float[n][];

            // Initialize corner
            for (int i = 0; i < n; i++)
            {
                d[i] = new float[m];
            }
            float max = d[0][0] = Math.Max(0, substitution.Compare(a, 0, b, 0));

            //float[][] d = new float[n][];

            // Initialize corner
            // float max = d[0] = new float Math.Max(0, substitution.Compare(a, 0, b, 0));

            // Initialize edge
            for (int i = 0; i < n; i++)
            {

                // Find most optimal deletion
                float maxGapCost = 0;
                for (int k = Math.Max(1, i - windowSize); k < i; k++)
                {
                    maxGapCost = Math.Max(maxGapCost, d[i - k][0] + gap.value(i - k, i));
                }

                d[i][0] = MathUtil.Max(0, maxGapCost, substitution.Compare(a, i, b, 0));

                max = Math.Max(max, d[i][0]);

            }

            // Initialize edge
            for (int j = 1; j < m; j++)
            {

                // Find most optimal insertion
                float maxGapCost = 0;
                for (int k = Math.Max(1, j - windowSize); k < j; k++)
                {
                    maxGapCost = Math.Max(maxGapCost, d[0][j - k] + gap.value(j - k, j));
                }

                d[0][j] = MathUtil.Max(0, maxGapCost, substitution.Compare(a, 0, b, j));

                max = Math.Max(max, d[0][j]);

            }

            // Build matrix
            for (int i = 1; i < n; i++)
            {

                for (int j = 1; j < m; j++)
                {

                    float maxGapCost = 0;
                    // Find most optimal deletion
                    for (int k = Math.Max(1, i - windowSize); k < i; k++)
                    {
                        maxGapCost = Math.Max(maxGapCost,
                                d[i - k][j] + gap.value(i - k, i));
                    }
                    // Find most optimal insertion
                    for (int k = Math.Max(1, j - windowSize); k < j; k++)
                    {
                        maxGapCost = Math.Max(maxGapCost,
                                d[i][j - k] + gap.value(j - k, j));
                    }

                    // Find most optimal of insertion, deletion and substitution
                    d[i][j] = MathUtil.Max(0, maxGapCost,
                            d[i - 1][j - 1] + substitution.Compare(a, i, b, j));

                    max = Math.Max(max, d[i][j]);
                }

            }

            return max;
        }

        public String toString()
        {
            return "SmithWaterman [gap=" + gap + ", substitution=" + substitution
                    + ", windowSize=" + windowSize + "]";
        }

    }