using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ImpactDamageSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<Health, ImpactDamage>> hitFilter = default;

        readonly EcsPoolInject<Health> healthPool = default;
        readonly EcsPoolInject<ImpactDamage> impactPool = default;
        readonly EcsPoolInject<AnimatorComponent> animatorPool = default;
        

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in hitFilter.Value)
            {
                ref var health = ref healthPool.Value.Get(entity);
                ref var impact = ref impactPool.Value.Get(entity);
                if (impact.hits.Count == 0) continue;
                if (animatorPool.Value.Has(entity))
                {
                    ref var anim = ref animatorPool.Value.Get(entity);
                    anim.value.SetTrigger("Damage");
                }
                foreach(var hit in impact.hits)
                    health.value -= hit;
                impact.hits.Clear();
            }
        }
    }
}
