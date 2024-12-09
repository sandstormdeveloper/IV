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
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        public override void Handle()
        {

        }

        public override void Exit() 
        { 
            
        }
        
    }
}

