namespace komyuter.data.Interfaces
{
    public interface IModelConfiguration
    {
        void SetPrimaryKeys();
        void SetForeignKeys();
        void SetRequiredFields();
        void SetColumnLengths();
        void SetIndices();
    }
}
