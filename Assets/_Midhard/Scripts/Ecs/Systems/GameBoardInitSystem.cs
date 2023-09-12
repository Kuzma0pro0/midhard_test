using Leopotam.EcsLite;
using Midhard_TEST.ECS.Components;
using Midhard_TEST.ECS.Configs;
using UnityEngine;

namespace Midhard_TEST.ECS.Systems
{
    sealed class GameBoardInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var data = systems.GetShared<InitData>();

            var bs = data.BoardService;

            var tilesPool = world.GetPool<GameTileComponent>();

            data.parent.Find("Quad").localScale = new Vector3(bs.Size.x, bs.Size.y, 1f);

            Vector2 offset = new Vector2((bs.Size.x - 1) * 0.5f, (bs.Size.y - 1) * 0.5f);
            for (int i = 0, y = 0; y < bs.Size.y; y++)
            {
                for (int x = 0; x < bs.Size.x; x++, i++)
                {
                    var tileEntity = world.NewEntity();
                    tilesPool.Add(tileEntity);
                    ref var tilesComponent = ref tilesPool.Get(tileEntity);

                    GameTile tile = MonoBehaviour.Instantiate(data.prefabTile);
                    tile.transform.SetParent(data.parent, false);
                    tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                    tilesComponent.IsBusy = false;
                    tilesComponent.Tile = tile;

                    bs.AddTile(new Vector2Int(x, y), tileEntity);
                }
            }
        }
    }
}

