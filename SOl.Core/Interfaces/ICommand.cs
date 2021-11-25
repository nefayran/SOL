using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOl.Core.Interfaces
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}
