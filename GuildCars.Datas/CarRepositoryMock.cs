using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System  .Threading.Tasks;
using GuildCars.Models;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.IO;

namespace GuildCars.Datas
{
    public class CarRepositoryMock : ICar
    {
        public string CurrentUrl;
        public CarRepositoryMock()
        {
            var request = HttpContext.Current.Request;
            CurrentUrl = "http://" + request.Url.Host + ":" + request.Url.Port;

            var ctx = new DBContext.CarDBContext();
            var adminDB = ctx.Users.First(u => u.UserName == "admin");
            var adminMock = _users.First(u => u.UserName == "admin");
            adminMock.Id = adminDB.Id;

            var saleDB = ctx.Users.First(u => u.UserName == "sale");
            var saleMock = _users.First(u => u.UserName == "sale");
            saleMock.Id = saleDB.Id;
        }

        static List<User> _users = new List<User>
        {
            new User { Id = "f830d1f6-4c74-4041-a460-05b2c0360f1d", UserName = "admin", Email = "admin@gmail.com", PasswordHash = "12345678", IsActive = true},
            new User { Id = "8728cb3d-8e99-4da6-bc70-2e7995af34ae", UserName = "sale", Email = "sale@gmail.com", PasswordHash = "12345678", IsActive = true},
            new User { Id = "3", UserName = "Nik", Email = "nik@gmail.com", PasswordHash = "12345678", IsActive = true},
            new User { Id = "4", UserName = "Javier", Email = "javier@gmail.com", PasswordHash = "12345678", IsActive = true}
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

        static List<Car> _carList = new List<Car>
        {
            new Car{ CarId = 1, BodyStyle = "C", Color = "red", Description = "Camry XSE 2015", Interior = "red", Mileage = 50, Type = "N",
                Transmission = "A", ModelId = 1, Picture = null, MSRP = 20000, SalePrice = 25000, ReleaseYear = 2016, VinNo = "1HGBH41JXMN109181",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 2, BodyStyle = "C", Color = "red", Description = "Camry XLE 2016", Interior = "gray", Mileage = 150, Type = "N",
                Transmission = "A", ModelId = 1, Picture = null, MSRP = 30000, SalePrice = 35000, ReleaseYear = 2016, VinNo = "1HGBH41JXMN109182",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 3, BodyStyle = "T", Color = "red", Description = "Ford Truck", Interior = "white", Mileage = 250, Type = "U",
                Transmission = "A", ModelId = 2, Picture = null, MSRP = 40000, SalePrice = 55000, ReleaseYear = 2017, VinNo = "1HGBH41JXMN109183",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 4, BodyStyle = "T", Color = "red", Description = "Toyota Truck", Interior = "white", Mileage = 300, Type = "U",
                Transmission = "A", ModelId = 2, Picture = null, MSRP = 40000, SalePrice = 65000, ReleaseYear = 2017, VinNo = "1HGBH41JXMN109184",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 5, BodyStyle = "V", Color = "red", Description = "Toyota Siana", Interior = "red", Mileage = 350, Type = "N",
                Transmission = "M", ModelId = 1, Picture = null, MSRP = 40000, SalePrice = 75000, ReleaseYear = 2018, VinNo = "1HGBH41JXMN109185",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 6, BodyStyle = "S", Color = "red", Description = "Lexus RX300", Interior = "red", Mileage = 50, Type = "N",
                Transmission = "M", ModelId = 1, Picture = null, MSRP = 40000, SalePrice = 85000, ReleaseYear = 2018, VinNo = "1HGBH41JXMN109186",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 7, BodyStyle = "V", Color = "red", Description = "Camry SE 2017", Interior = "gray", Mileage = 150, Type = "U",
                Transmission = "M", ModelId = 3, Picture = null, MSRP = 40000, SalePrice = 45000, ReleaseYear = 2015, VinNo = "1HGBH41JXMN109187",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 8, BodyStyle = "S", Color = "red", Description = "Camry SE 2017", Interior = "gray", Mileage = 250, Type = "U",
                Transmission = "M", ModelId = 3, Picture = null, MSRP = 40000, SalePrice = 55000, ReleaseYear = 2015, VinNo = "1HGBH41JXMN109188",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
            new Car{ CarId = 9, BodyStyle = "S", Color = "red", Description = "Camry SE 2016", Interior = "gray", Mileage = 350, Type = "U",
                Transmission = "M", ModelId = 3, Picture = null, MSRP = 40000, SalePrice = 55000, ReleaseYear = 2015, VinNo = "1HGBH41JXMN109189",
                AddUserId = "f830d1f6-4c74-4041-a460-05b2c0360f1d", AddDate = DateTime.Parse("11/05/2017 11:30 AM"), IsFeature = true},
        };
        
        static List<Sale> _saleList = new List<Sale>
        {
            new Sale{ SaleId = 1, CarId = 1, car = _carList[0], CustomerName = "Borothana", Email = "borothana@gmail.com", Phone = "952 846 9047", ZipCode = "55379"
                , State = "MN", Street1 = "1008 Thrist Lane", PurchasePrice = 37000, PurchaseType = "D", AddUserId = "8728cb3d-8e99-4da6-bc70-2e7995af34ae", AddDate = DateTime.Parse("11/05/2017 11:30 AM")}
        };

        static List<Special> _specialList = new List<Special>
        {
            new Special{ SpecialId = 1, Title = "Mid-Sale", Description = "Discount for mid-sale....", FDate = DateTime.Parse("11/15/2017"), TDate = DateTime.Parse("12/31/2017"), image = null },
            new Special{ SpecialId = 2, Title = "New Year", Description = "Discount for New Year....", FDate = DateTime.Parse("11/01/2017"), TDate = DateTime.Parse("12/01/2017"), image = null },
            new Special{ SpecialId = 3, Title = "Black Friday", Description = "This Black Friday....", FDate = DateTime.Parse("11/10/2017"), TDate = DateTime.Parse("12/10/2017"), image = null },
            new Special{ SpecialId = 4, Title = "Mid-Winter Sale", Description = "January is coming, ....", FDate = DateTime.Parse("11/01/2017"), TDate = DateTime.Parse("12/01/2017"), image = null },
            new Special{ SpecialId = 5, Title = "10th Years Anniverary", Description = "10th Years Anniverary....", FDate = DateTime.Parse("11/10/2017"), TDate = DateTime.Parse("12/10/2017"), image = null },
        };

        static List<Contact> _contactList = new List<Contact>
        {
            new Contact{ ContactId = 1, Name = "Na", Email = "na@gmail.com", Phone = "952 846 1111", Message = "1HGBH41JXMN109187 Message 1"},
            new Contact{ ContactId = 2, Name = "Ko", Email = "ko@gmail.com", Phone = "952 846 2222", Message = "1HGBH41JXMN109185 Message 2"},
            new Contact{ ContactId = 3, Name = "My", Email = "my@gmail.com", Phone = "952 846 3333", Message = "1HGBH41JXMN109186 Message 3"},
        };
        
        #region "Report"
        public List<InventoryReportVM> GetInventoryReport()
        {
            return (from i in GetCarVMList()
                    group i by new { i.ReleaseYear, i.Make, i.Model, i.Type } into r
                    select new InventoryReportVM { ReleaseYear = r.Key.ReleaseYear, CarMake = r.Key.Make, CarModel = r.Key.Model, Type = r.Key.Type, QTY = r.Count(), Amount = r.Sum(c => c.MSRP) }).ToList();
        }

        public List<SaleReportVM> GetSaleReport(string UserName, DateTime fd, DateTime td)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return (from i in GetSaleVMList()
                        where i.AddDate >= fd && i.AddDate <= td
                        group i by new { i.AddUser } into r
                        select new SaleReportVM { User = r.Key.AddUser, QTY = r.Count(), Amount = r.Sum(c => c.PurchasePrice) }).ToList();
            }
            else
            {
                User user = GetUserById(UserName);
                return (from i in GetSaleVMList()
                        where i.AddUserId == user.Id && i.AddDate >= fd && i.AddDate <= td
                        group i by new { i.AddUser } into r
                        select new SaleReportVM { User = r.Key.AddUser, QTY = r.Count(), Amount = r.Sum(c => c.PurchasePrice) }).ToList();
            }
        }
        #endregion

