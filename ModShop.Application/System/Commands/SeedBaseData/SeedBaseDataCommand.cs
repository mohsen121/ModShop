using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.Common.Interfaces;

namespace ModShop.Application.System.Commands.SeedBaseData
{
    public class SeedBaseDataCommand : IRequest
    {
    }

    public class SeedBaseDataCommandHandler : IRequestHandler<SeedBaseDataCommand>
    {
        private IAppDb _appDb;

        public SeedBaseDataCommandHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<Unit> Handle(SeedBaseDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new BaseDataSeeder(_appDb);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
