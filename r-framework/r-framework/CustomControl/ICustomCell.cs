namespace r_framework.CustomControl
{
    public interface ICustomCell
    {
        object CellParsing(object formattedValue);

        object CellFormatting(object value);

        bool CellValidating(object formattedValue);
        void PreCellValidating();
        void PostCellValidating(bool cancel);
    }
}