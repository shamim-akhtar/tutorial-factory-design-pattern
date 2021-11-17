using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerProgression;
using System.IO;
using SimpleJSON;

public class RandomActivities : MonoBehaviour
{
  List<CActivity> mActivities = new List<CActivity>();

  public string Filename = "GameActivities.json";


  // Start is called before the first frame update
  void Start()
  {
    string path = Application.persistentDataPath + "/" + Filename;
    // check if the file exists.
    if(File.Exists(path))
    {
      // Load the activities.
      string str = File.ReadAllText(path);
      JSONArray a = JSON.Parse(str).AsArray;
      for (int i = 0; i < a.Count; ++i)
      {
        Debug.Log("a[" + i + "]: " + a[i].ToString());
        CActivity activity = 
          CObjectFactory<CActivity>.Instance.Create(
            a[i]["activity"]["type"]);

        if(activity != null)
        {
          activity.FromJson(a[i]);
          mActivities.Add(activity);
        }
      }
    }
  }

  public void OnClickButton1()
  {
    CActivityWithTineStamp activity = 
      new CActivityWithTineStamp()
    {
      Duration = 7.0f,
      Name = "OnClickButton1",
      Time = System.DateTime.Now
    };
    mActivities.Add(activity);
  }

  public void OnClickButton2()
  {
    CActivityWithResponses activity = 
      new CActivityWithResponses()
    {
      Duration = 5.0f,
      Name = "OnClickButton2",
      Time = System.DateTime.Now,
    };
    // add some dummy responses
    for (int i = 0; i < 10; ++i)
    {
      CActivityWithResponses.Response res = 
        new CActivityWithResponses.Response()
      {
        duration = Random.Range(0.0f, 5.0f),
        id = i,
        value = "dummy value " + i.ToString(),
      };
      activity.Responses.Add(res);
    }
    mActivities.Add(activity);
  }

  public void OnClickButton3()
  {
    CActivity activity =
      new CActivity()
      {
        Name = "OnClickButton3"
      };
    mActivities.Add(activity);
  }

  public void OnClickButtonSave()
  {
    string path = Application.persistentDataPath + "/" + Filename;
    JSONArray json = new JSONArray();
    for (int i = 0; i < mActivities.Count; ++i)
    {
      json.Add(mActivities[i].ToJson());
    }
    File.WriteAllText(path, json.ToString());
  }
}
