using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


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
        print("[SY] ConnectHelper.cs Start!!!");
        DoStart();
    }

    // Todo 삭제 예정
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeConnection(GameMode.Host);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeConnection(GameMode.Client);
        }
    }

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
        //Debug.Log( "Start game " + s_GameMode.ToString() );
        Debug.Log( "[SY] Start game " + s_GameMode.ToString() );

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
