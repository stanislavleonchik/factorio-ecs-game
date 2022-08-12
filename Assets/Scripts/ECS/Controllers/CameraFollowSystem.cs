using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public struct PositionAtTarget
    {
    }

    sealed class CameraFollowSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<PositionAtTarget, Target, TransformComponent, CameraComponent>> filter = default;
        readonly EcsWorldInject ecsWorld = default;
        readonly EcsPoolInject<Target> targetPool = default;
        readonly EcsPoolInject<CameraComponent> cameraPool = default;
        readonly EcsPoolInject<TransformComponent> transformPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var target = ref targetPool.Value.Get(entity);
                if (ecsWorld.Value.GetEntityGen(target.value.index) <= 0) continue; 
                
                var targetPos = (Vector2) transformPool.Value.Get(target.value.index).value.position;
                ref var cameraTransform = ref transformPool.Value.Get(entity);

                ref var camera = ref cameraPool.Value.Get(entity);
                cameraTransform.value.position = new Vector3(targetPos.x, targetPos.y, cameraTransform.value.position.z) ;
            }
        }
    }
}