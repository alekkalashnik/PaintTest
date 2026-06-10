using System.Drawing;

namespace PaintTest.Core
{
    public class ImageComparisonService
    {
        public (bool AreSimilar, double DifferencePercentage) CompareImages(
            Bitmap baseline, 
            Bitmap actual, 
            double thresholdPercentage = 0.1)
        {
            // If sizes differ, compare the overlapping region and penalize missing/excess pixels
            int compareWidth = Math.Min(baseline.Width, actual.Width);
            int compareHeight = Math.Min(baseline.Height, actual.Height);

            if (compareWidth == 0 || compareHeight == 0)
                return (false, 100.0);

            int differentPixels = 0;
            int overlapPixels = compareWidth * compareHeight;

            for (int y = 0; y < compareHeight; y++)
            {
                for (int x = 0; x < compareWidth; x++)
                {
                    if (!ColorsAreSimilar(baseline.GetPixel(x, y), actual.GetPixel(x, y)))
                        differentPixels++;
                }
            }

            // Any baseline pixels outside the overlapping area are considered different
            int baselineTotal = baseline.Width * baseline.Height;
            int extraBaselinePixels = baselineTotal - overlapPixels;
            if (extraBaselinePixels < 0) extraBaselinePixels = 0;

            double totalDifferent = differentPixels + extraBaselinePixels;
            double differencePercentage = (totalDifferent * 100.0) / baselineTotal;

            return (differencePercentage < thresholdPercentage, differencePercentage);
        }

        private bool ColorsAreSimilar(Color c1, Color c2, int threshold = 10)
        {
            return Math.Abs(c1.R - c2.R) <= threshold &&
                   Math.Abs(c1.G - c2.G) <= threshold &&
                   Math.Abs(c1.B - c2.B) <= threshold;
        }
    }
}