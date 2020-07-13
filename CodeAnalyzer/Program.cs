using CodeAnalyzer.Analyzers;
using System;
using System.Diagnostics;
using System.IO;

namespace CodeAnalyzer {
    class Program {
        static void Main(string[] args) {
            if(args.Length == 1) {
                try {
                    string path = args[0];
                    if(!ProcessDirectory(path))
                        ProcessFile(path);
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.GetType().FullName);
                    Console.WriteLine(ex.Message);
                    var st = new StackTrace();
                    Console.WriteLine(st.ToString());
                }
            }
            Console.WriteLine("Completed");
            Console.ReadKey();
        }
        static bool ProcessDirectory(string path) {
            if(Directory.Exists(path)) {
                var files = Directory.EnumerateFiles(path, "*.cs", SearchOption.AllDirectories);
                foreach(string filePath in files)
                    Analyze(filePath);
                return true;
            }
            return false;
        }
        static void Analyze(string filePath) {
            var analyzer = new Analyzer(new[] { new GdiMemoryLeakCollector() });
            analyzer.Analyze(GetCode(filePath));
            foreach(var error in analyzer.Errors)
                Console.WriteLine($"{filePath} {error}");
        }
        static string GetCode(string filePath) {
            using(var stream = File.OpenRead(filePath)) {
                using(var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
        static bool ProcessFile(string path) {
            if(File.Exists(path)) {
                Analyze(path);
                return true;
            }
            return false;
        }
    }
}
