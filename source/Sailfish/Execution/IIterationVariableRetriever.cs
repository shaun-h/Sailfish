﻿using System;
using System.Collections.Generic;

namespace Sailfish.Execution;

internal interface IIterationVariableRetriever
{
    Dictionary<string, VariableAttributeMeta> RetrieveIterationVariables(Type type);
}