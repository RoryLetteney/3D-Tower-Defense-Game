using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreation : MonoBehaviour {

    //Prefabs/////////////////////////////
    [SerializeField]
    private GameObject swordsmen;
    [SerializeField]
    private GameObject archers;
    [SerializeField]
    private GameObject cavalry;
    [SerializeField]
    private GameObject ballistas;
    //Prefabs/////////////////////////////

    private GameObject selectedPrefab;
    private int selectedCost;
    private int groundLayerMask = 1 << 8;

    [SerializeField]
    private GameObject key1Selected;
    [SerializeField]
    private GameObject key2Selected;
    [SerializeField]
    private GameObject key3Selected;
    [SerializeField]
    private GameObject key4Selected;
    [SerializeField]
    private GameObject swordsmenInfo;
    [SerializeField]
    private GameObject archerInfo;
    [SerializeField]
    private GameObject cavalryInfo;
    [SerializeField]
    private GameObject ballistaInfo;

    private Image key1Image;
    private Image key2Image;
    private Image key3Image;
    private Image key4Image;

    private void Start()
    {
        key1Image = key1Selected.GetComponent<Image>();
        key2Image = key2Selected.GetComponent<Image>();
        key3Image = key3Selected.GetComponent<Image>();
        key4Image = key4Selected.GetComponent<Image>();
    }

    private void Update()
    {
        GetSelected();
        if (selectedPrefab != null)
        {
            Create();
        }        
    }

    void GetSelected()
    {
        if (Input.GetKeyUp("1"))
        {
            selectedPrefab = swordsmen;
            selectedCost = Shop.swordsmenCost;
            key1Image.enabled = true;
            key2Image.enabled = false;
            key3Image.enabled = false;
            key4Image.enabled = false;
            swordsmenInfo.SetActive(true);
            archerInfo.SetActive(false);
            cavalryInfo.SetActive(false);
            ballistaInfo.SetActive(false);
        }
        if (Input.GetKeyUp("2"))
        {
            selectedPrefab = archers;
            selectedCost = Shop.archerCost;
            key1Image.enabled = false;
            key2Image.enabled = true;
            key3Image.enabled = false;
            key4Image.enabled = false;
            swordsmenInfo.SetActive(false);
            archerInfo.SetActive(true);
            cavalryInfo.SetActive(false);
            ballistaInfo.SetActive(false);
        }
        if (Input.GetKeyUp("3"))
        {
            selectedPrefab = cavalry;
            selectedCost = Shop.cavalryCost;
            key1Image.enabled = false;
            key2Image.enabled = false;
            key3Image.enabled = true;
            key4Image.enabled = false;
            swordsmenInfo.SetActive(false);
            archerInfo.SetActive(false);
            cavalryInfo.SetActive(true);
            ballistaInfo.SetActive(false);
        }
        if (Input.GetKeyUp("4"))
        {
            selectedPrefab = ballistas;
            selectedCost = Shop.ballistaCost;
            key1Image.enabled = false;
            key2Image.enabled = false;
            key3Image.enabled = false;
            key4Image.enabled = true;
            swordsmenInfo.SetActive(false);
            archerInfo.SetActive(false);
            cavalryInfo.SetActive(false);
            ballistaInfo.SetActive(true);
        }
    }

    void Create()
    {
        if (Shop.currency >= selectedCost)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (!Physics.CheckSphere(hit.point, 3f, ~groundLayerMask))
                    {
                        if (selectedPrefab == ballistas)
                        {
                            Instantiate(selectedPrefab, new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z), selectedPrefab.transform.rotation);
                        } else
                        {                            
                            Instantiate(selectedPrefab, hit.point, selectedPrefab.transform.rotation);
                        }
                        Shop.PurchaseUnits(selectedCost);
                    }
                }
            }
        }
    }
}
