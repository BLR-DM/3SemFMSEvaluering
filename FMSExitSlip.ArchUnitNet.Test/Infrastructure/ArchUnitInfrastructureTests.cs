using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace FMSExitSlip.ArchUnitNet.Test.Infrastructure
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
