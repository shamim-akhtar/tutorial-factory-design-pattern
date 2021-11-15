using System;
using System.Collections.Generic;
using Patterns;
using SimpleJSON;

namespace Patterns
{
  public class CActivity
  {
    protected string type;
    protected string name;
    
    public string Type
    {
      get { return type; }
      set { type = value; }
    }
    public string Name
    {
      get { return name; }
    }
    public CActivity()
    {
      type = this.GetType().Name;
    }

    public virtual void FromJson(JSONNode n)
    {
      JSONNode node = n["activity"];
      name = node["name"];
      type = node["type"];
    }
    public virtual JSONNode ToJson()
    {
      JSONNode node = new JSONObject();
      JSONNode n = new JSONObject();
      node["activity"] = n;

      n["name"] = name;
      n["type"] = type;

      Console.WriteLine(n.ToString());
      return node;
    }
  }
  public class CActivityWithTineStamp : CActivity
  {
    private string time;
    public DateTime Time
    {
      get { return System.DateTime.Parse(time); }
      set { time = value.ToString(); }
    }
    public CActivityWithTineStamp()
      : base()
    {
      Time = System.DateTime.Now;
    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
      n["time"] = time;
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      time = n["time"];
      return n;
    }
  }
  public class CActivityDailyLogin : CActivity
  {
    public CActivityDailyLogin()
      : base()
    {

    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      return n;
    }
  }

  public class CActivityDailySpin : CActivity
  {
    private int reward = 0;
    public int SpinReward
    {
      get { return reward; }
      set { reward = value; }
    }
    public CActivityDailySpin()
      : base()
    {

    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      return n;
    }
  }

  public class CActivityWithResponses : CActivityWithTineStamp
  {
    [System.Serializable]
    public struct Response
    {
      int id;
      string value;
    }
    private List<Response> mResponses 
      = new List<Response>();

    public List<Response> Responses
    {
      get { return mResponses; }
    }

    public CActivityWithResponses()
      : base()
    {

    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      return n;
    }
  }

  public class CActivityWithDuration : CActivityWithTineStamp
  {

    float duration = 0.0f;
    public float DurationInseconds
    {
      get { return duration; }
      set { duration = value; }
    }
    public CActivityWithDuration()
      : base()
    {
    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      return n;
    }
  }

  public class CActivityWithLocation : CActivityWithDuration
  {
    [System.Serializable]
    public struct Point3
    {
      float x;
      float y;
      float z;
    }
    private Point3 pos;
    public Point3 Position
    {
      get { return pos; }
      set { pos = value; }
    }

    public CActivityWithLocation()
      : base()
    {

    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      return n;
    }
  }

  public class CActivityMiniGame : CActivityWithDuration
  {
    string miniGameName;
    public string MiniGameName
    {
      get { return miniGameName; }
      set { miniGameName = value; }
    }

    public CActivityMiniGame()
      : base()
    {

    }

    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      return n;
    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      CActivity act1 = CObjectFactory<CActivity>.Instance.Create("CActivityMiniGame");
      CActivity act2 = CObjectFactory<CActivity>.Instance.Create("CActivityDailySpin");
      Console.WriteLine("Hello World!");
    }
  }
}
