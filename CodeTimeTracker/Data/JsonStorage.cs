using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using CodeTimeTracker.Data.Models;

namespace CodeTimeTracker.Data
{
    public static class JsonStorage
    {
        private static readonly string AppDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CodeTimeTracker");

        private static readonly string DataFilePath = Path.Combine(AppDataFolder, "time-tracker-data.json");

        public static TimeTrackerData Load()
        {
            EnsureDirectoryExists();

            if (!File.Exists(DataFilePath))
            {
                return new TimeTrackerData();
            }

            try
            {
                string json = File.ReadAllText(DataFilePath);
                JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<TimeTrackerData>(json, options);
                return data ?? new TimeTrackerData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}. Starting with empty data.");
                return new TimeTrackerData();
            }
        }

        public static void Save(TimeTrackerData data)
        {
            if (data == null) return;

            EnsureDirectoryExists();

            try
            {
                JsonSerializerOptions options = new()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };
                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(DataFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public static void ExportToTxt(string filePath, TimeTrackerData data, Guid? projectId = null)
        {
            using StreamWriter writer = new(filePath);
            writer.WriteLine("=====================================");
            writer.WriteLine("         CodeTime Tracker Report     ");
            writer.WriteLine($"         Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}         ");
            writer.WriteLine("=====================================");
            writer.WriteLine();

            var projectsToExport = projectId.HasValue
                ? data.Projects.Where(p => p.Id == projectId.Value)
                : data.Projects;

            if (!projectsToExport.Any())
            {
                writer.WriteLine("No projects found to export.");
                return;
            }

            foreach (var project in projectsToExport.OrderBy(p => p.Name))
            {
                writer.WriteLine($"Project: {project.Name}");
                writer.WriteLine($"  Created: {project.CreatedAt:yyyy-MM-dd}");
                writer.WriteLine("  ───────────────────────────────────");

                var codeObjects = data.CodeObjects
                    .Where(co => co.ProjectId == project.Id)
                    .OrderBy(co => co.Name);

                if (!codeObjects.Any())
                {
                    writer.WriteLine("  (No code objects in this project)");
                    writer.WriteLine();
                    continue;
                }

                foreach (var codeObj in codeObjects)
                {
                    writer.WriteLine($"  Code Object: {codeObj.Name}  ({codeObj.Type})");
                    writer.WriteLine($"    Created: {codeObj.CreatedAt:yyyy-MM-dd}");

                    List<TimeEntry> entries = data.TimeEntries
                        .Where(e => e.CodeObjectId == codeObj.Id && !e.IsDeleted)
                        .OrderBy(e => e.StartTime)
                        .ToList();

                    if (!entries.Any())
                    {
                        writer.WriteLine("    (No time entries)");
                        writer.WriteLine();
                        continue;
                    }

                    TimeSpan codeObjTotal = TimeSpan.Zero;

                    foreach (var entry in entries)
                    {
                        string endTimeStr = entry.EndTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Still running";
                        writer.WriteLine($"    Task: {entry.TaskName}");
                        writer.WriteLine($"      Start: {entry.StartTime:yyyy-MM-dd HH:mm:ss}");
                        writer.WriteLine($"      End:   {endTimeStr}");
                        writer.WriteLine($"      Duration: {entry.DurationFormatted} ({entry.Duration.TotalHours:F2} hours)");
                        if (!string.IsNullOrWhiteSpace(entry.Notes))
                            writer.WriteLine($"      Notes: {entry.Notes}");
                        writer.WriteLine();

                        codeObjTotal += entry.Duration;
                    }

                    writer.WriteLine($"  Total time on {codeObj.Name}: {FormatTotalTime(codeObjTotal)}");
                    writer.WriteLine();
                }

                TimeSpan overallProjectTotal = codeObjects
                    .SelectMany(co => data.TimeEntries.Where(e => e.CodeObjectId == co.Id && !e.IsDeleted))
                    .Aggregate(TimeSpan.Zero, (sum, e) => sum + e.Duration);

                writer.WriteLine($"Total Project Time: {FormatTotalTime(overallProjectTotal)}");
                writer.WriteLine("─────────────────────────────────────");
                writer.WriteLine();
            }

            writer.WriteLine("End of Report");
        }

        public static void ExportToCsv(string filePath, TimeTrackerData data, Guid? projectId = null)
        {
            using StreamWriter writer = new(filePath);
            writer.WriteLine("Project,CodeObject,CodeObjectType,TaskName,StartTime,EndTime,DurationHours,DurationFormatted,Notes");

            var filteredEntries = projectId.HasValue
                ? data.TimeEntries.Where(e =>
                    data.CodeObjects.Any(co => co.Id == e.CodeObjectId && co.ProjectId == projectId.Value) &&
                    !e.IsDeleted)
                : data.TimeEntries.Where(e => !e.IsDeleted);

            foreach (var entry in filteredEntries.OrderBy(e => e.StartTime))
            {
                var codeObj = data.CodeObjects.Find(co => co.Id == entry.CodeObjectId);
                var project = codeObj != null ? data.Projects.Find(p => p.Id == codeObj.ProjectId) : null;

                string projectName = project?.Name ?? "Unknown";
                string codeObjName = codeObj?.Name ?? "Unknown";
                string codeObjType = codeObj?.Type ?? "";

                writer.WriteLine(
                    $"\"{projectName}\",\"{codeObjName}\",\"{codeObjType}\",\"{entry.TaskName}\"," +
                    $"\"{entry.StartTime:yyyy-MM-dd HH:mm:ss}\"," +
                    $"\"{(entry.EndTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "")}\"," +
                    $"{entry.Duration.TotalHours:F2},\"{entry.DurationFormatted}\",\"{entry.Notes?.Replace("\"", "\"\"")}\""
                );
            }
        }

        private static string FormatTotalTime(TimeSpan ts)
        {
            return $"{(ts.Days * 24) + ts.Hours:D2} hours and {ts.Minutes:D2} minutes";
        }

        private static void EnsureDirectoryExists()
        {
            Directory.CreateDirectory(AppDataFolder);
        }
    }
}