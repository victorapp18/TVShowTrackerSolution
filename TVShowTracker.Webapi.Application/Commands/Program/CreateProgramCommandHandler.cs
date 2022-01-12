using Dm = TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate;
using Cm = TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;
using Im = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Message.Concrete;
using Framework.Seedworks.Concrete.Enums;
using Microsoft.Extensions.Configuration;

namespace TVShowTracker.Webapi.Application.Commands.Program
{
    public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, ApplicationResult<bool>>
    {
        private Dm.IProgramRepository ProgramRepository { get; }
        private Cm.IChannelRepository ChannelRepository { get; }
        private Im.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }
        
        public static IConfiguration Configuration;

        public CreateProgramCommandHandler( Dm.IProgramRepository programRepository,
                                            Cm.IChannelRepository channelRepository,
                                            Im.IIdentityRepository identityRepository,
                                            IUnitOfWork unitOfWork,
                                            IConfiguration configuration)
        {
            ProgramRepository = programRepository;
            ChannelRepository = channelRepository;
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
            Configuration = configuration;
        }

        public async Task<ApplicationResult<bool>> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>() { Result = false,  Message = "Invalid request, see validation field for more details." };

            Im.IdentityRole identityRole = IdentityRepository.GetMyRoleByIdentityId(request.IdentityId);
            if (identityRole.RoleId != 1)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Permission denied. To add a new channel you need admin permission.");

                return result;
            }

            Cm.Channel channel = ChannelRepository.Get(request.ChannelId);
            if (channel == null)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("The channel informed does not exist. Please enter a valid channel.");

                return result;
            }

            Dm.Program program = ProgramRepository.Get(request.ExhibitionDate, request.ChannelId);
            if (program != null)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("There is already a schedule for the informed date.");

                return result;
            }

            program = Dm.Program.Create(request.ChannelId, request.GenderId, request.Name, request.Description, request.ExhibitionDate);

            ProgramRepository.Create(program);
            await UnitOfWork.CommitAsync(EventDispatchMode.AfterSaveChanges);

            result.Message = $"Program has been created successfully.";
            result.Result = true;

            return result;
        }
    }
}
