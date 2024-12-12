using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Pause : Navegacion.AMenuState
    {
        private UIController uiController;


        public Pause(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al menu de Pausa");

            uiController.pauseInstance.SetActive(true); //Se activa el menu de pausa
            uiController.panel_pause.SetActive(true); //Se activa el panel

            Time.timeScale = 0f; //Se detiene el juego
            
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del menu de Pausa");

            uiController.pauseInstance.SetActive(false); //Se oculta el menu de pausa
            uiController.panel_pause.SetActive(false); //Se oculta el panel 

            Time.timeScale = 1f; //Se reanuda el juego
            
        }

       
    }
}

