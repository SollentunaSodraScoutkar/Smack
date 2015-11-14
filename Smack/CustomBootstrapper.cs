using Nancy;
using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.Hosting.Aspnet;
using Nancy.TinyIoc;
using Smack.DataAccess;

namespace Smack
{
    public class CustomBootstrapper : DefaultNancyAspNetBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IUserRepository>(new UserRepository());
            container.Register<IMemberRepository>(new MemberRepository());
            container.Register<IPasswordHasher>(new Pbkdf2PasswordHasher());
            container.Register<IDivisionRepository>(new DivisionRepository());

            container.Register<ITokenizer>(new Tokenizer());
            // Example options for specifying additional values for token generation
            //container.Register<ITokenizer>(new Tokenizer(cfg =>
            //                                             cfg.AdditionalItems(
            //                                                 ctx =>
            //                                                 ctx.Request.Headers["X-Custom-Header"].FirstOrDefault(),
            //                                                 ctx => ctx.Request.Query.extraValue)));
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            StaticConfiguration.DisableErrorTraces = false;
            pipelines.OnError.AddItemToEndOfPipeline((z, a) =>
            {
                return null;// ErrorResponse.FromException(a);
            });
            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));
            base.RequestStartup(container, pipelines, context);
        }
    }
}