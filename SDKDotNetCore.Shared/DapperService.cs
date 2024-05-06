namespace SDKDotNetCore.Shared
{
    public class DapperService
    {
        public readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
