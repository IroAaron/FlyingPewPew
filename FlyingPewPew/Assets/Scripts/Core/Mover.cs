using CreatedEventArgs;
using System;
using UnityEngine;

namespace Core
{
    public class Mover
    {
        private IMoverController _controller;
        private Rigidbody2D _rigidbody;

        public Mover(IMoverController controller, Rigidbody2D rigidbody)
        {
            _controller = controller;
            _rigidbody = rigidbody;

            _controller.OnControllerMoved += OnControllerMoved;
            _controller.OnControllerStoped += OnControllerStoped;
        }

        private void OnControllerStoped(object sender, EventArgs e)
        {
            _rigidbody.linearVelocity = new Vector2(0,0);
        }

        private void OnControllerMoved(object sender, MoveEventArgs e)
        {
            _rigidbody.linearVelocity = e.Direction * e.MoveSpeed;
        }
    }
}