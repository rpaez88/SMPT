using Microsoft.EntityFrameworkCore;
using SMPT.Api.Services.Interface;
using SMPT.DataServices.Repository.Interface;

namespace SMPT.Api.Services
{
    public class Service<T> : IService<T> where T : class
    {
        public readonly ILogger _logger;
        protected readonly IConfiguration _config;
        protected IUnitOfWork _unitOfWork;

        public Service(ILoggerFactory loggerFactory, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _logger = loggerFactory.CreateLogger("Service");
            _config = config;
            _unitOfWork = unitOfWork;
        }
    }
}
