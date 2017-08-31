namespace VolskNet.Csv
{
    using LumenWorks.Framework.IO.Csv;
    using System;
    using System.IO;
    using System.Collections.Generic;

    public class CsvProvider
    {
        /// <summary>
        /// Reads the CSV.
        /// </summary>
        /// <param name="readerSettings">The reader settings.</param>
        /// <returns>List of records composed of fields</returns>
        public static IEnumerable<Field[]> ReadAllRecords(ReaderSettings readerSettings)
        {
            var records = new List<Field[]>();

            using (var reader = new StreamReader(readerSettings.FilePath))
            {
                using (var csv = new CsvReader(reader, readerSettings.HasHeaders, readerSettings.Delimiter))
                {
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;
                    var fieldCount = csv.FieldCount;
                    var headers = readerSettings.HasHeaders
                        ? csv.GetFieldHeaders() 
                        : null;
                   
                    while (csv.ReadNextRecord())
                    {
                        var record = new Field[fieldCount];
                        for (var i = 0; i < fieldCount; i++)
                        {
                            var field = new Field(csv[i]);

                            if (headers != null)
                            {
                                field.Column = headers[i];
                            }

                            record[i] = field;
                        }

                        records.Add(record);
                    }
                }
            }

            return records;
        }

        public static IEnumerable<TRecord> ReadAllRecords<TRecord>(ReaderSettings readerSettings)
        {
            var records = new List<TRecord>();

            using (var streamReader = new StreamReader(readerSettings.FilePath))
            {
                using (var csvReader = new CsvReader(streamReader, readerSettings.HasHeaders, readerSettings.Delimiter))
                {
                    csvReader.MissingFieldAction = MissingFieldAction.ReplaceByNull;
                    var fieldCount = csvReader.FieldCount;
                    var headers = readerSettings.HasHeaders
                        ? csvReader.GetFieldHeaders()
                        : null;

                    while (csvReader.ReadNextRecord())
                    {
                        records.Add(GetRecordObject<TRecord>(ExtractRecord(fieldCount, headers, csvReader)));
                    }
                }
            }

            return records;
        }

        /// <summary>
        /// Gets the record object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="fieldCollection">The field collection.</param>
        /// <returns>Record object</returns>
        public static TObject GetRecordObject<TObject>(Field[] fieldCollection)
        {
            var type = typeof(TObject);
            var recordObject = Activator.CreateInstance(type);

            foreach (var field in fieldCollection)
            {
                type.GetProperty(field.Column).SetValue(recordObject, field.Value);
            }

            return (TObject)recordObject;
        }

        private static Field[] ExtractRecord(int fieldCount, IReadOnlyList<string> headers, CsvReader reader)
        {
            var record = new Field[fieldCount];
            for (var i = 0; i < fieldCount; i++)
            {
                var field = new Field(reader[i]);

                if (headers != null)
                {
                    field.Column = headers[i];
                }

                record[i] = field;
            }

            return record;
        }
    }
}