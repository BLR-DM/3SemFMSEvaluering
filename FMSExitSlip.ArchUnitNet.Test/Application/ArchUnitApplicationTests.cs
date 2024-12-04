using ArchUnitNET.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchUnitNET.xUnit;

namespace FMSExitSlip.ArchUnitNet.Test.Application
{
    public class ArchUnitApplicationTests : ArchUnitBaseTest
    {
        [Fact]
        public void Application_Layer_Should_not_Have_Dependency_On_Infrastructure()
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(ApplicationLayer)
                .Should()
                .NotDependOnAny(InfrastructureLayer)
                .Check(Architecture);
        }

        [Fact]
        public void Application_Layer_Should_not_Have_Dependency_On_Presentation()
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(ApplicationLayer)
                .Should()
                .NotDependOnAny(PresentationLayer)
                .Check(Architecture);
        }
    }
}
