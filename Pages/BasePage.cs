using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using PaintTest.Core;
using PaintTest.Core.Exceptions;

namespace PaintTest.Pages
{
    public abstract class BasePage
    {
        protected readonly ApplicationManager AppManager;

        protected BasePage(ApplicationManager appManager)
        {
            AppManager = appManager ?? throw new ArgumentNullException(nameof(appManager));
        }

        public Window MainWindow => AppManager.MainWindow;
        protected AutomationBase Automation => AppManager.Automation;

        public virtual bool IsPageLoaded()
        {
            return MainWindow.IsAvailable;
        }

        public void WaitForPageLoad(int timeoutMs = 5000)
        {
            var startTime = DateTime.Now;
            while (!IsPageLoaded() && (DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                Thread.Sleep(100);
            }

            if (!IsPageLoaded())
            {
                throw new TimeoutException($"Page {GetType().Name} did not load within {timeoutMs}ms");
            }
        }



        public void ClickButtonWithName(string buttonName)
        {
            var button = MainWindow.FindFirstDescendant(cf =>
                cf.ByName(buttonName).And(cf.ByControlType(ControlType.Button)));

            if (button != null)
            {
                button.Click();
                Thread.Sleep(300);
            }
            else
            {
                throw new ElementNotFoundException($"Button '{buttonName}' not found", buttonName);
            }
        }


    }
}