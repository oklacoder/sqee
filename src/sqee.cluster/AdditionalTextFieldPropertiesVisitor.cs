﻿using System.Reflection;
using Nest;

namespace sqee.cluster
{
    public class AdditionalTextFieldPropertiesVisitor :
     NoopPropertyVisitor
    {
        public override void Visit(
            ITextProperty type,
            PropertyInfo propertyInfo,
            ElasticsearchPropertyAttributeBase attribute)
        {
            var sort =
                new KeywordPropertyDescriptor<string>()
                    .Name(Constants.Fields.SortField)
                    .Normalizer(Constants.Normalizers.Lowercase);

            type.Fields.Add(Constants.Fields.SortField, sort);

            base.Visit(type, propertyInfo, attribute);
        }
    }

}
