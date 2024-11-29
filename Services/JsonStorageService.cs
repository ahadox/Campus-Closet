using System.Text.Json;
using System.IO;
using Campuscloset.Models;
using System.Collections.Generic;

namespace Campuscloset.Services
{
    public class JsonStorageService
    {
        private readonly string settingsFilePath;
        private readonly string itemsFilePath;

        public JsonStorageService()
        {
            // Define file paths for storage
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            settingsFilePath = Path.Combine(appDataPath, "user_settings.json");
            itemsFilePath = Path.Combine(appDataPath, "posted_items.json");
        }

        // Save User Settings
        public void SaveUserSettings(UserSettings settings)
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsFilePath, json);
        }

        // Load User Settings
        public UserSettings LoadUserSettings()
        {
            if (!File.Exists(settingsFilePath))
                return new UserSettings(); // Default if no file exists

            var json = File.ReadAllText(settingsFilePath);
            return JsonSerializer.Deserialize<UserSettings>(json);
        }

        // Save Posted Items
        public void SavePostedItems(List<PostedItem> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(itemsFilePath, json);
        }

        // Load Posted Items
        public List<PostedItem> LoadPostedItems()
        {
            if (!File.Exists(itemsFilePath))
                return new List<PostedItem>(); // Default empty list

            var json = File.ReadAllText(itemsFilePath);
            return JsonSerializer.Deserialize<List<PostedItem>>(json);
        }
    }
}
