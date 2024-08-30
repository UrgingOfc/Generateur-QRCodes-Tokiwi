
# Générateur QR-Codes Tokiwi

Cette application permet que vous faites des QR-Codes vers un lien en se basant sur l'identifiant de la personne de votre document .csv (Excel)



## Dépendences

- [.NET 4.8 ou supérieur](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install?pivots=os-linux-ubuntu-2404&tabs=dotnet8)




## Installation

Pour installer c'est très simple. Vous devez simplement ouvrir votre terminal et coller cette commande:
```bash
git clone https://github.com/UrgingOfc/Generateur-QRCodes-Tokiwi
cd Generateur-QRCodes-Tokiwi
```

Si vous n'avez pas git installé, vous pouvez **l'installer** ou bien tout simplement **télécharger** le **source code** dans votre ordinateur.    

## Configuration

Pour utiliser l'application, vous devez d'abord configurer le fichier **config.json** avec les détailes que vous voullez pour que l'application puisse identifier vos identifiants ainsi que le lien à générer pour le QR-Code. Pour ça, on va ouvrir le fichier **config.json**. 

Votre fichier doit se ressembler à ça:
```json
{
    "SiteQRCodes": "https://example.com",
    "NomTableauIdentifiants": "identifiants"
}
```
Dans ce fichier, vous pouvez modifier les données avec les informations que vous avez besoin, par exemple si dans votre fichier **csv** le tableau avec les identifiants s'appelle **utilisateurs**, vous devez le changer dans la configuration de façon que le programme puisse ensuite identifier le tableau des identifiants.

#### Exemple:
```json
{
    "SiteQRCodes": "https://tokiwi.ch",
    "NomTableauIdentifiants": "identifiants"
}
```

> [!WARNING]  
> Dans la configuration, le site ne peut pas avoir une "/" à la fin, si non le lien aura deux "/" et possiblement le QR-Code ne marchera pas.
## Configuration

Pour utiliser l'application, vous devez d'abord configurer le fichier **config.json** avec les détailes que vous voullez pour que l'application puisse identifier vos identifiants ainsi que le lien à générer pour le QR-Code. Pour ça, on va ouvrir le fichier **config.json**. 

Votre fichier doit se ressembler à ça:
```json
{
    "SiteQRCodes": "https://example.com",
    "NomTableauIdentifiants": "identifiants"
}
```
Dans ce fichier, vous pouvez modifier les données avec les informations que vous avez besoin, par exemple si dans votre fichier **csv** le tableau avec les identifiants s'appelle **utilisateurs**, vous devez le changer dans la configuration de façon que le programme puisse ensuite identifier le tableau des identifiants.

#### Exemple:
```json
{
    "SiteQRCodes": "https://tokiwi.ch",
    "NomTableauIdentifiants": "identifiants"
}
```

> [!WARNING]  
> Dans la configuration, le site ne peut pas avoir une "/" à la fin, si non le lien aura deux "/" et possiblement le QR-Code ne marchera pas.
## Utilisation

### Façon "Prompt"
Après avoir configuré l'application, maintenant vous pouvez exécuter cette commande dans votre terminal de façon à exécuter l'application. 

```bash
dotnet diogo2.dll
```

Si tout se passe bien, vous aurez du recevoir un message pour débuter les étapes pour générer les QR-Codes.
```bash
=============================
Générateur QR-Code Tokiwi
Programmé par: Diogo
=============================

Étape Nº 1: Choisir le chemin du fichier CSV
Veuillez coller le chemin de votre fichier CSV avec les identifiants des utilisateurs.
Chemin Fichier CSV: 

```
Ici vous aller coller le chemin de votre fichier **.csv (Excel)** contenant les identifiants et vous allez appuyer sur **Enter**.

```bash
=============================
Générateur QR-Code Tokiwi
Programmé par: Diogo
=============================

Étape Nº 1: Choisir le chemin du fichier CSV
Veuillez coller le chemin de votre fichier CSV avec les identifiants des utilisateurs.
Chemin Fichier CSV: 
/home/mint/diogo2/identifiants.csv
```

Maintenant, vous allez écrire le nom du fichier **ZIP** qui sera généré avec tous les QR-Codes de tous les identifiants. Moi j'aimerai que mon fichier **ZIP** s'appelle **QR-Codes Identifiants**, alors je vais taper ça sur le terminal et appuyer sur **Enter**.
```bash
=============================
Générateur QR-Code Tokiwi
Programmé par: Diogo
=============================

Étape Nº 2: Choisir le nom du ZIP à générer pour les QR-Codes
Veuillez introduire le nom du fichier ZIP pour générer les QR-Codes.
Nom du ZIP: 
QR-Codes Identifiants
```

Pour finir, vous devez maintenant choisir l'emplacement dans lequel votre fichier **ZIP** sera sauvegardé avec tous les QR-Codes générés. Moi je souhaite l'enregistrer sur **mon bureau**.
```bash
=============================
Générateur QR-Code Tokiwi
Programmé par: Diogo
=============================

Étape Nº 3: Choisir le chemin de sortie
Veuillez coller le chemin de sortie de votre QR-Code.
Chemin Sortie QR-Code: 
/home/mint/Desktop
```

Après avoir fini, vous allez remarquer que les identifiants vont apparaître sur le terminal et le programme va vous indiquer que les QR-Codes ont bien été générés. Pour vérifier, on va ouvir l'emplacement du fichier **ZIP** et vérifier que les fichiers se retrouvent tous sur le **ZIP**.

### Façon "Commande"
Ce générateur de QR-Codes a une possibilité de générer les QR-Codes seulement via une commande pour le démarrer. Cette façon est faite pour les personnes qui ont une certaine expérience avec un terminal pour générer les Codes-QR plus vite. Pour qu'on puisse générer les QR-Codes, on doit faire passer quelques arguments dans notre programme.

#### Arguments
| Argument  | Description |
| ------------- | ------------- |
| --csvPath  | Cet argument permet de définir l'emplacement de notre fichier **.csv (Excel)**  |
| --zipName | Cet argument permet de définir le nom de notre fichier **.ZIP**  |
| --outputPath | Cet argument permet de définir l'emplacement dans lequel notre fichier **.ZIP** sera sauvegardé. |
| --help | Cet argument permet d'obtenir de l'aide avec les arguments. |

#### Utilisation
Maintenant qu'on connaît les arguments on peut commencer à générer des QR-Codes. Pour ça, vous allez retourner vers votre terminal et vous allez coller cette commande:
```bash
dotnet diogo2.dll --csvPath EMPLACEMENT_CSV --zipName NOM_ZIP --outputPath EMPLACEMENT_ZIP
```

#### Exemple de génération de QR-Codes:
```bash
dotnet diogo2.dll --csvPath /home/mint/diogo2/identifiants.csv --zipName output --outputPath /home/mint/diogo2
```



## License
```
MIT License

Copyright (c) 2024 Urging

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```