using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance {  get; private set; }
    
    [SerializeField] private GameObject parentObj;
    [field: SerializeField] public NetWorkController networkController {  get; private set; }
    

    //public PlayerSpawnerController playerSpawnController { get; set; }
    //public ObjectPollingManager objectPoolingManager { get; set; }
    //DontDestroyOnLoad �ߺ��� �����ϴ� �ڵ� 
    private void Awake()
    {
        //Instance = GlobalManager
        if(Instance == null)
        {
            Instance = this;
        }
        //parentObj = DontDestroyOnLoad 
        else
        {
            Destroy(parentObj);
        }
    }
}
