using SimpleJSON;

namespace PlayerProgression
{
  // The base class for all types 
  // of activities.
  public class CActivity
  {
    #region Protected variables
    // The type of activity. 
    // This is same as this.GetType().Name or
    // typeof(ClassName).Name
    protected string mType;

    // A user recognizable name for the class.
    protected string mName;
    #endregion

    #region Properties
    public string Type
    {
      get 
      { 
        return mType; 
      }
      set 
      { 
        mType = value; 
      }
    }
    public string Name
    {
      get 
      { 
        return mName;
      }
      set
      {
        mName = value;
      }
    }
    #endregion

    // Constructor.
    public CActivity()
    {
      mType = this.GetType().Name;

      // by default we keep the name same as type.
      mName = this.GetType().Name;
    }

    #region JSON serialization.
    // Serialize from json.
    public virtual void FromJson(JSONNode n)
    {
      JSONNode node = n["activity"];
      mName = node["name"];
      mType = node["type"];
    }
    // Serialize to json.
    public virtual JSONNode ToJson()
    {
      JSONNode node = new JSONObject();
      JSONNode n = new JSONObject();
      node["activity"] = n;

      n["name"] = mName;
      n["type"] = mType;

      return node;
    }
    #endregion
  }
}