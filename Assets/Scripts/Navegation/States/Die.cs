using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navegacion.State
{
    public class Die : Navegacion.AMenuState
    {
        private UIController uiController;


        public Die(UIController uiController)
        {
            this.uiController = uiController;
        }

        public override void Enter()
        {
            Debug.Log("Entrando al menu de muerte del jugador");

            uiController.dieInstance.SetActive(true); //Se activa el menu de pausa
            uiController.panel_background.SetActive(true); //Se activa el panel

            Time.timeScale = 0f; //Se detiene el juego

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del menu de muerte del jugador");

            uiController.dieInstance.SetActive(false); //Se oculta el menu de pausa
            uiController.panel_background.SetActive(false); //Se oculta el panel 

            Time.timeScale = 1f; //Se reanuda el juego

        }
    }
}


