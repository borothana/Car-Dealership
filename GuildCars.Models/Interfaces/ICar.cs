using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Interface
{
    public interface ICar
    {
        Response ReturnSuccess();

        List<Car> GetCarList();
        Car GetCarById(int carId);
        int AddCar(Car car);
        bool UpdateCar(Car car);
        bool DeleteCar(int carId);

        List<Contact> GetContactList();
        Contact GetContactById(int contactId);
        int AddContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool DeleteContact(int contactId);

        List<Make> GetMakeList();
        List<MakeVM> GetMakeVMList();
        Make GetMakeById(int makeId);
        MakeVM AddMake(MakeVM make);
        MakeVM UpdateMake(MakeVM make);
        Response DeleteMake(MakeVM make);
        MakeVM ConvertMakeToVM(Make input);
        Make ConvertVMToMake(MakeVM input);


        List<Model> GetModelList();
        List<ModelVM> GetModelVMList();
        Model GetModelById(int modelId);
        ModelVM AddModel(ModelVM model);
        ModelVM UpdateModel(ModelVM model);
        Response DeleteModel(ModelVM model);
        ModelVM ConvertModelToVM(Model input);
        Model ConvertVMToModel(ModelVM input);

        List<PostalCode> GetPostalCodeList();
        PostalCode GetPostalCodeById(string postalCode);
        bool AddPostalCode(PostalCode postalCode);
        bool UpdatePostalCode(PostalCode postalCode);
        bool DeletePostalCode(string postalCode);

        List<Sale> GetSaleList();
        List<Sale> GetSaleListBySaleman(string userId);
        Sale GetSaleById(int saleId);
        int AddSale(Sale sale);
        bool UpdateSale(Sale sale);
        bool DeleteSale(int saleId);

        List<Special> GetSpecialList();
        List<SpecialVM> GetSpecialVMList();
        Special GetSpecialById(int specialId);
        SpecialVM AddSpecial(SpecialVM special);
        SpecialVM UpdateSpecial(SpecialVM special);
        Response DeleteSpecial(SpecialVM special);
        SpecialVM ConvertSpecialToVM(Special input);
        Special ConvertVMToSpecial(SpecialVM input);

        List<State> GetStateList();
        State GetStateById(string stateAbbreviation);
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
        bool Login(string userId, string password);
        bool Logout();
        #endregion
    }
}
