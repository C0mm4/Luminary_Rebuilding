using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class SpellManager
{
    public Dictionary<int, Spell> spells;

    string spellXMLFile = "SpellsXML";

    XmlNodeList text;

    // Load Spell Data by XML
    public void init()
    {
        XmlDocument doc = GameManager.Resource.LoadXML(spellXMLFile);
        spells = new Dictionary<int, Spell>();  

        text = doc.GetElementsByTagName("spell");

        createSpellObj();
    }

    // Create Spell Object
    public void createSpellObj()
    {
        foreach (XmlNode node in text)
        {
            Spell spl = new Spell();
            spl.setData(setSpellData(node));
            spells.Add(int.Parse(node["Index"].InnerText), spl);

        }
    }
    
    // Set Spell Datas
    public SpellData setSpellData(XmlNode node)
    {
        SpellData spellData = new SpellData();
        spellData.name = node["name"].InnerText;
        spellData.circle = int.Parse(node["circle"].InnerText);
        spellData.xRange = float.Parse(node["xRange"].InnerText);
        spellData.yRange = float.Parse(node["yRange"].InnerText);
        if (node["type"].InnerText == "Projectile")
        {
            spellData.type = 1;
        }
        else
        {
            spellData.type = 2;
        }
        spellData.damage = int.Parse(node["damage"].InnerText);
        spellData.hits = int.Parse(node["hits"].InnerText);
        spellData.projectileN = int.Parse(node["projectileN"].InnerText);
        spellData.castTime = float.Parse(node["castTime"].InnerText);
        spellData.durateT = float.Parse(node["durateT"].InnerText);
        spellData.spd = float.Parse(node["spd"].InnerText);
        spellData.path = node["path"].InnerText;
        spellData.spr = GameManager.Resource.LoadSprite(node["spr"].InnerText);

        return spellData;
    }

    // return spell data
    public SpellData getSpellData(int index)
    {
        return spells[index].data;
    }
}
