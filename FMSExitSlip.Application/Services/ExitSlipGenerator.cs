using FMSExitSlip.Application.Helpers;
using FMSExitSlip.Application.Repositories;
using FMSExitSlip.Application.Services.ApplicationServiceInterface;
using FMSExitSlip.Domain.Entities;

namespace FMSExitSlip.Application.Services
{
    public class ExitSlipGenerator : IExitSlipGenerator
    {
        private readonly ILectureApplicationService _lectureApplicationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExitSlipRepository _exitSlipRepository;

        public ExitSlipGenerator(ILectureApplicationService lectureApplicationService, IUnitOfWork unitOfWork, IExitSlipRepository exitSlipRepository)
        {
            _lectureApplicationService = lectureApplicationService;
            _unitOfWork = unitOfWork;
            _exitSlipRepository = exitSlipRepository;
        }

        async Task IExitSlipGenerator.GenerateExitslipsForLectures()
        {

            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var lectures = await _lectureApplicationService.GetLecturesAsync();
                var otherExitSlips = await _exitSlipRepository.GetExitSlipsAsync();


                // Do
                foreach (var lecture in lectures)
                {
                    var exitSlip = ExitSlip.Create(lecture.Title, 5, Convert.ToInt32(lecture.Id), otherExitSlips);
                    await _exitSlipRepository.AddExitSlipAsync(exitSlip);
                }

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                await _unitOfWork.Rollback();
                throw;
            }

        }
    }

    public interface IExitSlipGenerator
    {
        Task GenerateExitslipsForLectures();
    }
}
