using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Navegacion.State
{
    public class MainMenu : Navegacion.AMenuState
    {
        private UIController uiController;

        public MainMenu(UIController uiController)
        {
            this.uiController = uiController;
        }
        
        public override void Enter()
        {
            Debug.Log("Entrando al Main Menu");
            uiController.LoadScene("MainMenu");
        }

        public override void Exit() 
        {
            Debug.Log("Saliendo del Main Menu");
        }



    }
}

