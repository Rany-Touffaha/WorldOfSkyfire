using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public class MovePlayerCommand : ICommand
    {
        private readonly Player _player;
        private Vector2 _direction;

        public MovePlayerCommand(Player player, Vector2 direction)
        {
            _player = player;
            _direction = direction;
        }

        public void Execute()
        {
            _player.Position += _direction;
        }
    }
}
