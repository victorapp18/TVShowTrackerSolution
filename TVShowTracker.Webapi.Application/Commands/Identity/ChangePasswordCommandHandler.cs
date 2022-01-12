using TVShowTracker.Webapi.Application.Services;
using Dm = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApplicationResult<bool>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ChangePasswordCommandHandler(Dm.IIdentityRepository identityRepository, 
                                            IUnitOfWork unitOfWork) 
        {
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>();
            Dm.Identity identity = IdentityRepository.Get(request.IdentityId);

            string password = PasswordService.GenerateHash(request.NewPassword, false);
            identity.ChangePassword(password);

            IdentityRepository.Update(identity);
            await UnitOfWork.CommitAsync();

            result.Result = true;
            result.Message = "Password has been changed successfully.";

            return result;
        }
    }
}
