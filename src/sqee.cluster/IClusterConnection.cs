namespace sqee.cluster
{
    public interface IClusterConnection
    {
        string ClusterUrl { get; }
        string Username { get; }
        string Password { get; }
        ISqeeElasticsearchSerializer Serializer { get; }
    }

}
