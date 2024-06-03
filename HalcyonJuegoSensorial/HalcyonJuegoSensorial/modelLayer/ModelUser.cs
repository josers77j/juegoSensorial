using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalcyonJuegoSensorial.modelLayer
{
    public class ModelUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255), Unique]
        public string NombreUsuario { get; set; }
            
        public int Puntuacion { get; set; }
    }
}
