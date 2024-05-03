using UnityEngine;
using System.Collections;
using System;

public class NGUICell : iCollectionViewCell {

    public int index;

    public void Update()
    {
        index = Index;
    }

    public override string NibName
    {
        get
        {
            return "NGUICell";
        }
    } 

    Material m_material;
    Renderer m_renderer;
    Texture2D m_texture;

    public override void SetData(object data)
    {
        base.SetData(data);

        if(m_renderer==null)
            m_renderer = GetComponent<Renderer>();

        if(m_renderer!=null && m_material==null)
        {
            m_material = m_renderer.material;
        }

        //Set Color
        if(m_material!=null)
        {
            QuadCell.QuadCellData quadData = data as QuadCell.QuadCellData;
            if(quadData!=null && quadData.MainColor != null)
            {
                m_material.color = quadData.MainColor;
                m_renderer.material = m_material;
            }
        }

        //Set Texture
        if (m_material != null)
        {
            QuadCell.QuadCellData quadData = data as QuadCell.QuadCellData;
            if (quadData != null && quadData.MainTexture != null)
            {
                //m_texture = quadData.MainTexture;
                //m_renderer.sprite = m_texture;
            }
        }
    }
}
