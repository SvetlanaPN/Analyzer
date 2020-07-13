using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeAnalyzer.Analyzers {
    public class GdiMemoryLeakCollector : MemoryLeakCollector {
        public GdiMemoryLeakCollector() : base(new[] {typeof(Font), typeof(Pen),
                typeof(StringFormat), typeof(Brush), typeof(SolidBrush), typeof(TextureBrush), 
            typeof(HatchBrush), typeof(LinearGradientBrush), typeof(PathGradientBrush) }) { }
    }
}