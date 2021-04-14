using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextureDivider : MonoBehaviour
{
    // Wherever you get the texture from
    public GameObject Pizza ;
    //public int partsTobeCut = 4;
    List<GameObject> pizzaParts = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //SetCutsAndDivide(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCutsAndDivide(int number)
    {
        DivideTexture(Pizza, number, this.gameObject);

    }

    //void TextureDivider()
    //{
    //    // E.g. for using the center of the image
    //    // but half of the size
    //    var rect = new Rect(texture.width / 4, texture.height / 4, texture.width / 2, texture.height / 2);

    //    // Create the sprite
    //    var sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
    //}

    public void DivideTexture(GameObject objToDivide,int partsDivision,GameObject parent)
    {
        if (objToDivide == null)
            return;
        ResetVar();
        //objToDivide.SetActive(true);
        objToDivide.GetComponent<Image>().fillAmount = (float)Math.Round(1 / (float)partsDivision, 2);
        objToDivide.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.7f;
        objToDivide.GetComponent<RectTransform>().anchoredPosition = new Vector3(-3f, 0f, 0f);
        // objToDivide.transform.position = Vector3.zero;
        //  Sprite.Create(objToDivide.GetComponent<Image>().)
        for (int i = 1; i < partsDivision; i++)
        {
            float zRot = 360 * i / (float)partsDivision;
            // Debug.Log("zrot " + zRot);
            Quaternion newRotation = Quaternion.Euler(objToDivide.transform.rotation.x, 0f, -zRot);
            GameObject obj = Instantiate(objToDivide, parent.transform);
            obj.transform.rotation = newRotation;
            pizzaParts.Add(obj);
            Vector3 pos;

            if (i % 2 != 0)
            {
                if(zRot<180)
                 pos = new Vector3(0f, 3f, 0f);
                else pos= new Vector3(0f, -3f, 0f);

            }
            else
            {
                
                pos = new Vector3(3f, 0f, 0f);
            }
            obj.GetComponent<RectTransform>().anchoredPosition = pos;
            //obj.transform.SetParent(parent.transform);
            // obj.transform.SetPositionAndRotation(Vector3.zero, newRotation);
        }

    }

    public void ResetVar()
    {
        foreach (GameObject obj in pizzaParts)
        {
            Destroy(obj);
        }
        Pizza.SetActive(true);
       Pizza.transform.localPosition = Vector3.zero;
        Pizza.GetComponent<Image>().fillAmount=1f;
    }
}
