using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Used by batch script ( -executeMethod) to automate building process for Unity from commandline or with CI.
/// </summary>
public class AutomatedBuildProcess
{
    private static string WINDOWS_BUILD_FOLDER_PATH = "D:/workspace/UnityBuilds";

    private static string PRJ_NAME = "sturdy_arrow";

    public static void StartWinBuild()
    {
        string executableDirectoryPath = WINDOWS_BUILD_FOLDER_PATH + "/" + PRJ_NAME + "/v"
            + Application.version + "_dt" + System.DateTime.Now.ToString("HH-mm-ss_dd-MM-yyyy");

        CreateDirectory(executableDirectoryPath);

        string winExecutableName = PRJ_NAME + "_" + Application.version + ".exe";

        Debug.Log("Starting windows build");
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetEnabledScenes();
        buildPlayerOptions.locationPathName = executableDirectoryPath+ "/" + winExecutableName;
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        ZipBuild(executableDirectoryPath);
    }

    private static void CreateDirectory(string executableDirectoryPath)
    {
        if(!Directory.Exists(executableDirectoryPath))
            Directory.CreateDirectory(executableDirectoryPath);
    }

    private static string[] GetEnabledScenes()
    {
        return EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();
            
    }

    private static void ZipBuild(string buildPath) 
    {
        string zipPath = buildPath + ".zip";
        if(File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        ZipFile.CreateFromDirectory(buildPath, zipPath);
    }
}
