# Bugs Destroyer

Bugs Destroyer est un jeu conçu en C.# [Monogame](https://www.monogame.net/). Il a été programmé pour la cité des métiers 2021. Il a pour but de réincarner un informaticien dans un ordinateur et son objectif à lui est de détruire tous les Bugs / Insects :ant:. Le jeu peut se jouer tous seul ou a deux, il y a un nombre de 12 niveaux qui augmente de difficulté plus un boss à la fin. Une fois le boss vaincu vous aurez la possibilité d'enregistrer votre score.

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
> :bulb: *Early GameBoy.ttf* ce trouve dans ***\BugsDestroyer\Content\Fonts***
