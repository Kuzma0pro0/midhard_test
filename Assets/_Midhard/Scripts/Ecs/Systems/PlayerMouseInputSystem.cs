using Leopotam.EcsLite;
using Midhard_TEST.ECS.Components;
using Midhard_TEST.ECS.Configs;
using UnityEngine;

namespace Midhard_TEST.ECS.Systems
{
    sealed class PlayerMouseInputSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<WorldPositionComponent>().End();
            var positionPool = systems.GetWorld().GetPool<WorldPositionComponent>();
            var gameData = systems.GetShared<GameData>();

            foreach (var i in filter)
            {
                ref var positionComponent = ref positionPool.Get(i);

                Ray ray = SetMousePos(gameData.Camera);
                positionComponent.Position = GetPosition(ray);
            }
        }

        private Ray SetMousePos(Camera camera)
        {
            return camera.ScreenPointToRay(Input.mousePosition);
        }

        private Vector3 GetPosition(Ray ray)
        {
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction);
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }

            return Vector3.zero;
        }
    }
}

