using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FMSEvaluering.ArchUnitNet.Test.Domain
{
    public class ArchUnitDomainTests : ArchUnitBaseTest
    {
        [Fact]
        public void Domain_Layer_Should_not_Have_Dependency_On_Application()
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(DomainLayer)
                .Should()
                .NotDependOnAny(ApplicationLayer)
                .Check(Architecture);
        }

        [Fact]
        public void Domain_Layer_Should_not_Have_Dependency_On_Infrastructure()
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(DomainLayer)
                .Should()
                .NotDependOnAny(InfrastructureLayer)
                .Check(Architecture);
        }

        [Fact]
        public void Domain_Layer_Should_not_Have_Dependency_On_Presentation()
        {
            ArchRuleDefinition
                .Types()
                .That()
                .Are(DomainLayer)
                .Should()
                .NotDependOnAny(PresentationLayer)
                .Check(Architecture);
        }
    }
}
