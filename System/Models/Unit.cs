using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace System.Models
{

    /* производственная единица */
    public class Unit
    {
        /* идентификатор в бд */
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /* наименование */
        public string Name { get; set; }
        /* оборудование, относящееся к производственной единице */
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}