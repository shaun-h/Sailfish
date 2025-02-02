using Sailfish.Analysis.SailDiff.Statistics.StatsCore.Distributions;
using System;

namespace Sailfish.Analysis.SailDiff.Statistics.StatsCore.Analysers;

public class TwoSampleTTestPowerAnalysis : BaseTwoSamplePowerAnalysis
{
    public TwoSampleTTestPowerAnalysis(TwoSampleHypothesis hypothesis)
        : base((DistributionTailSailfish)hypothesis)
    {
    }

    public TwoSampleTTestPowerAnalysis(TwoSampleT test)
        : base(test.Tail)
    {
        Power = test.Analysis.Power;
        Size = test.Analysis.Size;
        Effect = test.Analysis.Effect;
        Samples1 = test.Analysis.Samples1;
        Samples2 = test.Analysis.Samples2;
    }

    public override void ComputePower()
    {
        var noncentrality = Effect / Math.Sqrt(1.0 / Samples1 + 1.0 / Samples2);
        var degreesOfFreedom = Samples1 + Samples2 - 2.0;
        var tdistribution = new TDistribution(degreesOfFreedom);
        var noncentralTdistribution = new NonCentralTDistribution(degreesOfFreedom, noncentrality);
        switch (Tail)
        {
            case DistributionTailSailfish.TwoTail:
                var x1 = tdistribution.InverseDistributionFunction(1.0 - Size / 2.0);
                Power = noncentralTdistribution.ComplementaryDistributionFunction(x1) + noncentralTdistribution.DistributionFunction(-x1);
                break;

            case DistributionTailSailfish.OneUpper:
                var x2 = tdistribution.InverseDistributionFunction(1.0 - Size);
                Power = noncentralTdistribution.ComplementaryDistributionFunction(x2);
                break;

            case DistributionTailSailfish.OneLower:
                var x3 = tdistribution.InverseDistributionFunction(Size);
                Power = noncentralTdistribution.DistributionFunction(x3);
                break;

            default:
                throw new InvalidOperationException();
        }
    }
}