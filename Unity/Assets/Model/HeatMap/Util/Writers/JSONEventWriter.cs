using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONEventWriter: IEventWriter
{
    private readonly bool createFileIfNonFound;
    private readonly string path;

    /// <summary>
    /// 是否可以写入到文件中
    /// </summary>
    private readonly bool hasFileToWrite;

    public JSONEventWriter(string path, bool createFileIfNonFound)
    {
        this.path = path;

        this.createFileIfNonFound = createFileIfNonFound;
        hasFileToWrite = Startup();
    }

    bool IEventWriter.IsWriterAvailable()
    {
        return hasFileToWrite;
    }

    /// <summary>
    /// 清空事件文件中的内容
    /// </summary>
    public void ClearAllEvent()
    {
        FileStream fileStream = File.Create(this.path);
        fileStream.Close();
    }

    bool IEventWriter.SaveEvent(BaseEvent baseEvent)
    {
        if (!hasFileToWrite || !File.Exists(path))
        {
            return false;
        }

        StreamWriter writer = new StreamWriter(path, true);

        try
        {
            writer.WriteLine(ComposeJsonString(baseEvent));

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());

            return false;
        }
        finally
        {
            writer.Close();
        }
    }

    bool IEventWriter.SaveEvents(List<BaseEvent> baseEvents)
    {
        if (!hasFileToWrite || !File.Exists(path))
        {
            return false;
        }

        StreamWriter writer = new StreamWriter(path, true);

        try
        {
            foreach (BaseEvent baseEvent in baseEvents)
            {
                writer.Write(ComposeJsonString(baseEvent));
            }

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());

            return false;
        }
        finally
        {
            writer.Close();
        }
    }

    private bool CreateFile(string path)
    {
        if (!createFileIfNonFound) return false;

        try
        {
            File.Create(path);

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());

            return false;
        }
    }

    private string ComposeJsonString(BaseEvent baseEvent)
    {
        return JsonUtility.ToJson(baseEvent);
    }

    private bool Startup()
    {
        if (File.Exists(path)) return true;

        if (!createFileIfNonFound) return false;

        return CreateFile(path);
    }
}