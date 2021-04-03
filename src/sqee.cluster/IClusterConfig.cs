namespace sqee.cluster
{
    public interface IClusterConfig
    {
        IClusterConnection Connection { get; }
        string ScopeId { get; }
    }

}
