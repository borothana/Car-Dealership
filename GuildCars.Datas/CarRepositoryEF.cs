using GuildCars.Models.Interface;
using GuildCars.Models;
using GuildCars.Datas.DBContext;
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
using System.IO;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Configuration;

namespace GuildCars.Datas
{
    public class CarRepositoryEF : ICar
    {
        public string CurrentUrl;
        public CarRepositoryEF()
        {
            var request = HttpContext.Current.Request;
            CurrentUrl = "http://" + request.Url.Host + ":" + request.Url.Port;
        }

        CarDBContext _ctx = new CarDBContext();
        
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

        #region "Car"
        public CarVM AddCar(CarVM carVM)
        {
            carVM.AddDate = DateTime.Now;
            carVM.AddUserId = CurrentUser.User.Id;
            if (carVM.UploadFile != null)
            {
                carVM.Picture = ConvertImgToByte(carVM.UploadFile);
            }
            else
            {
                carVM.Picture = null;
            }
            _ctx.Cars.Add(ConvertVMToCar(carVM));
            _ctx.SaveChanges();
            carVM.Result = ReturnSuccess();
            return carVM;
        }

        public CarVM UpdateCar(CarVM carVM)
        {
            carVM.EditDate = DateTime.Now;
            carVM.EditUserId = CurrentUser.User.Id;
            if (carVM.UploadFile != null)
            {
                carVM.Picture = ConvertImgToByte(carVM.UploadFile);
            }
            else
            {
                carVM.Picture = null;
            }
            _ctx.Entry(ConvertVMToCar(carVM)).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            carVM.Result = ReturnSuccess();
            return carVM;
        }

        public bool DeleteCar(int carId)
        {
            Car car = GetCarById(carId);
            if (car != null)
            {
                _ctx.Entry(car).State = System.Data.Entity.EntityState.Deleted;
                _ctx.SaveChanges();
            }
            return true;
        }

        public List<Car> GetCarList()
        {
            return _ctx.Cars.ToList();
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
            foreach (var r in result)
            {
                r.BodyStyle = (r.BodyStyle == "Car" || r.BodyStyle == "C") ? "Car" : (r.BodyStyle == "Truck" || r.BodyStyle == "T") ? "Truck" : (r.BodyStyle == "Van" || r.BodyStyle == "V") ? "Van" : "SUV";
                r.Transmission = (r.Transmission == "Automatic" || r.Transmission == "A") ? "Automatic" : "Manual";
                r.Type = (r.Type == "New" || r.Type == "N") ? "New" : "Used";
                switch (r.Color)
                {
                    case "RED":
                        r.Color = "Red";
                        break;
                    case "BLU":
                        r.Color = "Blue";
                        break;
                    case "BLK":
                        r.Color = "Black";
                        break;
                    default:
                        r.Color = "White";
                        break;
                }

                switch (r.Interior)
                {
                    case "RED":
                        r.Interior = "Red";
                        break;
                    case "BLU":
                        r.Interior = "Blue";
                        break;
                    case "BLK":
                        r.Interior = "Black";
                        break;
                    default:
                        r.Interior = "White";
                        break;
                }
            }
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
            _ctx.Contacts.Add(ConvertVMToContact(contact));
            _ctx.SaveChanges();
            contact.ContactId = _ctx.Contacts.Max(c => c.ContactId);
            contact.Result = ReturnSuccess();
            return contact;
        }

        public Response DeleteContact(ContactVM contact)
        {
            _ctx.Entry(contact).State = System.Data.Entity.EntityState.Deleted;
            _ctx.SaveChanges();
            return ReturnSuccess();
        }

        public ContactVM UpdateContact(ContactVM contact)
        {
            _ctx.Entry(contact).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            contact.Result = ReturnSuccess();
            return contact;
        }

        public List<Contact> GetContactList()
        {
            return _ctx.Contacts.ToList();
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

        #endregion

        #region "Make"
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

            _ctx.Entry(ConvertVMToMake(make)).State = System.Data.Entity.EntityState.Deleted;
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
            _ctx.Set<Make>().AddOrUpdate(ConvertVMToMake(make));
            //_ctx.Entry(tmp).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return make;
        }

