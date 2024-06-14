using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TableReader
{
    public static IEnumerator LoadData(string address, int sheet, Action<List<Dictionary<string, object>>> action = null, int range = 3)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{address}/export?format=tsv&&gid={sheet}");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.responseCode != 200)
        {
            yield break;
        }

        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        string data = www.downloadHandler.text;
        string[] line = data.Split('\n');

        if (line.Length < 1) yield break;

        string[] header = line[0].Split('\t');

        for (int i = range; i < line.Length; i++)
        {
            string[] values = line[i].Split('\t');

            if (NullOrWhiteSpace(values)) continue;

            Dictionary<string, object> dic = new Dictionary<string, object>();

            for (int j = 0; j < header.Length; j++)
            {
                if (header[j].Contains("#") || string.IsNullOrWhiteSpace(header[j]))
                    continue;

                object finalvalue = values[j].Trim();

                if (!dic.ContainsKey(header[j]))
                {
                    dic.Add(header[j].Trim(), finalvalue);
                }
            }

            list.Add(dic);
        }

        action.Invoke(list);
    }

    static bool NullOrWhiteSpace(string[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(values[i]))
            {
                return false;
            }
        }

        return true;
    }
}
