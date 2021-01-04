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
  

# Exemple (With a button to launch):
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

# Todo:
 - [ ] Modifier les box pour rendre plus modulaire, quand c'est possible.
 - [ ] Nettoyer un peu le code des progress
 - [ ] Faire quelques screenshots
 - [ ] Simplifier les box en utilisant soir le StringFormat, soit le multibinding converter
 - [ ] Réfléchir à la dépendance d'un tracer afin d'unifier avec les applications ou faire à la place un système de messages à coupler

# Notes: 
 * Dans la mesure du possible un nouveau système d'algorithmes dénués d'interfaces va être créé pour manager les transferts, il devrait probablement être écrit en .net Standart.
