using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace System.Models
{
    public class Equipment
    {
        /* идентификатор в бд */
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /* наименование */
        public string Name { get; set; }

        [Required]
        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }
    }

    /* класс, описывающий информацию о событиях, происходящих с оборудованием */
    public class DataEquipment
    {
        /* идентификатор в бд */
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentId { get; set; }
        /* начало события */
        public DateTime Start { get; set; }
        /* конец события */
        public DateTime End { get; set; }
        /* длительность события */
        public int Duration
        {
            get { return (int)(End - Start).TotalMinutes; }
        }
        /* тип события */
        public int PeriodType { get; set; }

        [Required]
        public int Equipment_Id { get; set; }
        [ForeignKey("Equipment_Id")]
        public virtual Equipment Equipment { get; set; }
    }
}