using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
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
        private List<string> registry_File_Paths()
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
        private List<string> task_Sheduler_Files_Path()
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
        private List<string> task_Sheduler_Dirs_Path()
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
        private List<string> dirsStartupFoldersPath()
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

        public List<path_with_comment> allPathes()
        {
            List<path_with_comment> paths = new List<path_with_comment>();
            foreach (var str in regStrings)
            {
                paths.Add(new path_with_comment(str, "registry string"));
            }
            foreach (var str in registry_File_Paths())
            {
                paths.Add(new path_with_comment(str, "file registry path"));
            }
            foreach (var str in task_Sheduler_Files_Path())
            {
                paths.Add(new path_with_comment(str, "file task sheduler path"));
            }
            foreach (var str in FilesStartupFoldersPath())
            {
                paths.Add(new path_with_comment(str, "file startap folder path"));
            }
            foreach (var str in task_Sheduler_Dirs_Path())
            {
                paths.Add(new path_with_comment(str, "dir task sheduler path"));
            }
            foreach (var str in dirsStartupFoldersPath())
            {
                paths.Add(new path_with_comment(str, "dir startap folder path"));
            }
            return paths;
        }
    }
    class path_with_comment
    {
        public string Path { get; set; }
        public string Comment { get; set; }
        public path_with_comment(string text, string comment)
        {
            Path = text;
            Comment = comment;
        }
    }
    internal class AllPaths
    {
        public string server;
        public List<path_with_comment> clientPaths;
        public AllPaths(string serv)
        {
            FilesFoldersAndRegStrings allClientPaths = new FilesFoldersAndRegStrings();
            clientPaths = allClientPaths.allPathes();
            server = serv;
        }
        public AllPaths (string serv, List<path_with_comment> Paths)
        {
            clientPaths = Paths;
            server = serv;
        }
    }
}
