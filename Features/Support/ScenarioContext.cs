using PaintTest.Core;
using PaintTest.Pages;

namespace PaintTest.Features.Support
{
    public class TestContext
    {
        public ApplicationManager? AppManager { get; set; }
        public PaintMainPage? PaintMainPage { get; set; }
        public string? CapturedWindowTitle { get; set; }
        public bool? CapturedCanvasState { get; set; }
    }
}