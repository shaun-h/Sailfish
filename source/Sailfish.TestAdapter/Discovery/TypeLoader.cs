﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Sailfish.Attributes;

namespace Sailfish.TestAdapter.Discovery;

internal static class TypeLoader
{
    public static Type[] LoadSailfishTestTypesFrom(string sourceDll, IMessageLogger logger)
    {
        var assembly = LoadAssemblyFromDll(sourceDll);
        var types = CollectSailfishTestTypesFromAssembly(assembly, logger);
        return types;
    }

    private static Type[] CollectSailfishTestTypesFromAssembly(Assembly assembly, IMessageLogger logger)
    {
        var perfTestTypes = assembly
            .GetTypes()
            .Where(x => x.HasAttribute<SailfishAttribute>())
            .ToArray();

        logger.SendMessage(TestMessageLevel.Informational, $"\rTest Types Discovered in {assembly.FullName ?? "Couldn't Find the assembly name property"}:\r");
        foreach (var testType in perfTestTypes) logger.SendMessage(TestMessageLevel.Informational, $"--- Perf tests: {testType.Name}");

        return perfTestTypes;
    }

    private static Assembly LoadAssemblyFromDll(string dllPath)
    {
        var assembly = Assembly.LoadFile(dllPath);
        AppDomain.CurrentDomain.Load(assembly.GetName()); // is this necessary?
        return assembly;
    }
}