using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MuqDice;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        B b = new B(1,2);
        //await Database.Instance.Init();
        ////Debug.Log(l0.Union(l1).Count());
        //Debug.Log(MuqDice.MagicHelper.GetMagic(new MuqDice.ElementEnum[] { MuqDice.ElementEnum.Fire, MuqDice.ElementEnum.Water,ElementEnum.Fire })._Id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class A
    {
        public A(int a) { Debug.Log(1); }
    }
    public class B:A
    {
        public B(int a,int b) : base(a) { Debug.Log(2); }
    }

}
