using System.Collections.Generic;
using BusControl.Data;

namespace BusControl.Business
{
    public class RutaService
    {
        private readonly RutaRepository _repository;

        public RutaService()
        {
            _repository = new RutaRepository();
        }

        public List<string> ObtenerRutas()
        {
            return _repository.GetAll();
        }
    }
}
