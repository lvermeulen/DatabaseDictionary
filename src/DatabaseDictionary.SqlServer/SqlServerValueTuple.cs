﻿namespace DatabaseDictionary.SqlServer
{
	public class SqlServerValueTuple<TKey, TValue>
	{
		public TKey Key { get; set; }
		public TValue Value { get; set; }
	}
}
