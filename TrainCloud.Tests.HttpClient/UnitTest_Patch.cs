using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TrainCloud.HttpClient;
using TrainCloud.HttpClient.TestServer.Models;
using TrainCloud.Tests.Microservices.Core;

namespace TrainCloud.Tests.HttpClient;

[TestClass]
public class UnitTest_Patch : AbstractTest<Program>
{
    [TestMethod]
    public async Task TestMethod_Ok_200()
    {
        // Arrange
        var client = GetClient("token");

        var patchModel = new PatchModel()
        {

        };

        // Act
        ResponseModel? model = await client.PatchRequestAsync<PatchModel, ResponseModel>($"/Patch/{Guid.NewGuid()}", patchModel, httpStatus =>
        {
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpStatus);
        });

        // Assert
        Assert.IsNotNull(model);
    }
}