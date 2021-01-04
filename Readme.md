# Version 2.0.0.0:
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

# Todo:
 - [ ] Modifier les box pour rendre plus modulaire, quand c'est possible.
 - [ ] Nettoyer un peu le code des progress
 - [ ] Faire quelques screenshots
 - [ ] Simplifier les box en utilisant soir le StringFormat, soit le multibinding converter

# Notes: 
 * Dans la mesure du possible un nouveau système d'algorithmes dénués d'interfaces va être créé pour manager les transferts, il devrait probablement être écrit en .net Standart.
