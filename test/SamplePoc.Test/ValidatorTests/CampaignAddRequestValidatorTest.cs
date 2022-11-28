using NUnit.Framework;
using SamplePoc.Contracts.Request;
using SamplePoc.Host.Validators;

namespace SamplePoc.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class CampaignAddRequestValidatorTest
    {
        private CampaignAddRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CampaignAddRequestValidator();
        }

        [Test]
        public void CampaignAddRequestValidator_Valid()
        {
            //Arrange
            var input = new CampaignAddRequest
            {
                Name = "name",
                Description = "description",
                Active = true,
                ModifiedBy = "test-modified",
                ModifiedDate = DateTime.Now
            };

            //Act
            var validationResult = _validator.Validate(input);

            //Assert
            Assert.IsTrue(validationResult.IsValid);
        }

        [Test]
        public void CampaignAddRequestValidator_InvalidName()
        {
            //Arrange
            var input = new CampaignAddRequest
            {
                Name = string.Empty,
                Description = "description",
                Active = true,
                ModifiedBy = "test-modified",
                ModifiedDate = DateTime.Now
            };

            //Act
            var validationResult = _validator.Validate(input);

            //Assert
            Assert.IsFalse(validationResult.IsValid);
        }

        [TearDown]
        public void TearDown()
        {
            //Dispose items here
        }
    }
}
