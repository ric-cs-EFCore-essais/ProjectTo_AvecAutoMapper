using System.Collections.Generic;

namespace Domaine.MyEntities
{
    public class Personne 
    { 
        public int Id { get; init; }

        public string Nom { get; init; }

        public string Prenom { get; init; }

        public IList<Coupon> Coupons { get; init; }
    }
}

