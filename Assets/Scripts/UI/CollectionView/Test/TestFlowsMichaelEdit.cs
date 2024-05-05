﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.UI;
using UnityEngine.UI;

public class TestFlowsMichaelEdit : MonoBehaviour 
{

    [SerializeField]
    private UICollectionView m_coverFlow;

    [SerializeField]
    private Color[] m_colourData;

    [SerializeField]
    private Sprite[] m_sprites;

    [SerializeField]
    private Texture2D[] m_textures;

    [SerializeField]
    private RawImage[] m_rawimages;

    [SerializeField]
    private Image[] m_images;

    [SerializeField]
    private int m_numberOfCells = 10;

    [SerializeField]
    private int m_groupSizes = 10;

  
    [Header ("BorderChanges")]

    [SerializeField]
    private bool EnableBorders; 

    [SerializeField]
    private Vector2 BorderSize; //0.35 in both X and Y is the size of the sprite so set it higher to see the border

    [SerializeField]
    private Color BorderColour;

    private GameObject testCellPrefab;

    private GameObject Border;

    // Use this for initialization
    void Start () 
    {
        testCellPrefab = Resources.Load<GameObject>("Prefabs/UI/CollectionViewCells/TestCell"); // Refrence to the prefab. I could have just serialized the field but I didn't want to to accidently get changed. Make sure to change this path if the name changes.
        if (testCellPrefab == null)
        {           
          Debug.LogWarning("TestCellPrefab wasn't loaded. Make sure the file path in the script " + this.name + "Is still the same and leads to the TestCell Prefab");

        }
        else
        {
           Border = testCellPrefab.transform.GetChild(0).gameObject; //This will always refrence the first child of the testcell prefab so if that's changed change this.
           Border.GetComponent<SpriteRenderer>().color = BorderColour;
           Border.transform.localScale = BorderSize;
            if (!EnableBorders)
            {
              Border.SetActive(false);


            }              
            else
            {
                Border.SetActive(true);
            }

        }

        
 

	    //Build cells
        if(m_coverFlow!=null)
        {
            //Build a bunch of cells - pass in data
            List<QuadCell.QuadCellData> data = new List<QuadCell.QuadCellData>();
            for (int i = 0; i < m_numberOfCells; i++)
            {
                if(m_colourData!=null && m_colourData.Length > 0)
                {
                    data.Add(new QuadCell.QuadCellData() {
                        MainColor = new Vector4(1,1,1,0),
                        MainTexture = m_textures[i],
                        MainSprite = m_sprites[i]

                    }

                    );
                }
                else
                {
                    data.Add(new QuadCell.QuadCellData());
                }

            }

            //Bleugh
            m_coverFlow.Data = new List<object>(data.ToArray());
        }
	}





}


