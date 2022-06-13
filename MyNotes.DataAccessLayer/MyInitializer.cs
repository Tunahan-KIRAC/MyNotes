using MyNotes.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<MyNoteContext>
    {
        protected override void Seed(MyNoteContext context)
        {
            MyNotesUser admin = new MyNotesUser()
            {
                Name = "Onur",
                LastName = "Agici",
                Email = "9731013@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                ProfileImageFileName = "users.jpg",
                UserName = "onuragici",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "system"
            };
            MyNotesUser stdUser = new MyNotesUser()
            {
                Name = "Recep",
                LastName = "Ivedik",
                Email = "recebim57@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                UserName = "recebim57",
                Password = "654321",
                ProfileImageFileName = "users.jpg",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "system"
            };
            context.MyNotesUsers.Add(admin);
            context.MyNotesUsers.Add(stdUser);

            for (int i = 0; i < 8; i++)
            {
                MyNotesUser user = new MyNotesUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    LastName = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    UserName = $"user-{i}",
                    Password = "123",
                    ProfileImageFileName = "users.jpg",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user-{i}"
                };
                context.MyNotesUsers.Add(user);
            }
            context.SaveChanges();
            List<MyNotesUser> userList = context.MyNotesUsers.ToList();



            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Tittle = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "onuragici"
                };
                context.Categories.Add(cat);
                //Adding fake Notes...
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    MyNotesUser owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Tittle = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 20)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        isDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CategoryId = FakeData.NumberData.GetNumber(1, 9),
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.UserName
                    };
                    cat.Notes.Add(note);
                    //Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        MyNotesUser comment_owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = comment_owner.UserName
                        };
                        note.Comments.Add(comment);
                    }
                    //Adding fake likes....
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userList[m]
                        };
                        note.Likes.Add(liked);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
