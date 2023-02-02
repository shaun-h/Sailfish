﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sailfish.Execution;

internal class TestInstanceContainerProvider
{
    public readonly MethodInfo Method;
    private readonly ITypeResolutionUtility typeResolutionUtility;
    private readonly Type test;
    private readonly IEnumerable<PropertySet> propertySets;

    public TestInstanceContainerProvider(
        ITypeResolutionUtility typeResolutionUtility,
        Type test,
        IEnumerable<PropertySet> propertySets,
        MethodInfo method)
    {
        Method = method;

        this.typeResolutionUtility = typeResolutionUtility;
        this.test = test;
        this.propertySets = propertySets;
    }

    public int GetNumberOfPropertySetsInTheQueue()
    {
        return propertySets.Count();
    }

    public async IAsyncEnumerable<TestInstanceContainer> ProvideNextTestInstanceContainer(Type[] additionalAnchorTypes)
    {
        if (GetNumberOfPropertySetsInTheQueue() is 0)
        {
            var instance = await typeResolutionUtility.CreateDehydratedTestInstance(test, additionalAnchorTypes);
            yield return TestInstanceContainer.CreateTestInstance(instance, Method, Array.Empty<string>(), Array.Empty<int>());
        }
        else
        {
            foreach (var nextPropertySet in propertySets)
            {
                var instance = await typeResolutionUtility.CreateDehydratedTestInstance(test, additionalAnchorTypes);

                HydrateInstance(instance, nextPropertySet);

                var propertyNames = nextPropertySet.GetPropertyNames().ToArray();
                var variableValues = nextPropertySet.GetPropertyValues().ToArray();
                yield return TestInstanceContainer.CreateTestInstance(instance, Method, propertyNames, variableValues);
            }
        }
    }

    private static void HydrateInstance(object obj, PropertySet propertySet)
    {
        foreach (var variable in propertySet.VariableSet)
        {
            var prop = obj.GetType().GetProperties().Single(x => x.Name == variable.Name);
            prop.SetValue(obj, variable.Value);
        }
    }
}