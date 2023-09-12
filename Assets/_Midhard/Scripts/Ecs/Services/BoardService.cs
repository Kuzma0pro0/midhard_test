using UnityEngine;

namespace Midhard_TEST.ECS.Services
{
    public sealed class BoardService
    {
        readonly int[] _tiles;
        readonly int _width;
        readonly int _height;

        public Vector2Int Size => new Vector2Int(_width, _height);

        public BoardService(int width, int height)
        {
            _tiles = new int[width * height];
            _width = width;
            _height = height;
        }

        public (int, bool) GetTile(Vector3 position)
        {
            int x = (int)(position.x + _width * 0.5f);
            int y = (int)(position.z + _height * 0.5f);
            if (x >= 0 && x < _width && y >= 0 && y < _height)
            {
                return GetTile(new Vector2Int(x, y));
            }

            return (0, false);
        }

        private (int, bool) GetTile(Vector2Int coords)
        {
            var entity = _tiles[_width * coords.y + coords.x] - 1;
            return (entity, entity >= 0);
        }

        public void AddTile(Vector2Int coords, int entity)
        {
            _tiles[_width * coords.y + coords.x] = entity + 1;
        }
    }
}

