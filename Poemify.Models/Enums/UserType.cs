using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.Enums
{
    public enum UserType
    {
        Poet,
        Reader,
        Moderator

    }
    public static class UserypeExtension
    {
        public static string GetStringValue(this UserType userType)
        {
            return userType switch
            {
                UserType.Poet => "poet",
                UserType.Reader => "reader",
                UserType.Moderator => "moderator",
                _ => null
            };
        }
    }
   
}
