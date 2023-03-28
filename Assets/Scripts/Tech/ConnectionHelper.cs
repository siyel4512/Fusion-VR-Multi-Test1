using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement; // 삭제 예정


public class ConnectionHelper : MonoBehaviour
{
    static GameMode s_GameMode = 0;

    NetworkDebugStart _debugStart;

    private void Awake()
    {
        _debugStart = GetComponent<NetworkDebugStart>();
    }
    private void Start()
    {
        DoStart();
    }

    // Todo 삭제 예정
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("O key pressed!!!");
            ChangeConnection(GameMode.Host);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P key pressed!!!");
            ChangeConnection(GameMode.Client);
        }
    }

    // Todo 삭제 예정
    //public void OnGUI()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex == 0)
    //    {
    //        if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
    //        {
    //            ChangeConnection(GameMode.Host);
    //        }
    //        if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
    //        {
    //            ChangeConnection(GameMode.Client);
    //        }
    //    }
    //}

    public void StartHost()
    {
        ChangeConnection( GameMode.Host );
    }
    public void StartClient()
    {
        ChangeConnection( GameMode.Client );
    }

    public void ChangeConnection( GameMode gameMode )
    {
        Debug.Log( "Shutdown. Change connection to " + gameMode.ToString() );
        s_GameMode = gameMode;

        StartCoroutine( ShutdownRoutine() );
    }

    IEnumerator ShutdownRoutine()
    {
        if( Camera.main != null )
        {
            Camera.main.cullingMask = 0;
            Camera.main.clearFlags = CameraClearFlags.Color;
            Camera.main.backgroundColor = Color.black;
        }

        var wait = new WaitForEndOfFrame();

        yield return wait;
        yield return wait;
        yield return wait;

        _debugStart.Shutdown();
    }

    void DoStart()
    {
        Debug.Log( "Start game " + s_GameMode.ToString() );

        switch( s_GameMode )
        {
        case GameMode.Client:
            _debugStart.StartClient();
            break;
        case GameMode.Host:
            _debugStart.StartHost();
            break;
        case GameMode.Single:
            _debugStart.StartSinglePlayer();
            break;
        case GameMode.Shared:
            _debugStart.StartSharedClient();
            break;
        }
    }
}
