﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Notes;

namespace Strasnote.Data.Entities.Auth
{
	/// <inheritdoc cref="IdentityUser{TKey}"/>
	public class UserEntity : IdentityUser<ulong>, IEntity
	{
		/// <summary>
		/// User ID (alias for <see cref="IdentityUser{TKey}.Id"/>)
		/// </summary>
		[Ignore]
		public ulong UserId
		{
			get => Id;
			set => Id = value;
		}

		/// <summary>
		/// This User's Public Key (for encryption)
		/// </summary>
		public byte[] UserPublicKey { get; set; } = Array.Empty<byte>();

		/// <summary>
		/// This User's Private Key (for decryption - encrypted using User's password)
		/// </summary>
		public byte[] UserPrivateKey { get; set; } = Array.Empty<byte>();

		/// <summary>
		/// User Profile information (e.g. name)
		/// TODO: Implement JSON converter to convert from string
		/// </summary>
		public string UserProfile { get; init; } = "{}";

		#region Lookups

		/// <summary>
		/// List of folders owned by this User
		/// </summary>
		[Ignore]
		public List<FolderEntity>? Folders { get; set; }

		/// <summary>
		/// List of notes owned by this User
		/// </summary>
		[Ignore]
		public List<NoteEntity>? Notes { get; set; }

		/// <summary>
		/// List of tags owned by this User
		/// </summary>
		[Ignore]
		public List<TagEntity>? Tags { get; set; }

		/// <summary>
		/// Folder encryption keys
		/// </summary>
		[Ignore]
		public List<EncryptedEntity>? FolderEncryptionKeys { get; set; }

		/// <summary>
		/// Note encryption keys
		/// </summary>
		[Ignore]
		public List<EncryptedEntity>? NoteEncryptionKeys { get; set; }

		#endregion

		/// <summary>
		/// User Profile information
		/// </summary>
		/// <param name="UserGivenName">The User's given name (or Christian / first name)</param>
		/// <param name="UserFullName">The User's full name</param>
		public record Profile(string? UserGivenName, string? UserFullName)
		{
			/// <summary>
			/// Create with blank details
			/// </summary>
			public Profile() : this(null, null) { }
		}
	}
}
