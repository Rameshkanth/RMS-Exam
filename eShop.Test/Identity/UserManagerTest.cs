using eShop.Web.IdentityProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Test.Identity
{
    [TestClass]
    public class UserManagerTest
    {
        [TestMethod]
        public async Task User_Registration_Calls_UserManager_CreateAsync()
        {
            
            // Setup
            var store = new Mock<ICustomUserStore>();
            var password = "Lejkguie26$%";
            var user = new ApplicationUser { UserName = "Foo" };
            store.Setup(s => s.CreateAsync(user, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
            store.Setup(s => s.GetUserNameAsync(user, CancellationToken.None)).Returns(Task.FromResult(user.UserName)).Verifiable();
            store.Setup(s => s.SetNormalizedUserNameAsync(user, user.UserName.ToUpperInvariant(), CancellationToken.None)).Returns(Task.FromResult(0)).Verifiable();
            var userManager = MockHelpers.TestUserManager<ApplicationUser>(store.Object);

            // Act
            var result = await userManager.CreateAsync(user, password);


            // Assert
            Assert.IsTrue(result.Succeeded);
            store.VerifyAll();
        }

    }
}
