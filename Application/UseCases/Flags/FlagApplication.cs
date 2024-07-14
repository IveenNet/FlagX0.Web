using AutoMapper;
using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Core.Entities;
using FlagX0.Web.Core.Interfaces;
using FlagX0.Web.Infrastructure.Data;
using System.Security.Claims;

namespace FlagX0.Web.Application.UseCases.Flags
{
    public class FlagApplication : IFlagApplication
    {

        /*private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public FlagApplication(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        */
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public FlagApplication(ApplicationDbContext applicationDbContext, IHttpContextAccessor contextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> Execute(string flagName, bool isActive)
        {
            string userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            FlagEntity flagEntity = new() { Name=flagName, UserId=userId, Value=isActive};

            var response = await _applicationDbContext.Flags.AddAsync(flagEntity);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    
    }
}
