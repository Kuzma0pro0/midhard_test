using Leopotam.EcsLite;
using Midhard_TEST.ECS.Events;
using Midhard_TEST.ECS.Tags;
using UnityEngine;

namespace Midhard_TEST.ECS.Systems
{
    sealed class PlayerDeploySendEventSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<MovableTag>().End();
            var deployPool = systems.GetWorld().GetPool<DeployEvent>();

            if (!Input.GetMouseButtonDown(1)) return;

            foreach (var i in filter)
            {
                var deploy = systems.GetWorld().NewEntity();
                deployPool.Add(deploy);
            }
        }
    }
}
