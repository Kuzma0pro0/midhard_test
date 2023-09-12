using UnityEngine;

namespace Midhard_TEST.ECS.Configs
{
    [CreateAssetMenu(menuName = "Configs/Init Config", fileName = "Init_Config")]
    public class InitConfig : ScriptableObject
    {
        public GameTile prefabTile;

        public Vector2Int Size;
    }
}
