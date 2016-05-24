using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Managers
{
    public static class UserManagerExtension
    {
        public static Task<ApplicationUser> FindByIdWithRefAsync(this UserManager<ApplicationUser> manager, string userId)
        {
            return manager.Users.Include(x => x.Answers).FirstOrDefaultAsync(x => userId.Equals(x.Id));            
        }        
    }


    //public class CustomUserManager<TUser, TAnswer> : UserManager<TUser>
    //    where TUser : ApplicationUser
    //    where TAnswer : Answer
    //{
    //    //IMeasure measure { get; private set;}
    //    public CustomUserManager(
    //        IUserStore<TUser> store,
    //        IOptions<IdentityOptions> optionsAccessor,
    //        IPasswordHasher<TUser> passwordHasher,
    //        IEnumerable<IUserValidator<TUser>> userValidators,
    //        IEnumerable<IPasswordValidator<TUser>> passwordValidators,
    //        ILookupNormalizer keyNormalizer,
    //        IdentityErrorDescriber errors,
    //        IServiceProvider services,
    //        ILogger<UserManager<TUser>> logger,
    //        IHttpContextAccessor contextAccessor/*, IMeasure measure*/) :
    //    base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger, contextAccessor)
    //    {
    //    }

    //    public async Task<IEnumerable<TAnswer>> GetUserAnswers(string userId, Category category)
    //    {
    //        var user = await Users.Include(x => x.Answers).FirstOrDefaultAsync(x => userId.Equals(x.Id));

    //        if (user == null)
    //            return null;

    //        return Task.FromResult(user.Answers.AsEnumerable());    
    //    }
    //}
         
}
