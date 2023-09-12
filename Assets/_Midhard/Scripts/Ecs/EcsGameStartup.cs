using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using Midhard_TEST.ECS.Configs;
using Midhard_TEST.ECS.Services;
using Midhard_TEST.ECS.Systems;
using UnityEngine;

namespace Midhard_TEST.ECS
{
    public class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;

        [SerializeField]
        private InitConfig _initConfig;
        [SerializeField]
        private Transform _parent;
        [SerializeField]
        private Camera _camera;

        private async UniTask Start()
        {
            await UniTask.WaitUntil(() => gameObject.scene.isLoaded);

            _world = new EcsWorld();

            var initData = new InitData();
            initData.parent = _parent;
            initData.prefabTile = _initConfig.prefabTile;
            initData.BoardService = Service<BoardService>.Set(new BoardService(_initConfig.Size.x, _initConfig.Size.y));

            _initSystems = new EcsSystems(_world, initData);

            AddInitSystems();

            _initSystems.Init();

            var gameData = new GameData();
            gameData.BoardService = Service<BoardService>.Get();
            gameData.Camera = _camera;
            gameData.SceneService = Service<SceneService>.Get(true);
            _updateSystems = new EcsSystems(_world, gameData);

            AddUpdateSystems();

            _updateSystems.Init();
        }

        private void AddInitSystems()
        {
            _initSystems
                .Add(new GameBoardInitSystem());
        }

        private void AddUpdateSystems()
        {
            _updateSystems
               .Add(new CreateModelSystem())
               .Add(new PlayerMouseInputSystem())
               .Add(new MovementSystem())
               .Add(new PlayerDeploySendEventSystem())
               .Add(new DeploySystem())
               .Add(new PlayerReloadSceneSystem());
        }

        private void Update()
        {
            _updateSystems.Run();
        }

        private void OnDestroy()
        {
            if (_initSystems == null || _updateSystems == null) return;

            _initSystems.Destroy();
            _initSystems = null;

            _updateSystems.Destroy();
            _updateSystems = null;

            _world.Destroy();
            _world = null;
        }
    }
}

