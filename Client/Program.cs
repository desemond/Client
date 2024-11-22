using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Newtonsoft.Json;
using Client;



public class DayLevel
{
    public string Date { get; set; }
    public List<DataLevel> dataStorage { get; set; }

    public DayLevel()
    {
        dataStorage = new List<DataLevel>();
    }
    public void AddOrUpdateData(DataLevel newData)
    {
        var existingData = dataStorage.FirstOrDefault(d => d.Path == newData.Path);

        if (existingData != null)
        {
            // Обновляем существующие данные
            existingData.Size = newData.Size;
            existingData.type = newData.type;
            existingData.Quantity = newData.Quantity;
            existingData.status = newData.status;
            existingData.checkTime = newData.checkTime;
            existingData.lastWriteTime = newData.lastWriteTime;
        }
        else
        {
            // Добавляем новые данные
            dataStorage.Add(newData);
        }
    }
    public static List<DayLevel> LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            return new List<DayLevel>();

        var jsonData = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<DayLevel>>(jsonData);
    }

    public static void SaveToFile(List<DayLevel> dayLevels, string filePath)
    {
        var jsonData = JsonConvert.SerializeObject(dayLevels, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
}

public class DataLevel
{
    public string Path { get; set; }
    public List<string> Size { get; set; }
    public string type { get; set; }
    public int Quantity { get; set; }
    public List<bool> status { get; set; }
    public List<DateTime> checkTime { get; set; }
    public List<DateTime> lastWriteTime { get; set; }

    public DataLevel()
    {
        Size = new List<string>();
        status = new List<bool>();
        checkTime = new List<DateTime>();
        lastWriteTime = new List<DateTime>();
    }

}





class Client
{
    private string _host = "localhost";

    static long GetDirectorySize(string path)
    {
        long size = 0;

        // Получаем информацию о папке
        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        // Запрашиваем все файлы в папке и добавляем их размер
        FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        foreach (FileInfo file in files)
        {
            size += file.Length;
        }

        return size;
    }
    static string GetParentKey(string fullKey)
    {
        int lastIndex = fullKey.LastIndexOf('\\');
        if (lastIndex != -1)
        {
            return fullKey.Substring(0, lastIndex);
        }
        return fullKey; // или fullKey
    }
    static DataLevel createFirstExample(AllPaths Path_comm)
    {
        var data = new DataLevel();
        DateTime dateZero = new DateTime();
        data.Path = Path_comm.Path;
        data.type = Path_comm.Comment;
        data.status.Add(true);
        data.checkTime.Add(DateTime.Now);
        if (Path_comm.Comment.Contains("file"))
        {
            if (File.Exists(Path_comm.Path))
            {
                data.Size.Add(new FileInfo(Path_comm.Path).Length.ToString());
                data.lastWriteTime.Add(File.GetLastWriteTime(Path_comm.Path));
                data.Quantity = 1;
            }
        }
        else if (Path_comm.Comment.Contains("dir"))
        {
            if (Directory.Exists(Path_comm.Path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path_comm.Path);
                data.Size.Add (GetDirectorySize(Path_comm.Path).ToString());
                data.lastWriteTime.Add(directoryInfo.LastWriteTime);
                data.Quantity = directoryInfo.GetFiles().Length+ directoryInfo.GetDirectories().Length;
            }
        }
        else if (Path_comm.Comment.Contains("registry string"))
        {

            if (Path_comm.Path.StartsWith(@"HKEY_CURRENT_USER"))
            {
                string subKeyPath = Path_comm.Path.Replace(@"HKEY_CURRENT_USER\", "");
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(subKeyPath))
                {
                    if (subKey != null)
                    {

                        data.Quantity = (subKey.SubKeyCount);
                        data.lastWriteTime.Add(DateTime.FromFileTimeUtc((long)(int)subKey.GetValue("LastWriteTime", 0)));
                    }
                    else
                    {
                        data.Path = GetParentKey(Path_comm.Path);
                        data.Quantity = 0;
                        data.Size.Add( "0");
                        data.lastWriteTime.Add(dateZero);
                    }
                }
            }
            else if (Path_comm.Path.StartsWith(@"HKEY_LOCAL_MACHINE"))
            {
                string subKeyPath = Path_comm.Path.Replace(@"HKEY_CURRENT_USER\", "");
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(subKeyPath))
                {
                    if (subKey != null)
                    {
                        data.Quantity = (subKey.SubKeyCount);
                        data.lastWriteTime.Add(DateTime.FromFileTimeUtc((long)(int)subKey.GetValue("LastWriteTime", 0)));
                    }
                    else
                    {
                        data.Path = GetParentKey(Path_comm.Path);
                        data.Quantity = 0;
                        data.Size.Add("0");
                        data.lastWriteTime.Add(dateZero);
                    }
                }
            }
            else
            {
                data.Quantity = 0;
            }
        }
        return data;

    }
    
    static DayLevel createFirstDay(List<path_with_comment> paths)
    {
        DayLevel lastDay = new DayLevel();
        lastDay.Date = (DateOnly.FromDateTime(DateTime.Now)).ToString();
        foreach (var path_with_comment in paths)
        {
            lastDay.dataStorage.Add(createFirstExample(path_with_comment));
        }
        return lastDay;
    }
    static DayLevel createDays(DayLevel day)
    {
        DayLevel anotherDay = new DayLevel();
        anotherDay.Date = (DateOnly.FromDateTime(DateTime.Now)).ToString();
        
        foreach (var data in day.dataStorage)
        {
            path_with_comment paths = new path_with_comment(data.Path,data.type);
            anotherDay.dataStorage.Add(createFirstExample(paths));
        }
        return anotherDay;
    }
    static DataLevel check(DataLevel data) 
    {
        DateTime dateZero = new DateTime();
        if (data.type.Contains("file"))
        {
            if (File.Exists(data.Path))
            {
                if (data.Size[data.Size.Count -1] == new FileInfo(data.Path).Length.ToString()
                    && data.lastWriteTime[data.lastWriteTime.Count - 1] == File.GetLastWriteTime(data.Path))
                {
                    data.status.Add(true);
                    data.checkTime.Add(DateTime.Now);

                }
                else
                {
                    data.status.Add(false);
                    data.checkTime.Add(DateTime.Now);
                }
            }

        }
        else if (data.type.Contains("dir"))
        {
            if (Directory.Exists(data.Path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(data.Path);
                if (data.Size[data.Size.Count -1] == GetDirectorySize(data.Path).ToString()
                   && data.lastWriteTime[data.lastWriteTime.Count - 1] == directoryInfo.LastWriteTime
                   && data.Quantity== directoryInfo.GetFiles().Length + directoryInfo.GetDirectories().Length)
                {
                    data.status.Add(true);
                    data.checkTime.Add(DateTime.Now);
                }
                else
                {
                    data.status.Add(false);
                    data.checkTime.Add(DateTime.Now);
                }
            }
        }
        else if (data.type.Contains("registry string"))
        {
            if (data.Path.StartsWith(@"HKEY_CURRENT_USER"))
            {
                string subKeyPath = data.Path.Replace(@"HKEY_CURRENT_USER\", "");
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(subKeyPath))
                {
                    if (subKey != null)
                    {
                        if (data.Quantity == (subKey.SubKeyCount)
                        && data.lastWriteTime[data.lastWriteTime.Count - 1] == DateTime.FromFileTimeUtc((long)(int)subKey.GetValue("LastWriteTime", 0))
                        )//&& data.Size[data.Size.Count - 1] == subKey.GetValue(( subKey.GetSubKeyNames())[0]).ToString())
                        {
                            data.status.Add(true);
                            data.checkTime.Add(DateTime.Now);

                        }
                        else
                        {
                            data.status.Add(false);
                            data.checkTime.Add(DateTime.Now);
                        }
                    }
                    else
                    {
                        data.Path = GetParentKey(data.Path);
                        data.status.Add(false);
                        data.checkTime.Add(DateTime.Now);
                    }

                }
            }
            else if (data.Path.StartsWith(@"HKEY_LOCAL_MACHINE"))
            {
                string subKeyPath = data.Path.Replace(@"HKEY_CURRENT_USER\", "");
                using (RegistryKey subKey = Registry.CurrentUser.OpenSubKey(subKeyPath))
                {
                    if (subKey != null)
                    {
                        if (data.Quantity == (subKey.SubKeyCount)
                        && data.lastWriteTime[data.lastWriteTime.Count - 1] == DateTime.FromFileTimeUtc((long)(int)subKey.GetValue("LastWriteTime", 0))
                        )//&& data.Size[data.Size.Count - 1] == subKey.GetValue((subKey.GetSubKeyNames())[0]).ToString())
                        {
                            data.status.Add(true);
                            data.checkTime.Add(DateTime.Now);
                            data.lastWriteTime.Add(dateZero);
                        }
                        else
                        {
                            data.status.Add(false);
                            data.checkTime.Add(DateTime.Now);
                            data.lastWriteTime.Add(dateZero);
                        }
                    }
                    else
                    {
                        data.Path = GetParentKey(data.Path);
                        data.status.Add(false);
                        data.checkTime.Add(DateTime.Now);
                    }
                }
            }
        }
        return data;
    }

    static async Task Main()
    {
        string PathToJsonFolder = "\\\\SERV\\programm";
        string computerName = System.Environment.MachineName + ".json";
        string PathToJsonFile = Path.Combine(PathToJsonFolder, computerName);
        Console.WriteLine(PathToJsonFile);
        FilesFoldersAndRegStrings firstday = new FilesFoldersAndRegStrings();
        while (true)
        {
            List<DayLevel> _days = new List<DayLevel>();
            if (File.Exists(PathToJsonFile))
            {

                _days = DayLevel.LoadFromFile(PathToJsonFile);
                if (_days[_days.Count - 1].Date != (DateOnly.FromDateTime(DateTime.Now)).ToString())
                {
                    _days.Add(createDays(_days[_days.Count - 1]));
                    await Task.Delay(1000);
                }
                else if (_days[_days.Count - 1].Date == (DateOnly.FromDateTime(DateTime.Now)).ToString())
                {
                    foreach (var data in _days[_days.Count - 1].dataStorage)
                    {
                        (_days[_days.Count - 1]).AddOrUpdateData(check(data));
                        DayLevel.SaveToFile(_days, PathToJsonFile);
                    }
                }

                await Task.Delay(60000);
            }
            else
            {
                _days.Add(createFirstDay(firstday.allPathes()));
                DayLevel.SaveToFile(_days, PathToJsonFile);
            }
        }


    }
}
