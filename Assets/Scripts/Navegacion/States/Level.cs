using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Level : Navegacion.AMenuState
    {
        private UIController uiController;

        public Level(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al Nivel");
            uiController.loadScene("Level");
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del Nivel");
        }

      
        
    }
}

