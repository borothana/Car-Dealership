using GuildCars.Models.Interface;
using GuildCars.Models;
using GuildCars.Datas.DBContext;
using SCMS.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using GuildCars.Models.ViewModels;

namespace GuildCars.Datas
{
    public class CarRepositoryEF : ICar
    {
        CarDBContext _ctx = new CarDBContext();


        public Response ReturnSuccess()
        {
            return new Response() { Success = true, ErrorMessage = "" };
        }

        #region "Car"
        public int AddCar(Car car)
        {
            _ctx.Cars.Add(car);
            _ctx.SaveChanges();            
            return _ctx.Cars.Max(c=>c.CarId);
        }
        public bool DeleteCar(int carId)
        {
            Car car = GetCarById(carId);
            if(car != null)
            {
                _ctx.Entry(car).State = System.Data.Entity.EntityState.Deleted;
                _ctx.SaveChanges();
            }            
            return true;
        }

        public Car GetCarById(int carId)
        {
            return GetCarList().FirstOrDefault(c => c.CarId == carId);
        }

        public List<Car> GetCarList()
        {
            return _ctx.Cars.ToList();
        }

        public bool UpdateCar(Car car)
        {
            _ctx.Entry(car).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return true;
        }

        #endregion

        #region "Contact"
        public int AddContact(Contact contact)
        {
            _ctx.Contacts.Add(contact);
            _ctx.SaveChanges();
            return _ctx.Contacts.Max(c=>c.ContactId);
        }
        public bool DeleteContact(int contactId)
        {
            Contact contact = GetContactById(contactId);
            if (contact != null)
            {
                _ctx.Entry(contact).State = System.Data.Entity.EntityState.Deleted;
                _ctx.SaveChanges();
            }
            return true;
        }

        public Contact GetContactById(int contactId)
        {
            return GetContactList().FirstOrDefault(c => c.ContactId == contactId);
        }

        public List<Contact> GetContactList()
        {
            return _ctx.Contacts.ToList();
        }

        public bool UpdateContact(Contact contact)
        {
            _ctx.Entry(contact).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return true;
        }

        #endregion

        #region "Make"
        public MakeVM ConvertMakeToVM(Make input)
        {
            MakeVM result = new MakeVM()
            {
                AddDate = input.AddDate,
                AddUser = GetUserById(input.AddUserId),
                AddUserId = input.AddUserId,
                Description = input.Description,
                EditDate = input.EditDate,
                EditUser = GetUserById(input.EditUserId),
                EditUserId = input.EditUserId,
                MakeId = input.MakeId,
            };
            return result;
        }
        public Make ConvertVMToMake(MakeVM input)
        {
            Make result = new Make()
            {
                AddDate = input.AddDate,
                AddUserId = input.AddUserId,
                Description = input.Description,
                EditDate = input.EditDate,
                EditUserId = input.EditUserId,
                MakeId = input.MakeId,
            };
            return result;
        }

        public Make GetMakeById(int makeId)
        {
            return GetMakeList().FirstOrDefault(m => m.MakeId == makeId);
        }

        public List<Make> GetMakeList()
        {
            return _ctx.Makes.ToList();
        }
        
        public List<MakeVM> GetMakeVMList()
        {
            List<MakeVM> result = new List<MakeVM>();
            List<Make> make = GetMakeList();
            foreach (Make m in make)
            {
                result.Add(ConvertMakeToVM(m));
            }
            return result;
        }

        public MakeVM AddMake(MakeVM make)
        {
            make.Result = ReturnSuccess();
            if (GetMakeList().Any(m => m.Description == make.Description))
            {
                make.Result.Success = false;
                make.Result.ErrorMessage = make.Description + " alreay existed";
                return make;
            }

            _ctx.Makes.Add(ConvertVMToMake(make));
            _ctx.SaveChanges();
            make.MakeId = _ctx.Makes.Max(m => m.MakeId);
            return make;
        }
        public Response DeleteMake(MakeVM make)
        {
            make.Result = ReturnSuccess();
            if (GetModelList().Any(m => m.MakeId == make.MakeId))
            {
                make.Result.Success = false;
                make.Result.ErrorMessage = make.Description + " is in used";
                return make.Result;
            }

            _ctx.Entry(make).State = System.Data.Entity.EntityState.Deleted;
            _ctx.SaveChanges();            
            return ReturnSuccess();
        }

