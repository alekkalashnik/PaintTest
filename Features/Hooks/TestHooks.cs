using Reqnroll.BoDi;
using PaintTest.Core;
using Reqnroll;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

namespace PaintTest.Features.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports? _extent;
        private ExtentTest? _scenario;
        private ExtentTest? _step;

        public TestHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extent = ExtentReportManager.GetExtent();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportManager.FlushReport();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var appManager = new ApplicationManager();
            appManager.StartApplication();
            
            _objectContainer.RegisterInstanceAs(appManager);
            
            // Create ExtentTest for scenario
            var featureName = _scenarioContext.ScenarioInfo.Title;
            _scenario = _extent!.CreateTest<Feature>(featureName);
            _objectContainer.RegisterInstanceAs(_scenario);
            
            Console.WriteLine($"Scenario started: {_scenarioContext.ScenarioInfo.Title}");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var appManager = _objectContainer.Resolve<ApplicationManager>();
            
            // Capture screenshot on failure
            if (_scenarioContext.TestError != null)
            {
                try
                {
                    var screenshotPath = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title);
                    _scenario?.Fail(_scenarioContext.TestError.Message, 
                        MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                catch (Exception ex)
                {
                    _scenario?.Fail($"Test failed: {_scenarioContext.TestError.Message}. Screenshot capture failed: {ex.Message}");
                }
            }
            else
            {
                _scenario?.Pass("Scenario passed successfully");
            }
            
            appManager?.Dispose();
            
            Console.WriteLine($"Scenario finished: {_scenarioContext.ScenarioInfo.Title}");
        }

        [BeforeStep]
        public void BeforeStep()                
        {
            var stepInfo = _scenarioContext.StepContext?.StepInfo;
            if (stepInfo != null)
            {
                // Create step node based on keyword
                _step = stepInfo.StepDefinitionType.ToString() switch
                {
                    "Given" => _scenario?.CreateNode<Given>(stepInfo.Text),
                    "When" => _scenario?.CreateNode<When>(stepInfo.Text),
                    "Then" => _scenario?.CreateNode<Then>(stepInfo.Text),
                    "And" => _scenario?.CreateNode<And>(stepInfo.Text),
                    _ => _scenario?.CreateNode(stepInfo.Text)
                };
                
                Console.WriteLine($"Starting step: {stepInfo.Text}");
            }
        }

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TestError != null)
            {
                _step?.Fail(_scenarioContext.TestError.Message);
                Console.WriteLine($"Step failed: {_scenarioContext.TestError.Message}");
            }
            else
            {
                _step?.Pass("Step passed");
            }
        }

        private string CaptureScreenshot(string scenarioName)
        {
            var fileName = $"{scenarioName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var screenshotDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "Screenshots");
            Directory.CreateDirectory(screenshotDir);
            var screenshotPath = Path.Combine(screenshotDir, fileName);
            
            // Capture screenshot using System.Drawing
            var bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            using var bitmap = new System.Drawing.Bitmap(bounds.Width, bounds.Height);
            using var graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, bounds.Size);
            bitmap.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
            
            return screenshotPath;
        }
    }
}