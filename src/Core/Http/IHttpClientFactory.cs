﻿using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http
{
    public interface IHttpClientFactory
    {
        IHttpClient GetHttpClient();
        IHttpClient GetHttpClientWithPolly(string applicationName, Func<string, IEnumerable<Policy>> pollices);
    }
}
