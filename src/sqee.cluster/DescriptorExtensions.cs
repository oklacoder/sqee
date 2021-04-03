using System;
using Nest;

namespace sqee.cluster
{
    public static partial class DescriptorExtensions
    {
        public static CreateIndexDescriptor Extend(
            this CreateIndexDescriptor descriptor,
            ICollectionConfig config,
            Type type)
        {
            return CreateIndexDescriptorExtender.Extend(config, type, descriptor);
        }
    }

}
