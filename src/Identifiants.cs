using CsvHelper.Configuration;

namespace Tokiwi {
    public class Identifiants {
        public string identifiant { get; set; }
    }

    public class IdentifiantsMap : ClassMap<Identifiants> {
        public IdentifiantsMap() {
            Map(m => m.identifiant).Name(ConfigFile.getConfig().nomTableauIdentifiants);
        }
    }
}