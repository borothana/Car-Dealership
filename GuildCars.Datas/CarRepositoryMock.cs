using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models;
using GuildCars.Models.Interface;
using SCMS.Datas;

namespace GuildCars.Datas
{
    public class CarRepositoryMock : ICar
    {
        static List<User> _users = new List<User>
        {
            new User { Id = "admin", UserName = "scms", Email = "scms@gmail.com", PasswordHash = "123456"},
            new User { Id = "Na", UserName = "Na", Email = "na@gmail.com", PasswordHash = "123456"},
            new User { Id = "Nik", UserName = "Nik", Email = "nik@gmail.com", PasswordHash = "123456"},
            new User { Id = "Javier", UserName = "Javier", Email = "javier@gmail.com", PasswordHash = "123456"}
        };

        static List<Contact> _contactList = new List<Contact>
        {
            new Contact{ ContactId = 1, Name = "Na", Email = "na@gmail.com", Phone = "952 846 1111", Message = "Message 1"},
            new Contact{ ContactId = 2, Name = "Ko", Email = "ko@gmail.com", Phone = "952 846 2222", Message = "Message 2"},
            new Contact{ ContactId = 3, Name = "My", Email = "my@gmail.com", Phone = "952 846 3333", Message = "Message 3"},
        };
                
        static List<Make> _makeList = new List<Make>
        {
            new Make{ MakeId = 1, Description = "Toyota", AddUser = "1", AddDate = DateTime.Parse("11/01/2017 08:50 AM") },
            new Make{ MakeId = 2, Description = "Saburu", AddUser = "2", AddDate = DateTime.Parse("11/02/2017 09:40 AM") },
            new Make{ MakeId = 3, Description = "Lexus", AddUser = "1", AddDate = DateTime.Parse("11/03/2017 10:30 AM") },
        };

        static List<Model> _modelList = new List<Model>
        {
            new Model{ ModelId = 1, Description = "Camry XSE", MakeId = 1, Make = _makeList[0], AddUser = "1", AddDate = DateTime.Parse("11/03/2017 10:30 AM") },
            new Model{ ModelId = 2, Description = "Camry XLE", MakeId = 1, Make = _makeList[0], AddUser = "1", AddDate = DateTime.Parse("11/04/2017 11:00 AM") },
            new Model{ ModelId = 3, Description = "RX 300", MakeId = 3, Make = _makeList[2], AddUser = "1", AddDate = DateTime.Parse("11/05/2017 11:30 AM") },
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
                , SalePrice = 25000, ReleaseYear = 2015, VinNo = "111111", AddUser = "1", AddDate = DateTime.Parse("11/05/2017 11:30 AM")},
            new Car{ CarId = 2, BodyStyle = 'B', Color = "red", Description = "Camry XLE 2016", Interior = "gray", Mileage = 50, Type = 'C', Transmision = 'T', ModelId = 1, Picture = null, MSRP = 0
                , SalePrice = 35000, ReleaseYear = 2016, VinNo = "222222", AddUser = "1", AddDate = DateTime.Parse("11/05/2017 11:30 AM")},
            new Car{ CarId = 3, BodyStyle = 'B', Color = "red", Description = "Camry SE 2017", Interior = "white", Mileage = 50, Type = 'C', Transmision = 'T', ModelId = 1, Picture = null, MSRP = 0
                , SalePrice = 45000, ReleaseYear = 2017, VinNo = "333333", AddUser = "1", AddDate = DateTime.Parse("11/05/2017 11:30 AM")},
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
                , StateAbbreviation = "MN", State = _stateList[0], Street1 = "1008 Thrist Lane", PurchasePrice = 37000, PurchaseType = 'D', UserId = "Na", AddDate = DateTime.Parse("11/05/2017 11:30 AM")}
        };

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
        public Car GetCarbyId(int carId)
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

        public Contact GetContactbyId(int contactId)
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
        public int AddMake(Make make)
        {
            make.MakeId = _makeList.Count + 1;
            _makeList.Add(make);
            return make.MakeId;
        }
        public bool DeleteMake(int makeId)
        {
            _makeList.RemoveAll(m => m.MakeId == makeId);
            return true;
        }
        public Make GetMakebyId(int makeId)
        {
            return GetMakeList().FirstOrDefault(m => m.MakeId == makeId);
        }

        public List<Make> GetMakeList()
        {
            return _makeList;
        }
        public bool UpdateMake(Make make)
        {
            _makeList.RemoveAll(m => m.MakeId == make.MakeId);
            _makeList.Add(make);
            return true;
        }


        #endregion

        #region "Model"
        public int AddModel(Model model)
        {
            model.ModelId = _modelList.Count + 1;
            _modelList.Add(model);
            return model.ModelId;
        }
        public bool DeleteModel(int modelId)
        {
            _modelList.RemoveAll(m => m.ModelId == modelId);
            return true;
        }
        public Model GetModelbyId(int modelId)
        {
            return GetModelList().FirstOrDefault(m => m.ModelId == modelId);
        }

        public List<Model> GetModelList()
        {
            return _modelList;
        }

        public bool UpdateModel(Model model)
        {
            _modelList.RemoveAll(m => m.ModelId == model.ModelId);
            _modelList.Add(model);
            return true;
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

        public PostalCode GetPostalCodebyId(string postalCode)
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
        public Sale GetSalebyId(int saleId)
        {
            return GetSaleList().FirstOrDefault(s => s.SaleId == saleId);
        }

        public List<Sale> GetSaleList()
        {
            return _saleList;
        }

        public List<Sale> GetSaleListBySaleman(string userId)
        {
            return GetSaleList().Where(s => s.UserId == userId).ToList();
        }

        public bool UpdateSale(Sale sale)
        {
            _saleList.RemoveAll(s => s.SaleId == sale.SaleId);
            _saleList.Add(sale);
            return true;
        }
        #endregion

        #region "Special"
        public int AddSpecial(Special special)
        {
            special.SpecialId = _specialList.Count + 1;
            _specialList.Add(special);
            return special.SpecialId;
        }
        public bool DeleteSpecial(int specialCode)
        {
            _specialList.RemoveAll(s => s.SpecialId == specialCode);
            return true;
        }

        public Special GetSpecialbyId(int specialId)
        {
            return GetSpecialList().FirstOrDefault(s => s.SpecialId == specialId);
        }

        public List<Special> GetSpecialList()
        {
            return _specialList;
        }

        public bool UpdateSpecial(Special special)
        {
            _specialList.RemoveAll(s => s.SpecialId == special.SpecialId);
            _specialList.Add(special);
            return true;
        }


        #endregion

        #region "State"
        public bool AddState(State state)
        {
            _stateList.Add(state);
            return true;
        }

        public State GetStatebyId(string stateAbbreviation)
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
            //Need Modify to get by role
            return GetUserList();
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

        public async Task<bool> Login(string userId, string password)
        {
            if (_users.Any(u => u.Id == userId && u.PasswordHash == password))
            {
                User user = _users.FirstOrDefault(u => u.Id == userId);
                CurrentUser.User = user;
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        #endregion



    }
}
