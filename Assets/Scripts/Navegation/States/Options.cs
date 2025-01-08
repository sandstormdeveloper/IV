using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Options : Navegacion.AMenuState
    {
        private UIController uiController;

        public Options(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al menu de Opciones");
            uiController.LoadScene("Options");
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del menu de Opciones");
        }
    }
}


