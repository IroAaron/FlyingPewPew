using CreatedEventArgs;
using Services;
using UnityEngine;

namespace Ships
{
    public class PlayerShip : Ship
    {
        private InputService _inputService;

        public override void Start()
        {
            base.Start();
            _inputService = new InputService(new GameInputSystem());
        }

        public void FixedUpdate()
        {
            if (_inputService.IsMoving)
            {
                OnShipMoved(new MoveEventArgs(_inputService.Moving, MoveSpeed));
            }
        }

        private void Update()
        {
            if (_inputService.FirePressed)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;
                OnShipFired(transform.position, direction);
            }

            if (_inputService.IsStopped)
            {
                OnShipStoped();
            }
        }
    }
}