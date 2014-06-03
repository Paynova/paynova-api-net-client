using System;
using System.Linq;

namespace Paynova.Api.Client.Extensions
{
    public static class UriExtensions
    {
        public static string GetAbsoluteUriExceptUserInfo(this Uri uri)
        {
            return string.IsNullOrEmpty(uri.UserInfo)
                ? uri.AbsoluteUri
                : uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.UserInfo, UriFormat.UriEscaped);
        }

        public static string[] GetUserInfoParts(this Uri uri)
        {
            return uri.UserInfo
                .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => Uri.UnescapeDataString(p))
                .ToArray();
        }
    }
}