﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.Clients.MySql.MySqlDbTables_Tests
{
	public class UserRole_Tests
	{
		[Fact]
		public void Returns_Correct_Table_Name()
		{
			// Arrange
			var tables = new MySqlDbTables();
			const string expected = "auth.user_role";

			// Act
			var result = tables.UserRole;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
