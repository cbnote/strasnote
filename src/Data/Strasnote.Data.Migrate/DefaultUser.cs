﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using MaybeF;
using MaybeF.Linq;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Encryption;
using Strasnote.Logging;

namespace Strasnote.Data.Migrate
{
	/// <summary>
	/// Create the default user
	/// </summary>
	public static class DefaultUser
	{
		/// <summary>
		/// Insert the default user from configuration values
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="repo">IUserRepository</param>
		/// <param name="config">UserConfig</param>
		public static async Task<Maybe<ulong>> InsertAsync(ILog log, IUserRepository repo, UserConfig config)
		{
			var user = from ep in GetEmailAndPassword(config)
					   from keys in Keys.Generate(ep.password)
					   select new UserEntity
					   {
						   UserName = ep.email,
						   NormalizedUserName = ep.email,
						   Email = ep.email,
						   NormalizedEmail = ep.email,
						   EmailConfirmed = true,
						   PasswordHash = ep.password,
						   PhoneNumber = string.Empty,
						   UserPublicKey = keys.PublicKey,
						   UserPrivateKey = keys.PrivateKey,
						   SecurityStamp = Guid.NewGuid().ToString(),
						   ConcurrencyStamp = Guid.NewGuid().ToString()
					   };

			return await user.SwitchAsync(
				async x =>
				{
					var userId = await repo.CreateAsync(x);
					log.Debug("Inserted default user {User}", userId);
					return F.Some(userId);
				},
				none: r =>
				{
					log.Error("Unable to create user: {Reason}", r);
					return F.None<ulong>(r);
				}
			);
		}

		/// <summary>
		/// Check email and password are both set before returning them
		/// </summary>
		/// <param name="config">UserConfig</param>
		static internal Maybe<(string email, string password)> GetEmailAndPassword(UserConfig config)
		{
			if (config.Email is string email && config.Password is string password)
			{
				var hasher = new PasswordHasher<UserEntity>();
				var hashed = hasher.HashPassword(new(), password);

				return F.Some(
					(email, hashed)
				);
			}

			return F.None<(string, string), R.EmailAndPasswordMustBothBeSetReason>();
		}

		/// <summary>Reasons</summary>
		public static class R
		{
			/// <summary>Email and password must both be set to automatically create a user</summary>
			public sealed record EmailAndPasswordMustBothBeSetReason : IReason { }
		}
	}
}
