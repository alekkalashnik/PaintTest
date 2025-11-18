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
            if (baseline.Size != actual.Size)
                return (false, 100.0);

            int differentPixels = 0;
            int totalPixels = baseline.Width * baseline.Height;

            for (int y = 0; y < baseline.Height; y++)
            {
                for (int x = 0; x < baseline.Width; x++)
                {
                    if (!ColorsAreSimilar(baseline.GetPixel(x, y), actual.GetPixel(x, y)))
                        differentPixels++;
                }
            }

            double differencePercentage = (differentPixels * 100.0) / totalPixels;
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