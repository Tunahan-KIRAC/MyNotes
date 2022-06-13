using MyNotes.BusinessLayer.Abstract;
using MyNotes.BusinessLayer.ValueObject;
using MyNotes.Common.Helper;
using MyNotes.EntityLayer;
using MyNotes.EntityLayer.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.BusinessLayer
{
    public class MyNotesUserManager : ManagerBase<MyNotesUser>
    {
        //Kullanıcı username kontrolu yapmalıyım
        //Kullanici emaili kontrolu yapmalıyım
        //Kayit islemi gerceklestirmeliyim
        //activation e-posta gonderiimi
        private BusinessLayerResult<MyNotesUser> res = new BusinessLayerResult<MyNotesUser>();
        public BusinessLayerResult<MyNotesUser> LoginUser(LoginViewModel data)
        {
            res.Result = Find(s => s.UserName == data.UserName && s.Password == data.Password && s.IsDelete!=true);
            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.UserIsNotActive, "Kullanici adi aktiflestirilmemis...");
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.CheckYourEmail, "Lutfen mailinizi kontrol ediniz!");
                }


            }
            else
            {
                res.AddError(EntityLayer.Messages.ErrorMessageCode.UsernameOrPassWrong, "Kullanici adi yada sifreniz yanlis lutfen kontrol edin...");
            }
            return res;

        }
        public BusinessLayerResult<MyNotesUser> RegisterUser(RegisterViewModel data)
        {
            MyNotesUser user = Find(s => s.UserName == data.Username || s.Email == data.Email);
            if (user != null)
            {
                if (user.UserName == data.Username)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.UserNameAlreadyExist, "Bu kullanici adi daha once alinmis");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.EmailAlreadyExist, "bu email daha once kullanilmis");
                }
            }
            else
            {
                int dbResult = base.Insert(new MyNotesUser()
                {
                    Name = data.Name,
                    LastName = data.Lastname,
                    Email = data.Email,
                    UserName = data.Username,
                    Password = data.Password,
                    IsActive = false,
                    IsAdmin = false,
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImageFileName = "users.jpg"
                });
                if (dbResult > 0)
                {
                    res.Result = Find(s => s.Email == data.Email && s.UserName == data.Username);
                    //Activasyon Maili Gonderilecek
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.UserName};<br><br> Hesabinizi aktiflestirmek icin <a href='{activateUri}' target='_blank'>bu linke tiklayin</a>.";
                    MailHelper.SendMail(body, res.Result.Email, "My Notes Activation Mail Hizmeti");
                }
            }
            return res;
        }
        public BusinessLayerResult<MyNotesUser> ActivateUser(Guid id)
        {
            res.Result = Find(s => s.ActivateGuid == id);
            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.UserAlreadyActive, "Kullanici zaten Aktif");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(EntityLayer.Messages.ErrorMessageCode.ActivateIdDoesNotExist, "Böyle bir aktivasyon kodu yoktur");
            }
            return res;
        }
        public new BusinessLayerResult<MyNotesUser> Insert(MyNotesUser data)
        {
            MyNotesUser user = Find(s => s.UserName == data.UserName || s.Email == data.Email);
            res.Result = data;
            if (user != null)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.UserNameAlreadyExist, "Bu kullanici adi daha once alinmis");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.EmailAlreadyExist, "Bu email daha once alinmis");

                }
            }
            else
            {
                res.Result.ActivateGuid = Guid.NewGuid();
                if (base.Insert(res.Result) == 0)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.UserCouldNotInserted, "Kullanici Eklenemedi");
                }
            }
            return res;
        }
        public new BusinessLayerResult<MyNotesUser> Update(MyNotesUser data)
        {
            MyNotesUser user = Find(s => s.Id != data.Id && (s.UserName == data.UserName || s.Email == data.Email));
            if (user != null && user.Id != data.Id)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.UserNameAlreadyExist, "Bu kullanici adını alamazsınız");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(EntityLayer.Messages.ErrorMessageCode.EmailAlreadyExist, "Bu email adını alamazsınız");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.LastName = data.LastName;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            res.Result.IsAdmin = data.IsAdmin;
            res.Result.IsActive = data.IsActive;

            if (base.Update(res.Result) == 0)
            {
                res.AddError(EntityLayer.Messages.ErrorMessageCode.UserCouldNotUpdated, "Kullanici bilgileri guncellenemedi");
            }
            return res;
        }

        public BusinessLayerResult<MyNotesUser> GetUserById(int id)
        {
            res.Result = Find(s => s.Id == id);
            if (res.Result == null)
            {
                res.AddError(EntityLayer.Messages.ErrorMessageCode.UserNotFound, "Kullanici bulunamadi");
            }
            return res;
        }

        public BusinessLayerResult<MyNotesUser> RemoveUserById(int id)
        {
            res.Result = Find(s => s.Id == id);
            if (res.Result != null)
            {
                res.Result.IsDelete = true;
                if (base.Update(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanici silinemedi.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Boyle bir kullanici bulunamadi..");
            }

            return res;
        }
        public BusinessLayerResult<MyNotesUser> SendMail(LoginViewModel data)
        {
            res.Result = Find(s => s.Password == data.Password && s.UserName == data.UserName
            );
            //Activasyon maili gonderilecek

            string siteUri = ConfigHelper.Get<string>("SiteRootUri");
            string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
            string body =
                $"Merhaba {res.Result.UserName};<br><br> Hesabinizi aktiflestirmek icin <a href='{activateUri}' target='_blank'>bu linke tiklayin</a>.";
            MailHelper.SendMail(body, res.Result.Email, "MyNotes Aktivasyon mail hizmet");

            return res;
        }
        public BusinessLayerResult<MyNotesUser> UpdateProfile(MyNotesUser data)
        {
            MyNotesUser user = Find(s => s.Id != data.Id && (s.UserName == data.UserName || s.Email == data.Email));
            if (user != null && user.Id != data.Id)
            {
                if (user.UserName == data.UserName)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Bu kullanici adi daha once kaydedilmis.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu email daha once kaydedilmis.");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.LastName = data.LastName;
            res.Result.Password = data.Password;
            res.Result.UserName = data.UserName;
            res.Result.IsDelete = data.IsDelete;
            if (!string.IsNullOrEmpty(data.ProfileImageFileName))
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil guncellenemedi.");
            }

            return res;
        }
    }
}