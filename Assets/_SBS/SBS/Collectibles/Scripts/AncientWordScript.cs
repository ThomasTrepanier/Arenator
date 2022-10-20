using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientWordScript : MonoBehaviour {

    public GameObject DictionnaryWord;
    public GameObject ScrollbarElement;
    public GameObject ScrollbarForm;
    public GameObject ScrollbarAtt;

    public int typeNb;

    public int T1Drop;
    public int T3Drop;

    public List<ScriptableSpell_Element> elementList;
    public ScriptableSpell_Element thisElement;
    public GameObject ElementModel;

    public List<ScriptableSpell_SpellType> formList;
    public ScriptableSpell_SpellType thisForm;
    public GameObject FormModel;

    public List<ScriptableSpell_Attribute> attributeList;
    public ScriptableSpell_Attribute thisAttribute;
    public GameObject AttModel;

    private GameObject Pedestral;
    private ParticleSystem.MainModule Beam;

    public AudioClip DropSound;

    void Awake () {

        Pedestral = GameObject.Find("Pedestral_SpawnPoint");
        Beam = transform.Find("Beam").GetComponent<ParticleSystem>().main;

        T1Drop = 70 - GameManager.CurrentRound * 5;
        T3Drop = 100 - (10 + GameManager.CurrentRound * 2);

        #region CheckIfAcquired

        ScrollbarElement = GameObject.FindGameObjectWithTag("ScrollElement");
        ScrollbarForm = GameObject.FindGameObjectWithTag("ScrollType");
        ScrollbarAtt = GameObject.FindGameObjectWithTag("ScrollAtt");

        for (int i = 1; i < ScrollbarElement.GetComponentsInChildren<Transform>().Length; i++)
        {
            GameObject wordChild = ScrollbarElement.GetComponentsInChildren<Transform>()[i].gameObject;

            #region ElementCheck
            if (wordChild.GetComponent<dictionnaryWord>())
            {
                if (wordChild.GetComponent<dictionnaryWord>().wordElement)
                {
                    ScriptableSpell_Element wordElement = wordChild.GetComponent<dictionnaryWord>().wordElement;
                    for (int x = 0; x < elementList.Count; x++)
                    {
                        if (elementList[x] == wordElement)
                        {
                            elementList.RemoveAt(x);
                        }

                    }
                }
            }
        }
        #endregion ElementCheck
            #region FormCheck
        for (int i = 0; i < ScrollbarForm.GetComponentsInChildren<Transform>().Length; i++)
        {
            GameObject wordChild = ScrollbarForm.GetComponentsInChildren<Transform>()[i].gameObject;
            if (wordChild.GetComponent<dictionnaryWord>())
            {
                if (wordChild.GetComponent<dictionnaryWord>().wordType)
                {
                    ScriptableSpell_SpellType wordForm = wordChild.GetComponent<dictionnaryWord>().wordType;
                    for (int x = 0; x < formList.Count; x++)
                    {
                        if (formList[x] == wordForm)
                            formList.RemoveAt(x);
                    }
                }
            }
        }
        #endregion FormCheck
            #region AttCheck
        for (int i = 0; i < ScrollbarAtt.GetComponentsInChildren<Transform>().Length; i++)
        {
            GameObject wordChild = ScrollbarAtt.GetComponentsInChildren<Transform>()[i].gameObject;
            if (wordChild.GetComponent<dictionnaryWord>())
            {
                if (wordChild.GetComponent<dictionnaryWord>().wordAtt)
                {
                    ScriptableSpell_Attribute wordAtt = wordChild.GetComponent<dictionnaryWord>().wordAtt;
                    for (int x = 0; x < attributeList.Count; x++)
                    {
                        if (attributeList[x] == wordAtt)
                            attributeList.RemoveAt(x);
                    }
                }
            }
        }
        #endregion AttCheck

#endregion CheckIfAcquired

        Drop();

        GetComponent<AudioSource>().clip = DropSound;
        GetComponent<AudioSource>().Play();
    }

    void Update () {

        //Debug Code
        /*if (Input.GetKeyUp(KeyCode.J))
        {
            thisElement = null;
            thisAttType = null;
            thisAttribute = null;
            Drop();
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            for (int i = 0; i < 100; i++)
            {
                Drop();
            }
        }*/

        if (GameManager.CurrentRound < 12)
        {
            T1Drop = 70 - GameManager.CurrentRound * 5;
            T3Drop = 100 - (10 + GameManager.CurrentRound * 2);
        }
    }

    void Drop()
    {
        if (elementList.Count == 0 &&
           formList.Count == 0 &&
           attributeList.Count == 0)
            return;

        int FirstDrop = 1;
        int SecondDrop = 2;
        int ThirdDrop = 3;

        int componentDrop = Mathf.RoundToInt(Random.Range(FirstDrop- 0.5f, ThirdDrop + 0.5f));

        if(componentDrop == FirstDrop && elementList.Count == 0)
        {
            Drop();
            return;
        }
        if (componentDrop == SecondDrop && formList.Count == 0)
        {
            Drop();
            return;
        }
        if (componentDrop == ThirdDrop && attributeList.Count == 0)
        {
            Drop();
            return;
        }

        int dropRate = Mathf.RoundToInt(Random.Range(-0.5f, 100.5f));

        //print(dropRate);

        if (componentDrop == FirstDrop)
        {
            ScriptableSpell_Element resultT1 = elementList.FindLast(delegate (ScriptableSpell_Element element)
            {
                return element.Tier == 1;
            });

            ScriptableSpell_Element resultT2 = elementList.FindLast(delegate (ScriptableSpell_Element element)
            {
                return element.Tier == 2;
            });

            int LastT1 = elementList.IndexOf(resultT1);
            int LastT2 = elementList.IndexOf(resultT2);
            int LastT3 = elementList.Count - 1;
            if (dropRate <= T1Drop)
            {
                thisElement = elementList[Mathf.RoundToInt(Random.Range(-0.5f, LastT1 + 0.5f))];
            }
            if (dropRate > T1Drop && dropRate < T3Drop)
            {
                thisElement = elementList[Mathf.RoundToInt(Random.Range(LastT1 + 0.5f, LastT2 + 0.5f))];
            }
            if (dropRate >= T3Drop)
            {
                thisElement = elementList[LastT3];
            }
        }
        else if (componentDrop == SecondDrop)
        {
            ScriptableSpell_SpellType resultT1 = formList.FindLast(delegate (ScriptableSpell_SpellType form)
            {
                return form.Tier == 1;
            });
            ScriptableSpell_SpellType resultT2 = formList.FindLast(delegate (ScriptableSpell_SpellType form)
            {
                return form.Tier == 2;
            });

            int LastT1 = formList.IndexOf(resultT1);
            int LastT2 = formList.IndexOf(resultT2);
            if (dropRate <= T1Drop)
            {
                thisForm = formList[Mathf.RoundToInt(Random.Range(-0.5f, LastT1 + 0.5f))];
            }
            if (dropRate > T1Drop && dropRate < T3Drop)
            {
                thisForm = formList[Mathf.RoundToInt(Random.Range(LastT1 + 0.5f, LastT2 + 0.5f))];
            }
            if (dropRate >= T3Drop)
            {
                thisForm = formList[formList.Count - 1];
            }
        }
        else if (componentDrop == ThirdDrop)
        {
            ScriptableSpell_Attribute resultT1 = attributeList.FindLast(delegate (ScriptableSpell_Attribute attribute)
            {
                return attribute.Tier == 1;
            });

            ScriptableSpell_Attribute resultT2 = attributeList.FindLast(delegate (ScriptableSpell_Attribute attribute)
            {
                return attribute.Tier == 2;
            });

            ScriptableSpell_Attribute resultT3 = attributeList.FindLast(delegate (ScriptableSpell_Attribute attribute)
            {
                return attribute.Tier == 3;
            });

            int LastT1 = attributeList.IndexOf(resultT1);
            int LastT2 = attributeList.IndexOf(resultT2);
            int LastT3 = attributeList.IndexOf(resultT3);

            if (dropRate <= T1Drop)
            {
                thisAttribute = attributeList[Mathf.RoundToInt(Random.Range(-0.5f, LastT1 + 0.5f))];
            }
            if (dropRate > T1Drop && dropRate < T3Drop)
            {
                thisAttribute = attributeList[Mathf.RoundToInt(Random.Range(LastT1 + 0.5f, LastT2 + 0.5f))];
            }
            if (dropRate >= T3Drop)
            {
                thisAttribute = attributeList[Mathf.RoundToInt(Random.Range(LastT2 + 0.5f, LastT3 + 0.5f))];
            }
        }

        #region Beam Color

        if (dropRate <= T1Drop)
        {
            Beam.startColor = Color.green;
        }
        if (dropRate > T1Drop && dropRate < T3Drop)
        {
            Beam.startColor = Color.blue;
        }
        if (dropRate >= T3Drop)
        {
            Beam.startColor = new Color(1, 0.5f, 0);
        }
        #endregion Beam Color

        if (thisElement != null)
            Instantiate(ElementModel, transform.position, ElementModel.transform.rotation, transform);

        if (thisForm != null)
            Instantiate(FormModel, transform.position, FormModel.transform.rotation, transform);

        if (thisAttribute != null)
            Instantiate(AttModel, transform.position, AttModel.transform.rotation, transform);
    }

    public void PickUp()
    {
        //print("Picked Up");
        if (thisElement != null)
        {
            GameObject DictionnaryInst = Instantiate(DictionnaryWord, ScrollbarElement.transform.position, ScrollbarElement.transform.rotation, ScrollbarElement.transform);

            DictionnaryInst.GetComponent<dictionnaryWord>().wordElement = thisElement;

            elementList.Remove(thisElement);

			if(thisElement.Tier == 1)
			{
				DeathScreen.T1Gathered++;
			}
			else if (thisElement.Tier == 2)
			{
				DeathScreen.T2Gathered++;
			}
			else
			{
				DeathScreen.T3Gathered++;
			}

            //print(thisElement.ElementName);
        }
        if (thisForm != null)
        {
            GameObject DictionnaryInst = Instantiate(DictionnaryWord, ScrollbarForm.transform.position, ScrollbarElement.transform.rotation, ScrollbarForm.transform);

            DictionnaryInst.GetComponent<dictionnaryWord>().wordType = thisForm;

            formList.Remove(thisForm);

			if(thisForm.Tier == 1)
			{
				DeathScreen.T1Gathered++;
			}
			else if (thisForm.Tier == 2)
			{
				DeathScreen.T2Gathered++;
			}
			else
			{
				DeathScreen.T3Gathered++;
			}
            //print(thisAttType.attName);
        }
        if (thisAttribute != null)
        {
            GameObject DictionnaryInst = Instantiate(DictionnaryWord, ScrollbarAtt.transform.position, ScrollbarElement.transform.rotation, ScrollbarAtt.transform);

            DictionnaryInst.GetComponent<dictionnaryWord>().wordAtt = thisAttribute;

            attributeList.Remove(thisAttribute);

			if(thisAttribute.Tier == 1)
			{
				DeathScreen.T1Gathered++;
			}
			else if (thisAttribute.Tier == 2)
			{
				DeathScreen.T2Gathered++;
			}
			else
			{
				DeathScreen.T3Gathered++;
			}
            //print(thisAttribute);
        }

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.PlaySound(player.TakeWord, 0.5f);

        Pedestral.GetComponent<PedestralScript>().PickedUp = true;

        float delay = Random.Range(30, 60);
        GameManager.instance.GetComponent<GameManager>().Invoke("WordSpawn", delay);

        Destroy(gameObject);
        //print("End");
    }
}
