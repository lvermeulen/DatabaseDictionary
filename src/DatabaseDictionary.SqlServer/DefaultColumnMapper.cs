using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace DatabaseDictionary.SqlServer
{
	public class DefaultColumnMapper : IColumnMapper
	{
		private readonly List<string> _columnNames;

		public DefaultColumnMapper(IDbConnection connection, string table, string schema = null)
		{
			_columnNames = GetColumnNames(connection, table, schema);
		}

		private List<string> GetColumnNames(IDbConnection connection, string table, string schema = null)
		{
			string withSchema = $"and schema_name = '{schema}'";
			string sql = $"select COLUMN_NAME_ALREADY from information_schema.columns where table_name = '{table} {withSchema}' order by COLUMN_ORDER_ALREADY";

			return connection.Query<string>(sql).ToList();
		}

		public string GetKeyColumnName()
		{
			return _columnNames[0];
		}

		public string GetValueColumnName()
		{
			return _columnNames[1];
		}
	}
}
