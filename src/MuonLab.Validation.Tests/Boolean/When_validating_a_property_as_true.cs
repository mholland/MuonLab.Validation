using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.Boolean
{
	public class When_validating_a_property_as_true
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Fact]
		public void ensure_true_returns_true()
		{
			var testClass = new TestClass(true);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Fact]
		public void ensure_false_returns_false()
		{
			var testClass = new TestClass(false);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			violations[0].Error.Key.ShouldEqual("BeTrue");
		}

		private class TestClass
		{
			public bool value { get; set; }

			public TestClass(bool value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsTrue());
			}
		}
	}
}