using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Security
{
    public static class PrivilegesPacker
    {
		public static string Pack(this IEnumerable<Privilege> privileges)
		{
			return string.Join(";", privileges);
		}

		public static List<Privilege> Unpack(this string privileges)
		{
			var privs = privileges.Split(';');
			var result = new List<Privilege>();
			foreach (var p in privs)
			{
				if (Enum.TryParse(p, out Privilege privilege))
				{
					result.Add(privilege);
				}
			}
			return result;
		}
    }
}
