﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Folder User entity
	/// </summary>
	public sealed record FolderUserEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			FolderUserId;

		/// <summary>
		/// Folder User ID
		/// </summary>
		public long FolderUserId { get; init; }

		#region Relationships

		/// <summary>
		/// User ID
		/// </summary>
		public long UserId { get; init; }

		/// <summary>
		/// Folder ID
		/// </summary>
		public long FolderId { get; init; }

		#endregion
	}
}
