using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


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

    private List<string> CollectPaths(Func<string, bool> existenceCheck, string rootPath, bool isDirectory = false)
    {
        List<string> paths = new List<string>();

        try
        {
            var entries = isDirectory ? Directory.GetDirectories(rootPath) : Directory.GetFiles(rootPath);
            foreach (var path in entries)
            {
                if (existenceCheck(path))
                {
                    paths.Add(path);
                }
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access denied: {ex.Message}");
        }

        return paths;
    }

    public List<string> GetAllPaths()
    {
        List<string> allPaths = new List<string>();

        allPaths.AddRange(regStrings);
        allPaths.AddRange(CollectPaths(File.Exists, "C:\\Windows\\ServiceProfiles\\LocalService"));
        allPaths.AddRange(CollectPaths(File.Exists, "C:\\Windows\\ServiceProfiles\\NetworkService"));
        allPaths.AddRange(CollectPaths(File.Exists, "C:\\Windows\\System32\\config"));
        allPaths.AddRange(CollectPaths(File.Exists, "C:\\Windows\\System32\\Tasks"));
        allPaths.AddRange(CollectPaths(Directory.Exists, "C:\\Windows\\System32\\Tasks", true));
        allPaths.AddRange(CollectPaths(File.Exists, "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp"));
        allPaths.AddRange(CollectPaths(File.Exists, pathToUserFolder));
        allPaths.AddRange(CollectPaths(Directory.Exists, "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp", true));
        allPaths.AddRange(CollectPaths(Directory.Exists, pathToUserFolder, true));
        return allPaths;
    }
}

public class DataLevel
{
    public string Path { get; set; }
    public List<string> Size { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public List<bool> Status { get; set; }
    public List<DateTime> CheckTime { get; set; }
    public List<DateTime> LastWriteTime { get; set; }
    public Dictionary<string, object> RegistryValues { get; set; }

    public DataLevel()
    {
        Size = new List<string>();
        Status = new List<bool>();
        CheckTime = new List<DateTime>();
        LastWriteTime = new List<DateTime>();
        RegistryValues = new Dictionary<string, object>();
    }
    public DataLevel(string path)
    {
        Size = new List<string>();
        Status = new List<bool>();
        CheckTime = new List<DateTime>();
        LastWriteTime = new List<DateTime>();
        RegistryValues = new Dictionary<string, object>();

        Path = path;
        CheckTime.Add(DateTime.Now);
        DetermineTypeAndProcess(path);
    }

    private void DetermineTypeAndProcess(string path)
    {
        if (File.Exists(path))
        {
            Type = "file";
            ProcessFile(path);
        }
        else if (Directory.Exists(path))
        {
            Type = "directory";
            ProcessDirectory(path);
        }
        else
        {
            Type = "registry";
            ProcessRegistry(path);
        }
    }

    private void ProcessFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            Size.Add(new FileInfo(filePath).Length.ToString());
            LastWriteTime.Add(File.GetLastWriteTime(filePath));
            Quantity = 1;
        }
    }

    private void ProcessDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            Size.Add(GetDirectorySize(directoryPath).ToString());
            LastWriteTime.Add(dirInfo.LastWriteTime);
            Quantity = dirInfo.GetFiles().Length + dirInfo.GetDirectories().Length;
        }
    }

    private void ProcessRegistry(string registryPath)
    {
        string rootKey = registryPath.Split('\\')[0];
        string subKeyPath = registryPath.Replace($"{rootKey}\\", "");
        Size.Add("0");

        RegistryKey rootRegistryKey = rootKey switch
        {
            "HKEY_CURRENT_USER" => Registry.CurrentUser,
            "HKEY_LOCAL_MACHINE" => Registry.LocalMachine,
            _ => null
        };

        if (rootRegistryKey != null)
        {
            string currentPath = subKeyPath;
            while (!string.IsNullOrEmpty(currentPath))
            {
                using (RegistryKey subKey = rootRegistryKey.OpenSubKey(currentPath))
                {
                    if (subKey != null)
                    {
                        // Успешно открыли ключ
                        Quantity = subKey.SubKeyCount;
                        foreach (string valueName in subKey.GetValueNames())
                        {
                            RegistryValues[valueName] = subKey.GetValue(valueName);
                        }
                        LastWriteTime.Add(DateTime.MinValue);
                    
                        Status.Add(true); // Обновление статуса как успешного
                        return; // Завершаем метод
                    }
                }

                // Если текущий путь недоступен, поднимаемся на уровень выше
                int lastSeparatorIndex = currentPath.LastIndexOf('\\');
                if (lastSeparatorIndex > 0)
                {
                    currentPath = currentPath.Substring(0, lastSeparatorIndex);
                }
                else
                {
                    // Если больше некуда подниматься, обнуляем путь
                    currentPath = string.Empty;
                }
            }
        }
        // Если дошли сюда, значит ключ не найден
        Status.Add(false);
    }


    private long GetDirectorySize(string folderPath)
    {
        return Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                        .Select(f => new FileInfo(f).Length)
                        .Sum();
    }
}
public class DayLevel
{
    public DateOnly Day { get; set; }
    public List<DataLevel> Data { get; set; }

