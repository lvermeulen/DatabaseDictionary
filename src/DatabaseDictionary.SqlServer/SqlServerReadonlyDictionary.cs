using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace DatabaseDictionary.SqlServer
{
	public class SqlServerReadonlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
	{
		private readonly IDbConnection _connection;
		private readonly string _table;
		private readonly string _schema;
		private readonly string _keyColumn;
		private readonly string _valueColumn;
		private readonly SqlServerAdapter _adapter;

		public SqlServerReadonlyDictionary(string connectionString, string table, string schema = null, IColumnMapper columnMapper = null)
			: this(new SqlConnection(connectionString), table, schema, columnMapper)
		{ }

		public SqlServerReadonlyDictionary(IDbConnection connection, string table, string schema = null, IColumnMapper columnMapper = null)
		{
			_connection = connection;
			_table = table;
			_schema = schema;

			if (columnMapper == null)
			{
				columnMapper = new DefaultColumnMapper(connection, table, schema);
			}

			_keyColumn = columnMapper.GetKeyColumnName();
			_valueColumn = columnMapper.GetValueColumnName();
			_adapter = new SqlServerAdapter(_connection, _keyColumn, _valueColumn, _table, _schema);

			Keys = new List<TKey>();
			Values = new List<TValue>();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _adapter.GetAllRows<TKey, TValue>();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count { get; }

		public bool ContainsKey(TKey key)
		{
			var results = _adapter.GetAllRows<TKey, TValue>();
			return results.Any(x => x.Item1.Equals(key));
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var results = _adapter.GetAllRows<TKey, TValue>();
			var result = results.FirstOrDefault(x => x.Item1.Equals(key));
			if (result.CompareTo() is default)
			{
				value = default;
				return true;
			}

			value = default;
			return false;
		}

		public TValue this[TKey key] => throw new NotImplementedException();

		public IEnumerable<TKey> Keys { get; }
		public IEnumerable<TValue> Values { get; }
	}
}
