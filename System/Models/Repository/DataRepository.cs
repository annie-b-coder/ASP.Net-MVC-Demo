using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace System.Models.Repository
{
    public class DataRepository
    {
        AppContext db;

        public DataRepository(AppContext db)
        {
            this.db = db;
        }

    }
}