    public DayLevel(List<DataLevel> data)
    {
        this.Day = DateOnly.FromDateTime(DateTime.Now);
        this.Data = data;
    }
}
public class ClientLevel
{
    public string ClientName { get; set; }
    public List<DayLevel> Days { get; set; }

    public ClientLevel()
    {
        this.ClientName = "client1";
        this.Days = new List<DayLevel>();
    }

    public ClientLevel(string name, List<DayLevel> days)
    {
        this.ClientName = name;
        this.Days = days;
    }
}

class Client
{
    private static async Task SendClientLevel(ClientWebSocket webSocket, ClientLevel client)
    {
        string jsonData = JsonConvert.SerializeObject(client);
        var buffer = Encoding.UTF8.GetBytes(jsonData);
        var segment = new ArraySegment<byte>(buffer);
        await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
    }
    static string GetLocalIPAddress()
    {
        string localIP = string.Empty;
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                // Check for IPv4 addresses only
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break; // Return the first IPv4 address found
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
        return localIP;
    }
    public static bool IsDeserializable<T>(string input)
    {
        try
        {
            JsonConvert.DeserializeObject<T>(input);
            return true;
        }
        catch
        {
            return false;
        }
    }
    private static async Task ReceiveMessages(ClientWebSocket webSocket)
    {
        var buffer = new byte[1024 * 64];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        ClientLevel clnt = new ClientLevel();
        clnt.ClientName = GetLocalIPAddress();
        Console.WriteLine(GetLocalIPAddress());
        try
        {
            List<DataLevel> data = new List<DataLevel>();
            //Console.WriteLine(message);

            if (message == "Are you there?")
            {
                await SocketExtensions.SendTextMessageAsync(webSocket, GetLocalIPAddress().ToString());
                
                return;
            }
            if (message == "first run")
            {
                
                FilesFoldersAndRegStrings paths = new FilesFoldersAndRegStrings();
                foreach (var path in paths.GetAllPaths())
                {
                    data.Add(new DataLevel(path));
                }
                clnt.Days.Add(new DayLevel(data));
                await SendClientLevel(webSocket, clnt);
                return;
            }
            if (IsDeserializable<List<string>>(message))
            {
                List<string> strings = new List<string>();
                
                strings = JsonConvert.DeserializeObject<List<string>>(message);
                foreach (var path in strings)
                {
                    data.Add(new DataLevel(path));
                }
                clnt.Days.Add(new DayLevel(data));
                await SendClientLevel(webSocket, clnt);
                Console.WriteLine("rar");
            }

        }
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON message received.");
        }
    }

    private static async Task ConnectWithRetry(string serverUri)
    {
        while (true)
        {
            ClientWebSocket webSocket = new ClientWebSocket(); // Новый экземпляр WebSocket при каждой попытке подключения

            try
            {
                await webSocket.ConnectAsync(new Uri(serverUri), CancellationToken.None);
                Console.WriteLine("Connected to the WebSocket server.");

                // Если соединение успешно, начинаем цикл получения сообщений
                while (webSocket.State == WebSocketState.Open)
                {
                    await ReceiveMessages(webSocket);
                }
            }
            catch (WebSocketException)
            {
                Console.WriteLine("Server not available. Retrying in 5 seconds...");
                await Task.Delay(5000);
            }
            finally
            {
                // Закрываем WebSocket перед следующей попыткой подключения
                if (webSocket.State != WebSocketState.Closed && webSocket.State != WebSocketState.Aborted)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
            }
        }
    }


    static async Task Main()
    {
        await ConnectWithRetry("ws://localhost:5000/");
    }
}
