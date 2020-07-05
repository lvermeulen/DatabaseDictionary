using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace DatabaseDictionary.SqlServer
{
	public class SqlServerDictionary<TKey, TValue> : SqlServerReadonlyDictionary<TKey, TValue>, IDictionary<TKey, TValue>
	{
		public SqlServerDictionary(string connectionString, string table, string schema = null)
			: base(connectionString, table, schema)
		{ }

		public SqlServerDictionary(IDbConnection connection, string table, string schema = null)
			: base(connection, table, schema)
		{ }

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			// add to table
		}

		public void Clear()
		{
			// clear table
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotImplementedException();
		}

		public bool IsReadOnly { get; } = false;

		public void Add(TKey key, TValue value)
		{
			// add to table
		}

		public bool Remove(TKey key)
		{
			throw new NotImplementedException();
		}

		public new TValue this[TKey key]
		{
			get => base[key];
			set => throw new NotImplementedException();
		}

		public new ICollection<TKey> Keys => new Collection<TKey>(base.Keys.ToList());
		public new ICollection<TValue> Values => new Collection<TValue>(base.Values.ToList());
	}
}
