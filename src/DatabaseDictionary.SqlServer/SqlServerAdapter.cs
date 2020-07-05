using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace DatabaseDictionary.SqlServer
{
	public class SqlServerAdapter
	{
		private readonly IDbConnection _connection;
		private readonly string _keyColumn;
		private readonly string _valueColumn;
		private readonly string _table;
		private readonly string _withSchema;

		public SqlServerAdapter(IDbConnection connection, string keyColumn, string valueColumn, string table, string schema = null)
		{
			_connection = connection;
			_keyColumn = keyColumn;
			_valueColumn = valueColumn;
			_table = table;
			_withSchema = schema == null
				? ""
				: $"[{schema}].";
		}

		public IEnumerable<ValueTuple<TKey, TValue>> ReadAllRows<TKey, TValue>()
		{
			string sql = $"select [{_keyColumn}], [{_valueColumn}] from {_withSchema}[{_table}]";
			var result = _connection.Query<SqlServerValueTuple<TKey, TValue>>(sql);

			return result.Select(x => (x.Key, x.Value));
		}

		public void WriteAllRows<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
		{
			var items = new
			{
				keys = dictionary.Keys,
				values = dictionary.Values
			};

			string sql = $"delete from {_withSchema}[{_table}];";
			_connection.Execute(sql);

			sql = $"insert into {_withSchema}[{_table}] ([{_keyColumn}], [{_valueColumn}]) values (@keys, @values)";
			_connection.Execute(sql, items);
		}
	}
}
