using System;
using System.Collections.Generic;
using System.Text;

partial class Folder
{
    internal class Backup
    {
        List<File> files = new List<File>();

        public Backup(Folder folder)
        {
            foreach (var file in folder.files)
                this.files.Add(new File(file.Name, file.Size));
        }


        public static void Restore(Folder folder, Backup backup)
        {
            folder.files.Clear();
            foreach (var file in backup.files)
                folder.files.Add(new File(file.Name, file.Size));
        }

    }

    public void AddFile(string name, int size)
    {
        files.Add(new File(name, size));
    }

    public void RemoveFile(File file)
    {
        files.Remove(file);
    }

    public File this[int i]
    {
        get {
            if (i < 0 || i >= files.Count)
                throw new IndexOutOfRangeException("Not enough files or index less zero");
            return files[i];
        }
    }

    public File this[string filename]
    {
        get {
            foreach (var file in files)
            {
                if (file.Name == filename)
                    return file;
            }
            throw new ArgumentException("File not found");
        }
    }

    public override string ToString()
    {
        string result = "Files in folder:" + Environment.NewLine;
        for(int i = 0; i < files.Count; i++)
            if (i == files.Count - 1)
                result += files[i];
            else
                result += files[i] + Environment.NewLine;
        return result;
    }
}

