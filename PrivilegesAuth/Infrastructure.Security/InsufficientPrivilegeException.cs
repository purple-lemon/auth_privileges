using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Security
{
	public class InsufficientPrivilegeException : Exception
	{
		public InsufficientPrivilegeException() : base() { }
		public InsufficientPrivilegeException(string message) : base(message) { }
	}
}
