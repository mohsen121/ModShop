using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;

namespace ModShop.Application.ErrorLogs.Commands.CreateErrorLog
{
    public class CreateErrorLogCommand : ICreateBaseEntityCommand<ErrorLog>
    {
        public ErrorLog Entity { get; set; }
    }

    public class CreateErrorLogCommandHandler : CreateBaseEntityCommandHandler<ErrorLog, CreateErrorLogCommand>
    {

        public CreateErrorLogCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
        //public async Task<Unit> Handle(CreateErrorLogCommand request, CancellationToken cancellationToken)
        //{
        //    var entity = new ErrorLog
        //    {
        //        Error = request.Error,
        //        ClassName = request.ClassName,
        //        MethodName = request.MethodName
        //    };

        //    _appDb.ErrorLogs.Add(entity);
        //    await _appDb.SaveChangesAsync(cancellationToken);

        //    return Unit.Value;
        //}
    }
}
