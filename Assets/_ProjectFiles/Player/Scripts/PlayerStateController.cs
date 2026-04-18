using System;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public PlayerMode CurrentMode { get; private set; } = PlayerMode.Gameplay;

    public event Action<PlayerMode> OnModeChanged;

    public void SetMode(PlayerMode newMode)
    {
        if (CurrentMode == newMode)
            return;

        CurrentMode = newMode;
        OnModeChanged?.Invoke(CurrentMode);
    }

    public bool IsGameplay()
    {
        return CurrentMode == PlayerMode.Gameplay;
    }
}