using CreatedEventArgs;
using System;

namespace Core
{
    public interface IMoverController
    {
        public event EventHandler<MoveEventArgs> OnControllerMoved;
        public event EventHandler OnControllerStoped;
    }
}