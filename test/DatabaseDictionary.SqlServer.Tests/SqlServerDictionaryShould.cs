using System;
using System.Collections.Generic;
using Xunit;

namespace DatabaseDictionary.SqlServer.Tests
{
	public class SqlServerDictionaryShould
	{
		private readonly SqlServerDictionary<string, int> _dictionary = new SqlServerDictionary<string, int>("", "sometable", "dbo", new ColumnMapper())
		{
			{ "One", 1 },
			{ "Two", 2 }
		};

		[Fact]
		public void Add()
		{
			_dictionary.Add("Three", 3);
			Assert.Equal(3, _dictionary.Count);
		}

		[Fact]
		public void Clear()
		{
			_dictionary.Clear();
			Assert.Empty(_dictionary);
		}

		[Theory]
		[InlineData("One", 1, true)]
		[InlineData(nameof(Contain), 0, false)]
		public void Contain(string key, int value, bool expectedResult)
		{
			Assert.Equal(expectedResult, _dictionary.Contains(new KeyValuePair<string, int>(key, value)));
		}

		[Fact]
		public void CopyTo()
		{
			Assert.Throws<NotImplementedException>(() => _dictionary.CopyTo(new KeyValuePair<string, int>[] {}, 0));
		}

		[Fact]
		public void Remove()
		{
			_dictionary.Remove("One");
			Assert.Single(_dictionary);
		}

		[Fact]
		public void GetIsReadOnly()
		{
			Assert.False(_dictionary.IsReadOnly);
		}

		[Fact]
		public void SetAndGetValue()
		{
			int value = _dictionary["One"];
			Assert.Equal(1, value);
			_dictionary["One"] = 3;
			Assert.Equal(3, value);
		}

		[Fact]
		public void GetKeys()
		{
			Assert.Equal(new[] { "One", "Two" }, _dictionary.Keys);
		}

		[Fact]
		public void GetValues()
		{
			Assert.Equal(new[] { 1, 2 }, _dictionary.Values);
		}
	}
}
