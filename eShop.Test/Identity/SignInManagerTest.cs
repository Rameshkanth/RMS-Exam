using eShop.Web.IdentityProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace eShop.Test.Identity
{
    [TestClass]
    public class SignInManagerTest
    {
        [TestMethod]
        public async Task Password_SignIn_Fails_With_Incorrect_Password()
        {
            // Setup
            var user = new ApplicationUser { UserName = "Foo" };
            var manager = MockHelpers.SetupUserManager(user);
            manager.Setup(m => m.SupportsUserLockout).Returns(true).Verifiable();
            manager.Setup(m => m.IsLockedOutAsync(user)).ReturnsAsync(false).Verifiable();
            manager.Setup(m => m.CheckPasswordAsync(user, "bogus")).ReturnsAsync(false).Verifiable();
            var context = new Mock<HttpContext>();
            var logStore = new StringBuilder();
            var helper = MockHelpers.SetupSignInManager(manager.Object, context.Object, logStore);
            
            // Act
            var result = await helper.PasswordSignInAsync(user.UserName, "bogus", false, false);
            var checkResult = await helper.CheckPasswordSignInAsync(user, "bogus", false);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.IsFalse(checkResult.Succeeded);
            StringAssert.Contains(logStore.ToString(), $"User {user.Id} failed to provide the correct password.");
            manager.Verify();
            context.Verify();
        }

        [TestMethod]
        public async Task Password_SignIn_Fails_With_Invalid_User()
        {
            // Setup
            var manager = MockHelpers.MockUserManager<ApplicationUser>();
            manager.Setup(m => m.FindByNameAsync("bogus")).ReturnsAsync(default(ApplicationUser)).Verifiable();
            var context = new Mock<HttpContext>();
            var helper = MockHelpers.SetupSignInManager(manager.Object, context.Object);

            // Act
            var result = await helper.PasswordSignInAsync("bogus", "bogus", false, false);

            // Assert
            Assert.IsFalse(result.Succeeded);
            manager.Verify();
            context.Verify();
        }

    }
}
