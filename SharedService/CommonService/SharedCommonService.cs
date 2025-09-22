using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SharedService.CommonService
{
    public class SharedCommonService
    {
        //private readonly ITokenService _tokenService;
        //private readonly IUserInfoService _userInfoService;

        //public SharedCommonService(IWorkContext workContext, ITokenService tokenService, IUserInfoService userInfoService)
        //{
        //    _workContext = workContext;
        //    _tokenService = tokenService;
        //    _userInfoService = userInfoService;
        //}

        //public async Task<(Guid userId, bool PublicKey, UserRetrievalStatus status, string message)> GetUserIdFromAuthorizationToken(string authorization_token)
        //{
        //    authorization_token = await _workContext.GetPublicOrToken(authorization_token);
        //    bool publicApiKey = false;

        //    if (string.IsNullOrWhiteSpace(authorization_token))
        //    {
        //        publicApiKey = false;
        //        return (Guid.Empty, publicApiKey, UserRetrievalStatus.TokenInvalid, ApiResponseMessage.invalidApiKeyOrToken);
        //    }

        //    var matchApiKey = await _workContext.MatchPublicKey(authorization_token);

        //    if (matchApiKey)
        //    {
        //        publicApiKey = true;
        //        return (Guid.Empty, publicApiKey, UserRetrievalStatus.TokenInvalid, ApiResponseMessage.invalidPublicApiKey);
        //    }

        //    var userId = await _tokenService.GetUserIdFromJwtToken(authorization_token);
        //    var userInfo = await _userInfoService.GetById(userId.ToString());

        //    if (userInfo.Result == null)
        //    {
        //        publicApiKey = false;
        //        return (Guid.Empty, publicApiKey, UserRetrievalStatus.NotFound, ApiResponseMessage.user_can_not_find);
        //    }

        //    if (!userInfo.Result.is_active)
        //    {
        //        publicApiKey = false;
        //        return (Guid.Empty, publicApiKey, UserRetrievalStatus.Inactive, ApiResponseMessage.user_incorrect_email_account_blocked);
        //    }

        //    return (userId, publicApiKey, UserRetrievalStatus.Success, null);
        //}

        //public async Task<List<TDestination>> MapList<TSource, TDestination>(IEnumerable<TSource> sourceList)
        //{
        //    var configuration = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<TSource, TDestination>();
        //    });

        //    var mapper = configuration.CreateMapper();

        //    return await Task.Run(() => mapper.Map<List<TDestination>>(sourceList));
        //}
        //public async Task<TDestination> MapSingle<TSource, TDestination>(TSource source)
        //{
        //    var configuration = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<TSource, TDestination>();
        //    });

        //    var mapper = configuration.CreateMapper();

        //    return await Task.Run(() => mapper.Map<TDestination>(source));
        //}

        //public async Task<string> GetFirstUniqueCharacters(string email, int length)
        //{
        //    HashSet<char> uniqueChars = new HashSet<char>();
        //    List<char> result = new List<char>();

        //    foreach (char c in email)
        //    {
        //        if (char.IsLetterOrDigit(c) && !uniqueChars.Contains(c))
        //        {
        //            uniqueChars.Add(c);
        //            result.Add(c);
        //        }

        //        if (result.Count == length)
        //        {
        //            break;
        //        }
        //    }

        //    return new string(result.ToArray());
        //}

        //public async Task<string> GetUniqueKey(string input, int length)
        //{
        //    HashSet<char> uniqueChars = new HashSet<char>();
        //    List<char> result = new List<char>();

        //    foreach (char c in input)
        //    {
        //        if (char.IsLetterOrDigit(c) && !uniqueChars.Contains(c))
        //        {
        //            uniqueChars.Add(c);
        //            result.Add(c);
        //        }

        //        if (result.Count == length)
        //        {
        //            break;
        //        }
        //    }

        //    return new string(result.ToArray());
        //}

        // Check if the text contains any Bengali characters
        public async Task<string> ContainsBengali(string input)
        {
            foreach (char c in input)
            {
                if (c >= '\u0980' && c <= '\u09FF')
                {
                    return "BNG";
                }
            }
            return "ENG";
        }
    }
}
