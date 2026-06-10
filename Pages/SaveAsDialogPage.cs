using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using PaintTest.Core;
using PaintTest.Core.Exceptions;

namespace PaintTest.Pages
{
    public class SaveAsDialogPage : BaseFileDialogPage
    {
        public SaveAsDialogPage(ApplicationManager appManager) : base(appManager)
        {
        }

        protected override string DialogTitle => "Save As";
        protected override string ActionButtonName => "Save";
        protected override string ActionButtonAutomationId => "1";

        public void ClickSaveButton() => ClickActionButton();

        public void SelectFileType(string fileType)
        {
            var fileTypeComboBox = Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("1001").And(cf.ByControlType(ControlType.ComboBox)))?.AsComboBox()
                ?? Dialog.FindFirstDescendant(cf =>
                cf.ByName("Save as type:").And(cf.ByControlType(ControlType.ComboBox)))?.AsComboBox();

            if (fileTypeComboBox != null)
            {
                fileTypeComboBox.Select(fileType);
                Thread.Sleep(200);
            }
            else
            {
                throw new ElementNotFoundException("File type combobox not found", "FileTypeComboBox");
            }
        }

        public string GetSelectedFileType()
        {
            var fileTypeComboBox = Dialog.FindFirstDescendant(cf =>
                cf.ByAutomationId("1001").And(cf.ByControlType(ControlType.ComboBox)))?.AsComboBox();

            return fileTypeComboBox?.SelectedItem?.Name ?? string.Empty;
        }

        public void SaveFile(string fileName, string? folderPath = null)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                NavigateToFolder(folderPath);
            }
            EnterFileName(fileName);
            ClickSaveButton();
        }

        public void SaveFileWithType(string fileName, string fileType, string? folderPath = null)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                NavigateToFolder(folderPath);
            }
            // Select the type first; in some OS/file dialogs selecting type can reset the filename field
            SelectFileType(fileType);
            Thread.Sleep(150);
            EnterFileName(fileName);
            ClickSaveButton();
        }

        public bool IsConfirmationDialogVisible()
        {
            var confirmDialog = Dialog.ModalWindows.FirstOrDefault();
            return confirmDialog != null && confirmDialog.IsAvailable;
        }

        public void ConfirmOverwrite()
        {
            var confirmDialog = Dialog.ModalWindows.FirstOrDefault();
            var yesButton = confirmDialog?.FindFirstDescendant(cf =>
                cf.ByName("Yes").And(cf.ByControlType(ControlType.Button)))?.AsButton();

            yesButton?.Click();
            Thread.Sleep(300);
        }

        public void CancelOverwrite()
        {
            var confirmDialog = Dialog.ModalWindows.FirstOrDefault();
            var noButton = confirmDialog?.FindFirstDescendant(cf =>
                cf.ByName("No").And(cf.ByControlType(ControlType.Button)))?.AsButton();

            noButton?.Click();
            Thread.Sleep(300);
        }
    }
}