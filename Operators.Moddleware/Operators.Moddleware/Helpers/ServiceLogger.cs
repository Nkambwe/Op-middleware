namespace Operators.Moddleware.Helpers {
 
    public class ServiceLogger : IServiceLogger {
 
        #region Fields
 
        private readonly string _fileName;
        private readonly bool _isFolder = false;
 
        #endregion
 
        public string Channel { set; get; }
        public string Id { set; get; }
 
        public ServiceLogger() { }
 
        public ServiceLogger(string name, bool isFolder = false) {
            _fileName = name;
            _isFolder = isFolder;

        }
 
        public void LogToFile(string message, string type = "MSG") {

            string filepath;
            EventWaitHandle waitHandle = null;
 
            // ... processing time
            var date = DateTime.Now;
            var shortDate2 = date.ToString("yyyy-MM-dd");

            try {

                waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "operators_logger_02022025");
                waitHandle.WaitOne();
 
 
                char[] delim = { ' ' };
                var tZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
                var words = tZone.StandardName.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                string abbrev = string.Empty;

                foreach (string chaStr in words) {
                    abbrev += chaStr[0];
                }
 
                string filename = $"operators_{(_fileName != null ? (_isFolder ? "" : _fileName) : " ")}_{shortDate2}.log";
                 filepath = ApplicationUtils.ISLIVE ?
                    $@"E:\AppLogs\Logs\Operators\{(_isFolder ? _fileName : "Activity_Log")}":
                    $@"C:\AppLogs\Logs\Operators\{(_isFolder ? _fileName : "Activity_Log")}";
 
                //..create directory if not found
                if (!Directory.Exists(filepath)) {
                    Directory.CreateDirectory(filepath);
                }
 
                //..file path
                filepath = $@"{filepath}\{filename}";
                if (!File.Exists(filepath)) {
                    File.Create(filepath).Close();
                }
 
                var timeIn = $"{date:yyyy.MM.dd HH:mm:ss} {abbrev}";
                string messageToLog = $"[{timeIn}]: [{type}]\t\t {(Channel != null ? $"CHANNEL: {Channel}\t" : "")} {(Id != null ? Id + "\t" : "")} {message}";
                using (var file = new StreamWriter(filepath, true)) {
                    file.WriteLine(messageToLog);
                }
 
            } catch (Exception ex) {
 
                try {

                    string filename = $"operators_{(_fileName != null ? (_isFolder ? "" : _fileName) : " ")}_{shortDate2}.log";
                    if (ApplicationUtils.ISLIVE) {
                        filepath = $@"E:\AppLogs\Logs\Operators\{(_isFolder ? _fileName : "Activity_Log")}";
                    } else {
                        filepath = $@"C:\AppLogs\Logs\Operators\{(_isFolder ? _fileName : "Activity_Log")}";
                    }
 
                    if (!Directory.Exists(filepath)) {
                        Directory.CreateDirectory(filepath);
                    }
 
                    using (var file = new StreamWriter(filepath, true)) {
                        file.WriteLine($"LOG ERROR :: An error occurred while creating log file.");
                        file.WriteLine($"{ex.Message}");
                    }
 
                } catch (Exception ex1) {
 
                    try {

                        string filename = $"operators_{(_fileName != null ? (_isFolder ? "" : _fileName) : " ")}_{shortDate2}.log";

                        if (ApplicationUtils.ISLIVE) {
                            filepath = $@"E:\AppLogs\Logs\Operators\{(_isFolder ? _fileName : "Activity_Log")}";
                        } else {
                            filepath = $@"C:\AppLogs\Logs\Operators\{(_isFolder ? _fileName : "Activity_Log")}";
                        }
 
                        if (!Directory.Exists(filepath)) {
                            Directory.CreateDirectory(filepath);
                        }
 
                        using (var file = new StreamWriter(filepath, true)) {
                            file.WriteLine($"LOG ERROR :: An error occurred while creating log file.");
                            file.WriteLine($"{ex1.Message}");
                        }

                    } catch (Exception) {
                        return;
                    }
 
                }

            } finally {
                waitHandle?.Set();
            }

        }

    }
}
