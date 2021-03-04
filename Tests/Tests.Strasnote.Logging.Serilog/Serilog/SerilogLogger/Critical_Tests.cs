﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Serilog;
using Strasnote.Logging;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Logging.Serilog_Tests
{
	public class Critical_Tests
	{
		[Fact]
		public void Calls_Serilog_Fatal_With_Message_And_Args()
		{
			// Arrange
			var serilog = Substitute.For<ILogger>();
			var logger = new SerilogLogger(serilog);
			var message = Rnd.Str;
			var arg0 = Rnd.Int;
			var arg1 = Rnd.Str;
			var args = new object[] { arg0, arg1 };

			// Act
			logger.Critical(message, args);

			// Assert
			serilog.Received().Fatal(SerilogLogger.Prefix + message, args);
		}

		[Fact]
		public void Calls_Serilog_Fatal_With_Exception_And_Message_And_Args()
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
			logger.Critical(exception, message, args);

			// Assert
			serilog.Received().Fatal(exception, SerilogLogger.Prefix + message, args);
		}
	}
}
