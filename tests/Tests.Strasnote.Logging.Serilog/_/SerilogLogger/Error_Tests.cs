﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Serilog;
using Strasnote.Logging;

namespace Tests.Strasnote.Logging.SerilogLogger_Tests
{
	public class Error_Tests
	{
		[Fact]
		public void Calls_Serilog_Error_With_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Error(message);
			logger.Error(message, args);

			// Assert
			serilog.Received(1).Error(SerilogLogger.Prefix + message, Array.Empty<object>());
			serilog.Received(1).Error(SerilogLogger.Prefix + message, args);
		}

		[Fact]
		public void Calls_Serilog_Error_With_Exception_And_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var exception = new Exception(Rnd.Str);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Error(exception, message);
			logger.Error(exception, message, args);

			// Assert
			serilog.Received(1).Error(exception, SerilogLogger.Prefix + message, Array.Empty<object>());
			serilog.Received(1).Error(exception, SerilogLogger.Prefix + message, args);
		}
	}
}
