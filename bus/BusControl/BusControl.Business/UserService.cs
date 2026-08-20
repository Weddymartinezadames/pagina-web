using System.Collections.Generic;
using BusControl.Data;

namespace BusControl.Business
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }

        public List<string> ObtenerUsuarios()
        {
            return _repository.GetAll();
        }
    }
}
