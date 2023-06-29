# emailValidator

Simple validator for well-formed email according to RFC standards. No memory allocation.

## Benchmark test result

``` console
BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.4.1 (22F82) [Darwin 22.5.0]
Intel Core i5-7360U CPU 2.30GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=7.0.304
  [Host]     : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2


| Method |     Mean |     Error |    StdDev | Allocated |
|------- |---------:|----------:|----------:|----------:|
| Modern | 2.889 us | 0.0306 us | 0.0256 us |         - |
```