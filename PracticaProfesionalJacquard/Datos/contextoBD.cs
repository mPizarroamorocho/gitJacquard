using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticaProfesionalJacquard.Models;

namespace PracticaProfesionalJacquard.Datos
{
    public class contextoBD:DbContext
    {
        public contextoBD(DbContextOptions<contextoBD> options):base(options)
        {

        }

        public DbSet<regionClass> TablaRegion { get; set; }
        public DbSet<ciudadClass> TablaCiudad { get; set; }
        public DbSet<comunaClass> TablaComuna { get; set; }


    }
}
