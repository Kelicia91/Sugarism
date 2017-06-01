using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 공략 대상. 
// 각 인물별로 이 클래스 상속받아서 특화시켜야 될듯.
public abstract class Hero
{
    // profile
    public int age;
    public string name;
    public EConstitution constitution;

    // stats
    public int feeling;
}
