// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using EmailValidator;

Console.WriteLine("Hello, World!");

var _ = BenchmarkRunner.Run<ValidatorBench>();