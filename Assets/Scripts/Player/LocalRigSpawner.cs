using UnityEngine;
using Fusion;

public class LocalRigSpawner : SimulationBehaviour, ISpawned
{
    [SerializeField] GUISkin _GUISkin;

    [SerializeField] bool _autoSpawnXR = false;
    [SerializeField] PlayerInputHandler _XRRig;
    [Space]
    [SerializeField] bool _autoSpawnPC = false;
    [SerializeField] PlayerInputHandler _PCRig;

    static bool s_SavedSpawnSelectionXR = false;
    static bool s_SavedSpawnSelectionPC = false;


    bool _showPlayerSpawnChoice = false;

    public void Spawned()
    {
        Debug.Log("[SY] LocalRigSpawner.cs / Spawned() Call!!!");
        if( Object.HasInputAuthority )
        {
            _showPlayerSpawnChoice = _autoSpawnPC == false && _autoSpawnXR == false;

            if( _autoSpawnXR || Application.platform == RuntimePlatform.Android || s_SavedSpawnSelectionXR )
            {
                SpawnPlayer( _XRRig );
            }
            else if( _autoSpawnPC || s_SavedSpawnSelectionPC )
            {
                SpawnPlayer( _PCRig );
            }
        }
    }

    // VR용 or PC용으로 사용할지 구분 
    private void OnGUI()
    {
        if( _showPlayerSpawnChoice == false )
        {
            return;
        }

        GUI.skin = FusionScalableIMGUI.GetScaledSkin( _GUISkin, out var height, out var width, out var padding, out var margin, out var leftBoxMargin );
        GUILayout.BeginArea( new Rect( leftBoxMargin, margin, width, Screen.height ) );
        {
            GUILayout.BeginVertical( GUI.skin.window );
            {

                if ( GUILayout.Button( "VR Rig" ) )
                {
                    s_SavedSpawnSelectionXR = true;
                    SpawnPlayer( _XRRig );
                    Debug.Log("[SY] LocalRigSpawner.cs / Create _XRRig");

                }
                if ( GUILayout.Button( "PC Rig (Debug)" ) )
                {
                    s_SavedSpawnSelectionPC = true;
                    SpawnPlayer( _PCRig );
                    Debug.Log("[SY] LocalRigSpawner.cs / Create _PCRig");
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }

    // VR용 or PC용 플레이어 생성
    void SpawnPlayer( PlayerInputHandler prefab )
    {
        Debug.Log("[SY] LocalRigSpawner.cs / SpawnPlayer() Call!!!");
        ObserverCamera.DisableObserverCamera();

        _showPlayerSpawnChoice = false;
        var rig = Instantiate( prefab, transform );
        rig.transform.localPosition = Vector3.zero;
        rig.transform.localRotation = Quaternion.identity;

        this.enabled = false;
    }
}
