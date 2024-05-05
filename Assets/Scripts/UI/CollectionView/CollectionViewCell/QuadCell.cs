using System;
using UnityEngine;
using UnityEngine.UI;

public class QuadCell : iCollectionViewCell
{
  
    //This was needed as Color can't be null
    public class QuadCellData
    {
        private Color m_color;
        public Color MainColor
        {
            get { return m_color; }
            set { m_color = value; }
        }

        private Texture2D m_texture;
        public Texture2D MainTexture
        {
            get { return m_texture; }
            set { m_texture = value; }
        }

        private Sprite m_sprite;
        public Sprite MainSprite
        {
            get { return m_sprite; }
            set { m_sprite = value; }
        }


        private RawImage m_rawimage;
        public RawImage MainRawImage
        {
            get { return m_rawimage; }
            set { m_rawimage = value; }
        }

        private Image m_image;
        public Image MainImage
        {
            get { return m_image; }
            set { m_image = value; }
        }
    }

    public int index;

    public void Update()
    {
        index = Index;
    }

    public override string NibName
    {
        get
        {
            return "QuadCell";
        }
    }


    Material m_material;
    Renderer m_renderer;
    SpriteRenderer m_sprite;
    private GameObject Reflection;
    private TestFlowsMichaelEdit m_testflow;


    void SetReflection()
    {
        m_testflow = GameObject.Find("Test").GetComponent<TestFlowsMichaelEdit>();
        Reflection = gameObject.transform.GetChild(2).gameObject; // Get's the third child object from the TestCell object which is reflection.
        Reflection.GetComponent<SpriteRenderer>().sprite = gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite; // Sets the reflections sprite to our image object in the prefab. Will break if the layout of the prefab is changed though.
        Reflection.transform.localScale = gameObject.transform.GetChild(1).transform.localScale;
        Reflection.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, m_testflow.Transparancy);



    }
    private void Start()
    {
        SetReflection();
    }


    public override void SetData(object data)
    {
        base.SetData(data);

        if (m_renderer == null)
            m_renderer = GetComponent<Renderer>();

        if (m_renderer != null && m_material == null)
        {
            m_material = m_renderer.material;
        }

       

        //Set Color
        if (m_material != null)
        {
            QuadCellData quadData = data as QuadCellData;


            if (quadData != null && quadData.MainColor != null)
            {
                m_material.color = quadData.MainColor;

                //m_material.SetTexture("_BaseMap", quadData.MainTexture);

                m_renderer.material = m_material;
                //m_renderer.transform.localScale = new Vector3(0.15f, 0.15f, 1);

            }


            if (m_sprite == null) { 

                m_sprite = gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>(); // Hello. Michael here. I edited this to specificly go to the Image object because it was trying to change the border insted since it just goes to the closes sprite renderer in a child

                m_sprite.sprite = quadData.MainSprite;
            }

            m_sprite.transform.localScale = new Vector3(1.65f, 1.65f, 1);

            /*
            // Get the child SpriteRenderer component of the current GameObject
            SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();


            if (quadData != null && quadData.MainTexture != null)
            {
                // Check if the childSpriteRenderer is found
                if (childSpriteRenderer != null)
                {
                    // Calculate the size of the child sprite renderer
                    //float spriteWidth = childSpriteRenderer.bounds.size.x;
                    //float spriteHeight = childSpriteRenderer.bounds.size.y;


                    float spriteWidth = childSpriteRenderer.bounds.size.x;
                    float spriteHeight = childSpriteRenderer.bounds.size.y;



                    // Create a sprite from the texture, scaled to fit the child sprite renderer
                    //Sprite newSprite = Sprite.Create(quadData.MainTexture, new Rect(0, 0, quadData.MainTexture.width, quadData.MainTexture.height), new Vector2(0.5f, 0.5f), Mathf.Max(spriteWidth, spriteHeight));

                    // Get the size of the child GameObject
                    float gameObjectWidth = transform.localScale.x;
                    float gameObjectHeight = transform.localScale.y;

                    // Calculate the aspect ratio of the texture
                    float textureAspectRatio = (float)quadData.MainTexture.width / quadData.MainTexture.height;

                    // Calculate the scale needed to fit the texture to the size of the child GameObject
                    float scaleX = gameObjectWidth / textureAspectRatio;
                    float scaleY = gameObjectHeight;

                    // Create a sprite from the texture, scaled to fit the size of the child GameObject
                    //Sprite newSprite = Sprite.Create(quadData.MainTexture, new Rect(0, 0, quadData.MainTexture.width/4, quadData.MainTexture.height/4), Vector2.one * 0.5f, Mathf.Min(scaleX, scaleY));

                    // Create a sprite from the texture
                    Sprite newSprite = Sprite.Create(quadData.MainTexture, new Rect(0, 0, quadData.MainTexture.width/5, quadData.MainTexture.height/5), Vector2.one * 0.5f);

                    // Assign the new sprite to the child sprite renderer
                    childSpriteRenderer.sprite = newSprite;


                    //childSpriteRenderer.size += new Vector2(0.05f, 0.05f);

                    childSpriteRenderer.transform.localScale = new Vector3(.65f, .65f, 1);



                    Debug.Log("Sprite texture applied to child sprite: " + childSpriteRenderer.gameObject.name);
                }
                else
                {
                    Debug.LogError("Child Sprite Renderer not found in the children of the current GameObject!");
                }
            }
            
            */
        }


    }

}