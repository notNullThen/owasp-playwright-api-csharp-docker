using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests;

public class UnitTest1()
{
    public class TestClass1(ITestOutputHelper output) : Test(output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task Test1()
        {
            var usersHelper = new UsersHelper();

            var user = await usersHelper.CreateRandomUserAsync();
            _output.WriteLine(
                $"Email: {user.Response.Data.Email}\nCreated at: {user.Response.Data.CreatedAt}\n"
            );
        }
    }

    public class TestClass2(ITestOutputHelper output) : Test(output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task Test2()
        {
            var usersHelper = new UsersHelper();

            var user = await usersHelper.CreateRandomUserAsync();
            _output.WriteLine(
                $"Email: {user.Response.Data.Email}\nCreated at: {user.Response.Data.CreatedAt}\n\n\n"
            );
        }
    }

    public class TestClass3(ITestOutputHelper output) : Test(output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task Test3()
        {
            var usersHelper = new UsersHelper();

            var user = await usersHelper.CreateRandomUserAsync();
            _output.WriteLine(
                $"Email: {user.Response.Data.Email}\nCreated at: {user.Response.Data.CreatedAt}\n\n\n"
            );
        }
    }

    public class TestClass4(ITestOutputHelper output) : Test(output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task Test4()
        {
            var usersHelper = new UsersHelper();

            var user = await usersHelper.CreateRandomUserAsync();
            _output.WriteLine(
                $"Email: {user.Response.Data.Email}\nCreated at: {user.Response.Data.CreatedAt}\n\n\n"
            );
        }
    }

    public class TestClass5(ITestOutputHelper output) : Test(output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task Test5()
        {
            var usersHelper = new UsersHelper();

            var user = await usersHelper.CreateRandomUserAsync();
            _output.WriteLine(
                $"Email: {user.Response.Data.Email}\nCreated at: {user.Response.Data.CreatedAt}\n\n\n"
            );
        }
    }

    public class TestClass6(ITestOutputHelper output) : Test(output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task Test6()
        {
            var usersHelper = new UsersHelper();

            var user = await usersHelper.CreateRandomUserAsync();
            _output.WriteLine(
                $"Email: {user.Response.Data.Email}\nCreated at: {user.Response.Data.CreatedAt}\n\n\n"
            );
        }
    }
}
