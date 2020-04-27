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
            var serverResponse = await _restClientBase.GetAsync<List<ServerResponse>>("v1/servers");
            if(serverResponse == null)
                return new List<ServerData>();
            var servers = _mapper.Map<List<ServerData>>(serverResponse);
            return servers;
        }
    }
}
