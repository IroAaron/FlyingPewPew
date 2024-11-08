using UnityEngine;

namespace Services
{
    public class InputService
    {
        private readonly GameInputSystem _gameInputSystem;

        public InputService(GameInputSystem gameInputSystem)
        {
            _gameInputSystem = gameInputSystem;

            _gameInputSystem.Player.Enable();
        }

        public bool FirePressed => _gameInputSystem.Player.Attack.WasPressedThisFrame();
        public bool IsMoving => _gameInputSystem.Player.Move.IsInProgress();
        public Vector2 Moving => _gameInputSystem.Player.Move.ReadValue<Vector2>();
        public bool IsStopped => _gameInputSystem.Player.Move.WasReleasedThisFrame();
    }
}