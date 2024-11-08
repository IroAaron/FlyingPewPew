using CreatedEventArgs;
using Ships;
using System;

namespace Core
{
    public interface IDamagable
    {
        public void ReceiveDamage(DefenceEventArgs defenceEventArgs);
        public event EventHandler<IDamagable> ObjectDied;
    }
}