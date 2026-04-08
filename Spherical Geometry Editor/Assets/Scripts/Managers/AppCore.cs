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
    private ColorMenu colorMenu;
    private EscapeMenu escapeMenu;
    private Highlighter highlighter;
    [SerializeField] private GameObject GrabablePointPreafab;
    [SerializeField] private GameObject GreatCirclePrefab;
    [SerializeField] private GameObject GreatCircleSegmentPrefab;
    [SerializeField] private GameObject SmallCirclePrefab;
    [SerializeField] private GameObject IntersectPointPrefab;
    [SerializeField] private GameObject LimitedPointPrefab;
    [SerializeField] private GameObject AntipodalPointPrefab;
    [SerializeField] private GameObject PolePointPrefab;
    [SerializeField] private GameObject ShadowPolePointPrefab;
    [SerializeField] private GameObject MidPointPrefab;
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;
    private SaveManager saveManager;

    private EditorState editorState;
    private ColorPickState colorPickState;
    private EscapeMenuState escapeMenuState;
    private SaveState saveState;
    private Mapper mapper;

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
            colorMenu = FindFirstObjectByType<ColorMenu>();
            escapeMenu = FindFirstObjectByType<EscapeMenu>();

            commandInvoker = new CommandInvoker();
            repoitory = new Repository();
            highlighter = new Highlighter();
            mapper = new Mapper();
            saveManager = new SaveManager(repoitory, mapper);
            factory = new SphericalGeometryFactory(GrabablePointPreafab, IntersectPointPrefab, SmallCirclePrefab, GreatCirclePrefab, GreatCircleSegmentPrefab, LimitedPointPrefab, AntipodalPointPrefab, PolePointPrefab, MidPointPrefab, ShadowPolePointPrefab);
            drawManager = new DrawManager(factory, commandInvoker, repoitory);
            editorState = new EditorState(this, inputHandler, sideMenu, drawManager, commandInvoker, cameraMovement, highlighter);
            colorPickState = new ColorPickState(this, sideMenu, drawManager, colorMenu);
            escapeMenuState = new EscapeMenuState(this, sideMenu, escapeMenu);
            saveState = new SaveState(this, saveManager);
            SetState(editorState);
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

    public void SetEditorState()
    {
        if (editorState != null)
        {
            SetState(editorState);
        }
    }

    public void SetColorPickState()
    {
        if (colorPickState != null)
        {
            SetState(colorPickState);
        }
    }

    public void SetEscapeMenuState()
    {
        //EscapeMenuState escapeMenuState = new EscapeMenuState(this, sideMenu, escapeMenu);
        //SetState(escapeMenuState);
        if (colorPickState != null)
        {
            SetState(escapeMenuState);
        }
    }

    public void SetSaveState()
    {
        if (colorPickState != null)
        {
            SetState(saveState);
        }
    }
}
