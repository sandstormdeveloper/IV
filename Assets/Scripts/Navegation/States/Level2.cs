using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Level2 : Navegacion.AMenuState
    {
        private UIController uiController;

        public Level2(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al Nivel 2");
            uiController.LoadScene("Level2");
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del Nivel 2");
        }

      
        
    }
}

