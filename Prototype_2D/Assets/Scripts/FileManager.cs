using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileManager {

    static string fileName = "textingConfig.txt";

    public static void Write(string text) {
        StreamWriter writer = new StreamWriter(Path.Combine(System.IO.Directory.GetCurrentDirectory(), fileName));
        writer.Write(text);
        writer.Close();
    }

    public static string Read() {
        StreamReader reader = new StreamReader(Path.Combine(System.IO.Directory.GetCurrentDirectory(), fileName));
        string text = reader.ReadToEnd();
        reader.Close();
        return text;
    }

}