        public List<Make> GetMakeList()
        {
            List<Make> result = new List<Make>();
            string cnnstr = ConfigurationManager.ConnectionStrings["CarDBString"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(cnnstr))
            {
                SqlCommand cmd = new SqlCommand("Select MakeId, Description, AddUserId, AddDate, Isnull(EditUserId, ''), EditDate From Makes Order by MakeId", cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Make()
                        {
                            MakeId = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            AddUserId = reader.GetString(2),
                            AddDate = reader.GetDateTime(3),
                            EditUserId = reader.GetString(4),
                            EditDate = reader.GetDateTime(5)
                        });
                    }
                }
                reader.Close();
                cnn.Close();
            }
            return result;
            //return _ctx.Makes.ToList();
        }

        public Make GetMakeById(int makeId)
        {
            return GetMakeList().FirstOrDefault(m => m.MakeId == makeId);
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
                EditDate = input.EditDate.Year < 1900 ? DateTime.Parse("01/01/1900").Date : input.EditDate,
                EditUserId = input.EditUserId,
                MakeId = input.MakeId,
            };
            return result;
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
            _ctx.Set<Model>().AddOrUpdate(ConvertVMToModel(model));
            //_ctx.Entry(ConvertVMToModel(model)).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return model;
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
                EditDate = modelVM.EditDate.Year < 1900 ? DateTime.Parse("01/01/1900") : modelVM.EditDate,
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
            sale.AddDate = DateTime.Now;
            sale.AddUserId = CurrentUser.User.Id;
            sale.EditDate = DateTime.Parse("01/01/1900");
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

        public bool UpdateSale(Sale sale)
        {
            _ctx.Entry(sale).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return true;
        }

        public List<Sale> GetSaleList()
        {
            return _ctx.Sales.ToList();
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
            _ctx.Specials.Add(ConvertVMToSpecial(special));
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
            if (GetSpecialList().Any(m => m.Title == special.Title && m.SpecialId != special.SpecialId))
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
            _ctx.Set<Special>().AddOrUpdate(ConvertVMToSpecial(special));
            //_ctx.Entry(special).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            return special;
        }

        public List<Special> GetSpecialList()
        {
            return _ctx.Specials.ToList();
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

        public User ConvertVMToUser(UserVM input)
        {
            User result = new User()
            {
                Id = input.Id,
                PasswordHash = input.PasswordHash,
                UserName = input.UserName,
                IsActive = input.IsActive
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
                IsActive = input.IsActive,
                Result = ReturnSuccess()
            };
            return result;
        }

        public UserVM AddUser(UserVM user, string role)
        {
            var userMgr = new UserManager<GuildCars.Models.User>(new UserStore<GuildCars.Models.User>(_ctx));

            //if user name or email not existed
            if (!userMgr.Users.Any(u => u.UserName == user.UserName))
            {
                user.IsActive = true;
                user.PasswordHash = userMgr.PasswordHasher.HashPassword(user.PasswordHash);
                userMgr.Create(ConvertVMToUser(user));

                var tmpuser = userMgr.Users.Single(u => u.UserName == user.UserName);
                if (!tmpuser.Roles.Any(r => r.RoleId == role))
                {
                    userMgr.AddToRole(tmpuser.Id, role);
                    return ConvertUserToVM(tmpuser);
                }
            }

            UserVM tmp = new UserVM();
            tmp.Result = ReturnSuccess();
            tmp.Result.Success = false;
            tmp.Result.ErrorMessage = "Cannot create user";
            return null;
        }

        public bool UpdateUser(User user)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User userTmp = userMgr.FindById(user.Id);
            if (userTmp == null)
            {
                return false;
            }
            var result = userMgr.Update(userTmp);
            return result.Succeeded;
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = userMgr.Find(userName, currentPassword);
            if (user != null)
            {
                user.PasswordHash = userMgr.PasswordHasher.HashPassword(newPassword);
                var result = userMgr.Update(user);
                return result.Succeeded;
            }
            return false;
        }

        public bool DeactivateUser(string userName)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = userMgr.FindByName(userName);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false;
            var result = userMgr.Update(user);
            return result.Succeeded;
        }


        public bool ReactivateUser(string userName)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = userMgr.FindByName(userName);
            if (user == null)
            {
                return false;
            }
            user.IsActive = true;
            var result = userMgr.Update(user);
            return result.Succeeded;
        }

        public bool DeleteUser(string userId)
        {
            var userMgr = new UserManager<User>(new UserStore<User>(_ctx));
            User user = userMgr.FindById(userId);
            var logins = user.Logins;
            var rolesForUser = userMgr.GetRoles(userId);

            using (var transaction = _ctx.Database.BeginTransaction())
            {
                foreach (var login in logins.ToList())
                {
                    userMgr.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = userMgr.RemoveFromRole(user.Id, item);
                    }
                }

                userMgr.Delete(user);
                transaction.Commit();
            }
            return true;
        }
        #endregion


    }
}
