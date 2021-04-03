using System;

namespace sqee.cluster
{
    public interface ICollection
    {
        string CollectionName { get; }
        Type DocumentType { get; }
        ICollectionSchema Schema { get; }
        bool ForceRefreshOnDocumentCommit { get; }

        void SetSchema(ICollectionSchema schema);
    }

}
