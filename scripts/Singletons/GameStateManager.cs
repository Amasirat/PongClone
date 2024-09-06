namespace PongClone.scripts;

using System;
using Godot;
using System.Collections.Generic;

public partial class GameStateManager : Node
{
    public static GameStateManager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
        if(!FileAccess.FileExists(ConfigPath))
            CreateUserConfig();
        DownloadStateFromConfig();
        DownloadBestTime();
        // Apply the global fullscreen state to game
        CheckAndApplyFullscreen();
    }
    // A method to apply fullscreen if the Fullscreen is true, otherwise it will make the game windowed
    public void CheckAndApplyFullscreen()
    {
        if (Fullscreen)
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
        else
        {
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        }
    }
    private void DownloadStateFromConfig()
    {
        var config = FileAccess.Open(ConfigPath, FileAccess.ModeFlags.Read);
        while (config.GetPosition() < config.GetLength())
        {
            var line = config.GetCsvLine();
            switch (line[0])
            {
                case "time_limit":
                    TimeLimit = int.Parse(line[1]);
                    break;
                case "revert_controls":
                    RevertControls = bool.Parse(line[1]);
                    break;
                case "fullscreen":
                    Fullscreen = bool.Parse(line[1]);
                    break;
                default:
                    throw new Exception("Config file is either broken or a non-existing file, " +
                                        "delete any remaining artifacts " +
                                        "and restart the game to generate a default config");
            }
        }
        config.Close();
    }

    public void UploadStateToConfig()
    {
        if (!FileAccess.FileExists(ConfigPath))
            throw new Exception("config file is corrupted or does not exist. You can delete any remnant of the config and " +
                                "try relaunching the game to generate new default configuration");
        
        using var config = FileAccess.Open(ConfigPath, FileAccess.ModeFlags.Write);
        // Since the states are constant, this solution is the simplest.
        // This needs to be refactored if the states of the program need to be scaled. 
        string[] fullscreen = { "fullscreen", Fullscreen.ToString() };
        string[] timeLimit = { "time_limit", TimeLimit.ToString() };
        string[] revertControls = { "revert_controls", RevertControls.ToString() };
        
        config.StoreCsvLine(fullscreen);
        config.StoreCsvLine(timeLimit);
        config.StoreCsvLine(revertControls);
        
        config.Close();
    }
    private void CreateUserConfig()
    {
        // The user's config file is only created on the game's first launch,
        // the config should be created if it is for whatever reason deleted, otherwise it should skip
        if (FileAccess.FileExists(ConfigPath)) return;
        
        var file = FileAccess.Open("res://config/default.csv", FileAccess.ModeFlags.Read);
        var config = FileAccess.Open(ConfigPath, FileAccess.ModeFlags.Write);
        while (file.GetPosition() < file.GetLength())
        {
            var line = file.GetCsvLine();
            config.StoreCsvLine(line);
        }
        file.Close();
        config.Close();
    }

    public void RecordBestTime(Vector2 bestTime)
    {
        if (bestTime > BestTime)
        {
            BestTime = bestTime;
        }
        CreateBestTimeRecord();
    }

    public void DownloadBestTime()
    {
        if (!FileAccess.FileExists(BestTimePath))
        {
            CreateBestTimeRecord();
            return;
        }
        using var file = FileAccess.Open(BestTimePath, FileAccess.ModeFlags.Read);
        
    }

    public void CreateBestTimeRecord()
    {
        using var file = FileAccess.Open(BestTimePath, FileAccess.ModeFlags.Write);
        string[] line = { BestTime.X.ToString(), BestTime.Y.ToString() };
        file.StoreCsvLine(line);
        file.Close();
    }
    public bool Fullscreen { get; set; }
    public bool RevertControls { get; set; }
    public int TimeLimit { get; set; }
    public Vector2 BestTime { get; set; } = Vector2.Zero;
    
    private const string ConfigPath = "user://config.csv";
    private const string BestTimePath = "user://best_time.csv";
}