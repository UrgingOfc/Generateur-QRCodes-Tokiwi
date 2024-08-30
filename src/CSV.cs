namespace Tokiwi {
    public class CSV {
        private string identifiant;

        public CSV(string identifiant) {
            this.identifiant = identifiant;
        }

        

        public string getUserURL() {
            return $"https://example.com/{this.identifiant}";
        }
    }
}