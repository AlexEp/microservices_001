using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Communication
{
    public interface ICommunicationProvider
    {
        public Task<T_OUT> SendAsync<T_OUT, T_IN>(string url, HttpMethod method, T_IN param);

    }
}
