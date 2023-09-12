using Leopotam.EcsLite;
using Midhard_TEST.ECS.Components;
using Midhard_TEST.ECS.Configs;
using Midhard_TEST.ECS.Events;
using Midhard_TEST.ECS.Tags;

namespace Midhard_TEST.ECS.Systems
{
    sealed class DeploySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<MovableTag>().Inc<ModelComponent>().End();
            var deployFilter = systems.GetWorld().Filter<DeployEvent>().End();

            var modelPool = systems.GetWorld().GetPool<ModelComponent>();
            var worldPositionPool = systems.GetWorld().GetPool<WorldPositionComponent>();
            var tilePool = systems.GetWorld().GetPool<GameTileComponent>();

            var gameData = systems.GetShared<GameData>();

            foreach (var i in deployFilter)
            {
                systems.GetWorld().DelEntity(i);
                foreach (var modelEntity in filter)
                {
                    ref var modelComponent = ref modelPool.Get(modelEntity);
                    ref var worldPositionComponent = ref worldPositionPool.Get(modelEntity);
                    ref var position = ref worldPositionComponent.Position;

                    var dataTile = gameData.BoardService.GetTile(position);

                    if (!dataTile.Item2) return;

                    ref var tileComponent = ref tilePool.Get(dataTile.Item1);

                    if (tileComponent.IsBusy) return;

                    tileComponent.IsBusy = true;
                    modelComponent.Transform.position = tileComponent.Tile.transform.position;

                    systems.GetWorld().DelEntity(modelEntity);
                }
            }
        }
    }
}

