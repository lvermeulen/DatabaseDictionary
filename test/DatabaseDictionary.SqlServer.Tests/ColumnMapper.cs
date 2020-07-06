namespace DatabaseDictionary.SqlServer.Tests
{
	public class ColumnMapper : IColumnMapper
	{
		public string GetKeyColumnName()
		{
			return "keycolumn";
		}

		public string GetValueColumnName()
		{
			return "valuecolumn";
		}
	}
}
