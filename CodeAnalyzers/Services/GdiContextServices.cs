namespace CodeAnalyzer.Services {
    public class FontContextService : IContextService {
        readonly string[] problematicMethods = {
            "e.Graphics.DrawString",
            "e.Graphics.MeasureCharacterRanges",
            "e.Graphics.MeasureString",
            "e.Cache.Graphics.DrawString",
            "e.Cache.Graphics.MeasureCharacterRanges",
            "e.Cache.Graphics.MeasureString",
            "e.Cache.CalcDefaultTextSize",
            "e.Cache.CalcTextSize",
            "e.Cache.CalcVTextSize",
            "e.Cache.DrawString",
            "e.Cache.DrawVString"
        };
        readonly string[] problematicProperties = {
            "e.Appearance.Font",
            "e.ViewInfo.Appearance.Font"
        };
        public string[] GetProblematicMethods() {
            return problematicMethods;
        }
        public string[] GetProblematicProperties() {
            return problematicProperties;
        }
    }

    public class BrushContextService : IContextService {
        readonly string[] problematicMethods = {
          "e.Graphics.DrawString",
          "e.Graphics.FillClosedCurve",
          "e.Graphics.FillEllipse",
          "e.Graphics.FillPath",
          "e.Graphics.FillPie",
          "e.Graphics.FillPolygon",
          "e.Graphics.FillRectangle",
          "e.Graphics.FillRectangles",
          "e.Graphics.FillRegion",
          "e.Cache.Graphics.DrawString",
          "e.Cache.Graphics.FillClosedCurve",
          "e.Cache.Graphics.FillEllipse",
          "e.Cache.Graphics.FillPath",
          "e.Cache.Graphics.FillPie",
          "e.Cache.Graphics.FillPolygon",
          "e.Cache.Graphics.FillRectangle",
          "e.Cache.Graphics.FillRectangles",
          "e.Cache.Graphics.FillRegion",
          "e.Cache.DrawString",
          "e.Cache.DrawVString",
          "e.Cache.FillEllipse",
          "e.Cache.FillPath",
          "e.Cache.FillPie",
          "e.Cache.FillPolygon",
          "e.Cache.FillRectangle"
        };
        public string[] GetProblematicMethods() {
            return problematicMethods;
        }
        public string[] GetProblematicProperties() {
            return null;
        }
    }

    public class PenContextService : IContextService {
        readonly string[] problematicMethods = {
        "e.Graphics.DrawArc",
        "e.Graphics.DrawBezier",
        "e.Graphics.DrawBeziers",
        "e.Graphics.DrawClosedCurve",
        "e.Graphics.DrawCurve",
        "e.Graphics.DrawEllipse",
        "e.Graphics.DrawLine",
        "e.Graphics.DrawLines",
        "e.Graphics.DrawPath",
        "e.Graphics.DrawPie",
        "e.Graphics.DrawPolygon",
        "e.Graphics.DrawRectangle",
        "e.Graphics.DrawRectangles",
        "e.Cache.Graphics.DrawArc",
        "e.Cache.Graphics.DrawBezier",
        "e.Cache.Graphics.DrawBeziers",
        "e.Cache.Graphics.DrawClosedCurve",
        "e.Cache.Graphics.DrawCurve",
        "e.Cache.Graphics.DrawEllipse",
        "e.Cache.Graphics.DrawLine",
        "e.Cache.Graphics.DrawLines",
        "e.Cache.Graphics.DrawPath",
        "e.Cache.Graphics.DrawPie",
        "e.Cache.Graphics.DrawPolygon",
        "e.Cache.Graphics.DrawRectangle",
        "e.Cache.Graphics.DrawRectangles",
        "e.Cache.DrawArc",
        "e.Cache.DrawBezier",
        "e.Cache.DrawBeziers",
        "e.Cache.DrawEllipse",
        "e.Cache.DrawLine",
        "e.Cache.DrawPath",
        "e.Cache.DrawPie",
        "e.Cache.DrawRectangle"
        };
        public string[] GetProblematicMethods() {
            return problematicMethods;
        }
        public string[] GetProblematicProperties() {
            return null;
        }
    }
    public class StringFormatContextService : IContextService {
        readonly string[] problematicMethods = {
        "e.Graphics.DrawString",
        "e.Graphics.MeasureCharacterRanges",
        "e.Graphics.MeasureString",
        "e.Cache.Graphics.DrawString",
        "e.Cache.Graphics.MeasureCharacterRanges",
        "e.Cache.Graphics.MeasureString",
        "e.Cache.CalcTextSize",
        "e.Cache.CalcVTextSize",
        "e.Cache.DrawString",
        "e.Cache.DrawVString"
        };
        public string[] GetProblematicMethods() {
            return problematicMethods;
        }
        public string[] GetProblematicProperties() {
            return null;
        }
    }
}