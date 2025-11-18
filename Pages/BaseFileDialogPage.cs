using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using PaintTest.Core;
using PaintTest.Core.Exceptions;

namespace PaintTest.Pages
{
    public abstract class BaseFileDialogPage : BasePage
    {
        protected BaseFileDialogPage(ApplicationManager appManager) : base(appManager)
        {
        }

        protected abstract string DialogTitle { get; }
        protected abstract string ActionButtonName { get; }
        protected abstract string ActionButtonAutomationId { get; }

        protected Window Dialog => MainWindow.ModalWindows.FirstOrDefault() 
            ?? MainWindow.FindFirstChild(cf => cf.ByName(DialogTitle).Or(cf.ByClassName("#32770")))?.AsWindow() 
            ?? MainWindow;

        public override bool IsPageLoaded()
        {
            return Dialog.IsAvailable && Dialog.Title.Contains(DialogTitle, StringComparison.OrdinalIgnoreCase);
        }

        public void EnterFileName(string fileName)
        {
            var textBox = Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("1148").And(cf.ByControlType(ControlType.Edit)))?.AsTextBox()
                ?? Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("1001").And(cf.ByControlType(ControlType.Edit)))?.AsTextBox();
            
            if (textBox != null)
            {
                // Wait for textbox to be enabled with retry logic
                int maxRetries = 10;
                int retryCount = 0;
                bool success = false;

                while (retryCount < maxRetries && !success)
                {
                    try
                    {
                        // Check if textbox is enabled
                        if (textBox.IsEnabled && !textBox.IsReadOnly)
                        {
                            textBox.Text = fileName;
                            success = true;
                            Thread.Sleep(200);
                        }
                        else
                        {
                            Thread.Sleep(200); // Wait before retry
                            retryCount++;
                        }
                    }
                    catch (FlaUI.Core.Exceptions.ElementNotEnabledException)
                    {
                        // Textbox not ready yet, wait and retry
                        Thread.Sleep(200);
                        retryCount++;
                    }
                }

                if (!success)
                {
                    throw new InvalidOperationException(
                        $"File name textbox was not enabled after {maxRetries} retries. IsEnabled: {textBox.IsEnabled}, IsReadOnly: {textBox.IsReadOnly}");
                }
            }
            else
            {
                throw new ElementNotFoundException("File name textbox not found", fileName);
            }
        }

        public string GetFileName()
        {
            var textBox = Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("1148").And(cf.ByControlType(ControlType.Edit)))?.AsTextBox()
                ?? Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("1001").And(cf.ByControlType(ControlType.Edit)))?.AsTextBox();

            return textBox?.Text ?? string.Empty;
        }

        public void SelectFileFromList(string fileName)
        {
            var fileList = Dialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.List))?.AsListBox();
            var file = fileList?.Items.FirstOrDefault(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));
            
            if (file != null)
            {
                file.Select();
                Thread.Sleep(200);
            }
            else
            {
                throw new ElementNotFoundException($"File '{fileName}' not found", fileName);
            }
        }

        public bool IsFileInList(string fileName)
        {
            var fileList = Dialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.List))?.AsListBox();
            return fileList?.Items.Any(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        public void NavigateToFolder(string folderPath)
        {
            var addressBar = Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("1001").And(cf.ByControlType(ControlType.ToolBar)))?.AsTextBox();
            
            if (addressBar != null)
            {
                addressBar.Text = folderPath;
                Keyboard.Press(VirtualKeyShort.ENTER);
                Thread.Sleep(500);
            }
        }

        public void ClickActionButton()
        {
            var actionButton = Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId(ActionButtonAutomationId).And(cf.ByControlType(ControlType.Button)))?.AsButton()
                ?? Dialog.FindFirstDescendant(cf => cf.ByName(ActionButtonName).And(cf.ByControlType(ControlType.Button)))?.AsButton();
            

            if (actionButton != null)
            {
                actionButton.Click();
                Thread.Sleep(500);
            }
            else
            {
                throw new ElementNotFoundException($"{ActionButtonName} button not found", $"{ActionButtonName}Button");
            }
        }

        public void ClickCancelButton()
        {
            var cancelButton = Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("2").And(cf.ByControlType(ControlType.Button)))?.AsButton()
                ?? Dialog.FindFirstDescendant(cf => cf.ByName("Cancel").And(cf.ByControlType(ControlType.Button)))?.AsButton();
            
            if (cancelButton != null)
            {
                cancelButton.Click();
                Thread.Sleep(300);
            }
            else
            {
                throw new ElementNotFoundException("Cancel button not found", "CancelButton");
            }
        }

        public void SelectFolderFromTree(string folderName)
        {
            var treeView = Dialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree))?.AsTree();
            var folderNode = treeView?.FindFirstDescendant(cf =>
                cf.ByName(folderName).And(cf.ByControlType(ControlType.TreeItem)))?.AsTreeItem();

            if (folderNode != null)
            {
                folderNode.Select();
                Thread.Sleep(300);
            }
            else
            {
                throw new ElementNotFoundException($"Folder '{folderName}' not found in tree", folderName);
            }
        }
    }
}