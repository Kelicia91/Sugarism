using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        setConstitutionText(get(constitution));

        EZodiac zodiac = Manager.Instance.Object.NurtureMode.Character.Zodiac;
        setZodiacText(get(zodiac));

        int age = mainCharacter.Age;
        setAgeText(get(age));

        string name = mainCharacter.Name;
        setNameText(name);   
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

    private void setAgeText(string s)
    {
        if (null == AgeText)
        {
            Log.Error("not found age text");
            return;
        }

        AgeText.text = s;
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

    private string get(int age)
    {
        string strAge = age.ToString();
        return strAge;
    }

    private string get(EZodiac zodiac)
    {
        int zodiacId = (int)zodiac;

        if (zodiacId < 0)
            return string.Empty;
        else if (zodiacId >= Manager.Instance.DTZodiac.Count)
            return string.Empty;
        else
            return Manager.Instance.DTZodiac[zodiacId].name;
    }

    private string get(EConstitution constitution)
    {
        int constitutionId = (int)constitution;

        if (constitutionId < 0)
            return string.Empty;
        else if (constitutionId >= Manager.Instance.DTConstitution.Count)
            return string.Empty;
        else
            return Manager.Instance.DTConstitution[constitutionId].name;
    }
}
