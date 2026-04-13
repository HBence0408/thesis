using UnityEngine;
using UnityEngine.SceneManagement;

public class AppCore : MonoBehaviour
{
    private CameraMovement cameraMovement;
    private SideMenu sideMenu;
    private InputHandler inputHandler;
    private IAppState currentState;
    private IDrawManager drawManager;
    private IRepository repository;
    private ColorMenu colorMenu;
    private EscapeMenu escapeMenu;
    private IHighlighter highlighter;
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
    private ISphericalGeometryFactory factory;
    private ICommandInvoker commandInvoker;
    private ISaveManager saveManager;
    private ILinker linker;
    private ISphericalGeometryLoadFactory sphericalGeometryLoadFactory;
    private ILoadManager loadManager;
    private IMapper mapper;
    private IParametricCurveMeshGenerator meshGenerator;
    private IIntersectionCalculater intersectionCalculater;
    private MainMenu mainMenu;
    private NewProjectMenu newProjectMenu;

    private EditorState editorState;
    private ColorPickState colorPickState;
    private EscapeMenuState escapeMenuState;
    private SaveState saveState;
    private LoadState loadState;

    private string fileToLoad = "";
    private string currentFile = "";
    
    public static AppCore Instance;
    private static AppCore instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogWarning("Multiple AppCore instances detected, destroying self.");
            Destroy(this.gameObject);
        }
        
    }

    //MainMenuScene
    //NewFileNameField

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
            repository = new Repository();
            highlighter = new Highlighter();
            mapper = new Mapper();
            meshGenerator = new ParametricCurveMeshGenerator();
            intersectionCalculater = new IntersectionCalculater();
            sphericalGeometryLoadFactory = new SphericalGeometryLoadFactory(meshGenerator, GrabablePointPreafab, IntersectPointPrefab, SmallCirclePrefab, GreatCirclePrefab, GreatCircleSegmentPrefab, LimitedPointPrefab, AntipodalPointPrefab, PolePointPrefab, MidPointPrefab, ShadowPolePointPrefab);
            linker = new Linker(repository, intersectionCalculater);
            loadManager = new LoadManager(repository, sphericalGeometryLoadFactory, linker);
            saveManager = new SaveManager(repository, mapper);
            factory = new SphericalGeometryFactory(intersectionCalculater, meshGenerator,GrabablePointPreafab, IntersectPointPrefab, SmallCirclePrefab, GreatCirclePrefab, GreatCircleSegmentPrefab, LimitedPointPrefab, AntipodalPointPrefab, PolePointPrefab, MidPointPrefab, ShadowPolePointPrefab);
            drawManager = new DrawManager(factory, commandInvoker, repository);
            editorState = new EditorState( inputHandler, sideMenu, drawManager, commandInvoker, cameraMovement, highlighter);
            colorPickState = new ColorPickState( sideMenu, drawManager, colorMenu);
            escapeMenuState = new EscapeMenuState( sideMenu, escapeMenu);
            saveState = new SaveState(saveManager, currentFile);
            loadState = new LoadState(loadManager, fileToLoad);

            if (fileToLoad != string.Empty)
            {
                SetState(loadState);
                Debug.Log("AppCore initialized in EditorScene with file to load: " + fileToLoad);
            }
            else
            {
                SetState(editorState);
                Debug.Log("AppCore initialized in EditorScene.");
            }
            
        }

        if (scene.name == "MainMenuScene")
        {
            mainMenu = FindFirstObjectByType<MainMenu>();
            newProjectMenu = FindFirstObjectByType<NewProjectMenu>();
            MainMenuState mainMenuState = new MainMenuState(mainMenu, newProjectMenu);
            SetState(mainMenuState);
            Debug.Log("AppCore initialized in MainMenuScene.");
        }
    }



    private void SetState(IAppState state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = state;

        currentState.OnEnter();
    }

    public void SetFileToLoad(string filePath)
    {
        fileToLoad = filePath;
        currentFile = fileToLoad;
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
        if (escapeMenuState != null)
        {
            SetState(escapeMenuState);
        }
    }

    public void SetSaveState()
    {
        if (saveState != null)
        {
            SetState(saveState);
        }
    }

    public void SetLoadState()
    {
        if (loadState != null)
        {
            SetState(loadState);
        }
    }

    public void SetCurrentFileName(string fileName)
    {
        currentFile = fileName;
        fileToLoad = string.Empty;
    }
}
