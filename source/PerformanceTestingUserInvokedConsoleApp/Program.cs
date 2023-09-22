﻿using PerformanceTestingUserInvokedConsoleApp;
using PerformanceTests;
using Sailfish;

var settings = RunSettingsBuilder.CreateBuilder()
    .TestsFromAssembliesContaining(typeof(PerformanceTestProjectDiscoveryAnchor))
    .ProvidersFromAssembliesContaining(typeof(AppRegistrationProvider))
    .WithSailDiff()
    .WithScalefish()
    .WithLocalOutputDirectory("my_custom_directory")
    .Build();
var result = await SailfishRunner.Run(settings);
var not = result.IsValid ? string.Empty : "not ";
Console.WriteLine($"Test run was {not}valid");