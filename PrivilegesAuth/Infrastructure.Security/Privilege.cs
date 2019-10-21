using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Security
{
    public enum Privilege
    {
		CanAccessAdminArea,
		CanDeleteData,
		CanCreateProducts
    }

	public class PrivilegeConstants
	{
		public const string PRIVILEGES_CLAIM_NAME = "PrivilegesClaim";
	}


}