        #region "Other"
        public Response ReturnSuccess()
        {
            return new Response() { Success = true, ErrorMessage = "" };
        }

        public byte[] ConvertImgToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

        public bool Login(string userName, string password)
        {
            if (_users.Any(u => u.UserName == userName && u.PasswordHash == password))
            {
                var userMgr = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<User>>();
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

        #region "Car"               
        public CarVM AddCar(CarVM carVM)
        {
            carVM.CarId = _carList.Count + 1;
            if (carVM.UploadFile != null)
            {
                carVM.Picture = ConvertImgToByte(carVM.UploadFile);
            }
            else
            {
                carVM.Picture = null;
            }
            _carList.Add(ConvertVMToCar(carVM));
            carVM.Result = ReturnSuccess();
            return carVM;
        }

        public CarVM UpdateCar(CarVM carVM)
        {
            _carList.RemoveAll(c => c.CarId == carVM.CarId);
            if (carVM.UploadFile != null)
            {
                carVM.Picture = ConvertImgToByte(carVM.UploadFile);
            }
            else
            {
                carVM.Picture = null;
            }
            _carList.Add(ConvertVMToCar(carVM));
            carVM.Result = ReturnSuccess();
            return carVM;
        }

        public bool DeleteCar(int carId)
        {
            _carList.RemoveAll(c => c.CarId == carId);
            return true;
        }

        public List<Car> GetCarList()
        {
            return _carList;
        }

        public Car GetCarById(int carId)
        {
            return GetCarList().FirstOrDefault(c => c.CarId == carId);
        }

        public List<CarVM> GetCarVMList()
        {
            List<CarVM> result = new List<CarVM>();
            List<Car> car = GetCarList();
            foreach (Car c in car)
            {
                result.Add(ConvertCarToVM(c));
            }
            return result;
        }

        public List<CarVM> GetCarVMList(string filter, int minPrice, int maxPrice, int minYear, int maxYear, string type)
        {
            int Year = 0;
            int.TryParse(filter, out Year);
            var result = GetCarVMList().Where(c => (type != "S" ? c.Type.Contains(type) : c.IsPurchase == false) &&
                                            ((filter != "ALL" ? c.Make.Description.Contains(filter) : true) ||
                                             (filter != "ALL" ? c.Model.Description.Contains(filter) : true) ||
                                                (Year > 0 ? c.ReleaseYear == Year : true)
                                            ) &&
                                            (minPrice > 0 ? c.SalePrice >= minPrice : true) &&
                                            (maxPrice > 0 ? c.SalePrice <= maxPrice : true) &&
                                            (minYear > 0 ? c.ReleaseYear >= minYear : true) &&
                                            (maxYear > 0 ? c.ReleaseYear <= maxYear : true)
                                        ).Take(20).OrderBy(c => c.MSRP).ToList();
            return result;
        }

        public CarVM GetCarVMById(int carId)
        {
            CarVM result = GetCarVMList().FirstOrDefault(c => c.CarId == carId);
            return result;
        }


        public Car ConvertVMToCar(CarVM model)
        {
            Car result = new Car()
            {
                AddDate = model.AddDate,
                AddUserId = model.AddUserId,
                //BodyStyle = (model.BodyStyle == "Car" || model.BodyStyle == "C") ? "C" : (model.BodyStyle == "Truck" || model.BodyStyle == "T") ? "T" : (model.BodyStyle == "Van" || model.BodyStyle == "V") ? "V" : "S",
                //Transmission = (model.Transmission == "Automatic" || model.Transmission == "A") ? "A" : "M",
                //Type = (model.Type == "New" || model.Type == "N") ? "N" : "U",
                BodyStyle = model.BodyStyle,
                Transmission = model.Transmission,
                Type = model.Type,
                CarId = model.CarId,
                Color = model.Color,
                Description = model.Description,
                EditDate = model.EditDate.Year > 1900 ? model.EditDate : DateTime.Parse("01/01/1900"),
                EditUserId = model.EditUserId,
                Interior = model.Interior,
                Mileage = model.Mileage,
                ModelId = model.ModelId,
                MSRP = model.MSRP,
                Picture = model.Picture,
                ReleaseYear = model.ReleaseYear,
                SalePrice = model.SalePrice,
                VinNo = model.VinNo,
                IsFeature = model.IsFeature
            };
            return result;
        }

        public CarVM ConvertCarToVM(Car model)
        {
            String path = HttpContext.Current.Server.MapPath("~/Content/Images/");
            CarVM result = new CarVM()
            {
                AddDate = model.AddDate,
                AddUserId = model.AddUserId,
                //BodyStyle = model.BodyStyle == "C" ? "Car" : model.BodyStyle == "T" ? "Truck" : model.BodyStyle == "V" ? "Van" : "SUV",
                BodyStyle = model.BodyStyle,
                CarId = model.CarId,
                Color = model.Color,
                Description = model.Description,
                EditDate = model.EditDate,
                EditUserId = model.EditUserId,
                Interior = model.Interior,
                Mileage = model.Mileage,
                ModelId = model.ModelId,
                MSRP = model.MSRP,
                ReleaseYear = model.ReleaseYear,
                SalePrice = model.SalePrice,
                //Transmission = model.Transmission == "A" ? "Automatic" : "Manual",
                //Type = model.Type == "N" ? "New" : "Used",
                Transmission = model.Transmission,
                Type = model.Type,
                VinNo = model.VinNo,
                IsFeature = model.IsFeature,
                Picture = model.Picture,
                PictureUrl = path + "Inventory_" + model.CarId + ".jpg",
                PictureName = "Inventory_" + model.CarId + ".jpg",
                IsPurchase = GetSaleList().Count(s => s.CarId == model.CarId) > 0,
                AddUser = GetUserById(model.AddUserId),
                Model = GetModelById(model.ModelId),
                Make = GetMakeById(GetModelById(model.ModelId).MakeId),
                Result = ReturnSuccess(),
                MakeId = GetModelById(model.ModelId).MakeId
            };


            if (result.Picture != null)
            {
                using (BinaryWriter b = new BinaryWriter(File.OpenWrite(result.PictureUrl)))
                {
                    b.Write(result.Picture);
                    b.Flush();
                    b.Close();
                }
                result.PictureUrl = @CurrentUrl + "/Content/Images/" + result.PictureName;
            }
            else
            {
                result.PictureUrl = @CurrentUrl + "/Content/Images/no-image.png";
            }

            return result;
        }
        #endregion

        #region "Contact"
        public ContactVM AddContact(ContactVM contact)
        {
            contact.Result = ReturnSuccess();
            contact.ContactId = _contactList.Count + 1;
            _contactList.Add(ConvertVMToContact(contact));
            return contact;
        }

        public Response DeleteContact(ContactVM contact)
        {
            contact.Result = ReturnSuccess();
            _contactList.RemoveAll(c => c.ContactId == contact.ContactId);
            return ReturnSuccess();
        }

        public ContactVM UpdateContact(ContactVM contact)
        {
            contact.Result = ReturnSuccess();
            _contactList.RemoveAll(c => c.ContactId == contact.ContactId);
            _contactList.Add(ConvertVMToContact(contact));
            return contact;
        }

        public List<Contact> GetContactList()
        {
            return _contactList;
        }

        public ContactVM ConvertContactToVM(Contact input)
        {
            ContactVM result = new ContactVM()
            {
                ContactId = input.ContactId,
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                Message = input.Message,

                Result = ReturnSuccess()
            };
            return result;
        }
        public Contact ConvertVMToContact(ContactVM input)
        {
            Contact result = new Contact()
            {
                ContactId = input.ContactId,
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                Message = input.Message
            };
            return result;
        }

        public Contact GetContactById(int contactId)
        {
            return GetContactList().FirstOrDefault(m => m.ContactId == contactId);
        }


        public List<ContactVM> GetContactVMList()
        {
            List<ContactVM> result = new List<ContactVM>();
            List<Contact> contact = GetContactList();
            foreach (Contact c in contact)
            {
                result.Add(ConvertContactToVM(c));
            }
            return result;
        }


        #endregion

        #region "Make"
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
        #endregion

        #region "Model"      
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


        public Model GetModelById(int modelId)
        {
            return GetModelList().FirstOrDefault(m => m.ModelId == modelId);
        }

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

        public bool UpdateSale(Sale sale)
        {
            _saleList.RemoveAll(s => s.SaleId == sale.SaleId);
            _saleList.Add(sale);
            return true;
        }

        public List<Sale> GetSaleList()
        {
            return _saleList;
        }

        public Sale GetSaleById(int saleId)
        {
            return GetSaleList().FirstOrDefault(s => s.SaleId == saleId);
        }

        public List<Sale> GetSaleListBySaleman(string userId)
        {
            return GetSaleList().Where(s => s.AddUserId == userId).ToList();
        }

        public List<SaleVM> GetSaleVMList()
        {
            List<SaleVM> result = new List<SaleVM>();
            List<Sale> sale = GetSaleList();
            foreach (Sale s in sale)
            {
                result.Add(new SaleVM
                {
                    AddDate = s.AddDate,
                    AddUser = GetUserById(s.AddUserId),
                    AddUserId = s.AddUserId,
                    car = GetCarById(s.CarId),
                    CarId = s.CarId,
                    CustomerName = s.CustomerName,
                    EditDate = s.EditDate,
                    EditUserId = s.EditUserId,
                    Email = s.Email,
                    Phone = s.Phone,
                    PurchasePrice = s.PurchasePrice,
                    PurchaseType = s.PurchaseType,
                    SaleId = s.SaleId,
                    State = s.State,
                    Street1 = s.Street1,
                    Street2 = s.Street2,
                    ZipCode = s.ZipCode
                });
            }
            return result;
        }
        #endregion

        #region "Special"
        public SpecialVM AddSpecial(SpecialVM special)
        {
            special.Result = ReturnSuccess();
            if (GetSpecialList().Any(m => m.Description == special.Description))
            {
                special.Result.Success = false;
                special.Result.ErrorMessage = special.Description + " is alreay existed";
                return special;
            }
            if (special.UploadFile != null)
            {
                special.Image = ConvertImgToByte(special.UploadFile);
            }
            else
            {
                special.Image = null;
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
            if (special.UploadFile != null)
            {
                special.Image = ConvertImgToByte(special.UploadFile);
            }
            else
            {
                special.Image = null;
            }
            _specialList.RemoveAll(m => m.SpecialId == special.SpecialId);
            _specialList.Add(ConvertVMToSpecial(special));
            special.Result = ReturnSuccess();
            return special;
        }

        public List<Special> GetSpecialList()
        {
            return _specialList;
        }

        public Special GetSpecialById(int SpecialId)
        {
            return GetSpecialList().FirstOrDefault(m => m.SpecialId == SpecialId);
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

        public List<SpecialVM> GetSpecialVMList(DateTime date)
        {
            return GetSpecialVMList().Where(s => s.FDate.Value <= date.Date && s.TDate.Value >= date.Date).ToList();
        }

        public SpecialVM ConvertSpecialToVM(Special input)
        {
            String path = HttpContext.Current.Server.MapPath("~/Content/Images/");
            SpecialVM result = new SpecialVM()
            {
                SpecialId = input.SpecialId,
                Title = input.Title,
                Description = input.Description,
                FDate = input.FDate,
                TDate = input.TDate,
                Image = input.image,
                ImageName = "Special_" + input.SpecialId + ".jpg",
                ImageSrc = path + "/Special_" + input.SpecialId + ".jpg",
                Result = ReturnSuccess()
            };
            if (result.Image != null)
            {
                using (BinaryWriter b = new BinaryWriter(File.OpenWrite(result.ImageSrc)))
                {
                    b.Write(result.Image);
                    b.Flush();
                    b.Close();
                }
                result.ImageSrc = @CurrentUrl + "/Content/Images/" + result.ImageName;
            }
            else
            {
                result.ImageSrc = @CurrentUrl + "/Content/Images/special.jpg";
            }
            return result;
        }

        public Special ConvertVMToSpecial(SpecialVM input)
        {
            Special result = new Special()
            {
                SpecialId = input.SpecialId,
                Title = input.Title,
                FDate = input.FDate.Value,
                TDate = input.TDate.Value,
                Description = input.Description,
                image = input.Image,
            };
            return result;
        }
        #endregion

        #region "User"
        public User ConvertVMToUser(UserVM input)
        {
            User result = new User()
            {
                Id = input.Id,
                PasswordHash = input.PasswordHash,
                UserName = input.UserName
            };
            return result;
        }

        public UserVM ConvertUserToVM(User input)
        {
            UserVM result = new UserVM()
            {
                Id = input.Id,
                PasswordHash = input.PasswordHash,
                UserName = input.UserName,
                Result = ReturnSuccess()
            };
            return result;
        }

        public List<User> GetUserList()
        {
            return _users;
        }

        public List<User> GetUserListByRole(string role)
        {
            //return GetUserList().Where(u => u.Roles.Any(r => r.RoleId == role)).ToList();
            return GetUserList().ToList();
        }

        public User GetUserById(string userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName == userName);
        }

        public UserVM AddUser(UserVM user, string role)
        {
            user.Id = _users.Max(c => c.Id) + 1;
            _users.Add(ConvertVMToUser(user));
            user.Result = ReturnSuccess();
            return user;
        }

        public bool DeactivateUser(string userName)
        {
            User user = GetUserByUserName(userName);
            if (user != null)
            {
                user.IsActive = false;
                _users.RemoveAll(u => u.Id == user.Id);
                _users.Add(user);
            }
            return true;
        }

        public bool ReactivateUser(string userName)
        {
            User user = GetUserByUserName(userName);
            if (user != null)
            {
                user.IsActive = true;
                _users.RemoveAll(u => u.Id == user.Id);
                _users.Add(user);
            }
            return true;
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            User user = GetUserByUserName(userName);
            if (user != null && user.PasswordHash == currentPassword)
            {
                user.PasswordHash = newPassword;
                _users.RemoveAll(u => u.Id == user.Id);
                _users.Add(user);
                return true;
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            _users.RemoveAll(u => u.Id == user.Id);
            _users.Add(user);
            return true;
        }

        public bool DeleteUser(string userId)
        {
            _users.RemoveAll(u => u.Id == userId);
            return true;
        }


        #endregion
    }
}
