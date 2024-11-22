using Microsoft.Win32;

class FilesFoldersAndRegStrings
{
    private List<string> regStrings = new List<string> {
        @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
        @"HKEY_CURRENT_USER\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run",
        @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows\Run",
        @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce",
        @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnceEx",
        @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\RunServices",
        @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\RunServicesOnce",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnceEx",
        @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunServices",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\RunServicesOnce",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Active Setup\Installed Components",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Active Setup\Installed Components",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellServiceObjects",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Explorer\ShellServiceObjects",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\ShellServiceObjectDelayLoad",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\ShellServiceObjectDelayLoad",
        @"HKEY_CURRENT_USER\Software\Classes\*\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\*\ShellEx\ContextMenuHandlers",
        @"HKEY_CURRENT_USER\Software\Classes\Drive\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\Drive\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Classes\*\ShellEx\PropertySheetHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\*\ShellEx\PropertySheetHandlers",
        @"HKEY_CURRENT_USER\Software\Classes\Directory\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Classes\Directory\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\Directory\ShellEx\ContextMenuHandlers",
        @"HKEY_CURRENT_USER\Software\Classes\Directory\Shellex\DragDropHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Classes\Directory\Shellex\DragDropHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\Directory\Shellex\DragDropHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Classes\Directory\Shellex\CopyHookHandlers",
        @"HKEY_CURRENT_USER\Software\Classes\Directory\Background\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Classes\Directory\Background\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\Directory\Background\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Classes\Folder\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\Folder\ShellEx\ContextMenuHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Classes\Folder\ShellEx\DragDropHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\Folder\ShellEx\DragDropHandlers",
        @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers",
        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Font Drivers",
        @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\Drivers32",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Microsoft\Windows NT\CurrentVersion\Drivers32",
        @"HKEY_LOCAL_MACHINE\Software\Classes\Filter",
        @"HKEY_LOCAL_MACHINE\Software\Classes\CLSID\{083863F1-70DE-11d0-BD40-00A0C911CE86}\Instance",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\CLSID\{083863F1-70DE-11d0-BD40-00A0C911CE86}\Instance",
        @"HKEY_LOCAL_MACHINE\Software\Classes\CLSID\{7ED96837-96F0-4812-B211-F13C24117ED3}\Instance",
        @"HKEY_LOCAL_MACHINE\Software\Wow6432Node\Classes\CLSID\{7ED96837-96F0-4812-B211-F13C24117ED3}\Instance",
        @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\KnownDlls",
        @"HKEY_CURRENT_USER\Control Panel\Desktop\Scrnsave.exe",
        @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinSock2\Parameters\Protocol_Catalog9\Catalog_Entries",
        @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinSock2\Parameters\Protocol_Catalog9\Catalog_Entries64",
        @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run",
        @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\Run"
    };
    static private string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    static private string pathToUserFolder = Path.Combine(appdataPath, "Microsoft\\Windows\\Start Menu\\Programs\\Startup");
    private List<string> Registry_File_Paths()
    {
        List<string> reg_Paths = new List<string>();
        foreach (var path0 in Directory.GetDirectories("C:\\Users"))
        {
            try
            {
                foreach (var path in Directory.GetFiles(path0))
                {
                    if (File.Exists(path))
                    {
                        reg_Paths.Add(path);
                    }
                    else
                    {
                        Console.WriteLine("user path error");
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        foreach (var path in Directory.GetFiles("C:\\Windows\\ServiceProfiles\\LocalService"))
        {
            if (File.Exists(path))
            {
                reg_Paths.Add(path);
            }
        }
        foreach (var path in Directory.GetFiles("C:\\Windows\\ServiceProfiles\\NetworkService"))
        {
            if (File.Exists(path))
            {
                reg_Paths.Add(path);
            }
        }

        foreach (var path in Directory.GetFiles("C:\\Windows\\System32\\config"))
        {
            if (File.Exists(path))
            {
                reg_Paths.Add((string)path);
            }
            else
            {
                Console.WriteLine("HKLM path error");
            }
        }
        return reg_Paths;
    }
    private List<string> Task_Sheduler_Files_Path()
    {
        List<string> tS_Files_Path = new List<string>();
        foreach (var path in Directory.GetFiles("C:\\Windows\\System32\\Tasks"))
        {
            if (File.Exists(path))
            {
                tS_Files_Path.Add(path);
            }
            else
            {
                Console.WriteLine("user path error");
            }
        }
        return tS_Files_Path;
    }
    private List<string> Task_Sheduler_Dirs_Path()
    {
        List<string> tS_Dirs_Path = new List<string>();
        foreach (var path in Directory.GetDirectories("C:\\Windows\\System32\\Tasks"))
        {

            if (Directory.Exists(path))
            {
                tS_Dirs_Path.Add(path);
            }
            else
            {
                Console.WriteLine("path error");
            }
        }
        return tS_Dirs_Path;
    }
    private List<string> DirsStartupFoldersPath()
    {
        List<string> dirs_Paths = new List<string>();
        foreach (var path0 in Directory.GetDirectories("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp"))
        {
            if (!Directory.Exists(path0))
            {
                dirs_Paths.Add(path0);
            }
            else
            {
                Console.WriteLine("Error with startup folder folders");
            }
        }
        foreach (var path0 in Directory.GetDirectories(pathToUserFolder))
        {
            if (!Directory.Exists(path0))
            {
                dirs_Paths.Add(path0);
            }
            else
            {
                Console.WriteLine(" Error with startup folder folders");
            }
        }
        return dirs_Paths;
    }
    private List<string> FilesStartupFoldersPath()
    {
        List<string> dirs_Paths = new List<string>();
        foreach (var path0 in Directory.GetFiles("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp"))
        {
            if (!Directory.Exists(path0))
            {
                dirs_Paths.Add(path0);
            }
            else
            {
                Console.WriteLine(" Error with startup folder folders");
            }
        }
        foreach (var path0 in Directory.GetFiles(pathToUserFolder))
        {
            if (!Directory.Exists(path0))
            {
                dirs_Paths.Add(path0);
            }
            else
            {
                Console.WriteLine(" Error with startup folder folders");
            }
        }
        return dirs_Paths;
    }

    public List<path_with_comment> AllPathes()
    {
        List<path_with_comment> paths = new List<path_with_comment>();
        foreach (var str in regStrings)
        {
            paths.Add(new path_with_comment(str, "registry string"));
        }
        foreach (var str in Registry_File_Paths())
        {
            paths.Add(new path_with_comment(str, "file registry path"));
        }
        foreach (var str in Task_Sheduler_Files_Path())
        {
            paths.Add(new path_with_comment(str, "file task sheduler path"));
        }
        foreach (var str in FilesStartupFoldersPath())
        {
            paths.Add(new path_with_comment(str, "file startap folder path"));
        }
        foreach (var str in Task_Sheduler_Dirs_Path())
        {
            paths.Add(new path_with_comment(str, "dir task sheduler path"));
        }
        foreach (var str in DirsStartupFoldersPath())
        {
            paths.Add(new path_with_comment(str, "dir startap folder path"));
        }
        return paths;
    }
}
public class path_with_comment
{
    public string Path { get; set; }
    public string Comment { get; set; }
    public path_with_comment(string text, string comment)
    {
        Path = text;
        Comment = comment;
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
    public DataLevel(path_with_comment Path_comm)
    {
        DateTime dateZero = new DateTime();
        Path = Path_comm.Path;
        type = Path_comm.Comment;
        status.Add(true);
        checkTime.Add(DateTime.Now);
        if (Path_comm.Comment.Contains("file"))
        {
            if (File.Exists(Path_comm.Path))
            {
                Size.Add(new FileInfo(Path_comm.Path).Length.ToString());
                lastWriteTime.Add(File.GetLastWriteTime(Path_comm.Path));
                Quantity = 1;
            }
        }
        else if (Path_comm.Comment.Contains("dir"))
        {
            if (Directory.Exists(Path_comm.Path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path_comm.Path);
                Size.Add(Client.GetDirectorySize(Path_comm.Path).ToString());
                lastWriteTime.Add(directoryInfo.LastWriteTime);
                Quantity = directoryInfo.GetFiles().Length + directoryInfo.GetDirectories().Length;
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

                        Quantity = (subKey.SubKeyCount);
                        lastWriteTime.Add(DateTime.FromFileTimeUtc((long)(int)subKey.GetValue("LastWriteTime", 0)));
                    }
                    else
                    {
                        Path = Client.GetParentKey(Path_comm.Path);
                        Quantity = 0;
                        Size.Add("0");
                        lastWriteTime.Add(dateZero);
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
                        Quantity = (subKey.SubKeyCount);
                        lastWriteTime.Add(DateTime.FromFileTimeUtc((long)(int)subKey.GetValue("LastWriteTime", 0)));
                    }
                    else
                    {
                        Path = Client.GetParentKey(Path_comm.Path);
                        Quantity = 0;
                        Size.Add("0");
                        lastWriteTime.Add(dateZero);
                    }
                }
            }
            else
            {
                Quantity = 0;
            }
        }
    }

}

class Client
{
    static public long GetDirectorySize(string path)
    {
        long size = 0;
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        foreach (FileInfo file in files)
        {
            size += file.Length;
        }
        return size;
    }
    static public string GetParentKey(string fullKey)
    {
        int lastIndex = fullKey.LastIndexOf('\\');
        if (lastIndex != -1)
        {
            return fullKey.Substring(0, lastIndex);
        }
        return fullKey;
    }
    static async Task Main()
    {
        FilesFoldersAndRegStrings clientStartPaths = new FilesFoldersAndRegStrings();
        List<DataLevel> data = new List<DataLevel>();
    }
}
