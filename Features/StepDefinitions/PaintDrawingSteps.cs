using NUnit.Framework;
using PaintTest.Core;
using PaintTest.Pages;
using Reqnroll;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;
using System.Drawing;
using System.IO;

namespace PaintTest.Features.StepDefinitions
{
    [Binding]
    public class PaintDrawingSteps
    {
        private readonly PaintContext _context;
        private readonly ScenarioContext _scenarioContext;
        private readonly BaseLineManager _baseLineManager;
        private readonly ImageComparisonService _imageComparisonService;

        private static readonly string BaseLineFolder = GetBaseLineFolder();
        private const string ActualFolder = "ActualResults";

        public PaintDrawingSteps(PaintContext context, ScenarioContext scenarioContext)
        {
            _context = context;
            _scenarioContext = scenarioContext;
            _baseLineManager = new BaseLineManager(BaseLineFolder, ActualFolder);
            _imageComparisonService = new ImageComparisonService();
        }

        private static string GetBaseLineFolder()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var parent = Directory.GetParent(baseDirectory);

            if (parent?.Parent?.Parent?.Parent != null)
            {
                return Path.Combine(parent.Parent.Parent.Parent.FullName, "Baselines");
            }

            return Path.Combine(baseDirectory, "Baselines");
        }

        [Then(@"The canvas should contain (.*) with drawn content")]
        public void ThenTheCanvasShouldContainDrawnContent(string screenshotName)
        {
            if (_context.PaintWindow == null)
            {
                Assert.Fail("Paint window is not available in context");
                return;
            }

            var canvasPage = new PaintCanvasPage(_context.PaintWindow);
            var screenshot = canvasPage.CaptureCanvas();

            _baseLineManager.SaveActual(screenshot, screenshotName);

            var baseline = _baseLineManager.LoadBaseline(screenshotName);

            if (baseline != null)
            {
                using (baseline)
                {
                    var result = _imageComparisonService.CompareImages(baseline, screenshot, thresholdPercentage: 2.0);
                    Assert.That(result.AreSimilar, Is.True,
                        $"Canvas differs from baseline. Difference: {result.DifferencePercentage:F2}%");
                }
            }
            else
            {
                _baseLineManager.CreateBaseline(screenshot, screenshotName);
                Assert.Pass("Baseline created. Please review and re-run tests.");
            }
        }
             
    }

    public class PaintContext
    {
        public Window? PaintWindow { get; set; }
    }
}