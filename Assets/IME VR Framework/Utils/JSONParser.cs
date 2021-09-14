using UnityEngine;

public class JSONParser {

    public static string toJSON(object objectToSerialize)
    {
        return JsonUtility.ToJson(objectToSerialize, false);
    }

	public static void fromJSON(string jsonMessage, object objectToOverwrite)
    {
        JsonUtility.FromJsonOverwrite(jsonMessage, objectToOverwrite);
    }
}