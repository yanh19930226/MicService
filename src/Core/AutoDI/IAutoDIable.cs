using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AutoDI
{
    public interface IAutoDIable
    {

    }

    public interface ISingletonAutoDIable : IAutoDIable
    {

    }
    public interface IScopedAutoDIable : IAutoDIable
    {

    }
    public interface ITransientAutoDIable : IAutoDIable
    {

    }
}
