using FMSEvalueringUI.ModelDto;

namespace FMSEvalueringUI.ExternalServices.Interfaces
{
    public interface IEvalueringProxy
    {
        Task<List<ForumDto>> GetForumsAsync();
    }
}
