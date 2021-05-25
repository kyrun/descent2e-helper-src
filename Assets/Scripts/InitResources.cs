using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InitResources : MonoBehaviour
{
    //[SerializeField] List<string> _subfolders = new List<string>();

    //string _resourcesFolder;

	void Start()
	{
        print(Resources.LoadAll<CharacterDef>("").Length);
        print(Resources.LoadAll<ClassDef>("").Length);
        print(Resources.LoadAll<ItemDef>("").Length);
        print(Resources.LoadAll<SkillDef>("").Length);
        print(Resources.LoadAll<MonsterDef>("").Length);
        print(Resources.LoadAll<DieDef>("").Length);
        print(Resources.LoadAll<CharacterModifierDef>("").Length);
        //foreach (var subfolder in _subfolders)
        //{
        //    print(Resources.LoadAll(subfolder).Length);
        //}
    }

	//void OnValidate()
 //   {
 //       _resourcesFolder = Application.dataPath + "/Resources/";
 //       _subfolders.Clear();
 //       RecursiveUpdateFields(_resourcesFolder);
 //   }

 //   void RecursiveUpdateFields(string dir)
 //   {
 //       var truncatedDir = dir.Replace(_resourcesFolder, "");
 //       _subfolders.Add(truncatedDir);

 //       string[] subdirectoryEntries = Directory.GetDirectories(dir);
 //       foreach (string subdirectory in subdirectoryEntries)
 //       {
 //           RecursiveUpdateFields(subdirectory);
 //       }
 //   }
}
