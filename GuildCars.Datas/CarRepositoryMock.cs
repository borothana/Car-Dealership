using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models;
using GuildCars.Models.Interface;
using SCMS.Datas;
using GuildCars.Models.ViewModels;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace GuildCars.Datas
{
    public class CarRepositoryMock : ICar
    {
        static List<User> _users = new List<User>
        {
            new User { Id = "1", UserName = "admin", Email = "admin@gmail.com", PasswordHash = "12345678"},
            new User { Id = "2", UserName = "sale", Email = "sale@gmail.com", PasswordHash = "12345678"},
            new User { Id = "3", UserName = "Nik", Email = "nik@gmail.com", PasswordHash = "12345678"},
            new User { Id = "4", UserName = "Javier", Email = "javier@gmail.com", PasswordHash = "12345678"}
        };

        static List<Contact> _contactList = new List<Contact>
        {
            new Contact{ ContactId = 1, Name = "Na", Email = "na@gmail.com", Phone = "952 846 1111", Message = "Message 1"},
            new Contact{ ContactId = 2, Name = "Ko", Email = "ko@gmail.com", Phone = "952 846 2222", Message = "Message 2"},
            new Contact{ ContactId = 3, Name = "My", Email = "my@gmail.com", Phone = "952 846 3333", Message = "Message 3"},
        };
                
        static List<Make> _makeList = new List<Make>
        {
            new Make{ MakeId = 1, Description = "Toyota", AddUserId = "1", AddDate = DateTime.Parse("11/01/2017 08:50 AM") },
            new Make{ MakeId = 2, Description = "Saburu", AddUserId = "2", AddDate = DateTime.Parse("11/02/2017 09:40 AM") },
            new Make{ MakeId = 3, Description = "Lexus", AddUserId = "1", AddDate = DateTime.Parse("11/03/2017 10:30 AM") },
        };

        static List<Model> _modelList = new List<Model>
        {
            new Model{ ModelId = 1, Description = "Camry XSE", MakeId = 1, Make = _makeList[0], AddUserId = "1", AddDate = DateTime.Parse("11/03/2017 10:30 AM") },
            new Model{ ModelId = 2, Description = "Camry XLE", MakeId = 1, Make = _makeList[0], AddUserId = "1", AddDate = DateTime.Parse("11/04/2017 11:00 AM") },
            new Model{ ModelId = 3, Description = "RX 300", MakeId = 3, Make = _makeList[2], AddUserId = "1", AddDate = DateTime.Parse("11/05/2017 11:30 AM") },
        };

        static List<State> _stateList = new List<State>
        {
            new State{ StateAbbreviation = "MN", StateName = "Minnesota"},
            new State{ StateAbbreviation = "NY", StateName = "New York"},
            new State{ StateAbbreviation = "MI", StateName = "Michigan"}
        };

        static List<PostalCode> _postalList = new List<PostalCode>
        {
            new PostalCode{ City = "Shakopee", ZipCode = "55379"},
            new PostalCode{ City = "Stillwater", ZipCode = "54082"},
            new PostalCode{ City = "Roseville", ZipCode = "55113"},
        };

        static List<Car> _carList = new List<Car>
        {
            new Car{ CarId = 1, BodyStyle = 'B', Color = "red", Description = "Camry XSE 2015", Interior = "red", Mileage = 50, Type = 'C', Transmision = 'T', ModelId = 1, Picture = null, MSRP = 0
                , SalePrice = 25000, ReleaseYear = 2015, VinNo = "111111", AddUserId = "1", AddDate = DateTime.Parse("11/05/2017 11:30 AM")},
            new Car{ CarId = 2, BodyStyle = 'B', Color = "red", Description = "Camry XLE 2016", Interior = "gray", Mileage = 50, Type = 'C', Transmision = 'T', ModelId = 1, Picture = null, MSRP = 0
                , SalePrice = 35000, ReleaseYear = 2016, VinNo = "222222", AddUserId = "2", AddDate = DateTime.Parse("11/05/2017 11:30 AM")},
            new Car{ CarId = 3, BodyStyle = 'B', Color = "red", Description = "Camry SE 2017", Interior = "white", Mileage = 50, Type = 'C', Transmision = 'T', ModelId = 1, Picture = null, MSRP = 0
                , SalePrice = 45000, ReleaseYear = 2017, VinNo = "333333", AddUserId = "3", AddDate = DateTime.Parse("11/05/2017 11:30 AM")},
        };

        static List<Special> _specialList = new List<Special>
        {
            new Special{ SpecialId = 1, Title = "Mid-Sale", Description = "Discount for mid-sale...." },
            new Special{ SpecialId = 2, Title = "New Year", Description = "Discount for New Year...." },
            new Special{ SpecialId = 3, Title = "Balck Friday", Description = "Discount for Black Friday...." },
        };

        static List<Sale> _saleList = new List<Sale>
        {
            new Sale{ SaleId = 1, CarId = 1, car = _carList[0], CustomerName = "Borothana", Email = "borothana@gmail.com", Phone = "952 846 9047", ZipCode = "55379", PostalCode = _postalList[0]
                , StateAbbreviation = "MN", State = _stateList[0], Street1 = "1008 Thrist Lane", PurchasePrice = 37000, PurchaseType = 'D', AddUserId = "2", AddDate = DateTime.Parse("11/05/2017 11:30 AM")}
        };

        public Response ReturnSuccess()
        {
            return new Response() { Success = true, ErrorMessage = "" };
        }

        #region "Car"
        public int AddCar(Car car)
        {
            car.CarId = _carList.Count + 1;
            _carList.Add(car);
            return car.CarId;
        }
        public bool DeleteCar(int carId)
        {
            _carList.RemoveAll(c => c.CarId == carId);
            return true;
        }
        public Car GetCarById(int carId)
        {
            return GetCarList().FirstOrDefault(c => c.CarId == carId);
        }

        public List<Car> GetCarList()
        {
            return _carList;
        }

        public bool UpdateCar(Car car)
        {
            _carList.RemoveAll(c => c.CarId == car.CarId);
            _carList.Add(car);
            return true;
        }

        #endregion

        #region "Contact"
        public int AddContact(Contact contact)
        {
            contact.ContactId = _contactList.Count + 1;
            _contactList.Add(contact);
            return contact.ContactId;
        }
        public bool DeleteContact(int contactId)
        {
            _contactList.RemoveAll(c => c.ContactId == contactId);
            return true;
        }

        public Contact GetContactById(int contactId)
        {
            return GetContactList().FirstOrDefault(c => c.ContactId == contactId);
        }

        public List<Contact> GetContactList()
        {
            return _contactList;
        }

        public bool UpdateContact(Contact contact)
        {
            _contactList.RemoveAll(c => c.ContactId== contact.ContactId);
            _contactList.Add(contact);
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
                Result = ReturnSuccess()
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
            return _makeList;
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
                make.Result.ErrorMessage = make.Description + " is alreay existed";
                return make;
            }

            make.MakeId = _makeList.Count + 1;
            _makeList.Add(ConvertVMToMake(make));
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

            _makeList.RemoveAll(m => m.MakeId == make.MakeId);
            return ReturnSuccess();
        }

        public MakeVM UpdateMake(MakeVM make)
        {
            make.Result = ReturnSuccess();
            if (GetMakeList().Any(m => m.Description == make.Description && m.MakeId != make.MakeId))
            {
                make.Result.Success = false;
                make.Result.ErrorMessage = make.Description + " is alreay existed";
                return make;
            }

            _makeList.RemoveAll(m => m.MakeId == make.MakeId);
            _makeList.Add(ConvertVMToMake(make));
            make.Result = ReturnSuccess();
            return make;
        }


        #endregion

        #region "Model"
        public ModelVM ConvertModelToVM(Model model) {
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
                ModelId = model.ModelId,
                Result = ReturnSuccess()
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
            return _modelList;
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

            model.ModelId = _modelList.Count + 1;
            _modelList.Add(ConvertVMToModel(model));
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

            _modelList.RemoveAll(m => m.ModelId == model.ModelId);
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

            _modelList.RemoveAll(m => m.ModelId == model.ModelId);
            _modelList.Add(ConvertVMToModel(model));
            return model;
        }

        #endregion

        #region "PostalCode"
        public bool AddPostalCode(PostalCode postalCode)
        {
            _postalList.Add(postalCode);
            return true;
        }

        public bool DeletePostalCode(string postalCode)
        {
            _postalList.RemoveAll(p => p.ZipCode == postalCode);
            return true;
        }

        public PostalCode GetPostalCodeById(string postalCode)
        {
            return GetPostalCodeList().FirstOrDefault(p => p.ZipCode == postalCode);
        }

        public List<PostalCode> GetPostalCodeList()
        {
            return _postalList;
        }

        public bool UpdatePostalCode(PostalCode postalCode)
        {
            _postalList.RemoveAll(p => p.ZipCode == postalCode.ZipCode);
            _postalList.Add(postalCode);
            return true;
        }
        #endregion

        #region "Sale"

        public int AddSale(Sale sale)
        {
            sale.SaleId = _saleList.Count + 1;
            _saleList.Add(sale);
            return sale.SaleId;
        }
        public bool DeleteSale(int saleId)
        {
            _saleList.RemoveAll(s => s.SaleId == saleId);
            return true;
        }
        public Sale GetSaleById(int saleId)
        {
            return GetSaleList().FirstOrDefault(s => s.SaleId == saleId);
        }

        public List<Sale> GetSaleList()
        {
            return _saleList;
        }

        public List<Sale> GetSaleListBySaleman(string userId)
        {
            return GetSaleList().Where(s => s.AddUserId == userId).ToList();
        }

        public bool UpdateSale(Sale sale)
        {
            _saleList.RemoveAll(s => s.SaleId == sale.SaleId);
            _saleList.Add(sale);
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

        public Special GetSpecialById(int SpecialId)
        {
            return GetSpecialList().FirstOrDefault(m => m.SpecialId == SpecialId);
        }

        public List<Special> GetSpecialList()
        {
            return _specialList;
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

            special.SpecialId = _specialList.Count + 1;
            _specialList.Add(ConvertVMToSpecial(special));
            return special;
        }

        public Response DeleteSpecial(SpecialVM special)
        {
            _specialList.RemoveAll(m => m.SpecialId == special.SpecialId);
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

            _specialList.RemoveAll(m => m.SpecialId == special.SpecialId);
            _specialList.Add(ConvertVMToSpecial(special));
            special.Result = ReturnSuccess();
            return special;
        }
        #endregion

        #region "State"
        public bool AddState(State state)
        {
            _stateList.Add(state);
            return true;
        }

        public State GetStateById(string stateAbbreviation)
        {
            return GetStateList().FirstOrDefault(s => s.StateAbbreviation == stateAbbreviation);
        }

        public List<State> GetStateList()
        {
            return _stateList;
        }
        public bool UpdateState(State state)
        {
            _stateList.RemoveAll(s => s.StateAbbreviation == state.StateAbbreviation);
            _stateList.Add(state);
            return true;
        }

        public bool DeleteState(string stateAbbreviation)
        {
            _stateList.RemoveAll(s => s.StateAbbreviation == stateAbbreviation);
            return true;
        }
        #endregion

        #region "User"
        public List<User> GetUserList()
        {
            return _users;
        }

        public List<User> GetUserListByRole(string role)
        {
            return GetUserList().Where(u => u.Roles.Any(r => r.RoleId == role)).ToList();
        }

        public User GetUserById(string userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName == userName);
        }

        public string AddUser(User user, string role)
        {
            _users.Add(user);
            return user.Id;
        }

        public async Task<bool> DeactivateUser(string userId)
        {
            User user = GetUserById(userId);
            if (user != null)
            {
                user.IsActive = false;
                _users.RemoveAll(u => u.Id == user.Id);
                _users.Add(user);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> ReactivateUser(string userId)
        {
            User user = GetUserById(userId);
            if (user != null)
            {
                user.IsActive = true;
                _users.RemoveAll(u => u.Id == user.Id);
                _users.Add(user);
            }
            return await Task.FromResult(true);
        }

        public bool ChangePassword(string userId, string currentPassword, string newPassword)
        {
            User user = GetUserById(userId);
            if (user != null && user.PasswordHash == currentPassword)
            {
                user.PasswordHash = newPassword;
                _users.RemoveAll(u => u.Id == user.Id);
                _users.Add(user);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _users.RemoveAll(u => u.Id == user.Id);
            _users.Add(user);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            _users.RemoveAll(u => u.Id == userId);
            return await Task.FromResult(true);
        }

        public bool Login(string userName, string password)
        {
            if (_users.Any(u => u.UserName == userName && u.PasswordHash == password))
            {
                var userMgr = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<User>>();
                //User user = _users.FirstOrDefault(u => u.UserName == userName);
                User user = userMgr.Find(userName, password);
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
