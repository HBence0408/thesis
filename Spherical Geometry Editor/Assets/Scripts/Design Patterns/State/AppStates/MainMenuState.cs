using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : AppState
{
    private MainMenu mainMenu;
    private NewProjectMenu newProjectMenu;
    private List<string> savedFiles;

    public MainMenuState(AppCore appCore, MainMenu mainMenu, NewProjectMenu newProjectMenu) : base(appCore)
    {
        this.mainMenu = mainMenu;
        this.newProjectMenu = newProjectMenu;
    }

    public override void OnEnter()
    {
        savedFiles = GetAllSaveFileNames();
        mainMenu.OnButtonClicked += OnFileButtonClicked;
        mainMenu.OnNewButtonClicked += OnNewButtonClicked;
        Debug.Log("MainMenuState entered. Found " + savedFiles.Count + " save files.");
        mainMenu.SetButtons(savedFiles.ToArray());

        newProjectMenu.OnCreateButtonClicked += CreateNewProject;
    }

    public override void OnExit()
    {
        savedFiles.Clear();
        mainMenu.OnButtonClicked -= OnFileButtonClicked;
        mainMenu.OnNewButtonClicked -= OnNewButtonClicked;
        newProjectMenu.OnCreateButtonClicked -= CreateNewProject;
    }

    public List<string> GetAllSaveFileNames()
    {
        string path = Application.persistentDataPath;

        string[] filePaths = Directory.GetFiles(path, "*.json");

        List<string> fileNames = filePaths
            .Select(p => Path.GetFileNameWithoutExtension(p))
            .ToList();

        return fileNames;
    }

    private void OnNewButtonClicked()
    {
        mainMenu.Hide();
        newProjectMenu.Reveal();
    }

    private void OnFileButtonClicked(string fileName)
    {
        appCore.SetFileToLoad(fileName);
        SceneManager.LoadScene("EditorScene");
    }

    private void CreateNewProject(string projectName)
    {
        appCore.SetCurrentFileName(projectName);
        SceneManager.LoadScene("EditorScene");
    }

}
