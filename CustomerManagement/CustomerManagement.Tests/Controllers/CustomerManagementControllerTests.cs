using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CustomerManagement.Api.Controllers;
using CustomerManagement.Api.Repositories;
using CustomerManagement.Client.Models;
using NUnit.Framework;
using Rhino.Mocks;

namespace CustomerManagement.Tests.Controllers
{
    public class CustomerManagementControllerTests
    {
        private ICustomerManagementRepository _mockRepository;

        private CustomerManagementController _sut;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = MockRepository.GenerateMock<ICustomerManagementRepository>();

            _sut = new CustomerManagementController(_mockRepository)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async Task Get_ShouldReturnOkResult()
        {
            _mockRepository.Stub(r => r.GetLatestCustomersAsync())
                .ReturnAsync(new List<Customer>());

            var result = await _sut.GetAsync();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode());
        }

        [Test]
        public async Task Get_ShouldCallRepository()
        {
            _mockRepository.Stub(r => r.GetLatestCustomersAsync())
                .ReturnAsync(new List<Customer>());

            await _sut.GetAsync();

            _mockRepository.AssertWasCalled(r => r.GetLatestCustomersAsync(), r => r.Repeat.Once());
        }

        [TestCase("Otto", "Anna")]
        public async Task Get_NameIsPalindrome_ShouldMarkNameAsPalindrome(params string[] customerNames)
        {
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in customerNames)
            {
                customerList.Add(new Customer
                {
                    Name = customer,
                    Palindrome = false
                });
            }

            _mockRepository.Stub(r => r.GetLatestCustomersAsync())
                .ReturnAsync(customerList);

            var result = await _sut.GetAsync();

            var resultList = result.Content<IReadOnlyCollection<Customer>>();

            Assert.IsTrue(resultList.All(c => c.Palindrome));
        }

        [TestCase("Otto", "Thor")]
        public async Task Get_NameIsNotPalindrome_ShouldMarkNameAsNotPalindrome(params string[] customerNames)
        {
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in customerNames)
            {
                customerList.Add(new Customer
                {
                    Name = customer,
                    Palindrome = false
                });
            }

            _mockRepository.Stub(r => r.GetLatestCustomersAsync())
                .ReturnAsync(customerList);

            var result = await _sut.GetAsync();

            var resultList = result.Content<IReadOnlyCollection<Customer>>();

            Assert.IsFalse(resultList.All(c => c.Palindrome));
        }

        [Test]
        public async Task Upsert_ShouldReturnOkResult()
        {
            _mockRepository.Stub(r => r.UpsertCustomerAsync(Arg<UpsertCustomer>.Is.Anything))
                .ReturnAsync(true);

            var result = await _sut.UpsertAsync(new UpsertCustomer());

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode());
        }

        [Test]
        public async Task Upsert_ShouldCallRepository()
        {
            var upsertCustomer = new UpsertCustomer();

            _mockRepository.Stub(r => r.UpsertCustomerAsync(upsertCustomer))
                .ReturnAsync(true);

            await _sut.UpsertAsync(upsertCustomer);

            _mockRepository.AssertWasCalled(r => r.UpsertCustomerAsync(upsertCustomer), r => r.Repeat.Once());
        }
    }
}
