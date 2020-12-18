using UnityEngine;

public class LayerMasks : MonoBehaviour
{
    public static LayerMask Weaponry => LayerMask.GetMask("Weaponry");
    public static LayerMask Player => LayerMask.GetMask("Player");

}
