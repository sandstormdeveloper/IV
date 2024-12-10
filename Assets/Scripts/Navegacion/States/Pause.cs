using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Pause : Navegacion.AMenuState
    {
        private UIController uiController;

        private GameObject pauseInstance;

        public Pause(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al menu de Pausa");

            uiController.pauseInstance.SetActive(true);
            uiController.panel_pause.SetActive(true);

            Time.timeScale = 0f; //Se detiene el juego
            
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del menu de Pausa");

            uiController.pauseInstance.SetActive(false);
            uiController.panel_pause.SetActive(false);

            Time.timeScale = 1f; //Se reanuda el juego
            
        }

       
    }
}

