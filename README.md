# Analyzer

This analyzer can be used as a console app or can be embedded into your code to find GDI leaks in C# code by means of static analysis. This project uses Roslyn APIs.

To use the console app, use GdiResourceLeakAnalyzer/CodeAnalyzer/CodeAnalyzer/. Pass a directory path or a file path enclosed into commas to a command line.
A core library containing all logic is contained in GdiResourceLeakAnalyzer/CodeAnalyzer/CodeAnalyzers/.
