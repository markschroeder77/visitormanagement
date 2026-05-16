using CleanArchitecture.Blazor.Application.Common.Behaviours;
using CleanArchitecture.Blazor.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.UnitTests.Common.Behaviours;

    public class RequestLoggerTests
{
 
    private Mock<ICurrentUserService> _currentUserService;
    private Mock<ILogger<string>> _logger;


    [SetUp]
    public void Setup()
    {
       _currentUserService = new Mock<ICurrentUserService>();
       _logger = new Mock<ILogger<string>>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserName()).ReturnsAsync("Administrator");

        var behaviour = new LoggingBehaviour<string>(_logger.Object, _currentUserService.Object);
        await behaviour.Process("Test request", CancellationToken.None);

        _currentUserService.Verify(x => x.UserName(), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        _currentUserService.Setup(x => x.UserName()).ReturnsAsync(string.Empty);

        var behaviour = new LoggingBehaviour<string>(_logger.Object, _currentUserService.Object);
        await behaviour.Process("Test request", CancellationToken.None);

        _currentUserService.Verify(x => x.UserName(), Times.Once);
    }
}
