using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Interface
{
    public interface ICar
    {
        List<Car> GetCarList();
        Car GetCarbyId(int carId);
        int AddCar(Car car);
        bool UpdateCar(Car car);
        bool DeleteCar(int carId);

        List<Contact> GetContactList();
        Contact GetContactbyId(int contactId);
        int AddContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool DeleteContact(int contactId);

        List<Make> GetMakeList();
        Make GetMakebyId(int makeId);
        int AddMake(Make make);
        bool UpdateMake(Make make);
        bool DeleteMake(int makeId);

        List<Model> GetModelList();
        Model GetModelbyId(int modelId);
        int AddModel(Model model);
        bool UpdateModel(Model model);
        bool DeleteModel(int modelId);

        List<PostalCode> GetPostalCodeList();
        PostalCode GetPostalCodebyId(string postalCode);
        bool AddPostalCode(PostalCode postalCode);
        bool UpdatePostalCode(PostalCode postalCode);
        bool DeletePostalCode(string postalCode);

        List<Sale> GetSaleList();
        List<Sale> GetSaleListBySaleman(string userId);
        Sale GetSalebyId(int saleId);
        int AddSale(Sale sale);
        bool UpdateSale(Sale sale);
        bool DeleteSale(int saleId);

        List<Special> GetSpecialList();
        Special GetSpecialbyId(int specialCode);
        int AddSpecial(Special special);
        bool UpdateSpecial(Special special);
        bool DeleteSpecial(int specialCode);

        List<State> GetStateList();
        State GetStatebyId(string stateAbbreviation);
        bool AddState(State state);
        bool UpdateState(State state);
        bool DeleteState(string stateAbbreviation);

        #region "User"
        List<User> GetUserList();
        User GetUserById(string userId);
        User GetUserByUserName(string userName);
        List<User> GetUserListByRole(string role);
        string AddUser(User user, string role);
        Task<bool> DeactivateUser(string userId);
        Task<bool> ReactivateUser(string userId);
        bool ChangePassword(string userId, string currentPassword, string newPassword);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(string userId);
        Task<bool> Login(string userId, string password);
        #endregion
    }
}
