using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TrainCloud.HttpClient;
using TrainCloud.HttpClient.TestServer.Models;
using TrainCloud.Tests.Microservices.Core;

namespace TrainCloud.Tests.HttpClient;

[TestClass]
public class UnitTest_Get : AbstractTest<Program>
{
    [TestMethod]
    public async Task TestMethod_Ok_200()
    { 
        // Arrange
        var client = GetClient("token");

        // Act
        ResponseModel? model = await client.GetRequestAsync<ResponseModel>($"/Get/{Guid.NewGuid()}", httpStatus =>
        {
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpStatus);
        });

        // Assert
        Assert.IsNotNull(model);
    }
}