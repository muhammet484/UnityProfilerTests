using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Getcomponent+
 * tryGetComponent+
 * hashset contains
 * tag+
 * comparetag+
 * layer+
 */
namespace TestClasses
{
    public class TS_TagSystemTest : MonoBehaviour
    {
        HashSet<GameObject> SomeTag;
        GameObject[] GameObjects;

        const int TestCount = 500000;

        int TagHash, AnimatorTagHash;

        [SerializeField] string str1, str2; //Player - Playes
        private void Start()
        {
            SomeTag = new HashSet<GameObject>();
            GameObjects = new GameObject[TestCount];
            for (int i = 0; i < GameObjects.Length; i++)
            {
                GameObjects[i] = new GameObject(i + ". Game Object");
                GameObjects[i].layer = 3;
                GameObjects[i].AddComponent<TS_SomeComponent>();
                GameObjects[i].tag = "Player";
                SomeTag.Add(GameObjects[i]);
            }

            TagHash = "Player".GetHashCode();


            AnimatorTagHash = Animator.StringToHash("Player");
        }

        void TestTag()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if ( GameObjects[i].tag == "Player") { }
            }
        }

        void TestCompareTag()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (GameObjects[i].CompareTag("Player")) { }
            }
        }

        void TestGetComponent()
        {
            for (int i = 0; i < TestCount; i++)
            {
                TS_SomeComponent com = GameObjects[i].GetComponent<TS_SomeComponent>();
            }
        }
        void TestTryGetComponent()
        {
            for (int i = 0; i < TestCount; i++)
            {
                TS_SomeComponent com;
                if(GameObjects[i].TryGetComponent(out com)) { }
            }
        }

        void TestLayer()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (GameObjects[i].layer == 3) { }
            }
        }


        void TestLayerWithEquals()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (GameObjects[i].layer.Equals(3)) { }
            }
        }

        void TestHashset()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (SomeTag.Contains(GameObjects[i])) { }
            }
        }

        void TestNormalInteger()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (5 == 5) { }
            }
        }

        void TestTagsByHash()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (GameObjects[i].tag.GetHashCode() == TagHash) { }
            }
        }

        void TestConstantStringByHash()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if ("Player".GetHashCode() == TagHash) { }
            }
        }

        void TestConstantString()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if ("Player" == "Player") { }
            }
        }
        void TestVariableString()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (str1 == str2) { }
            }
        }
        void TestVariableStringsByConvertingThemToHash()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (str1.GetHashCode() == str2.GetHashCode()) { }
            }
        }
        void TestVariableStringByHash()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (str1.GetHashCode() == TagHash) { }
            }
        }
        void TestVariableStringByAnimatorHash()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (Animator.StringToHash(str1) == AnimatorTagHash) { }
            }
        }
        void TestConstantStringByAnimatorHash()
        {
            for (int i = 0; i < TestCount; i++)
            {
                if (Animator.StringToHash("Player") == AnimatorTagHash) { }
            }
        }

        private void Update()
        { 
            //for 500000 object
            if (Input.GetKeyDown(KeyCode.R)) 
                TestTag(); // 234.59 ms - 219 - 224 - 221.32
            if (Input.GetKeyDown(KeyCode.V))
                TestTagsByHash(); // 213.78 - 249.59 - 211.07 - 212.73
            if (Input.GetKeyDown(KeyCode.T))
                TestCompareTag(); // 89 ms - 87.93 - 87.30 - 88.29
            if (Input.GetKeyDown(KeyCode.Y))
                TestGetComponent(); // 190.30 ms - 196.11 - 197.23 - 202.15 - 199.26
            if (Input.GetKeyDown(KeyCode.U))
                TestTryGetComponent(); // 185.02 - 183.37 - 185.93 - 183.29 - 187.22
            if (Input.GetKeyDown(KeyCode.J))
                TestLayer(); // 53.83 - 56.52 - 54.42 - 53.90 - (54 avarage for trying 10 times in 2 second) - 53.86 - 53.87 - 54.17 - 53.57
            if (Input.GetKeyDown(KeyCode.G))
                TestLayerWithEquals(); // 56.46 - 57.32 - 54.04 - 53.92 (53.8 avarage for trying 10 times in 2 second)
            if (Input.GetKeyDown(KeyCode.H))
                TestHashset(); // 25.39 - 24.48 - 24.35 - 24.30 (24 avarage for trying 10 times in 2 second) - 28.65 - 22.89 - 22.77 - 22.67 - 22.86 - 23.43 - 23.03 - 22.53 - 22.87
            if (Input.GetKeyDown(KeyCode.F))
                TestNormalInteger(); //  0.33 ms - 0.3 - 0.29
            if (Input.GetKeyDown(KeyCode.B))
                TestConstantStringByHash(); // 3.97 - 4.1 - 3.77
            if (Input.GetKeyDown(KeyCode.C))
                TestConstantString(); // 0.31 - 0.33 - 0.32
            if (Input.GetKeyDown(KeyCode.N))
                TestVariableString(); // 15.30 - 15.22 - 15.26 - 15.29 - 15.29 - 15.33 - 400(when i put 10 times larger string to both variable) 403
            if (Input.GetKeyDown(KeyCode.O))
                TestVariableStringsByConvertingThemToHash(); // 7.65 - 7.19 - 7.33 - 6.38 - 182(when i put 10 times larger string to both variable) 183
            if (Input.GetKeyDown(KeyCode.M))
                TestVariableStringByHash(); // 3.95 - 4.33 - 3.8 - 3.86 - 3.65 - 3.86 - 92(when i put 10 times larger string to both variable)
            if (Input.GetKeyDown(KeyCode.K))
                TestConstantStringByAnimatorHash(); // 26.19 - 25.55 - 25.69
            if (Input.GetKeyDown(KeyCode.L))
                TestVariableStringByAnimatorHash(); // 26.34 - 25.74 - 25.65

            if (Input.GetKeyDown(KeyCode.A))
            {
                foreach (GameObject i in SomeTag)
                {
                    print(i.name);
                }
                print("length: " + SomeTag.Count);
            }
        }
    }
}
