namespace VolskSoft.Bibliotheca
{
    using System;
    using System.ComponentModel;
    using System.Data;

    public class DataTableUtils
    {
        /// <summary>
        /// Creates table with columns from type T
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <returns>DataTable</returns>
        public static DataTable CreateTable<T>()
        {
            var entityType = typeof (T);
            var table = new DataTable(entityType.Name);
            var properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                var columnType = prop.PropertyType;
                if (columnType.IsGenericType
                    && columnType.GetGenericTypeDefinition() == typeof (Nullable<>))
                {
                    columnType = columnType.GetGenericArguments()[0];
                }

                table.Columns.Add(prop.Name, columnType);
            }

            return table;
        }

        /// <summary>
        /// Creates item with type T from DataRow
        /// </summary>
        /// <typeparam name="T">type of item to create</typeparam>
        /// <param name="row">from which data container fill item</param>
        /// <returns>Created item of type T</returns>
        public static T CreateItem<T>(DataRow row)
        {
            var obj = default(T);
            if (row == null) return obj;
            obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                var prop = obj.GetType()
                    .GetProperty(column.ColumnName);
                var value = row[column.ColumnName];
                if (prop != null) prop.SetValue(obj, value, null);
            }

            return obj;
        }
    }
}