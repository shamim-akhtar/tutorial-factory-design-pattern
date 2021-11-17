using System;
using SimpleJSON;

namespace PlayerProgression
{
  // An activity that has a time stamp.
  public class CActivityWithTineStamp : CActivity
  {
    #region Private data
    //store the time as a string.
    private DateTime mTime = System.DateTime.Now;
    private float mDuration = 0.0f;
    #endregion

    #region Properties
    public DateTime Time
    {
      get 
      {
        return mTime;
      }
      set 
      { 
        mTime = value; 
      }
    }

    // The duration in seconds the user spent
    // in this activity.
    public float Duration
    {
      get
      {
        return mDuration;
      }
      set
      {
        mDuration = value;
      }
    }
    #endregion

    public CActivityWithTineStamp()
      : base()
    {
    }

    #region JSON Serialization
    public override void FromJson(JSONNode n)
    {
      base.FromJson(n);
      mTime = System.DateTime.Parse(n["time"]);
      mDuration = n["duration"].AsFloat;
    }
    public override JSONNode ToJson()
    {
      JSONNode n = base.ToJson();
      n["time"] = mTime.ToString();
      n["duration"] = mDuration.ToString();
      return n;
    }
    #endregion
  }
}
