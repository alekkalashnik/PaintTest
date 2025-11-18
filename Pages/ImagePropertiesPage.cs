using FlaUI.Core.AutomationElements;
using PaintTest.Core;
using PaintTest.Core.Exceptions;

namespace PaintTest.Pages
{
    public class ImagePropertiesPage : BasePage
    {

        public ImagePropertiesPage(ApplicationManager appManager) : base(appManager)
        {
        }

        public string WindowTitle => MainWindow.Title;

        public bool IsApplicationOpen()
        {
            return MainWindow.IsAvailable && !string.IsNullOrEmpty(MainWindow.Title);
        }
    }
}