        public MakeVM UpdateMake(MakeVM make)
        {
            make.Result = ReturnSuccess();
            if (GetMakeList().Any(m => m.Description == make.Description && m.MakeId != make.MakeId))
            {
                make.Result.Success = false;
                make.Result.ErrorMessage = make.Description + " alreay existed";
                return make;
            }

            _ctx.Entry(make).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return make;
        }


        #endregion

        #region "Model"
        public ModelVM ConvertModelToVM(Model model)
        {
            ModelVM result = new ModelVM()
            {
                AddDate = model.AddDate,
                AddUser = GetUserById(model.AddUserId),
                AddUserId = model.AddUserId,
                Description = model.Description,
                EditDate = model.EditDate,
                EditUser = GetUserById(model.EditUserId),
                EditUserId = model.EditUserId,
                Make = GetMakeById(model.MakeId),
                MakeId = model.MakeId,
                ModelId = model.ModelId
            };
            return result;
        }
        public Model ConvertVMToModel(ModelVM modelVM)
        {
            Model result = new Model()
            {
                AddDate = modelVM.AddDate,
                AddUserId = modelVM.AddUserId,
                Description = modelVM.Description,
                EditDate = modelVM.EditDate,
                EditUserId = modelVM.EditUserId,
                Make = GetMakeById(modelVM.MakeId),
                MakeId = modelVM.MakeId,
                ModelId = modelVM.ModelId
            };
            return result;
        }

        public Model GetModelById(int modelId)
        {
            return GetModelList().FirstOrDefault(m => m.ModelId == modelId);
        }

        public List<Model> GetModelList()
        {
            return _ctx.Models.ToList();
        }

        public List<ModelVM> GetModelVMList()
        {
            List<ModelVM> result = new List<ModelVM>();
            List<Model> model = GetModelList();
            foreach (Model m in model)
            {
                result.Add(ConvertModelToVM(m));
            }
            return result;
        }

        public ModelVM AddModel(ModelVM model)
        {
            model.Result = ReturnSuccess();
            if (GetMakeList().Any(m => m.Description == model.Description))
            {
                model.Result.Success = false;
                model.Result.ErrorMessage = model.Description + " is alreay existed";
                return model;
            }

            _ctx.Models.Add(ConvertVMToModel(model));
            _ctx.SaveChanges();
            model.ModelId = _ctx.Models.Max(m => m.ModelId);
            return model;
        }

        public Response DeleteModel(ModelVM model)
        {
            model.Result = ReturnSuccess();
            if (GetCarList().Any(m => m.ModelId == model.ModelId))
            {
                model.Result.Success = false;
                model.Result.ErrorMessage = model.Description + " is in used";
                return model.Result;
            }

            _ctx.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            _ctx.SaveChanges();
            return ReturnSuccess();
        }

        public ModelVM UpdateModel(ModelVM model)
        {
            model.Result = ReturnSuccess();
            if (GetModelList().Any(m => m.Description == model.Description && m.ModelId != model.ModelId))
            {
                model.Result.Success = false;
                model.Result.ErrorMessage = model.Description + " is alreay existed";
                return model;
            }

            _ctx.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return model;
        }

        #endregion

        #region "PostalCode"
        public bool AddPostalCode(PostalCode postalCode)
        {
            _ctx.PostalCodes.Add(postalCode);
            _ctx.SaveChanges();
            return true;
        }

        public bool DeletePostalCode(string postalCode)
        {
            PostalCode postal = GetPostalCodeById(postalCode);
            if (postal != null)
            {
                _ctx.Entry(postal).State = System.Data.Entity.EntityState.Deleted;
                _ctx.SaveChanges();
            }
            return true;
        }

