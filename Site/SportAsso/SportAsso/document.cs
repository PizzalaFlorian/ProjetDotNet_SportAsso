//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportAsso
{
    using System;
    using System.Collections.Generic;
    
    public partial class document
    {
        public long document_id { get; set; }
        public long utilisateur_id { get; set; }
        public string type_document { get; set; }
        public string path_document { get; set; }
    
        public virtual utilisateur utilisateur { get; set; }
    }
}
