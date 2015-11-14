using System.Collections.Generic;
using Smack.Models;

namespace Smack.DataAccess
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        void Save(Member member);
    }
}