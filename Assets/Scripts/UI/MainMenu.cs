using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public event Action StartingRandomLevel;
        public event Action StartingCreatedLevel;
        
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private Button startRandomLevel;
        [SerializeField] private Button startCreatedLevel;

        private void OnEnable()
        {
            startRandomLevel.onClick.AddListener(StartRandomLeve);
            startCreatedLevel.onClick.AddListener(StartCreatedLeve);
        }

        private void OnDisable()
        {
            startRandomLevel.onClick.RemoveListener(StartRandomLeve);
            startCreatedLevel.onClick.RemoveListener(StartCreatedLeve);
        }

        private void StartRandomLeve()
        {
            HideMainMenu();
            StartingRandomLevel?.Invoke();
        }
        private void StartCreatedLeve()
        {
            HideMainMenu();
            StartingCreatedLevel?.Invoke();
        }
        
        private void HideMainMenu()
        {
            mainMenuPanel.SetActive(false);
        }
    }
}

