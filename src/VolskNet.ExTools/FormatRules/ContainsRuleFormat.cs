namespace VolskSoft.ExTools
{
    using OfficeOpenXml.ConditionalFormatting.Contracts;

    public class ContainsRuleFormat : BaseRuleFormat, IExcelConditionalFormattingWithText
    {
        public ContainsRuleFormat(int columnNumber, string text, System.Drawing.Color? backgroundColor = null)
            :base(columnNumber, backgroundColor)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}