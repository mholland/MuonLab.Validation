using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable
{
	public class When_validating_a_property_as_less_than_a_scalar
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Fact]
		public void test_1_less_than_4_returns_true()
		{
			var testClass = new TestClass(1);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Fact]
		public void test_8_less_than_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			validationReport.Violations.First().Error.Key.ShouldEqual("LessThan");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		[Fact]
		public void test_4_less_than_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("LessThan");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		private class TestClass
		{
			public int value { get; set; }

			public TestClass(int value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsLessThan(4));
			}
		}
	}
}