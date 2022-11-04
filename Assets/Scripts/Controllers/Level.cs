using UI;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Spawner")] 
    
    [SerializeField]private ThrowableBubble prefabThrowableBubble;
    [SerializeField] private LevelBubble levelBubblePrefab;
    [SerializeField] private GameObject createdLevelPrefab;
    [SerializeField] private Transform throwableBubbleContainer;
    [SerializeField] private Transform levelBubbleContainer;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int startCountBubble;
    
    [Header("Aim")] 
    [SerializeField] private LineRenderer aimPrefab;
    [SerializeField] private int startCountAim;
    [SerializeField] private float maxDistanceReflectionRay;
    
    [Header("Grid Settings")] 
    [SerializeField] private GridGenerator gridGenerator;

    [Header("UI")] 
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private GameMenu gameMenu;

    private LevelState _levelState;
    private GameState _gameState;
    private GameObject _currentCreatedLevel;
    private BubbleSpawner _bubbleSpawner;
    private ThrowableBubble _throwableBubble;
    private ListExecuteObject _listExecuteObjects;
    private InputController _inputController;
    private Aim _aim;

    private bool _isStartGame;

    private void Awake()
    {
        _levelState = new LevelState();
        _gameState = new GameState();
        _gameState = GameState.MainMenu;
        _listExecuteObjects= new ListExecuteObject();
        _bubbleSpawner = new BubbleSpawner(prefabThrowableBubble,spawnPoint,throwableBubbleContainer,
                         gridGenerator.Generate(),levelBubblePrefab,levelBubbleContainer,
                                           startCountBubble);
        
        SubscribeUI();
        _aim = new Aim(aimPrefab,startCountAim,maxDistanceReflectionRay);
        _isStartGame = false;
    }
    
    private void Update()
    {
        if (!_isStartGame||_gameState!=GameState.PlayGame) return;
        for (var i = 0; i < _listExecuteObjects.Length; i++)
        {
            var interactiveObject = _listExecuteObjects[i];

            interactiveObject?.Execute();
        }
    }

    private void OnDisable()
    {
        UnsubscribeUI();
    }

    private void SubscribeUI()
    {
        mainMenu.StartingRandomLevel += PlayRandomLevel;
        mainMenu.StartingCreatedLevel += PlayCreateLevel;
        gameMenu.ShowingMainMenu += ClearLevel;
        gameMenu.RestartingLevel += RestartLevel;
        gameMenu.OnPause += Pause;
        gameMenu.OnResume += Resume;
    }

    private void UnsubscribeUI()
    {
        mainMenu.StartingRandomLevel -= PlayRandomLevel;
        mainMenu.StartingCreatedLevel -= PlayCreateLevel;
        gameMenu.ShowingMainMenu -= ClearLevel;
        gameMenu.RestartingLevel -= RestartLevel;
        gameMenu.OnPause -= Pause;
        gameMenu.OnResume -= Resume;
    }
    
    private void PlayRandomLevel()
    {
        _isStartGame = true;
        _levelState = LevelState.RandomLevel;
        _gameState = GameState.PlayGame;
        _bubbleSpawner.SpawnRandomLevel();
        AddNewBubble();
    }

    private void PlayCreateLevel()
    {
        _levelState = LevelState.CreatedLevel;
        _gameState = GameState.PlayGame;
        _isStartGame = true;
        _currentCreatedLevel = _bubbleSpawner.SpawnCreatedLevel(createdLevelPrefab);
        AddNewBubble();
    }

    private void Pause()
    {
        _gameState = GameState.Pause;
        Time.timeScale = 0;
    }

    private void Resume()
    {
        _gameState = GameState.PlayGame;
        Time.timeScale = 1;
    }

    private void RestartLevel()
    {
        ClearLevel();
        switch (_levelState)
        {
            case LevelState.CreatedLevel:
                PlayCreateLevel();
                break;
            case LevelState.RandomLevel:
                PlayRandomLevel();
                break;
                
        }
    }

    private void ClearLevel()
    {
        _isStartGame = false;
        if (_currentCreatedLevel!=null)
        {
            Destroy(_currentCreatedLevel);
        }
        _aim.DisableAim();
        _bubbleSpawner.DisablePoolObjects();
        _gameState = GameState.MainMenu;
    }

    private void AddNewBubble()
    {
        if (_throwableBubble!=null)
        {
            _throwableBubble.StopedBubBle -= AddNewBubble;
        }
        SpawnBubble();
        _throwableBubble.StopedBubBle +=AddNewBubble;
        
    }

    private void SpawnBubble()
    {
        _throwableBubble = _bubbleSpawner.SpawnBubble();
        _inputController= new InputController(_throwableBubble,_aim);
        _listExecuteObjects.AddExecuteObject(_inputController);
    }
}
