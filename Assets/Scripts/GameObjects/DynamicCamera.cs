using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Client
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class DynamicCamera : EcsMonoBehaviour
    {
        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);

            ref var camera = ref ecsWorld.GetPool<CameraComponent>().Add(entity.index);
            camera.value = GetComponent<UnityEngine.Camera>();
            ecsWorld.GetPool<PositionAtTarget>().Add(entity.index);
            
            ref var target = ref  ecsWorld.GetPool<Target>().Add(entity.index);
            foreach (var player in  ecsWorld.Filter<PlayerTag>().End()){
                var playerEntity = new Entity() {
                    index = player, gen = ecsWorld.GetEntityGen(player)
                };
                target.value = playerEntity;
                break;
            }
        }

    }
}