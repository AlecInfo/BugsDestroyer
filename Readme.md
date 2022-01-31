# Bugs Destroyer

Bugs Destroyer est un jeu conçu en C.# Monogame. Il a été programmé pour la cité des métiers 2021. Il a pour but de réincarner un informaticien dans un ordinateur et son objectif à lui est de détruire tous les Bugs / Insects :ant:. Le jeu peut se jouer tous seul ou a deux, il y a un nombre de 12 niveaux qui augmente de difficulté plus un boss à la fin. Une fois le boss vaincu vous aurez la possibilité d'enregistrer votre score.

## Installation

1. Vous devrez installer l'extension ***Monogame*** dans Visual Studio 2019.
> Qui peut être installée à partir de ***Extensions -> Gérer les extensions*** dans la barre de menus de  Visual Studio. 

2. Éditeur MGCB
> MGCB Editor est un outil d'édition de fichiers .mgcb, qui sont utilisés pour créer du 
contenu. 
Télécharger[Monogame pages](https://docs.monogame.net/articles/tools/mgcb_editor.html) le dans l'invite de commande.

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

## Usage

```python
import foobar

# returns 'words'
foobar.pluralize('word')

# returns 'geese'
foobar.pluralize('goose')

# returns 'phenomenon'
foobar.singularize('phenomena')
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)