using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion
{
    public abstract class AMenuState : IState
    {
        public abstract void Enter();
        public abstract void Exit();

    }
}

