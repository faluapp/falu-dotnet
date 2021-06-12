using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Falu.Tests
{
    public class FaluClientTests
    {
        //[Fact]
        //public async Task SearchIdentityAsync_Throws_InvalidOperationException()
        //{
        //    var services = new ServiceCollection();
        //    services.AddFalu("TEST_APIKEY");

        //    var provider = services.BuildServiceProvider();
        //    using var scope = provider.CreateScope();
        //    var sp = scope.ServiceProvider;
        //    var client = sp.GetRequiredService<FaluClient>();

        //    var msg = "Either 'documentNumber' or 'phone' must be provided";
        //    var ex = await Assert.ThrowsAsync<InvalidOperationException>(
        //        () => client.SearchIdentityAsync(nationalId: null, phone: null));
        //    Assert.Equal(msg, ex.Message);

        //    ex = await Assert.ThrowsAsync<InvalidOperationException>(
        //        () => client.SearchIdentityAsync(nationalId: "", phone: ""));
        //    Assert.Equal(msg, ex.Message);
        //}
    }
}
