using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Result.WebAPI.Abstractions
{
    public interface IMethodResult
    {
        object ExecuteResult();
    }
}
