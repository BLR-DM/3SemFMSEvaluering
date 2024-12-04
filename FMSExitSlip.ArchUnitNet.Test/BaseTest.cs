using FMSExitSlip.Application.Commands.Interfaces;
using FMSExitSlip.Domain;
using FMSExitSlip.Infrastructure;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Assembly = System.Reflection.Assembly;

namespace FMSExitSlip.ArchUnitNet.Test
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(DomainEntity).Assembly;
        protected static readonly Assembly ApplicationAssembly = typeof(IExitSlipCommand).Assembly;
        protected static readonly Assembly InfrastructureAssembly = typeof(ExitSlipContext).Assembly;
        protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
    }
}
