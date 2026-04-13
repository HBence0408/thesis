using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : IAppState
{
    private MainMenu mainMenu;
    private NewProjectMenu newProjectMenu;
    private List<string> savedFiles;

    public MainMenuState(MainMenu mainMenu, NewProjectMenu newProjectMenu)
    {
        this.mainMenu = mainMenu;
        this.newProjectMenu = newProjectMenu;
    }

    public void OnEnter()
    {
        savedFiles = GetAllSaveFileNames();
        mainMenu.OnButtonClicked += OnExistingSaveFileButtonClicked;
        mainMenu.OnNewButtonClicked += OnNewCreateSaveButtonClicked;
        Debug.Log("MainMenuState entered. Found " + savedFiles.Count + " save files.");
        mainMenu.SetButtons(savedFiles.ToArray());

        newProjectMenu.OnCreateButtonClicked += CreateNewProject;
    }

    public void OnExit()
    {
        savedFiles.Clear();
        mainMenu.OnButtonClicked -= OnExistingSaveFileButtonClicked;
        mainMenu.OnNewButtonClicked -= OnNewCreateSaveButtonClicked;
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

    private void OnNewCreateSaveButtonClicked()
    {
        mainMenu.Hide();
        newProjectMenu.Reveal();
    }

    private void OnExistingSaveFileButtonClicked(string fileName)
    {
        AppCore.Instance.SetFileToLoad(fileName);
        SceneManager.LoadScene("EditorScene");
    }

    private void CreateNewProject(string projectName)
    {
        AppCore.Instance.SetCurrentFileName(projectName);
        SceneManager.LoadScene("EditorScene");
    }

}
