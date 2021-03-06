﻿using UnityEngine;
using UnityEngine.UI;


public class ProfilePanel : Panel
{
    /********* Editor Interface *********/
    // prefabs
    public Image ConstitutionImage;
    public Text ConstitutionText;
    public Image ZodiacImage;
    public Text ZodiacText;
    public Text AgeText;
    public Text NameText;


    void Start()
    {
        MainCharacter mainCharacter = Manager.Instance.Object.MainCharacter;

        EConstitution constitution = mainCharacter.Constitution;
        int constitutionId = (int)constitution;
        Constitution c = Manager.Instance.DT.Constitution[constitutionId];
        ConstitutionImage.sprite = c.sprite;
        ConstitutionImage.color = c.color;
        //setConstitutionText(get(constitution));

        EZodiac zodiac = Manager.Instance.Object.NurtureMode.Character.Zodiac;
        ZodiacImage.sprite = get(zodiac);
        //setZodiacText(get(zodiac));

        int age = mainCharacter.Age;
        setAgeText(age);

        string name = mainCharacter.Name;
        setNameText(name);

        Manager.Instance.Object.NurtureMode.Character.AgeChangeEvent.Attach(onAgeChanged);
    }

    private void onAgeChanged(int age)
    {
        setAgeText(age);
    }

    private void setConstitutionText(string s)
    {
        if (null == ConstitutionText)
        {
            Log.Error("not found constitution text");
            return;
        }

        ConstitutionText.text = s;
    }

    private void setZodiacText(string s)
    {
        if (null == ZodiacText)
        {
            Log.Error("not found zodiac text");
            return;
        }

        ZodiacText.text = s;
    }

    private void setAgeText(int age)
    {
        if (null == AgeText)
        {
            Log.Error("not found age text");
            return;
        }

        AgeText.text = age.ToString();
    }

    private void setNameText(string s)
    {
        if (null == NameText)
        {
            Log.Error("not found name text");
            return;
        }

        NameText.text = s;
    }

    private Sprite get(EZodiac zodiac)
    {
        int zodiacId = (int)zodiac;

        if (zodiacId < 0)
            return null;
        else if (zodiacId >= Manager.Instance.DT.Zodiac.Count)
            return null;
        else
            return Manager.Instance.DT.Zodiac[zodiacId].image;
    }

    private string get(EConstitution constitution)
    {
        int constitutionId = (int)constitution;

        if (constitutionId < 0)
            return string.Empty;
        else if (constitutionId >= Manager.Instance.DT.Constitution.Count)
            return string.Empty;
        else
            return Manager.Instance.DT.Constitution[constitutionId].name;
    }
}
