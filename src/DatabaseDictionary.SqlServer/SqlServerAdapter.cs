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
		private readonly string _keyColumnName;
		private readonly string _valueColumnName;
		private readonly string _table;
		private readonly string _withSchema;

		public SqlServerAdapter(IDbConnection connection, string keyColumnName, string valueColumnName, string table, string schema = null)
		{
			_connection = connection;
			_keyColumnName = keyColumnName;
			_valueColumnName = valueColumnName;
			_table = table;
			_withSchema = schema == null
				? ""
				: $"[{schema}].";
		}

		public IEnumerable<ValueTuple<TKey, TValue>> GetAllRows<TKey, TValue>()
		{
			string sql = $"select {_keyColumnName}, {_valueColumnName} from {_withSchema}[{_table}]";
			var result = _connection.Query<SqlServerValueTuple<TKey, TValue>>(sql);

			return result.Select(x => (x.Key, x.Value));
		}
	}
}
