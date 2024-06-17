using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace SaveUp
{
    public static class ItemService
    {
        private static readonly string FileName = Path.Combine(FileSystem.AppDataDirectory, "items.json");

        public static List<Item> LoadItems()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<List<Item>>(json);
            }
            return new List<Item>();
        }

        public static async Task SaveItemAsync(Item item)
        {
            var items = LoadItems();
            items.Add(item);
            var json = JsonSerializer.Serialize(items);
            await File.WriteAllTextAsync(FileName, json);
        }

        public static void DeleteItem(Item item)
        {
            var items = LoadItems();
            var itemToDelete = items.FirstOrDefault(i => i.Description == item.Description && i.Price == item.Price && i.Date == item.Date);
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
                var json = JsonSerializer.Serialize(items);
                File.WriteAllText(FileName, json);
            }
        }

        public static void ClearAllItems()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }
    }
}
