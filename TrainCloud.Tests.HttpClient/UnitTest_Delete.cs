using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TrainCloud.HttpClient;
using TrainCloud.Tests.Microservices.Core;

namespace TrainCloud.Tests.HttpClient;

[TestClass]
public class UnitTest_Delete : AbstractTest<Program>
{
    [TestMethod]
    public async Task TestMethod_NotAuthorized_401()
    { 
        // Arrange
        var anonymousClient = GetClient();

        // Act
        bool success = await anonymousClient.DeleteRequestAsync($"/Delete/{Guid.NewGuid()}", httpStatus =>
        {
            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, httpStatus);
        });

        // Assert
        Assert.IsFalse(success);
    }
}