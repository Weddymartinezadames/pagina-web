using System.Collections.Generic;
using BusControl.Data;

namespace BusControl.Business
{
    public class BusService
    {
        private readonly BusRepository _repository;

        public BusService()
        {
            _repository = new BusRepository();
        }

        public List<string> ObtenerBuses()
        {
            return _repository.GetAll();
        }
    }
}
