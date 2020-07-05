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

		private void Write()
		{
			Adapter.WriteAllRows(Dictionary);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Dictionary.Add(item.Key, item.Value);
		}

		public void Add(TKey key, TValue value)
		{
			Dictionary.Add(key, value);
			Write();
		}

		public void Clear()
		{
			Dictionary.Clear();
			Write();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return Dictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return Dictionary.Remove(item.Key);
		}

		public bool Remove(TKey key)
		{
			Dictionary.Remove(key);
			Write();
			return true;
		}

		public bool IsReadOnly => false;

		public new TValue this[TKey key]
		{
			get => base[key];
			set
			{
				Dictionary[key] = value;
				Write();
			}
		}

		public new ICollection<TKey> Keys => new Collection<TKey>(base.Keys.ToList());
		public new ICollection<TValue> Values => new Collection<TValue>(base.Values.ToList());
	}
}