        public PostalCode GetPostalCodeById(string postalCode)
        {
            return GetPostalCodeList().FirstOrDefault(p => p.ZipCode == postalCode);
        }

        public List<PostalCode> GetPostalCodeList()
        {
            return _ctx.PostalCodes.ToList();
        }

        public bool UpdatePostalCode(PostalCode postalCode)
        {
            _ctx.Entry(postalCode).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return true;
        }
        #endregion

        #region "Sale"

        public int AddSale(Sale sale)
        {
            _ctx.Sales.Add(sale);
            _ctx.SaveChanges();
            return _ctx.Sales.Max(s => s.SaleId);
        }
        public bool DeleteSale(int saleId)
        {
            Sale sale = GetSaleById(saleId);
            if (sale != null)
            {
                _ctx.Entry(sale).State = System.Data.Entity.EntityState.Deleted;
                _ctx.SaveChanges();
            }
            return true;
        }
        public Sale GetSaleById(int saleId)
        {
            return GetSaleList().FirstOrDefault(s => s.SaleId == saleId);
        }

        public List<Sale> GetSaleList()
        {
            return _ctx.Sales.ToList();
        }

        public List<Sale> GetSaleListBySaleman(string userId)
        {
            return GetSaleList().Where(s => s.AddUserId == userId).ToList();
        }

        public bool UpdateSale(Sale sale)
        {
            _ctx.Entry(sale).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return true;
        }
        #endregion

        #region "Special"
        public SpecialVM ConvertSpecialToVM(Special input)
        {
            SpecialVM result = new SpecialVM()
            {
                SpecialId = input.SpecialId,
                Title = input.Title,
                Description = input.Description,
                Result = ReturnSuccess()
            };
            return result;
        }

        public Special ConvertVMToSpecial(SpecialVM input)
        {
            Special result = new Special()
            {
                SpecialId = input.SpecialId,
                Title = input.Title,
                Description = input.Description
            };
            return result;
        }

        public List<Special> GetSpecialList()
        {
            return _ctx.Specials.ToList();
        }

        public Special GetSpecialById(int specialId)
        {
            return GetSpecialList().FirstOrDefault(s => s.SpecialId == specialId);
        }

        public List<SpecialVM> GetSpecialVMList()
        {
            List<SpecialVM> result = new List<SpecialVM>();
            List<Special> special = GetSpecialList();
            foreach (Special m in special)
            {
                result.Add(ConvertSpecialToVM(m));
            }
            return result;
        }

        public SpecialVM AddSpecial(SpecialVM special)
        {
            special.Result = ReturnSuccess();
            if (GetSpecialList().Any(m => m.Description == special.Description))
            {
                special.Result.Success = false;
                special.Result.ErrorMessage = special.Description + " is alreay existed";
                return special;
            }

            _ctx.Specials.Add( ConvertVMToSpecial(special));
            _ctx.SaveChanges();
            special.SpecialId = _ctx.Specials.Max(s => s.SpecialId);
            return special;
        }

        public Response DeleteSpecial(SpecialVM special)
        {           
            _ctx.Entry(special).State = System.Data.Entity.EntityState.Deleted;
            _ctx.SaveChanges();
            return ReturnSuccess();
        }

        public SpecialVM UpdateSpecial(SpecialVM special)
        {
            special.Result = ReturnSuccess();
            if (GetSpecialList().Any(m => m.Description == special.Description && m.SpecialId != special.SpecialId))
            {
                special.Result.Success = false;
                special.Result.ErrorMessage = special.Description + " is alreay existed";
                return special;
            }

            _ctx.Entry(special).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return special;
        }
        #endregion

        #region "State"
        public bool AddState(State state)
        {
            _ctx.States.Add(state);
            _ctx.SaveChanges();
            return true;
        }

        public State GetStateById(string stateAbbreviation)
        {
            return GetStateList().FirstOrDefault(s => s.StateAbbreviation == stateAbbreviation);
        }

