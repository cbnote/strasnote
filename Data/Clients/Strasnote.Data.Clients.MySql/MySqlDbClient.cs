﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Exceptions;

namespace Strasnote.Data.Clients.MySql
{
	/// <summary>
	/// MySQL-compatible database client
	/// </summary>
	public sealed class MySqlDbClient : IDbClient
	{
		/// <inheritdoc/>
		public string ConnectionString { get; }

		/// <summary>
		/// Inject and verify database configuration
		/// </summary>
		/// <param name="config">DbConfig</param>
		public MySqlDbClient(IOptions<DbConfig> config)
		{
			// Verify configuration
			if (config.Value.MySql is null)
			{
				throw new DbConfigMissingException<MySqlDbConfig>();
			}

			if (!config.Value.MySql.IsValid)
			{
				throw new DbConfigInvalidException<MySqlDbConfig>();
			}

			// Define connection string
			var mysql = config.Value.MySql;
			ConnectionString = string.Format(
				"server={0};port={1};user id={2};password={3};database={4};convert zero datetime=True;{5}",
				mysql.Host,
				mysql.Port,
				mysql.User,
				mysql.Pass,
				mysql.Database,
				mysql.Custom
			);
		}

		/// <summary>
		/// Define connection string manually
		/// </summary>
		/// <param name="connectionString">Connection String</param>
		public MySqlDbClient(string connectionString) =>
			ConnectionString = connectionString;

		/// <inheritdoc/>
		public IDbConnection Connect() =>
			new MySqlConnection(ConnectionString);

		/// <inheritdoc/>
		public bool MigrateTo(long version) =>
			MigrateTo(version, null);

		public bool MigrateTo(long version, ILogger? logger)
		{
			// Connection to database
			using var db = new MySqlConnection(ConnectionString);

			// Get migration objects
			var provider = new MysqlDatabaseProvider(db);
			var migrator = new SimpleMigrator(typeof(MySqlDbClient).Assembly, provider, logger);

			// Perform the migration
			migrator.Load();
			migrator.MigrateTo(version);

			// Ensure the migration succeeded
			return migrator.LatestMigration.Version == version;
		}
	}
}
