using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShrineScript : MonoBehaviour {

    public float MAXHPBANK = 100;
    public float MAXMANABANK = 100;

    public float HPBank = 0;
    public float ManaBank = 0;

    public bool refill = true;

    public Image HPBankBar;
    public Image ManaBankBar;

    public Color HPEnabled;
    public Color ManaEnabled;
    public Color HPDisabled;
    public Color ManaDisabled;

    void Awake()
    {
        HPBank = MAXHPBANK;
        ManaBank = MAXMANABANK;

        HPBankBar = transform.Find("Canvas/HPBankBar").gameObject.GetComponent<Image>();
        ManaBankBar = transform.Find("Canvas/ManaBankBar").gameObject.GetComponent<Image>();
    }
	// Update is called once per frame
	void Update () {
        if(HPBank <= 0 || ManaBank <= 0)
        {
			print ("Start Refill");
            refill = true;

            HPBankBar.color = HPDisabled;
            ManaBankBar.color = ManaDisabled;
        }

        if(HPBank >= MAXHPBANK && ManaBank >= MAXMANABANK)
        {
			print ("End Refill");
            refill = false;

            HPBankBar.color = HPEnabled;
            ManaBankBar.color = ManaEnabled;
        }

		if(refill == true)
        {
            if(HPBank <= MAXHPBANK)
            {
                HPBank += 100 / 90 * Time.deltaTime;
            }
            if(ManaBank <= MAXMANABANK)
            {
                HPBank += 100 / 60 * Time.deltaTime;
            }
			/*if (HPBank >= 99 && ManaBank >= 99) {
				print ("End Refill 1");
				refill = false;

				HPBankBar.color = HPEnabled;
				ManaBankBar.color = ManaEnabled;
			}*/
        }

        HPBankBar.fillAmount = HPBank / MAXHPBANK;
        ManaBankBar.fillAmount = ManaBank / MAXMANABANK;
	}
}
