using FMSEvaluering.Application.Commands.Interfaces;
using FMSEvaluering.Application.Repositories;
using FMSEvaluering.Domain;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using FMSEvaluering.Domain.Entities.ForumEntities;
using FMSEvaluering.Infrastructure;
using Assembly = System.Reflection.Assembly;

namespace FMSEvaluering.ArchUnitNet.Test
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(DomainEntity).Assembly;
        protected static readonly Assembly ApplicationAssembly = typeof(IForumCommand).Assembly;
        protected static readonly Assembly InfrastructureAssembly = typeof(EvaluationContext).Assembly;
        protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
    }

}
