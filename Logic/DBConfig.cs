namespace Logic
{
    public class DBConfig
    {
        public static readonly string ConnectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=..\..\..\Database\db.mdb;";
        public static readonly string PathToFolder =
            @"..\..\..\Database";
    }
}
