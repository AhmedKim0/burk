using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Burk.DAL.Context.contextIdentity;
public interface IUserContextService
{
	Guid GetCurrentUserId();
}
public class UserContextService : IUserContextService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserContextService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public Guid GetCurrentUserId()
	{
		var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		return string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
	}
}
