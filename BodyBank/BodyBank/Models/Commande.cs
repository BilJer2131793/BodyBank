﻿using BodyBank.Models;
using System.ComponentModel.DataAnnotations;

namespace BodyBank.Model
{
    public class Commande
    {
        public int CommandeId { set;get; }
        public DateTime? Date { get; set; }
        public decimal? Total { get; set; }
        public Addresse? AdresseLivraison { get; set; }
        [Required]
        public Util Util { get; set; }

        public Commande()
        {

        }
        public Commande(Util util)
        {
            Util = util;
        }
    }
}
