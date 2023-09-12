using Leopotam.EcsLite;
using Midhard_TEST.ECS.Components;
using Midhard_TEST.ECS.Tags;

namespace Midhard_TEST.ECS.Systems
{
    sealed class MovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<ModelComponent>().Inc<MovableTag>().Inc<WorldPositionComponent>().End();
            var modelPool = systems.GetWorld().GetPool<ModelComponent>();
            var worldPositionPool = systems.GetWorld().GetPool<WorldPositionComponent>();

            foreach (var i in filter)
            {
                ref var modelComponent = ref modelPool.Get(i);
                ref var worldPosComponent = ref worldPositionPool.Get(i);

                ref var position = ref worldPosComponent.Position;
                ref var transform = ref modelComponent.Transform;

                transform.position = position;
            }

        }
    }
}

