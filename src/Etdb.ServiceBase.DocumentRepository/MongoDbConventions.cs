using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
// ReSharper disable UnusedMember.Global

namespace Etdb.ServiceBase.DocumentRepository
{
    public static class MongoDbConventions
    {
        public static void UseImmutableConvention()
        {
            ConventionRegistry.Register(nameof(ImmutableTypeClassMapConvention), new ConventionPack
            {
                new ImmutableTypeClassMapConvention()
            }, type => true);
        }

        public static void UseCamelCaseConvention()
        {
            ConventionRegistry.Register(nameof(CamelCaseElementNameConvention),
                new ConventionPack {new CamelCaseElementNameConvention()},
                type => true);
        }
        
        public static void UseIgnoreNullValuesConvention()
        {
            ConventionRegistry.Register(nameof(IgnoreIfDefaultConvention),
                new ConventionPack {new IgnoreIfDefaultConvention(true)}, _ => true);
        }

        public static void UseEnumStringRepresentation()
        {
            ConventionRegistry.Register(nameof(EnumRepresentationConvention), new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            }, _ => true);
        }
    }
}