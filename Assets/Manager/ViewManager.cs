using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{

    public Camera MainCamera;

    public bool isFollowingPlayer;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        MainCamera.transform.position = new Vector3(WorldData.WorldDataInstance.SizeX / 2, 10f, WorldData.WorldDataInstance.SizeZ / 2);

    }

    void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            MainCamera.transform.position = new Vector3(Player.transform.position.x, 10.4f, Player.transform.position.z - 20f);

        }

    }

    public void MoveCamera(Vector3 Movement)
    {
        //Camera speed is dependent on camera FOV
        MainCamera.transform.position += Movement / (200f / (MainCamera.fieldOfView/4f));

    }

    /// <summary>
    /// Updates the camera's ortkographic size depending on what the scrollwheel input returns.
    /// </summary>
    public void ZoomCamera(float ZoomAmmount)
    {

        MainCamera.fieldOfView -= (ZoomAmmount) * MainCamera.fieldOfView;

        //Clamp it, this sets the minimum-maximum orthographic size.
        MainCamera.orthographicSize = Mathf.Clamp(MainCamera.orthographicSize, 1f, 70f);
    }


}
