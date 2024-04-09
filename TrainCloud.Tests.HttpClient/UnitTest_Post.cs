using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TrainCloud.HttpClient;
using TrainCloud.HttpClient.TestServer.Models;
using TrainCloud.Tests.Microservices.Core;

namespace TrainCloud.Tests.HttpClient;

[TestClass]
public class UnitTest_Post : AbstractTest<Program>
{
    [TestMethod]
    public async Task TestMethod_Created_201()
    {
        // Arrange
        var client = GetClient("token");

        var postModel = new PostModel()
        {

        };

        // Act
        ResponseModel? model = await client.PostRequestAsync<PostModel, ResponseModel>($"/Post", postModel, httpStatus =>
        {
            // Assert
            Assert.AreEqual(HttpStatusCode.Created, httpStatus);
        });

        // Assert
        Assert.IsNotNull(model);
    }
}