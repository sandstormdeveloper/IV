using Navegacion.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IState = Navegacion.IState;

namespace Navegacion
{
    public class UIController : MonoBehaviour
    {
        private IState currentState;

        private void Start()
        {
            setState(new MainMenu(this));
        }

        private void setState(IState state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = state;
            currentState.Enter();
        }

        private void Update()
        {
            currentState.Handle();
        }
    }

    

}
