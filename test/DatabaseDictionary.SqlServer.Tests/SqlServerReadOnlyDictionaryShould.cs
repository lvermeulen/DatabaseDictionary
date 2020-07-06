using Xunit;

namespace DatabaseDictionary.SqlServer.Tests
{
	public class SqlServerReadOnlyDictionaryShould
	{
		private readonly SqlServerReadOnlyDictionary<string, int> _dictionary = new SqlServerDictionary<string, int>("", "sometable", "dbo", new ColumnMapper())
		{
			{ "One", 1 },
			{ "Two", 2 }
		};

		[Fact]
		public void Count()
		{
			Assert.Equal(2, _dictionary.Count);
		}

		[Theory]
		[InlineData("One", true)]
		[InlineData(nameof(ContainKey), false)]
		public void ContainKey(string key, bool expectedResult)
		{
			Assert.Equal(expectedResult, _dictionary.ContainsKey(key));
		}

		[Theory]
		[InlineData("One", 1, true)]
		[InlineData(nameof(ContainKey), 0, false)]
		public void TryGetValue(string key, int expectedValue, bool expectedResult)
		{
			Assert.Equal(expectedResult, _dictionary.TryGetValue(key, out int value));
			if (expectedResult)
			{
				Assert.Equal(expectedValue, value);
			}
		}

		[Fact]
		public void GetValue()
		{
			Assert.Equal(1, _dictionary["One"]);
		}

		[Fact]
		public void GetKeys()
		{
			Assert.Equal(new [] { "One", "Two" }, _dictionary.Keys);
		}

		[Fact]
		public void GetValues()
		{
			Assert.Equal(new[] { 1, 2 }, _dictionary.Values);
		}
	}
}
