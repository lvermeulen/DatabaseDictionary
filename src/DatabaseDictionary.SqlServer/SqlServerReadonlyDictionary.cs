﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace DatabaseDictionary.SqlServer
{
	public class SqlServerReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
	{
		protected readonly SqlServerAdapter Adapter;
		protected Dictionary<TKey, TValue> Dictionary;
		protected bool IsModified;

		public SqlServerReadOnlyDictionary(string connectionString, string table, string schema = null, IColumnMapper columnMapper = null)
			: this(new SqlConnection(connectionString), table, schema, columnMapper)
		{ }

		public SqlServerReadOnlyDictionary(IDbConnection connection, string table, string schema = null, IColumnMapper columnMapper = null)
		{
			if (columnMapper == null)
			{
				columnMapper = new DefaultColumnMapper(connection, table, schema);
			}

			string keyColumn = columnMapper.GetKeyColumnName();
			string valueColumn = columnMapper.GetValueColumnName();
			Adapter = new SqlServerAdapter(connection, keyColumn, valueColumn, table, schema);
			IsModified = true;
			Read();
		}

		private void Read()
		{
			if (IsModified)
			{
				Dictionary = Adapter.ReadAllRows<TKey, TValue>()
					.ToDictionary(x => x.Item1, x => x.Item2);
				IsModified = false;
			}
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			Read();
			return Dictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count
		{
			get
			{
				Read();
				return Dictionary.Count;
			}
		}

		public bool ContainsKey(TKey key)
		{
			Read();
			return Dictionary.ContainsKey(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			Read();
			return Dictionary.TryGetValue(key, out value);
		}

		public TValue this[TKey key]
		{
			get
			{
				Read();
				return Dictionary[key];
			}
		}

		public IEnumerable<TKey> Keys => Dictionary.Keys;
		public IEnumerable<TValue> Values => Dictionary.Values;
	}
}
