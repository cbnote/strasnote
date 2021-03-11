﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;

namespace Strasnote.Notes.Data.Entities
{
	/// <summary>
	/// Tag entity
	/// </summary>
	public sealed record TagEntity : IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			TagId;

		/// <summary>
		/// Tag ID
		/// </summary>
		public long TagId { get; init; }

		/// <summary>
		/// Tag Name
		/// </summary>
		public string TagName { get; init; } = string.Empty;

		/// <summary>
		/// When the Tag was created
		/// </summary>
		public DateTimeOffset TagCreated { get; init; }

		/// <summary>
		/// When the Tag was last updated
		/// </summary>
		public DateTimeOffset TagUpdated { get; init; }

		#region Relationships

		/// <summary>
		/// User ID (of the User who owns this Tag)
		/// </summary>
		public long UserId { get; init; }

		#endregion

		#region Lookups

		/// <summary>
		/// List of notes with this Tag
		/// </summary>
		public List<NoteEntity>? Notes { get; set; }

		#endregion
	}
}
