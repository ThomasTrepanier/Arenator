using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuHUDManager : MonoBehaviour {

    public VRTK_SDKManager currentManager;
    private GameObject Player;

    public VRTK_SDKSetup ViveSetup;
    public VRTK_SDKSetup SimulatorSetup;
    private VRTK_SDKSetup currentSetup;

    void Awake () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {

        if (currentSetup == null)
        {
            if (currentManager.loadedSetup == ViveSetup)
            {
                currentSetup = ViveSetup;
            }
            else if (currentManager.loadedSetup == SimulatorSetup)
            {
                currentSetup = SimulatorSetup;
            }
        }
        if (currentSetup == SimulatorSetup)
        {
            transform.position = Player.transform.position - new Vector3(0, 2f, 0f);
        }
        if (currentSetup == ViveSetup)
        {
            transform.localPosition = Camera.main.gameObject.transform.position -= new Vector3(0, 2f, 0f);
            /*Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + 90, transform.eulerAngles.z);
		    transform.rotation = Quaternion.Euler(eulerRotation);*/
        }

        if (Camera.main)
        {
            float yRot = Camera.main.transform.eulerAngles.y;
            if (yRot < 45 | yRot > 315)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (yRot < 135 && yRot > 45)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (yRot < 225 && yRot > 135)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (yRot < 315 && yRot > 225)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
