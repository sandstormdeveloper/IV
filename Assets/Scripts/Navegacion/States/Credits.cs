using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Credits : Navegacion.AMenuState
    {
        private UIController uiController;

        public Credits(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando a los Créditos");
            uiController.loadScene("Credits");
        }

        public override void Exit()
        {
            Debug.Log("Saliendo de los Créditos");
        }

     
    }
}