        public List<State> GetStateList()
        {
            return _ctx.States.ToList();
        }
        public bool UpdateState(State state)
        {
            _ctx.Entry(state).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return true;
        }

        public bool DeleteState(string stateAbbreviation)
        {
            State state = GetStateById(stateAbbreviation);
            if (state != null)
            {
                _ctx.Entry(state).State = System.Data.Entity.EntityState.Deleted;
                _ctx.SaveChanges();
            }
            return true;
        }
        #endregion

        #region "User"
        public List<User> GetUserList()
        {
            return _ctx.Users.ToList();
        }
        
        public List<User> GetUserListByRole(string role)
        {
            return GetUserList().Where(u => u.Roles.Any(r => r.RoleId == role)).ToList();
        }

        public User GetUserById(string userId)
        {
            return GetUserList().FirstOrDefault(u => u.Id == userId);
        }

        public User GetUserByUserName(string userName)
        {
            return GetUserList().FirstOrDefault(u => u.UserName == userName);
        }

        public string AddUser(User user, string role)
        {
            var userMgr = new UserManager<GuildCars.Models.User>(new UserStore<GuildCars.Models.User>(_ctx));

            //if user name or email not existed
            if (!userMgr.Users.Any(u => u.UserName == user.UserName))
            {
                user.IsActive = true;
                userMgr.Create(user);

                var tmpuser = userMgr.Users.Single(u => u.UserName == user.UserName);
                if (!tmpuser.Roles.Any(r => r.RoleId == role))
                {
                    userMgr.AddToRole(tmpuser.Id, role);
                    return tmpuser.Id;
                }
            }

            return "";
        }

        public async Task<bool> UpdateUser(User user)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User userTmp = await userMgr.FindByIdAsync(user.Id);
            if (userTmp == null)
            {
                return false;
            }
            var result = await userMgr.UpdateAsync(userTmp);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public bool ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = userMgr.Find(userId, currentPassword);
            if (user != null)
            {
                user.PasswordHash = userMgr.PasswordHasher.HashPassword(newPassword);
                var result = userMgr.Update(user);
                return result.Succeeded;
            }
            return false;
        }

        //public async Task<bool> ChangePassword(string userId, string currentPassword, string newPassword)
        //{
        //    var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
        //    User user = await userMgr.FindAsync(userId, currentPassword);
        //    if (user != null)
        //    {
        //        user.PasswordHash = userMgr.PasswordHasher.HashPassword(newPassword);
        //        var result = await userMgr.UpdateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return await Task.FromResult(true);
        //        }
        //    }
        //    return await Task.FromResult(false);
        //}

        public async Task<bool> DeactivateUser(string userName)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = await userMgr.FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false;
            var result = await userMgr.UpdateAsync(user);
            return result.Succeeded;
        }


        public async Task<bool> ReactivateUser(string userName)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = await userMgr.FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }
            user.IsActive = true;
            var result = await userMgr.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = await userMgr.FindByIdAsync(userId);
            var logins = user.Logins;
            var rolesForUser = await userMgr.GetRolesAsync(userId);

            using (var transaction = _ctx.Database.BeginTransaction())
            {
                foreach (var login in logins.ToList())
                {
                    await userMgr.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await userMgr.RemoveFromRoleAsync(user.Id, item);
                    }
                }

                await userMgr.DeleteAsync(user);
                transaction.Commit();
            }
            return true;
        }

        public bool Login(string userName, string password)
        {
            var userMgr = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<User>>();
            var user = userMgr.Find(userName, password);
            if (user != null)
            {
                var identity = userMgr.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                var authManager = HttpContext.Current.GetOwinContext().Authentication;
                authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                CurrentUser.User = user;
                return true;
            }
            return false;
        }


        public bool Logout()
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var authMgr = ctx.Authentication;
            authMgr.SignOut("ApplicationCookie");

            return true;
        }



        #endregion


    }
}
