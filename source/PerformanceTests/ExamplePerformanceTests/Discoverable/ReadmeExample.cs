﻿using System;
using System.Collections.Concurrent;
using Sailfish.Attributes;
using System.Threading;
using System.Threading.Tasks;

namespace PerformanceTests.ExamplePerformanceTests.Discoverable
{
    [Sailfish(Disabled = false)]
    public class ReadmeExample
    {
        private ConcurrentDictionary<string, string> scenarioMap = null!;

        [SailfishVariable("wow", "ok")]
        public string N { get; set; } = null!;

        private const string ScenarioA = "ScenarioA";
        private const string ScenarioB = "ScenarioB";

        [SailfishVariable(ScenarioA, ScenarioB)]
        public string Scenario { get; set; } = null!;

        [SailfishGlobalSetup]
        public void GlobalSetup()
        {
            scenarioMap = new ConcurrentDictionary<string, string>();
            scenarioMap.AddOrUpdate(ScenarioA, (val) => "WOW", (old, newish) => "OK");
            scenarioMap.AddOrUpdate(ScenarioB, (val) => "ok", (old, newish) => "wow");
        }

        [SailfishMethod]
        public async Task TestMethod(CancellationToken cancellationToken) // token is injected when requested
        {
            Console.WriteLine(scenarioMap[Scenario]);
            await Task.Delay(100, cancellationToken);
        }
    }
}