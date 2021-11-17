using System;
using System.Collections.Generic;
using SimpleJSON;

namespace PlayerProgression
{
  public class CActivityWithResponses : CActivityWithTineStamp
  {
    // The response class.
    // This class is an example to show
    // how you can serialize internal data
    // array to JSON.
    public class Response
    {
      public int id;
      public string value;
      public float duration;

      public void FromJson(JSONNode n)
      {
        id = n["id"].AsInt;
        value = n["value"];
        duration = n["duration"].AsFloat;
      }
      public JSONNode ToJson()
      {
        JSONNode n = new JSONObject();
        n["id"] = id;
        n["value"] = value;
        n["duration"] = duration;

        return n;
      }
    }
    private List<Response> mResponses
      = new List<Response>();

    public List<Response> Responses
    {
      get 
      { 
        return mResponses; 
      }
    }

    public CActivityWithResponses()
      : base()
    {

    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);

      // now load the responses array from json.
      JSONArray a = n["responses"].AsArray;

      mResponses = new List<Response>();
      for (int i = 0; i < a.Count; ++i)
      {
        Response r = new Response();
        r.FromJson(a[i]);
        Responses.Add(r);
      }
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();

      JSONArray a = new JSONArray();
      for (int i = 0; i < Responses.Count; ++i)
      {
        JSONNode node = Responses[i].ToJson();
        a.Add(node);
      }
      n["responses"] = a;
      return n;
    }
  }

}
