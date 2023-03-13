using AspNetCore.VersionInfo.Benchmarks;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<Md5VsSha256>();
