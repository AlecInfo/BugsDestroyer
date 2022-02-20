# Bugs Destroyer

Bugs Destroyer est un jeu conçu en C# [Monogame](https://www.monogame.net/). Il a été programmé pour la cité des métiers 2021 (Genève). Vous réincarnez un informaticien dans un ordinateur et son objectif est de détruire tous les Bugs / Insects :ant: de son pc. Le jeu peut se jouer tous seul ou a deux, il y a 12 niveaux qui augmentent de difficulté plus un boss à la fin. Une fois le boss vaincu vous aurez la possibilité d'enregistrer votre score.

<br>
<p align="center">
  <img src="https://github.com/AlecInfo/BugsDestroyer/blob/master/Doc/GifTroKoul.gif?raw=true" alt="Gameplay" Width="640" Height="360">
</p>
<br>

## Installation

1. Vous devrez installer l'extension ***Monogame*** dans Visual Studio 2019.
> :bulb: Qui peut être installée à partir de ***Extensions -> Gérer les extensions*** dans la barre de menus de  Visual Studio. 

2. Éditeur MGCB
> :bulb: MGCB Editor est un outil d'édition de fichiers .mgcb, qui sont utilisés pour mettre du 
contenu comme des images, vidéos, musiques ... <br>
[Télécharger](https://docs.monogame.net/articles/tools/mgcb_editor.html) le dans l'invite de commande.

```shell
# Générateur de contenu MonoGame (MGCB)
dotnet tool install -g dotnet-mgcb

#  Éditeur MGCB (anciennement outil Pipeline)
dotnet tool install -g dotnet-mgcb-editor

# Compilateur d'effets MonoGame (MGFXC ; auparavant 2MGFX)
dotnet tool install -g dotnet-mgfxc

# Après installation
mgcb-editor
mgcb-editor --register
```

3. Récupérer le code source 

4. Installez la police d'écriture
> :bulb: *Early GameBoy.ttf* ce trouve dans [/BugsDestroyer/Content/Fonts](https://github.com/AlecInfo/BugsDestroyer/tree/master/BugsDestroyer/Content/Fonts)

## Jouer
### Lancement du jeu
Si vous avez télécharger le code source vous pouvez directement compiler le programme pour jouer.
Mais si vous ne voulez pas ou pouvez pas alors le programme se trouve dans les fichiers du jeu.

> :bulb: Le programme est dans le dossier [/Doc/App/](https://github.com/AlecInfo/BugsDestroyer/tree/master/Doc/App), il ne vous restera plus qu'à lancer le ficher ***BugsDestroyer.exe***.

### Créer un Publish
Pour créer ce dossier App/Publish qui permet de concevoir un .exe de votre application, il suffit d'aller dans le terminal de commande, aller dans le dossier de votre programme et faire la commande suivante.

> 💡 Le dossier publish créé est un dossier temporaire pour cela vous deverez aller dans le dossier de votre application, exemple [BugsDestroyer/BugsDestroyer/](https://github.com/AlecInfo/BugsDestroyer/tree/master/BugsDestroyer). Et chercher le dossier publish dans la barre de recherche. 

```shell
# Windows
dotnet publish -c Release -r win-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained
```

```shell
# Linux
dotnet publish -c Release -r linux-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained
```

```shell
# macOs
dotnet publish -c Release -r osx-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained
```


## Contrôles

> :bulb: Je jeu a été conçu pour être joué sur une borne d'arcade, c'est pour ça que les touches peuvent être un peu particullières

### Menu
- 1 joueur : 7
- 2 joueur : 9
- Entrer / Pause : 8
- Quitter : 0

### Joueur 1
- Mouvement : W A S D
- Tir : F
- Entrer : G

### Joueur 2
- Mouvement : Flèches directionnelles
- Tir : 4
- Entrer : 5

