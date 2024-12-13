using Navegacion.State;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using IState = Navegacion.IState;

namespace Navegacion
{
    public class UIController : MonoBehaviour
    {
        //VARIABLES 
        private IState currentState;

        public GameObject pauseInstance;
        public GameObject panel_pause;

        public GameObject panel_integrantes;
        public GameObject panel_assets;

        public Transform canvas;


        //Función para cargar las escenes
        public void loadScene(string scene)
        {
            Debug.Log("Cargando escena de " + scene);
            SceneManager.LoadScene(scene);
        }

        //Función para devolver el estado actual
        public IState getState()
        {
            return currentState;
        }

        //Función para actualizar el estado
        public void setState(IState state)
        {
            if (currentState != null)
            {
                currentState.Exit(); //Sale del estado actual
            }

            currentState = state; //Actualiza el estado nuevo
            currentState.Enter(); //Entra en el estado nuevo
        }

        /// BOTONES 

        //Botones Menu Principal
        public void OnStartButtonPressed() //Empezar el juego
        {
            setState(new Level(this));
        }

        public void OnCreditsButtonPressed() //Ir a la escena de creditos
        {
            setState(new Credits(this));
        }
        public void OnOptionsButtonPressed() //Ir a la escena de opciones
        {
            setState(new Options(this));
        }

        public void OnQuitButtonPressed() //Cerrar el juego
        {
            Debug.Log("Saliendo del juego");
            Application.Quit(); //Salir del juego
        }

        //Botones Menu Créditos
        public void OnExitCreditsButtonPressed() //Volver al menu principal desde el menu de creditos
        {
            setState(new MainMenu(this));
        }

        public void ShowPanelIntegrantes() //Mostrar panel de integrantes y tareas
        {
            panel_assets.SetActive(false); //Ocultarlo
            panel_integrantes.SetActive(true); //Mostrarlo
        }

        public void ShowPanelAssets() //Mostrar panel de assets
        {
            panel_integrantes.SetActive(false); //Ocultarlo
            panel_assets.SetActive(true); //Mostrarlo

        }

        //Botones Menu Pausa
        public void OnPauseButtonPressed() //Pausar el juego y mostrar el menu de pausa
        {
            setState(new Pause(this));
        }
        public void OnResumeButtonPressed() //Reanudar el nivel
        {
            pauseInstance.SetActive(false); //Se oculta el menu de pausa
            panel_pause.SetActive(false); //Se oculta el panel 
            Time.timeScale = 1f;
        }
        public void OnRestartButtonPressed() //Reiniciar el nivel
        {
            setState(new Level(this));
        }
        public void OnQuitLevelButtonPressed() //Voler al menu principal desde el nivel
        {
            setState(new MainMenu(this));
        }

        //Botones Menu Opciones       
        public void OnExitOptionsButtonPressed() //Volver al menu principal desde el menu de opciones
        {
            setState(new MainMenu(this));

        }


        //FUNCIONES MENU OPCIONES

    }



}
