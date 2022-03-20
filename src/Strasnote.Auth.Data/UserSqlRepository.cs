﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Data
{
	public sealed class UserSqlRepository : SqlRepository<UserEntity>, IUserRepository
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog with context</param>
		public UserSqlRepository(ISqlClient client, ILog<UserSqlRepository> log)
			: base(client, log, client.Tables.User) { }

		/// <inheritdoc/>
		public Task<TModel> RetrieveAsync<TModel>(ulong entityId) =>
			RetrieveAsync<TModel>(entityId, null);

		/// <inheritdoc/>
		public Task<TModel> RetrieveByEmailAsync<TModel>(string email) =>
			QuerySingleAsync<TModel>(null,
				(u => u.Email, SearchOperator.Equal, email)
			);

		/// <inheritdoc/>
		public Task<TModel> RetrieveByUsernameAsync<TModel>(string name) =>
			RetrieveByEmailAsync<TModel>(name);

		/// <inheritdoc/>
		public Task<TModel> UpdateAsync<TModel>(ulong entityId, TModel model) =>
			UpdateAsync(entityId, model, null);

		/// <inheritdoc/>
		public Task<int> DeleteAsync(ulong entityId) =>
			DeleteAsync(entityId, null);
	}
}
