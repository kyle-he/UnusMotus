using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GamemodeManager : MonoBehaviour
{
    private string _selectedMode = "relaxed";
    public LevelSelection LevelSelection;

    [SerializeField]
    private Image _relaxed;

    [SerializeField]
    private Image _challenge;

    [SerializeField]
    private Text _title;

    [SerializeField]
    private Text _description;

    private Color _green = new Color(106.0f / 255, 230.0f / 255, 76.0f / 255, 1f);

    /// <summary>
    /// Start selected gamemode
    /// </summary>
    public void StartGame()
    {
        SetGameMode(_selectedMode);
        if (_selectedMode == "challenge")
        {
            LevelSelection.SceneSelection("Level1");
        }
        else if (_selectedMode == "relaxed")
        {
            LevelSelection.SceneSelection("Level Selection");
        }
    }

    /// <summary>
    /// Select gamemode
    /// </summary>
    /// <param name="mode">name of gamemode</param>
    public void SetGameMode(string mode)
    {
        if (mode == "challenge")
        {
            GameManager.Instance.setGameModeChallenge();
        }
        else if (mode == "relaxed")
        {
            GameManager.Instance.setGameModeRelaxed();
        }
    }

    /// <summary>
    /// Select gamemode visually
    /// </summary>
    /// <param name="name">name of the gamemode</param>
    public void SelectMode(string mode)
    {
        _selectedMode = mode;
        ChangeColor(mode);
    }

    /// <summary>
    /// Change color
    /// </summary>
    /// <param name="name">name of the gamemode</param>
    public void ChangeColor(string mode)
    {
        if (mode == "challenge")
        {
            _relaxed.DOColor(Color.white, 0.1f);
            _challenge.DOColor(_green, 0.1f);
            _title.text = "Hardcore Mode";
            _description.text = "In Hardcore mode, you can challenge your speed, accuracy, and efficiency, and compete with players from all around the world. \n \nThis game mode has: \n3 Lives \nMove Count \nGlobal Leaderboard";
        }
        else if (mode == "relaxed")
        {
            _challenge.DOColor(Color.white, 0.1f);
            _relaxed.DOColor(_green, 0.1f);
            _title.text = "Casual Play";
            _description.text = "In Casual Mode, you can relax and enjoy the puzzles with unlimited lives, no pressure, and a level selector.\n\nThis game mode has: \nUnlimited Lives \nLevel Selector";
        }
    }
}
