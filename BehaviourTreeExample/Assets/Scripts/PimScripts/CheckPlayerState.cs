using UnityEngine;


public class CheckPlayerState : MonoBehaviour
{
    public bool IsPlayerDeath { get { return isPlayerDeath; } set { isPlayerDeath = value; } }
    private bool isPlayerDeath;
}
