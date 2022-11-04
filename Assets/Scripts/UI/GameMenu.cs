using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameMenu : MonoBehaviour
    {
        public event Action RestartingLevel;
        public event Action ShowingMainMenu;
        public event Action OnPause;
        public event Action OnResume;

        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gameMenuPanel;
        [SerializeField] private Button pauseMenuButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainMenuButton;

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(HideGameMenu);
            pauseMenuButton.onClick.AddListener(ShowGameMenu);
            restartButton.onClick.AddListener(RestartLevel);
            mainMenuButton.onClick.AddListener(GoMainMenu);
        }

        private void OnDisable()
        {
            resumeButton.onClick.RemoveListener(HideGameMenu);
            pauseMenuButton.onClick.RemoveListener(ShowGameMenu);
            restartButton.onClick.RemoveListener(RestartLevel);
            mainMenuButton.onClick.RemoveListener(GoMainMenu);
        }

        private void RestartLevel()
        {
            HideGameMenu();
            RestartingLevel?.Invoke();
        }

        private void GoMainMenu()
        {
            HideGameMenu();
            ShowMainMenu();
            ShowingMainMenu?.Invoke();
        }
        
        private void ShowGameMenu()
        {
            gameMenuPanel.SetActive(true);
            OnPause?.Invoke();
        }
        
        private void ShowMainMenu()
        {
            mainMenuPanel.gameObject.SetActive(true);
        }

        private void HideGameMenu()
        {
            gameMenuPanel.SetActive(false);
            OnResume?.Invoke();
        }
    }
}
