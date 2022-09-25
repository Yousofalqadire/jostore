using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;

namespace Api.Interfaces
{
    public interface IMember
    {
        Task<IEnumerable<MemberDto>> GetMembers();
        Task<IEnumerable<string>> GetUserRoles(string id);
        
    }
}