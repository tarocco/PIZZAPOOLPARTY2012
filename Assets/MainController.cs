using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{

    public async Task LoadScenes(IEnumerable<string> scene_names)
    {
        if (!scene_names.Any())
            return;
        foreach (var scene_name in scene_names)
            if (SceneManager.GetSceneByName(scene_name) == gameObject.scene)
                throw new ArgumentException("Cannot additively load main scene.");
        var operations = scene_names
            .Select(s => SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive))
            .ToArray();
        foreach (var operation in operations)
            while (!operation.isDone)
                await Task.Yield();
        var last_scene_name = scene_names.Last();
        var last_scene = SceneManager.GetSceneByName(last_scene_name);
        SceneManager.SetActiveScene(last_scene);
    }

    public async Task LoadScenes(params string[] scene_names) => await LoadScenes((IEnumerable<string>)scene_names);

    public async Task UnloadScenes(IEnumerable<string> scene_names)
    {
        if (!scene_names.Any())
            return;
        foreach (var scene_name in scene_names)
            if (SceneManager.GetSceneByName(scene_name) == gameObject.scene)
                throw new ArgumentException("Cannot unload main scene.");
        var operations = scene_names
            .Select(s => SceneManager.LoadSceneAsync(s))
            .ToArray();
        foreach (var operation in operations)
            while (!operation.isDone)
                await Task.Yield();
    }

    public async Task UnloadScenes(params string[] scene_names) => await UnloadScenes((IEnumerable<string>)scene_names);

    public async Task UnloadAllScenes()
    {
        var scenes = Enumerable
            .Range(0, SceneManager.sceneCount)
            .Select(SceneManager.GetSceneAt)
            .ToArray();

        var operations = scenes
            .Where(s => s != gameObject.scene)
            .Select(s => SceneManager.UnloadSceneAsync(s))
            .ToArray();

        foreach (var operation in operations)
            while (!operation.isDone)
                await Task.Yield();
    }

    public async Task LoadProgram(string program_name)
    {
        await UnloadAllScenes();
        await LoadScenes("Program Add-in", program_name);
        var program_controller = FindObjectOfType<ProgramAddInController>();
        program_controller.Exit += HandleProgramExit; ;
    }

    private void HandleProgramExit()
    {
        LoadMenu();
    }

    public async Task LoadMenu()
    {
        await UnloadAllScenes();
        await LoadScenes("Menu");
        var menu_controller = FindObjectOfType<ProgramMenu>();
        menu_controller.ProgramSelected += HandleProgramSelected;
    }

    private void HandleProgramSelected(string program_name)
    {
        LoadProgram(program_name);
    }

    void Start()
    {
        LoadMenu();
    }

    void Update()
    {
    }
}
