﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using SimpleMigrations;

namespace Strasnote.Data.Clients.MySql.Migrations
{
	[Migration(1, "Create tables: auth.user, auth.refresh_token")]
	public sealed class CreateTables_Auth_0001 : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `auth.user` (
					`Id` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
					`UserName` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					`NormalizedUserName` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					`PasswordHash` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					`Email` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					`NormalizedEmail` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					`EmailConfirmed` BIT(1) NOT NULL DEFAULT b'0',
					`PhoneNumber` VARCHAR(20) NOT NULL DEFAULT '' COLLATE 'utf8_general_ci',
					`PhoneNumberConfirmed` BIT(1) NOT NULL DEFAULT b'0',
					`TwoFactorEnabled` BIT(1) NOT NULL DEFAULT b'0',
					`UserPublicKey` BLOB NOT NULL,
					`UserPrivateKey` BLOB NOT NULL,
					`UserProfile` LONGTEXT NULL DEFAULT NULL COLLATE 'utf8mb4_bin',
					`AccessFailedCount` INT(11) NOT NULL DEFAULT '0',
					`LockoutEnd` TIMESTAMP NULL DEFAULT NULL,
					`LockoutEnabled` BIT(1) NOT NULL DEFAULT b'0',
					`ConcurrencyStamp` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					`SecurityStamp` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					PRIMARY KEY (`Id`) USING BTREE,
					UNIQUE INDEX `UserName` (`UserName`) USING BTREE,
					UNIQUE INDEX `Email` (`Email`) USING BTREE,
					CONSTRAINT `UserProfile` CHECK (json_valid(`UserProfile`))
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");

			Execute(@"
				CREATE TABLE `auth.refresh_token` (
					`RefreshTokenId` BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT,
					`RefreshTokenExpires` DATETIME NOT NULL,
					`RefreshTokenValue` VARCHAR(255) NOT NULL COLLATE 'utf8_general_ci',
					`UserId` BIGINT(20) UNSIGNED NOT NULL,
					PRIMARY KEY (`RefreshTokenId`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");
		}

		protected override void Down()
		{
			Execute("DROP TABLE IF EXISTS `auth.user`;");
			Execute("DROP TABLE IF EXISTS `auth.refresh_token`;");
		}
	}
}
