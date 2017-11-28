using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GuildCars.Models.Interface
{
    public interface ICar
    {
        #region "Other"
        Response ReturnSuccess();
        byte[] ConvertImgToByte(HttpPostedFileBase file);
        //String ConvertByteToImg(byte[] img);
        #endregion

        #region "Car"
        List<CarVM> GetCarVMList(string filter, int minPrice, int maxPrice, int minYear, int maxYear, string type);
        List<CarVM> GetCarVMList();
        List<Car> GetCarList();
        Car GetCarById(int carId);
        CarVM GetCarVMById(int carId);
        CarVM AddCar(CarVM carVM);
        CarVM UpdateCar(CarVM carVM);
        bool DeleteCar(int carId);
        Car ConvertVMToCar(CarVM model);
        CarVM ConvertCarToVM(Car model);
        #endregion

        #region "Contact"
        List<Contact> GetContactList();
        List<ContactVM> GetContactVMList();
        Contact GetContactById(int contactId);
        ContactVM AddContact(ContactVM contact);
        ContactVM UpdateContact(ContactVM contact);
        Response DeleteContact(ContactVM contact);
        ContactVM ConvertContactToVM(Contact input);
        Contact ConvertVMToContact(ContactVM input);
        #endregion

        #region "Make"
        List<Make> GetMakeList();
        List<MakeVM> GetMakeVMList();
        Make GetMakeById(int makeId);
        MakeVM AddMake(MakeVM make);
        MakeVM UpdateMake(MakeVM make);
        Response DeleteMake(MakeVM make);
        MakeVM ConvertMakeToVM(Make input);
        Make ConvertVMToMake(MakeVM input);
        #endregion

        #region "Model"
        List<Model> GetModelList();
        List<ModelVM> GetModelVMList();
        Model GetModelById(int modelId);
        ModelVM AddModel(ModelVM model);
        ModelVM UpdateModel(ModelVM model);
        Response DeleteModel(ModelVM model);
        ModelVM ConvertModelToVM(Model input);
        Model ConvertVMToModel(ModelVM input);
        #endregion

        #region "Sale"
        List<Sale> GetSaleList();
        List<SaleVM> GetSaleVMList();
        List<Sale> GetSaleListBySaleman(string userId);
        Sale GetSaleById(int saleId);
        int AddSale(Sale sale);
        bool UpdateSale(Sale sale);
        bool DeleteSale(int saleId);
        #endregion

        #region "Special"
        List<Special> GetSpecialList();
        List<SpecialVM> GetSpecialVMList(DateTime date);
        List<SpecialVM> GetSpecialVMList();
        Special GetSpecialById(int specialId);
        SpecialVM AddSpecial(SpecialVM special);
        SpecialVM UpdateSpecial(SpecialVM special);
        Response DeleteSpecial(SpecialVM special);
        SpecialVM ConvertSpecialToVM(Special input);
        Special ConvertVMToSpecial(SpecialVM input);
        #endregion

        #region "User"
        List<User> GetUserList();
        User GetUserById(string userId);
        User GetUserByUserName(string userName);
        List<User> GetUserListByRole(string role);
        UserVM AddUser(UserVM user, string role);
        bool DeactivateUser(string userId);
        bool ReactivateUser(string userId);
        bool ChangePassword(string userId, string currentPassword, string newPassword);
        bool UpdateUser(User user);
        bool DeleteUser(string userId);
        bool Login(string userId, string password);
        bool Logout();
        User ConvertVMToUser(UserVM input);
        UserVM ConvertUserToVM(User input);

        #endregion

        #region "Report"
        List<InventoryReportVM> GetInventoryReport();
        List<SaleReportVM> GetSaleReport(string UserName, DateTime fd, DateTime td);
        #endregion
    }
}
