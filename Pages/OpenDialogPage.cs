using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using PaintTest.Core;
using PaintTest.Core.Exceptions;

namespace PaintTest.Pages
{
    public class OpenDialogPage : BaseFileDialogPage
    {
        public OpenDialogPage(ApplicationManager appManager) : base(appManager)
        {
        }

        protected override string DialogTitle => "Open";
        protected override string ActionButtonName => "Open";
        protected override string ActionButtonAutomationId => "1";

        public void ClickOpenButton() => ClickActionButton();

        public void OpenFile(string fileName, string? folderPath = null)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                NavigateToFolder(folderPath);
            }
            EnterFileName(fileName);
            ClickOpenButton();
        }
    }
}