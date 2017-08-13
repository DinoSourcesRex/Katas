using System.Collections.Generic;
using System.Data;

namespace CustomerManagement.Api.Repositories
{
    internal static class DataTableConversionExtensions
    {
        public static DataTable StringToDataTable(this List<string> list)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Colours", typeof(string));

            foreach (var item in list)
            {
                dataTable.Rows.Add(item);
            }

            return dataTable;
        }
    }
}