using Eventos.IO.Domain.Core.COMMANDS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.INTERFACES
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
