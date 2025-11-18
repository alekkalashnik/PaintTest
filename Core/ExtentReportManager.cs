using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace PaintTest.Core
{
    public class ExtentReportManager
    {
        private static ExtentReports? _extent;
        private static ExtentSparkReporter? _sparkReporter;
        
        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.html");
                Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);
                
                _sparkReporter = new ExtentSparkReporter(reportPath);
                _sparkReporter.Config.DocumentTitle = "Paint UI Automation Test Report";
                _sparkReporter.Config.ReportName = "Paint Test Execution Report";
                _sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
                
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
                _extent.AddSystemInfo("Application", "Microsoft Paint");
                _extent.AddSystemInfo("Environment", "Windows");
                _extent.AddSystemInfo("Framework", ".NET 8.0");
                _extent.AddSystemInfo("Test Framework", "NUnit + Reqnroll");
            }
            return _extent;
        }
        
        public static void FlushReport()
        {
            _extent?.Flush();
        }
    }
}