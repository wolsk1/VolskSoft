namespace VolskNet.ExTools
{
    using OfficeOpenXml;
    using OfficeOpenXml.ConditionalFormatting.Contracts;
    using System;

    public static class WorksheetProvider
    {
        /// <summary>
        /// Adds the duplicate value rule.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="formatSettings">The format settings.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// worksheet
        /// or
        /// formatSettings
        /// </exception>
        public static IExcelConditionalFormattingDuplicateValues AddDuplicateValueRule(this ExcelWorksheet worksheet, DuplicateRuleFormat formatSettings)
        {
            if (worksheet == null)
            {
                throw new ArgumentNullException(nameof(worksheet));
            }

            if (formatSettings == null)
            {
                throw new ArgumentNullException(nameof(formatSettings));
            }

            var excelAddress = worksheet.GetCellRange(formatSettings.Address);
            var formatRule = worksheet.ConditionalFormatting.AddDuplicateValues(excelAddress);
            formatRule.Style.Fill.BackgroundColor.Color = formatSettings.BackgroundColor;

            return formatRule;
        }

        /// <summary>
        /// Adds the contains rule.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="formatSettings">The format settings.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// worksheet
        /// or
        /// formatSettings
        /// </exception>
        public static IExcelConditionalFormattingContainsText AddContainsRule(this ExcelWorksheet worksheet, ContainsRuleFormat formatSettings)
        {
            if (worksheet == null)
            {
                throw new ArgumentNullException(nameof(worksheet));
            }

            if (formatSettings == null)
            {
                throw new ArgumentNullException(nameof(formatSettings));
            }

            var excelAddress = worksheet.GetCellRange(formatSettings.Address);
            var formatRule = worksheet.ConditionalFormatting.AddContainsText(excelAddress);
            formatRule.Style.Fill.BackgroundColor.Color = formatSettings.BackgroundColor;
            formatRule.Text = formatSettings.Text;

            return formatRule;
        }

        private static ExcelRange GetCellRange(this ExcelWorksheet worksheet, string rangeAddress)
        {
            return worksheet.Cells[rangeAddress];
        }
    }
}