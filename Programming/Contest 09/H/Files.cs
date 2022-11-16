using System;
using System.Collections.Generic;
using System.Linq;

public class Files
{
    Dictionary<string, List<Permissions>> database = new Dictionary<string, List<Permissions>>();

    public void CreateFile(string filename)
    {
        var list = new List<Permissions>();
        list.Add(Permissions.Default);
        database.Add(filename, list);
    }

    public void AddPermission(string filename, string permissionName)
    {
        Permissions permission = PermissionBuilder.FromName(permissionName);
        database[filename].Add(permission);
    }

    public void RemovePermission(string filename, string permissionName)
    {
        Permissions permission = PermissionBuilder.FromName(permissionName);
        database[filename].Remove(permission);
    }

    public override string ToString()
    {
        string answer = "";
        foreach (var pair in database)
        {
            pair.Value.Sort();
            answer += $"{pair.Key}:{GetLine(pair.Value)}" + Environment.NewLine;
        }
        return answer;
    }

    public string GetLine(List<Permissions> value)
    {
        string line = "";
        foreach (var elem in value)
            line += $" {elem}";
        return line;
    }
}