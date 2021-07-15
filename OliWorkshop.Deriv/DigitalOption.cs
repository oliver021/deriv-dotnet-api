using OliWorkshop.Deriv.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliWorkshop.Deriv
{
    /// <summary>
    /// Represent parameters to set an binary option contract
    /// </summary>
    public class DigitalOption
    {
        /// <summary>
        /// simbolo del instrumento financiero a usar
        /// </summary>
        public string market { get; set; }

        /// <summary>
        /// Tipo de contrato
        /// </summary>
        public ContractOption type { get; set; }

        /// <summary>
        /// opcion a elegir
        /// </summary>
        public bool option { get; set; }

        /// <summary>
        /// ticks para fijar el plazo del contrato
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// ticks para fijar el plazo del contrato
        /// </summary>
        public ContractDuration durationType { get; set; }

        /// <summary>
        /// Objetivo para señalar la variable que define el parametros de las pruebas
        /// que indican una entrada
        /// </summary>
        public double barrier { get; set; }

        /// <summary>
        /// Segundo objetivo para señalar la variable que define el parametros de las pruebas
        /// que indican una entrada
        /// </summary>
        public double barrier2 { get; set; }
    }
}
