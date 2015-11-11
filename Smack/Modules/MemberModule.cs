using Smack.DataAccess;

namespace Smack.Modules
{
    public class MemberModule : SecureModule
    {
        private readonly IMemberRepository _memberRepository;

        public MemberModule(IMemberRepository memberRepository) : base("smack/members")
        {
            _memberRepository = memberRepository;
            Get["/"] = x => GetAll();
        }

        private object GetAll()
        {
            return _memberRepository.GetAll();
        }
    }
}