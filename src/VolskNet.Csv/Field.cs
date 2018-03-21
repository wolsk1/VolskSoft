namespace VolskSoft.Bibliotheca.Csv
{
    public class Field
    {
        public Field(object value)
        {
            Value = value;
        }

        public string Column { get; set; }
        public object Value { get; set; }
    }
}