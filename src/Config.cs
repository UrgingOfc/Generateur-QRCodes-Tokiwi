using Newtonsoft.Json;

namespace Tokiwi {
    public class Config {
        public string siteQRCodes;
        public string nomTableauIdentifiants;
    }

    public class ConfigFile {
        private static Config config;

        public static Config getConfig() {
            return config;
        }

        public static void loadJson() {
            try {
                StreamReader r = new StreamReader("config.json");
                string json = r.ReadToEnd();
                Config configG = JsonConvert.DeserializeObject<Config>(json);
                config = configG;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}