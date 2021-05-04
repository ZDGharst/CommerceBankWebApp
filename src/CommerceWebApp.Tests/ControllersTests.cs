using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Commerce_WebApp;
using Commerce_WebApp.Controllers;
using Commerce_WebApp.Data;
using Commerce_WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace CommerceWebApp.Tests
{
    public class ControllersTests
    {
        [Fact]
        public async Task NotificationRule_Create_0Deposit_Test()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new NotificationController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            
            var notificationRule = new Notification_Rule();
            notificationRule.Type = "Deposit";
            notificationRule.Condition = "NA";
            notificationRule.Value = 0;

            //Act
            var result = await controller.Create(notificationRule);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task NotificationRule_Create_Invalid_Notification_Test()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new NotificationController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var notificationRule = new Notification_Rule();
            notificationRule.Type = "Deposit";
            notificationRule.Condition = "NA";
            notificationRule.Value = -1;

            //Act
            var result = await controller.Create(notificationRule);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task NotificationRule_Delete_Test_Null_ID()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new NotificationController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var notificationRule = new Notification_Rule();
            notificationRule.Type = "Deposit";
            notificationRule.Condition = "NA";
            notificationRule.Value = -1;

            //Act
            var result = await controller.Delete(null);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task NotificationRule_Delete_Test_Not_Null_ID()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new NotificationController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var notificationRule = new Notification_Rule();
            notificationRule.Type = "Deposit";
            notificationRule.Condition = "NA";
            notificationRule.Value = -1;

            //Act
            var result = await controller.Delete(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task NotificationRule_Edit_Test_Null_ID()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new NotificationController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //Act
            var result = await controller.Edit(null);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task NotificationRule_Edit_Test_Valid_ID()
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new NotificationController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //Act
            var result = await controller.Edit(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AccountController_Index_Test_Null_User()
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new AccountController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            int id = 211111110;

            //Act
            var result = await controller.Index(id) as ViewResult;

            //Assert
            Assert.Null(result.ViewName); ;
        }

        [Fact]
        public async Task AccountController_Index_Test_Valid_User()
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "JohnSmith"),
                new Claim(ClaimTypes.NameIdentifier, "211111110"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new AccountController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            int id = 211111110;

            //Act
            var result = await controller.Index(id) as ViewResult;

            //Assert
            Assert.Null(result.ViewName); ;
        }

        [Fact]
        public async Task AccountController_Index_Test_Null_User2()
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new AccountController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            int id = 211111110;
            string type = "CR";
            string description = "Test Transaction";
            float amount = 999999.99F;

            //Act
            var result = await controller.Index(id, type, description, amount) as ViewResult;

            //Assert
            Assert.Null(result.ViewName);
        }

        [Fact]
        public async Task AccountController_Index_Test_Correct_User()
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "JohnSmith"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "CommerceTestDB");
            var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var controller = new AccountController(_dbContext);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            int id = 211111110;
            string type = "CR";
            string description = "Test Transaction";
            float amount = 999999.99F;

            //Act
            var result = await controller.Index(id, type, description, amount) as ViewResult;

            //Assert
            Assert.Null(result.ViewName);
        }
        /*
        [Fact]
        public Task HomeController_Index_Test()
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
             new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var controller = new HomeController();

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //act
            var result = ;

            //assert
        }*/
    }
    public class ModelsTests
    {
        
        [Fact]
        public void Accounts_Decimal_Test_InterestRate()
        {
            //arange
            Account account = new Account();
            account.Interest_rate = 9999.999M;

            //act 
            var accountValue = account.Interest_rate;

            //assert
            Assert.StrictEqual<decimal>(9999.999M, accountValue);
        }

        [Fact]
        public void Accounts_Decimal_Test_Balance()
        {
            //arange
            Account account = new Account();

            //act 
            account.Balance = 9999.999M;
            var accountValue = account.Balance;

            //assert
            Assert.StrictEqual<decimal>(9999.999M, accountValue);
        }

        [Fact]
        public void Financial_Transaction_TimeStamp_Test()
        {
            //arrange
            Financial_Transaction transaction = new Financial_Transaction();

            //act
            transaction.TimeStamp = new DateTime(2020, 5, 5, 1, 1, 1);
            var testdate = transaction.TimeStamp;

            //assert
            Assert.Equal(testdate.ToString(), transaction.TimeStamp.ToString());
        }
    }
}
