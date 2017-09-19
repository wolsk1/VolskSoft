namespace VolskNet.ExTools
{
    using OfficeOpenXml;
    using OfficeOpenXml.DataValidation;
    using OfficeOpenXml.DataValidation.Formulas.Contracts;
    using System;

    public class WholeNumberValidation : BaseValidation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WholeNumberValidation"/> class.
        /// </summary>
        /// <param name="columnNumber">The column number.</param>
        /// <param name="firstValidationFormula">The first validation formula.</param>
        /// <param name="secondValidationFormula">The second validation formula.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">{nameof(columnNumber)}</exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public WholeNumberValidation(
           int columnNumber,
           IntegerValidationFormula firstValidationFormula,
           IntegerValidationFormula secondValidationFormula)
        {
            if (columnNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(columnNumber), columnNumber, $"{nameof(columnNumber)} value must be greater than 0");
            }

            if (firstValidationFormula == null)
            {
                throw new ArgumentNullException(nameof(firstValidationFormula));
            }

            ColumnNumber = columnNumber;
            FirstFormula = firstValidationFormula;
            SecondFormula = secondValidationFormula;
            Operator = ExcelDataValidationOperator.between;
        }

        public override string Address => ExcelCellBase.GetAddress(RowNumber, ColumnNumber, ExcelPackage.MaxRows, ColumnNumber);

        public override DataValidationType ValidationType => DataValidationType.WholeNumber;

        public override ExcelDataValidationOperator Operator { get; }

        public override int ColumnNumber { get; }

        public IExcelDataValidationFormulaInt FirstFormula { get; set; }

        public IExcelDataValidationFormulaInt SecondFormula { get; set; }
    }
}