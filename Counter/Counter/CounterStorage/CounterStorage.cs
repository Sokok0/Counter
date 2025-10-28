using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Counter
{
    public static class CounterStorage
    {
        private static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "counters.xml");

        public static void Save(ObservableCollection<CounterViewModel> counters)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<CounterViewModel>));
            using var writer = new StreamWriter(FilePath);
            serializer.Serialize(writer, counters);
        }

        public static ObservableCollection<CounterViewModel> Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return new ObservableCollection<CounterViewModel>();

                var serializer = new XmlSerializer(typeof(ObservableCollection<CounterViewModel>));
                using var reader = new StreamReader(FilePath);
                return (ObservableCollection<CounterViewModel>)serializer.Deserialize(reader);
            }
            catch
            {
                return new ObservableCollection<CounterViewModel>();
            }
        }

        public static void DeleteStorage()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}