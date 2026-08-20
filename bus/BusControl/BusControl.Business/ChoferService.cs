using System.Collections.Generic;
using BusControl.Data;

namespace BusControl.Business
{
    public class ChoferService
    {
        private readonly ChoferRepository _repository;

        public ChoferService()
        {
            _repository = new ChoferRepository();
        }

        public List<string> ObtenerChoferes()
        {
            return _repository.GetAll();
        }
    }
}
