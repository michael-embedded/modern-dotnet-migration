// Modern .NET, Chapter 6, Section 6.1
using System.Diagnostics;

ThreadPool.SetMinThreads(8, 8);
ThreadPool.SetMaxThreads(8, 8);

const int requests = 40;

var sw = Stopwatch.StartNew();
var blocking = Enumerable.Range(0, requests)
    .Select(_ => Task.Run(() => Thread.Sleep(200)))   // holds a pool thread
    .ToArray();
Task.WaitAll(blocking);
Console.WriteLine($"Blocking: {sw.ElapsedMilliseconds} ms");

sw.Restart();
var asyncTasks = Enumerable.Range(0, requests)
    .Select(_ => Task.Run(async () => await Task.Delay(200)))  // releases the thread
    .ToArray();
await Task.WhenAll(asyncTasks);
Console.WriteLine($"Async   : {sw.ElapsedMilliseconds} ms");
