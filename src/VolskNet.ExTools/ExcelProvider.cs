namespace VolskNet.ExTools
{
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ExcelProvider
    {
        /// <summary>
        /// Loads the rows.
        /// </summary>
        /// <typeparam name="TRowData">The type of the row data.</typeparam>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="dataCollection">The data collection.</param>
        /// <param name="printHeaders">if set to <c>true</c> [print headers].</param>
        /// <exception cref="System.ArgumentNullException">worksheet
        /// or
        /// dataCollection</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void LoadDataFiltered<TRowData>(ExcelWorksheet worksheet, ICollection<TRowData> dataCollection, bool printHeaders = true)
        {
            if (worksheet == null)
            {
                throw new ArgumentNullException(nameof(worksheet));
            }
            if (dataCollection == null)
            {
                throw new ArgumentNullException(nameof(dataCollection));
            }

            worksheet.Cells.LoadFromCollectionFiltered(dataCollection);

            if (!printHeaders)
            {
                return;
            }

            var headerNames = ExcelUtils.GetClassAsHeaders(typeof(TRowData))
                .ToList();
            worksheet.InsertRow(1, 1);
            AddWorksheetHeaders(worksheet, headerNames);
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="dynamicCollection">The dynamic collection.</param>
        /// <exception cref="ArgumentNullException">
        /// worksheet
        /// or
        /// dynamicCollection
        /// </exception>
        public static void LoadData(ExcelWorksheet worksheet, List<DynamicRow> dynamicCollection)
        {
            if (worksheet == null)
            {
                throw new ArgumentNullException(nameof(worksheet));
            }
            if (dynamicCollection == null)
            {
                throw new ArgumentNullException(nameof(dynamicCollection));
            }

            var firstRow = dynamicCollection.FirstOrDefault();

            if (firstRow == null)
            {
                return;
            }

            var memberNames = firstRow.GetDynamicMemberNames()
                .ToList();
            var rowId = Constants.MIN_ROW_ID;
            var rowList = dynamicCollection.Select(dynamicRow => dynamicRow.Properties.Select(p => p.Value)
                .ToList());

            foreach (var cellValues in rowList)
            {
                SetDynamicRow(rowId, memberNames.Count, worksheet, cellValues);
                rowId++;
            }
            SetHeaders(worksheet, memberNames);

            //for (var rowId = Constants.MIN_ROW_ID; rowId < dynamicCollection.Count; rowId++)
            //{
            //    var cellValues = dynamicCollection[rowId].Properties.Select(p => p.Value)
            //        .ToList();
            //    SetDynamicRow(rowId, worksheet, cellValues);
            //}
        }

        private static void SetHeaders(ExcelWorksheet worksheet, IReadOnlyList<object> cellValues)
        {
            worksheet.InsertRow(Constants.MIN_ROW_ID, Constants.MIN_COLUMN_ID);
            var columnId = Constants.MIN_COLUMN_ID;
            foreach (var cellValue in cellValues)
            {
                worksheet.Cells[Constants.MIN_ROW_ID, columnId].Value = cellValue;
                columnId++;
            }
        }

        private static void SetDynamicRow(int rowId, int columnCount, ExcelWorksheet worksheet, IReadOnlyList<object> cellValues)
        {
            //var columnId = Constants.MIN_COLUMN_ID;
            //if (worksheet.Dimension == null)
            //{
            //    return;
            //}

            //var columnCount = worksheet.Dimension.Columns;
            for (var columnId = Constants.MIN_COLUMN_ID; columnId <= columnCount; columnId++)
            {
                worksheet.Cells[rowId, columnId].Value = cellValues[columnId - 1];
            }
            //for (var columnId = Constants.MIN_COLUMN_ID; columnId < cellValues.Count; columnId++)
            //{
            //    worksheet.Cells[rowId, columnId].Value = cellValues[columnId - 1];
            //}
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="dataRows">The data rows.</param>
        /// <exception cref="ArgumentNullException">
        /// worksheet
        /// or
        /// dataRows
        /// </exception>
        public static void LoadData(ExcelWorksheet worksheet, IList<DataRow> dataRows)
        {
            if (worksheet == null)
            {
                throw new ArgumentNullException(nameof(worksheet));
            }
            if (dataRows == null)
            {
                throw new ArgumentNullException(nameof(dataRows));
            }

            var values = dataRows.Select(r => 
                r.Select(c => c)
                .ToArray()).ToList();
            
            worksheet.Cells.LoadFromArrays(values);

            if (!dataRows.Any())
            {
                return;
            }
            worksheet.InsertRow(1, 1);
            var headers = dataRows[0].ColumnNames.ToList();
            AddWorksheetHeaders(worksheet, headers);
        }

        /// <summary>
        /// Extracts the data rows.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="worksheet">The worksheet.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IList<T> ExtractData<T>(ExcelWorksheet worksheet) where T : struct
        {
            if (worksheet == null)
            {
                throw new ArgumentNullException(nameof(worksheet));
            }

            var rows = ExtractDataRows(worksheet);

            var rowList = rows as IList<DataRow> ?? rows.ToList();

            return rowList
                .Select(r => (T)Activator.CreateInstance(typeof(T), r))
                .ToList();
        }

        /// <summary>
        /// Extracts the data rows.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">worksheet</exception>
        public static List<DynamicRow> ExtractData(ExcelWorksheet worksheet)
        {
            if (worksheet == null)
            {
                throw new ArgumentNullException(nameof(worksheet));
            }

            var rows = ExtractDynamicRows(worksheet);

            return rows as List<DynamicRow> ?? rows.ToList();
        }

        private static void AddWorksheetHeaders(ExcelWorksheet worksheet, IList<string> columnNames)
        {
            if (columnNames == null)
            {
                throw new ArgumentNullException(nameof(columnNames));
            }
            var columnId = Constants.MIN_COLUMN_ID;
            foreach (var columnName in columnNames)
            {
                worksheet.Cells[1, columnId].Value = columnName;
                columnId++;
            }
        }
        
        private static IEnumerable<DataRow> ExtractDataRows(ExcelWorksheet worksheet, bool hasHeaders = true)
        {
            if (worksheet.Dimension == null)
            {
                throw new ArgumentOutOfRangeException(nameof(worksheet.Dimension));
            }

            var coulmnIndexMapping = GetColumnCellMapping(worksheet, hasHeaders);
            var fromRow = hasHeaders ? 2 : Constants.MIN_ROW_ID;

            for (var rowId = fromRow; rowId <= worksheet.Dimension.Rows; rowId++)
            {
                yield return new DataRow(ExtractDataCells(worksheet, rowId), coulmnIndexMapping);
            }

        }

        private static IEnumerable<DynamicRow> ExtractDynamicRows(ExcelWorksheet worksheet)
        {
            if (worksheet.Dimension == null)
            {
                throw new ArgumentOutOfRangeException(nameof(worksheet.Dimension));
            }

            const int fromRow = 2;
            var headerMap = GetHeaderMap(worksheet);
            for (var rowId = fromRow; rowId <= worksheet.Dimension.Rows; rowId++)
            {
                var cellValues = ExtractCellValues(worksheet, rowId);
                var cellValueList = cellValues.ToList();
                var columnValueMap = new Dictionary<string, object>();
                for (var i = 0; i < cellValueList.Count; i++)
                {
                    columnValueMap.Add(headerMap[i], cellValueList[i]);
                }

                yield return new DynamicRow(columnValueMap);
            }
        }

        private static IEnumerable<object> ExtractCellValues(ExcelWorksheet worksheet, int rowId)
        {
            if (rowId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(rowId));
            }

            for (var colId = Constants.MIN_COLUMN_ID; colId <= worksheet.Dimension.Columns; colId++)
            {
                yield return worksheet.Cells[rowId, colId].Value;
            }
        }

        private static IEnumerable<DataCell> ExtractDataCells(ExcelWorksheet worksheet, int rowId)
        {
            if (rowId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(rowId));
            }

            for (var colId = Constants.MIN_COLUMN_ID; colId <= worksheet.Dimension.Columns; colId++)
            {
                yield return new DataCell(worksheet.Cells[rowId, colId].Value);
            }
        }
       
        private static IEnumerable<string> GetHeadings(ExcelWorksheet worksheet)
        {
            if (worksheet.Dimension == null)
            {
                throw new ArgumentOutOfRangeException(nameof(worksheet.Dimension));
            }

            for (var columnId = 1; columnId <= worksheet.Dimension.Columns; columnId++)
            {
                var cellValue = worksheet.Cells[1, columnId].Value;
                yield return cellValue?.ToString() ?? columnId.ToString();
            }
        }
        
        private static Dictionary<string, int> GetColumnCellMapping(ExcelWorksheet worksheet, bool hasHeaders = true)
        {
            var headings = GetHeadings(worksheet);
            var coulumnIndexMapping = new Dictionary<string, int>();
            var headerList = headings.ToList();

            for (var i = 0; i < headerList.Count; i++)
            {
                coulumnIndexMapping.Add(hasHeaders ? headerList[i].RemoveWhiteSpaces() : i.ToString(), i);
            }

            return coulumnIndexMapping;
        }

        private static Dictionary<int, string> GetHeaderMap(ExcelWorksheet worksheet)
        {
            var headings = GetHeadings(worksheet);
            var coulumnIndexMapping = new Dictionary<int, string>();
            var headerList = headings.ToList();

            for (var i = 0; i < headerList.Count; i++)
            {
                coulumnIndexMapping.Add(i, headerList[i].RemoveWhiteSpaces());
            }

            return coulumnIndexMapping;
        }
    }
}