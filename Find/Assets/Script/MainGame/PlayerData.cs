using Fusion;
using UnityEngine;

enum InputButtons
{
    forward = 0, 
    back =1,
    right =2,
    left =3,
    jump = 4, 
}

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
