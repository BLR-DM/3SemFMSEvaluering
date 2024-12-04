using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace FMSEvaluering.ArchUnitNet.Test.Infrastructure
{
    public class ArchUnitInfrastructureTests : ArchUnitBaseTest
    {
        [Fact]
        public void Infrastructure_Layer_Should_not_Have_Dependency_On_Presentation()
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(InfrastructureLayer)
                .Should()
                .NotDependOnAny(PresentationLayer)
                .Check(Architecture);
        }
    }
}
