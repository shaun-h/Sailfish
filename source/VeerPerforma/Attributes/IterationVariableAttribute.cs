namespace VeerPerforma.Attributes;

/// <summary>
/// This is used to decorate a property that will be referenced within the test.
/// A unique execution set of the performance tests is executed for each value provided,
/// where an execution set is the total number of executions specified by the
/// VeerPerforma attribute
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class IterationVariableAttribute : Attribute
{
    public IterationVariableAttribute(params int[] n)
    {
        if (n.Length == 0) throw new InvalidOperationException("No values were provided to the IteratationVariable attribute.");
        N = n;
    }

    public int[] N { get; set; }
}