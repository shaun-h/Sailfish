﻿using Sailfish.Analysis;
using Sailfish.Attributes;
using Shouldly;
using Tests.E2ETestSuite.Utils;

namespace Tests.E2ETestSuite.Discoverable;

[Sailfish]
public class ResolveTestCaseIdTest
{
    private readonly TestCaseId testCaseId;

    public ResolveTestCaseIdTest(TestCaseId testCaseId)
    {
        this.testCaseId = testCaseId;
    }
    [SailfishMethod]
    public void MainMethod()
    {
        testCaseId.DisplayName.ShouldBe($"{nameof(ResolveTestCaseIdTest)}.{nameof(MainMethod)}()");
    }
}

[Sailfish]
public class ResolveTestCaseIdTestMultipleCtorArgs
{
    private readonly TestCaseId testCaseId;

    public ResolveTestCaseIdTestMultipleCtorArgs(ExampleDependencyForAltRego dep, TestCaseId testCaseId)
    {
        this.testCaseId = testCaseId;
    }
    [SailfishMethod]
    public void MainMethod()
    {
        testCaseId.DisplayName.ShouldBe($"{nameof(ResolveTestCaseIdTestMultipleCtorArgs)}.{nameof(MainMethod)}()");
    }
}
