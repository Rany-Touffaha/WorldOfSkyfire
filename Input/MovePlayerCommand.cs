using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public class MovePlayerCommand : ICommand
    {
        private readonly Player _player;
        private Vector2 _direction;
        private const float Speed = 2f;

        public MovePlayerCommand(Player player, Vector2 direction)
        {
            _player = player;
            _direction = direction;
        }

        public void Execute() => _player.Position += _direction * Speed;
    }
}
