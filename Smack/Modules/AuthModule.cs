using Nancy;
using Nancy.Authentication.Token;
using Nancy.ModelBinding;
using Smack.DataAccess;
using Smack.Models;

namespace Smack.Modules
{
    public class AuthModule : NancyModule
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenizer _tokenizer;
        private readonly IPasswordHasher _passwordHasher;

        public AuthModule(IUserRepository userRepository, ITokenizer tokenizer, IPasswordHasher passwordHasher) : base("smack/auth")
        {
            _userRepository = userRepository;
            _tokenizer = tokenizer;
            _passwordHasher = passwordHasher;
            Post["/login"] = x => Login();
        }

        private object Login()
        {
            var authUser = this.Bind<User>();
            var user = _userRepository.GetByUserName(authUser.VarUserName);
            if (IsValidUser(user, authUser))
            {
//                user.Roles = GetRoles(user);
                user.VarPassword = null;
                var token = _tokenizer.Tokenize(user, Context);
//                _logger.LogInfo("User logged in", user.Id);
                return new
                {
                    Token = token,
                    User = user
                };
            }
//            _logger.LogInfo("Failed login with email address: " + authUser.Email);
            return HttpStatusCode.Unauthorized;
        }

        private bool IsValidUser(User user, User authUser)
        {
            if (user == null)
            {
                return false;
            }

            //Temopraily commented, since we must use old encryption /TB
            if (!_passwordHasher.ValidatePassword(authUser, user))
            {
                return false;
            }

            return true;
        }
    }
}