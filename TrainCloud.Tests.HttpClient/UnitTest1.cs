using Microsoft.Extensions.DependencyInjection;
using TrainCloud.Tests.Microservices.Core;

namespace TrainCloud.Tests.HttpClient;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
    }
}
[TestClass]
public class UnitTest_Identity_SignIn : AbstractTest<Program>
{
    [TestInitialize]
    public void TestInitialize()
    {
        InitializeApplication();

        Client = ApplicationFactory!.CreateClient();
    }

    [TestMethod]
    public async Task TestMethod_SignIn_Administrator()
    { 

    }
}