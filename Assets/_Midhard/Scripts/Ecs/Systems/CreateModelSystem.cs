using Leopotam.EcsLite;
using Midhard_TEST.ECS.Components;
using Midhard_TEST.ECS.Tags;
using UnityEngine;

namespace Midhard_TEST.ECS.Systems
{
    sealed class CreateModelSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var modelEntity = world.NewEntity();

            var movablePool = world.GetPool<MovableTag>();
            movablePool.Add(modelEntity);

            var worldPosPool = world.GetPool<WorldPositionComponent>();
            worldPosPool.Add(modelEntity);

            var modelPool = world.GetPool<ModelComponent>();
            modelPool.Add(modelEntity);
            ref var modelComponent = ref modelPool.Get(modelEntity);

            var model = GameObject.CreatePrimitive(PrimitiveType.Cube);
            model.layer = 2;
            modelComponent.Transform = model.transform;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<MovableTag>().End();

            if (filter.GetEntitiesCount() > 0) return;

            var modelEntity = world.NewEntity();

            var movablePool = world.GetPool<MovableTag>();
            movablePool.Add(modelEntity);

            var worldPosPool = world.GetPool<WorldPositionComponent>();
            worldPosPool.Add(modelEntity);

            var modelPool = world.GetPool<ModelComponent>();
            modelPool.Add(modelEntity);
            ref var modelComponent = ref modelPool.Get(modelEntity);

            var model = GameObject.CreatePrimitive(PrimitiveType.Cube);
            model.layer = 2;
            modelComponent.Transform = model.transform;
        }
    }
}

