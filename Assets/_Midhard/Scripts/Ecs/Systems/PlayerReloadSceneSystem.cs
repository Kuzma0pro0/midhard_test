using Leopotam.EcsLite;
using Midhard_TEST.ECS.Configs;
using UnityEngine;

namespace Midhard_TEST.ECS.Systems
{
    sealed class PlayerReloadSceneSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var gameData = systems.GetShared<GameData>();

            if (Input.GetKeyDown(KeyCode.R))
            {
                gameData.SceneService.ReloadScene();
            }
        }
    }
}

