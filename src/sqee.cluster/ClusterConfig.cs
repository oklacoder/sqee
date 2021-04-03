namespace sqee.cluster
{
    public class ClusterConfig :
        IClusterConfig
    {
        private ClusterConnection _connection;
        public IClusterConnection Connection => _connection;

        public string ScopeId { get; }

        public ClusterConfig(
            string scopeId,
            ClusterConnection connection)
        {
            ScopeId = scopeId;
            _connection = connection;
        }
    }

}
