using System;
using System.Diagnostics;
using System.Net.Http;

namespace PerfIt
{
    public interface ICounterHandler : IDisposable
    {
        string CounterType { get; }
        void OnRequestStarting(HttpRequestMessage request);
        void OnRequestEnding(HttpResponseMessage response);
        string Name { get; }
        CounterCreationData[] BuildCreationData();
    }
}
