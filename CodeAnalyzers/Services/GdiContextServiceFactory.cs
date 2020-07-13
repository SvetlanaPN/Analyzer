using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CodeAnalyzer.Services {
    public class GdiContextServiceFactory : IServiceFactory<IContextService> {
        public IContextService CreateService(string type) {
            switch(type) {
                case nameof(Font):
                    return new FontContextService();
                case nameof(Brush):
                case nameof(SolidBrush):
                case nameof(TextureBrush):
                case nameof(HatchBrush):
                case nameof(LinearGradientBrush):
                case nameof(PathGradientBrush):
                    return new BrushContextService();
                case nameof(Pen):
                    return new PenContextService();
                case nameof(StringFormat):
                    return new StringFormatContextService();
                default:
                    return null;
            }
        }
    }
}