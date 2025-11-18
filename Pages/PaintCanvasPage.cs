using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;
using System.Drawing;
using System.Drawing.Imaging;

namespace PaintTest.Pages
{
    public class PaintCanvasPage
    {
        private readonly Window _paintWindow;
        
        public PaintCanvasPage(Window paintWindow)
        {
            _paintWindow = paintWindow;
        }

        public AutomationElement GetCanvasElement()
        {
            return _paintWindow.FindFirstDescendant(cf => 
                cf.ByClassName("NamedContainerAutomationPeer")
                  .And(cf.ByAutomationId("image")))
                ?? throw new InvalidOperationException("Canvas element not found");
        }

        public Bitmap CaptureCanvas()
        {
            var canvasElement = GetCanvasElement();
            var capture = Capture.Element(canvasElement);
            return capture.Bitmap;
        }
    }
}