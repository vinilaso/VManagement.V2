using VManagement.Commons.Interfaces;
using VManagement.Database;
using VManagement.Database.SqlClauses;

namespace VManagement.Core.Entities
{
    public class EntityAggregation<TSource, TTarget> where TSource : IEntity, new() where TTarget : IEntity, new()
    {
        public long FatherId { get; set; }
        public List<TTarget> Instances { get; set; } = new List<TTarget>();

        public EntityAggregation(long fatherId)
        {
            FatherId = fatherId;
            GetInstances(fatherId);
        }

        private void GetInstances(long fatherId)
        {
            var restriction = new Restriction($"A.SOURCE = @pKEY");
            restriction.Parameters.Add("pKEY", fatherId);

            var instances = EntityController<TSource>.GetMany(restriction);

            foreach (var instance in instances)
            {
                Instances.Add(EntityController<TTarget>.GetFirstOrDefault(instance.Id)!);
            }
            
        }
    }
}
