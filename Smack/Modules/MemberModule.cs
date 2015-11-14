using Nancy;
using Nancy.ModelBinding;
using Smack.DataAccess;
using Smack.Models;

namespace Smack.Modules
{
    public class MemberModule : SecureModule
    {
        private readonly IMemberRepository _memberRepository;

        public MemberModule(IMemberRepository memberRepository) : base("smack/members")
        {
            _memberRepository = memberRepository;
            Get["/"] = x => GetAll();
            Post["/"] = x => SavNewMember();
        }

        private object SavNewMember()
        {
            Member member = this.Bind<Member>();
            _memberRepository.Save(member);
            return HttpStatusCode.Created;
        }

        private object GetAll()
        {
            return _memberRepository.GetAll();
        }
    }
}