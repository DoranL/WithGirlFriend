using Fusion;
using UnityEngine;

public struct PlayerData : INetworkInput
{
    // Start is called before the first frame update
    public NetworkButtons NetworkButtons;

    public float HorizontalInput;
    public float VerticalInput;
    public Vector3 direction;

    public Angle yaw;
    public Angle pitch;
}
