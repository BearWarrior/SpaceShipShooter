/*

Pour implanter une nouvelle arme : 

-Placer le .blend dans ressource/arme

-Créer un script avec son nom dans Script/Armes
	-Refaire la méthode Fire (override) s nécessaire

-Dans CaraArmes, ajouter un struct et ajouter au tableau

-Dans MenuAchat (3)
	-Ajouter a la liste de plan du magasin : listPlanToBuy.Add ("nomArme");
	-Ajouter un case au switch avec l'arme
	
-Dans MenuAtelier
	-Ajouter un case au switch avec l'arme

-Dans Chassis
	-Ajouter le cas de la nouvelle arme dans les 2 switchs
*/