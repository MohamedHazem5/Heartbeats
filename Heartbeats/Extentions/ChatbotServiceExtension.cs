using System.Diagnostics;

namespace Heartbeats.Extentions
{
    public static class ChatbotServiceExtension
    {
        public static void StartChatbot(this IServiceCollection services)
        {
            // Get the path to the folder where the program.cs file is located
            var basePath = AppContext.BaseDirectory;
            Console.WriteLine(basePath);

            // Navigate to the Chatbot folder relative to program.cs
            var chatbotFolderPath = Path.Combine(basePath, "..", "..", "..", "Chatbot");
            Console.WriteLine(chatbotFolderPath);
            // Setting up the shell command to activate the virtual environment and start the chatbot
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"cd /d {chatbotFolderPath} && .\\venv\\Scripts\\activate && chainlit run .\\app.py -w -h\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            try
            {
                var process = new Process { StartInfo = startInfo };
                process.Start();

                Console.WriteLine($"Started Python chatbot with PID {process.Id}");

                // Optional: read output or error streams if needed
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine("Error: " + e.Data);

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start Python chatbot: {ex.Message}");
            }
        }
    }
}