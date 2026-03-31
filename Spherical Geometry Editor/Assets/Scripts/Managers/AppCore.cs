using UnityEngine;
using UnityEngine.SceneManagement;

public class AppCore : MonoBehaviour
{
    private CameraMovement cameraMovement;
    private SideMenu sideMenu;
    private InputHandler inputHandler;
    private static AppCore instance;
    private AppState currentState;
    private DrawManager drawManager;
    private IRepository repoitory;
    [SerializeField] private GameObject GrabablePointPreafab;
    [SerializeField] private GameObject GreatCirclePrefab;
    [SerializeField] private GameObject GreatCircleSegmentPrefab;
    [SerializeField] private GameObject SmallCirclePrefab;
    [SerializeField] private GameObject IntersectPointPrefab;
    [SerializeField] private GameObject LimitedPointPrefab;
    [SerializeField] private GameObject AntipodalPointPrefab;
    [SerializeField] private GameObject PolePointPrefab;
    [SerializeField] private GameObject MidPointPrefab;
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;

    public static AppCore Instance;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple AppCore instances detected, destroying self.");
            Destroy(this.gameObject);
        }
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EditorScene")
        {
            cameraMovement = FindFirstObjectByType<CameraMovement>();
            sideMenu = FindFirstObjectByType<SideMenu>();
            inputHandler = FindFirstObjectByType<InputHandler>();

            commandInvoker = new CommandInvoker();
            repoitory = new Repository();
            factory = new SphericalGeometryFactory(GrabablePointPreafab, IntersectPointPrefab, SmallCirclePrefab, GreatCirclePrefab, GreatCircleSegmentPrefab, LimitedPointPrefab, AntipodalPointPrefab, PolePointPrefab, MidPointPrefab);
            drawManager = new DrawManager(factory, commandInvoker, repoitory);
            SetState(new EditorState(this, inputHandler,  sideMenu, drawManager, commandInvoker));
            Debug.Log("AppCore initialized in EditorScene.");
        }
    }

    private void SetState(AppState state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = state;

        currentState.OnEnter();
    }
}
