using Cheremushkinae_107d2.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace Cheremushkinae_107d2.Model
{
    public static class DataWorker
    {
        // add user
        public static string CreateUserInDB(string username, string password, string email)
        {
            string result = "Пользователь уже существует либо произошла непредвиденная ошибка";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем, существует ли пользователь
                bool checkIsExist = db.Users.Any(el => el.Username == username);
                if (!checkIsExist) 
                {
                    User newUser = new User { Username = username, Password = password, Email = email};
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    // Получаем ID только что созданного пользователя
                    GlobalSettings.SavedUserID = newUser.ID_user;
                    GlobalSettings.SavedUsername = newUser.Username;

                    result = "Сделано";
                }
            }
            return result;
        }
        // add new word
        public static string AddNewWordInDB(string word_in_english, string word_in_russian, string using_example, User user)
        {
            string result = "Такое слово на английском языке уже было добавлено";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем, существует ли слово на английском
                bool checkIsExist = db.LearnDicts.Any(el => el.Word_in_English == word_in_english);
                if (!checkIsExist)
                {
                    LearnDict newLearnDict = new LearnDict {
                        ID_user = user.ID_user,
                        Word_in_English = word_in_english,
                        Word_in_Russian = word_in_russian,
                        Using_example = using_example,
                        Right_answers_count = 0 };
                    db.LearnDicts.Add(newLearnDict);
                    db.SaveChanges();
                    result = "Сделано";
                }
            }
            return result;
        }
        // add word in known
        public static string AddKnownWordInDB(string word_in_english, string word_in_russian, string using_example, User user)
        {
            string result = "Сделано";
            using (ApplicationContext db = new ApplicationContext())
            {
                KnowDict newKnowDict = new KnowDict
                {
                    ID_user = user.ID_user,
                    Word_in_English = word_in_english,
                    Word_in_Russian = word_in_russian,
                    Using_example = using_example
                };
                db.KnowDicts.Add(newKnowDict);
                db.SaveChanges();

            }
            return result;
        }
        // delete user
        public static string DeleteUserInDB(string username)
        {
            string result = "Такого пользователя не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username == username);
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Пользователь с ником " + user.Username + " удален";

                
            }
            return result;
        }
        // delete word in learning
        public static string DeleteLearningWordInDB(LearnDict learnDict)
        {
            string result = "Такого слова вы не добавляли";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.LearnDicts.Remove(learnDict);
                db.SaveChanges();
                result = "cлово " + learnDict.Word_in_English + " удалено";
 
            }
            return result;
        }
        // delete word in known
        public static string DeleteKnownWordInDB(KnowDict knowDict)
        {
            string result = "Такого слова в выученных не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.KnowDicts.Remove(knowDict);
                db.SaveChanges();
                result = "cлово " + knowDict.Word_in_English + " удалено";
   
            }
            return result;
        }

        // take all Learning words
        public static List<LearnDict> GetAllLearningWords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.LearnDicts.ToList();
                return result;
            }
            
        }
        // take all known words
        public static List<KnowDict> GetAllKnowWords()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.KnowDicts.ToList();
                return result;
            }

        }
    }
}
