using CodeAnalyzer.Analyzers;
using CodeAnalyzer.Error;
using NUnit.Framework;

namespace Tests {
    public class Tests {
        string CheckFontLeak_Test1 = @"
        void CheckFontLeak_Test1() {
            Font f = new Font(""Tahoma"", 12);
        }";
        string CheckFontLeak_Test2 = @"
        void CheckFontLeak_Test2() {
            System.Drawing.Font f = new System.Drawing.Font(""Tahoma"", 12);
        }";
        string CheckFontLeak_Test3 = @"
        void CheckFontLeak_Test3() {
            Font f;
            f = new Font(""Tahoma"", 12);
        }";
        string CheckFontLeak_Test4 = @"
        void CheckFontLeak_Test4() {
            Font f = new Font(""Tahoma"", 12);
            string text = ""Test"";
            f.Dispose();
        }";
        string CheckFontLeak_Test5 = @"
        void CheckFontLeak_Test5() {
            Font f;
            f = new Font(""Tahoma"", 12);
            string text = ""Test"";
            f.Dispose();
        }";
        string CheckFontLeak_Test6 = @"
        void CheckFontLeak_Test6() {
            using(Font f = new Font(""Tahoma"", 12)) {
                string text = ""Test"";
            }
            string text2 = ""Test2"";
        }";
        string CheckFontLeak_Test7 = @"
        void CheckFontLeak_Test7() {
            Font f;
            using(f = new Font(""Tahoma"", 12)) {
                string text = ""Test"";
            }
            string text2 = ""Test2"";
        }";
        string CheckFontLeak_Test8 = @"
        void CheckFontLeak_Test8() {
            Font f = new Font(""Tahoma"", 12);
            e.Appearance.Font = f;
        }";
        string CheckFontLeak_Test9 = @"
        void CheckFontLeak_Test9() {
            Font f;
            f = new Font(""Tahoma"", 12);
            e.Appearance.Font = f;
        }";
        string CheckFontLeak_Test10 = @"
        void CheckFontLeak_Test10() {
            e.Appearance.Font = new Font(""Tahoma"", 12); ;
        }";
        string CheckFontLeak_Test11 = @"
        void CheckFontLeak_Test11() {
            Font f = new Font(""Tahoma"", 12);
            Font = f;
        }";
        string CheckFontLeak_Test12 = @"
        void CheckFontLeak_Test12() {
            Font f;
            f = new Font(""Tahoma"", 12);
            Font = f;
        }";
        string CheckFontLeak_Test13 = @"
        void CheckFontLeak_Test13() {
           Font = new Font(""Tahoma"", 12); 
        }";
        string CheckFontLeak_Test14 = @"
        void CheckFontLeak_Test14(PaintEventArgs e) {
            e.Graphics.DrawString(""Text"", new Font(""Tahoma"", 12), Brushes.Red, Point.Empty);
        }";
        string CheckFontLeak_Test15 = @"
        void CheckFontLeak_Test15(PaintEventArgs e) {
            Font f = new Font(""Tahoma"", 12);
            e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
        }";
        string CheckFontLeak_Test16 = @"
        void CheckFontLeak_Test16(PaintEventArgs e) {
            Font f;
            f = new Font(""Tahoma"", 12);
            e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
        }";
        string CheckFontLeak_Test17 = @"
        void CheckFontLeak_Test17(PaintEventArgs e) {
            Font f = new Font(""Tahoma"", 12);
            e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
            f.Dispose();
        }";
        string CheckFontLeak_Test18 = @"
        void CheckFontLeak_Test18(PaintEventArgs e) {
            Font f;
            f = new Font(""Tahoma"", 12);
            e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
            f.Dispose();
        }";
        string CheckFontLeak_Test19 = @"
        void CheckFontLeak_Test19(PaintEventArgs e) {
            using(Font f = new Font(""Tahoma"", 12)) 
                e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
        }";
        string CheckFontLeak_Test20 = @"
        void CheckFontLeak_Test20(PaintEventArgs e) {
            Font f;
            using(f = new Font(""Tahoma"", 12))
                e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
        }";
        string CheckFontLeak_Test21 = @"
        Font CheckFontLeak_Test21(PaintEventArgs e) {
            Font f = new Font(""Tahoma"", 12))
            e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
            return f;
        }";
        string CheckFontLeak_Test22 = @"
        Font CheckFontLeak_Test22(PaintEventArgs e) {
            Font f;
            f= new Font(""Tahoma"", 12))
            e.Graphics.DrawString(""Text"", f, Brushes.Red, Point.Empty);
            return f;
        }";
        string CheckFontLeak_Test23 = @"
        Font CheckFontLeak_Test23() {
            Font f = new Font(""Tahoma"", 12))
            return f;
        }";
        string CheckFontLeak_Test24 = @"
        Font CheckFontLeak_Test24() {
            Font f;
            f= new Font(""Tahoma"", 12))
            return f;
        }";
        string CheckFontLeak_Test25 = @"
        Font CheckFontLeak_Test25() {
            return new Font(""Tahoma"", 12))
        }";
        string CheckFontLeak_Test26 = @"
        void CheckFontLeak_Test26() {
            Font f = new Font(""Tahoma"", 12))
            FontInfo info = new FontInfo(f);
        }";
        string CheckFontLeak_Test27 = @"
        void CheckFontLeak_Test27() {
            Font f;
            f= new Font(""Tahoma"", 12))
            FontInfo info = new FontInfo(f);
        }";
        string CheckFontLeak_Test28 = @"
        void CheckFontLeak_Test28() {
            Font f = new Font(""Tahoma"", 12))
            Process(new FontInfo(f));
        }";
        string CheckFontLeak_Test29 = @"
        void CheckFontLeak_Test29() {
            Font f;
            f = new Font(""Tahoma"", 12))
            Process(new FontInfo(f));
        }";
        string CheckFontLeak_Test30 = @"
        void CheckFontLeak_Test30() {
            Process(new FontInfo(new Font(""Tahoma"", 12)));
        }";
        string CheckFontLeak_Test31 = @"
        void CheckFontLeak_Test31() {
            Font f = new Font(""Tahoma"", 12))
            testObject.Info = new FontInfo(f);
        }";
        string CheckFontLeak_Test32 = @"
        void CheckFontLeak_Test32() {
            Font f;
            f= new Font(""Tahoma"", 12))
            testObject.Info = new FontInfo(f);
        }";
        string CheckFontLeak_Test33 = @"
        void CheckFontLeak_Test33() {
           testObject.Info = new FontInfo(new Font(""Tahoma"", 12));
        }";
        public string[] InvalidCodeSnippets {
            get {
                return new[]{ CheckFontLeak_Test1, CheckFontLeak_Test2, CheckFontLeak_Test3, CheckFontLeak_Test8,
                    CheckFontLeak_Test9, CheckFontLeak_Test10, CheckFontLeak_Test14, CheckFontLeak_Test15, CheckFontLeak_Test16};
            }
        }
        public string[] ValidCodeSnippets {
            get {
                return new[]{ CheckFontLeak_Test4,
                    CheckFontLeak_Test5, CheckFontLeak_Test6, CheckFontLeak_Test7, CheckFontLeak_Test11, CheckFontLeak_Test12, CheckFontLeak_Test13,
                    CheckFontLeak_Test17, CheckFontLeak_Test18, CheckFontLeak_Test19, CheckFontLeak_Test20,
                    CheckFontLeak_Test21, CheckFontLeak_Test22, CheckFontLeak_Test23, CheckFontLeak_Test24, CheckFontLeak_Test25,
                    CheckFontLeak_Test26, CheckFontLeak_Test27, CheckFontLeak_Test28, CheckFontLeak_Test29, CheckFontLeak_Test30,
                    CheckFontLeak_Test31, CheckFontLeak_Test32, CheckFontLeak_Test33};
            }
        }
        [Test]
        public void CheckFontLeak() {
            var analyzer = new Analyzer(new[] { new GdiMemoryLeakCollector() });
            foreach(var code in InvalidCodeSnippets)
                analyzer.Analyze(code);
            Assert.AreEqual(InvalidCodeSnippets.Length, analyzer.Errors.Count);
            foreach(var error in analyzer.Errors) {
                Assert.AreEqual(ErrorType.Leak, error.ErrorType);
                Assert.AreEqual("Font", error.VariableType);
            }
            analyzer.ClearErrors();
            foreach(var code in ValidCodeSnippets)
                analyzer.Analyze(code);
            Assert.AreEqual(0, analyzer.Errors.Count);
        }
    }
}