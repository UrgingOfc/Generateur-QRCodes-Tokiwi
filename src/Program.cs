using System.Drawing;
using System.Globalization;
using System.IO.Compression;
using CsvHelper;
using CsvHelper.Configuration;
using NDesk.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Tokiwi {
    public class Program {
        public static void Main(string[] args) {
            bool help = false;
            ConfigFile.loadJson();
            var options = new OptionSet() {
                { "csvPath=", "Le chemin de votre fichier CSV", csv => Data.csvFilePath = csv },
                { "zipName=", "Le nom du fichier ZIP", zipName => Data.zipName = zipName },
                { "outputPath=", "Le chemin du fichier ZIP avec les QR-Codes", outputPath => Data.outputFilePath = outputPath },
                { "help", "Obtenir de l'aide pour utilizer cette application", v => help = true },
            };
            List<string> extra;
            try {
                extra = options.Parse(args);
            } catch (OptionException e) {
                Console.WriteLine("Erreure avec les flags");
                Console.WriteLine(e.Message);
                Console.WriteLine("Veuillez essayer le flag '--help' pour obtenir de l'aide.");
            }

            if (help) {
                showHelp();
            } else {
                if (Data.csvFilePath != null || Data.zipName != null || Data.outputFilePath != null) {
                    writeCopyrightNotice();
                    startGeneration();
                } else {
                    startProgram();
                }
            }
        }

        private static void showHelp() {
            writeCopyrightNotice();
            Console.WriteLine("Voici les flags que vous pouvez utilizer pour générer les QR-Codes:");
            Console.WriteLine("--csvPath | Le chemin de votre fichier CSV avec les identifiants.");
            Console.WriteLine("--zipName | Le nom que vous allez donner pour votre fichier ZIP.");
            Console.WriteLine("--outputPath | Le chemin dans lequel votre fichier ZIP sera sauvegardé.");
            Console.WriteLine("");
            Console.WriteLine("Vous pouvez aussi lancer l'application sans aucun flag pour débuter les étapes.");
        }

        private static void startProgram() {
            Data.csvFilePath = null;
            Data.zipName = null;
            Data.outputFilePath = null;
            writeCopyrightNotice();
            Console.WriteLine("Étape Nº 1: Choisir le chemin du fichier CSV");
            Console.WriteLine("Veuillez coller le chemin de votre fichier CSV avec les identifiants des utilisateurs.");
            Console.WriteLine("Chemin Fichier CSV: ");
            string csvFilePath = Console.ReadLine();
            if (csvFilePath != null || csvFilePath != "" || !String.IsNullOrEmpty(csvFilePath) || !String.IsNullOrWhiteSpace(csvFilePath)) {
                Data.csvFilePath = csvFilePath;
                writeCopyrightNotice();
                Console.WriteLine("Étape Nº 2: Choisir le nom du ZIP à générer pour les QR-Codes");
                Console.WriteLine("Veuillez introduire le nom du fichier ZIP pour générer les QR-Codes.");
                Console.WriteLine("Nom du ZIP: ");
                string zipName = Console.ReadLine();
                if (zipName != null || zipName != "" || !String.IsNullOrEmpty(zipName) || !String.IsNullOrWhiteSpace(zipName)) {
                    writeCopyrightNotice();
                    Console.WriteLine("Étape Nº 3: Choisir le chemin de sortie");
                    Console.WriteLine("Veuillez coller le chemin de sortie de votre QR-Code.");
                    Console.WriteLine("Chemin Sortie QR-Code: ");
                    string outputFilePath = Console.ReadLine();
                    if (outputFilePath != null || outputFilePath != "" || !String.IsNullOrEmpty(outputFilePath) || !String.IsNullOrWhiteSpace(outputFilePath)) {
                        Data.outputFilePath = outputFilePath;
                        writeCopyrightNotice();
                        Console.WriteLine("En génération des QR-Code...");
                        startGeneration();
                    } else {
                        writeCopyrightNotice();
                        Console.WriteLine("Vous devez coller le chemin de sortie de votre QR-Code pour que l'application puisse le générer.");
                        Console.WriteLine("Appuyez sur une touche pour recommancer.");
                        Console.ReadKey();
                        startProgram();
                    }
                } else {
                    writeCopyrightNotice();
                    Console.WriteLine("Vous devez coller le nom du ZIP pour que l'application puisse générer un QR-Code.");
                    Console.WriteLine("Appuyez sur une touche pour recommancer.");
                    Console.ReadKey();
                    startProgram();
                }
            } else {
                writeCopyrightNotice();
                Console.WriteLine("Vous devez coller le chemin de votre fichier CSV pour que l'application puisse générer un QR-Code.");
                Console.WriteLine("Appuyez sur une touche pour recommancer.");
                Console.ReadKey();
                startProgram();
            }
        }

        private static void writeCopyrightNotice() {
            Console.Clear();
            Console.WriteLine("=============================");
            Console.WriteLine("Générateur QR-Code Tokiwi");
            Console.WriteLine("Programmé par: Diogo");
            Console.WriteLine("=============================");
            Console.WriteLine("");
        }

        private static void startGeneration() {
            int codesGenere = 0;
            Config configg = ConfigFile.getConfig();
            try {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                config.HasHeaderRecord = true;               
                StreamReader streamReader = new StreamReader(@$"{Data.csvFilePath}");
                CsvReader csvReader = new CsvReader(streamReader, config);
                csvReader.Context.RegisterClassMap<IdentifiantsMap>();
                csvReader.Read();
                csvReader.ReadHeader();
                string[] headerRow = csvReader.HeaderRecord;
                foreach (string header in headerRow) {
                    if (header == configg.nomTableauIdentifiants) {
                        IEnumerable<Identifiants> fields = csvReader.GetRecords<Identifiants>();
                        foreach (Identifiants field in fields) {
                            CodeQR qrCode = new CodeQR(configg, field.identifiant);
                            qrCode.generateQRCode();
                            byte[] qrCodeByte = qrCode.getQRCode();
                            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(qrCodeByte);
                            image.Mutate(x => x.Grayscale());
                            if (!Directory.Exists($"{Data.outputFilePath}/{Data.zipName}")) {
                                Directory.CreateDirectory($"{Data.outputFilePath}/{Data.zipName}");
                            }
                            image.SaveAsPng(Path.Combine($"{Data.outputFilePath}/{Data.zipName}", $"qrcode-{field.identifiant}.png"));
                            Console.WriteLine($"Le QR-Code de {field.identifiant} a bien été généré!");
                            codesGenere++;
                        }
                    }
                }

                if (codesGenere > 0) {
                    if (File.Exists(Path.Combine($"{Data.outputFilePath}", $"{Data.zipName}.zip"))) {
                        File.Delete(Path.Combine($"{Data.outputFilePath}", $"{Data.zipName}.zip"));
                    }
                    ZipFile.CreateFromDirectory($"{Data.outputFilePath}/{Data.zipName}", Path.Combine($"{Data.outputFilePath}", $"{Data.zipName}.zip"));   
                    Directory.Delete($"{Data.outputFilePath}/{Data.zipName}", true);
                    Console.WriteLine("");
                    Console.WriteLine("Les QR-Codes ont bien été généré!");
                    Console.WriteLine("Totale Codes Générés: " + codesGenere);
                } else {
                    Console.WriteLine("Aucun code n'a pas été généré!");
                    Console.WriteLine("Cela peut arriver car vous n'avez pas configuré le fichier 'config.json' et les identifiants sont placés dans une autre section de votre fichier CSV.");
                    Console.WriteLine("Veuillez consultez les documentations pour configurer l'application et essayez à nouveau.");
                }
            } catch (Exception e) {
                writeCopyrightNotice();
                Console.WriteLine("Une erreure est apparue.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Appuyez sur une touche pour recommancer.");
                Console.ReadKey();
                startProgram();
            }
        }
    }
}