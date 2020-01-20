using Moq;
using NUnit.Framework;
using Solution.Dispatchers;
using Solution.Sinks;
using System;
using System.Collections.Generic;

namespace Solution.Tests.Dispatchers
{
    [TestFixture]
    public class AsyncLogStatementsDispatcherTests
    {
        [Test]
        public void LogStatementIsWrittenToAllSinks()
        {
            // Arrange
            var firstSinkMock = new Mock<ISink>(MockBehavior.Strict);
            var secondSinkMock = new Mock<ISink>(MockBehavior.Strict);
            var thirdSinkMock = new Mock<ISink>(MockBehavior.Strict);
            var dispatcher = new AsyncLogStatementDispatcher(new List<ISink> { firstSinkMock.Object, secondSinkMock.Object, thirdSinkMock.Object });

            firstSinkMock.Setup(sink => sink.Write(It.IsAny<string>()));
            secondSinkMock.Setup(sink => sink.Write(It.IsAny<string>()));
            thirdSinkMock.Setup(sink => sink.Write(It.IsAny<string>()));

            var logStatement = new LogStatement(DateTimeOffset.UtcNow, LogLevel.Information, "Message");

            // Act
            dispatcher.Dispatch(logStatement);

            // Assert
            firstSinkMock.Verify(sink => sink.Write(logStatement.Render()), Times.Once);
            secondSinkMock.Verify(sink => sink.Write(logStatement.Render()), Times.Once);
            thirdSinkMock.Verify(sink => sink.Write(logStatement.Render()), Times.Once);
        }
    }
}
