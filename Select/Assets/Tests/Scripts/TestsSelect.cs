using UnityEngine;
using System.Collections.Generic;
using System;
using SelectShouldContain;

public class TestsSelect : MonoBehaviour
{
    void Start()
    {
        new Select("#Paws").ShouldContain(3, "Paws");
        new Select("#Ears").ShouldContain("Ears");
        new Select("#ChildA").ShouldContain("ChildA");
        new Select(".Brown").ShouldContain("Dog", "Paws", "Cat");
        new Select(".Parent").ShouldContain("ParentA", "ParentB", "Grandparent");
        new Select("feature").ShouldContain(3, "Paws");
        new Select("feature").ShouldContain("Ears");

        new Select("animal.White").ShouldContain("Cat");
        new Select("feature.White").ShouldContain("Paws", "Ears");
        new Select(".Brown.White").ShouldContain("Cat");
        new Select(".Parent.Child").ShouldContain("ParentA", "ParentB");

        new Select("#Cat #Paws").ShouldContain("Paws");
        if (new Select("#Cat #Paws")[0].transform.parent.name != "Cat")
        {
            Debug.LogError("#Cat #Paws should have Cat as a parent");
        }

        new Select(".White feature").ShouldContain(2, "Paws");
        new Select(".White feature").ShouldContain("Ears");
        new Select(".Parent .Child").ShouldContain("ParentA", "ParentB", "ChildA", "ChildB", "ChildC");
        new Select(".Brown #Paws").ShouldContain(2, "Paws");

        new Select(".Parent, .Brown #Paws").ShouldContain(2, "Paws");
        new Select(".Parent, .Brown #Paws").ShouldContain("ParentA", "ParentB", "Grandparent");

        new Select(".Brown #Paws, .White #Ears, .Parent").ShouldContain(2, "Paws");
        new Select(".Brown #Paws, .White #Ears, .Parent").ShouldContain("Ears", "Grandparent", "ParentA", "ParentB");

        new Select(".White:enabled").ShouldContainExactly("Bunny", "Paws");
        new Select(".White:!enabled").ShouldContainExactly("Cat", "Ears");

        new Select(".Brown:fluffy").ShouldContainExactly("Dog");
        new Select(".Brown:fluffy .Brown:!fluffy").ShouldContainExactly("Paws");
    }
}

namespace SelectShouldContain
{
    public static class SelectExtensions
    {
        public static void ShouldContain(this Select select, params string[] names)
        {
            foreach (string name in names)
            {
                select.ShouldContain(1, name);
            }
        }

        public static void ShouldContainExactly(this Select select, params string[] names)
        {
            select.ShouldContain(names);

            foreach (string selected in select.names)
            {
                bool found = false;

                foreach (string name in names)
                {
                    if (selected == name)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    select.ShouldContain(0, selected);
                }
            }
        }

        public static void ShouldContain(this Select select, string name)
        {
            select.ShouldContain(1, name);
        }

        public static void ShouldContain(this Select select, int number, string name)
        {
            int count = 0;
            foreach (string selected in select.names)
            {
                if (selected == name)
                {
                    count++;
                }
            }

            if (number != count)
            {
                Debug.LogError(select.selector + " should return " + number + " " + name + " but returned " + count);
            }
            else
            {
                Debug.Log(select.selector + " returned the correct number of " + name);
            }
        }
    }
}