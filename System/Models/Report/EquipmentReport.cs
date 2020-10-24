using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Models.Report
{
    /* структура для создания отчёта, которая содержит наименование производственной единицы и относящееся к ней оборудование*/
    public struct EquipmentDailyReport
    {
        public string UnitName { get; set; }
        public List<Report> Equipments { get; set; }
    }
    /* структура, содержащая информацию по конкретной единице оборудования () */
    public struct Report
    {
        /* наименование */
        public string Name { get; set; }
        /* список событий с подробностями за сутки */
        public List<Event> Events { get; set; }
        /* список событий за сутки */
        public List<DayEvent> DayEvents { get; set; }
        /* список событий за месяц */
        public List<MonthEvent> MonthEvents { get; set; }
    }

    /* структура, содержащая обобщенную информацию о событиях за сутки */
    public struct DayEvent
    {
        /* длительность */
        public int Duration { get; set; }
        /* количество */
        public int Count { get; set; }
        /* тип события */
        public string Type { get; set; }
    }
    /* структура, содержащая обобщенную информацию о событиях за месяц */
    public struct MonthEvent
    {
        /* длительность */
        public int Duration { get; set; }
        /* количество */
        public int Count { get; set; }
        /* тип события */
        public string Type { get; set; }
    }

    /* структура, содержащая подробную информацию о событиях за сутки */
    public struct Event
    {
        /* начало события */
        public DateTime SD { get; set; }
        /* конец события */
        public DateTime ED { get; set; }
        /* длительность */
        public int Duration { get; set; }
        /* тип события */
        public string Type { set; get; }
    }
}