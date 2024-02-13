﻿using System.Security.Claims;

namespace FullSharedCore.Aspects.Secured.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddIpAdressForUserData(this ICollection<Claim> claims, string ipAdress)
        {
            claims.Add(new Claim(ClaimTypes.UserData, ipAdress));
        }
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }


        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
