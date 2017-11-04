using GuildCars.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models;

namespace GuildCars.Datas
{
    public class CarRepositoryEF : ICar
    {
        public int AddCar(Car car)
        {
            throw new NotImplementedException();
        }

        public int AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public int AddMake(Make make)
        {
            throw new NotImplementedException();
        }

        public int AddModel(Model model)
        {
            throw new NotImplementedException();
        }

        public bool AddPostalCode(PostalCode postalCode)
        {
            throw new NotImplementedException();
        }

        public int AddSale(Sale sale)
        {
            throw new NotImplementedException();
        }

        public int AddSpecial(Special special)
        {
            throw new NotImplementedException();
        }

        public bool AddState(State state)
        {
            throw new NotImplementedException();
        }

        public string AddUser(User user, string role)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateUser(string userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCar(int carId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteContact(int contactId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMake(int makeId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteModel(int modelId)
        {
            throw new NotImplementedException();
        }

        public bool DeletePostalCode(string postalCode)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSale(int saleId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSpecial(int specialCode)
        {
            throw new NotImplementedException();
        }

        public bool DeleteState(string stateAbbreviation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Car GetCarbyId(int carId)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetCarList()
        {
            throw new NotImplementedException();
        }

        public Contact GetContactbyId(int contactId)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetContactList()
        {
            throw new NotImplementedException();
        }

        public Make GetMakebyId(int makeId)
        {
            throw new NotImplementedException();
        }

        public List<Make> GetMakeList()
        {
            throw new NotImplementedException();
        }

        public Model GetModelbyId(int modelId)
        {
            throw new NotImplementedException();
        }

        public List<Model> GetModelList()
        {
            throw new NotImplementedException();
        }

        public PostalCode GetPostalCodebyId(string postalCode)
        {
            throw new NotImplementedException();
        }

        public List<PostalCode> GetPostalCodeList()
        {
            throw new NotImplementedException();
        }

        public Sale GetSalebyId(int saleId)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSaleList()
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSaleListBySaleman(string userId)
        {
            throw new NotImplementedException();
        }

        public Special GetSpecialbyId(int specialCode)
        {
            throw new NotImplementedException();
        }

        public List<Special> GetSpecialList()
        {
            throw new NotImplementedException();
        }

        public State GetStatebyId(string stateAbbreviation)
        {
            throw new NotImplementedException();
        }

        public List<State> GetStateList()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUserList()
        {
            throw new NotImplementedException();
        }

        public List<User> GetUserListByRole(string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login(string userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReactivateUser(string userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCar(Car car)
        {
            throw new NotImplementedException();
        }

        public bool UpdateContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMake(Make make)
        {
            throw new NotImplementedException();
        }

        public bool UpdateModel(Model model)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePostalCode(PostalCode postalCode)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSale(Sale sale)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSpecial(Special special)
        {
            throw new NotImplementedException();
        }

        public bool UpdateState(State state)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
