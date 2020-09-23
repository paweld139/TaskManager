using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PDCore.Extensions;
using PDCore.Interfaces;
using PDCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Strategies;
using TaskManager.Web.Api;

namespace TaskManager.Web.Tests
{
    [TestClass]
    public class TicketsControllerTests
    {
        [TestMethod]
        public async Task CanCreateTicketWithCorrectModel()
        {
            // ARRANGE 

            var ticketRepository = new Mock<ITicketRepository>();


            var unitOfWork = new Mock<ITaskManagerUow>();

            unitOfWork.Setup(uow => uow.Tickets).Returns(() => ticketRepository.Object);


            var principal = GetPrincipal();


            var ticketsController = new TicketsController(unitOfWork.Object);

            ticketsController.ControllerContext.RequestContext.Principal = principal;


            var ticketBasic = new TicketBasic
            {
                StatusId = 6,
                Subject = SecurityUtils.GetUniqueCode(4),
                Description = SecurityUtils.GetUniqueCode(1),
                TypeId = 12,
                PriorityId = 1,
                RepresentativeId = SecurityUtils.GetGuid(),
                ContrahentId = 1
            };

            // ACT

            var validationResult = ObjectUtils.Validate(ticketBasic);

            await ticketsController.Post(ticketBasic);

            // ASSERT

            Assert.IsNull(validationResult);

            ticketRepository.Verify(r => r.SaveNewAsync(It.IsAny<TicketBrief>(), principal, null), Times.AtLeastOnce());
        }

        [TestMethod]
        public async Task TicketDataAccessStrategyChecksUsersContrahentIdAndRole()
        {
            var employeeId = SecurityUtils.GetGuid();

            var principals = new[]
            {
                GetPrincipal(employeeId, 1, "Klient"),
                GetPrincipal(employeeId, 0, "Klient"),
                GetPrincipal(employeeId, 1),
                GetPrincipal(employeeId, 0),
            };

            var actual = principals.Select(p => new TicketDataAccessStrategy(p).CanAdd());

            var expected = new[] { true, false, false, false };

            await Task.WhenAll(actual);

            Assert.IsTrue(actual.Select(p => p.Result).SequenceEqual(expected));
        }

        private IPrincipal GetPrincipal(string employeeId, int contrahentId, string role = null)
        {
            var claims = new List<Claim>
            {
               new Claim("EmployeeId", employeeId),
               new Claim("ContrahentId", contrahentId.ToString())
            };

            if (role != null)
            {
                var roleCLaim = new Claim(ClaimTypes.Role, role);

                claims.Add(roleCLaim);
            }

            var identity = new ClaimsIdentity(claims, "mock");

            return new ClaimsPrincipal(identity);
        }

        private IPrincipal GetPrincipal()
        {
            var username = "FakeUserName";
            var identity = new GenericIdentity(username, "");

            var mockPrincipal = new Mock<ClaimsPrincipal>();

            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            return mockPrincipal.Object;
        }
    }
}
