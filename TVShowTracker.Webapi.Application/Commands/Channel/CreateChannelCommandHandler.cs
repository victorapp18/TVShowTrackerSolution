using Dm = TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;
using Im = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Message.Concrete;
using Framework.Seedworks.Concrete.Enums;
using Microsoft.Extensions.Configuration;

namespace TVShowTracker.Webapi.Application.Commands.Channel
{
    public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, ApplicationResult<bool>>
    {
        private Dm.IChannelRepository ChannelRepository { get; }
        private Im.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }
        
        public static IConfiguration Configuration;

        public CreateChannelCommandHandler( Dm.IChannelRepository channelRepository,
                                            Im.IIdentityRepository identityRepository,
                                            IUnitOfWork unitOfWork,
                                            IConfiguration configuration)
        {
            ChannelRepository = channelRepository;
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
            Configuration = configuration;
        }

        public async Task<ApplicationResult<bool>> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>() { Result = false,  Message = "Invalid request, see validation field for more details." };

            Im.IdentityRole identityRole = IdentityRepository.GetMyRoleByIdentityId(request.IdentityId);
            if (identityRole.RoleId != 1)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Permission denied. To add a new channel you need admin permission.");

                return result;
            }

            Dm.Channel channel = ChannelRepository.Get(request.Namber);

            if (channel != null)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Namber the channel already exists.");

                return result;
            }

            channel = Dm.Channel.Create(request.Name, request.Description, request.Namber);

            ChannelRepository.Create(channel);
            await UnitOfWork.CommitAsync(EventDispatchMode.AfterSaveChanges);

            result.Message = $"Channel has been created successfully.";
            result.Result = true;

            return result;
        }
    }
}
