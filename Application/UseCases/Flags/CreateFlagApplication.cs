    using FlagX0.Web.Application.Interface.UseCases;
    using FlagX0.Web.Core.Entities;
    using FlagX0.Web.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using ROP;
    using System.Security.Claims;

    namespace FlagX0.Web.Application.UseCases.Flags
    {
        //A traves de .Net 8 se puede hacer asi, si no es record es privado
        public class CreateFlagApplication(ApplicationDbContext _applicationDbContext, IFlagUserDetails _flagUserDetails) : ICreateFlagApplication
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


        /*public async Task<Result<bool>> Execute(string flagName, bool isActive)
        {

            FlagEntity flagEntity = new() { Name=flagName, UserId= _flagUserDetails.UserId, Value=isActive};

            var response = await _applicationDbContext.Flags.AddAsync(flagEntity);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }*/

        public async Task<Result<bool>> Execute(string flagName, bool isActive) => await ValidateFlag(flagName).Bind(x => AddFlagToDatabase(x, isActive));

        private async Task<Result<string>> ValidateFlag(string flagName)
        {
            var flags = await _applicationDbContext.Flags.ToListAsync();

            bool flagExist = flags.Any(a => a.Name.Equals(flagName, StringComparison.InvariantCulture));
    
            if (flagExist) return Result.Failure<string>("Flag name already exists");

            return flagName;
        }


        private async Task<Result<bool>> AddFlagToDatabase(string flagName, bool isActive)
            {
                FlagEntity entity = new FlagEntity() { UserId = _flagUserDetails.UserId, Name = flagName, Value = isActive };

                _ = await _applicationDbContext.Flags.AddAsync(entity);
                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
        }
    }
