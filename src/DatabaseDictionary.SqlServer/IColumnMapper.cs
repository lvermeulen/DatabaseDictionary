using System.Data;

namespace DatabaseDictionary.SqlServer
{
	public interface IColumnMapper
	{
		string GetKeyColumnName();
		string GetValueColumnName();
	}
}