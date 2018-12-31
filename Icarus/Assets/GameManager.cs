using System.IO;
using System.Linq;
using Icarus.Engine.Framework.Logging;
using Icarus.Engine.Framework.Modding;
using Icarus.Engine.Framework.Spawning;
using Icarus.Utilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButton_MainMenu_Start_Click()
    {
        var mods = ModLoader.GetMods(
            new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Mods")));

        Log.Debug(mods.Select(x => x.ModInfo.Name).Join(", "));
        
        ModLoader.LoadMods(mods);
        
        Log.Debug("Loaded???");

        var ship = Spawner.TestSpawn();
        
        Log.Debug(ship.Model.Name);
    }

    public void OnButton_MainMenu_OpenModMenu_Click()
    {
        var path = Path.Combine(Application.persistentDataPath, "Mods");
        Debug.Log(path);
        Directory.CreateDirectory(path);
        Application.OpenURL($"file://{path}");
    }
}
