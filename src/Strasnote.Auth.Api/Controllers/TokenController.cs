﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Auth.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Auth.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class TokenController : Controller
	{
		private readonly IJwtTokenIssuer jwtTokenIssuer;
		private readonly ILog<TokenController> log;

		public sealed record TokenRequest([Required] string Email, [Required] string Password);
		public sealed record TokenResponseViewModel(string AccessToken, string RefreshToken, string? Message, bool Success);

		public TokenController(IJwtTokenIssuer jwtToken, ILog<TokenController> log) =>
			(this.jwtTokenIssuer, this.log) = (jwtToken, log);

		[HttpPost]
		public async Task<IActionResult> GetToken(TokenRequest tokenRequest)
		{
			log.Trace("User {@User} logging in", tokenRequest.Email);
			var token = await jwtTokenIssuer.GetTokenAsync(tokenRequest.Email, tokenRequest.Password);

			var tokenResponse = new TokenResponseViewModel(token.AccessToken, token.RefreshToken, token.Message, token.Success);

			if (!token.Success)
			{
				log.Trace("User {@User} failed to log in with message {@Message}", tokenRequest.Email, tokenResponse.Message ?? "Login failed");
				return Unauthorized(tokenResponse);
			}

			log.Trace("User {@User} logged in successfully", tokenRequest.Email);
			return Ok(tokenResponse);
		}

		[HttpPost("refresh")]
		public Task<IActionResult> GetRefreshToken()
		{
			throw new NotImplementedException();
		}
	}
}
