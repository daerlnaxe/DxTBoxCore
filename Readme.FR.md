# Version 2.0.0.1 (Coming Soon)
 * Box progress: Ajout d'une classe générique dérivée d'I_ASBase en charge d'encapsuler n'importe quelle méthode passée afin de rendre compatible avec le système.
 * Box progress: Correction sur la box de collec d'un bug relatif à la fermeture
 * Choose Folder: Actualisation graphique et migration vers un système avec modèle
 * Choose Boxes: 
	- Correction des bugs relatifs aux images suite au passage vers .net Core
 * Choose Boxes experimentale:
	du contenu
	- Dissociation code du model du reste + classe abstraite + classe héritée
	- Ajout: Dossier spécial via une dll c++ managée
	- Remplacement de la recherche temps réel par une recherche lancée via Ctrl+G, moins gourmand en ressources
	- Mode alternatif fichier/dosser pour empêcher la selection de fichiers si l'on souhaite les afficher mais jamais pouvoir les sélectionner
	- Correction mise à jour du dossier en collapsant/développant un dossier en cas de modification par l'explorer
	- Correction diverses dont l'algorithme de recherche
	- Correction : Erreur sur les dossiers dont l'accès est refusé par l'OS
	- Correction : Blocage du développement quand il n'y a pas d'enfants


# Version 2.0.0.0 (04/01/2021):
 * Portage de DxTBoxWpf de .Net Framework vers .Net Core
 * Création d'un système de box progress qui gère seul en fond une tâche qu'on lui passe:
	 - Le système est créé pour totalement dissocier le modèle de la fenêtre
	 - Un système d'interfaces et de classes abstraites a été créé pour fournir une base et un pattern pour que tout soi géré avec le minimum d'intervention derrière
	- Je ne sais plus comment les anciennes box progress fonctionnaient, elles ont été désactivées, il faudra voir quelles applications les utilisaient.
	- La dernière version de SappPasRoot est basé sur cette version.
	- Deux classes existent pour tester les box progress
	- On lancera toujours le code à partir de l'event Loaded de la fenêtre.
	- Pour ne pas rencontrer de soucis sur les collections un locker a été installé
 * L'évolution naturelle va être de dissocier les modèles des box pour faire un système beaucoup plus modulaire.
 
# Fonctionnalités:
 * Boites de progression (simple et double):
	- Basé sur un système avec modele à part.
 	- Systeme d'interfaces et classes abstraites afin de permettre d'autres modeles.
  		* Le système d'interface I_ASBase permet de posséder un token pour clore la tâche et un booléen pour la mettre en pause si vous l'intégrez à votre algorithme. Ce booléen est activé lorsque l'utilisateur clique pour fermer afin de permettre la mise en pause de l'algorithme.
 	- Fonctionnement avec tâche en fond
 	- Fenêtre se termine seule
 	- Fenêtre se ferme si l'utilisateur appuie sur le bouton de fermeture (demande avant)
	- La methode de lancement de la tâche incluse dans la classe abstraite incorpore un délais de 50ms avant de lancer la tâche et ajoute la fermeture de la fenêtre à la fin de la tâche.

 * Box Choose:
	- Recherche dans l'arborescence quand on remplit une textbox ou en initialisant le chemin à la création

  

# Exemples (With a button to launch):
 - Avec une classe héritant d'I_ASBase
	```
	DxAsCollecProgress db2;
	private void test(object sender, System.Windows.RoutedEventArgs e)
	{
		db2 = new DxAsCollecProgress(DxTBLang.File)
		{
			TaskToRun = new TestProgressCollec()
			{                  
				// --- TaskToRun Properties
			},
		};

		db2.ShowDialog();        
        }

	// TaskToRun must implement I_ASBase interface from my library
	``` 

# Todo:
 - [ ] Modifier les box pour rendre plus modulaire, quand c'est possible.
 - [ ] Simplifier les box en utilisant soir le StringFormat, soit le multibinding converter
 - [ ] Nettoyer un peu le code des progress
 - [ ] Faire quelques screenshots
 - [ ] Mettre les fonctionnalités dans le wiki
 - [ ] Réfléchir à la dépendance d'un tracer afin d'unifier avec les applications ou faire à la place un système de messages à coupler
 - [ ] Box Choose: Réfléchir à merger le build pour lecteur et le build pour les autres drives
 - [ ] Box Choose: nettoyer le code commenté si pas de bugs constatés
 - [ ] Box Choose: experimentale, voir pour vider les enfants quand collapsé
 - [ ] Box Choose: trouver ou faire un icone pour le dossier "downloads"
 - [ ] Box Choose: Refresh complet (En cours)
		- graphique
		- séparer view/view-model/model
		- Utiliser les ressources pour le texte
 - [ ] Box Choose: Intégrer le système de gestion des erreurs sur la textbox de recherche (dont le refus de laisser un chemin finir par \
 - [ ] Box Choose: Intégrer la commande paste par le menu item, de sorte de lancer directement la recherche
 - [x] Box Choose: Mettre la possibilité de sensibilité à la casse dans la comparaison
 - [x] Box Choose: Locker la possibilité de sélectionner un dossier sur un select folder
 - [x] Box Choose: Locker la possibilité de sélectionner un fichier sur un select file
 - [x] Box Choose: experimentale, collapser les enfants
 - [x] Box Choose: Mettre un booléen pour utiliser les fonctions d'affichage des fichiers
 - [x] Box Choose: le expanse semble ne fonctionner qu'une fois'
 - [x] Box Choose: afficher aussi les fichiers
 - [x] Box Choose: Peut être voir pour la recherche car ça ralentit pas mal, ou faire une icone.
 - [x] Box Choose: ~~Réfléchir si on doit laisser le signal remonter les parents~~ => Bug: engendre une panne cyclique

# Notes: 
 * Dans la mesure du possible un nouveau système d'algorithmes dénués d'interfaces va être créé pour manager les transferts, il devrait probablement être écrit en .net Standart.
 * Box Choose: l'explorer de windows ne collapse pas les anciens dossiers quand on déploie une autre branche:
		Le choix a été fait de collapser quand on utilise le mode recherche uniquement
		
