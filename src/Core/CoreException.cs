using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class CoreException : Exception
    {
        public CoreException()
        {

        }
        public CoreException(string message) : base(message)
        {

        }
        public CoreException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
