﻿namespace sqee.cluster
{
    public class ClusterConnection :
        IClusterConnection
    {
        public ClusterConnection(
            string clusterUrl,
            string username,
            string password,
            ISqeeElasticsearchSerializer serializer = null)
        {
            ClusterUrl = clusterUrl;
            Username = username;
            Password = password;
            Serializer = serializer;
        }

        public string ClusterUrl { get; }

        public string Username { get; }

        public string Password { get; }
        public ISqeeElasticsearchSerializer Serializer { get; }
    }

}
