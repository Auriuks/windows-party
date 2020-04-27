using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsPartyBase.Interfaces;
using WindowsPartyBase.Models;
using AutoMapper;

namespace WindowsPartyBase.Services
{
    public class ServerInformationService : IServerInformationService
    {
        private readonly IRestClientBase _restClientBase;
        private readonly IMapper _mapper;
        public ServerInformationService(IRestClientBase restClientBase, IMapper mapper)
        {
            _restClientBase = restClientBase;
            _mapper = mapper;
        }

        public async Task<List<ServerData>> GetServers()
        {
            List<ServerData> servers = null;

            try
            {
                var serverResponse = await _restClientBase.GetAsync<List<ServerResponse>>("v1/servers");
                servers = _mapper.Map<List<ServerData>>(serverResponse);
            }
            catch (Exception)
            {
                servers = new List<ServerData>();
            }

            return servers;
        }
    }
}
