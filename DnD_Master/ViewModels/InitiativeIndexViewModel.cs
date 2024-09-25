using DnD_Master.Models;
using NuGet.DependencyResolver;

namespace DnD_Master.ViewModels
{
    public class InitiativeIndexViewModel
    {
        public List<Monster> Monster { get; set; }
        public List<Character> Character { get; set; }
        public Scene Scene { get; set; }

        // Объединенный и отсортированный список сущностей
        public List<object> SortedQueue => GetSortedEntities(Monster, Character);

        // Добавляем свойство SceneName
        public string SceneName
        {
            get => Scene?.SceneName;
            set { if (Scene == null) { Scene = new Scene(); } Scene.SceneName = value; }
        }

        // Метод для получения отсортированного списка сущностей
        public static List<object> GetSortedEntities(List<Monster> monsters, List<Character> characters)
        {
            return characters.Cast<object>()
                .Concat(monsters)
                .OrderByDescending(c => (c is Character character ? character.Initiative : ((Monster)c).Initiative))
                .ToList();
        }
    }
}
