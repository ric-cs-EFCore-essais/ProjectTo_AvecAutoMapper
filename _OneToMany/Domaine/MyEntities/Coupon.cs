namespace Domaine.MyEntities
{
    public class Coupon
    {
        public int Id { get; init; }

        public string Code { get; init; }

        
        
        public int? PersonneId { get; init; } //Membre mis UNIQUEMENT ! pour pouvoir initialiser par code, le contenu de la table Coupons (cf. Program.cs).
                                              // c-à-d que même si on ne le mettait pas, la table Coupons comporterait QUAND MÊME un champ personneId créé automatiquement par EF,
                                              // du fait de la relation One to Many détectée par EF (car l'entité Personne possède en effet une Liste de Coupons !)

        // public Personne Personne { get; init; } //<<< NE SURTOUT PAS ! mettre ce membre car on aurait alors une RÉFÉRENCE CIRCULAIRE : Coupon --> Personne --> Personne.Coupons ....
                                                   // alors détectée lors d'une itération sur liste de Coupons (via par ex. méthode Include() d'EF).
                                                   //  (l'entité Personne possède en effet une Liste de Coupons !)

    }
}
